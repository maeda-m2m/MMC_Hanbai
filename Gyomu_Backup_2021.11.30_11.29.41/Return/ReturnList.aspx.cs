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
    public partial class ReturnList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Create();
                ListSet.SetTanto(RcbTanto);
                ListSet.SetBumon(RcbBumon);
                ListSet.SetCate(RcbCategory);
            }
        }

        private void Create()
        {
            try
            {
                ClassKensaku.KensakuParam k = SetKensakuParam();
                DataReturn.T_ReturnHeaderDataTable dt = ClassKensaku.GetReturnHeader(k, Global.GetConnection());

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
            catch (Exception ex)
            {
                LblErr.Text = ex.Message;
            }
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
            if (RcbTokuisaki.SelectedValue != "")
            {
                k.TokuisakiName = RcbTokuisaki.SelectedValue;
            }
            if(RcbFacility.SelectedValue != "")
            {
                k.FacilityCode = RcbFacility.SelectedValue;
            }
            if(RcbCategory.SelectedValue != "")
            {
                k.CategoryNo = RcbCategory.SelectedValue;
            }
            if(RcbBumon.Text != "")
            {
                k.sBumon = RcbBumon.Text;
            }
            if(RcbTanto.Text != "")
            {
                k.TantoName = RcbTanto.Text;
            }

            Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlReturnDate") as Common.CtlNengappiForm;
            if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k.JucyuBi = this.CtlReturnDate.GetNengappiKikan();
            }
            return k;
        }

        protected void RcbTokuisaki_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTokuisaki(sender, e);
            }
        }

        protected void RcbTokuisaki_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RcbSeikyusaki_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetKensakuTyokusoSaki(sender, e);
            }
        }

        protected void RcbSeikyusaki_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RcbTyokusosaki_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void RcbTyokusosaki_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            Create();
        }

        protected void RcbFacility_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if(e.Text != "")
            {
                ListSet.SetFacility(sender, e);
            }
        }

        protected void RcbFacility_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RcbHinban_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void RcbHinban_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RcbProductName_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void RcbProductName_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RcbTanto_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void RcbTanto_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時処理
            string mNo = "";
            string strKeys = "";
            for (int i = 0; i < RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (strKeys != "") {strKeys += "_"; }

                    strKeys += chk.Value;
                    string[] str = strKeys.Split(',');
                    mNo = str[0];
                }
            }
            SessionManager.MitumoriSyusei(mNo);
            SessionManager.MitumoriSyuseiRow("");
            //SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("ReturnInput.aspx");

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }
        //ItemDataBound
        protected void RadG_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                DataReturn.T_ReturnHeaderRow dr = (DataReturn.T_ReturnHeaderRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;
                string sKey = dr.ReturnHeaderNo.ToString();
                HtmlInputCheckBox chk = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;
                chk.Value = string.Format("{0}", dr.ReturnHeaderNo);
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColReturnNo").OrderIndex].Text = dr.ReturnHeaderNo.ToString();
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategori").OrderIndex].Text = dr.CategoryName;
                if (!dr.IsBumonNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColBumon").OrderIndex].Text = dr.Bumon;
                }
                if (!dr.IsTokuisakiCodeNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiCode").OrderIndex].Text = dr.TokuisakiCode.ToString();
                }
                if (!dr.IsTokuisakiNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiName").OrderIndex].Text = dr.TokuisakiName;
                }


                if (!dr.IsFacilityNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSisetu").OrderIndex].Text = dr.FacilityName;
                }
                if (!dr.IsTantoNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTantousya").OrderIndex].Text = dr.TantoName;
                }
                if (!dr.IsSouSuryouNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSuryo").OrderIndex].Text = dr.SouSuryou.ToString();
                }
                if (!dr.IsSoukeiGakuNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColKingaku").OrderIndex].Text = dr.SoukeiGaku.ToString("0,0");
                }
                if (!dr.IsSiyouOwariNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColEndDate").OrderIndex].Text = dr.SiyouOwari.ToShortDateString();
                }
                if (dr.ReturnFlg)
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColStatus").OrderIndex].Text = "済";
                }
                else
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColStatus").OrderIndex].Text = "未";
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
    }
}