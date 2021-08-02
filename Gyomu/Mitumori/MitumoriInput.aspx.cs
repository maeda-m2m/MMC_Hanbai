using Core.Sql;
using DLL;
using Gyomu.Mitumori.Syosai;
using System;
using System.Collections;
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

namespace Gyomu.Mitumori
{
    public partial class MitumoriInput : System.Web.UI.Page
    {
        List<CtlMitumoriSyosai> C;

        //HtmlInputFile[] files = null;

        //const int LIST_ID = 60;

        //見積No
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

        //ボタンタイプ
        private string VsType
        {
            get { return Convert.ToString(this.ViewState["VsID"]); }
            set { this.ViewState["VsID"] = value; }
        }

        private int VsRowCount
        {
            get
            {
                return Convert.ToInt16(this.ViewState["VsRowCount"]);
            }
            set
            {
                this.ViewState["VsRowCount"] = value;
            }
        }

        public static int dtcount = 0;

        private enum EnumSetType
        {
            Add = 0,
            Delete = 1,
            Copy = 2,
            Register = 3,
            Create = 4,
            Mitumori = 5
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.BtnDlt.Style.Add("display", "none");
            CreateGoukeiRan();
        }

        private void CreateGoukeiRan()
        {
            int nGokei = 0;
            int nShiire = 0;
            double nSyohizei = 0;
            double nArari = 0;
            int nSuryo = 0;

            for (int i = 0; i < this.G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;

                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblJutyuKingaku");
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblHattyuKingaku");
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblSyohiZei");
                HtmlInputText lblArari = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblArari");
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxjutyuSuryo");
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuSuryou");

                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHyojunKakaku");
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxJutyuTanka");
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuTanka");

                //価格の設定

                //掛率
                int Kakeritu = 0;
                //標準価格
                int Hyojun = 0;
                //受注金額
                int JutyuKin = 0;
                //発注金額
                int HattyuKin = 0;
                //消費税
                int nZei = 0;
                //粗利
                int tmArari = 0;
                //受注数量
                int njSuryo = 0;
                //発注数量
                int nhSuryo = 0;
                //税合計
                int nZeiGokei = 0;
                //発注単価
                int hTanka = 0;

                int.TryParse(TbxHyojunKakaku.Value, out Hyojun);
                int.TryParse(lblKakeritu.Text, out Kakeritu);
                int.TryParse(lblJutyuKingaku.Value, out JutyuKin);
                int.TryParse(lblHattyuKingaku.Value, out HattyuKin);
                int.TryParse(lblSyohiZei.Value, out nZei);
                int.TryParse(TbxHyojunKakaku.Value, out Hyojun);
                int.TryParse(lblKakeritu.Text, out Kakeritu);
                int.TryParse(lblArari.Value, out tmArari);
                int.TryParse(TbxjutyuSuryo.Value, out njSuryo);
                int.TryParse(TbxHaccyuSuryou.Value, out nhSuryo);
                int.TryParse(TbxHaccyuTanka.Value, out hTanka);

                //今回の価格
                int Kakaku = 0;

                //掛率計算 0.01は掛率が％表示ではないため
                if (Kakeritu != 0)
                {
                    Kakaku = Kakeritu * Hyojun / 100;
                }
                else
                {
                    Kakaku = Hyojun;
                }

                //税抜き 税込
                if (RadZeikubun.SelectedValue == "2")
                {
                    //消費税
                    nZei = Kakaku / 10;

                    nZeiGokei = nZei * njSuryo;
                    lblSyohiZei.Value = nZeiGokei.ToString();
                }
                else
                {
                    lblSyohiZei.Value = "0";
                }

                TbxJutyuTanka.Value = Kakaku.ToString();
                //受注合計金額
                double nJTotal = 0;
                nJTotal = Kakaku * njSuryo;
                lblJutyuKingaku.Value = nJTotal.ToString();

                //発注合計金額
                double nHattyuTotal = 0;
                nHattyuTotal = hTanka * nhSuryo;
                lblHattyuKingaku.Value = nHattyuTotal.ToString();

                nGokei += JutyuKin;
                nShiire += HattyuKin;
                nSyohizei += nZeiGokei;
                nArari += tmArari;
                nSuryo += njSuryo;
            }
            double nTotal = (nGokei + nSyohizei);
            lblTotal.Text = nTotal.ToString();
            lblShiireGokei.Text = nShiire.ToString();
            lblGokei.Text = nGokei.ToString();
            lblSyouhizei.Text = nSyohizei.ToString();
            lblArariGokei.Text = nArari.ToString();
            lblSuryou.Text = nSuryo.ToString();
            if (nArari != 0)
            {
                double nArariritu = (nArari / nTotal) * 100;
                double nRitu = Math.Round(nArariritu);
                lblArariritu.Text = nRitu.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BtnDlt.Style.Add("display", "none");
                this.BtnCopy.Style.Add("display", "none");

                home.Style["display"] = "";

                //担当名DropDwonList
                ListSet.SetTanto(RadTanto);

                //得意先、仕入先DDropDownList
                ListSet.SetRyakusyou(RadTokuiMeisyo, RadSekyuMeisyo);
                // ListSet.SetRyakusyou();

                //直送先・施設DropDownList
                ListSet.SetTyokuso(RadTyokusoMeisyo, RadShisetu);

                //CategoryDropDownList
                ListSet.SetCate(RadCate);

                //部門DropDownList
                ListSet.SetBumon(RadBumon);

                //JavaScriptに必要な情報
                Page.Header.DataBind();

                BtnDel.Attributes["onclick"] = "return confirm('削除しますか?');";
                BtnBack.Attributes["onclick"] = "return confirm('一覧情報に戻りますがよろしいですか?');";

                if (SessionManager.HACCYU_NO != null && SessionManager.HACCYU_NO != "")
                {
                    vsNo = SessionManager.HACCYU_NO;
                    VsType = SessionManager.Mitumori_Type;
                }


                //メイン登録画面
                Create();
            }
        }

