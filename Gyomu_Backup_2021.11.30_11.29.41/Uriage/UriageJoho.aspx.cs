using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;

namespace Gyomu.Uriage
{
    public partial class UriageJoho : System.Web.UI.Page
    {
        const int LIST_ID_DL = 45;

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                SessionManager.JucyuSyusei("");
                SessionManager.MitumoriType("");

                Create();
            }
        }

        private void Create()
        {
            ClassKensaku.KensakuParam k = SetKensakuParam();

            DataUriage.T_UriageDataTable dt =
                ClassKensaku.GetUriage(k, Global.GetConnection());

            if (dt.Rows.Count == 0)
            {
                lblMsg.Text = "データがありません";
                this.RadG.Visible = false;
                return;
            }
            else
            {
                this.RadG.Visible = true;
            }

            this.RadG.VirtualItemCount = dt.Count;

            int nPageSize = this.RadG.PageSize;
            int nPageCount = dt.Count / nPageSize;
            if (0 < dt.Count % nPageSize) nPageCount++;
            if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

            DataTable dtC = new DataTable();

            dtC = dt;

            this.RadG.DataSource = dtC;

            this.RadG.DataBind();

        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();

            return k;
        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                DataUriage.T_UriageRow dr = (DataUriage.T_UriageRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;

                string sKey = dr.UriageNo.ToString();

                Button BtnSyusei = e.Item.FindControl("BtnSyusei") as Button;
                BtnSyusei.Attributes["onclick"] = string.Format("CntRow('{0}')", dr.UriageNo);
                BtnSyusei.Text = "修正";

                e.Item.Cells[RadG.Columns.FindByUniqueName("ColNo").OrderIndex].Text = dr.UriageNo.ToString();
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiName").OrderIndex].Text = dr.TokuisakiMei;
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColSisetu").OrderIndex].Text = dr.SisetuMei;
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColTanto").OrderIndex].Text = dr.TanTouName;
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColNyuryoku").OrderIndex].Text = dr.TourokuName;
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColHinmei").OrderIndex].Text = dr.SyouhinMei;
                //e.Item.Cells[RadG.Columns.FindByUniqueName("ColJutyu").OrderIndex].Text = dr.KeijoBi.ToShortDateString();

            }
        }

        protected void BtnDownlod_Click(object sender, EventArgs e)
        {
            //CSVDownLoad
            OnDownLoad();
            Create();
        }

        private void OnDownLoad()
        {
            try
            {
                UserViewManager.UserView v = SessionManager.User.GetUserView(LIST_ID_DL);
                v.SortExpression = "UriageNo ASC";

                SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (0 == dt.Rows.Count)
                {
                    lblMsg.Text = "該当データがありません";
                    return;
                }

                string strData = v.SqlDataFactory.GetTextData(dt, this.RadG.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

                string strExt = "csv";
                string strFileName = ("売上DL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                     this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                     Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時処理

            string UriageNo = count.Value;

            SessionManager.MitumoriSyusei(UriageNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("UriageSyusei.aspx");
        }

        protected void RadG_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            Create();
        }

        protected void RadG_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }
    }
}