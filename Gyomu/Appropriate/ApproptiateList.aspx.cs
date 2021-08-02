using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using ClosedXML.Excel;
using WebSupergoo.ABCpdf6;

namespace Gyomu.Appropriate
{
    public partial class ApproptiateList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {
            try
            {
                DataAppropriate.T_AppropriateHeaderDataTable dt = Class1.GetAppropriateHeader(Global.GetConnection());

                if (dt.Rows.Count == 0)
                {
                    err.Text = "データがありません";
                    this.RadG.Visible = false;
                    return;
                }
                else
                {
                    err.Text = "";
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
            catch (Exception ex)
            {
                this.err.Text = ex.Message;
            }
        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //DataMitumori.T_MitumoriRow dr = (DataMitumori.T_MitumoriRow)drv.Row;
                DataAppropriate.T_AppropriateHeaderRow dr = (DataAppropriate.T_AppropriateHeaderRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;

                //string sKey = dr.MitumoriNo;
                string sKey = dr.ShiireNo.ToString();


                HtmlInputCheckBox chk = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;
                bool b = chk.Checked;

                chk.Value = string.Format("{0}", dr.ShiireNo);



                e.Item.Cells[RadG.Columns.FindByUniqueName("ColNo").OrderIndex].Text = dr.ShiireNo.ToString();
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategory").OrderIndex].Text = dr.CategoryName;
                if (!dr.IsShiiresakiCodeNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColShiiresakiCode").OrderIndex].Text = dr.ShiiresakiCode;
                }
                if (!dr.IsShiiresakiNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColShiire").OrderIndex].Text = dr.ShiiresakiName.ToString();
                }
                if (!dr.IsShiireAmountNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSuryou").OrderIndex].Text = dr.ShiireAmount.ToString();
                }


                if (!dr.IsShiireKingakuNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColShiireKingaku").OrderIndex].Text = dr.ShiireKingaku.ToString("0,0");
                }

                if(!dr.IsCreateDateNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColCreateDate").OrderIndex].Text = dr.CreateDate.ToShortDateString();
                }
               

            }
        }

        protected void RadG_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            Create();
        }

        protected void RadG_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void BtnDenpyou_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.RadG.Items.Count; i++)
            {
                string strKyes = "";
                SessionManager.OrderedData(strKeys);
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                HtmlInputCheckBox chk2 = (HtmlInputCheckBox)RadG.Items[i].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += "_"; }

                    this.strKeys += chk.Value;
                }
            }

            string[] strShiire = strKeys.Split('_');

            Doc pdf = new Doc();
            AppCommon acApp = new AppCommon();
            string Type = "DenPyou";
            acApp.ShiireDenpyou(Type, pdf, strShiire);

            string sArg = "";

            sArg = Common.DownloadDataForm.GetQueryString4Binary("ShiireDenpyou" + DateTime.Now + ".pdf", acApp.theData);
            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + sArg)));

        }

        string strKeys = "";


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < this.RadG.Items.Count; i++)
                {
                    HtmlInputCheckBox chk =
                        (HtmlInputCheckBox)RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                    HtmlInputCheckBox chk2 = (HtmlInputCheckBox)RadG.Items[i].FindControl("ChkRow");

                    if (chk.Checked && chk.Visible)
                    {
                        if (this.strKeys != "") { this.strKeys += "_"; }

                        this.strKeys += chk.Value;
                    }
                }

                SessionManager.OrderedData(strKeys);
                Response.Redirect("AppropriateInput.aspx");
            }
            catch(Exception ex)
            {
                err.Text = ex.Message;
            }
        }

        protected void RadCate_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetCategory(sender, e);
        }

        protected void RadShiire_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetShiireSaki3(sender, e);
            }
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            Create();
        }
    }
}