        private void SyuryouDate()
        {
            //日付の自動計算
            string sKikan = RadKikan.SelectedValue;
            DateTime? dKaisibi = RadSiyouCalendar.SelectedDate;
            string sdate = dKaisibi.ToString();
            DateTime dk = DateTime.Parse(sdate);

            if (sKikan == "oneDay")
            {
                RadSyuryoCalendar.SelectedDate = RadSiyouCalendar.SelectedDate;
            }
            if (sKikan == "TowDay")
            {
                dk = dk.AddDays(1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "ThreeDay")
            {
                dk = dk.AddDays(2);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "fourDay")
            {
                dk = dk.AddDays(3);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "fiveDay")
            {
                dk = dk.AddDays(4);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "oneMonth")
            {

                dk = dk.AddMonths(1);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "twoMonth")
            {
                dk = dk.AddMonths(2);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "threeMonth")
            {
                dk = dk.AddMonths(3);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "fourMonth")
            {
                dk = dk.AddMonths(4);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "fiveMonth")
            {
                dk = dk.AddMonths(5);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "sixMonth")
            {
                dk = dk.AddMonths(6);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "oneYear")
            {
                dk = dk.AddYears(1);
                dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }
            if (sKikan == "allYear")
            {
                //最大日数
                dk = DateTime.Parse("2099/12/30");
                //dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }

            for (int i = 0; i < this.G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;

                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuSyuryo");

                if (i == 0)
                {
                    RadHattyuKaisi.SelectedDate = RadSiyouCalendar.SelectedDate;
                    RadHattyuSyuryo.SelectedDate = RadSyuryoCalendar.SelectedDate;
                }
                else
                if (Chek.Checked)
                {

                    RadHattyuKaisi.SelectedDate = RadSiyouCalendar.SelectedDate;
                    RadHattyuSyuryo.SelectedDate = RadSyuryoCalendar.SelectedDate;
                }
            }
        }

        private void Create()
        {
            if (vsNo == "")
            {
                TbxTax.Value = "10";

                lblNo.Text = "Noは自動です。";
                lblCate.Text = "カテゴリーを<br/>選択してください。";

                //初回入力時は修正ボタンは非表示
                BtnSyusei.Visible = false;
                BtnDel.Visible = false;
                BtnJutyu.Visible = false;

                DateTime date = DateTime.Now;
                RadDayMitumori.SelectedDate = date;
                RadDaySyoukai.SelectedDate = date;
                RadSiyouCalendar.SelectedDate = date;

                //初回はログインしたユーザーが入力者になるよう設定
                lblUser.Text = SessionManager.User.M_user.UserName;

                DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
                DataMitumori.T_RowDataTable Tdt = new DataMitumori.T_RowDataTable();

                //Gの表示
                for (int i = 0; i < 1; i++)
                {
                    this.NewRow(Tdt);
                }
                this.G.DataSource = Tdt;
                dtcount = Tdt.Rows.Count;
                this.G.DataBind();

                HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";

                //新規Grid
                //CreateEdit(Tdt);
            }
            else
            {
                DataMitumori.T_MitumoriDataTable Mdt =
                   ClassMitumori.GetMitumoriTable(vsNo, Global.GetConnection());

                DataMitumori.T_MitumoriHeaderDataTable Hdt =
                    ClassMitumori.GetMitumoriHeader(vsNo, Global.GetConnection());

                lblNo.Text = vsNo;
                if (VsType == "Syusei")
                {
                    BtnToroku.Visible = false;
                    BtnJutyu.Visible = false;

                    //受注したデータは削除修正できないようにする
                    if (Mdt[0].JutyuFlg != false)
                    {
                        BtnSyusei.Visible = BtnDel.Visible = false;
                    }
                    else
                    {
                        BtnSyusei.Visible = BtnDel.Visible = true;
                    }
                }
                else
                if (VsType == "Jutyu")
                {
                    BtnToroku.Visible = false;
                    BtnSyusei.Visible = false;
                    BtnDel.Visible = false;
                    lblMes.Text = "この画面で内容を変えても反映されません。";
                }
                else
                    if (VsType == "Copy")
                {
                    BtnToroku.Visible = true;
                    BtnJutyu.Visible = false;
                    BtnSyusei.Visible = BtnDel.Visible = false;
                    lblNo.Text = "Noは自動です。";
                }

                //Hdtデータ
                lblSuryou.Text = Hdt[0].SouSuryou.ToString();
                lblGokei.Text = Hdt[0].GokeiKingaku.ToString();
                lblShiireGokei.Text = Hdt[0].ShiireKingaku.ToString();
                lblArariGokei.Text = Hdt[0].ArariGokeigaku.ToString();
                lblTotal.Text = Hdt[0].SoukeiGaku.ToString();
                lblArariritu.Text = Hdt[0].Arariritu.ToString();
                lblSyouhizei.Text = Hdt[0].SyohiZeiGokei.ToString();

                lblCate.Text = Mdt[0].CateGory.ToString();
                RadCate.SelectedValue = Mdt[0].CateGory.ToString();
                RadBumon.SelectedValue = Mdt[0].BumonKubun.ToString();
                RadTanto.Text = Mdt[0].TanTouName;
                lblUser.Text = Mdt[0].TourokuName;
                RadTokuiMeisyo.SelectedValue = Mdt[0].TokuisakiCode.ToString();

                RadSekyuMeisyo.SelectedValue = Mdt[0].SeikyusakiMei;

                RadTyokusoMeisyo.SelectedValue = Mdt[0].TyokusousakiMei.ToString();

                RadShisetu.SelectedValue = Mdt[0].SisetuMei.ToString();

                //RadInji.SelectedValue = Hdt[0].SasisasiInjiFlg.ToString();
                //RadDate.SelectedValue = Hdt[0].SasidasiHidukeFlg.ToString();

                RadDayMitumori.SelectedDate = Mdt[0].MitumoriBi;
                RadDaySyoukai.SelectedDate = Mdt[0].Syokaibi;
                RadSiyouCalendar.SelectedDate = Mdt[0].SiyouKaishi;
                RadSyuryoCalendar.SelectedDate = Mdt[0].SiyouOwari;
                lblSuryou.Text = Hdt[0].SouSuryou.ToString();
                if (!Mdt[0].IsTekiyou1Null())
                    TbxTekiyou1.Text = Mdt[0].Tekiyou1;
                if (!Mdt[0].IsTekiyou2Null())
                    TbxTekiyou2.Text = Mdt[0].Tekiyou2;

                if (RadTokuiMeisyo.SelectedValue != "")
                {
                    string sValue = RadTokuiMeisyo.SelectedValue;

                    DataMitumori.M_Customer_NewRow dr =
                        ClassMitumori.GetMTokuisaki(sValue, Global.GetConnection());

                    if (dr.TaxAdviceDistinguish != 2)
                    {
                        RadZeikubun.SelectedValue = dr.TaxAdviceDistinguish.ToString();
                        TbxTax.Value = "0";
                        TbxTax.Visible = false;
                    }
                    else
                    {
                        RadZeikubun.SelectedValue = dr.TaxAdviceDistinguish.ToString();
                        TbxTax.Value = "10";
                        TbxTax.Visible = true;
                    }

                    lblKakeritu.Text = dr.Rate.ToString();
                }

                if (Mdt[0].Zasu != "")
                {
                    RadZasu.SelectedValue = Mdt[0].Zasu;
                }
                else
                {
                    Zasu.Visible = false;
                    TBZasu.Visible = false;
                }

                if (Mdt[0].Kaisu != "")
                {
                    RadKaisu.SelectedValue = Mdt[0].Kaisu;
                }
                else
                {
                    Kaisu.Visible = false;
                    TBKaisu.Visible = false;
                }

                if (Mdt[0].Ryoukin != "")
                {
                    RadRyokin.SelectedValue = Mdt[0].Ryoukin;
                }
                else
                {
                    Ryoukin.Visible = false;
                    TBRyoukin.Visible = false;
                }

                if (Mdt[0].Basyo != "")
                {
                    RadBasyo.SelectedValue = Mdt[0].Basyo;
                }
                else
                {
                    Basyo.Visible = false;
                    TBBasyo.Visible = false;
                }
                DataMitumori.T_RowDataTable Tdt = new DataMitumori.T_RowDataTable();

                //Gの表示
                for (int i = 0; i < 1; i++)
                {
                    this.NewRow(Tdt);
                }
                this.G.DataSource = Mdt;
                dtcount = Tdt.Rows.Count;
                this.G.DataBind();

                HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";

                //修正のGrid
                // CreateEdit2(Mdt);
            }

        }

        //新規Grid表示
        private void CreateEdit(DataMitumori.T_RowDataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                RadComboBox RadSyohinmeisyou = (RadComboBox)CtlMitumoriSyosai.FindControl("RadSyohinmeisyou");
                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuSyuryo");
                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHyojunKakaku");
                TbxHyojunKakaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHyojunKakaku.ClientID);
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblSyohiZei");
                lblSyohiZei.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblSyohiZei.ClientID);
                HtmlInputText lblArari = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblArari");
                lblArari.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblArari.ClientID);
                Label lblHinmei = (Label)CtlMitumoriSyosai.FindControl("lblHinmei");
                Label lblKaisu = (Label)CtlMitumoriSyosai.FindControl("lblKaisu");
                Label lblRyoukin = (Label)CtlMitumoriSyosai.FindControl("lblRyoukin");
                Label lblBasyo = (Label)CtlMitumoriSyosai.FindControl("lblBasyo");
                TextBox TbxHattyuNo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHattyuNo");
                TextBox TbxHattyuSakiNo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHattyuSakiNo");
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxjutyuSuryo");
                TbxjutyuSuryo.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxjutyuSuryo.ClientID);
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxJutyuTanka");
                TbxJutyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxJutyuTanka.ClientID);
                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblJutyuKingaku");
                lblJutyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblJutyuKingaku.ClientID);
                TextBox TbxJTekiyou = (TextBox)CtlMitumoriSyosai.FindControl("TbxJTekiyou");
                RadComboBox RadShisetuName = (RadComboBox)CtlMitumoriSyosai.FindControl("RadShisetuName");
                // Label lblMeisyou = (Label)CtlMitumoriSyosai.FindControl("lblMeisyou");
                Label KmkZasu = (Label)CtlMitumoriSyosai.FindControl("KmkZasu");
                Label lblZasu = (Label)CtlMitumoriSyosai.FindControl("lblZasu");
                // RadComboBox RadMeisyo = (RadComboBox)CtlMitumoriSyosai.FindControl("RadMeisyo");
                Label lblHattyusaki = (Label)CtlMitumoriSyosai.FindControl("lblHattyusaki");
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuSuryou");
                TbxHaccyuSuryou.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuSuryou.ClientID);
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuTanka");
                TbxHaccyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuTanka.ClientID);
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblHattyuKingaku");
                lblHattyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblHattyuKingaku.ClientID);
                TextBox TbxHTekiyo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHTekiyo");
                //20200121追加
                Label lblHinban = (Label)CtlMitumoriSyosai.FindControl("lblHinban");
                Label lblHani = (Label)CtlMitumoriSyosai.FindControl("lblHani");
                Label lblMedia = (Label)CtlMitumoriSyosai.FindControl("lblMedia");

                //ListSet.SetSyohin(RadSyohinmeisyou);
                //ListSet.SetHattyuMei(RadMeisyo);
                //ListSet.SetTyokuso(RadShisetu);

                Label lblKubun = (Label)CtlMitumoriSyosai.FindControl("lblKubun");

                int nRowNo = i + 1;
                lblKubun.Text = nRowNo.ToString();

                HtmlInputButton DltBtn = (HtmlInputButton)CtlMitumoriSyosai.FindControl("DltBtn");
                DltBtn.Attributes["onclick"] = string.Format("CtnRowDlt('{0}')", i);
                DltBtn.Value = "削除";

                HtmlInputButton CopyBtn = (HtmlInputButton)CtlMitumoriSyosai.FindControl("CopyBtn");
                CopyBtn.Attributes["onclick"] = string.Format("CtnRowCopy('{0}')", i);
                CopyBtn.Value = "複製";

                //HtmlInputButton BtnSet = (HtmlInputButton)CtlMitumoriSyosai.FindControl("BtnSet");
                //BtnSet.Attributes["onclick"] = string.Format("CntSetRow('{0}')", i);
                //BtnSet.Value = "施設選択";

                //BtnMeisyou.Attributes["onclick"] = 

                this.SetHidMeisaiClientIdCsv(CtlMitumoriSyosai, i, this.G.Rows.Count);

                //カテゴリー表示の設定
                GCate(CtlMitumoriSyosai);

                if (!dt[i].IsSyouhinCodeNull() && dt[i].SyouhinCode != "")
                {
                    RadSyohinmeisyou.SelectedValue = dt[i].SyouhinCode;
                    RadSyohinmeisyou.Text = dt[i].SyouhinMei;
                }
                if (!dt[i].IsSiyouKaishiNull())
                    RadHattyuKaisi.SelectedDate = dt[i].SiyouKaishi;
                if (!dt[i].IsSiyouOwariNull())
                    RadHattyuSyuryo.SelectedDate = dt[i].SiyouOwari;
                if (!dt[i].IsHyojunKakakuNull())
                    TbxHyojunKakaku.Value = dt[i].HyojunKakaku.ToString();
                if (RadZeikubun.SelectedValue == "2")
                {
                    if (!dt[i].IsSyohizeiNull())
                        lblSyohiZei.Value = dt[i].Syohizei.ToString();
                }
                else
                {
                    lblSyohiZei.Value = "0";
                }
                if (!dt[i].IsArariGakuNull())
                    lblArari.Value = dt[i].ArariGaku.ToString();
                if (!dt[i].IsMekarHinbanNull())
                    lblHinmei.Text = dt[i].MekarHinban;
                if (!dt[i].IsKaisuNull())
                    lblKaisu.Text = dt[i].Kaisu;
                if (!dt[i].IsRyoukinNull())
                    lblRyoukin.Text = dt[i].Ryoukin;
                if (!dt[i].IsBasyoNull())
                    lblBasyo.Text = dt[i].Basyo;
                //TbxHattyuNo.Text = 
                if (!dt[i].IsJutyuSuryouNull())
                    TbxjutyuSuryo.Value = dt[i].JutyuSuryou.ToString();
                if (!dt[i].IsJutyuTankaNull())
                    TbxJutyuTanka.Value = dt[i].JutyuTanka.ToString();
                if (!dt[i].IsJutyuGokeiNull())
                    lblJutyuKingaku.Value = dt[i].JutyuGokei.ToString();
                if (!dt[i].IsSisetuMeiNull())
                {
                    RadShisetuName.SelectedValue = dt[i].SisetuMei;
                    RadShisetuName.Text = dt[i].SisetuMei;
                    //lblMeisyou.Text = dt[i].SisetuMei;
                }
                if (!dt[i].IsZasuNull())
                    lblZasu.Text = dt[i].Zasu;
                if (!dt[i].IsHattyuSakiMeiNull())
                {
                    //RadMeisyo.SelectedValue = dt[i].HttyuSakiMei;
                    lblHattyusaki.Text = dt[i].HattyuSakiMei;
                }
                if (!dt[i].IsHattyuSuryouNull())
                    TbxHaccyuSuryou.Value = dt[i].HattyuSuryou.ToString();
                if (!dt[i].IsHattyuTankaNull())
                    TbxHaccyuTanka.Value = dt[i].HattyuTanka.ToString();
                if (!dt[i].IsHattyuGokeiNull())
                    lblHattyuKingaku.Value = dt[i].HattyuGokei.ToString();

                if (!dt[i].IsRangeNull())
                    lblHani.Text = dt[i].Range;
                if (!dt[i].IsKeitaiMeiNull())
                    lblMedia.Text = dt[i].KeitaiMei;
                if (!dt[i].IsMekarHinbanNull())
                    lblHinban.Text = dt[i].MekarHinban;
                if (!dt[i].IsJutyuTekiyoNull())
                    TbxJTekiyou.Text = dt[i].JutyuTekiyo;
                if (!dt[i].IsHaccyuTekiyoNull())
                    TbxHTekiyo.Text = dt[i].HaccyuTekiyo;

                //javascript
                TbxjutyuSuryo.Attributes["OnBlur"] = TbxJutyuTanka.Attributes["OnBlur"] = TbxHaccyuSuryou.Attributes["OnBlur"] = TbxHaccyuTanka.Attributes["OnBlur"] = TbxTax.Attributes["OnBlur"] =
                string.Format("CalcGoukeiTax('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                TbxjutyuSuryo.ClientID, TbxJutyuTanka.ClientID, lblJutyuKingaku.ClientID, lblSyohiZei.ClientID, TbxHaccyuSuryou.ClientID, TbxHaccyuTanka.ClientID, lblHattyuKingaku.ClientID, lblArari.ClientID, TbxTax.ClientID);

                if (this.HidCsv_TbxJutyuGoukei.Value != "") { this.HidCsv_TbxJutyuGoukei.Value += ","; }
                this.HidCsv_TbxJutyuGoukei.Value += lblJutyuKingaku.ClientID;
                if (this.HidCsv_TbxHattyuGoukei.Value != "") { this.HidCsv_TbxHattyuGoukei.Value += ","; }
                this.HidCsv_TbxHattyuGoukei.Value += lblHattyuKingaku.ClientID;
                if (this.HidCsv_TbxSyouhiZei.Value != "") { this.HidCsv_TbxSyouhiZei.Value += ","; }
                this.HidCsv_TbxSyouhiZei.Value += lblSyohiZei.ClientID;
                if (this.HidCsv_TbxArari.Value != "") { this.HidCsv_TbxArari.Value += ","; }
                this.HidCsv_TbxArari.Value += lblArari.ClientID;
                if (this.HidCsv_Suryo.Value != "") { this.HidCsv_Suryo.Value += ","; }
                this.HidCsv_Suryo.Value += TbxjutyuSuryo.ClientID;

            }
        }


        private void CreateEdit2(DataMitumori.T_MitumoriDataTable dt)
        {
            //修正時の時のGridデータの確認
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataMitumori.T_MitumoriRow dr = dt[i];
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;

                Label KmkKaisu = (Label)CtlMitumoriSyosai.FindControl("KmkKaisu");
                Label KmkRyokin = (Label)CtlMitumoriSyosai.FindControl("KmkRyokin");
                Label KmkBasyo = (Label)CtlMitumoriSyosai.FindControl("KmkBasyo");
                Label KmkZasu = (Label)CtlMitumoriSyosai.FindControl("KmkZasu");
                Label titolZasu = (Label)CtlMitumoriSyosai.FindControl("titolZasu");
                RadComboBox RadSyohinmeisyou = (RadComboBox)CtlMitumoriSyosai.FindControl("RadSyohinmeisyou");
                RadComboBox RadShisetuName = (RadComboBox)CtlMitumoriSyosai.FindControl("RadShisetuName");
                // Label lblMeisyou = (Label)CtlMitumoriSyosai.FindControl("lblMeisyou");
                Label lblHinmei = (Label)CtlMitumoriSyosai.FindControl("lblHinmei");
                // RadComboBox RadMeisyo = (RadComboBox)CtlMitumoriSyosai.FindControl("RadMeisyo");
                Label lblHattyusaki = (Label)CtlMitumoriSyosai.FindControl("lblHattyusaki");
                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHyojunKakaku");
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxJutyuTanka");
                TbxJutyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxJutyuTanka.ClientID);
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxjutyuSuryo");
                TbxjutyuSuryo.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxjutyuSuryo.ClientID);
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuSuryou");
                TbxHaccyuSuryou.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuSuryou.ClientID);
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuTanka");
                TbxHaccyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuTanka.ClientID);
                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuSyuryo");
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblSyohiZei");
                lblSyohiZei.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblSyohiZei.ClientID);
                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblJutyuKingaku");
                lblJutyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblJutyuKingaku.ClientID);
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblHattyuKingaku");
                lblHattyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblHattyuKingaku.ClientID);
                HtmlInputText lblArari = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblArari");
                Label lblKubun = (Label)CtlMitumoriSyosai.FindControl("lblKubun");
                Label lblKaisu = (Label)CtlMitumoriSyosai.FindControl("lblKaisu");
                Label lblRyoukin = (Label)CtlMitumoriSyosai.FindControl("lblRyoukin");
                Label lblBasyo = (Label)CtlMitumoriSyosai.FindControl("lblBasyo");
                Label lblZasu = (Label)CtlMitumoriSyosai.FindControl("lblZasu");
                //20200121追加
                //20200121追加
                Label lblHinban = (Label)CtlMitumoriSyosai.FindControl("lblHinban");
                Label lblHani = (Label)CtlMitumoriSyosai.FindControl("lblHani");
                Label lblMedia = (Label)CtlMitumoriSyosai.FindControl("lblMedia");

                TextBox TbxHTekiyo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHTekiyo");
                TextBox TbxJTekiyou = (TextBox)CtlMitumoriSyosai.FindControl("TbxJTekiyou");


                int nRowNo = i + 1;
                lblKubun.Text = nRowNo.ToString();

                RadSyohinmeisyou.SelectedValue = dt[i].MekarHinban;
                RadSyohinmeisyou.Text = dt[i].SyouhinMei;
                if (!dt[0].IsSisetuMeiNull())
                {
                    RadShisetuName.SelectedValue = dt[i].SisetuMei.ToString();
                    RadShisetuName.Text = dt[i].SisetuMei.ToString();
                }
                //lblMeisyou.Text = dt[i].SisetuMei.ToString();
                // RadMeisyo.SelectedValue = dt[i].HttyuSakiMei;
                lblHattyusaki.Text = dt[i].HttyuSakiMei;

                lblHinmei.Text = dt[i].SyouhinCode;

                TbxHyojunKakaku.Value = dt[i].HyojunKakaku.ToString();
                TbxJutyuTanka.Value = dt[i].JutyuTanka.ToString();
                TbxjutyuSuryo.Value = dt[i].JutyuSuryou.ToString();
                TbxHaccyuTanka.Value = dt[i].HattyuTanka.ToString();
                TbxHaccyuSuryou.Value = dt[i].HattyuSuryou.ToString();

                RadHattyuKaisi.SelectedDate = dt[i].SiyouKaishi;
                RadHattyuSyuryo.SelectedDate = dt[i].SiyouOwari;
                lblSyohiZei.Value = dt[i].Syohizei.ToString();
                lblJutyuKingaku.Value = dt[i].JutyuGokei.ToString();
                lblHattyuKingaku.Value = dt[i].HattyuGokei.ToString();

                lblArari.Value = dt[i].ArariGaku.ToString();

                lblHinban.Text = dt[i].MekarHinban;
                lblHani.Text = dt[i].Range;
                lblMedia.Text = dt[i].KeitaiMei;

                TbxHTekiyo.Text = dt[i].HaccyuTekiyo;
                TbxJTekiyou.Text = dt[i].JutyuTekiyo;

                if (!dt[i].IsKaisuNull())
                    lblKaisu.Text = dt[i].Kaisu;

                if (!dt[i].IsZasuNull())
                    lblZasu.Text = dt[i].Zasu;

                if (!dt[i].IsBasyoNull())
                    lblBasyo.Text = dt[i].Basyo;

                if (!dt[i].IsRyoukinNull())
                    lblRyoukin.Text = dt[i].Ryoukin;

                //Ctlのボタンの設定
                HtmlInputButton DltBtn = (HtmlInputButton)CtlMitumoriSyosai.FindControl("DltBtn");
                DltBtn.Attributes["onclick"] = string.Format("CtnRowDlt('{0}')", i);
                DltBtn.Value = "削除";

                HtmlInputButton CopyBtn = (HtmlInputButton)CtlMitumoriSyosai.FindControl("CopyBtn");
                CopyBtn.Attributes["onclick"] = string.Format("CtnRowCopy('{0}')", i);
                CopyBtn.Value = "複製";

                //削除ボタンのデータを持ってくる
                this.SetHidMeisaiClientIdCsv(CtlMitumoriSyosai, i, this.G.Rows.Count);

                //カテゴリーによって表示、非表示の設定
                GCate(CtlMitumoriSyosai);

                //javascript
                TbxjutyuSuryo.Attributes["OnBlur"] = TbxJutyuTanka.Attributes["OnBlur"] = TbxHaccyuSuryou.Attributes["OnBlur"] = TbxHaccyuTanka.Attributes["OnBlur"] = TbxTax.Attributes["OnBlur"] =
                string.Format("CalcGoukeiTax('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                TbxjutyuSuryo.ClientID, TbxJutyuTanka.ClientID, lblJutyuKingaku.ClientID, lblSyohiZei.ClientID, TbxHaccyuSuryou.ClientID, TbxHaccyuTanka.ClientID, lblHattyuKingaku.ClientID, lblArari.ClientID, TbxTax.ClientID);

                if (this.HidCsv_TbxJutyuGoukei.Value != "")
                {
                    this.HidCsv_TbxJutyuGoukei.Value += ",";
                }
                this.HidCsv_TbxJutyuGoukei.Value += lblJutyuKingaku.ClientID;

                if (this.HidCsv_TbxHattyuGoukei.Value != "")
                {
                    this.HidCsv_TbxHattyuGoukei.Value += ",";
                }
                this.HidCsv_TbxHattyuGoukei.Value += lblHattyuKingaku.ClientID;

                if (this.HidCsv_TbxSyouhiZei.Value != "")
                {
                    this.HidCsv_TbxSyouhiZei.Value += ",";
                }
                this.HidCsv_TbxSyouhiZei.Value += lblSyohiZei.ClientID;

                if (this.HidCsv_TbxArari.Value != "")
                {
                    this.HidCsv_TbxArari.Value += ",";
                }
                this.HidCsv_TbxArari.Value += lblArari.ClientID;

                if (this.HidCsv_Suryo.Value != "")
                {
                    this.HidCsv_Suryo.Value += ",";
                }
                this.HidCsv_Suryo.Value += TbxjutyuSuryo.ClientID;
            }
        }

        private void SetHidMeisaiClientIdCsv(CtlMitumoriSyosai ctlMitumoriSyosai, int currentRowIdx, int maxRowIdx)
        {
            ctlMitumoriSyosai.InitClientId();
            string str = "";
            for (int i = 0; i < ctlMitumoriSyosai.ClientIdList.Count; i++)
            {
                if (str != "")
                    str += ",";
                str += ctlMitumoriSyosai.ClientIdList[i];
            }
            this.HidMeisaiClientIdCsv.Value += str;
            if (currentRowIdx < maxRowIdx - 1)
                this.HidMeisaiClientIdCsv.Value += "\t";
        }

        private void NewRow(DataMitumori.T_RowDataTable dt)
        {
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();

            dr.SyouhinCode = "";

            if (RadZasu.SelectedValue != "0")
                dr.Zasu = RadZasu.SelectedValue;

            if (RadKaisu.SelectedValue != "0")
                dr.Kaisu = RadKaisu.SelectedValue;

            if (RadRyokin.SelectedValue != "0")
                dr.Ryoukin = RadRyokin.SelectedValue;

            if (RadBasyo.SelectedValue != "0")
                dr.Basyo = RadBasyo.SelectedValue;

            if (ChkHukusu.Checked != false)
            {
                dr.SisetuMei = RadShisetu.Text;
            }

            if (Chek.Checked != false)
            {
                if (RadSiyouCalendar.SelectedDate != null)
                {
                    string sDate = RadSiyouCalendar.SelectedDate.ToString();
                    DateTime dDate = DateTime.Parse(sDate);

                    dr.SiyouKaishi = dDate;
                }

                if (RadSyuryoCalendar.SelectedDate != null)
                {
                    string sDate = RadSyuryoCalendar.SelectedDate.ToString();
                    DateTime dDate = DateTime.Parse(sDate);

                    dr.SiyouOwari = dDate;
                }
            }
            dt.AddT_RowRow(dr);
        }

        protected void BtnTokuisaki_Click(object sender, EventArgs e)
        {
            if (RadTokuiMeisyo.SelectedValue != "")
            {
                string sValue = RadTokuiMeisyo.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                //ボタンクリック時に得意先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/TokuisakiSyosai.aspx?" + "&" + QueryString);
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
            else
            {
                string url = string.Format("/Mitumori/Syosai/TokuisakiSyosai.aspx");
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
        }

        protected void BtnSekyusaki_Click(object sender, EventArgs e)
        {
            if (RadSekyuMeisyo.SelectedValue != "")
            {
                string sValue = RadSekyuMeisyo.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                //ボタンクリック時に得意先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SeikyuSyosai.aspx?" + "&" + QueryString);
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
            else
            {
                //ボタンクリック時に請求先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SeikyuSyosai.aspx");
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
        }

        protected void BtnTyokuso_Click(object sender, EventArgs e)
        {
            if (RadTyokusoMeisyo.SelectedValue != "")
            {
                string sValue = RadTyokusoMeisyo.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                //ボタンクリック時に得意先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/TyokusosakiSyosai.aspx?" + "&" + QueryString);
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
            else
            {
                //ボタンクリック時に直送先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/TyokusosakiSyosai.aspx");
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
        }

        protected void BtnSisetu_Click(object sender, EventArgs e)
        {

            if (RadShisetu.SelectedValue != "")
            {
                string sValue = RadShisetu.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                //ボタンクリック時に施設詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SisetuSyosai.aspx?" + "&" + QueryString);
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
            else
            {
                //ボタンクリック時に施設詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SisetuSyosai.aspx");
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
        }

        protected void RadCate_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //カテゴリーによって表示、非表示の設定
            Category();
        }

        private void Category()
        {
            //カテゴリーを選択したらカテゴリーNoを表示する。
            if (RadCate.SelectedValue != "" && RadCate.SelectedValue != "-1")
            {
                string sValue = RadCate.SelectedValue;

                DataDrop.M_CategoryRow dr =
                    ClassDrop.GetCateNo(sValue, Global.GetConnection());

                lblCate.Text = dr.Category.ToString();

                //カテゴリーによって内容を非表示にする
                if (dr.Zaseki == false)
                {
                    lblZasu.Text = "";
                    RadZasu.Text = "";
                    Zasu.Visible = false;
                    TBZasu.Visible = false;
                }
                else
                {
                    lblZasu.Text = "座数";
                    Zasu.Visible = true;
                    TBZasu.Visible = true;
                }

                if (dr.Kaisu == false)
                {
                    lblKaisu.Text = "";
                    RadKaisu.Text = "";
                    Kaisu.Visible = false;
                    TBKaisu.Visible = false;
                }
                else
                {
                    lblKaisu.Text = "回数";
                    Kaisu.Visible = true;
                    TBKaisu.Visible = true;
                }

                if (dr.Ryoukin == false)
                {
                    lblRyoukin.Text = "";
                    RadRyokin.Text = "";
                    Ryoukin.Visible = false;
                    TBRyoukin.Visible = false;
                }
                else
                {
                    lblRyoukin.Text = "料金";
                    Ryoukin.Visible = true;
                    TBRyoukin.Visible = true;
                }

                if (dr.Basyo == false)
                {
                    lblBasyo.Text = "";
                    RadBasyo.Text = "";
                    Basyo.Visible = false;
                    TBBasyo.Visible = false;
                }
                else
                {
                    lblBasyo.Text = "場所";
                    Basyo.Visible = true;
                    TBBasyo.Visible = true;
                }

            }
            else
            {
                lblCate.Text = "カテゴリーを<br/>選択してください。";

                lblZasu.Text = "座数";
                Zasu.Visible = true;
                TBZasu.Visible = true;

                lblKaisu.Text = "回数";
                Kaisu.Visible = true;
                TBKaisu.Visible = true;

                lblRyoukin.Text = "料金";
                Ryoukin.Visible = true;
                TBRyoukin.Visible = true;

                lblBasyo.Text = "場所";
                Basyo.Visible = true;
                TBBasyo.Visible = true;
            }

            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;

                GCate(CtlMitumoriSyosai);
            }
        }

        private void GCate(CtlMitumoriSyosai CtlMitumoriSyosai)
        {
            Label KmkKaisu = (Label)CtlMitumoriSyosai.FindControl("KmkKaisu");
            Label KmkRyokin = (Label)CtlMitumoriSyosai.FindControl("KmkRyokin");
            Label KmkBasyo = (Label)CtlMitumoriSyosai.FindControl("KmkBasyo");
            Label KmkZasu = (Label)CtlMitumoriSyosai.FindControl("KmkZasu");
            Label titolZasu = (Label)CtlMitumoriSyosai.FindControl("titolZasu");
            Label lblZasu = (Label)CtlMitumoriSyosai.FindControl("lblZasu");
            Label lblKaisu = (Label)CtlMitumoriSyosai.FindControl("lblKaisu");
            Label lblRyoukin = (Label)CtlMitumoriSyosai.FindControl("lblRyoukin");
            Label lblBasyo = (Label)CtlMitumoriSyosai.FindControl("lblBasyo");
            Label lblCateG = (Label)CtlMitumoriSyosai.FindControl("lblCateG");
            RadComboBox RadSyohinmeisyou = (RadComboBox)CtlMitumoriSyosai.FindControl("RadSyohinmeisyou");
            HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxJutyuTanka");
            HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuTanka");
            HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHyojunKakaku");

            string sValue = RadCate.SelectedValue;

            //カテゴリー選択時に、RadSyohinmeisyouが空になるので情報を取得
            string sSyouhin = "";

            if (RadSyohinmeisyou.SelectedValue != "")
            {
                sSyouhin = RadSyohinmeisyou.SelectedValue;

            }

            //カテゴリー選択時に、商品を絞り込む
            if (sValue != "")
            {
                ListSet.SetCateSyohin(RadSyohinmeisyou, sValue);
            }
            //else
            //{
            //    ListSet.SetSyohin(RadSyohinmeisyou);
            //}

            lblCateG.Text = RadCate.SelectedValue; ;

            //情報があった場合表示
            if (sSyouhin != "")
            {
                //上で商品カテゴリーを作成し一旦情報がリセットされてしまうため
                RadSyohinmeisyou.SelectedValue = sSyouhin;

                DataMaster.M_Kakaku_NewRow dr =
                ClassMaster.GetM_SyohinCate(sSyouhin, lblCate.Text, Global.GetConnection());

                if (dr != null)
                {
                    TbxJutyuTanka.Value = dr.HyojunKakaku.ToString();
                    TbxHaccyuTanka.Value = dr.ShiireKakaku.ToString();
                    TbxHyojunKakaku.Value = dr.HyojunKakaku.ToString();
                }
                else
                {
                    TbxJutyuTanka.Value = "";
                    TbxHaccyuTanka.Value = "";
                    TbxHyojunKakaku.Value = "";

                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("この得意先には商品情報がありません。");
                    return;
                }
            }

            //カテゴリーによって内容を非表示にする
            if (Zasu.Visible == false)
            {
                titolZasu.Text = "";
                KmkZasu.Text = "";
                lblZasu.Text = "";
            }
            else
            {
                titolZasu.Text = "座数";
                KmkZasu.Text = "座数";
            }

            if (Kaisu.Visible == false)
            {
                KmkKaisu.Text = "";
                lblKaisu.Text = "";
            }
            else
            {
                KmkKaisu.Text = "回数";
            }

            if (Ryoukin.Visible == false)
            {
                KmkRyokin.Text = "";
                lblRyoukin.Text = "";
            }
            else
            {
                KmkRyokin.Text = "料金";
            }

            if (Basyo.Visible == false)
            {
                KmkBasyo.Text = "";
                lblBasyo.Text = "";
            }
            else
            {
                KmkBasyo.Text = "場所";
            }
        }

        protected void RadKikan_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //使用期間と使用開始日が埋まっている場合自動で終了日付が出るようにする
            if (RadKikan.SelectedValue != "" && RadSiyouCalendar.SelectedDate != null)
            {
                SyuryouDate();
            }
        }

        protected void RadSiyouCalendar_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            //使用期間と使用開始日が埋まっている場合自動で終了日付が出るようにする
            if (RadKikan.SelectedValue != "" && RadSiyouCalendar.SelectedDate != null)
            {
                SyuryouDate();
            }
        }

        protected void RadTokuiMeisyo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //得意先コードを選んだ場合,請求先や税込みなどを表示させる
            if (RadTokuiMeisyo.SelectedValue != "" && RadTokuiMeisyo.SelectedValue != "-1")
            {
                string sValue = RadTokuiMeisyo.SelectedValue;

                DataMitumori.M_Customer_NewRow dr =
                    ClassMitumori.GetMTokuisaki(sValue, Global.GetConnection());

                RadSekyuMeisyo.SelectedValue = sValue;
                RadSekyuMeisyo.Text = RadTokuiMeisyo.Text;
                RadZeikubun.SelectedValue = dr.TaxAdviceDistinguish.ToString();
                lblKakeritu.Text = dr.Rate.ToString();

                if (dr.TaxAdviceDistinguish != 1)
                {
                    TbxTax.Visible = true;
                    TbxTax.Value = "10";
                }
                else
                {
                    TbxTax.Visible = false;
                    TbxTax.Value = "0";
                }

                RadTanto.SelectedValue = dr.PersonnelCode.ToString();

                DataMitumori.M_TantoRow Tdr =
                    ClassMitumori.GetMTanto(dr.PersonnelCode, Global.GetConnection());

                if (Tdr != null)
                {
                    RadBumon.SelectedValue = Tdr.BumonKubun.ToString();
                }
                else
                {
                    RadBumon.SelectedValue = "";
                }
            }
            else
            {
                RadSekyuMeisyo.SelectedValue = "";
                lblKakeritu.Text = "";
                RadZeikubun.SelectedValue = "";
                RadTanto.Text = "-------";
                RadBumon.SelectedValue = "";
            }
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            vsNo = "";
            //新規
            BtnSyori();
            BtnBack.Attributes["onclick"] = "return confirm('戻りますか?');";
        }

        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            //修正
            BtnSyori();
            BtnBack.Attributes["onclick"] = "return confirm('戻りますか?');";
        }

        private void BtnSyori()
        {
            try
            {
                bool bError = false;

                DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
                DataMitumori.T_MitumoriHeaderDataTable Hdt = new DataMitumori.T_MitumoriHeaderDataTable();

                DataMitumori.T_MitumoriHeaderRow Hdr = Hdt.NewT_MitumoriHeaderRow();

                int nextID = ClassMitumori.getMitumoriNo(Global.GetConnection());

                //MitumoriNO
                if (vsNo == "")
                {
                    //新規
                    // Hdr.MitumoriNo = nextID.ToString();
                }
                else
                {
                    //Hdr.MitumoriNo = vsNo;
                }

                //ヘッダー情報

                int nCate = 0;

                if (lblCate.Text != "カテゴリーを<br/>選択してください。")
                {
                    nCate = int.Parse(lblCate.Text);
                    Hdr.CateGory = nCate;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("カテゴリーが選択されていません。");
                    return;
                }

                Hdr.CategoryName = RadCate.SelectedItem.Text;

                if (lblGokei.Text != "")
                {
                    int nGokei = int.Parse(lblGokei.Text);
                    Hdr.GokeiKingaku = nGokei;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("合計が空白です。");
                    return;
                }

                if (lblShiireGokei.Text != "")
                {
                    int nShiireGokei = int.Parse(lblShiireGokei.Text);
                    Hdr.ShiireKingaku = nShiireGokei;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("仕入合計が空白です。");
                    return;
                }

                if (lblSyouhizei.Text != "")
                {
                    int nSyouhiZei = int.Parse(lblSyouhizei.Text);
                    Hdr.SyohiZeiGokei = nSyouhiZei;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("消費税が空白です。");
                    return;
                }

                if (RadInji.SelectedItem.Value != "True")
                {
                    //Hdr.SasisasiInjiFlg = false;
                }
                else
                {
                    //Hdr.SasisasiInjiFlg = true;
                }

                if (RadDate.SelectedItem.Value != "True")
                {
                    //Hdr.SasidasiHidukeFlg = false;
                }
                else
                {
                    //Hdr.SasidasiHidukeFlg = true;
                }

                int nSougaku = int.Parse(lblTotal.Text);
                Hdr.SoukeiGaku = nSougaku;

                if (lblArariritu.Text != "")
                {
                    int nArariritu = int.Parse(lblArariritu.Text);
                    Hdr.Arariritu = nArariritu;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("粗利率が空白です。");
                    return;
                }

                int nArarigaku = int.Parse(lblArariGokei.Text);
                Hdr.ArariGokeigaku = nArarigaku;

                int nSouSu = int.Parse(lblSuryou.Text);
                Hdr.SouSuryou = nSouSu;

                if (RadZasu.Text != "")
                { Hdr.Zasu = RadZasu.Text; }

                if (RadKaisu.Text != "")
                { Hdr.Kaisu = RadKaisu.Text; }

                if (RadRyokin.Text != "")
                { Hdr.Ryoukin = RadRyokin.Text; }

                if (RadBasyo.Text != "")
                { Hdr.Basyo = RadBasyo.Text; }

                //GridViewのデータ登録
                for (int i = 0; i < G.Rows.Count; i++)
                {
                    CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                    RadComboBox RadSyohinmeisyou = (RadComboBox)CtlMitumoriSyosai.FindControl("RadSyohinmeisyou");
                    RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuKaisi");
                    RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuSyuryo");
                    HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHyojunKakaku");
                    HtmlInputText lblSyohiZei = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblSyohiZei");
                    HtmlInputText lblArari = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblArari");
                    Label lblHinmei = (Label)CtlMitumoriSyosai.FindControl("lblHinmei");
                    Label lblKaisu = (Label)CtlMitumoriSyosai.FindControl("lblKaisu");
                    Label lblRyoukin = (Label)CtlMitumoriSyosai.FindControl("lblRyoukin");
                    Label lblBasyo = (Label)CtlMitumoriSyosai.FindControl("lblBasyo");
                    TextBox TbxHattyuNo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHattyuNo");
                    TextBox TbxHattyuSakiNo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHattyuSakiNo");
                    HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxjutyuSuryo");
                    HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxJutyuTanka");
                    HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblJutyuKingaku");
                    TextBox TbxJTekiyou = (TextBox)CtlMitumoriSyosai.FindControl("TbxJTekiyou");
                    RadComboBox RadShisetuName = (RadComboBox)CtlMitumoriSyosai.FindControl("RadShisetuName");
                    Label KmkZasu = (Label)CtlMitumoriSyosai.FindControl("KmkZasu");
                    Label lblZasu = (Label)CtlMitumoriSyosai.FindControl("lblZasu");
                    Label lblHattyusaki = (Label)CtlMitumoriSyosai.FindControl("lblHattyusaki");
                    HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuSuryou");
                    HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuTanka");
                    HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblHattyuKingaku");
                    TextBox TbxHTekiyo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHTekiyo");
                    //20200121追加
                    Label lblHinban = (Label)CtlMitumoriSyosai.FindControl("lblHinban");
                    Label lblHani = (Label)CtlMitumoriSyosai.FindControl("lblHani");
                    Label lblMedia = (Label)CtlMitumoriSyosai.FindControl("lblMedia");

                    if (RadSyohinmeisyou.SelectedValue != "")
                    {

                        DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();

                        if (vsNo == "")
                        {
                            dr.MitumoriNo = nextID.ToString();
                        }
                        else
                        {
                            dr.MitumoriNo = vsNo;
                        }

                        dr.RowNo = i + 1;

                        //得意先情報
                        if (RadTokuiMeisyo.SelectedValue != "")
                        {
                            DataMitumori.M_Customer_NewRow Tdr =
                                ClassMitumori.GetMTokuisaki(RadTokuiMeisyo.SelectedValue, Global.GetConnection());

                            dr.TokuisakiMei = Tdr.CustomerName1;
                            dr.TokuisakiCode = Tdr.CustomerCode;
                            if (!Tdr.IsCustomerName2Null())
                                dr.TokuisakiMei2 = Tdr.CustomerName2;
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("得意先名称を選んでください。");
                            return;
                        }

                        //請求先情報
                        if (RadSekyuMeisyo.SelectedValue != "")
                        {
                            dr.SeikyusakiMei = RadSekyuMeisyo.SelectedValue;
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("請求先名称を選んでください。");
                            return;
                        }

                        dr.CateGory = nCate;

                        dr.CategoryName = RadCate.SelectedItem.Text;

                        //直送先情報
                        if (RadTyokusoMeisyo.SelectedValue != "")
                        {
                            DataMitumori.M_Facility_NewRow Tdr =
                              ClassMitumori.GetTyokusosaki(RadTyokusoMeisyo.SelectedValue, Global.GetConnection());

                            dr.TyokusousakiCD = Tdr.FacilityNo;
                            dr.TyokusousakiMei = Tdr.FacilityName1;
                            dr.Jusyo1 = Tdr.Address1;
                            if (!Tdr.IsAddress2Null())
                                dr.Jusyo2 = Tdr.Address2;
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("直送先名称を選んでください。");
                            return;
                        }

                        //施設情報
                        if (RadShisetuName.SelectedValue != "")
                        {
                            //int nsCode = int.Parse(RadShisetu.SelectedValue);

                            DataMitumori.M_Facility_NewRow Tdr =
                              ClassMitumori.GetTyokusosaki(RadShisetuName.SelectedValue, Global.GetConnection());

                            if (i == 0)
                            {
                                if (RadShisetuName.SelectedValue == RadShisetu.SelectedValue)
                                {
                                    dr.SisetuMei = RadShisetuName.SelectedValue;
                                    dr.SisetuJusyo1 = Tdr.Address1;
                                    if (!Tdr.IsAddress2Null())
                                        dr.SisetuJusyo2 = Tdr.Address2;
                                }
                                else
                                {
                                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("1行目の施設がヘッダーと合っていません。");
                                    return;
                                }
                            }
                            else
                            {
                                dr.SisetuMei = RadShisetuName.SelectedValue;
                                dr.SisetuJusyo1 = Tdr.Address1;
                                if (!Tdr.IsAddress2Null())
                                    dr.SisetuJusyo2 = Tdr.Address2;
                            }
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("施設名称を選んでください。");
                            return;
                        }

                        if (RadHattyuKaisi.SelectedDate != null || RadHattyuSyuryo.SelectedDate != null)
                        {
                            dr.SiyouKaishi = RadHattyuKaisi.SelectedDate.Value;
                            dr.SiyouOwari = RadHattyuSyuryo.SelectedDate.Value;
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("日付を選択してください。。");
                            return;
                        }

                        dr.Tekiyou1 = TbxTekiyou1.Text;
                        dr.Tekiyou2 = TbxTekiyou2.Text;

                        if (RadSyohinmeisyou.SelectedValue != "")
                        {
                            //商品名検索

                            dr.SyouhinCode = lblHinmei.Text;
                            dr.SyouhinMei = RadSyohinmeisyou.Text;
                            dr.MekarHinban = lblHinban.Text;
                            dr.KeitaiMei = lblMedia.Text;
                            dr.Range = lblHani.Text;


                            if (TbxjutyuSuryo.Value != "")
                            {
                                int nJutyuSuryo = int.Parse(TbxjutyuSuryo.Value);
                                dr.JutyuSuryou = nJutyuSuryo;
                            }
                            else
                            {
                                lblMes.Text = "受注数量を選んでください。";
                                continue;
                            }
                        }

                        int nHyojunKakaku = int.Parse(TbxHyojunKakaku.Value);
                        dr.HyojunKakaku = nHyojunKakaku.ToString();

                        int nJutyuSu = int.Parse(TbxjutyuSuryo.Value);
                        dr.JutyuSuryou = nJutyuSu;

                        int nJutyuTanak = int.Parse(TbxJutyuTanka.Value);
                        dr.JutyuTanka = nJutyuTanak;

                        int nJutyuGokei = int.Parse(lblJutyuKingaku.Value);
                        dr.JutyuGokei = nJutyuGokei;

                        int nHaccyuSu = int.Parse(TbxHaccyuSuryou.Value);
                        dr.HattyuSuryou = nHaccyuSu;

                        int nHaccyuTanak = int.Parse(TbxHaccyuTanka.Value);
                        dr.HattyuTanka = nHaccyuTanak;

                        int nHaccyuGokei = int.Parse(lblHattyuKingaku.Value);
                        dr.HattyuGokei = nHaccyuGokei;

                        int nZei = int.Parse(lblSyohiZei.Value);
                        dr.Syohizei = nZei;

                        dr.MitumoriBi = RadDayMitumori.SelectedDate.Value;
                        //dr.MitumoriYukoKigen= 
                        dr.TanTouName = RadTanto.Text;
                        dr.TourokuName = lblUser.Text;

                        int nBumon = int.Parse(RadBumon.SelectedValue);
                        dr.BumonKubun = nBumon;

                        dr.Busyo = RadBumon.SelectedItem.Text;

                        dr.Syokaibi = RadDaySyoukai.SelectedDate.Value;

                        if (RadZasu.SelectedValue != "")
                        {
                            dr.Zasu = RadZasu.Text;
                        }
                        if (RadKaisu.SelectedValue != "")
                        {
                            dr.Kaisu = RadKaisu.Text;
                        }
                        if (RadRyokin.SelectedValue != "")
                        {
                            dr.Ryoukin = RadRyokin.Text;
                        }
                        if (RadBasyo.SelectedValue != "")
                        {
                            dr.Basyo = RadBasyo.Text;
                        }

                        dr.JutyuTekiyo = TbxJTekiyou.Text;
                        dr.HaccyuTekiyo = TbxHTekiyo.Text;

                        dr.HttyuSakiMei = lblHattyusaki.Text;
                        dr.Tourokubi = DateTime.Now;

                        int nArari = int.Parse(lblArari.Value);
                        dr.ArariGaku = nArari;

                        dr.JutyuFlg = false;

                        dt.AddT_MitumoriRow(dr);
                    }
                }
                Hdt.AddT_MitumoriHeaderRow(Hdr);

                if (vsNo == "")
                {
                    //新規登録処理
                    bError = ClassMitumori.InsertMitumori(dt, Hdt, Global.GetConnection());
                    if (bError)
                    {
                        lblMes.Text = "エラー";
                    }
                    lblMes.Text = "登録しました。";
                    BtnToroku.Visible = false;
                }
                else
                {
                    //修正処理
                    //登録処理
                    bError = ClassMitumori.UpDateMitumori(vsNo, dt, Hdt, Global.GetConnection());
                    if (bError)
                    {
                        lblMes.Text = "エラー";
                    }
                    lblMes.Text = "修正しました。";
                    BtnSyusei.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMes.Text = ex.Message;
            }
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                //削除処理
                bool bErorr =
                    ClassMitumori.DelMitumori(vsNo, Global.GetConnection());

                G.Visible = false;
                MitumoriGamen.Visible = false;

                BtnDel.Visible = false;
                BtnSyusei.Visible = false;

                lblMes.Text = "削除しました";

                BtnBack.Attributes["onclick"] = "return confirm('戻りますか?');";
            }
            catch (Exception ex)
            {
                lblMes.Text = ex.Message;
                return;
            }
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            int nRow = G.Rows.Count - 1;

            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();

            this.SetGridData(EnumSetType.Add, nRow, ref dt);

            Create(dt);

            //カテゴリーによって表示、非表示の設定
            //Category();
        }

        protected void BtnDlt_Click(object sender, EventArgs e)
        {
            try
            {
                int nCount = G.Rows.Count;
                if (nCount != 1)
                {
                    int nRow = int.Parse(count.Value);

                    DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();

                    this.SetGridData(EnumSetType.Delete, nRow, ref dt);

                    Create(dt);

                    //カテゴリーによって表示、非表示の設定
                    //Category();
                }
            }
            catch (Exception ex)
            {
                lblMes.Text = ex.Message;
            }
        }

        protected void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int nCount = G.Rows.Count;

                int nRow = int.Parse(count.Value);

                DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();

                this.SetGridData(EnumSetType.Copy, nRow, ref dt);

                Create(dt);

                //カテゴリーによって表示、非表示の設定
                //Category();
            }
            catch (Exception ex)
            {
                lblMes.Text = ex.Message;
            }
        }

        private void Create(DataMitumori.T_RowDataTable dt)
        {
            this.VsRowCount = this.G.Rows.Count;

            this.G.PageSize = this.VsRowCount + 1;

            this.G.DataSource = dt;
            dtcount = dt.Rows.Count;
            this.G.DataBind();

            this.C = new List<CtlMitumoriSyosai>();
            this.HidMeisaiClientIdCsv.Value = "";
            HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";

            CreateEdit(dt);
        }

        private void SetGridData(EnumSetType type, int nRow, ref DataMitumori.T_RowDataTable dt)
        {
            int nowCnt = (G.Rows.Count - 1);
            int nCnt = (G.Rows.Count + 1);

            for (int i = 0; i < G.Rows.Count; i++)
            {
                DataMitumori.T_RowRow dr = dt.NewT_RowRow();

                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                RadComboBox RadSyohinmeisyou = (RadComboBox)CtlMitumoriSyosai.FindControl("RadSyohinmeisyou");
                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlMitumoriSyosai.FindControl("RadHattyuSyuryo");
                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHyojunKakaku");
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblSyohiZei");
                HtmlInputText lblArari = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblArari");
                Label lblHinmei = (Label)CtlMitumoriSyosai.FindControl("lblHinmei");
                Label lblKaisu = (Label)CtlMitumoriSyosai.FindControl("lblKaisu");
                Label lblRyoukin = (Label)CtlMitumoriSyosai.FindControl("lblRyoukin");
                Label lblBasyo = (Label)CtlMitumoriSyosai.FindControl("lblBasyo");
                TextBox TbxHattyuNo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHattyuNo");
                TextBox TbxHattyuSakiNo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHattyuSakiNo");
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxjutyuSuryo");
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxJutyuTanka");
                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblJutyuKingaku");
                TextBox TbxJTekiyou = (TextBox)CtlMitumoriSyosai.FindControl("TbxJTekiyou");
                RadComboBox RadShisetuName = (RadComboBox)CtlMitumoriSyosai.FindControl("RadShisetuName");
                //Label lblMeisyou = (Label)CtlMitumoriSyosai.FindControl("lblMeisyou");
                Label KmkZasu = (Label)CtlMitumoriSyosai.FindControl("KmkZasu");
                Label lblZasu = (Label)CtlMitumoriSyosai.FindControl("lblZasu");
                //RadComboBox RadMeisyo = (RadComboBox)CtlMitumoriSyosai.FindControl("RadMeisyo");
                Label lblHattyusaki = (Label)CtlMitumoriSyosai.FindControl("lblHattyusaki");
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuSuryou");
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlMitumoriSyosai.FindControl("TbxHaccyuTanka");
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlMitumoriSyosai.FindControl("lblHattyuKingaku");
                TextBox TbxHTekiyo = (TextBox)CtlMitumoriSyosai.FindControl("TbxHTekiyo");
                //20200121追加
                Label lblHinban = (Label)CtlMitumoriSyosai.FindControl("lblHinban");
                Label lblHani = (Label)CtlMitumoriSyosai.FindControl("lblHani");
                Label lblMedia = (Label)CtlMitumoriSyosai.FindControl("lblMedia");


                if (type == EnumSetType.Delete && i == nRow)
                {
                    //削除ボタンを押したときの処理
                    //データを持ってこないようにする
                    continue;
                }
                else
                {
                    if (RadSyohinmeisyou.SelectedValue != "" && RadSyohinmeisyou.SelectedValue != "-1")
                    {
                        dr.SyouhinMei = RadSyohinmeisyou.Text;
                        dr.SyouhinCode = RadSyohinmeisyou.SelectedValue;
                    }

                    if (RadShisetuName.SelectedValue != "")
                    {
                        dr.SisetuMei = RadShisetuName.SelectedValue;
                    }

                    if (RadHattyuKaisi.SelectedDate != null)
                    {
                        dr.SiyouKaishi = RadHattyuKaisi.SelectedDate.Value;
                    }

                    if (RadHattyuSyuryo.SelectedDate != null)
                    {
                        dr.SiyouOwari = RadHattyuSyuryo.SelectedDate.Value;
                    }

                    if (TbxHyojunKakaku.Value != "")
                    {
                        dr.HyojunKakaku = TbxHyojunKakaku.Value;
                    }

                    if (lblSyohiZei.Value != "")
                    {
                        int nSyouhizei = int.Parse(lblSyohiZei.Value);
                        dr.Syohizei = nSyouhizei;
                    }

                    if (lblArari.Value != "")
                    {
                        int nArari = int.Parse(lblArari.Value);
                        dr.ArariGaku = nArari;
                    }

                    dr.MekarHinban = lblHinmei.Text;

                    if (lblKaisu.Text != "")
                    {
                        dr.Kaisu = lblKaisu.Text;
                    }

                    if (lblRyoukin.Text != "")
                    {
                        dr.Ryoukin = lblRyoukin.Text;
                    }

                    if (lblBasyo.Text != "")
                    {
                        dr.Basyo = lblBasyo.Text;
                    }

                    //発注No?
                    //発注先No?
                    if (TbxjutyuSuryo.Value != "")
                    {
                        int njyutyuSu = int.Parse(TbxjutyuSuryo.Value);
                        dr.JutyuSuryou = njyutyuSu;
                    }

                    if (TbxJutyuTanka.Value != "")
                    {
                        int nJyutyuTanka = int.Parse(TbxJutyuTanka.Value);
                        dr.JutyuTanka = nJyutyuTanka;
                    }

                    if (lblJutyuKingaku.Value != "")
                    {
                        int njyutyuKingaku = int.Parse(lblJutyuKingaku.Value);
                        dr.JutyuGokei = njyutyuKingaku;
                    }
                    //受注備考?


                    //int nCode = int.Parse(RadShisetuName.SelectedValue);
                    //dr.SisetuCode = nCode;
                    //dr.SisetuMei = lblMeisyou.Text;

                    dr.MekarHinban = lblHinban.Text;
                    dr.KeitaiMei = lblMedia.Text;
                    dr.Range = lblHani.Text;
                    dr.JutyuTekiyo = TbxJTekiyou.Text;
                    dr.HaccyuTekiyo = TbxHTekiyo.Text;


                    dr.Zasu = lblZasu.Text;

                    if (TbxHaccyuSuryou.Value != "")
                    {
                        int nHattyuSu = int.Parse(TbxHaccyuSuryou.Value);
                        dr.HattyuSuryou = nHattyuSu;
                    }

                    if (TbxHaccyuTanka.Value != "")
                    {
                        int nHattyuTanak = int.Parse(TbxHaccyuTanka.Value);
                        dr.HattyuTanka = nHattyuTanak;
                    }
                    if (lblHattyuKingaku.Value != "")
                    {
                        int nHattyuKingaku = int.Parse(lblHattyuKingaku.Value);
                        dr.HattyuGokei = nHattyuKingaku;
                    }

                    if (lblHattyusaki.Text != "")
                        dr.HattyuSakiMei = lblHattyusaki.Text;

                    dt.AddT_RowRow(dr);

                    //追加
                    if (type == EnumSetType.Add && i == nRow)
                    {
                        this.NewRow(dt);
                    }
                    else
                    //複製
                        if (type == EnumSetType.Copy && i == nRow)
                    {
                        DataMitumori.T_RowRow dr_copy = dt.NewT_RowRow();
                        dr_copy.ItemArray = dt[i].ItemArray;
                        //dr_copy.RowNo = 0;

                        dt.AddT_RowRow(dr_copy);
                    }
                }
            }
            G.DataSource = dt;
            G.DataBind();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mitumori/MitumoriItiran.aspx");
        }

        protected void RadTokuiMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //得意先検索
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        protected void RadCate_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //カテゴリー検索
            ListSet.SetcateSerch(sender, e);
        }

        protected void RadShisetu_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //施設検索
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //直送先検索
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadBumon_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //部門検索
            ListSet.SerchBumon(sender, e);
        }

        protected void RadTanto_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //担当検索
            ListSet.SerchTanto(sender, e);
        }

        protected void BtnJutyu_Click(object sender, EventArgs e)
        {
            string MitumoriNo = vsNo;

            if (MitumoriNo != "")
            {
                string sMitumori = EnumSetType.Mitumori.ToString();

                //見積の受注フラグを変更
                ClassMitumori.UpDateMitumorijutyu(sMitumori, MitumoriNo, Global.GetConnection());

                //T_Jutyuを作成(中身はフラグ以外T_Mitumoriと同じ)
                DataMitumori.T_MitumoriDataTable Mdt =
                   ClassMitumori.GetMitumoriTable(MitumoriNo, Global.GetConnection());

                DataMitumori.T_MitumoriHeaderDataTable Hdt =
                    ClassMitumori.GetMitumoriHeader(MitumoriNo, Global.GetConnection());

                DataJutyu.T_JutyuDataTable Jdt = new DataJutyu.T_JutyuDataTable();
                DataJutyu.T_JutyuHeaderDataTable HJdt = new DataJutyu.T_JutyuHeaderDataTable();

                for (int i = 0; i < Mdt.Rows.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = Jdt.NewT_JutyuRow();
                    for (int column = 0; column < Mdt.Columns.Count; column++)
                    {
                        if (column != 0 && column != Mdt.Columns.Count - 0)
                            dr[column] = Mdt[i][column];
                    }
                    dr.JutyuNo = int.Parse(MitumoriNo);
                    dr.UriageFlg = false;

                    Jdt.Rows.Add(dr);
                }

                DataJutyu.T_JutyuHeaderRow Jdr = HJdt.NewT_JutyuHeaderRow();
                for (int column = 0; column < Hdt.Columns.Count; column++)
                {
                    if (column != 0 && column != Hdt.Columns.Count - 0)
                        Jdr[column] = Hdt[0][column];
                }
                Jdr.JutyuNo = int.Parse(MitumoriNo);
                HJdt.Rows.Add(Jdr);

                ClassJutyu.GetJutyu(MitumoriNo, HJdt, Jdt, Global.GetConnection());

                Create();

                BtnJutyu.Visible = false;
                lblMes.Text = "受注しました。";
                BtnBack.Attributes["onclick"] = "return confirm('戻りますか?');";
            }
        }

        protected void RadZasu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                Label lblZasu = (Label)CtlMitumoriSyosai.FindControl("lblZasu");

                if (RadZasu.SelectedValue != "0")
                {
                    lblZasu.Text = RadZasu.SelectedValue;
                }
                else
                {
                    lblZasu.Text = "";
                }
            }
        }

        protected void RadKaisu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                Label lblKaisu = (Label)CtlMitumoriSyosai.FindControl("lblKaisu");

                if (RadKaisu.SelectedValue != "0")
                {
                    lblKaisu.Text = RadKaisu.SelectedValue;
                }
                else
                {
                    lblKaisu.Text = "";
                }
            }
        }

        protected void RadRyokin_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                Label lblRyoukin = (Label)CtlMitumoriSyosai.FindControl("lblRyoukin");

                if (RadRyokin.SelectedValue != "0")
                {
                    lblRyoukin.Text = RadRyokin.SelectedValue;
                }
                else
                {
                    lblRyoukin.Text = "";
                }
            }
        }

        protected void RadBasyo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                Label lblBasyo = (Label)CtlMitumoriSyosai.FindControl("lblBasyo");

                if (RadBasyo.SelectedValue != "0")
                {
                    lblBasyo.Text = RadBasyo.SelectedValue;
                }
                else
                {
                    lblBasyo.Text = "";
                }
            }
        }

        protected void RadTyokusoMeisyo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadShisetu.SelectedValue == "" && RadTyokusoMeisyo.SelectedValue != "")
            {
                RadShisetu.SelectedValue = RadTyokusoMeisyo.SelectedValue;
                RadShisetu.Text = RadTyokusoMeisyo.Text;

                if (ChkHukusu.Checked == true)
                {
                    GridShisetu();
                }
            }
        }

        protected void RadShisetu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ChkHukusu.Checked == true)
            {
                GridShisetu();
            }
        }

        private void GridShisetu()
        {
            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlMitumoriSyosai CtlMitumoriSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlMitumoriSyosai;
                RadComboBox RadShisetuName = (RadComboBox)CtlMitumoriSyosai.FindControl("RadShisetuName");

                RadShisetuName.SelectedValue = RadShisetu.SelectedValue;
                RadShisetuName.Text = RadShisetu.Text;
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void RadZeikubun_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadZeikubun.SelectedValue != "2")
            {
                TbxTax.Visible = false;
                TbxTax.Value = "0";
            }
            else
            {
                TbxTax.Visible = true;
                TbxTax.Value = "10";
            }
        }
    }
}