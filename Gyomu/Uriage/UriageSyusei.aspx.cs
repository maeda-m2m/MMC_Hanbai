using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Uriage
{
    public partial class UriageSyusei : System.Web.UI.Page
    {
        List<CtlUriageSyosai> C;

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

        //ボタンタイプ
        private string VsType
        {
            get { return Convert.ToString(this.ViewState["VsID"]); }
            set { this.ViewState["VsID"] = value; }
        }

        private enum EnumSetType
        {
            Add = 0,
            Delete = 1,
            Copy = 2,
            Register = 3,
            Create = 4
        }

        public static int dtcount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BtnDlt.Style.Add("display", "none");
                this.BtnCopy.Style.Add("display", "none");

                //担当名DropDwonList
                ListSet.SetTanto(RadTanto);

                //得意先、仕入先DDropDownList
                ListSet.SetRyakusyou(RadTokuiMeisyo, RadSekyuMeisyo);
                // ListSet.SetRyakusyou();

                //直送先・施設DropDownList
                ListSet.SetTyokuso(RadTyokusoMeisyo, RadSisetMeisyo);

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

                Create();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.BtnDlt.Style.Add("display", "none");
            this.CreateGoukeiRan();
        }

        private void CreateGoukeiRan()
        {
            int nGokei = 0;
            int nShiire = 0;
            int nSyohizei = 0;
            double nArari = 0;
            int nSuryo = 0;

            for (int i = 0; i < this.G.Rows.Count; i++)
            {
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;

                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblJutyuKingaku");
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblHattyuKingaku");
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlUriageSyosai.FindControl("lblSyohiZei");
                HtmlInputText lblArari = (HtmlInputText)CtlUriageSyosai.FindControl("lblArari");
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlUriageSyosai.FindControl("TbxjutyuSuryo");

                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHyojunKakaku");
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxJutyuTanka");

                //価格の設定
                int Kakaku = 0;
                int Hyojun = 0;
                if (lblKakeritu.Text != "")
                {
                    int.TryParse(TbxHyojunKakaku.Value, out Hyojun);
                    int.TryParse(lblKakeritu.Text, out Kakaku);

                    double nKakeritu = Kakaku * 0.01;
                    double Hattyu = nKakeritu * Hyojun;

                    if (Hattyu != 0)
                        TbxJutyuTanka.Value = Hattyu.ToString();
                }

                int tmGokei = 0;
                int tmShiire = 0;
                int tmSyohizei = 0;
                int tmArari = 0;
                int tmSuryo = 0;

                int.TryParse(lblJutyuKingaku.Value, out tmGokei);
                int.TryParse(lblHattyuKingaku.Value, out tmShiire);
                int.TryParse(lblSyohiZei.Value, out tmSyohizei);
                int.TryParse(lblArari.Value, out tmArari);
                int.TryParse(TbxjutyuSuryo.Value, out tmSuryo);

                nGokei += tmGokei;
                nShiire += tmShiire;
                nSyohizei += tmSyohizei;
                nArari += tmArari;
                nSuryo += tmSuryo;
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

        private void Create()
        { 

            //lblNo.Text = vsNo;

            //DataUriage.T_UriageDataTable Mdt =
            //        ClassUriage.GetUriage(vsNo, Global.GetConnection());

            //DataUriage.T_UriageHeaderDataTable Hdt =
            //    ClassUriage.GetUriageHeader(vsNo, Global.GetConnection());

            //DataNyukin.T_NyukinDataTable Ndt =
            //        ClassNyukin.GetT_NyukinData(vsNo, Global.GetConnection());

            //if(Ndt.Rows.Count!=0)
            //{
            //    BtnDel.Visible = false;
            //}

            ////Hdtデータ
            //lblSuryou.Text = Hdt[0].SouSuryou.ToString();
            //lblGokei.Text = Hdt[0].GokeiKingaku.ToString();
            //lblShiireGokei.Text = Hdt[0].ShiireKingaku.ToString();
            //lblArariGokei.Text = Hdt[0].ArariGokeigaku.ToString();
            //lblTotal.Text = Hdt[0].SoukeiGaku.ToString();
            //lblArariritu.Text = Hdt[0].Arariritu.ToString();
            //lblSyouhizei.Text = Hdt[0].SyohiZeiGokei.ToString();

            //lblCate.Text = Mdt[0].CateGory.ToString();
            //RadCate.SelectedValue = Mdt[0].CateGory.ToString();
            //RadBumon.SelectedValue = Mdt[0].BumonKubun.ToString();
            //RadTanto.Text = Mdt[0].TanTouName;
            //lblUser.Text = Mdt[0].TourokuName;
            //RadTokuiMeisyo.SelectedValue = Mdt[0].TokuisakiCode.ToString();

            //RadSekyuMeisyo.SelectedValue = Mdt[0].SeikyusakiMei;

            //RadTyokusoMeisyo.SelectedValue = Mdt[0].TyokusousakiMei.ToString();

            //RadSisetMeisyo.SelectedValue = Mdt[0].SisetuMei.ToString();

            //RadInji.SelectedValue = Hdt[0].SasisasiInjiFlg.ToString();
            //RadDate.SelectedValue = Hdt[0].SasidasiHidukeFlg.ToString();

            //RadDayMitumori.SelectedDate = Mdt[0].KeijoBi;
            //RadDaySyoukai.SelectedDate = Mdt[0].Syokaibi;
            //RadSiyouCalendar.SelectedDate = Mdt[0].SiyouKaishi;
            //RadSyuryoCalendar.SelectedDate = Mdt[0].SiyouOwari;
            //lblSuryou.Text = Hdt[0].SouSuryou.ToString();
            //if (!Mdt[0].IsTekiyou1Null())
            //    TbxTekiyou1.Text = Mdt[0].Tekiyou1;
            //if (!Mdt[0].IsTekiyou2Null())
            //    TbxTekiyou2.Text = Mdt[0].Tekiyou2;

            //if (RadTokuiMeisyo.SelectedValue != "")
            //{
            //    string sValue = RadTokuiMeisyo.SelectedValue;

            //    DataMitumori.M_Customer_NewRow dr =
            //        ClassMitumori.GetMTokuisaki(sValue, Global.GetConnection());

            //    if (dr.TaxAdviceDistinguish != 2)
            //    {
            //        RadZeikubun.SelectedValue = dr.TaxAdviceDistinguish.ToString();
            //        TbxTax.Value = "0";
            //        TbxTax.Visible = false;
            //    }
            //    else
            //    {
            //        RadZeikubun.SelectedValue = dr.TaxAdviceDistinguish.ToString();
            //        TbxTax.Value = "10";
            //        TbxTax.Visible = true;
            //    }

            //    lblKakeritu.Text = dr.Rate.ToString();
            //}

            //if (Mdt[0].Zasu != "")
            //{
            //    RadZasu.SelectedValue = Mdt[0].Zasu;
            //}
            //else
            //{
            //    Zasu.Visible = false;
            //    TBZasu.Visible = false;
            //}

            //if (Mdt[0].Kaisu != "")
            //{
            //    RadKaisu.SelectedValue = Mdt[0].Kaisu;
            //}
            //else
            //{
            //    Kaisu.Visible = false;
            //    TBKaisu.Visible = false;
            //}

            //if (Mdt[0].Ryoukin != "")
            //{
            //    RadRyokin.SelectedValue = Mdt[0].Ryoukin;
            //}
            //else
            //{
            //    Ryoukin.Visible = false;
            //    TBRyoukin.Visible = false;
            //}

            //if (Mdt[0].Basyo != "")
            //{
            //    RadBasyo.SelectedValue = Mdt[0].Basyo;
            //}
            //else
            //{
            //    Basyo.Visible = false;
            //    TBBasyo.Visible = false;
            //}
            //DataUriage.T_RowDataTable Tdt = new DataUriage.T_RowDataTable();

            ////Gの表示
            //for (int i = 0; i < 1; i++)
            //{
            //    this.NewRow(Tdt);
            //}
            //this.G.DataSource = Mdt;
            //dtcount = Tdt.Rows.Count;
            //this.G.DataBind();

            //HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";

            ////修正のGrid
            //CreateEdit(Mdt);
        }

        private void CreateEdit(DataUriage.T_UriageDataTable dt)
        {
            //修正時の時のGridデータの確認
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataUriage.T_UriageRow dr = dt[i];
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;

                Label KmkKaisu = (Label)CtlUriageSyosai.FindControl("KmkKaisu");
                Label KmkRyokin = (Label)CtlUriageSyosai.FindControl("KmkRyokin");
                Label KmkBasyo = (Label)CtlUriageSyosai.FindControl("KmkBasyo");
                Label KmkZasu = (Label)CtlUriageSyosai.FindControl("KmkZasu");
                Label titolZasu = (Label)CtlUriageSyosai.FindControl("titolZasu");
                RadComboBox RadSyohinmeisyou = (RadComboBox)CtlUriageSyosai.FindControl("RadSyohinmeisyou");
                RadComboBox RadShisetuName = (RadComboBox)CtlUriageSyosai.FindControl("RadShisetuName");
                // Label lblMeisyou = (Label)CtlUriageSyosai.FindControl("lblMeisyou");
                Label lblHinmei = (Label)CtlUriageSyosai.FindControl("lblHinmei");
                // RadComboBox RadMeisyo = (RadComboBox)CtlUriageSyosai.FindControl("RadMeisyo");
                Label lblHattyusaki = (Label)CtlUriageSyosai.FindControl("lblHattyusaki");
                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHyojunKakaku");
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxJutyuTanka");
                TbxJutyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxJutyuTanka.ClientID);
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlUriageSyosai.FindControl("TbxjutyuSuryo");
                TbxjutyuSuryo.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxjutyuSuryo.ClientID);
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuSuryou");
                TbxHaccyuSuryou.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuSuryou.ClientID);
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuTanka");
                TbxHaccyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuTanka.ClientID);
                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuSyuryo");
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlUriageSyosai.FindControl("lblSyohiZei");
                lblSyohiZei.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblSyohiZei.ClientID);
                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblJutyuKingaku");
                lblJutyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblJutyuKingaku.ClientID);
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblHattyuKingaku");
                lblHattyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblHattyuKingaku.ClientID);
                HtmlInputText lblArari = (HtmlInputText)CtlUriageSyosai.FindControl("lblArari");
                Label lblKubun = (Label)CtlUriageSyosai.FindControl("lblKubun");
                Label lblKaisu = (Label)CtlUriageSyosai.FindControl("lblKaisu");
                Label lblRyoukin = (Label)CtlUriageSyosai.FindControl("lblRyoukin");
                Label lblBasyo = (Label)CtlUriageSyosai.FindControl("lblBasyo");
                Label lblZasu = (Label)CtlUriageSyosai.FindControl("lblZasu");
                //20200121追加
                //20200121追加
                Label lblHinban = (Label)CtlUriageSyosai.FindControl("lblHinban");
                Label lblHani = (Label)CtlUriageSyosai.FindControl("lblHani");
                Label lblMedia = (Label)CtlUriageSyosai.FindControl("lblMedia");

                TextBox TbxHTekiyo = (TextBox)CtlUriageSyosai.FindControl("TbxHTekiyo");
                TextBox TbxJTekiyou = (TextBox)CtlUriageSyosai.FindControl("TbxJTekiyou");


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
                lblHattyusaki.Text = dt[i].HattyuSakiMei;

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
                HtmlInputButton DltBtn = (HtmlInputButton)CtlUriageSyosai.FindControl("DltBtn");
                DltBtn.Attributes["onclick"] = string.Format("CtnRowDlt('{0}')", i);
                DltBtn.Value = "削除";

                HtmlInputButton CopyBtn = (HtmlInputButton)CtlUriageSyosai.FindControl("CopyBtn");
                CopyBtn.Attributes["onclick"] = string.Format("CtnRowCopy('{0}')", i);
                CopyBtn.Value = "複製";

                //削除ボタンのデータを持ってくる
                this.SetHidMeisaiClientIdCsv(CtlUriageSyosai, i, this.G.Rows.Count);

                //カテゴリーによって表示、非表示の設定
                GCate(CtlUriageSyosai);

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

        private void Category()
        {
            //カテゴリーを選択したらカテゴリーNoを表示する。
            if (RadCate.SelectedValue != "")
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

            for (int i = 0; i < this.G.Rows.Count; i++)
            {
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;

                GCate(CtlUriageSyosai);
            }
        }

        private void GCate(CtlUriageSyosai CtlUriageSyosai)
        {
            Label KmkKaisu = (Label)CtlUriageSyosai.FindControl("KmkKaisu");
            Label KmkRyokin = (Label)CtlUriageSyosai.FindControl("KmkRyokin");
            Label KmkBasyo = (Label)CtlUriageSyosai.FindControl("KmkBasyo");
            Label KmkZasu = (Label)CtlUriageSyosai.FindControl("KmkZasu");
            Label titolZasu = (Label)CtlUriageSyosai.FindControl("titolZasu");
            Label lblZasu = (Label)CtlUriageSyosai.FindControl("lblZasu");
            Label lblKaisu = (Label)CtlUriageSyosai.FindControl("lblKaisu");
            Label lblRyoukin = (Label)CtlUriageSyosai.FindControl("lblRyoukin");
            Label lblBasyo = (Label)CtlUriageSyosai.FindControl("lblBasyo");
            Label lblCateG = (Label)CtlUriageSyosai.FindControl("lblCateG");
            RadComboBox RadSyohinmeisyou = (RadComboBox)CtlUriageSyosai.FindControl("RadSyohinmeisyou");

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
            else
            {
                ListSet.SetSyohin(RadSyohinmeisyou);
            }

            //情報があった場合表示
            if (sSyouhin != "")
            {
                RadSyohinmeisyou.SelectedValue = sSyouhin;
            }

            lblCateG.Text = RadCate.SelectedValue; ;

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

        private void SetHidMeisaiClientIdCsv(CtlUriageSyosai ctlUriageSyosai, int currentRowIdx, int maxRowIdx)
        {
            ctlUriageSyosai.InitClientId();
            string str = "";
            for (int i = 0; i < ctlUriageSyosai.ClientIdList.Count; i++)
            {
                if (str != "")
                    str += ",";
                str += ctlUriageSyosai.ClientIdList[i];
            }
            this.HidMeisaiClientIdCsv.Value += str;
            if (currentRowIdx < maxRowIdx - 1)
                this.HidMeisaiClientIdCsv.Value += "\t";
        }

        private void NewRow(DataUriage.T_RowDataTable dt)
        {
            DataUriage.T_RowRow dr = dt.NewT_RowRow();

            dr.SyouhinCode = "";

            if (RadZasu.SelectedValue != "0")
                dr.Zasu = RadZasu.SelectedValue;

            if (RadKaisu.SelectedValue != "0")
                dr.Kaisu = RadKaisu.SelectedValue;

            if (RadRyokin.SelectedValue != "0")
                dr.Ryoukin = RadRyokin.SelectedValue;

            if (RadBasyo.SelectedValue != "0")
                dr.Basyo = RadBasyo.SelectedValue;

            if(ChkHukusu.Checked!=false)
            {
                if (RadSisetMeisyo.SelectedValue != "")
                    dr.SisetuMei = RadSisetMeisyo.SelectedValue;
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
                dk = DateTime.Parse("2099/12/30");
                //dk = dk.AddDays(-1);
                RadSyuryoCalendar.SelectedDate = dk;
            }

            for (int i = 0; i < this.G.Rows.Count; i++)
            {
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;

                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuSyuryo");

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
        

        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            //修正
            BtnSyori();

            BtnBack.Attributes["onclick"] = "return confirm('戻りますか?');";
        }

        private void BtnSyori()
        {
            //try
            //{
            //    bool bError = false;

            //    DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            //    DataUriage.T_UriageHeaderDataTable Hdt = new DataUriage.T_UriageHeaderDataTable();

            //    DataUriage.T_UriageHeaderRow Hdr = Hdt.NewT_UriageHeaderRow();

            //    //MitumoriNO
            //    Hdr.UriageNo = vsNo;


            //    //ヘッダー情報

            //    int nCate = int.Parse(lblCate.Text);
            //    Hdr.CateGory = nCate;

            //    Hdr.CategoryName = RadCate.SelectedItem.Text;

            //    int nGokei = int.Parse(lblGokei.Text);
            //    Hdr.GokeiKingaku = nGokei;

            //    int nShiireGokei = int.Parse(lblShiireGokei.Text);
            //    Hdr.ShiireKingaku = nShiireGokei;

            //    int nSyouhiZei = int.Parse(lblSyouhizei.Text);
            //    Hdr.SyohiZeiGokei = nSyouhiZei;

            //    if (RadInji.SelectedItem.Value != "True")
            //    {
            //        Hdr.SasisasiInjiFlg = false;
            //    }
            //    else
            //    {
            //        Hdr.SasisasiInjiFlg = true;
            //    }

            //    if (RadDate.SelectedItem.Value != "True")
            //    {
            //        Hdr.SasidasiHidukeFlg = false;
            //    }
            //    else
            //    {
            //        Hdr.SasidasiHidukeFlg = true;
            //    }

            //    int nSougaku = int.Parse(lblTotal.Text);
            //    Hdr.SoukeiGaku = nSougaku;

            //    int nArariritu = int.Parse(lblArariritu.Text);
            //    Hdr.Arariritu = nArariritu;

            //    int nArarigaku = int.Parse(lblArariGokei.Text);
            //    Hdr.ArariGokeigaku = nArarigaku;

            //    int nSouSu = int.Parse(lblSuryou.Text);
            //    Hdr.SouSuryou = nSouSu;

            //    if (RadZasu.Text != "")
            //    { Hdr.Zasu = RadZasu.Text; }

            //    if (RadKaisu.Text != "")
            //    { Hdr.Kaisu = RadKaisu.Text; }

            //    if (RadRyokin.Text != "")
            //    { Hdr.Ryoukin = RadRyokin.Text; }

            //    if (RadBasyo.Text != "")
            //    { Hdr.Basyo = RadBasyo.Text; }

            //    //Hdr.JutyuFlg = false;

            //    for (int i = 0; i < G.Rows.Count; i++)
            //    {
            //        CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
            //        RadComboBox RadSyohinmeisyou = (RadComboBox)CtlUriageSyosai.FindControl("RadSyohinmeisyou");
            //        RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuKaisi");
            //        RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuSyuryo");
            //        HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHyojunKakaku");
            //        HtmlInputText lblSyohiZei = (HtmlInputText)CtlUriageSyosai.FindControl("lblSyohiZei");
            //        HtmlInputText lblArari = (HtmlInputText)CtlUriageSyosai.FindControl("lblArari");
            //        Label lblHinmei = (Label)CtlUriageSyosai.FindControl("lblHinmei");
            //        Label lblKaisu = (Label)CtlUriageSyosai.FindControl("lblKaisu");
            //        Label lblRyoukin = (Label)CtlUriageSyosai.FindControl("lblRyoukin");
            //        Label lblBasyo = (Label)CtlUriageSyosai.FindControl("lblBasyo");
            //        TextBox TbxHattyuNo = (TextBox)CtlUriageSyosai.FindControl("TbxHattyuNo");
            //        TextBox TbxHattyuSakiNo = (TextBox)CtlUriageSyosai.FindControl("TbxHattyuSakiNo");
            //        HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlUriageSyosai.FindControl("TbxjutyuSuryo");
            //        HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxJutyuTanka");
            //        HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblJutyuKingaku");
            //        TextBox TbxJTekiyou = (TextBox)CtlUriageSyosai.FindControl("TbxJTekiyou");
            //        RadComboBox RadShisetuName = (RadComboBox)CtlUriageSyosai.FindControl("RadShisetuName");
            //        //Label lblMeisyou = (Label)CtlUriageSyosai.FindControl("lblMeisyou");
            //        Label KmkZasu = (Label)CtlUriageSyosai.FindControl("KmkZasu");
            //        Label lblZasu = (Label)CtlUriageSyosai.FindControl("lblZasu");
            //        // RadComboBox RadMeisyo = (RadComboBox)CtlUriageSyosai.FindControl("RadMeisyo");
            //        Label lblHattyusaki = (Label)CtlUriageSyosai.FindControl("lblHattyusaki");
            //        HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuSuryou");
            //        HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuTanka");
            //        HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblHattyuKingaku");
            //        TextBox TbxHTekiyo = (TextBox)CtlUriageSyosai.FindControl("TbxHTekiyo");
            //        //20200121追加
            //        Label lblHinban = (Label)CtlUriageSyosai.FindControl("lblHinban");
            //        Label lblHani = (Label)CtlUriageSyosai.FindControl("lblHani");
            //        Label lblMedia = (Label)CtlUriageSyosai.FindControl("lblMedia");

            //        if (RadSyohinmeisyou.SelectedValue != "")
            //        {

            //            DataUriage.T_UriageRow dr = dt.NewT_UriageRow();

            //            dr.UriageNo = vsNo;

            //            dr.RowNo = i + 1;

            //            //得意先情報
            //            if (RadTokuiMeisyo.SelectedValue != "")
            //            {
            //                DataMitumori.M_Customer_NewRow Tdr =
            //                    ClassMitumori.GetMTokuisaki(RadTokuiMeisyo.SelectedValue, Global.GetConnection());

            //                dr.TokuisakiMei = Tdr.CustomerName1;
            //                dr.TokuisakiCode = Tdr.CustomerCode;
            //                if (!Tdr.IsCustomerName2Null())
            //                    dr.TokuisakiMei2 = Tdr.CustomerName2;
            //            }
            //            else
            //            {
            //                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("得意先名称を選んでください。");
            //                return;
            //            }

            //            //請求先情報
            //            if (RadSekyuMeisyo.SelectedValue != "")
            //            {
            //                dr.SeikyusakiMei = RadSekyuMeisyo.Text;
            //            }
            //            else
            //            {
            //                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("請求先名称を選んでください。");
            //                return;
            //            }

            //            //int nCate = int.Parse(lblCate.Text);
            //            dr.CateGory = nCate;

            //            dr.CategoryName = RadCate.SelectedItem.Text;

            //            //直送先情報
            //            if (RadTyokusoMeisyo.SelectedValue != "")
            //            {
            //                DataMitumori.M_Facility_NewRow Tdr =
            //                  ClassMitumori.GetTyokusosaki(RadTyokusoMeisyo.SelectedValue, Global.GetConnection());

            //                dr.TyokusousakiCD = Tdr.FacilityNo;
            //                dr.TyokusousakiMei = Tdr.FacilityName1;
            //                dr.Jusyo1 = Tdr.Address1;
            //                if (!Tdr.IsAddress2Null())
            //                    dr.Jusyo2 = Tdr.Address2;
            //            }
            //            else
            //            {
            //                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("直送先名称を選んでください。");
            //                return;
            //            }

            //            //施設情報
            //            if (RadShisetuName.SelectedValue != "")
            //            {
            //                DataMitumori.M_Facility_NewRow Tdr =
            //                  ClassMitumori.GetTyokusosaki(RadShisetuName.SelectedValue, Global.GetConnection());

            //                if (i == 0)
            //                {
            //                    if (RadShisetuName.SelectedValue == RadSisetMeisyo.SelectedValue)
            //                    {
            //                        dr.SisetuMei = RadShisetuName.SelectedValue;
            //                        //dr.SisetuCode = nsCode;
            //                        dr.SisetuJusyo1 = Tdr.Address1;
            //                        if (!Tdr.IsAddress2Null())
            //                            dr.SisetuJusyo2 = Tdr.Address2;
            //                    }
            //                    else
            //                    {
            //                        Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("1行目の施設がヘッダーと合っていません。");
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    dr.SisetuMei = RadShisetuName.SelectedValue;
            //                    //dr.SisetuCode = nsCode;

            //                    dr.SisetuJusyo1 = Tdr.Address1;
            //                    if (!Tdr.IsAddress2Null())
            //                        dr.SisetuJusyo2 = Tdr.Address2;
            //                }
            //            }
            //            else
            //            {
            //                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("施設名称を選んでください。");
            //                return;
            //            }

            //            if (RadHattyuKaisi.SelectedDate != null || RadHattyuSyuryo.SelectedDate != null)
            //            {
            //                dr.SiyouKaishi = RadHattyuKaisi.SelectedDate.Value;
            //                dr.SiyouOwari = RadHattyuSyuryo.SelectedDate.Value;
            //            }
            //            else
            //            {
            //                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("日付を選択してください。。");
            //                return;
            //            }

            //            dr.Tekiyou1 = TbxTekiyou1.Text;
            //            dr.Tekiyou2 = TbxTekiyou2.Text;

            //            if (RadSyohinmeisyou.SelectedValue != "" )
            //            {
            //                //DataMitumori.M_Syohin_NewRow Sdr =
            //                //    ClassMitumori.GetMSyouhin(RadSyohinmeisyou.SelectedValue,RadMeisyo.SelectedValue ,Global.GetConnection());

            //                dr.SyouhinCode = lblHinmei.Text;
            //                dr.SyouhinMei = RadSyohinmeisyou.Text;
            //                dr.MekarHinban = lblHinban.Text;
            //                dr.KeitaiMei = lblMedia.Text;
            //                dr.Range = lblHani.Text;

            //                if (TbxjutyuSuryo.Value != "")
            //                {
            //                    int nJutyuSuryo = int.Parse(TbxjutyuSuryo.Value);
            //                    dr.JutyuSuryou = nJutyuSuryo;
            //                }
            //                else
            //                {
            //                    lblMes.Text = "受注数量を選んでください。";
            //                    continue;
            //                }
            //            }

            //            int nHyojunKakaku = int.Parse(TbxHyojunKakaku.Value);
            //            dr.HyojunKakaku = nHyojunKakaku;

            //            int nJutyuSu = int.Parse(TbxjutyuSuryo.Value);
            //            dr.JutyuSuryou = nJutyuSu;

            //            int nJutyuTanak = int.Parse(TbxJutyuTanka.Value);
            //            dr.JutyuTanka = nJutyuTanak;

            //            int nJutyuGokei = int.Parse(lblJutyuKingaku.Value);
            //            dr.JutyuGokei = nJutyuGokei;

            //            int nHaccyuSu = int.Parse(TbxHaccyuSuryou.Value);
            //            dr.HattyuSuryou = nHaccyuSu;

            //            int nHaccyuTanak = int.Parse(TbxHaccyuTanka.Value);
            //            dr.HattyuTanka = nHaccyuTanak;

            //            int nHaccyuGokei = int.Parse(lblHattyuKingaku.Value);
            //            dr.HattyuGokei = nHaccyuGokei;

            //            int nZei = int.Parse(lblSyohiZei.Value);
            //            dr.Syohizei = nZei;

            //            dr.KeijoBi = RadDayMitumori.SelectedDate.Value;
            //            //dr.MitumoriYukoKigen= 
            //            dr.TanTouName = RadTanto.Text;
            //            dr.TourokuName = lblUser.Text;

            //            int nBumon = int.Parse(RadBumon.SelectedValue);
            //            dr.BumonKubun = nBumon;

            //            dr.Busyo = RadBumon.SelectedItem.Text;

            //            dr.Syokaibi = RadDaySyoukai.SelectedDate.Value;

            //            if (RadZasu.SelectedValue != "")
            //            {
            //                dr.Zasu = RadZasu.Text;
            //            }
            //            if (RadKaisu.SelectedValue != "")
            //            {
            //                dr.Kaisu = RadKaisu.Text;
            //            }
            //            if (RadRyokin.SelectedValue != "")
            //            {
            //                dr.Ryoukin = RadRyokin.Text;
            //            }
            //            if (RadBasyo.SelectedValue != "")
            //            {
            //                dr.Basyo = RadBasyo.Text;
            //            }

            //            dr.JutyuTekiyo = TbxJTekiyou.Text;
            //            dr.HaccyuTekiyo = TbxHTekiyo.Text;

            //            dr.HattyuSakiMei = lblHattyusaki.Text;
            //            dr.Tourokubi = DateTime.Now;

            //            int nArari = int.Parse(lblArari.Value);
            //            dr.ArariGaku = nArari;

            //            dr.KeijoFlg = false;

            //            dt.AddT_UriageRow(dr);
            //        }
            //    }
            //    Hdt.AddT_UriageHeaderRow(Hdr);

            //    //修正処理
            //    //登録処理
            //    bError = ClassUriage.UpdateUriage(vsNo, dt, Hdt, Global.GetConnection());
            //    if (bError)
            //    {
            //        lblMes.Text = "エラー";
            //    }
            //    lblMes.Text = "修正しました。";
            //    BtnSyusei.Visible = false;
            //}
            //catch (Exception ex)
            //{
            //    lblMes.Text = ex.Message;
            //}
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Uriage/UriageJoho.aspx");
        }

        protected void RadCate_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Category();
        }

        protected void RadTyokusoMeisyo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string type = "Tyokuso";
            SisetYomikomi(type);
        }

        protected void RadSisetMeisyo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string type = "Sisetu";
            SisetYomikomi(type);
        }

        private void SisetYomikomi(string type)
        {
           // if (type == "Sisetu")
           // {
           //     if (RadSisetMeisyo.SelectedValue == "" || RadSisetMeisyo.SelectedValue == "-1")
           //     {
           //         RadSisetMeisyo.SelectedValue = "";
           //     }
           // }
           // else
           //if (type == "Tyokuso")
           // {
           //     if (RadTyokusoMeisyo.SelectedValue != "" && RadTyokusoMeisyo.SelectedValue != "-1")
           //     {
           //         string sValue = RadTyokusoMeisyo.SelectedValue;
           //         DataMitumori.M_TyokusosakiRow dr =
           //            ClassMitumori.GetTyokusosaki(sValue, Global.GetConnection());

           //         RadSisetMeisyo.SelectedValue = "";
           //         RadSisetMeisyo.SelectedValue = dr.TyokusousakiCode.ToString();
           //         RadSisetMeisyo.Text = dr.TyokusousakiMei1;
           //     }
           //     else
           //     {
           //         RadSisetMeisyo.SelectedValue = "";
           //     }
           // }

           // for (int i = 0; i < G.Rows.Count; i++)
           // {
           //     CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
           //     RadComboBox RadShisetu = (RadComboBox)CtlUriageSyosai.FindControl("RadShisetu");

           //     if (i == 0)
           //     {
           //         RadShisetu.SelectedValue = RadSisetMeisyo.SelectedValue;
           //         RadShisetu.Text = RadSisetMeisyo.Text;
           //     }
           //     else
           //     if (ChkHukusu.Checked)
           //     {
           //         RadShisetu.SelectedValue = RadSisetMeisyo.SelectedValue;
           //         RadShisetu.Text = RadSisetMeisyo.Text;
           //     }
           // }

        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void BtnSisetu_Click(object sender, EventArgs e)
        {
            if (RadSisetMeisyo.SelectedValue != "")
            {
                string sValue = RadSisetMeisyo.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                //ボタンクリック時に得意先詳細画面へ別タブで開く
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

        protected void BtnSekyusaki_Click(object sender, EventArgs e)
        {
            if (RadSekyuMeisyo.SelectedValue != "")
            {
                string sValue = RadTokuiMeisyo.SelectedValue;
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

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                //削除処理
                bool bErorr =
                    ClassUriage.DelUriage(vsNo, Global.GetConnection());

                string sJutyu = EnumSetType.Delete.ToString();

                //受注のkeijoFlgを更新
                //ClassJutyu.UpDateJutyuKeijo(sJutyu, vsNo, Global.GetConnection());

                G.Visible = false;
                MitumoriGamen.Visible = false;

                lblMes.Text = "削除しました";

                BtnBack.Attributes["onclick"] = "return confirm('戻りますか?');";
            }
            catch (Exception ex)
            {
                lblMes.Text = ex.Message;
                return;
            }
        }


        protected void RadTokuiMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void BtnDlt_Click(object sender, EventArgs e)
        {
            try
            {
                int nCount = G.Rows.Count;
                if (nCount != 1)
                {
                    int nRow = int.Parse(count.Value);

                    DataUriage.T_RowDataTable dt = new DataUriage.T_RowDataTable();

                    this.SetGridData(EnumSetType.Delete,nRow, ref dt);


                    HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";
                    CreateEdit2(dt);

                    //カテゴリーによって表示、非表示の設定
                    Category();
                }
            }
            catch (Exception ex)
            {
                lblMes.Text = ex.Message;
            }
        }

        private void CreateEdit2( DataUriage.T_RowDataTable dt)
        {
            for (int i = 0; i < this.G.Rows.Count; i++)
            {
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
                RadComboBox RadSyohinmeisyou = (RadComboBox)CtlUriageSyosai.FindControl("RadSyohinmeisyou");
                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuSyuryo");
                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHyojunKakaku");
                TbxHyojunKakaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHyojunKakaku.ClientID);
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlUriageSyosai.FindControl("lblSyohiZei");
                lblSyohiZei.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblSyohiZei.ClientID);
                HtmlInputText lblArari = (HtmlInputText)CtlUriageSyosai.FindControl("lblArari");
                lblArari.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblArari.ClientID);
                Label lblHinmei = (Label)CtlUriageSyosai.FindControl("lblHinmei");
                Label lblKaisu = (Label)CtlUriageSyosai.FindControl("lblKaisu");
                Label lblRyoukin = (Label)CtlUriageSyosai.FindControl("lblRyoukin");
                Label lblBasyo = (Label)CtlUriageSyosai.FindControl("lblBasyo");
                TextBox TbxHattyuNo = (TextBox)CtlUriageSyosai.FindControl("TbxHattyuNo");
                TextBox TbxHattyuSakiNo = (TextBox)CtlUriageSyosai.FindControl("TbxHattyuSakiNo");
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlUriageSyosai.FindControl("TbxjutyuSuryo");
                TbxjutyuSuryo.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxjutyuSuryo.ClientID);
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxJutyuTanka");
                TbxJutyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxJutyuTanka.ClientID);
                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblJutyuKingaku");
                lblJutyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblJutyuKingaku.ClientID);
                Label lblJutyuBiko = (Label)CtlUriageSyosai.FindControl("lblJutyuBiko");
                RadComboBox RadShisetu = (RadComboBox)CtlUriageSyosai.FindControl("RadShisetu");
                Label KmkZasu = (Label)CtlUriageSyosai.FindControl("KmkZasu");
                Label lblZasu = (Label)CtlUriageSyosai.FindControl("lblZasu");
                RadComboBox RadMeisyo = (RadComboBox)CtlUriageSyosai.FindControl("RadMeisyo");
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuSuryou");
                TbxHaccyuSuryou.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuSuryou.ClientID);
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuTanka");
                TbxHaccyuTanka.Attributes["onfocusout"] = string.Format("isNum('{0}');", TbxHaccyuTanka.ClientID);
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblHattyuKingaku");
                lblHattyuKingaku.Attributes["onfocusout"] = string.Format("isNum('{0}');", lblHattyuKingaku.ClientID);
                Label lblHacctyuBiko = (Label)CtlUriageSyosai.FindControl("lblHacctyuBiko");

                ListSet.SetSyohin(RadSyohinmeisyou);
                ListSet.SetShisetu(RadShisetu);
                ListSet.SetHattyuMei(RadMeisyo);

                Label lblKubun = (Label)CtlUriageSyosai.FindControl("lblKubun");

                int nRowNo = i + 1;
                lblKubun.Text = nRowNo.ToString();

                HtmlInputButton DltBtn = (HtmlInputButton)CtlUriageSyosai.FindControl("DltBtn");
                DltBtn.Attributes["onclick"] = string.Format("CtnRowDlt('{0}')", i);
                DltBtn.Value = "削除";

                if (!dt[i].IsSyouhinCodeNull())
                {
                    RadSyohinmeisyou.SelectedValue = dt[i].SyouhinCode;
                }
                if (!dt[i].IsSiyouKaishiNull())
                    RadHattyuKaisi.SelectedDate = dt[i].SiyouKaishi;
                if (!dt[i].IsSiyouOwariNull())
                    RadHattyuSyuryo.SelectedDate = dt[i].SiyouOwari;
                if (!dt[i].IsHyojunKakakuNull())
                    TbxHyojunKakaku.Value = dt[i].HyojunKakaku.ToString();
                if (!dt[i].IsSyohizeiNull())
                    lblSyohiZei.Value = dt[i].Syohizei.ToString();
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
                    RadShisetu.SelectedValue = dt[i].SisetuMei;
                if (!dt[i].IsZasuNull())
                    lblZasu.Text = dt[i].Zasu;
                if (!dt[i].IsHattyuSakiMeiNull())
                    RadMeisyo.SelectedValue = dt[i].HattyuSakiMei;
                if (!dt[i].IsHattyuSuryouNull())
                    TbxHaccyuSuryou.Value = dt[i].HattyuSuryou.ToString();
                if (!dt[i].IsHattyuTankaNull())
                    TbxHaccyuTanka.Value = dt[i].HattyuTanka.ToString();
                if (!dt[i].IsHattyuGokeiNull())
                    lblHattyuKingaku.Value = dt[i].HattyuGokei.ToString();

                //javascript
                TbxjutyuSuryo.Attributes["OnBlur"] = TbxHaccyuSuryou.Attributes["OnBlur"] =
                string.Format("CalcGoukeiTax('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                TbxjutyuSuryo.ClientID, TbxJutyuTanka.ClientID, lblJutyuKingaku.ClientID, lblSyohiZei.ClientID, TbxHaccyuSuryou.ClientID, TbxHaccyuTanka.ClientID, lblHattyuKingaku.ClientID, lblArari.ClientID);

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

        private void SetGridData(EnumSetType type, int nRow, ref DataUriage.T_RowDataTable dt)
        {
            int nowCnt = (G.Rows.Count - 1);
            int nCnt = (G.Rows.Count + 1);

            for (int i = 0; i < G.Rows.Count; i++)
            {
                DataUriage.T_RowRow dr = dt.NewT_RowRow();

                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
                RadComboBox RadSyohinmeisyou = (RadComboBox)CtlUriageSyosai.FindControl("RadSyohinmeisyou");
                RadDatePicker RadHattyuKaisi = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuKaisi");
                RadDatePicker RadHattyuSyuryo = (RadDatePicker)CtlUriageSyosai.FindControl("RadHattyuSyuryo");
                HtmlInputText TbxHyojunKakaku = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHyojunKakaku");
                HtmlInputText lblSyohiZei = (HtmlInputText)CtlUriageSyosai.FindControl("lblSyohiZei");
                HtmlInputText lblArari = (HtmlInputText)CtlUriageSyosai.FindControl("lblArari");
                Label lblHinmei = (Label)CtlUriageSyosai.FindControl("lblHinmei");
                Label lblKaisu = (Label)CtlUriageSyosai.FindControl("lblKaisu");
                Label lblRyoukin = (Label)CtlUriageSyosai.FindControl("lblRyoukin");
                Label lblBasyo = (Label)CtlUriageSyosai.FindControl("lblBasyo");
                TextBox TbxHattyuNo = (TextBox)CtlUriageSyosai.FindControl("TbxHattyuNo");
                TextBox TbxHattyuSakiNo = (TextBox)CtlUriageSyosai.FindControl("TbxHattyuSakiNo");
                HtmlInputText TbxjutyuSuryo = (HtmlInputText)CtlUriageSyosai.FindControl("TbxjutyuSuryo");
                HtmlInputText TbxJutyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxJutyuTanka");
                HtmlInputText lblJutyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblJutyuKingaku");
                Label lblJutyuBiko = (Label)CtlUriageSyosai.FindControl("lblJutyuBiko");
                RadComboBox RadShisetu = (RadComboBox)CtlUriageSyosai.FindControl("RadShisetu");
                Label KmkZasu = (Label)CtlUriageSyosai.FindControl("KmkZasu");
                Label lblZasu = (Label)CtlUriageSyosai.FindControl("lblZasu");
                RadComboBox RadMeisyo = (RadComboBox)CtlUriageSyosai.FindControl("RadMeisyo");
                HtmlInputText TbxHaccyuSuryou = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuSuryou");
                HtmlInputText TbxHaccyuTanka = (HtmlInputText)CtlUriageSyosai.FindControl("TbxHaccyuTanka");
                HtmlInputText lblHattyuKingaku = (HtmlInputText)CtlUriageSyosai.FindControl("lblHattyuKingaku");
                Label lblHacctyuBiko = (Label)CtlUriageSyosai.FindControl("lblHacctyuBiko");

                if (type == EnumSetType.Delete && i == nRow)
                {
                    //削除ボタンを押したときの処理
                    //データを持ってこないようにする
                    continue;
                }
                else
                {
                    if (RadSyohinmeisyou.SelectedValue != "")
                    {
                        dr.SyouhinMei = RadSyohinmeisyou.SelectedItem.Text;
                        dr.SyouhinCode = RadSyohinmeisyou.SelectedItem.Value;
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
                        int nHyojun = int.Parse(TbxHyojunKakaku.Value);
                        dr.HyojunKakaku = nHyojun;
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

                    if (RadShisetu.SelectedValue != "")
                    {
                        dr.SisetuMei = RadShisetu.SelectedValue;
                    }

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

                    if (RadMeisyo.SelectedItem.Text != "")
                        dr.HattyuSakiMei = RadMeisyo.SelectedValue;

                    dt.AddT_RowRow(dr);

                    if (type == EnumSetType.Add && i == nRow)
                    {
                        this.NewRow(dt);
                    }
                    //発注備考
                }
            }

            G.DataSource = dt;
            G.DataBind();
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            int nRow = G.Rows.Count - 1;

            DataUriage.T_RowDataTable dt = new DataUriage.T_RowDataTable();

            this.SetGridData(EnumSetType.Add, nRow, ref dt);

            //カテゴリーによって表示、非表示の設定
            Category();

            HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";
            CreateEdit2(dt);
        }

        protected void RadZasu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < G.Rows.Count; i++)
            {
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
                Label lblZasu = (Label)CtlUriageSyosai.FindControl("lblZasu");

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
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
                Label lblKaisu = (Label)CtlUriageSyosai.FindControl("lblKaisu");

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
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
                Label lblRyoukin = (Label)CtlUriageSyosai.FindControl("lblRyoukin");

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
                CtlUriageSyosai CtlUriageSyosai = G.Rows[i].FindControl("CtlSyosai") as CtlUriageSyosai;
                Label lblBasyo = (Label)CtlUriageSyosai.FindControl("lblBasyo");

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

        protected void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int nCount = G.Rows.Count;

                int nRow = int.Parse(count.Value);

                DataUriage.T_RowDataTable dt = new DataUriage.T_RowDataTable();

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

        private void Create(DataUriage.T_RowDataTable dt)
        {
            this.VsRowCount = this.G.Rows.Count;

            this.G.PageSize = this.VsRowCount + 1;

            this.G.DataSource = dt;
            dtcount = dt.Rows.Count;
            this.G.DataBind();

            this.C = new List<CtlUriageSyosai>();
            this.HidMeisaiClientIdCsv.Value = "";
            HidCsv_TbxJutyuGoukei.Value = HidCsv_TbxHattyuGoukei.Value = HidCsv_TbxSyouhiZei.Value = HidCsv_TbxArari.Value = HidCsv_Suryo.Value = "";

            CreateEdit2(dt);
        }
    }
}