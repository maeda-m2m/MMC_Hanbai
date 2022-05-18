using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using ClosedXML.Excel;


namespace Gyomu.Mitumori
{
    public partial class MitumoriItiran : System.Web.UI.Page
    {
        const int LIST_ID_DL = 10;

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
                SessionManager.MitumoriSyusei("");
                //得意先、仕入先DDropDownList
                //ListSet.SetRyakusyou(RadTokuiMeisyo, RadSekyuMeisyo);
                //担当名DropDwonList
                ListSet.SetTanto(RadTanto);
                //ListSet.SetTanto(RadNyuryoku);

                //部門DropDownList
                ListSet.SetBumon(RadBumon);

                //CategoryDropDownList
                ListSet.SetCate(RadCate);

                //直送先・施設DropDownList
                //ListSet.SetTyokuso(RadTyokusoMeisyo, RadSisetMeisyo);
                //ListSet.SetTyokuso(RadSisetMeisyo);

                //商品情報DropDownList
                //ListSet.SetSyohin(RadSyohinmeisyou);

                //Create();
            }
        }

        protected void RadG_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            Create();
        }

        private void Create()
        {
            try
            {
                string code = "";
                ClassKensaku.KensakuParam k = SetKensakuParam();
                DataMitumori.T_MitumoriHeaderDataTable dtH = new DataMitumori.T_MitumoriHeaderDataTable();

                DataMitumori.T_MitumoriHeaderDataTable dt =
                    ClassKensaku.GetMitumoriData(k, Global.GetConnection());

                if (TbxHinban.Text != "" || RadSyohinmeisyou.Text != "")
                {
                    if (TbxHinban.Text != "")
                    {
                        DataMitumori.T_MitumoriDataTable dtHIN = ClassMitumori.GetMitumoriHinban(TbxHinban.Text, Global.GetConnection());
                        for (int h = 0; h < dtHIN.Count; h++)
                        {
                            if (code == "")
                            {
                                code += dtHIN[h].MitumoriNo;
                            }
                            else
                            {
                                if (!code.Contains(dtHIN[h].MitumoriNo))
                                {
                                    code += "," + dtHIN[h].MitumoriNo;
                                }
                            }
                        }
                    }
                    if (RadSyohinmeisyou.Text != "")
                    {
                        DataMitumori.T_MitumoriDataTable dtSYO = ClassMitumori.GetMitumoriHinban(RadSyohinmeisyou.SelectedValue, Global.GetConnection());
                        for (int s = 0; s < dtSYO.Count; s++)
                        {
                            if (code == "")
                            {
                                code += dtSYO[s].MitumoriNo;
                            }
                            else
                            {
                                if (!code.Contains(dtSYO[s].MitumoriNo))
                                {
                                    code += "," + dtSYO[s].MitumoriNo;
                                }
                            }
                        }
                    }
                    if (code != "")
                    {
                        string codeH = "";
                        for (int r = 0; r < dt.Count; r++)
                        {
                            if (code.Contains(dt[r].MitumoriNo.ToString()))
                            {
                                if (codeH == "")
                                {
                                    codeH += dt[r].MitumoriNo.ToString();
                                }
                                else
                                {
                                    codeH += "," + dt[r].MitumoriNo.ToString();
                                }
                            }
                        }
                        string[] codeAry = code.Split(',');
                        for (int j = 0; j < codeAry.Length; j++)
                        {
                            DataMitumori.T_MitumoriHeaderRow dtR = dtH.NewT_MitumoriHeaderRow();
                            DataMitumori.T_MitumoriHeaderDataTable dtK = ClassMitumori.GETMitsumorihead(codeAry[j], Global.GetConnection());
                            dtR.ItemArray = dtK[0].ItemArray;
                            dtH.AddT_MitumoriHeaderRow(dtR);
                        }
                    }
                    else
                    {
                        for (int d = 0; d < dt.Count; d++)
                        {
                            DataMitumori.T_MitumoriHeaderRow dtR = dtH.NewT_MitumoriHeaderRow();
                            dtR.ItemArray = dt[d].ItemArray;
                            dtH.AddT_MitumoriHeaderRow(dtR);
                        }
                    }
                }
                else
                {
                    for (int d = 0; d < dt.Count; d++)
                    {
                        DataMitumori.T_MitumoriHeaderRow dtR = dtH.NewT_MitumoriHeaderRow();
                        dtR.ItemArray = dt[d].ItemArray;
                        dtH.AddT_MitumoriHeaderRow(dtR);
                    }

                }
                //DataMitumori.T_MitumoriHeaderDataTable dt = ClassMitumori.GetMitsuHeadKensaku(k,Global.GetConnection());

                if (dtH.Rows.Count == 0)
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

                this.RadG.VirtualItemCount = dtH.Count;

                if (dtH.Count < 10)
                {
                    RadG.ClientSettings.Scrolling.AllowScroll = false;
                }
                else
                {
                    RadG.ClientSettings.Scrolling.AllowScroll = true;
                }

                int nPageSize = this.RadG.PageSize;
                int nPageCount = dtH.Count / nPageSize;
                if (0 < dtH.Count % nPageSize) nPageCount++;
                if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

                DataTable dtC = new DataTable();

                dtC = dtH;

                this.RadG.DataSource = dtC;

                this.RadG.DataBind();
            }

            catch (Exception ex)
            {
                this.lblMsg.Text = ex.Message;
            }

        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();

            if (DrpFlg.SelectedValue != "")
            {
                k.sFlg = DrpFlg.SelectedValue.Trim();
            }

            if (TbxMitumoriNo.Text != "")
            {
                k.sMitimoriNo = TbxMitumoriNo.Text.Trim();
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
                k.sSeikyu = RadSekyuMeisyo.SelectedValue.Trim();
            }
            if (RadTyokusoMeisyo.SelectedValue != "" && RadTyokusoMeisyo.SelectedValue != "-1")
            {
                k.sTyokuso = RadTyokusoMeisyo.SelectedValue;
            }
            if (RadSisetMeisyo.SelectedValue != "" && RadSisetMeisyo.SelectedValue != "-1")
            {
                k.sSisetu = RadSisetMeisyo.Text;
            }
            if (RadCate.SelectedValue != "")
            {
                k.sCate = RadCate.SelectedValue.Trim();
            }
            if (RadBumon.SelectedValue != "")
            {
                k.sBumon = RadBumon.SelectedItem.Text;
            }
            //if (TbxHinban.Text != "")
            //{
            //    k.sHinban = TbxHinban.Text;
            //}
            //if (RadSyohinmeisyou.SelectedValue != "" && RadSyohinmeisyou.SelectedValue != "-1")
            //{
            //    k.sHinmei = RadSyohinmeisyou.SelectedValue;
            //}
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
                string strFacility = "";
                //DataMitumori.T_MitumoriRow dr = (DataMitumori.T_MitumoriRow)drv.Row;
                DataMitumori.T_MitumoriHeaderRow dr = (DataMitumori.T_MitumoriHeaderRow)drv.Row;
                DataMitumori.T_MitumoriDataTable dtM = ClassMitumori.GetFacility(dr.MitumoriNo, Global.GetConnection());
                for (int m = 0; m < dtM.Count; m++)
                {
                    if (strFacility == "")
                    {
                        strFacility = dtM[m].SisetsuAbbreviration;
                    }
                    else
                    {
                        if (!strFacility.Contains(dtM[m].SisetsuAbbreviration))
                        {
                            strFacility += "/" + dtM[m].SisetsuAbbreviration;
                        }
                    }
                }
                int rgItemIndex = e.Item.ItemIndex;

                //string sKey = dr.MitumoriNo;
                string sKey = dr.MitumoriNo.ToString();


                HtmlInputCheckBox chk = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;

                chk.Value = string.Format("{0}", dr.MitumoriNo);

                //Button BtnJutyu = e.Item.FindControl("BtnJutyu") as Button;

                e.Item.Cells[RadG.Columns.FindByUniqueName("ColMitumori").OrderIndex].Text = dr.MitumoriNo.ToString();
                e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategori").OrderIndex].Text = dr.CategoryName;
                if (!dr.IsBumonNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColBumon").OrderIndex].Text = dr.Bumon;
                }
                if (!dr.IsTokuisakiCodeNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiCode").OrderIndex].Text = dr.TokuisakiCode.ToString();
                }
                if (!dr.IsTokuisakiRyakusyoNull())
                {
                    if (dr.TokuisakiRyakusyo.Length < 15)
                    {
                        e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiName").OrderIndex].Text = dr.TokuisakiRyakusyo;
                    }
                    else
                    {
                        string data = dr.TokuisakiRyakusyo.Substring(0, 15);
                        e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiName").OrderIndex].Text = data + "...";
                    }
                }


                if (!dr.IsFacilityNameNull())
                {
                    if (strFacility.Length < 15)
                    {
                        e.Item.Cells[RadG.Columns.FindByUniqueName("ColSisetu").OrderIndex].Text = strFacility;
                    }
                    else
                    {
                        string data = strFacility.Substring(0, 15);
                        e.Item.Cells[RadG.Columns.FindByUniqueName("ColSisetu").OrderIndex].Text = data + "...";
                    }
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
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColMitumoriDay").OrderIndex].Text = dr.CreateDate.ToShortDateString();
                }
            }
        }

        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時処理
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            //SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("../Kaihatsu.aspx");
        }

        protected void BtnCopy_Click(object sender, EventArgs e)
        {
            //複製ボタンクリック時処理
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Copy");
            this.Response.Redirect("../Kaihatsu.aspx");
        }

        protected void BtnJutyu_Click(object sender, EventArgs e)
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
                v.SortExpression = "MitumoriNo ASC";

                ClassKensaku.KensakuParam k = SetKensakuParam();
                DataMitumori.T_MitumoriHeaderDataTable dt =
                   ClassKensaku.GetMitumoriData(k, Global.GetConnection());


                if (0 == dt.Rows.Count)
                {
                    lblMsg.Text = "該当データがありません";
                    return;
                }

                string strData = v.SqlDataFactory.GetTextData(dt, this.RadG.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

                string strExt = "csv";
                string strFileName = ("見積DL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
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
            ListSet.SetTokuisaki(sender, e);
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadSyohinmeisyou_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text.Trim() != "")
            {
                //ドロップダウン検索用
                ListSet.SetKensakuSyohin(sender, e);
            }
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            this.RadG.MasterTableView.CurrentPageIndex = 0;
            this.Create();
        }

        string strKeys = "";
        string Type = "";
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
                string strMitumoriNo = "";
                string strShisetuCode = "";
                string strRowNo = "";
                string sNo = "";
                string sShisetu = "";

                //印刷するためのデータの詳細を分ける
                for (int i = 0; i < strAry.Length; i++)
                {
                    string[] str = strAry[i].Split(',');

                    if (str.Length < 1) { continue; }

                    //見積が同じ場合でも施設が違ったら分けている
                    if (strMitumoriNo != "")
                    {
                        if ((sNo != str[0] || sShisetu != str[2]))
                        {
                            strMitumoriNo += ",";
                            strMitumoriNo += string.Format("{0}", str[0]);
                            if (strRowNo != "") { strRowNo += "_"; }
                            //strRowNo += str[1];
                            if (strShisetuCode != "") { strShisetuCode += ","; }
                            strShisetuCode += str[1];

                            //前回の見積データを確認
                            sNo = str[0];
                            sShisetu = str[1];
                        }
                        else
                        {
                            if (strRowNo != "") { strRowNo += ","; }
                            //strRowNo += str[1];
                        }
                    }
                    else
                    {
                        strMitumoriNo += string.Format("{0}", str[0]);
                        strShisetuCode += str[1];

                        sNo = str[0];
                        sShisetu = str[1];
                    }
                }

                string[] strMitumoriAry = strMitumoriNo.Split(',');
                //string[] strRowAry = strRowNo.Split('_');
                // string[] strShisetuiAry = strShisetuCode.Split(',');

                AppCommon acApp = new AppCommon();

                //acApp.MitumoriInsatu(Type, strMitumoriAry, strShisetuiAry);

                acApp.MitumoriInsatu2(Type, strMitumoriAry);

                string sArg = "";


                if (Type == "Mitumori")
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary("MitumoriFormat.pdf", acApp.theData);
                }

                if (Type == "Nouhin")
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary("NouhinFormat.pdf", acApp.theData);
                }

                if (type == "Sekyu")
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary("SekyuFormat.pdf", acApp.theData);
                }


                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + sArg)));
            }
        }

        protected void RadG_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

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

        protected void BtnSyusei_Click1(object sender, EventArgs e)
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
                    if (this.strKeys != "") { this.strKeys += "_"; }

                    this.strKeys += chk.Value;
                    string[] str = this.strKeys.Split(',');
                    mNo = str[0];
                }
            }


            SessionManager.MitumoriSyusei(mNo);
            SessionManager.MitumoriSyuseiRow("");
            //SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("../Kaihatsu.aspx");

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
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
                }
            }
            if (strKeys == "")
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("チェックしてください。");
                return;
            }
            else
            {
                string[] strAry = strKeys.Split('_');
                for (int l = 0; l < strAry.Length; l++)
                {

                    string[] str = strAry[l].Split(',');
                    //mNo = str[0];
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (str[j] != "")
                        {
                            mNo = str[0];

                            ClassMitumori.DelMitumoriHeader(mNo, Global.GetConnection());
                            ClassMitumori.DelMitumori3(mNo, Global.GetConnection());
                        }
                    }
                }
            }
            Create();

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
            Response.Write("window.open('Print.aspx?id=" + this.strKeys + "' , '印刷', 'width=400, height=400, toolbar=1, menubar=1, scrollbars=1')");
            Response.Write("</script>");
        }

        protected void RadSisetMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.GetFacility(sender, e);
        }
    }

}

