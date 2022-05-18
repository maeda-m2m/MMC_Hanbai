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

namespace Gyomu.Return
{
    public partial class ReturnInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListSet.SetTanto(RcbTanto);
                ListSet.SetCate(RcbCategory);
                Create();
            }
        }

        private void Create()
        {
            try
            {
                ClassKensaku.KensakuParam k = SetKensakuParam();
                string no = SessionManager.HACCYU_NO;

                if (no == "")
                {
                    DataReturn.T_ReturnDataTable dt = ClassKensaku.GetReturnMeisaiSerch(k, Global.GetConnection());
                    if (dt.Rows.Count == 0)
                    {
                        LblErr.Text = "データがありません";
                        this.RadG.Visible = false;
                        return;
                    }
                    else
                    {
                        LblErr.Text = "";
                        this.RadG.Visible = true;
                    }

                    this.RadG.VirtualItemCount = dt.Count;

                    if (dt.Count < 10)
                    {
                        RadG.ClientSettings.Scrolling.AllowScroll = false;
                    }
                    else
                    {
                        RadG.ClientSettings.Scrolling.AllowScroll = true;
                    }

                    int nPageSize = this.RadG.PageSize;
                    int nPageCount = dt.Count / nPageSize;
                    if (0 < dt.Count % nPageSize) nPageCount++;
                    if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

                    DataTable dtC = new DataTable();

                    dtC = dt;

                    this.RadG.DataSource = dtC;

                    this.RadG.DataBind();

                }
                else
                {
                    TbxReturnNo.Text = no;
                    DataReturn.T_ReturnDataTable dt = ClassReturn.GetReturnMeisai(no, Global.GetConnection());
                    if (dt.Rows.Count == 0)
                    {
                        LblErr.Text = "データがありません";
                        this.RadG.Visible = false;
                        return;
                    }
                    else
                    {
                        LblErr.Text = "";
                        this.RadG.Visible = true;
                    }

                    this.RadG.VirtualItemCount = dt.Count;

                    if (dt.Count < 10)
                    {
                        RadG.ClientSettings.Scrolling.AllowScroll = false;
                    }
                    else
                    {
                        RadG.ClientSettings.Scrolling.AllowScroll = true;
                    }

                    int nPageSize = this.RadG.PageSize;
                    int nPageCount = dt.Count / nPageSize;
                    if (0 < dt.Count % nPageSize) nPageCount++;
                    if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

                    DataTable dtC = new DataTable();

                    dtC = dt;

                    this.RadG.DataSource = dtC;

                    this.RadG.DataBind();

                }

            }
            catch (Exception ex)
            {
                LblErr.Text = ex.Message;
            }
            SessionManager.MitumoriSyusei("");
        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();
            if (DDLStatus.SelectedValue != "")
            {
                k.rFlg = DDLStatus.SelectedValue;
            }
            if (TbxReturnNo.Text != "")
            {
                k.RetrunHeaderNo = TbxReturnNo.Text;
            }
            if (RcbFacility.SelectedValue != "")
            {
                k.Facility = RcbFacility.SelectedValue;
            }
            if (RcbCategory.SelectedValue != "")
            {
                k.sCate = RcbCategory.SelectedValue;
            }
            if (RcbProduct.Text != "")
            {
                k.sHinmei = RcbProduct.Text;
            }
            if (DDLMedia.SelectedValue != "")
            {
                k.sMedia = DDLMedia.SelectedValue;
            }
            if (RcbTanto.Text != "")
            {
                k.sTantoMei = RcbTanto.Text;
            }

            Common.CtlNengappiForm CtlNF = FindControl("CtlReturnDate") as Common.CtlNengappiForm;
            if (CtlNF.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k.sReturnDate = CtlNF.GetNengappiKikan().ToString();
            }

            return k;
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            Create();
        }

        protected void RcbFacility_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetFacility(sender, e);
            }
        }

        protected void RcbProduct_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetProduct(sender, e);
            }
        }

        protected void RcbTanto_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SerchTanto(sender, e);
            }
        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                DataReturn.T_ReturnRow dr = (DataReturn.T_ReturnRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;
                string sKey = dr.ReturnHeaderNo.ToString();
                HtmlInputCheckBox chkreturn = e.Item.FindControl("ChkReturn") as HtmlInputCheckBox;
                chkreturn.Value = string.Format("{0}", dr.ReturnNo);
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategori").OrderIndex].Text = dr.CategoryName;
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColTantousya").OrderIndex].Text = dr.TanTouName;
                if (!dr.IsSisetuCodeNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColFacilityCode").OrderIndex].Text = dr.SisetuCode.ToString();
                }
                if (!dr.IsSisetuMeiNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColFacilityName").OrderIndex].Text = dr.SisetuMei;
                }
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColProductName").OrderIndex].Text = dr.SyouhinMei;
                if (!dr.IsKeitaiMeiNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColMedia").OrderIndex].Text = dr.KeitaiMei;
                }
                if (!dr.IsJutyuSuryouNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSuryo").OrderIndex].Text = dr.JutyuSuryou.ToString();
                }
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColReturnNo").OrderIndex].Text = dr.ReturnNo.ToString();
                if (!dr.IsJutyuBiNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColEndDate").OrderIndex].Text = dr.JutyuBi.ToShortDateString();
                }


                if (!dr.IsReturnFlgNull())
                {
                    if (!dr.ReturnFlg)
                    {
                        chkreturn.Checked = false;
                    }
                    else
                    {
                        chkreturn.Checked = true;
                    }
                }
            }
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

        protected void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            string strKeys = "";
            string[] str = null;
            for (int i = 0; i < RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColStatus").OrderIndex].FindControl("ChkReturn");

                if (chk.Checked && chk.Visible)
                {
                    if (strKeys != "") { strKeys += "_"; }
                    strKeys += chk.Value;
                    str = strKeys.Split('_');
                }
            }
            ClassReturn.UpdateReturnFlg(str, Global.GetConnection());
            ClassReturn.UpdateReturnHeader(str, Global.GetConnection());

            Create();
            LblErr.Text = "返却処理が行われました。";
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}