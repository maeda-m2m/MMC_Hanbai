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


namespace Gyomu.Order
{
    public partial class OrderedList : System.Web.UI.Page
    {
        const int LIST_ID_DL = 30;
        private string vsNo
        {
            get
            {
                object obj = this.ViewState["vsNo"];
                if (null == obj) return "";
                return Convert.ToString(obj);
            }
            set
            {
                this.ViewState["vsNo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                SessionManager.JucyuSyusei("");
                SessionManager.MitumoriType("");

                //得意先、仕入先DDropDownList
                //ListSet.SetRyakusyou(RadTokuiMeisyo, RadSekyuMeisyo);

                ////担当名DropDwonList
                //ListSet.SetTanto(RadTanto);
                //ListSet.SetTanto(RadNyuryoku);

                ////部門DropDownList
                //ListSet.SetBumon(RadBumon);

                ////CategoryDropDownList
                //ListSet.SetCate(RadCate);

                ////直送先・施設DropDownList
                //ListSet.SetTyokuso(RadTyokusoMeisyo, RadSisetMeisyo);
                ////ListSet.SetTyokuso(RadSisetMeisyo);

                ////商品情報DropDownList
                //ListSet.SetSyohin(RadSyohinmeisyou);

                Create();
            }
        }

        private void Create()
        {
            try
            {
                ClassKensaku.KensakuParam k = SetKensakuParam();
                DataSet1.T_OrderedHeaderDataTable dt = ClassKensaku.GetOrderedHeader(k, Global.GetConnection());
                //DataSet1.T_OrderedHeaderDataTable dt = ClassOrdered.GetOrderedHeader(Global.GetConnection());
                //DataSet1.T_OrderedHeaderDataTable dt = ClassOrdered.GetOrderedHeader(Global.GetConnection());

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
                this.lblMsg.Text = ex.Message;
            }
        }

        protected void RadG_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            Create();
        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();

            //if (DrpFlg.SelectedValue != "")
            //{
            //    k.sFlg = DrpFlg.SelectedValue;
            //}

            //if (TbxMitumoriNo.Text != "")
            //{
            //    k.sMitimoriNo = TbxMitumoriNo.Text;
            //}

            //if (RadTokuiMeisyo.SelectedValue != "" && RadTokuiMeisyo.SelectedValue != "-1")
            //{
            //    k.sTokuisaki = RadTokuiMeisyo.SelectedValue;
            //}
            //if (RadSekyuMeisyo.SelectedValue != "" && RadSekyuMeisyo.SelectedValue != "-1")
            //{
            //    k.sSeikyu = RadSekyuMeisyo.SelectedValue;
            //}
            //if (RadTyokusoMeisyo.SelectedValue != "" && RadTyokusoMeisyo.SelectedValue != "-1")
            //{
            //    k.sTyokuso = RadTyokusoMeisyo.SelectedValue;
            //}
            //if (RadSisetMeisyo.SelectedValue != "" && RadSisetMeisyo.SelectedValue != "-1")
            //{
            //    k.sSisetu = RadSisetMeisyo.SelectedValue;
            //}
            //if (RadCate.SelectedValue != "")
            //{
            //    k.sCate = RadCate.SelectedValue;
            //}
            //if (RadBumon.SelectedValue != "")
            //{
            //    k.sBumon = RadBumon.SelectedValue;
            //}
            //if (TbxHinban.Text != "")
            //{
            //    k.sHinban = TbxHinban.Text;
            //}
            //if (RadSyohinmeisyou.SelectedValue != "" && RadSyohinmeisyou.SelectedValue != "-1")
            //{
            //    k.sHinmei = RadSyohinmeisyou.SelectedValue;
            //}
            //if (RadTanto.SelectedValue != "")
            //{
            //    k.sTanto = RadTanto.SelectedItem.Text;
            //}
            //if (RadNyuryoku.SelectedValue != "")
            //{
            //    k.sNyuryoku = RadNyuryoku.SelectedItem.Text;
            //}

            //Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlJucyuBi") as Common.CtlNengappiForm;
            //if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            //{
            //    k.JucyuBi = this.CtlJucyuBi.GetNengappiKikan();
            //}

            k.oFlg = DrpFlg.Text;
            if(RadCate.Text != "")
            {
                k.oCate = RadCate.Text;
            }
            if(RadShiire.Text != "")
            {
                k.sShiire = RadShiire.Text;
            }


            return k;
        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //DataMitumori.T_MitumoriRow dr = (DataMitumori.T_MitumoriRow)drv.Row;
                //DataSet1.T_OrderedHeaderRow dr = (DataSet1.T_OrderedHeaderRow)drv.Row;
                DataSet1.T_OrderedHeaderRow dr = (DataSet1.T_OrderedHeaderRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;

                //string sKey = dr.MitumoriNo;
                string sKey = dr.OrderedNo.ToString();


                HtmlInputCheckBox chk = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;
                bool b = chk.Checked;


                chk.Value = string.Format("{0}", dr.OrderedNo);


                e.Item.Cells[RadG.Columns.FindByUniqueName("ColMitumori").OrderIndex].Text = dr.OrderedNo.ToString();
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategori").OrderIndex].Text = dr.CategoryName;
                if (!dr.IsShiiresakiCodeNull())
                { e.Item.Cells[RadG.Columns.FindByUniqueName("ColShiireCode").OrderIndex].Text = dr.ShiiresakiCode; }
                if (!dr.IsShiiresakiNameNull())
                { e.Item.Cells[RadG.Columns.FindByUniqueName("ColShiireName").OrderIndex].Text = dr.ShiiresakiName; }
                if (!dr.IsOrderedAmountNull())
                { e.Item.Cells[RadG.Columns.FindByUniqueName("ColSuryo").OrderIndex].Text = dr.OrderedAmount.ToString(); }
                if (!dr.IsShiireKingakuNull())
                { e.Item.Cells[RadG.Columns.FindByUniqueName("ColShiireKingaku").OrderIndex].Text = dr.ShiireKingaku.ToString("0,0"); }
                if(!dr.IsCreateDateNull())
                { e.Item.Cells[RadG.Columns.FindByUniqueName("ColOrderedDate").OrderIndex].Text = dr.CreateDate.ToShortDateString(); }

            }
        }

        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時処理
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            //SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("OrderedInput.aspx");
        }

