using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;

namespace Gyomu.Master
{
    public partial class MasterShiire : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;

        const int LIST_ID = 100;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.F.Create(LIST_ID, 3);

                Craete();
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
        }
        private void Craete()
        {
            try
            {
                lblMsg.Text = "";
                L.Visible = true;

                UserViewManager.UserView v = SessionManager.User.GetUserView(LIST_ID);

                SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

                string strWhere = "";
                try
                {
                    strWhere = this.F.GetFilter(da.SelectCommand);
                    if (!string.IsNullOrEmpty(strWhere))
                        da.SelectCommand.CommandText += " where " + strWhere;
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    return;
                }

                Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
                info.nStartNumber = this.D.CurrentPageIndex * D.PageSize + 1;
                info.nEndNumber = (this.D.CurrentPageIndex + 1) * D.PageSize;

                DataTable dt = new DataTable();
                int nRecCount = 0;
                info.LoadData(da.SelectCommand, Global.GetConnection(), dt, ref nRecCount);


                if (0 == nRecCount)
                {
                    L.Visible = false;
                    lblMsg.Text = "データがありません。";
                    return;
                }
                if (0 == dt.Rows.Count)
                {
                    L.Visible = false;
                    this.D.CurrentPageIndex = 0;
                    lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                    return;
                }

                this.D.VirtualItemCount = nRecCount;

                int nPageSize = this.D.PageSize;
                int nPageCount = nRecCount / nPageSize;
                if (0 < nRecCount % nPageSize) nPageCount++;
                if (nPageCount <= this.D.CurrentPageIndex) this.D.MasterTableView.CurrentPageIndex = 0;

                this.D.DataSource = dt;

                v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

                this.D.DataBind();

                this.ShowList(true);
            }
            catch (Exception ex)
            {

            }
        }

        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.lblMsg.ForeColor = (bErrorMsg) ? Color.Red : Color.Blue;
            this.lblMsg.Text = strMsg;
        }

        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            for (int i = 0; i < this.L.Controls.Count; i++)
                this.L.Controls[i].Visible = bShow;
        }

        protected void D_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (!(Telerik.Web.UI.GridItemType.Item == e.Item.ItemType ||
                Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType))
                return;

            Button btnEdit = e.Item.FindControl("E") as Button;
            string strCode = Convert.ToString((e.Item.DataItem as DataRowView).Row["ShiireCode"]);

            Button E = e.Item.FindControl("E") as Button;
            E.Attributes["onclick"] = string.Format("CntRow('{0}')", strCode);
        }

        private void UserView_DataBound(UserViewManager.UserViewEventArgs e)
        {
            if (e.ItemType != UserViewManager.EnumItemType.DataRow) return;

        }

        protected void E_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック動作
            string ShiireCode = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

            Shiire.Create(ShiireCode);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterShiire.aspx");
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Shiire.Clear();
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Shiire.Add();

            if (bo)
            {
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";

                Craete();

                lblMsg.Text = "登録しました";
            }
        }

        protected void D_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void D_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            D.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            this.Craete();
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Craete();
        }
    }
}