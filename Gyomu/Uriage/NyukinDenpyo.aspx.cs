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

namespace Gyomu.Uriage
{
    public partial class NyukinDenpyo : System.Web.UI.Page
    {
        // 伝番（修正時に使用）
        private string VsDenBan
        {
            set
            {
                this.ViewState["VsDenBan"] = value;
            }
            get
            {
                object obj = this.ViewState["VsDenBan"];
                if (obj == null) { return null; }
                return obj.ToString();
            }
        }

        private const string LOCK_COLOR = "#dfdfdf";
        private const string HISSU_COLOR = "#FFCCFF";

        protected void Ch_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.G.Items.Count; i++)
            {
                CheckBox C = this.G.Items[i].FindControl("C") as CheckBox;
                C.Checked = ((CheckBox)sender).Checked;
            }
        }
        protected void G_PreRender(object sender, EventArgs e)
        {
            this.G.MasterTableView.Attributes["bordercolor"] = "black";
            this.G.MasterTableView.Attributes["border"] = "1";

            this.G.Columns.FindByUniqueName("C").Visible = this.RdoKobetu.Checked;
        }


        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LblMsg.Text = "";

            if (!this.IsPostBack)
            {
                this.LblDate.Text = "登録日時：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm");//登録日

                //部門DropDownList
                ListSet.SetBumon(RadBumon);

                //担当名DropDwonList
                ListSet.SetTanto(RadTanto);

                //得意先、仕入先DDropDownList
                ListSet.SetRyakusyou2(RadTokuiMeisyo);

                string DenBan = this.Request.QueryString["DenBan"];

                if (DenBan != null && DenBan != "")
                {
                    this.RdoAdd.Checked = false;
                    this.RdoShusei.Checked = true;

                    this.CreateByDenpyoNo(DenBan);

                    this.ShowMsg("最新の入金伝票から順に表示しています。", false);
                }

                this.TbxGenkin.Attributes["onBlur"] = "OnBlur1('TbxGenkin'); return false;";
                this.TbxKogitte.Attributes["onBlur"] = "OnBlur1('TbxKogitte'); return false;";
                this.TbxFurikomi.Attributes["onBlur"] = "OnBlur1('TbxFurikomi'); return false;";
                this.TbxTegata.Attributes["onBlur"] = "OnBlur1('TbxTegata'); return false;";
                this.TbxChousei.Attributes["onBlur"] = "OnBlur1('TbxChousei'); return false;";
                this.TbxSousai.Attributes["onBlur"] = "OnBlur1('TbxSousai'); return false;";

                this.RdpNyukinBi.DateInput.Focus();
            }
        }

        protected void RadTokuiMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        protected void RadTokuiMeisyo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadTokuiMeisyo.SelectedValue != "" && RadTokuiMeisyo.SelectedValue != "-1")
            {
                string sValue = RadTokuiMeisyo.SelectedValue;

                DataMitumori.M_Customer_NewRow dr =
                    ClassMitumori.GetMTokuisaki(sValue, Global.GetConnection());

                TbxTokuiTantouName.Text = dr.CustomerPersonnelName;
                TbxTokuiCode.Text = dr.CustomerCode.ToString();


                if (dr.CutoffDate == 5)
                {
                    LblShimeBi.Text = "5日締め";
                }
                else
                   if (dr.CutoffDate == 10)
                {
                    LblShimeBi.Text = "10日締め";
                }
                else
                   if (dr.CutoffDate == 15)
                {
                    LblShimeBi.Text = "15日締め";
                }
                if (dr.CutoffDate == 20)
                {
                    LblShimeBi.Text = "20日締め";
                }
                else
               if (dr.CutoffDate == 25)
                {
                    LblShimeBi.Text = "25日締め";
                }
                else
               if (dr.CutoffDate == 99)
                {
                    LblShimeBi.Text = "月末締め";
                }
                if (dr.CutoffDate == 0)
                {
                    LblShimeBi.Text = "随時締め";
                }

                DataMitumori.M_TantoRow Tdr =
                    ClassMitumori.GetMTanto(dr.PersonnelCode, Global.GetConnection());

                RadBumon.SelectedValue = Tdr.BumonKubun.ToString();
                RadTanto.SelectedValue = Tdr.UserName;


                //if (RdpNyukinBi.SelectedDate != null)
                //{
                //    NyukinYoteiData(dr, RdpNyukinBi.SelectedDate);
                //}

                this.Lock(false);
            }
            else
            {
                TbxTokuiTantouName.Text = "";
                TbxTokuiCode.Text = "";
                LblShimeBi.Text = "";
                LblShukinYotei.Text = "";
                LblSeikyuKikan.Text = "";
                LblNyukinKikan.Text = "";

                this.BtnReg.Enabled =
                    this.BtnDel.Enabled = false;
            }
        }

        protected void RdpNyukinBi_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (RadTokuiMeisyo.SelectedValue != "" && RadTokuiMeisyo.SelectedValue != "-1")
            {
                string sValue = RadTokuiMeisyo.SelectedValue;

                DataMitumori.M_Customer_NewRow dr =
                    ClassMitumori.GetMTokuisaki(sValue, Global.GetConnection());
            }
        }

        protected void TbxJuchuNo_TextChanged(object sender, EventArgs e)
            {
            // 受注単位 または 個別単位の場合
            if (TbxJuchuNo.Text != "" && TbxJuchuNo.Text != "-1")
            {
                DataUriage.T_UriageDataTable dtUriage =
                    ClassUriage.GetUriage(TbxJuchuNo.Text, Global.GetConnection());

                this.CreateTokuisakiInfo(dtUriage);
            }
            else
            {
                Clear();
            }
        }

        //private void NyukinYoteiData(DataMitumori.M_TokuisakiRow dr , DateTime? date)
        //{
        //一括の場合
        //if (this.RdoIkkatu.Checked)
        //{
        //    DateTime SeikyuDate = this.RdpNyukinBi.SelectedDate.Value.AddMonths((-1));
        //    int SeikyuYear = SeikyuDate.Year;
        //    int SeikyuMonth = SeikyuDate.Month;

        //    // 請求月
        //    this.TbxSeikyuYear.Text = SeikyuYear.ToString();
        //    this.TbxSeikyuMonth.Text = SeikyuMonth.ToString();

        //    // 請求期間
        //    DateTime HitotukiMae = SeikyuDate.AddMonths(-1);

        //    //都度
        //    if (dr.ShimebikubunCode == 0)
        //    {
        //        LblShukinYotei.Text = "";

        //        LblSeikyuKikan.Text = "";

        //        LblNyukinKikan.Text = "";

        //        //請求期間、入金期間は表示しない
        //    }
        //    else
        //    {
        //        int ShimeBi_HitokukiMae;
        //        int ShimeBi_SeikyuShime;

        //        LblShukinYotei.Text = "翌月支払い";


        //        if (dr.ShimebikubunCode == 20)
        //        {
        //            //締日20日
        //            ShimeBi_HitokukiMae = ShimeBi_SeikyuShime = dr.ShimebikubunCode;

        //        }
        //        else
        //        {
        //            //締日月末
        //            ShimeBi_HitokukiMae = DateTime.DaysInMonth(HitotukiMae.Year, HitotukiMae.Month);
        //            ShimeBi_SeikyuShime = DateTime.DaysInMonth(SeikyuDate.Year, SeikyuDate.Month);
        //        }

        //        //請求期間
        //        DateTime SeikyuDateFrom = (new DateTime(HitotukiMae.Year, HitotukiMae.Month, ShimeBi_HitokukiMae)).AddDays(1);
        //        DateTime SeikyuDateTo = new DateTime(SeikyuDate.Year, SeikyuDate.Month, ShimeBi_SeikyuShime);

        //        this.LblSeikyuKikan.Text = string.Format("{0:yyyy年MM月dd日}～{1:yyyy年MM月dd日}", SeikyuDateFrom, SeikyuDateTo);

        //        // 入金期間
        //        DateTime NyukinDateFrom = SeikyuDateFrom;
        //        DateTime NyukinDateTo = RdpNyukinBi.SelectedDate.Value;
        //        if (SeikyuDateTo > NyukinDateTo)
        //        {
        //            NyukinDateTo = SeikyuDateTo;
        //        }
        //        this.LblNyukinKikan.Text = string.Format("{0:yyyy年MM月dd日}～{1:yyyy年MM月dd日}", NyukinDateFrom, NyukinDateTo);

        //        //請求残
        //        //請求額
        //        DataUriage.T_UriageDataTable dtUriage = ClassUriage.GetT_UriageDataTable(this.TbxTokuiCode.Text.Trim(), SeikyuDateFrom, SeikyuDateTo, Global.GetConnection());
        //        object objGoukeiKingaku = dtUriage.Compute("SUM(JutyuGokei)", null);
        //        object objSyouhiZei = dtUriage.Compute("SUM(Syohizei)", null);

        //        long nGoukeiKingaku = 0;
        //        if (objGoukeiKingaku != null)
        //        {
        //            long.TryParse(objGoukeiKingaku.ToString(), out nGoukeiKingaku);
        //        }

        //        long nShouhiZei = 0;
        //        if (objSyouhiZei != null)
        //        {
        //            long.TryParse(objSyouhiZei.ToString(), out nShouhiZei);
        //        }

        //        this.LblKonkaiSeikyuGaku.Text = string.Format("{0:#,##0}", nGoukeiKingaku + nShouhiZei);
        //        this.LblSyouhiZei.Text = string.Format("{0:#,##0}", nShouhiZei);
        //        //消費税
        //        // 入金済金額
        //        DataUriage.T_NyukinDataTable dtNyukin = ClassUriage.GetT_NyukinDataTable(this.TbxTokuiCode.Text.Trim(), NyukinDateFrom, NyukinDateTo, Global.GetConnection());
        //        //今回入金額
        //        //入金残高

        //        this.CreateUriageNyukinMeisai(dtUriage, dtNyukin);
        //    }
        //}

        //}


        private void CreateUriageNyukinMeisai(DataUriage.T_UriageDataTable dtUriage, DataUriage.T_NyukinDataTable dtNyukin)
        {
            //if (dtUriage.Count > 0 && !dtUriage[0].IsKeijoBiNull())
            //{
            //    this.LblUriageKeijouBi.Text = dtUriage[0].KeijoBi.ToString("yyyy/MM/dd");
            //}

            //// 売上金額 税抜きなので税込み表示にさせる
            //object objUriageKingaku = dtUriage.Compute("SUM(JutyuGokei)", null);
            //string sUriageKingaku = objUriageKingaku.ToString();
            //int nUriage = int.Parse(sUriageKingaku);

            //object objSyohiZei = dtUriage.Compute("SUM(Syohizei)", null);
            //string sSyohiZei = objSyohiZei.ToString();
            //int nSyohiZei = int.Parse(sSyohiZei);

            //int nGokei = nUriage + nSyohiZei;


            //long nUriageKingaku = 0;
            //if (objUriageKingaku != null)
            //{
            //    long.TryParse(nGokei.ToString(), out nUriageKingaku);
            //}
            //this.LblUriageKingaku.Text = string.Format("{0:#,##0}", nUriageKingaku);

            //object objNyukinZumiKingaku = dtNyukin.Compute("SUM(Sashihiki)", string.Format("UriageKanriCode <> '{0}'", this.VsDenBan));
            //long nNyukinZumiKingaku = 0;
            //if (objNyukinZumiKingaku != null)
            //{
            //    long.TryParse(objNyukinZumiKingaku.ToString(), out nNyukinZumiKingaku);
            //}
            //this.LblNyukinZumiKingaku.Text = string.Format("{0:#,##0}", nNyukinZumiKingaku);

            //this.Set_KonkaiNyukinGaku_NyukinZan();

            //List<UriageNyukinMeisai> ListUriageNyukinMeisai = new List<UriageNyukinMeisai>();

            //// ■個別入金以外の場合
            //if (!this.RdoKobetu.Checked)
            //{
            //    for (int i = 0; i < dtUriage.Count; i++)
            //    {
            //        UriageNyukinMeisai UriageMeisai = new UriageNyukinMeisai();
            //        UriageMeisai.No = (i + 1).ToString();
            //        UriageMeisai.SyouhiCode = dtUriage[i].UriageNo + "/" + dtUriage[i].SyouhinCode;
            //        UriageMeisai.Syiyou = dtUriage[i].Tekiyou1 + "/" + dtUriage[i].Tekiyou2;
            //        UriageMeisai.KeijouBi = dtUriage[i].KeijoBi.ToString("yyyy/MM/dd");
            //        decimal nSuryou = dtUriage[i].IsJutyuSuryouNull() ? 0 : dtUriage[i].JutyuSuryou;
            //        UriageMeisai.Suuryou = string.Format("{0:#,##0}", nSuryou);
            //        int nTanka = dtUriage[i].IsJutyuTankaNull() ? 0 : dtUriage[i].JutyuTanka;
            //        UriageMeisai.Tanka_Kinshu = string.Format("{0:#,##0}", nTanka);
            //        long nGoukeiKingaku = dtUriage[i].IsJutyuGokeiNull() ? 0 : dtUriage[i].JutyuGokei;
            //        double nSyouhiZei = dtUriage[i].IsSyohizeiNull() ? 0 : dtUriage[i].Syohizei;
            //        UriageMeisai.Kingaku = string.Format("{0:#,##0}", nGoukeiKingaku + nSyouhiZei);
            //        UriageMeisai.Zei = string.Format("{0:#,##0}", nSyouhiZei);
            //        UriageMeisai.isUriage = true;
            //        UriageMeisai.isBlank = false;
            //        ListUriageNyukinMeisai.Add(UriageMeisai);
            //    }

            //    if (dtUriage.Count > 0 && dtNyukin.Count > 0)
            //    {
            //        UriageNyukinMeisai blankMeisai = new UriageNyukinMeisai();
            //        blankMeisai.isUriage = false;
            //        blankMeisai.isBlank = true;
            //        ListUriageNyukinMeisai.Add(blankMeisai);
            //    }

            //    for (int i = 0; i < dtNyukin.Count; i++)
            //    {
            //        UriageNyukinMeisai NyukinMeisai = new UriageNyukinMeisai();

            //        string Shiyou = "";

            //        string strNyukinKunbun = "";
            //        if (!dtNyukin[i].IsNyukinKubunNull())
            //        {
            //            if (dtNyukin[i].NyukinKubun == NyukinKubun.IKKATU)
            //            {
            //                strNyukinKunbun = "(一括)";
            //            }
            //            if (dtNyukin[i].NyukinKubun == NyukinKubun.JYUCHU_TANI)
            //            {
            //                strNyukinKunbun = "(受注単位)";
            //            }
            //            if (dtNyukin[i].NyukinKubun == NyukinKubun.KOBETU_TANI)
            //            {
            //                strNyukinKunbun = "(個別単位)";
            //            }
            //        }

            //        NyukinMeisai.SyouhiCode = dtNyukin[i].UriageKanriCode + "&nbsp;" + strNyukinKunbun;

            //        if (!dtNyukin[i].IsGenkinNull())
            //        {
            //            Shiyou += string.Format("現金[{0:#,##0}]&nbsp;", dtNyukin[i].Genkin);
            //        }
            //        if (!dtNyukin[i].IsFurikomiNull())
            //        {
            //            Shiyou += string.Format("振込[{0:#,##0}]&nbsp;", dtNyukin[i].Furikomi);
            //        }
            //        if (!dtNyukin[i].IsTegataNull())
            //        {
            //            Shiyou += string.Format("手形[{0:#,##0}]&nbsp;", dtNyukin[i].Tegata);
            //        }
            //        if (!dtNyukin[i].IsKogitteNull())
            //        {
            //            Shiyou += string.Format("小切手[{0:#,##0}]&nbsp;", dtNyukin[i].Kogitte);
            //        }
            //        if (!dtNyukin[i].IsSousaiNull())
            //        {
            //            Shiyou += string.Format("相殺[{0:#,##0}]&nbsp;", dtNyukin[i].Sousai);
            //        }
            //        if (!dtNyukin[i].IsCyouseiNull())
            //        {
            //            Shiyou += string.Format("調整[{0:#,##0}]&nbsp;", dtNyukin[i].Cyousei);
            //        }
            //        NyukinMeisai.Syiyou = Shiyou;

            //        NyukinMeisai.KeijouBi = dtNyukin[i].NyukinBi.ToString("yyyy/MM/dd");

            //        NyukinMeisai.Kingaku = dtNyukin[i].Sashihiki.ToString("#,##0");

            //        NyukinMeisai.isUriage = false;
            //        NyukinMeisai.isBlank = false;

            //        ListUriageNyukinMeisai.Add(NyukinMeisai);
            //    }
            //}
            //else // ■個別入金の場合
            //{
            //    DataView dvNyukin = dtNyukin.DefaultView;

            //    for (int i = 0; i < dtUriage.Count; i++)
            //    {
            //        UriageNyukinMeisai UriageMeisai = new UriageNyukinMeisai();
            //        UriageMeisai.No = (i + 1).ToString();
            //        UriageMeisai.SyouhiCode = dtUriage[i].UriageNo + "/" + dtUriage[i].SyouhinCode;
            //        UriageMeisai.Syiyou = dtUriage[i].Tekiyou1 + "/" + dtUriage[i].Tekiyou2;
            //        UriageMeisai.KeijouBi = dtUriage[i].KeijoBi.ToString("yyyy/MM/dd");
            //        decimal dSuryou = dtUriage[i].IsJutyuSuryouNull() ? 0 : dtUriage[i].JutyuSuryou;
            //        UriageMeisai.Suuryou = string.Format("{0:#,##0}", dSuryou);
            //        int nTanka = dtUriage[i].IsJutyuTankaNull() ? 0 : dtUriage[i].JutyuTanka;
            //        UriageMeisai.Tanka_Kinshu = string.Format("{0:#,##0}", nTanka);
            //        long nGoukeiKingaku = dtUriage[i].IsJutyuGokeiNull() ? 0 : dtUriage[i].JutyuGokei;
            //        double nSyouhiZei = dtUriage[i].IsSyohizeiNull() ? 0 : dtUriage[i].Syohizei;
            //        UriageMeisai.Kingaku = string.Format("{0:#,##0}", nGoukeiKingaku + nSyouhiZei);
            //        UriageMeisai.Zei = string.Format("{0:#,##0}", dtUriage[i].Syohizei);

            //        UriageMeisai.isUriage = true;
            //        UriageMeisai.isBlank = false;
            //        ListUriageNyukinMeisai.Add(UriageMeisai);

            //        dvNyukin.RowFilter = string.Format("RowNo = '{0}'", dtUriage[i].RowNo);

            //        DataUriage.T_NyukinDataTable dtNyukinNew = new DataUriage.T_NyukinDataTable();

            //        for (int j = 0; j < dvNyukin.Count; j++)
            //        {
            //            dtNyukinNew.ImportRow(dvNyukin[j].Row);
            //        }

            //        for (int j = 0; j < dtNyukinNew.Count; j++)
            //        {
            //            UriageNyukinMeisai NyukinMeisai = new UriageNyukinMeisai();

            //            string strNyukinKunbun = "";
            //            if (!dtNyukin[i].IsNyukinKubunNull())
            //            {
            //                if (dtNyukin[i].NyukinKubun == NyukinKubun.IKKATU)
            //                {
            //                    strNyukinKunbun = "(一括)";
            //                }
            //                if (dtNyukin[i].NyukinKubun == NyukinKubun.JYUCHU_TANI)
            //                {
            //                    strNyukinKunbun = "(受注単位)";
            //                }
            //                if (dtNyukin[i].NyukinKubun == NyukinKubun.KOBETU_TANI)
            //                {
            //                    strNyukinKunbun = "(個別単位)";
            //                }
            //            }

            //            NyukinMeisai.SyouhiCode = dtNyukinNew[j].UriageKanriCode + "&nbsp" + strNyukinKunbun;

            //            string Shiyou = "";

            //            if (!dtNyukinNew[j].IsGenkinNull())
            //            {
            //                Shiyou += string.Format("現金[{0:#,##0}]&nbsp;", dtNyukinNew[j].Genkin);
            //            }
            //            if (!dtNyukinNew[j].IsFurikomiNull())
            //            {
            //                Shiyou += string.Format("振込[{0:#,##0}]&nbsp;", dtNyukinNew[j].Furikomi);
            //            }
            //            if (!dtNyukinNew[j].IsTegataNull())
            //            {
            //                Shiyou += string.Format("手形[{0:#,##0}]&nbsp;", dtNyukinNew[j].Tegata);
            //            }
            //            if (!dtNyukinNew[j].IsKogitteNull())
            //            {
            //                Shiyou += string.Format("小切手[{0:#,##0}]&nbsp;", dtNyukinNew[j].Kogitte);
            //            }
            //            if (!dtNyukinNew[j].IsSousaiNull())
            //            {
            //                Shiyou += string.Format("相殺[{0:#,##0}]&nbsp;", dtNyukinNew[j].Sousai);
            //            }
            //            if (!dtNyukinNew[j].IsCyouseiNull())
            //            {
            //                Shiyou += string.Format("調整[{0:#,##0}]&nbsp;", dtNyukinNew[j].Cyousei);
            //            }
            //            NyukinMeisai.Syiyou = Shiyou;

            //            NyukinMeisai.KeijouBi = dtNyukinNew[j].NyukinBi.ToString("yyyy/MM/dd");

            //            NyukinMeisai.Kingaku = dtNyukinNew[j].Sashihiki.ToString("#,##0");

            //            NyukinMeisai.isUriage = false;
            //            NyukinMeisai.isBlank = false;

            //            ListUriageNyukinMeisai.Add(NyukinMeisai);
            //        }
            //    }
            //}

            //this.G.Visible = ListUriageNyukinMeisai.Count > 0;
            //this.G.DataSource = ListUriageNyukinMeisai;
            //this.G.DataBind();
        }

        private void Set_KonkaiNyukinGaku_NyukinZan()
        {
            // 請求残
            string strSeikyuZan = this.LblSeikyuZan.Text.Trim().Replace(",", "");
            strSeikyuZan = Utility.StrConvToHankaku(strSeikyuZan);
            long nSeikyuZan = 0;
            long.TryParse(strSeikyuZan, out nSeikyuZan);

            // 入金済金額
            string strNyukinZumiKingaku = this.LblNyukinZumiKingaku.Text.Trim().Replace(",", "");
            strNyukinZumiKingaku = Utility.StrConvToHankaku(strNyukinZumiKingaku);
            long nNyukinZumiKingaku = 0;
            long.TryParse(strNyukinZumiKingaku, out nNyukinZumiKingaku);

            // 今回請求額
            string strKonkaiSeikyuGaku = this.LblKonkaiSeikyuGaku.Text.Trim().Replace(",", "");
            strKonkaiSeikyuGaku = Utility.StrConvToHankaku(strKonkaiSeikyuGaku);
            long nKonkaiSeikyuGaku = 0;
            long.TryParse(strKonkaiSeikyuGaku, out nKonkaiSeikyuGaku);

            // 消費税
            string strSyouhiZei = this.LblSyouhiZei.Text.Trim().Replace(",", "");
            strSyouhiZei = Utility.StrConvToHankaku(strSyouhiZei);
            long nSyouhiZei = 0;
            long.TryParse(strSyouhiZei, out nSyouhiZei);

            // 売上金額
            string strUriageKingaku = this.LblUriageKingaku.Text.Trim().Replace(",", "");
            strUriageKingaku = Utility.StrConvToHankaku(strUriageKingaku);
            long nUriageKingaku = 0;
            long.TryParse(strUriageKingaku, out nUriageKingaku);

            string strGenkin = this.TbxGenkin.Text.Trim().Replace(",", "");
            strGenkin = Utility.StrConvToHankaku(strGenkin);
            long nGenkin = 0;
            long.TryParse(strGenkin, out nGenkin);
            if (strGenkin != "")
            {
                this.TbxGenkin.Text = string.Format("{0:#,##0}", nGenkin);
            }

            string strKogitte = this.TbxKogitte.Text.Trim().Replace(",", "");
            strKogitte = Utility.StrConvToHankaku(strKogitte);
            long nKogitte = 0;
            long.TryParse(strKogitte, out nKogitte);
            if (strKogitte != "")
            {
                this.TbxKogitte.Text = string.Format("{0:#,##0}", nKogitte);
            }

            string strFurikomi = this.TbxFurikomi.Text.Trim().Replace(",", "");
            strFurikomi = Utility.StrConvToHankaku(strFurikomi);
            long nFurikomi = 0;
            long.TryParse(strFurikomi, out nFurikomi);
            if (strFurikomi != "")
            {
                this.TbxFurikomi.Text = string.Format("{0:#,##0}", nFurikomi);
            }

            string strTegata = this.TbxTegata.Text.Trim().Replace(",", "");
            strTegata = Utility.StrConvToHankaku(strTegata);
            long nTegata = 0;
            long.TryParse(strTegata, out nTegata);
            if (strTegata != "")
            {
                this.TbxTegata.Text = string.Format("{0:#,##0}", nTegata);
            }

            string strChousei = this.TbxChousei.Text.Trim().Replace(",", "");
            strChousei = Utility.StrConvToHankaku(strChousei);
            long nChousei = 0;
            long.TryParse(strChousei, out nChousei);
            if (strChousei != "")
            {
                this.TbxChousei.Text = string.Format("{0:#,##0}", nChousei);
            }

            string strSousai = this.TbxSousai.Text.Trim().Replace(",", "");
            strSousai = Utility.StrConvToHankaku(strSousai);
            long nSousai = 0;
            long.TryParse(strSousai, out nSousai);
            if (strSousai != "")
            {
                this.TbxSousai.Text = string.Format("{0:#,##0}", nSousai);
            }

            // 今回入金額
            long nKonkaiNyukinGaku = nGenkin + nKogitte + nFurikomi + nTegata + nChousei + nSousai;
            this.LblNyukinGaku.Text = string.Format("{0:#,##0}", nKonkaiNyukinGaku);

            this.LblNyukinZandaka.Text = "";

            //if (this.RdoIkkatu.Checked)
            //{
            //    // 入金残高（請求残＋今回請求額(税込)－入金済金額－今回入金額）
            //    long nNyukinZandaka = nSeikyuZan + nKonkaiSeikyuGaku - nNyukinZumiKingaku - nKonkaiNyukinGaku;
            //    this.LblNyukinZandaka.Text = string.Format("{0:#,##0}", nNyukinZandaka);
            //}

            if (this.RdoJuchu.Checked || this.RdoKobetu.Checked)
            {
                // 入金残高（売上金額(税込)－入金済金額－今回入金額）
                long nNyukinZandaka = nUriageKingaku - nNyukinZumiKingaku - nKonkaiNyukinGaku;
                this.LblNyukinZandaka.Text = string.Format("{0:#,##0}", nNyukinZandaka);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // ■画面表示切替
            if (this.RdoAdd.Checked)
            {
                this.TblDenban.Visible = false;
                this.BtnDel.Visible = false;
                this.DivSearch.Visible = false;

                //this.TbxJuchuNo.ReadOnly = false;
                this.TbxJuchuNo.Enabled = true;

                /*this.RdoIkkatu.Enabled = */
                this.RdoJuchu.Enabled = this.RdoKobetu.Enabled = true;

                this.LblDate.Text = "登録日時：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm");//登録日

                this.BtnReg.Text = "登録";
            }
            else
            {
                this.TblDenban.Visible = true;
                this.BtnDel.Visible = true;
                this.TbxDenBan.Text = this.VsDenBan == null ? "" : this.VsDenBan;
                this.DivSearch.Visible = true;

                //this.TbxJuchuNo.ReadOnly = true;
                this.TbxJuchuNo.Enabled = false;

                /*this.RdoIkkatu.Enabled = */
                this.RdoJuchu.Enabled = this.RdoKobetu.Enabled = false;

                this.BtnReg.Text = "修正";
            }

            //if (this.RdoIkkatu.Checked)
            //{
            //    this.TblJuchuNo.Visible = false;

            //    //this.TbxTokuiCode.ReadOnly = false;
            //    this.TbxTokuiCode.Enabled = true;

            //    this.TblIkkatu.Visible = true;
            //    this.TblJyuchuKobetu.Visible = false;

            //    this.TdNyukinKikanHeader.Visible = this.TdNyukinKikan.Visible = true;
            //}
            if (this.RdoJuchu.Checked || this.RdoKobetu.Checked)
            {
                this.TblJuchuNo.Visible = true;

                //this.TbxTokuiCode.ReadOnly = true;
                this.TbxTokuiCode.Enabled = false;

                this.TblIkkatu.Visible = false;
                this.TblJyuchuKobetu.Visible = true;

                this.TdNyukinKikanHeader.Visible = this.TdNyukinKikan.Visible = false;
            }
        }

        private void ShowMsg(string msg, bool bErr)
        {
            this.LblMsg.Text = msg;
            this.LblMsg.Font.Bold = true;
            if (bErr)
            {
                this.LblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.LblMsg.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void Lock(bool isLock)
        {
            if (isLock)
            {
                this.ShowMsg("[ロック済]", false);
            }

            this.BtnReg.Enabled = this.BtnClear.Enabled = this.BtnDel.Enabled = !isLock;
            /*this.RdoIkkatu.Enabled =*/
            this.RdoJuchu.Enabled = this.RdoKobetu.Enabled = !isLock;

            this.TbxJuchuNo.ReadOnly = this.TbxTokuiCode.ReadOnly =
                this.TbxTokuiTantouName.ReadOnly = this.TbxSeikyuYear.ReadOnly = this.TbxSeikyuMonth.ReadOnly = this.TbxTekiyou.ReadOnly =
                this.TbxGenkin.ReadOnly = this.TbxGenkinBikou.ReadOnly =
                this.TbxKogitte.ReadOnly = this.TbxKogitteBikou.ReadOnly =
                this.TbxFurikomi.ReadOnly = this.TbxFurikomiBikou.ReadOnly =
                this.TbxTegata.ReadOnly = this.TbxTegataBikou.ReadOnly =
                this.TbxFuridashiNin.ReadOnly =
                this.TbxChousei.ReadOnly = this.TbxChouseiBikou.ReadOnly =
                this.TbxSousai.ReadOnly = this.TbxSousaiBikou.ReadOnly = isLock;

            this.RdpNyukinBi.Enabled = this.RadBumon.Enabled = this.RadTanto.Enabled = this.RdpKogitteKijitu.Enabled = this.RdpTegataKijitu.Enabled = !isLock;

            if (isLock)
            {
                this.TbxJuchuNo.BackColor = this.TbxTokuiCode.BackColor =
                    this.TbxTokuiTantouName.BackColor = this.TbxSeikyuYear.BackColor = this.TbxSeikyuMonth.BackColor = this.TbxTekiyou.BackColor =
                    this.TbxGenkin.BackColor = this.TbxGenkinBikou.BackColor =
                    this.TbxKogitte.BackColor = this.TbxKogitteBikou.BackColor =
                    this.TbxFurikomi.BackColor = this.TbxFurikomiBikou.BackColor =
                    this.TbxTegata.BackColor = this.TbxTegataBikou.BackColor =
                    this.TbxFuridashiNin.BackColor =
                    this.TbxChousei.BackColor = this.TbxChouseiBikou.BackColor =
                    this.TbxSousai.BackColor = this.TbxSousaiBikou.BackColor = System.Drawing.Color.FromName(LOCK_COLOR);

                this.RdpKogitteKijitu.DateInput.BackColor = this.RdpTegataKijitu.DateInput.BackColor = this.RadBumon.BackColor = System.Drawing.Color.FromName(LOCK_COLOR);
            }
            else
            {
                this.TbxJuchuNo.BackColor = this.TbxTokuiCode.BackColor =
                    this.TbxTokuiTantouName.BackColor = this.TbxSeikyuYear.BackColor = this.TbxSeikyuMonth.BackColor = this.TbxTekiyou.BackColor =
                    this.TbxGenkin.BackColor = this.TbxGenkinBikou.BackColor =
                    this.TbxKogitte.BackColor = this.TbxKogitteBikou.BackColor =
                    this.TbxFurikomi.BackColor = this.TbxFurikomiBikou.BackColor =
                    this.TbxTegata.BackColor = this.TbxTegataBikou.BackColor =
                    this.TbxFuridashiNin.BackColor =
                    this.TbxChousei.BackColor = this.TbxChouseiBikou.BackColor =
                    this.TbxSousai.BackColor = this.TbxSousaiBikou.BackColor = System.Drawing.Color.White;

                this.RdpKogitteKijitu.DateInput.BackColor = this.RdpTegataKijitu.DateInput.BackColor = System.Drawing.Color.White;

                this.TbxTokuiCode.BackColor = this.TbxSeikyuYear.BackColor = this.TbxSeikyuMonth.BackColor = this.RadBumon.BackColor =
                    this.TbxGenkin.BackColor = this.TbxKogitte.BackColor = this.TbxFurikomi.BackColor = this.TbxTegata.BackColor = this.TbxChousei.BackColor = this.TbxSousai.BackColor = this.RadBumon.BackColor = System.Drawing.Color.FromName(HISSU_COLOR);
            }
        }

        private class UriageNyukinMeisai
        {
            public string No { set; get; }
            public string SyouhiCode { set; get; }
            public string Syiyou { set; get; }
            public string KeijouBi { set; get; }
            public string Suuryou { set; get; }
            public string Tanka_Kinshu { set; get; }
            public string Kingaku { set; get; }
            public string Zei { set; get; }

            public bool isUriage { set; get; }
            public bool isBlank { set; get; }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void Clear()
        {
            this.BtnReg.Enabled = true;
            this.DisableMainTextbox(false);

            this.TbxDenBan.Text = this.TbxJuchuNo.Text = this.RdpNyukinBi.DateInput.Text = this.TbxTokuiCode.Text =
            this.TbxTokuiTantouName.Text = this.LblShimeBi.Text =
            this.LblShukinYotei.Text = this.TbxSeikyuYear.Text = this.TbxSeikyuMonth.Text =
            this.LblSeikyuKikan.Text = this.LblSeikyuZan.Text = this.LblKonkaiSeikyuGaku.Text = this.LblSyouhiZei.Text =
            this.LblNyukinZumiKingaku.Text = this.LblNyukinKikan.Text = this.LblNyukinGaku.Text =
            this.LblNyukinZandaka.Text = this.TbxTekiyou.Text =
            this.TbxGenkin.Text = this.TbxGenkinBikou.Text =
            this.TbxKogitte.Text = this.RdpKogitteKijitu.DateInput.Text = this.TbxKogitteBikou.Text = this.RadTokuiMeisyo.Text =
            this.TbxFurikomi.Text = this.TbxFurikomiBikou.Text =
            this.TbxTegata.Text = this.RdpTegataKijitu.DateInput.Text = this.TbxTegataBikou.Text =
            this.TbxFuridashiNin.Text =
            this.TbxChousei.Text = this.TbxChouseiBikou.Text =
            this.TbxSousai.Text = this.TbxSousaiBikou.Text = "";

            RadTokuiMeisyo.SelectedValue = RadBumon.SelectedValue = RadTanto.SelectedValue = "";

            LblUriageKeijouBi.Text = LblUriageKingaku.Text = "";

            this.RdpNyukinBi.SelectedDate = this.RdpKogitteKijitu.SelectedDate = this.RdpTegataKijitu.SelectedDate = null;

            this.G.Visible = false;
        }

        private void DisableMainTextbox(bool bDisalbe)
        {
            this.TbxDenBan.Enabled = !bDisalbe;
            this.TbxJuchuNo.Enabled = !bDisalbe;
            this.RdpNyukinBi.Enabled = !bDisalbe;
            this.TbxTokuiCode.Enabled = !bDisalbe;
            this.TbxSeikyuYear.Enabled = this.TbxSeikyuMonth.Enabled = !bDisalbe;
            this.RadBumon.Enabled = !bDisalbe;
            this.TbxGenkin.Enabled = this.TbxKogitte.Enabled = this.TbxFurikomi.Enabled = this.TbxTegata.Enabled = this.TbxChousei.Enabled = this.TbxSousai.Enabled = !bDisalbe;
        }

        protected void RdoIkkatu_CheckedChanged(object sender, EventArgs e)
        {
            this.Clear();

            this.RdpNyukinBi.DateInput.Focus();
        }

        protected void RdoJuchu_CheckedChanged(object sender, EventArgs e)
        {
            this.Clear();

            this.TbxJuchuNo.Focus();

            this.ShowMsg("入金対象の受注番号を入力してください。", false);
        }

        protected void RdoKobetu_CheckedChanged(object sender, EventArgs e)
        {
            this.Clear();

            this.TbxJuchuNo.Focus();

            this.ShowMsg("入金対象の受注番号を入力してください。", false);
        }

        protected void BtnReg_Click(object sender, EventArgs e)
        {
            //登録
            if (this.RdoAdd.Checked)
            {
                if (RdpNyukinBi.SelectedDate != null)
                {
                    try
                    {
                        DataNyukin.T_NyukinDataTable dtNyukin = new DataNyukin.T_NyukinDataTable();
                        DataNyukin.T_NyukinRow drNyukin = dtNyukin.NewT_NyukinRow();

                        this.CheckData_SetData(drNyukin);

                        dtNyukin.AddT_NyukinRow(drNyukin);

                        string NewDenBan = ClassNyukin.InsertT_NyukinRow(SessionManager.User.UserID, TorihikisakiType.TOKUISAKI, drNyukin, Global.GetConnection());

                        this.CreateByDenpyoNo(NewDenBan);

                        this.ShowMsg(string.Format("伝番:[{0}] で入金登録が完了しました。続けて登録する場合は、「ｸﾘｱ」ボタンを押してください。", NewDenBan), false);

                        this.BtnReg.Enabled = false;

                        this.DisableMainTextbox(true);

                        this.LblDate.Text = "登録日時：" + drNyukin.TourokuBi.ToString("yyyy/MM/dd HH:mm");//登録日
                    }
                    catch (ApplicationException ex)
                    {
                        this.Ram.Alert(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        this.ShowMsg("エラー：" + ex.Message, true);
                    }
                }
                else
                {
                    this.ShowMsg("入金日を入力してください。", false);
                }
            }
            // ■修正
            if (this.RdoShusei.Checked)
            {
                SqlConnection sqlCon = Global.GetConnection();
                SqlTransaction sqlTran = null;

                try
                {
                    DataNyukin.T_NyukinRow drNyukin = ClassNyukin.GetT_NyukinRow(this.VsDenBan, TorihikisakiType.TOKUISAKI, Global.GetConnection());

                    if (!drNyukin.IsNyukinBiNull())
                    {

                        string strMonth = "";
                        if (this.IsBeforeMonth(drNyukin.NyukinBi, ref strMonth))
                        {
                        }
                        if (strMonth != "")
                        {
                            this.ShowMsg(string.Format("入金日が{0}カ月以前のデータは修正できません。", strMonth), true);
                            return;
                        }
                    }


                    this.CheckData_SetData(drNyukin);

                    drNyukin.UriageKanriCode = this.VsDenBan;

                    sqlCon.Open();
                    sqlTran = sqlCon.BeginTransaction();

                    ClassNyukin.UpdateT_NyukinRow(SessionManager.User.UserID, drNyukin, sqlTran);

                    ClassNyukin.SetNyukinRireki(this.VsDenBan, TorihikisakiType.TOKUISAKI, DateTime.Now, SessionManager.User.UserID, "入金修正", sqlTran);

                    sqlTran.Commit();

                    this.CreateByDenpyoNo(this.VsDenBan);

                    this.ShowMsg(string.Format("伝番:[{0}] の入金修正が完了しました。", this.VsDenBan), false);

                    this.LblDate.Text = "修正日時：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm");//登録日
                }
                catch (ApplicationException ex)
                {
                    if (sqlTran != null) { sqlTran.Rollback(); }
                    this.Ram.Alert(ex.Message);
                }
                catch (Exception ex)
                {
                    if (sqlTran != null) { sqlTran.Rollback(); }
                    this.ShowMsg("エラー：" + ex.Message, true);
                }
                finally
                {
                    if (sqlCon != null) { sqlCon.Close(); }
                }
            }
        }

        private bool IsBeforeMonth(DateTime date, ref string strErr)
        {
            try
            {
                DataMaster.M_AppSettingRow drAppSeting = ClassMaster.GetM_AppSettingRow("InputInhibit_MoneyReceived", Global.GetConnection());
                if (drAppSeting != null)
                {
                    int nSettingValue = int.Parse(drAppSeting.SettingValue.Trim().Replace(",", ""));


                    if (date <= DateTime.Today.AddMonths(-nSettingValue))
                    {
                        this.RdpNyukinBi.DateInput.Focus();
                        throw new Exception(nSettingValue.ToString());
                    }
                }
                else
                {
                    if (date <= DateTime.Today.AddMonths(-2))
                    {
                        this.RdpNyukinBi.DateInput.Focus();
                        throw new Exception("2");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
        }

        int CurrentRowNo;   // 個別入金の修正時に、修正対象明細の行番号を一時的に保存する変数

        private bool CreateByDenpyoNo(string DenpyouNo)
        {
            DataNyukin.T_NyukinRow drNyukin = ClassNyukin.GetT_NyukinRow(DenpyouNo, TorihikisakiType.TOKUISAKI, Global.GetConnection());

            if (drNyukin == null) { return false; }

            if (!drNyukin.IsKoushinBiNull())
                this.LblDate.Text = "登録日時：" + drNyukin.KoushinBi.ToString("yyyy/MM/dd HH:mm");//更新日
            else
                this.LblDate.Text = "登録日時：" + drNyukin.TourokuBi.ToString("yyyy/MM/dd HH:mm");//登録日

            this.VsDenBan = DenpyouNo;

            this.CurrentRowNo = drNyukin.RowNo;

            if (this.RdoShusei.Checked)
            {
                this.TbxDenBan.Text = DenpyouNo;
            }
            else
            {
                this.TbxDenBan.Text = "";
            }

            // 入金区分
            if (!drNyukin.IsNyukinKubunNull())
            {
                if (drNyukin.NyukinKubun == NyukinKubun.IKKATU)
                {
                    //this.RdoIkkatu.Checked = true;
                    this.RdoJuchu.Checked = false;  // 念の為
                    this.RdoKobetu.Checked = false; // 念の為
                }
                if (drNyukin.NyukinKubun == NyukinKubun.JYUCHU_TANI)
                {
                    //this.RdoIkkatu.Checked = false; // 念の為
                    this.RdoJuchu.Checked = true;
                    this.RdoKobetu.Checked = false; // 念の為
                }
                if (drNyukin.NyukinKubun == NyukinKubun.KOBETU_TANI)
                {
                    //this.RdoIkkatu.Checked = false; // 念の為
                    this.RdoJuchu.Checked = false;  // 念の為
                    this.RdoKobetu.Checked = true;
                }
            }
            else
            {
                //this.RdoIkkatu.Checked = true;
                this.RdoJuchu.Checked = false;  // 念の為
                this.RdoKobetu.Checked = false; // 念の為
            }

            // 得意先情報・請求情報
            this.TbxTokuiTantouName.Text = drNyukin.IsTokuisakiTantousyaNull() ? "" : drNyukin.TokuisakiTantousya;



            // 受注番号
            this.TbxJuchuNo.Text = drNyukin.IsJyuchuNoNull() ? "" : drNyukin.JyuchuNo;

            if (!drNyukin.IsJyuchuNoNull() && drNyukin.JyuchuNo != "")
            {
                DataUriage.T_UriageDataTable dtUriage = ClassUriage.GetUriage(drNyukin.JyuchuNo, Global.GetConnection());

                this.CreateTokuisakiInfo(dtUriage);
            }


            // 入金日
            this.RdpNyukinBi.SelectedDate = drNyukin.NyukinBi;

            // 営業所
            if (!drNyukin.IsEigyousyoCodeNull())
            {
                this.RadBumon.SelectedValue = drNyukin.EigyousyoCode;

                //ListSet.TantoushaByEigyousho(this.RadTanto, drNyukin.EigyousyoCode);
                if (RadTanto.SelectedValue != "")
                {
                    this.RadTanto.SelectedValue = drNyukin.TantousyaCode;

                }
            }

            // ■入金情報

            // 備考
            this.TbxTekiyou.Text = drNyukin.IsBikouNull() ? "" : drNyukin.Bikou;

            // 現金
            this.TbxGenkin.Text = drNyukin.IsGenkinNull() ? "" : string.Format("{0:#,##0}", drNyukin.Genkin);
            this.TbxGenkinBikou.Text = drNyukin.IsGenkinMemoNull() ? "" : drNyukin.GenkinMemo;

            // 小切手
            this.TbxKogitte.Text = drNyukin.IsKogitteNull() ? "" : string.Format("{0:#,##0}", drNyukin.Kogitte);

            if (!drNyukin.IsKogitteKijitsuNull())
            {
                this.RdpKogitteKijitu.SelectedDate = drNyukin.KogitteKijitsu;
            }
            else
            {
                this.RdpKogitteKijitu.SelectedDate = null;
            }

            this.TbxKogitteBikou.Text = drNyukin.IsKogitteMemoNull() ? "" : drNyukin.KogitteMemo;

            // 振込
            this.TbxFurikomi.Text = drNyukin.IsFurikomiNull() ? "" : string.Format("{0:#,##0}", drNyukin.Furikomi);

            this.TbxFurikomiBikou.Text = drNyukin.IsFurikomiMemoNull() ? "" : drNyukin.FurikomiMemo;

            // 手形
            this.TbxTegata.Text = drNyukin.IsTegataNull() ? "" : string.Format("{0:#,##0}", drNyukin.Tegata);

            if (!drNyukin.IsTegataKijitsuNull())
            {
                this.RdpTegataKijitu.SelectedDate = drNyukin.TegataKijitsu;
            }
            else
            {
                this.RdpTegataKijitu.SelectedDate = null;
            }

            this.TbxTegataBikou.Text = drNyukin.IsTegataMemoNull() ? "" : drNyukin.TegataMemo;
            this.TbxFuridashiNin.Text = drNyukin.IsFurikomininNull() ? "" : drNyukin.Furikominin;

            // 調整
            this.TbxChousei.Text = drNyukin.IsCyouseiNull() ? "" : string.Format("{0:#,##0}", drNyukin.Cyousei);

            this.TbxChouseiBikou.Text = drNyukin.IsCyoseiMemoNull() ? "" : drNyukin.CyoseiMemo;

            // 相殺
            this.TbxSousai.Text = drNyukin.IsSousaiNull() ? "" : string.Format("{0:#,##0}", drNyukin.Sousai);
            this.TbxSousaiBikou.Text = drNyukin.IsSousaiMemoNull() ? "" : drNyukin.SousaiMemo;

            this.Set_KonkaiNyukinGaku_NyukinZan();

            this.TbxGenkin.Attributes["onBlur"] = "OnBlur1('TbxGenkin'); return false;";
            this.TbxKogitte.Attributes["onBlur"] = "OnBlur1('TbxKogitte'); return false;";
            this.TbxFurikomi.Attributes["onBlur"] = "OnBlur1('TbxFurikomi'); return false;";
            this.TbxTegata.Attributes["onBlur"] = "OnBlur1('TbxTegata'); return false;";
            this.TbxChousei.Attributes["onBlur"] = "OnBlur1('TbxChousei'); return false;";
            this.TbxSousai.Attributes["onBlur"] = "OnBlur1('TbxSousai'); return false;";

            return true;

        }

        private void CreateTokuisakiInfo(DataUriage.T_UriageDataTable dtUriage)
        {
            //if (dtUriage.Rows.Count != 0)
            //{
            //    // 得意先ｺｰﾄﾞ
            //    this.TbxTokuiCode.Text = dtUriage[0].IsTokuisakiCodeNull() ? "" : dtUriage[0].TokuisakiCode.ToString();

            //    Dataset.M_Customer_NewRow dr = ClassUriage.M_TokuisakiRow(this.TbxTokuiCode.Text, Global.GetConnection());

            //    //登録日
            //    //this.LblDate.Text = "登録日時：" + dr.TourokuBi.ToString("yyyy/MM/dd HH:mm");

            //    if (dr != null)
            //    {

            //        // 得意先名
            //        TbxTokuiTantouName.Text = dr.CustomerPersonnelName;
            //        TbxTokuiCode.Text = dr.CustomerCode;

            //        //締日
            //        if (dr.CutoffDate == 5)
            //        {
            //            LblShimeBi.Text = "5日締め";
            //        }
            //        else
            //       if (dr.CutoffDate == 10)
            //        {
            //            LblShimeBi.Text = "10日締め";
            //        }
            //        else
            //       if (dr.CutoffDate == 15)
            //        {
            //            LblShimeBi.Text = "15日締め";
            //        }
            //        if (dr.CutoffDate == 20)
            //        {
            //            LblShimeBi.Text = "20日締め";
            //        }
            //        else
            //       if (dr.CutoffDate == 25)
            //        {
            //            LblShimeBi.Text = "25日締め";
            //        }
            //        else
            //       if (dr.CutoffDate == 99)
            //        {
            //            LblShimeBi.Text = "月末締め";
            //        }
            //        if (dr.CutoffDate == 0)
            //        {
            //            LblShimeBi.Text = "随時締め";
            //        }

            //        DataMitumori.M_TantoRow Tdr =
            //            ClassMitumori.GetMTanto(dr.PersonnelCode, Global.GetConnection());

            //        RadBumon.SelectedValue = Tdr.BumonKubun.ToString();
            //        RadTanto.SelectedValue = dr.PersonnelCode.ToString();

            //        RadTokuiMeisyo.SelectedValue = dr.CustomerCode.ToString();

            //        string sCode = dr.CustomerCode.ToString();

            //        DataMitumori.M_Customer_NewRow Mdr =
            //            ClassMitumori.GetMTokuisaki(sCode, Global.GetConnection());
            //        RadTokuiMeisyo.Text = Mdr.Abbreviation;

            //        // 締日
            //    }

            //    // 売上計上日
            //    this.LblUriageKeijouBi.Text = dtUriage[0].IsKeijoBiNull() ? "" : dtUriage[0].KeijoBi.ToString("yyyy/MM/dd");

            //    // 売上金額 税抜きなので税込み表示にさせる
            //    object objUriageKingaku = dtUriage.Compute("SUM(JutyuGokei)", null);
            //    string sUriageKingaku = objUriageKingaku.ToString();
            //    int nUriage = int.Parse(sUriageKingaku);

            //    object objSyohiZei = dtUriage.Compute("SUM(Syohizei)", null);
            //    string sSyohiZei = objSyohiZei.ToString();
            //    int nSyohiZei = int.Parse(sSyohiZei);

            //    int nGokei = nUriage + nSyohiZei;


            //    long nUriageKingaku = 0;
            //    if (objUriageKingaku != null)
            //    {
            //        long.TryParse(nGokei.ToString(), out nUriageKingaku);
            //    }
            //    this.LblUriageKingaku.Text = string.Format("{0:#,##0}", nUriageKingaku);

            //    // 入金済金額
            //    DataUriage.T_NyukinDataTable dtNyukin =
            //        ClassUriage.GetT_NyukinDataTableByKanriCode(dtUriage[0].KanriCode, TorihikisakiType.TOKUISAKI, Global.GetConnection());

            //    object objNyukinKingaku = dtNyukin.Compute("SUM(NyukinGaku)", string.Format("UriageKanriCode <> '{0}'", this.TbxDenBan.Text.Trim()));
            //    long nNyukinGaku = 0;
            //    if (objNyukinKingaku != null)
            //    {
            //        long.TryParse(objNyukinKingaku.ToString(), out nNyukinGaku);
            //    }
            //    this.LblNyukinZumiKingaku.Text = string.Format("{0:#,##0}", nNyukinGaku);

            //    this.LblNyukinKikan.Text = "";

            //    this.Set_KonkaiNyukinGaku_NyukinZan();

            //    this.CreateUriageNyukinMeisai(dtUriage, dtNyukin);
            //}
            //else
            //{
            //    Clear();

            //    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("データがありません");
            //    return;
            //}
        }

        private void CheckData_SetData(DataNyukin.T_NyukinRow drNyukin)
        {
            //// 受注番号、管理コード
            //drNyukin.KanriCode = "";

            //if (this.RdoJuchu.Checked || this.RdoKobetu.Checked)
            //{
            //    if (this.TbxJuchuNo.Text.Trim() == "")
            //    {
            //        this.TbxJuchuNo.Focus();
            //        throw new ApplicationException("受注番号を入力してください。");
            //    }
            //    drNyukin.JyuchuNo = this.TbxJuchuNo.Text.Trim();

            //    DataUriage.T_UriageRow drUriage = ClassUriage.GetT_UriageRow(this.TbxJuchuNo.Text.Trim(), Global.GetConnection());
            //    if (drUriage == null)
            //    {
            //        throw new ApplicationException("受注番号と紐づく売上データが見つからないため、入金登録できません。");
            //    }
            //    drNyukin.KanriCode = drUriage.KanriCode;
            //}

            //// 入金区分
            //if (this.RdoJuchu.Checked)
            //{
            //    drNyukin.NyukinKubun = NyukinKubun.JYUCHU_TANI;
            //}
            //if (this.RdoKobetu.Checked)
            //{
            //    drNyukin.NyukinKubun = NyukinKubun.KOBETU_TANI;
            //}

            //// 入金日
            //if (this.RdpNyukinBi.SelectedDate == null || !this.RdpNyukinBi.SelectedDate.HasValue)
            //{
            //    this.RdpNyukinBi.DateInput.Focus();
            //    throw new ApplicationException("入金日を入力してください。");
            //}

            //drNyukin.NyukinBi = this.RdpNyukinBi.SelectedDate.Value;
            //drNyukin.Year = this.RdpNyukinBi.SelectedDate.Value.Year.ToString();
            //drNyukin.Month = string.Format("{0:00}", this.RdpNyukinBi.SelectedDate.Value.Month);
            //drNyukin.Day = string.Format("{0:00}", this.RdpNyukinBi.SelectedDate.Value.Day);
            //drNyukin.RowNo = 1;

            //// 得意先ｺｰﾄﾞ
            //if (this.TbxTokuiCode.Text.Trim() == "")
            //{
            //    this.TbxTokuiCode.Focus();
            //    throw new ApplicationException("得意先ｺｰﾄﾞを入力してください。");
            //}
            //drNyukin.TorihikisakiCode = this.TbxTokuiCode.Text.Trim();

            //// ｸﾞﾙｰﾌﾟｺｰﾄﾞ 2013.03.25 追加
            ////Dataset.M_TokuisakiRow drTokui = TokuisakiClass.GetM_TokuisakiRow(drNyukin.TorihikisakiCode, Global.GetConnection());
            ////if (drTokui != null && !drTokui.IsGroupCodeNull())
            ////{
            ////    drNyukin.GroupCode = drTokui.GroupCode;
            ////}

            //// 得意先担当者
            //drNyukin.TokuisakiTantousya = this.TbxTokuiTantouName.Text.Trim();

            //// 営業所ｺｰﾄﾞ
            //if (this.RadBumon.SelectedIndex == 0)
            //{
            //    this.RadBumon.Focus();
            //    throw new ApplicationException("営業所を選択してください。");
            //}
            //drNyukin.EigyousyoCode = this.RadBumon.SelectedValue;

            //// 担当者ｺｰﾄﾞ
            //if (this.RadTanto.SelectedIndex == 0)
            //{
            //    this.RadTanto.Focus();
            //    throw new ApplicationException("担当者を選択してください。");
            //}
            //drNyukin.TantousyaCode = this.RadTanto.SelectedValue;

            //// 摘要
            //drNyukin.Bikou = this.TbxTekiyou.Text.Trim();

            //// 現金
            //string strGenkin = this.TbxGenkin.Text.Trim().Replace(",", "");
            //int nGenkin = 0;
            //if (strGenkin != "")
            //{
            //    if (!int.TryParse(strGenkin, out nGenkin))
            //    {
            //        this.TbxGenkin.Focus();
            //        throw new ApplicationException("現金は数値で入力してください。");
            //    }
            //}
            //drNyukin.Genkin = nGenkin;

            //// 現金備考
            //drNyukin.GenkinMemo = this.TbxGenkinBikou.Text.Trim();

            //// 小切手
            //string strKogitte = this.TbxKogitte.Text.Trim().Replace(",", "");
            //int nKogitte = 0;
            //if (strKogitte != "")
            //{
            //    if (!int.TryParse(strKogitte, out nKogitte))
            //    {
            //        this.TbxKogitte.Focus();
            //        throw new ApplicationException("小切手は数値で入力してください。");
            //    }
            //}
            //drNyukin.Kogitte = nKogitte;

            //// 小切手備考
            //drNyukin.KogitteMemo = this.TbxKogitteBikou.Text.Trim();

            //// 小切手期日
            //if (this.RdpKogitteKijitu.SelectedDate != null && this.RdpKogitteKijitu.SelectedDate.HasValue)
            //{
            //    drNyukin.KogitteKijitsu = this.RdpKogitteKijitu.SelectedDate.Value;
            //}
            //else
            //{
            //    drNyukin.SetKogitteKijitsuNull();
            //}

            //// 振込
            //string strFurikomi = this.TbxFurikomi.Text.Trim().Replace(",", "");
            //int nFurikomi = 0;
            //if (strFurikomi != "")
            //{
            //    if (!int.TryParse(strFurikomi, out nFurikomi))
            //    {
            //        this.TbxFurikomi.Focus();
            //        throw new ApplicationException("振込は数値で入力してください。");
            //    }
            //}
            //drNyukin.Furikomi = nFurikomi;

            //// 振込備考
            //drNyukin.FurikomiMemo = this.TbxFurikomiBikou.Text.Trim();

            //// 手形
            //string strTegata = this.TbxTegata.Text.Trim().Replace(",", "");
            //int nTegata = 0;
            //if (strTegata != "")
            //{
            //    if (!int.TryParse(strTegata, out nTegata))
            //    {
            //        this.TbxTegata.Focus();
            //        throw new ApplicationException("手形は数値で入力してください。");
            //    }
            //}
            //drNyukin.Tegata = nTegata;

            //// 手形備考
            //drNyukin.TegataMemo = this.TbxTegataBikou.Text.Trim();

            //// 手形期日
            //if (this.TbxTegata.Text.Trim() != "" && this.TbxTegata.Text.Trim() != "0")
            //{
            //    if (this.RdpTegataKijitu.SelectedDate == null || !this.RdpTegataKijitu.SelectedDate.HasValue)
            //    {
            //        this.RdpTegataKijitu.DateInput.Focus();
            //        throw new ApplicationException("手形期日を入力してください。");
            //    }

            //    drNyukin.TegataKijitsu = this.RdpTegataKijitu.SelectedDate.Value;
            //}
            //else
            //{
            //    drNyukin.SetTegataKijitsuNull();
            //}

            //drNyukin.Furikominin = TbxFuridashiNin.Text;

            //// 調整
            //string strChousei = this.TbxChousei.Text.Trim().Replace(",", "");
            //int nChousei = 0;
            //if (strChousei != "")
            //{
            //    if (!int.TryParse(strChousei, out nChousei))
            //    {
            //        this.TbxChousei.Focus();
            //        throw new ApplicationException("調整は数値で入力してください。");
            //    }
            //}
            //drNyukin.Cyousei = nChousei;

            //// 調整備考
            //drNyukin.CyoseiMemo = this.TbxChouseiBikou.Text.Trim();

            //// 相殺
            //string strSousai = this.TbxSousai.Text.Trim().Replace(",", "");
            //int nSousai = 0;
            //if (strSousai != "")
            //{
            //    if (!int.TryParse(strSousai, out nSousai))
            //    {
            //        this.TbxChousei.Focus();
            //        throw new ApplicationException("相殺は数値で入力してください。");
            //    }
            //}
            //drNyukin.Sousai = nSousai;

            //// 相殺備考
            //drNyukin.SousaiMemo = this.TbxSousaiBikou.Text.Trim();

            //// その他の項目
            //drNyukin.No = 0;    // ダミーの値。
            //drNyukin.UriageKanriCode = "";
            //drNyukin.TorihikisakiType = TorihikisakiType.TOKUISAKI;
            //drNyukin.NyukinGaku = nGenkin + nKogitte + nFurikomi + nTegata;
            //drNyukin.Sashihiki = drNyukin.NyukinGaku + nChousei + nSousai;

            //if(drNyukin.NyukinGaku==0)
            //{
            //    throw new ApplicationException("金額を入力してください。");
            //}

            //if (this.RdoKobetu.Checked)
            //{
            //    int CheckedRowNo = -1;
            //    bool isExistsUriageMeisai = false;

            //    for (int i = 0; i < this.G.Items.Count; i++)
            //    {
            //        CheckBox C = this.G.Items[i].FindControl("C") as CheckBox;

            //        if (C == null) { continue; }

            //        isExistsUriageMeisai = true;

            //        if (!C.Visible || !C.Checked) { continue; }

            //        if (CheckedRowNo > 0) { throw new ApplicationException("チェック可能な売上明細は１つだけです。"); }

            //        string strNo = this.G.Items[i]["No"].Text;

            //        int.TryParse(strNo, out CheckedRowNo);
            //    }

            //    if (!isExistsUriageMeisai) { throw new ApplicationException("売上明細がないため、入金登録ができません。"); }
            //    if (CheckedRowNo == -1) { throw new ApplicationException("売上明細にチェックを付けてください。"); }

            //    drNyukin.RowNo = CheckedRowNo;
            //}
        }

        protected void RdoShusei_CheckedChanged(object sender, EventArgs e)
        {
            this.Clear();

            // 最新の伝番を取得
            int latestDenBan = ClassNyukin.Get_LatestDenBan(TorihikisakiType.TOKUISAKI, this.DdlSortType.SelectedValue + "DESC", Global.GetConnection());

            if (latestDenBan == -1)
            {
                this.Ram.Alert("データが見つかりません。");
                return;
            }

            this.CreateByDenpyoNo(latestDenBan.ToString());

            this.TbxDenBan.Focus();

            this.ShowMsg("最新の入金伝票から順に表示しています。", false);
        }

        protected void RdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.RdpNyukinBi.DateInput.Focus();

            this.RdoJuchu.Checked = true;
            this.RdoKobetu.Checked = false;

            this.Clear();

            this.VsDenBan = null;

            {
                Func<string, string> RegScript = (txt) => { return string.Format("if (!confirm('入金伝票の{0}を行います。よろしいですか？')) return false;", txt); };
                if (this.RdoAdd.Checked) { this.BtnReg.OnClientClick = RegScript("登録"); }
                if (this.RdoShusei.Checked) { this.BtnReg.OnClientClick = RegScript("修正"); }
                this.BtnDel.OnClientClick = this.BtnDel.OnClientClick = RegScript("削除");

                this.TbxJuchuNo.Attributes["onBlur"] = this.RdoAdd.Checked ? "OnBlur1('TbxJuchuNo'); return false;" : "";
                this.TbxTokuiCode.Attributes["onBlur"] = "OnBlur1('TbxTokuiCode'); return false;";
                this.TbxSeikyuYear.Attributes["onBlur"] = "OnBlur1('TbxSeikyuYear'); return false;";
                this.TbxSeikyuMonth.Attributes["onBlur"] = "OnBlur1('TbxSeikyuMonth'); return false;";

                this.TbxGenkin.Attributes["onBlur"] = "OnBlur1('TbxGenkin'); return false;";
                this.TbxKogitte.Attributes["onBlur"] = "OnBlur1('TbxKogitte'); return false;";
                this.TbxFurikomi.Attributes["onBlur"] = "OnBlur1('TbxFurikomi'); return false;";
                this.TbxTegata.Attributes["onBlur"] = "OnBlur1('TbxTegata'); return false;";
                this.TbxChousei.Attributes["onBlur"] = "OnBlur1('TbxChousei'); return false;";
                this.TbxSousai.Attributes["onBlur"] = "OnBlur1('TbxSousai'); return false;";

                this.Lock(false);
            }
        }

        protected void G_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType != GridItemType.Item && e.Item.ItemType != GridItemType.AlternatingItem) { return; }

            UriageNyukinMeisai meisaiData = e.Item.DataItem as UriageNyukinMeisai;

            CheckBox C = e.Item.FindControl("C") as CheckBox;

            if (C != null)
            {
                C.Visible = meisaiData.isUriage;

                C.Checked = (meisaiData.No == this.CurrentRowNo.ToString());

                C.Enabled = (this.RdoAdd.Checked && !this.RdoShusei.Checked);
            }

            if (meisaiData.isUriage)
            {
                e.Item.ForeColor = System.Drawing.Color.Black;
            }
            if (!meisaiData.isUriage && !meisaiData.isBlank)
            {
                e.Item.ForeColor = System.Drawing.Color.Blue;
                if (this.VsDenBan != null && meisaiData.SyouhiCode.Contains(this.VsDenBan))
                {
                    e.Item.Font.Bold = true;
                }
            }
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {

            SqlConnection sqlCon = Global.GetConnection();
            SqlTransaction sqlTran = null;

            try
            {
                sqlCon.Open();
                sqlTran = sqlCon.BeginTransaction();

                // 2013.04.15 TorihikisakiTypeが引数になかったため、支払伝票削除時に、同一伝票番号の入金伝票が削除されるという不具合があった。

                ClassNyukin.SetNyukinRireki(this.VsDenBan, TorihikisakiType.TOKUISAKI, DateTime.Now, SessionManager.User.UserID, "入金削除", sqlTran);

                ClassNyukin.DeleteT_NyukinRow(this.VsDenBan, TorihikisakiType.TOKUISAKI, sqlTran);

                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                if (sqlTran != null) { sqlTran.Rollback(); }
                this.ShowMsg(ex.Message, true);
                return;
            }
            finally
            {
                if (sqlCon != null) { sqlCon.Close(); }
            }

            int LatestDenBan = ClassNyukin.Get_LatestDenBan(TorihikisakiType.TOKUISAKI, this.DdlSortType.SelectedValue + "DESC", Global.GetConnection());

            this.ShowMsg(string.Format("伝番:[{0}] の削除が完了しました。", this.VsDenBan), false);

            this.CreateByDenpyoNo(LatestDenBan.ToString());

            this.TbxDenBan.Focus();
        }

        protected void BtnPostBack_Click(object sender, EventArgs e)
        {
            string cmd = this.Request.Params["__EVENTARGUMENT"];

            try
            {
                if (cmd == "TbxGenkin" || cmd == "TbxKogitte" || cmd == "TbxFurikomi" || cmd == "TbxTegata" || cmd == "TbxChousei" || cmd == "TbxSousai")
                {
                    this.Set_KonkaiNyukinGaku_NyukinZan();
                }

                if (cmd == "TbxGenkin")
                {
                    this.TbxGenkinBikou.Focus();
                }
                if (cmd == "TbxKogitte")
                {
                    this.RdpKogitteKijitu.DateInput.Focus();
                }
                if (cmd == "TbxFurikomi")
                {
                    this.TbxFurikomiBikou.Focus();
                }
                if (cmd == "TbxTegata")
                {
                    this.RdpTegataKijitu.DateInput.Focus();
                }
                if (cmd == "TbxChousei")
                {
                    this.TbxChouseiBikou.Focus();
                }
                if (cmd == "TbxSousai")
                {
                    this.TbxSousaiBikou.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg(cmd + "でエラー", true);
            }
        }

        //protected void TbxGenkin_TextChanged(object sender, EventArgs e)
        //{
        //    NyukinGaku();
        //}

        //protected void TbxKogitte_TextChanged(object sender, EventArgs e)
        //{
        //    NyukinGaku();
        //}

        //protected void TbxFurikomi_TextChanged(object sender, EventArgs e)
        //{
        //    NyukinGaku();
        //}

        //protected void TbxTegata_TextChanged(object sender, EventArgs e)
        //{
        //    NyukinGaku();
        //}

        //protected void TbxChousei_TextChanged(object sender, EventArgs e)
        //{
        //    NyukinGaku();
        //}

        //protected void TbxSousai_TextChanged(object sender, EventArgs e)
        //{
        //    NyukinGaku();
        //}

        //private void NyukinGaku()
        //{
        //    double nKonkaiNyukin = 0;
        //    double nNyukinZan = 0;

        //    //nNyukinZumi = int.Parse(LblNyukinZumiKingaku.Text);
        //    //string sNyukin = LblNyukinZandaka.Text;
        //    //string sNyukin = String.Format("{0:G}", LblNyukinZandaka.Text);
        //    if (LblNyukinZandaka.Text != "")
        //    {
        //        nNyukinZan = double.Parse(LblNyukinZandaka.Text);

        //        if (TbxGenkin.Text != "")
        //        {
        //            nKonkaiNyukin += double.Parse(TbxGenkin.Text);
        //        }
        //        else
        //        {
        //            nKonkaiNyukin += 0;
        //        }

        //        if (TbxKogitte.Text != "")
        //        {
        //            nKonkaiNyukin += double.Parse(TbxKogitte.Text);
        //        }
        //        else
        //        {
        //            nKonkaiNyukin += 0;
        //        }

        //        if (TbxFurikomi.Text != "")
        //        {
        //            nKonkaiNyukin += double.Parse(TbxFurikomi.Text);
        //        }
        //        else
        //        {
        //            nKonkaiNyukin += 0;
        //        }

        //        if (TbxTegata.Text != "")
        //        {
        //            nKonkaiNyukin += double.Parse(TbxTegata.Text);
        //        }
        //        else
        //        {
        //            nKonkaiNyukin += 0;
        //        }

        //        if (TbxChousei.Text != "")
        //        {
        //            nKonkaiNyukin += double.Parse(TbxChousei.Text);
        //        }
        //        else
        //        {
        //            nKonkaiNyukin += 0;
        //        }

        //        if (TbxSousai.Text != "")
        //        {
        //            nKonkaiNyukin += double.Parse(TbxSousai.Text);
        //        }
        //        else
        //        {
        //            nKonkaiNyukin += 0;
        //        }
        //        LblNyukinGaku.Text = nKonkaiNyukin.ToString("#,##0");
        //        nNyukinZan -= nKonkaiNyukin;

        //        LblNyukinZandaka.Text = nNyukinZan.ToString("#,##0");
        //    }
        //}
    }
}