        protected void BtnCopy_Click(object sender, EventArgs e)
        {
            //複製ボタンクリック時処理
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Copy");
            this.Response.Redirect("OrderedInput.aspx");
        }

        protected void BtnKeijo_Click(object sender, EventArgs e)
        {
            //受注ボタンクリック処理
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Jutyu");
            this.Response.Redirect("../OrderInput.aspx");
        }

        protected void BtnDownlod_Click(object sender, EventArgs e)
        {
            //CsvDownLoad
            OnDownLoad();
            //Create();
        }

        private void OnDownLoad()
        {
            try
            {
                UserViewManager.UserView v = SessionManager.User.GetUserView(LIST_ID_DL);
                v.SortExpression = "OrderedNo ASC";

                ClassKensaku.KensakuParam k = SetKensakuParam();
                DataSet1.T_OrderedHeaderDataTable dt =
                   ClassKensaku.GetOrderedDL(k, Global.GetConnection());

                //SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

                //DataTable dt = new DataTable();
                //da.Fill(dt);

                if (0 == dt.Rows.Count)
                {
                    lblMsg.Text = "該当データがありません";
                    return;
                }

                string strData = v.SqlDataFactory.GetTextData(dt, this.RadG.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

                string strExt = "csv";
                string strFileName = ("発注DL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
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

        protected void RadTokuiMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadSyohinmeisyou_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakuSyohin(sender, e);
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            this.RadG.MasterTableView.CurrentPageIndex = 0;
            this.Create();
        }

        string strKeys = "";
        string Type = "";

        protected void BtnOrdered_Click(object sender, EventArgs e)
        {
            //印刷タイプの設定
            Type = "Ordered";
            Insatu(Type);
        }

        private void Insatu(string type)
        {
            //納品印刷設定
            for (int i = 0; i < this.RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += "_"; }

                    this.strKeys += chk.Value;
                }
            }

            if (strKeys == "")
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("チェックしてください。");
                return;
            }
            else
            {
                string[] strAry = this.strKeys.Split('_');
                string strOrderedNo = "";
                string strShiireCode = "";
                string strRowNo = "";
                string sNo = "";
                string sShisetu = "";

                string[] strMitumoriAry = strOrderedNo.Split(',');
                //string[] strRowAry = strRowNo.Split('_');
                string[] strShiireAry = strShiireCode.Split(',');

                AppCommon acApp = new AppCommon();

                //acApp.OrderPrint(Type, strMitumoriAry, strShiireAry);
                acApp.OrderPrint2(Type, strAry);
                string sArg = "";

                if (Type == "Ordered")
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary
                        ("Ordered.pdf", acApp.theData);
                }
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + sArg)));
                ClassOrdered.UpdateFlg(strAry, Global.GetConnection());
                Create();
            }
        }

        protected void RadG_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void test_Click(object sender, EventArgs e)
        {
            XLWorkbook workbook = new XLWorkbook(System.Configuration.ConfigurationManager.AppSettings["MitumoriFormat"]);
            IXLWorksheet worksheet = workbook.Worksheet(1);
            int lastrow = worksheet.LastRowUsed().RowNumber();
            for (int i = 1; i <= lastrow; i++)
            {
                IXLCell cell = worksheet.Cell(i, 1);
                Console.WriteLine(cell.Value);
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {

        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += "_"; }

                    this.strKeys += chk.Value;
                }
            }
            string[] strAry = strKeys.Split('_');
            if (strAry.Length >= 1)
            {

                SessionManager.OrderedData(strKeys);
                Response.Redirect("OrderedInput.aspx");
            }
            else
            {
                lblMsg.Text = "修正時のチェックは１つまでです。";
            }
        }

        protected void RadCate_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetCategory(sender, e);
        }

        protected void RadShiire_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if(e.Text != "")
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