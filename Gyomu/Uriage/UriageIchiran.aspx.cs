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

namespace Gyomu.Uriage
{
    public partial class UriageIchiran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.JucyuSyusei("");
                SessionManager.MitumoriType("");

                //得意先、仕入先DDropDownList
                ListSet.SetRyakusyou(RadTokuiMeisyo, RadSekyuMeisyo);

                //担当名DropDwonList
                ListSet.SetTanto(RadTanto);
                //ListSet.SetTanto(RadNyuryoku);

                //部門DropDownList
                ListSet.SetBumon(RadBumon);

                //CategoryDropDownList
                ListSet.SetCate(RadCate);

                //直送先・施設DropDownList
                ListSet.SetTyokuso(RadTyokusoMeisyo, RadSisetMeisyo);
                //ListSet.SetTyokuso(RadSisetMeisyo);

                //商品情報DropDownList
                ListSet.SetSyohin(RadSyohinmeisyou);


                Create();
            }
        }

        private void Create()
        {
            try
            {
                ClassKensaku.KensakuParam k = SetKensakuParam();

                DataUriage.T_UriageHeaderDataTable dt =
                    ClassKensaku.KensakuUriageHeader(k, Global.GetConnection());

                //DataMitumori.T_MitumoriHeaderDataTable dt = ClassMitumori.GetMitsuHeadKensaku(k,Global.GetConnection());

                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "データがありません";
                    this.RadG.Visible = false;
                    return;
                }
                else
                {
                    lblMsg.Text = "";
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
            catch
            {
                lblMsg.Text = "データを取得できませんでした。";
            }
        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();

            if (DrpFlg.SelectedValue != "")
            {
                k.uriFlg = DrpFlg.SelectedValue;
            }

            if (TbxUriageNo.Text != "")
            {
                k.sUriageNo = TbxUriageNo.Text;
            }

            if (RadTokuiMeisyo.SelectedValue != "" && RadTokuiMeisyo.SelectedValue != "-1")
            {
                string toku = RadTokuiMeisyo.Text;
                string[] srr = toku.Split('/');
                k.sTokuisaki = srr[1];
            }
            if (RadSekyuMeisyo.SelectedValue != "" && RadSekyuMeisyo.SelectedValue != "-1")
            {
                string sei = RadSekyuMeisyo.Text;
                string[] srr = sei.Split('/');
                k.sSeikyu = RadSekyuMeisyo.SelectedValue;
            }
            if (RadTyokusoMeisyo.SelectedValue != "" && RadTyokusoMeisyo.SelectedValue != "-1")
            {
                k.sTyokuso = RadTyokusoMeisyo.SelectedValue;
            }
            if (RadSisetMeisyo.SelectedValue != "" && RadSisetMeisyo.SelectedValue != "-1")
            {
                k.sSisetu = RadSisetMeisyo.SelectedValue;
            }
            if (RadCate.SelectedValue != "")
            {
                k.sCate = RadCate.SelectedValue;
            }
            if (RadBumon.SelectedValue != "")
            {
                k.sBumon = RadBumon.SelectedItem.Text;
            }
            if (TbxHinban.Text != "")
            {
                k.sHinban = TbxHinban.Text;
            }
            if (RadSyohinmeisyou.SelectedValue != "" && RadSyohinmeisyou.SelectedValue != "-1")
            {
                k.sHinmei = RadSyohinmeisyou.SelectedValue;
            }
            if (RadTanto.SelectedValue != "")
            {
                k.sTanto = RadTanto.SelectedItem.Text;
            }

            Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlJucyuBi") as Common.CtlNengappiForm;
            if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k.JucyuBi = this.CtlJucyuBi.GetNengappiKikan();
            }

            return k;

        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //DataMitumori.T_MitumoriRow dr = (DataMitumori.T_MitumoriRow)drv.Row;
                DataUriage.T_UriageHeaderRow dr = (DataUriage.T_UriageHeaderRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;

                //string sKey = dr.MitumoriNo;
                string sKey = dr.UriageNo.ToString();


                HtmlInputCheckBox chk = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;

                chk.Value = string.Format("{0}", dr.UriageNo);

                //Button BtnJutyu = e.Item.FindControl("BtnJutyu") as Button;

                e.Item.Cells[RadG.Columns.FindByUniqueName("ColUriage").OrderIndex].Text = dr.UriageNo.ToString();
                if (!dr.IsCategoryNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategori").OrderIndex].Text = dr.CategoryName;
                }

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
                if (!dr.IsCreateDateNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColMitumoriDay").OrderIndex].Text = dr.CreateDate.ToLongDateString();

                }
            }

        }

        protected void RadG_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

        }

        protected void RadG_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void RadTokuiMeisyo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        string strKeys = "";
        string Type = "";

        protected void RadSekyuMeisyo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetTokuisaki(sender, e);
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadSisetMeisyo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.GetFacility(sender, e);
        }

        protected void RadSyohinmeisyou_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakuSyohin(sender, e);
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {

        }

        protected void BtnDownlod_Click(object sender, EventArgs e)
        {

        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            string mNo = "";
            string strKeys = "";
            for (int i = 0; i < RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (strKeys != "") { strKeys += "_"; }

                    strKeys += chk.Value;
                    string[] str = strKeys.Split(',');
                    mNo = str[0];
                }
            }


            SessionManager.MitumoriSyusei(mNo);
            SessionManager.MitumoriSyuseiRow("");
            //SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("UriageMeisai.aspx");

        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {

        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void RadG_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }

        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += ","; }

                    this.strKeys += chk.Value;
                }
            }

            Response.Write("<script>");
            Response.Write("window.open('../Mitumori/Print.aspx?id=" + this.strKeys + "' , '印刷', 'width=600, height=600, toolbar=1, menubar=1, scrollbars=1')");
            Response.Write("</script>");
        }
    }
}