using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;


namespace Gyomu
{
    public partial class OrderInput : System.Web.UI.Page
    {
        public static int dtcount = 0;
        public static object[] objFacility;
        public static string strStartDate;
        public static string strEndDate;
        public static string strCategoryCode;
        public static string strCategoryName;
        public static string strKakeritsu;
        public static string strZeikubun;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                KariNouhin.Visible = false;
                LblTantoStaffCode.Text = HidTantoStaffCode.Value;
                Label3.Text = TbxKakeritsu.Text;
                strKakeritsu = TbxKakeritsu.Text;
                Shimebi.Text = RcbShimebi.Text;
                strZeikubun = RcbTax.Text;
                Err.Text = "";
                End.Text = "";
                if (!IsPostBack)
                {
                    Session["MeisaiJutyuData"] = null;
                    Button1.OnClientClick = string.Format("Meisai2('{0}'); return false;", TBSyousais.ClientID);
                    Button6.OnClientClick = string.Format("Close2('{0}'); return false;", TBSyousais.ClientID);
                    DropDownList9.Attributes["onchange"] = "KikanChange(); return false;";
                    mInput2.Visible = true;
                    CtrlSyousai.Visible = true;
                    SubMenu.Visible = true;
                    SubMenu2.Style["display"] = "none";
                    TBSyousais.Style["display"] = "none";
                    RadDatePicker1.SelectedDate = DateTime.Now;
                    RadDatePicker2.SelectedDate = DateTime.Now;
                    RadDatePicker3.SelectedDate = DateTime.Now;
                    ListSet.SetCategory(RadComboCategory);
                    if (SessionManager.HACCYU_NO != "")
                    {
                        Create2();
                    }
                    else
                    {
                        Create();
                    }
                }
            }
            catch (Exception ex)
            {
                ClassMail.ErrorMail("maeda@m2m-asp.com", "受注明細", SessionManager.HACCYU_NO + "^" + ex.Message);
            }
        }

        private void Create2()
        {
            string MNo = SessionManager.HACCYU_NO;
            string[] strAry = MNo.Split('_');
            string MiNo = strAry[0];
            DataJutyu.T_JutyuHeaderDataTable dtH = ClassJutyu.GetJutyuHeader(MiNo, Global.GetConnection());
            DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu3(MiNo, Global.GetConnection());
            DataJutyu.T_JutyuHeaderRow dr = dtH[0];
            if (!dr.IsFukusuFacilityNull())
            {
                if (dr.FukusuFacility.Equals("True"))
                {
                    CheckBox5.Checked = true;
                    DataMaster.M_Facility_NewDataTable dtF = new DataMaster.M_Facility_NewDataTable();
                    DataMaster.M_Facility_NewRow drF = dtF.NewM_Facility_NewRow();
                    drF.FacilityNo = int.Parse(dr.FacilityCode);
                    drF.Code = dr.FacilityRowCode;
                    if (!dr.IsFacilityNameNull())
                    {
                        drF.FacilityName1 = dr.FacilityName;
                    }
                    if (!dr.IsFacilityName2Null())
                    {
                        drF.FacilityName2 = dr.FacilityName2;
                    }
                    if (!dr.IsFacilityAbbreviationNull())
                    {
                        drF.Abbreviation = dr.FacilityAbbreviation;
                    }
                    if (!dr.IsFacilityResponsibleNull())
                    {
                        drF.FacilityResponsible = dr.FacilityResponsible;
                    }
                    if (!dr.IsFacilityPostNoNull())
                    {
                        drF.PostNo = dr.FacilityPostNo;
                    }
                    if (!dr.IsFacilityAddress1Null())
                    {
                        drF.Address1 = dr.FacilityAddress1;
                    }
                    if (!dr.IsFacilityAddress2Null())
                    {
                        drF.Address2 = dr.FacilityAddress2;
                    }
                    if (!dr.IsFacilityTellNull())
                    {
                        drF.Tell = dr.FacilityTell;
                    }
                    if (!dr.IsFacilityCityCodeNull())
                    {
                        if (!string.IsNullOrEmpty(dr.FacilityCityCode))
                        {
                            drF.CityCode = int.Parse(dr.FacilityCityCode);
                        }
                    }
                    Session["FacilityData"] = drF.ItemArray;
                }
                else
                {
                    CheckBox5.Checked = false;
                }
            }
            if (!dr.IsSiyouKaisiNull())
            {
                RadDatePicker3.SelectedDate = dr.SiyouKaisi;
            }
            if (!dr.IsBikouNull())
            {
                TextBox2.Text = dr.Bikou;
            }
            if (!dr.IsSiyouOwariNull())
            {
                RadDatePicker4.SelectedDate = dr.SiyouOwari;
            }
            if (!dr.IsKakeritsuNull())
            {
                strKakeritsu = dr.Kakeritsu;
                Session["Kakeritsu"] = dr.Kakeritsu;
            }
            if (!dr.IsCategoryNameNull())
            {
                RadComboCategory.SelectedValue = dr.CateGory.ToString();
                strCategoryCode = dr.CateGory.ToString();
                Session["CategoryCode"] = dr.CateGory.ToString();
                strCategoryName = dr.CategoryName;
            }
            //if (!dr.IsKariFLGNull())
            //{
            //    if (dr.KariFLG == "True")
            //    {
            //        CheckBox1.Checked = true;
            //    }
            //}
            if (!dr.IsGokeiKingakuNull())
            {
                Uriagekeijyou.Text = dr.GokeiKingaku.ToString("0,0");
                urikei.Value = dr.GokeiKingaku.ToString();
            }
            if (!dr.IsShiireKingakuNull())
            {
                Shiirekei.Text = dr.ShiireKingaku.ToString("0,0");
                shikei.Value = dr.ShiireKingaku.ToString();
            }
            if (!dr.IsSyohiZeiGokeiNull())
            {
                Zei.Text = dr.SyohiZeiGokei.ToString("0,0");
            }
            if (!dr.IsSoukeiGakuNull())
            {
                UriageGokei.Text = dr.SoukeiGaku.ToString("0,0");
                soukei.Value = dr.SoukeiGaku.ToString();
            }
            if (!dr.IsArariGokeigakuNull())
            {
                TextBox6.Text = dr.ArariGokeigaku.ToString("0,0");
                arakei.Value = dr.ArariGokeigaku.ToString();
            }
            if (!dr.IsSouSuryouNull())
            {
                TextBox10.Text = dr.SouSuryou.ToString();
            }
            if (!dr.IsCateGoryNull())
            {
                CategoryCode.Value = dr.CateGory.ToString();
            }
            if (!dr.IsTokuisakiNameNull())
            {
                if (CheckBox1.Checked == true)
                {
                    KariTokui.Text = dr.TokuisakiRyakusyo;
                    kariSekyu.Text = dr.TokuisakiRyakusyo;
                    string toku = dr.TokuisakiName;
                    DataSet1.M_Tokuisaki2DataTable dg = Class1.GetTokuisakiName(toku, Global.GetConnection());
                    for (int j = 0; j < dg.Count; j++)
                    {
                        RadComboBox1.SelectedValue = dg[j].TokuisakiCode.ToString();
                        TokuisakiCode.Value = dg[j].TokuisakiCode.ToString();
                        CustomerCode.Value = dg[j].CustomerCode;
                    }
                }
                else
                {
                    if (!dr.IsTokuisakiRyakusyoNull())
                    {
                        RadComboBox1.Text = dr.TokuisakiRyakusyo;
                        TbxTokuisakiRyakusyo.Text = dr.TokuisakiRyakusyo;
                    }
                    RcbTokuisakiNameSyousai.Text = dr.TokuisakiName;
                    if (!dr.IsTokuisakiFuriganaNull())
                    {
                        TbxTokuisakiFurigana.Text = dr.TokuisakiFurigana;
                    }
                    if (!dr.IsTokuisakiStaffNull())
                    {
                        TbxTokuisakiStaff.Text = dr.TokuisakiStaff;
                    }
                    if (!dr.IsTokuisakiPostNoNull())
                    {
                        TbxTokuisakiPostNo.Text = dr.TokuisakiPostNo;
                    }
                    if (!dr.IsTokuisakiTELNull())
                    {
                        TbxTokuisakiTEL.Text = dr.TokuisakiTEL;
                    }
                    if (!dr.IsTokuisakiFAXNull())
                    {
                        TbxTokuisakiFax.Text = dr.TokuisakiFAX;
                    }
                    if (!dr.IsTokuisakiDepartmentNull())
                    {
                        TbxTokuisakiDepartment.Text = dr.TokuisakiDepartment;
                    }
                    if (!dr.IsTokuisakiAddressNull())
                    {
                        TbxTokuisakiAddress.Text = dr.TokuisakiAddress;
                    }
                    if (!dr.IsTokuisakiAddress2Null())
                    {
                        TbxTokuisakiAddress1.Text = dr.TokuisakiAddress2;
                    }
                    if (!dr.IsSekyuCustomerCodeNull())
                    {
                        TbxCustomerCode2.Text = dr.SekyuCustomerCode;
                    }
                    if (!dr.IsSekyuTokuisakiCodeNull())
                    {
                        TbxTokuisakiCode2.Text = dr.SekyuTokuisakiCode;
                    }
                    if (!dr.IsSekyuTokuisakiMeiNull())
                    {
                        RcbSeikyusaki.Text = dr.SekyuTokuisakiMei;
                    }
                    if (!dr.IsSekyuTokuisakiFuriganaNull())
                    {
                        TbxTokuisakiFurigana2.Text = dr.SekyuTokuisakiFurigana;

                    }
                    if (!dr.IsSekyuTokuisakiRyakusyoNull())
                    {
                        TbxTokuisakiRyakusyou2.Text = dr.SekyuTokuisakiRyakusyo;
                        RadComboBox3.Text = dr.SekyuTokuisakiRyakusyo;
                    }
                    if (!dr.IsSekyuTokuisakiTantoNull())
                    {
                        TbxTokuisakiStaff2.Text = dr.SekyuTokuisakiTanto;
                    }
                    if (!dr.IsSekyuTokuisakiPostNoNull())
                    {
                        TbxPostNo2.Text = dr.SekyuTokuisakiPostNo;
                    }
                    if (!dr.IsSekyuTokuisakiAddressNull())
                    {
                        TbxTokuisakiAddress2.Text = dr.SekyuTokuisakiAddress;
                    }
                    if (!dr.IsSekyuTokuisakiAddress2Null())
                    {
                        TbxTokuisakiAddress3.Text = dr.SekyuTokuisakiAddress2;
                    }
                    if (!dr.IsSekyuTokuisakiTelNull())
                    {
                        TbxTel2.Text = dr.SekyuTokuisakiTel;
                    }
                    if (!dr.IsSekyuTokuisakiFaxNull())
                    {
                        TbxFAX2.Text = dr.SekyuTokuisakiFax;
                    }
                    if (!dr.IsSekyuTokuisakiDepartmentNull())
                    {
                        TbxDepartment2.Text = dr.SekyuTokuisakiDepartment;
                    }
                    if (!dr.IsSekyuTokuisakiZeikubunNull())
                    {
                        RcbZeikubun2.Text = dr.SekyuTokuisakiZeikubun;
                    }
                    if (!dr.IsSekyuTokuisakiShimebiNull())
                    {
                        RadShimebi2.Text = dr.SekyuTokuisakiShimebi;
                    }
                    string[] tokucode = dr.TokuisakiCode.Trim().Split('/');
                    TbxCustomer.Text = tokucode[0];
                    TbxTokuisakiCode.Text = tokucode[1];
                    string un = Tantou.Text = dr.TantoName;
                    string bn = dr.Bumon;
                    RadComboBox5.Text = dr.TantoName;
                    DataSet1.M_TantoDataTable dtT = Class1.GetStaff3(un, bn, Global.GetConnection());
                    for (int t = 0; t < dtT.Count; t++)
                    {
                        RadComboBox4.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
                    }
                    LblTantoStaffCode.Text = dtT[0].UserID.ToString();
                    RadZeiKubun.SelectedValue = dr.Zeikubun;
                    RcbTax.SelectedValue = dr.Zeikubun;
                    Session["Zeikubun"] = dr.Zeikubun;
                    Shimebi.Text = dr.Shimebi;
                    RcbShimebi.SelectedValue = dr.Shimebi;
                    Label3.Text = dr.Kakeritsu;
                    TbxKakeritsu.Text = dr.Kakeritsu;
                }
            }
            if (!dr.IsFacilityNameNull())
            {
                FacilityRad.Text = dr.FacilityName;
                FacilityRad.SelectedValue = dr.FacilityCode + "/" + dr.FacilityRowCode;
            }
            if (!dr.IsTyokusosakiNameNull())
            {
                if (CheckBox1.Checked == true)
                {
                    KariNouhin.Text = dr.TyokusosakiName;
                }
                else
                {
                    RadComboBox2.Text = dr.TyokusosakiName;
                }
            }
            if (!dr.IsCreateDateNull())
            {
                RadDatePicker2.SelectedDate = dr.CreateDate;
            }

            if (!dr.IsNouhinSisetsuCodeNull())
            {
                TbxNouhinSisetsu.Text = dr.NouhinSisetsuCode.Split('/')[0];
                TbxCode.Text = dr.NouhinSisetsuCode.Split('/')[1];
            }
            if (!dr.IsNouhinTyokusousakiMei1Null())
            {
                RcbNouhinsakiMei.Text = dr.NouhinTyokusousakiMei1;
            }
            if (!dr.IsNouhinTyokusousakiMei2Null())
            {
                TbxNouhinTyokusousakiMei2.Text = dr.NouhinTyokusousakiMei2;
            }
            if (!dr.IsNouhinTyokusousakiRyakusyouNull())
            {
                TbxNouhinsakiRyakusyou.Text = dr.NouhinTyokusousakiRyakusyou;
            }
            if (!dr.IsNouhinTantoNull())
            {
                TbxNouhinsakiTanto.Text = dr.NouhinTanto;
            }
            if (!dr.IsNouhinYubinNull())
            {
                TbxNouhinsakiYubin.Text = dr.NouhinYubin;
            }
            if (!dr.IsNouhinAddressNull())
            {
                TbxNouhinsakiAddress.Text = dr.NouhinAddress;
            }
            if (!dr.IsNouhinAddress2Null())
            {
                TbxNouhinsakiAddress2.Text = dr.NouhinAddress2;
            }
            if (!dr.IsNouhinCityNull())
            {
                RcbNouhinsakiCity.SelectedValue = dr.NouhinCity;
            }
            if (!dr.IsNouhinTellNull())
            {
                TbxNouhinsakiTell.Text = dr.NouhinTell;
            }

            //施設詳細
            if (!dr.IsFacilityCodeNull())
            {
                TbxFacilityCode.Text = dr.FacilityCode;
            }
            if (!dr.IsFacilityRowCodeNull())
            {
                TbxFacilityRowCode.Text = dr.FacilityRowCode;
            }
            if (!dr.IsFacilityNameNull())
            {
                RcbFacility.Text = dr.FacilityName;
            }
            if (!dr.IsFacilityName2Null())
            {
                TbxFacilityName2.Text = dr.FacilityName2;
            }
            if (!dr.IsFacilityAbbreviationNull())
            {
                TbxFaci.Text = dr.FacilityAbbreviation;
            }
            if (!dr.IsFacilityResponsibleNull())
            {
                TbxFacilityResponsible.Text = dr.FacilityResponsible;
            }
            if (!dr.IsFacilityPostNoNull())
            {
                TbxYubin.Text = dr.FacilityPostNo;
            }
            if (!dr.IsFacilityAddress1Null())
            {
                TbxFaciAdress1.Text = dr.FacilityAddress1;
            }
            if (!dr.IsFacilityAddress2Null())
            {
                TbxFaciAdress2.Text = dr.FacilityAddress2;
            }
            if (!dr.IsFacilityCityCodeNull())
            {
                RcbCity.SelectedValue = dr.FacilityCityCode;
            }
            if (!dr.IsFacilityTellNull())
            {
                TbxTel.Text = dr.FacilityTell;
            }
            if (!dr.IsFacilityTitlesNull())
            {
                TbxKeisyo.Text = dr.FacilityTitles;
            }
            if (!dr.IsKibouNoukiNull())
            {
                TbxKibouNouki.Text = dr.KibouNouki;
            }
            if (!dr.IsSyokaiBiNull())
            {
                RadDatePicker1.SelectedDate = dr.SyokaiBi;
            }
            if (!dr.IsMitsumoriBiNull())
            {
                RadDatePicker2.SelectedDate = dr.MitsumoriBi;
            }
            JutyuNo.Text = dr.JutyuNo.ToString();
            Session["MeisaiJutyuData"] = dt;
            this.CtrlSyousai.DataSource = dt;
            this.CtrlSyousai.DataBind();
        }

        private void kari()
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                TextBox KariFaci = (TextBox)CtlMitsuSyosai.FindControl("KariFaci");
                bool ckb = CheckBox1.Checked;
                CtlMitsuSyosai.kari(ckb);
            }
        }

        private void GETkensaku(ClassKensaku.KensakuParam p)
        {
            Tantou.Text = SessionManager.User.M_user.UserName;
            p.Busyo = Tantou.Text;

        }

        private void Create()
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            //Gの表示
            for (int i = 0; i < 1; i++)
            {
                this.NewRow(dt);
            }
            this.CtrlSyousai.DataSource = dt;
            this.CtrlSyousai.DataBind();
        }

        private void NewRow(DataMitumori.T_RowDataTable dt)
        {
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            dt.AddT_RowRow(dr);
        }

        private void GetKensaku(ClassKensaku.KensakuParam p)
        {
            if (RadComboCategory.SelectedValue != "")
            {
                p.CategoryName = RadComboCategory.SelectedValue;
            }
        }

        protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
        {
            string d = DropDownList9.SelectedValue;
            if (d == "1日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(1);
            }

            if (d == "2日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(2);
            }

            if (d == "3日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(3);
            }
            if (d == "4日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(4);
            }
            if (d == "5日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(5);
            }
            if (d == "1ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddMonths(1);
            }
            if (d == "2ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddMonths(2);
            }
            if (d == "3ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddMonths(3);
            }
            if (d == "4ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddMonths(4);
            }
            if (d == "5ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddMonths(5);
            }
            if (d == "6ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(6);
            }
            if (d == "1年")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddYears(1);
            }
            if (d == "99年")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddYears(98);
            }
        }


        //protected void Button3_Click(object sender, EventArgs e)
        //{
        //    TokuisakiSyousai.Visible = true;
        //    mInput2.Visible = false;
        //    CtrlSyousai.Visible = false;
        //    Button13.Visible = false;
        //    head.Visible = false;
        //    SubMenu.Visible = false;
        //    SubMenu2.Visible = true;
        //    ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
        //    KensakuParam(p);

        //    DataSet1.M_Tyokusosaki1DataTable dt = ClassKensaku.GetTyokusou(p, Global.GetConnection());
        //    for (int j = 0; j < dt.Count; j++)
        //    {
        //        DataSet1.M_Tyokusosaki1Row dr = dt.Rows[j] as DataSet1.M_Tyokusosaki1Row;

        //        TextBox27.Text = dr.TyokusousakiCode.ToString();
        //        if (!dr.IsTyokusousakiMei1Null())
        //        {
        //            TextBox28.Text = dr.TyokusousakiMei1;
        //        }

        //        if (!dr.IsYubinbangouNull())
        //        {
        //            TextBox35.Text = dr.Yubinbangou;
        //        }
        //        if (!dr.IsJusyo1Null() && !dr.IsJusyo2Null())
        //        {
        //            TextBox29.Text = dr.Jusyo1 + dr.Jusyo2;
        //        }
        //        if (!dr.IsTellNull())
        //        {
        //            TextBox30.Text = dr.Tell;
        //        }
        //        if (!dr.IsFaxNull())
        //        {
        //            TextBox31.Text = dr.Fax;
        //        }
        //        if (!dr.IsTyokusousakiTantouNull())
        //        {
        //            TextBox32.Text = dr.TyokusousakiTantou;
        //        }
        //    }
        //}

        private void GetKensakuParam(ClassKensaku.KensakuParam p)
        {
            if (RadComboBox1.Text.Trim() != "")
            {
                p.tokuisakimei = RadComboBox1.Text.Trim();
            }
        }

        private void KensakuParam(ClassKensaku.KensakuParam p)
        {
            if (RadComboBox2.Text.Trim() != "")
            {
                p.nouhinsaki = RadComboBox2.Text.Trim();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadComboBox3.Text = RadComboBox1.SelectedValue;
        }

        protected void RadComboBox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTokuisaki(sender, e);
            }
        }

        protected void RadComboBox2_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTyokusousaki(sender, e);
            }
        }

        protected void RadComboCategory_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetCategory(sender, e);
        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string[] val = e.Value.Split(',');
                Label3.Text = val[16];
                Session["Kakeritsu"] = val[16];
                Shimebi.Text = val[17];
                Session["Zeikubun"] = val[19];
                LblTantoStaffCode.Text = val[5];
                string tanto = LblTantoStaffCode.Text;
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTanto1(LblTantoStaffCode.Text, Global.GetConnection());
                RadComboBox5.Text = dt[0].UserName;
                Tantou.Text = dt[0].UserName;
                for (int i = 0; i < dt.Count; i++)
                {
                    RadComboBox4.Items.Add(new RadComboBoxItem(dt[0].Busyo));
                }
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    Label zeiku = (Label)CtlMitsuSyosai.FindControl("zeiku");
                    Label Kakeri = (Label)CtlMitsuSyosai.FindControl("Kakeri");
                    zeiku.Text = RadZeiKubun.Text;
                    Kakeri.Text = Label3.Text;
                }
                if ((DataMitumori.T_MitumoriDataTable)Session["MeisaiJutyuData"] != null)
                {
                    DataMitumori.T_MitumoriDataTable dtM = (DataMitumori.T_MitumoriDataTable)Session["MeisaiJutyuData"];
                    dtM.AsEnumerable().Select(drN => drN.Kakeritsu = (string)Session["Kakeritsu"]).ToList();
                    //掛率や税区分が変化する場合があるので
                    //この段階でも計算を行う
                    //MeisaiDataに直接変更をかける
                    int iHyoujunTanka = 0;
                    int iKingaku = 0;
                    double iTanka = 0;
                    int iUrikageKingaku = 0;
                    int iShiireTanka = 0;
                    double iShiireKingaku = 0;
                    int iSuryo = 0;
                    for (int i = 0; i < dtM.Count; i++)
                    {
                        DataMitumori.T_MitumoriRow dr = dtM[i];
                        if (Session["Zeikubun"].Equals("税込"))
                        {
                            if (!dr.IsHyojunKakakuNull())
                            {
                                iHyoujunTanka = int.Parse(dr.HyojunKakaku);
                                iSuryo = dr.JutyuSuryou;
                                iKingaku = iHyoujunTanka * iSuryo;
                                iTanka = iHyoujunTanka * int.Parse(dr.Kakeritsu) * 1.1 / 100;
                                iUrikageKingaku = int.Parse(iTanka.ToString()) * iSuryo;
                                iShiireTanka = dr.ShiireTanka;
                                iShiireKingaku = (iShiireTanka * iSuryo * 11) / 10;

                                dr.JutyuTanka = int.Parse(iTanka.ToString());//単価
                                dr.JutyuGokei = iUrikageKingaku;
                                dr.ShiireKingaku = int.Parse(iShiireKingaku.ToString());
                            }
                        }
                        else
                        {
                            if (!dr.IsHyojunKakakuNull())
                            {
                                iHyoujunTanka = int.Parse(dr.HyojunKakaku);
                                iSuryo = dr.JutyuSuryou;
                                iKingaku = iHyoujunTanka * iSuryo;
                                iTanka = iHyoujunTanka * int.Parse(dr.Kakeritsu) / 100;
                                iUrikageKingaku = int.Parse(iTanka.ToString()) * iSuryo;
                                iShiireTanka = dr.ShiireTanka;
                                iShiireKingaku = iShiireTanka * iSuryo;

                                dr.JutyuTanka = int.Parse(iTanka.ToString());//単価
                                dr.JutyuGokei = iUrikageKingaku;
                                dr.ShiireKingaku = int.Parse(iShiireKingaku.ToString());
                            }
                        }
                    }
                    Session["MeisaiJutyuData"] = dtM;
                    CtrlSyousai.DataSource = dtM;
                    CtrlSyousai.DataBind();
                }

                FacilityRad.Focus();
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
            }
        }

        private void KensakuPara(ClassKensaku.KensakuParam p)
        {

            if (RadComboBox1.SelectedValue.Trim() != "")
            {
                p.tokuiTObumonTOtanto = RadComboBox1.SelectedValue.Trim();
            }
        }

        protected void RadComboBox3_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTokuisaki(sender, e);
            }
        }

        protected void ProductName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string cate = RadComboCategory.Text;
            if (e.Text != "")
            {
                ListSet.SetProductName(sender, e, cate);
            }
        }

        protected void RadComboCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CategoryCode.Value = RadComboCategory.SelectedValue;
            Session["CategoryCode"] = RadComboCategory.SelectedValue;
            string a = CategoryCode.Value;
            strCategoryCode = RadComboCategory.SelectedValue;
            category(a);
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                HtmlInputHidden cate = CtlMitsuSyosai.FindControl("HidCategoryCode") as HtmlInputHidden;
                Label LblCateCode = CtlMitsuSyosai.FindControl("LblCateCode") as Label;
                Label LblCategoryName = CtlMitsuSyosai.FindControl("LblCategoryName") as Label;
                CtlMitsuSyosai.CategoryChange(RadComboCategory.SelectedValue);

                LblCateCode.Text = RadComboCategory.SelectedValue;
                LblCategoryName.Text = RadComboCategory.SelectedItem.Text;

                //CtlMitsuSyosai.Test4(strCategoryCode);
            }
            RadComboBox1.Focus();
        }

        private void category(string a)
        {
            if (a != "")
            {
                switch (int.Parse(a))
                {
                    case 101:
                    case 102:
                    case 103:
                    case 199:
                        RadDatePicker3.Visible = DropDownList9.Visible = CheckBox4.Visible = RadDatePicker4.Visible = false;
                        break;
                    default:
                        RadDatePicker3.Visible = DropDownList9.Visible = CheckBox4.Visible = RadDatePicker4.Visible = true;
                        break;
                }
            }
            else
            {
                RadDatePicker3.Visible = DropDownList9.Visible = CheckBox4.Visible = RadDatePicker4.Visible = true;
            }
        }
        //ItemDataBound
        public void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    int page = CtrlSyousai.CurrentPageIndex * 10;
                    int no = CtrlSyousai.Items.Count + 1 + page;
                    DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                    DataJutyu.T_JutyuRow df = (e.Item.DataItem as DataRowView).Row as DataJutyu.T_JutyuRow;
                    CtrlMitsuSyousai Ctl = e.Item.FindControl("Syosai") as CtrlMitsuSyousai;
                    RadComboBox Zaseki = (RadComboBox)Ctl.FindControl("Zasu");
                    RadComboBox SerchProduct = (RadComboBox)Ctl.FindControl("SerchProduct");
                    TextBox HyoujyunTanka = (TextBox)Ctl.FindControl("HyoujyunTanka");
                    TextBox Kingaku = (TextBox)Ctl.FindControl("Kingaku");
                    TextBox Tanka = (TextBox)Ctl.FindControl("Tanka");
                    TextBox Uriage = (TextBox)Ctl.FindControl("Uriage");
                    RadComboBox Hachu = (RadComboBox)Ctl.FindControl("Hachu");
                    TextBox ShiireTanka = (TextBox)Ctl.FindControl("ShiireTanka");
                    TextBox ShiireKingaku = (TextBox)Ctl.FindControl("ShiireKingaku");
                    TextBox Suryo = (TextBox)Ctl.FindControl("Suryo");
                    Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                    TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
                    TextBox TbxHyoujun = (TextBox)Ctl.FindControl("TbxHyoujun");
                    Label LblShiireCode = (Label)Ctl.FindControl("LblShiireCode");
                    RadComboBox RcbHanni = (RadComboBox)Ctl.FindControl("RcbHanni");
                    Panel SyouhinSyousai = (Panel)Ctl.FindControl("SyouhinSyousai");
                    Button BtnSyouhinSyousai = (Button)Ctl.FindControl("BtnProductMeisai");
                    Button BtnClose = (Button)Ctl.FindControl("BtnClose");
                    Button BtnFacilityMeisai = (Button)Ctl.FindControl("BtnFacilityMeisai");
                    Panel SisetuSyousai = (Panel)Ctl.FindControl("SisetuSyousai");
                    Button ButtonClose = (Button)Ctl.FindControl("ButtonClose");

                    Label RowNo = e.Item.FindControl("RowNo") as Label;
                    RowNo.Text = no.ToString();

                    //読み込んだ単価テキストボックスにJavaScriptを実行させる魔法を付与
                    string strClientID = Tanka.ClientID + "-" + Suryo.ClientID + "-" + Uriage.ClientID + "-" + HyoujyunTanka.ClientID + "-" + ShiireTanka.ClientID + "-" + Kingaku.ClientID + "-" + ShiireKingaku.ClientID + "-" + TbxHyoujun.ClientID;
                    Tanka.Attributes["oninput"] = string.Format("Keisan('{0}');", strClientID);
                    Suryo.Attributes["oninput"] = string.Format("Keisan('{0}');", strClientID);
                    HyoujyunTanka.Attributes["oninput"] = string.Format("Keisan('{0}');", strClientID);
                    ShiireTanka.Attributes["oninput"] = string.Format("Keisan('{0}');", strClientID);
                    BtnSyouhinSyousai.OnClientClick = string.Format("Meisai('{0}'); return false;", SyouhinSyousai.ClientID);
                    BtnClose.OnClientClick = string.Format("Close('{0}'); return false;", SyouhinSyousai.ClientID);
                    BtnFacilityMeisai.OnClientClick = string.Format("Meisai('{0}'); return false;", SisetuSyousai.ClientID);
                    ButtonClose.OnClientClick = string.Format("Close('{0}'); return false;", SisetuSyousai.ClientID);

                    if (CheckBox1.Checked == true)
                    {
                        KariTokui.Visible = true;
                        kariSekyu.Visible = true;
                        KariNouhin.Visible = true;
                        RadComboBox1.Visible = false;
                        RadComboBox2.Visible = false;
                        RadComboBox3.Visible = false;
                    }
                    if (df != null)
                    {
                        Ctl.ItemSet3(df);
                    }
                    else
                    {
                        Ctl.ItemSet(dr);
                        string pn = CtrlMitsuSyousai.ProductName;
                        if (string.IsNullOrEmpty(pn))
                        {
                            SerchProduct.Focus();
                        }
                    }//見積データがないとき(明細追加など)

                    if (RadComboCategory.SelectedValue != "")
                    {
                        string a = RadComboCategory.SelectedValue;
                        Ctl.Test4(a);
                        CategoryCode.Value = RadComboCategory.SelectedValue;
                    }
                    kari();
                }
                catch (Exception ex)
                {
                    Err.Text = ex.Message;
                }
            }
        }

        //ItemDataBound
        private void AllFukusu()
        {
            Fukusu();
        }

        //登録ボタン
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                AllFukusu();
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録

                DataJutyu.T_JutyuHeaderDataTable dd = ClassJutyu.GetJutyuH(Global.GetConnection());
                DataJutyu.T_JutyuHeaderDataTable dp = new DataJutyu.T_JutyuHeaderDataTable();
                DataJutyu.T_JutyuHeaderRow dl = dp.NewT_JutyuHeaderRow();

                if (Uriagekeijyou.Text != "")
                {
                    string r1 = Uriagekeijyou.Text.Replace(",", "");
                    dl.GokeiKingaku = int.Parse(r1);
                }
                if (Shiirekei.Text != "")
                {
                    string r2 = Shiirekei.Text.Replace(",", "");
                    dl.ShiireKingaku = int.Parse(r2);
                }
                if (Zei.Text != "")
                {
                    string r3 = Zei.Text.Replace(",", "");
                    dl.SyohiZeiGokei = int.Parse(r3);
                }
                dl.CategoryName = RadComboCategory.Text;
                dl.CateGory = int.Parse(CategoryCode.Value);
                if (TextBox6.Text != "")
                {
                    string r4 = TextBox6.Text.Replace(",", "");
                    dl.ArariGokeigaku = int.Parse(r4);
                }
                if (TextBox10.Text != "")
                {
                    dl.SouSuryou = int.Parse(TextBox10.Text);
                }
                if (UriageGokei.Text != "0")
                {
                    string sou = UriageGokei.Text;
                    string r9 = sou.Replace(",", "");
                    dl.SoukeiGaku = int.Parse(r9);
                }
                else
                {
                    string r5 = Uriagekeijyou.Text.Replace(",", "");
                    dl.SoukeiGaku = int.Parse(r5);
                }

                if (CheckBox1.Checked == true)
                {
                    dl.TokuisakiName = KariTokui.Text;
                }
                else
                {
                    if (RadComboBox1.Text != "")
                    {
                        dl.TokuisakiName = RadComboBox1.Text;
                        string[] toku = TokuisakiCode.Value.Split('/');
                        DataSet1.M_Tokuisaki2DataTable du = Class1.GetTokuisaki2(toku[0], toku[1], Global.GetConnection());
                        for (int i = 0; i < du.Count; i++)
                        {
                            dl.TokuisakiCode = du[i].CustomerCode + "/" + du[i].TokuisakiCode.ToString();
                        }
                    }
                }
                if (RadComboBox3.Text != "")
                {
                    dl.SeikyusakiName = RadComboBox3.Text;
                }
                if (RadComboBox2.Text != "")
                {
                    dl.TyokusosakiName = RadComboBox2.Text;
                }
                if (FacilityRad.Text != "")
                {
                    dl.FacilityName = FacilityRad.Text;
                }
                if (Tantou.Text != "")
                {
                    dl.TantoName = Tantou.Text;
                }
                if (RadComboBox4.Text != "")
                {
                    dl.Bumon = RadComboBox4.Text;
                }
                if (RadZeiKubun.Text != "")
                {
                    dl.Zeikubun = RadZeiKubun.Text;
                }
                //if (CheckBox1.Checked == true)
                //{
                //    dl.KariFLG = CheckBox1.Checked.ToString();
                //}

                if (Label3.Text != "")
                {
                    dl.Kakeritsu = Label3.Text;
                }

                if (TbxKake.Text != "")
                {
                    dl.Kakeritsu = TbxKake.Text;
                }

                if (Shimebi.Text != "")
                {

                }
                string vsNo = "";
                if (JutyuNo.Text != "")
                {
                    string mNo = SessionManager.HACCYU_NO;
                    string[] strAry = mNo.Split('_');
                    string MiNo = strAry[0];
                    DataJutyu.T_JutyuHeaderDataTable dx = ClassJutyu.GetJutyuHeader1(MiNo, Global.GetConnection());

                    if (dx.Count >= 1)
                    {
                        string[] jno = mNo.Split('_');
                        dl.JutyuNo = int.Parse(jno[0]);
                        //dl.HatyuFlg = "false";
                        dl.Relay = "受注";
                        dl.CreateDate = DateTime.Now;
                        dp.AddT_JutyuHeaderRow(dl);

                        //DataMitumori.T_MitumoriHeaderDataTable dv = ClassMitumori.GETMitsumorihead(mNo, Global.GetConnection());
                        ClassJutyu.UpDateJutyuHeader(jno[0], dp, Global.GetConnection());
                        ClassJutyu.DelJutyu2(jno[0], Global.GetConnection());
                        vsNo = jno[0];
                    }
                }
                else
                {
                    SessionManager.KI();
                    int ki = int.Parse(SessionManager.KII);
                    DataJutyu.T_JutyuHeaderRow dr = ClassJutyu.GetMaxNo(ki, Global.GetConnection());
                    if (dr != null)
                    {
                        vsNo = (dr.JutyuNo + 1).ToString();
                    }
                    else
                    {
                        vsNo = "2" + (ki * 10 + 1).ToString();
                    }
                    dl.JutyuNo = int.Parse(vsNo);
                    dp.AddT_JutyuHeaderRow(dl);
                    ClassJutyu.InsertJutyuHeader(dp, Global.GetConnection());
                }

                DataJutyu.T_JutyuDataTable dtN = null;
                if ((DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"] == null)
                {
                    dtN = new DataJutyu.T_JutyuDataTable();
                }
                else
                {
                    dtN = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
                }


                int NowPage = CtrlSyousai.CurrentPageIndex;
                int kijunRow = NowPage * 10;
                if (dtN.Count > 0)
                {
                    for (int d = kijunRow; kijunRow < dtN.Count;)
                    {
                        dtN.RemoveT_JutyuRow(dtN[kijunRow]);
                    }
                }
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = dtN.NewT_JutyuRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    dr = CtlMitsuSyosai.ItemGet3(dr);

                    dr.TokuisakiCode = TbxCustomer.Text + "/" + TbxTokuisakiCode.Text;
                    dr.TokuisakiMei = RcbTokuisakiNameSyousai.Text;
                    dr.SeikyusakiMei = RcbSeikyusaki.Text;
                    dr.CateGory = int.Parse(RadComboCategory.SelectedValue);
                    dr.CategoryName = RadComboCategory.Text;
                    dr.TourokuName = Tantou.Text;
                    dr.TanTouName = Tantou.Text;
                    dr.Busyo = RadComboBox4.Text;
                    if (JutyuNo.Text != "")
                    {
                        dr.JutyuNo = JutyuNo.Text;
                        //dr.UriageFlg = false;
                        dtN.AddT_JutyuRow(dr);
                    }
                    else
                    {
                        dr.JutyuNo = vsNo;
                        //dr.UriageFlg = false;
                        dtN.AddT_JutyuRow(dr);
                    }
                }
                ClassJutyu.InsertJutyu2(dtN, Global.GetConnection());
                JutyuNo.Text = dl.JutyuNo.ToString();
            }
            catch (Exception ex)
            {
                ClassMail.ErrorMail("maeda@m2m-asp.com", "エラーメール | 受注登録", ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source);
                Err.Text = "データを登録することができませんでした。";
            }
            if (Err.Text == "")
            {
                End.Text = "データを登録しました。";
            }
        }
        //登録ボタン
        protected void Button6_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Style["display"] = "none";
            mInput2.Visible = true;
            CtrlSyousai.Visible = true;
            //AddBtn.Visible = true;
            Button13.Visible = true;
            head.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
        }

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    TokuisakiSyousai.Visible = true;
        //    mInput2.Visible = false;
        //    CtrlSyousai.Visible = false;
        //    SubMenu.Visible = false;
        //    SubMenu2.Visible = true;
        //}
        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Master/MsterTokuisaki.aspx");
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            Response.Redirect("Master/MasterTyokuso.aspx");

        }

        //追加ボタン
        protected void AddBtn_Click(object sender, EventArgs e)
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            //仮のデータテーブル設置
            try
            {
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                //グリッドの中で表示しているユーザーコントロールの数だけループ
                {
                    DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                    //dtのrow

                    //定義集
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    //TextBox MakerHinban = (TextBox)CtlMitsuSyosai.FindControl("MakerHinban");
                    //RadComboBox ProductName = (RadComboBox)CtlMitsuSyosai.FindControl("ProductName");
                    //RadComboBox Hanni = (RadComboBox)CtlMitsuSyosai.FindControl("Hanni");
                    RadComboBox Zaseki = (RadComboBox)CtlMitsuSyosai.FindControl("Zasu");
                    Label Media = (Label)CtlMitsuSyosai.FindControl("Baitai");
                    TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
                    TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
                    TextBox Suryo = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
                    RadComboBox ShiyouShisetsu = (RadComboBox)CtlMitsuSyosai.FindControl("ShiyouShisetsu");
                    RadDatePicker StartDate = (RadDatePicker)CtlMitsuSyosai.FindControl("StartDate");
                    RadDatePicker EndDate = (RadDatePicker)CtlMitsuSyosai.FindControl("EndDate");
                    TextBox Tanka = (TextBox)CtlMitsuSyosai.FindControl("Tanka");
                    TextBox Uriage = (TextBox)CtlMitsuSyosai.FindControl("Uriage");
                    RadComboBox Hachu = (RadComboBox)CtlMitsuSyosai.FindControl("Hachu");
                    TextBox ShiireTanka = (TextBox)CtlMitsuSyosai.FindControl("ShiireTanka");
                    TextBox ShiireKingaku = (TextBox)CtlMitsuSyosai.FindControl("ShiireKingaku");
                    HtmlInputHidden ShisetsuCode = (HtmlInputHidden)CtlMitsuSyosai.FindControl("Shisetsu");
                    HtmlInputHidden procd = (HtmlInputHidden)CtlMitsuSyosai.FindControl("procd");
                    Label Facility = (Label)CtlMitsuSyosai.FindControl("Facility");
                    HtmlInputHidden FacilityCode = (HtmlInputHidden)CtlMitsuSyosai.FindControl("FacilityCode");
                    Label end = (Label)CtlMitsuSyosai.FindControl("Label12");
                    Label start = (Label)CtlMitsuSyosai.FindControl("Label11");
                    HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
                    HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
                    HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
                    HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
                    HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
                    HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
                    HtmlInputHidden zht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeiht");
                    HtmlInputHidden zst = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeist");
                    HtmlInputHidden zeikgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeikgk");
                    HtmlInputHidden zeisk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeisk");
                    Label WareHouse = (Label)CtlMitsuSyosai.FindControl("WareHouse");
                    TextBox KariFaci = (TextBox)CtlMitsuSyosai.FindControl("KariFaci");
                    Label kakeri = (Label)CtlMitsuSyosai.FindControl("Kakeri");
                    Label zeiku = (Label)CtlMitsuSyosai.FindControl("zeiku");
                    Label Lblproduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    Label LblHanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");
                    RadComboBox Tekiyo = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");


                    Fukusu();
                    string s = RadZeiKubun.Text;
                    if (Tekiyo.Text != "")
                    {
                        dr.JutyuTekiyo = Tekiyo.Text;
                    }
                    if (kakeri.Text != "")
                    {
                        dr.Kakeritsu = kakeri.Text;
                    }
                    if (zeiku.Text != "")
                    {
                        dr.Zeiku = RadZeiKubun.Text;
                    }

                    CtlMitsuSyosai.Zeikei(s);
                    //Fukudate();

                    //すでに入力していた文字たちをdrに入れる
                    if (StartDate.SelectedDate != null || start.Text != "")
                    {
                        if (StartDate.SelectedDate == null)
                        {
                            dr.SiyouKaishi = DateTime.Parse(start.Text);
                        }
                        if (start.Text == "")
                        {
                            dr.SiyouKaishi = StartDate.SelectedDate.Value;
                        }
                    }
                    if (EndDate.SelectedDate != null || end.Text != "")
                    {
                        if (EndDate.SelectedDate == null)
                        {
                            dr.SiyouOwari = DateTime.Parse(end.Text);
                        }
                        if (end.Text == "")
                        {
                            dr.SiyouOwari = EndDate.SelectedDate.Value;
                        }
                    }


                    if (SerchProduct.Text != "")
                    {
                        dr.SyouhinMei = SerchProduct.Text;
                    }
                    if (HyoujyunTanka.Text != "")
                    {
                        string hyoutan = HyoujyunTanka.Text;
                        string r1 = hyoutan.Replace(",", "");
                        dr.HyojunKakaku = r1;
                    }
                    if (LblHanni.Text != "")
                    {
                        dr.Range = LblHanni.Text;
                    }
                    if (ShiyouShisetsu.Text != "" || Facility.Text != "")
                    {
                        if (ShiyouShisetsu.Text == "")
                        {
                            dr.Basyo = Facility.Text;
                        }
                        else
                        {
                            if (CheckBox1.Checked == true)
                            {
                                dr.Basyo = KariFaci.Text;
                            }
                            else
                            {
                                dr.Basyo = ShiyouShisetsu.Text;
                            }
                        }
                    }
                    if (Zaseki.Text != "")
                    {
                        dr.Zasu = Zaseki.SelectedItem.Text;
                    }
                    if (Media.Text != "")
                    {
                        dr.Media = Media.Text;
                    }
                    if (Lblproduct.Text != "")
                    {
                        dr.MekarHinban = Lblproduct.Text;
                    }
                    if (Kingaku.Text != "")
                    {
                        string kg = Kingaku.Text;
                        string r1 = kg.Replace(",", "");
                        dr.Ryoukin = r1;
                    }
                    if (FacilityCode.Value != "")
                    {
                        dr.SisetuCode = int.Parse(FacilityCode.Value);
                    }
                    if (Tanka.Text != "")
                    {
                        string tkV = Tanka.Text;
                        string r2 = tkV.Replace(",", "");
                        dr.HattyuTanka = int.Parse(r2);
                    }
                    if (Uriage.Text != "")
                    {
                        string ur = Uriage.Text.Replace(",", "");
                        dr.Uriage = ur;
                    }

                    if (Suryo.Text != "")
                    { dr.HattyuSuryou = int.Parse(Suryo.Text); }
                    if (ShiireKingaku.Text != "")
                    {
                        string shiirekin = ShiireKingaku.Text;
                        string r1 = shiirekin.Replace(",", "");
                        dr.ShiireKingaku = r1;
                    }
                    if (ShiireTanka.Text != "")
                    {
                        string shiiretan = ShiireTanka.Text;
                        string r1 = shiiretan.Replace(",", "");
                        dr.ShiireTanka = r1;
                    }
                    if (Hachu.Text != "")
                    {
                        dr.HattyuSakiMei = Hachu.Text;
                    }
                    if (WareHouse.Text != "")
                    {
                        dr.WareHouse = WareHouse.Text;
                    }
                    string a = RadComboCategory.SelectedValue;
                    CtlMitsuSyosai.Test4(a);
                    //drをdtに追加
                    dt.AddT_RowRow(dr);
                }
                //dtを維持しながらAddCreateに移動
                AddCreate(dt);
            }
            catch (Exception ex)
            {
                Err.Text = ex.ToString();
            }
        }
        //追加ボタン
        private void AddCreate(DataMitumori.T_RowDataTable dt)
        {
            //空の列をdtに追加
            AddNewRow(dt);

            for (int i = 0; i < dt.Count; i++)
            {
                if (Label3.Text != "")
                {
                    dt[i].Kakeritsu = Label3.Text;
                }
                else
                {
                    dt[i].Kakeritsu = TbxKake.Text;
                }

                dt[i].Zeiku = RadZeiKubun.Text;
            }
            if (CheckBox4.Checked)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    if (RadDatePicker3.SelectedDate != null)
                    {
                        dt[i].SiyouKaishi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                    }
                    if (RadDatePicker4.SelectedDate != null)
                    {
                        dt[i].SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                    }
                }
            }
        }

        private void AddNewRow(DataMitumori.T_RowDataTable dt)
        {
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            dt.AddT_RowRow(dr);
        }

        protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (FacilityRad.SelectedValue != "")
            {
                if (CheckBox5.Checked)
                {
                    string[] facCode = FacilityRad.SelectedValue.Split('/');
                    DataMaster.M_Facility_NewRow dr = ClassMaster.GetFacilityRow(facCode, Global.GetConnection());
                    objFacility = dr.ItemArray;
                    if (dr != null)
                    {
                        for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                        {
                            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                            RadComboBox ShiyouShisetsu = (RadComboBox)CtlMitsuSyosai.FindControl("ShiyouShisetsu");
                            TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                            RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                            TextBox TbxFacilityName = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName");
                            TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                            TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                            TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                            TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                            TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                            TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                            TextBox TbxFacilityRowCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityRowCode");

                            TbxFacilityCode.Text = dr.FacilityNo.ToString();
                            ShiyouShisetsu.Text = dr.Abbreviation;
                            TbxFacilityName.Text = dr.FacilityName1;
                            TbxFacilityRowCode.Text = dr.Code;
                            if (!dr.IsFacilityName2Null())
                            {
                                TbxFacilityName2.Text = dr.FacilityName2;
                            }
                            if (!dr.IsAbbreviationNull())
                            {
                                TbxFaci.Text = dr.Abbreviation;
                            }
                            if (!dr.IsPostNoNull())
                            {
                                TbxYubin.Text = dr.PostNo;
                            }
                            if (!dr.IsAddress1Null())
                            {
                                TbxFaciAdress.Text = dr.Address1;
                            }
                            if (!dr.IsTellNull())
                            {
                                TbxTel.Text = dr.Tell;
                            }
                            if (!dr.IsFacilityResponsibleNull())
                            {
                                TbxFacilityResponsible.Text = dr.FacilityResponsible;
                            }
                            if (!dr.IsCityCodeNull())
                            {
                                DataMaster.M_CityRow dc = ClassMaster.GetCity(dr.CityCode.ToString(), Global.GetConnection());
                                if (dc != null)
                                {
                                    RcbCity.Text = dc.CityName;
                                    RcbCity.SelectedValue = dc.CityCode.ToString();
                                }
                            }
                        }
                        CtrlMitsuSyousai Ctl = CtrlSyousai.Items[0].FindControl("Syosai") as CtrlMitsuSyousai;
                        RadComboBox serchproduct = (RadComboBox)Ctl.FindControl("SerchProduct");
                        serchproduct.Focus();
                    }
                    else
                    {
                        Err.Text = "施設情報がありませんでした。";
                    }
                }
            }
            else
            {
                Err.Text = "使用施設を選択して下さい。";
            }
        }

        protected void Fukusu()
        {
            try
            {
                if (CheckBox5.Checked == true)
                {
                }
                else
                {
                    for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                    {
                        CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                        CtlMitsuSyosai.FukusuCheckedTrueFalse();
                    }
                }
            }
            catch (Exception ex)
            {
                Err.Text = ex.ToString();
            }
        }

        protected void FacilityRad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Fukusu();
            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[0].FindControl("Syosai") as CtrlMitsuSyousai;
            RadComboBox serchproduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
            serchproduct.Focus();
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            Fukudate();
        }

        private void Fukudate()
        {
            if (CheckBox4.Checked == true)
            {
                if (RadDatePicker3.SelectedDate != null)
                {
                    string StartDate = RadDatePicker3.SelectedDate.ToString();

                    if (RadDatePicker4.SelectedDate != null)
                    {
                        string EndDate = RadDatePicker4.SelectedDate.ToString();
                        for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                        {
                            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                            string s = CheckBox4.Checked.ToString();
                            CtlMitsuSyosai.FukudateTrue(StartDate, EndDate);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    CtlMitsuSyosai.FukudateFalse();
                    string s = CheckBox4.Checked.ToString();
                }
            }
        }

        //行削除、行追加、行複写---------------------------------------------------------------------------------------
        protected void CtrlSyousai_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //-------------------------------------------------------------------------------------------------
            //【明細追加、明細削除、明細複写】
            // 
            //・明細複写
            //　明細複写は追加ボタンを押下した行の下に空明細が追加されるイメージ
            //　（途中の行でも追加できる）
            //
            //・明細削除
            //    押下した行が削除される
            //
            //・明細複写
            //    押下した行のデータをコピーし、押下した下の行に挿入される
            //
            //-------------------------------------------------------------------------------------------------

            int a = e.Item.ItemIndex;
            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            //int sIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Del")//--------------明細削除-----------------------
            {
                DataJutyu.T_JutyuDataTable dtN = null;
                if ((DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"] == null)
                {
                    dtN = new DataJutyu.T_JutyuDataTable();
                }
                else
                {
                    dtN = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
                }
                int NowPage = CtrlSyousai.CurrentPageIndex;
                int kijunRow = NowPage * 10;
                if (dtN.Count > 0)
                {
                    for (int d = kijunRow; kijunRow < dtN.Count;)
                    {
                        dtN.RemoveT_JutyuRow(dtN[kijunRow]);
                    }
                    dtN.AsEnumerable().Where(dr => int.Parse(dr["RowNo"].ToString()) > kijunRow).Select(dr => dr["RowNo"] = int.Parse(dr["RowNo"].ToString()) + 99).ToList();
                    dtN.AsEnumerable().Where(dr => int.Parse(dr["RowNo"].ToString()) > kijunRow).Select(dr => dr["RowNo"] = int.Parse(dr["RowNo"].ToString()) - 100).ToList();
                }
                int row = 0;
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = dtN.NewT_JutyuRow();
                    try
                    {
                        if (a != i)
                        {
                            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                            dr = CtlMitsuSyosai.ItemGet3(dr);
                            dr.RowNo = e.Item.ItemIndex + (kijunRow);
                            dr.CategoryName = RadComboCategory.Text;
                            dr.CateGory = int.Parse(RadComboCategory.SelectedValue);
                            dr.JutyuFlg = false;
                            if (dr.IsSyouhinCodeNull())
                            {
                                dr.SyouhinCode = "";
                            }
                            dr.JutyuNo = "";
                            dr.RowNo = kijunRow + row;
                            dtN.AddT_JutyuRow(dr);
                            row++;
                        }
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }

                dtN.AsEnumerable().OrderBy(dr => dr.RowNo).ToList();
                Session["MeisaiJutyuData"] = dtN;
                int amari = (dtN.Count - 1) % 10;
                if (amari.Equals(0))
                {
                    CtrlSyousai.CurrentPageIndex = (dtN.Count - 1) / 10;
                }
                CtrlSyousai.DataSource = dtN;
                CtrlSyousai.DataBind();
            }
            if (e.CommandName.Equals("Add"))//--------------明細追加-----------------------
            {
                strCategoryCode = RadComboCategory.SelectedValue;
                strCategoryName = RadComboCategory.Text;

                DataJutyu.T_JutyuDataTable dtN = null;
                if ((DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"] == null)
                {
                    dtN = new DataJutyu.T_JutyuDataTable();
                }
                else
                {
                    dtN = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
                }
                int NowPage = CtrlSyousai.CurrentPageIndex;
                int kijunRow = NowPage * 10;
                if (dtN.Count > 0)
                {
                    for (int d = kijunRow; kijunRow < dtN.Count;)
                    {
                        dtN.RemoveT_JutyuRow(dtN[kijunRow]);
                    }
                }
                int row = 0;
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    row++;
                    DataJutyu.T_JutyuRow drN = dtN.NewT_JutyuRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    drN = CtlMitsuSyosai.ItemGet4(drN);
                    drN.ItemArray = drN.ItemArray;
                    drN.JutyuNo = "";
                    drN.JutyuFlg = false;
                    drN.RowNo = kijunRow + row;
                    drN.CategoryName = RadComboCategory.Text;
                    dtN.AddT_JutyuRow(drN);
                    if (a == i)
                    {
                        row++;
                        //新規の空の明細を追加
                        DataJutyu.T_JutyuRow drN2 = dtN.NewT_JutyuRow();
                        drN2.JutyuNo = "";
                        drN2.JutyuFlg = false;
                        drN2.RowNo = kijunRow + row;
                        drN2.CategoryName = RadComboCategory.Text;
                        drN2 = AddNewRow3(drN2);
                        dtN.AddT_JutyuRow(drN2);
                    }
                }

                int page = dtN.Count / (CtrlSyousai.PageSize + 1);
                if (page < 0)
                {
                    page = 0;
                }

                CtrlSyousai.CurrentPageIndex = page;
                Session["MeisaiJutyuData"] = dtN;
                CtrlSyousai.DataSource = dtN;
                CtrlSyousai.DataBind();
            }
            if (e.CommandName.Equals("Copy"))//--------------明細複写-----------------------
            {
                strCategoryCode = RadComboCategory.SelectedValue;
                strCategoryName = RadComboCategory.Text;
                DataJutyu.T_JutyuDataTable dtN = null;
                if ((DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"] == null)
                {
                    dtN = new DataJutyu.T_JutyuDataTable();
                }
                else
                {
                    dtN = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
                }
                int NowPage = CtrlSyousai.CurrentPageIndex;
                int kijunRow = NowPage * 10;
                if (dtN.Count > 0)
                {
                    for (int d = kijunRow; kijunRow < dtN.Count;)
                    {
                        dtN.RemoveT_JutyuRow(dtN[kijunRow]);
                    }
                }
                int row = 0;
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuRow drN = dtN.NewT_JutyuRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    DataJutyu.T_JutyuRow drG = CtlMitsuSyosai.ItemGet4(drN);
                    drN.ItemArray = drG.ItemArray;
                    drN.JutyuNo = "";
                    drN.JutyuFlg = false;
                    drN.CategoryName = RadComboCategory.Text;
                    drN.RowNo = kijunRow + row;
                    dtN.AddT_JutyuRow(drN);
                    row++;
                    if (a == i)
                    {
                        //新規の空の明細を追加
                        DataJutyu.T_JutyuRow drN2 = dtN.NewT_JutyuRow();
                        DataJutyu.T_JutyuRow drG2 = CtlMitsuSyosai.ItemGet4(drN2);
                        drN2.ItemArray = drG2.ItemArray;
                        drN2.JutyuNo = "";
                        drN2.JutyuFlg = false;
                        drN2.RowNo = kijunRow + row;
                        drN2.CategoryName = RadComboCategory.Text;
                        dtN.AddT_JutyuRow(drN2);
                        row++;
                    }
                }
                Session["MeisaiJutyuData"] = dtN;
                CtrlSyousai.DataSource = dtN;
                CtrlSyousai.DataBind();
            }

        }//

        private DataJutyu.T_JutyuRow AddNewRow3(DataJutyu.T_JutyuRow drN2)
        {
            drN2.Kakeritsu = strKakeritsu;
            drN2.Zeikubun = strZeikubun;
            drN2.CateGory = int.Parse(strCategoryCode);
            drN2.CategoryName = strCategoryName;
            if (CheckBox4.Checked)
            {
                if (!string.IsNullOrEmpty(strStartDate))
                {
                    drN2.SiyouKaishi = DateTime.Parse(strStartDate);
                }
                if (!string.IsNullOrEmpty(strEndDate))
                {
                    drN2.SiyouOwari = DateTime.Parse(strEndDate);
                }
            }
            if (objFacility.Length > 0)
            {
                drN2.SisetuCode = int.Parse(objFacility[0].ToString());
                drN2.SisetuRowCode = objFacility[1].ToString();
                drN2.SisetuMei = objFacility[2].ToString();
                if (!string.IsNullOrEmpty(objFacility[3].ToString()))
                {
                    drN2.SisetsuMei2 = objFacility[3].ToString();
                }
                drN2.SisetsuAbbreviration = objFacility[4].ToString();
                drN2.SisetuTanto = objFacility[5].ToString();
                drN2.SisetuPost = objFacility[6].ToString();
                drN2.SisetuJusyo1 = objFacility[7].ToString();
                if (!string.IsNullOrEmpty(objFacility[8].ToString()))
                {
                    drN2.SisetuJusyo2 += objFacility[8].ToString();
                }
                drN2.SisetsuTell = objFacility[9].ToString();
                drN2.SisetuCityCode = objFacility[10].ToString();
            }
            return drN2;
        }

        //行削除、行追加、行複写---------------------------------------------------------------------------------------

        private DataMitumori.T_RowRow AddNewRow2(DataMitumori.T_RowRow dr)
        {
            dr.Kakeritsu = strKakeritsu;
            dr.Zeiku = strZeikubun;
            dr.CategoryCode = strCategoryCode;
            dr.CategoryName = strCategoryName;
            if (CheckBox4.Checked)
            {
                if (!string.IsNullOrEmpty(strStartDate))
                {
                    dr.SiyouKaishi = DateTime.Parse(strStartDate);
                }
                if (!string.IsNullOrEmpty(strEndDate))
                {
                    dr.SiyouOwari = DateTime.Parse(strEndDate);
                }
            }
            if (objFacility.Length > 0)
            {
                dr.SisetuCode = int.Parse(objFacility[0].ToString());
                dr.SisetsuRowCode = objFacility[1].ToString();
                dr.SisetsuMei1 = objFacility[2].ToString();
                if (!string.IsNullOrEmpty(objFacility[3].ToString()))
                {
                    dr.SisetsuMei2 = objFacility[3].ToString();
                }
                dr.SisetsuAbbreviration = objFacility[4].ToString();
                dr.SisetsuTanto = objFacility[5].ToString();
                dr.SisetsuPost = objFacility[6].ToString();
                dr.SisetsuJusyo = objFacility[7].ToString();
                if (!string.IsNullOrEmpty(objFacility[8].ToString()))
                {
                    dr.SisetsuJusyo += objFacility[8].ToString();
                }
                dr.SisetsuTell = objFacility[9].ToString();
                dr.SisetsuCityCode = objFacility[10].ToString();
            }
            return dr;
        }

        //削除-----------------------------------------------------------------------------------------------
        private void DelCreate(DataMitumori.T_RowDataTable dt)
        {
            CtrlSyousai.DataSource = dt;
            //データバインドに飛ぶ
            CtrlSyousai.DataBind();
            //Keisan();
        }
        protected void FacilityRad_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetFacility(sender, e);
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mitumori/MitumoriItiran.aspx");
        }

        protected void Keisan_Click(object sender, EventArgs e)
        {
            //Keisan2();
            //Register.Focus();
        }


        protected void TbxKake_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                Label kakeri = (Label)CtlMitsuSyosai.FindControl("Kakeri");

                kakeri.Text = TbxKake.Text;
            }
        }

        protected void RadZeiKubun_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                Label zeiku = (Label)CtlMitsuSyosai.FindControl("zeiku");

                zeiku.Text = RadZeiKubun.Text;
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            JutyuNo.Text = "";
        }
        //Delete
        protected void DelBtn_Click(object sender, EventArgs e)
        {
            string mNo = JutyuNo.Text;
            ClassMitumori.DelMitumori3(mNo, Global.GetConnection());
            ClassMitumori.DelMitumoriHeader(mNo, Global.GetConnection());
            End.Text = "見積No." + mNo + "を削除しました。";
        }
        //Delete
        //発注ボタン
        protected void HatyuBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AllFukusu();

                DataMitumori.T_RowDataTable df = new DataMitumori.T_RowDataTable();

                //TokuisakiCode.Value = RadComboBox1.SelectedValue;
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録

                DataSet1.T_OrderedDataTable dd = ClassOrdered.GetOrdered(Global.GetConnection());
                DataJutyu.T_JutyuHeaderDataTable dw = new DataJutyu.T_JutyuHeaderDataTable();
                DataJutyu.T_JutyuHeaderRow dh = dw.NewT_JutyuHeaderRow();
                DataSet1.T_OrderedHeaderDataTable dp = new DataSet1.T_OrderedHeaderDataTable();
                DataSet1.T_OrderedHeaderRow dl = dp.NewT_OrderedHeaderRow();

                //if (Uriagekeijyou.Text != "")
                //{
                //    string r1 = Uriagekeijyou.Text.Replace(",", "");
                //    dl.SoukeiGaku = int.Parse(r1);
                //    dh.GokeiKingaku = int.Parse(r1);
                //    dh.SoukeiGaku = int.Parse(r1);
                //}
                //if (Shiirekei.Text != "")
                //{
                //    string r2 = Shiirekei.Text.Replace(",", "");
                //    dl.ShiireKingaku = int.Parse(r2);
                //    dh.ShiireKingaku = int.Parse(r2);
                //}
                //if (Zei.Text != "")
                //{
                //    string r3 = Zei.Text.Replace(",", "");
                //    dl.Tax = int.Parse(r3);
                //    dh.SyohiZeiGokei = int.Parse(r3);
                //}
                //dl.CategoryName = RadComboCategory.Text;
                //dl.Category = int.Parse(CategoryCode.Value);
                //dh.CategoryName = RadComboCategory.Text;
                //dh.CateGory = int.Parse(CategoryCode.Value);

                //if (TextBox6.Text != "")
                //{
                //    string r4 = TextBox6.Text.Replace(",", "");
                //    dl.ArariGokeigaku = r4;
                //    dh.ArariGokeigaku = int.Parse(r4);
                //}

                //if (TextBox2.Text != "")
                //{
                //    dh.Bikou = TextBox2.Text;
                //}
                //if (UriageGokei.Text != "0")
                //{
                //    string sou = UriageGokei.Text;
                //    string r9 = sou.Replace(",", "");
                //    dl.SoukeiGaku = int.Parse(r9);
                //    dh.SoukeiGaku = int.Parse(r9);
                //}
                //else
                //{
                //    string r5 = Uriagekeijyou.Text.Replace(",", "");
                //    dl.SoukeiGaku = int.Parse(r5);
                //    dh.GokeiKingaku = int.Parse(r5);
                //}

                //if (CheckBox1.Checked == true)
                //{
                //    dl.TokuisakiName = KariTokui.Text;
                //    dh.TokuisakiName = KariTokui.Text;
                //}
                //else
                //{
                //    if (RadComboBox1.Text != "")
                //    {
                //        dl.TokuisakiName = RcbTokuisakiNameSyousai.Text;
                //        dh.TokuisakiName = RcbTokuisakiNameSyousai.Text;

                //        DataSet1.M_Tokuisaki2DataTable du = Class1.GetTokuisaki2(TbxCustomer.Text, TbxTokuisakiCode.Text, Global.GetConnection());
                //        for (int j = 0; j < du.Count; j++)
                //        {
                //            dl.TokuisakiCode = du[j].CustomerCode + "/" + du[j].TokuisakiCode;
                //            dh.TokuisakiCode = du[j].CustomerCode + "/" + du[j].TokuisakiCode;
                //        }
                //    }
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiRyakusyo.Text))
                //{
                //    dh.TokuisakiRyakusyo = TbxTokuisakiRyakusyo.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiFurigana.Text))
                //{
                //    dh.TokuisakiFurigana = TbxTokuisakiFurigana.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiStaff.Text))
                //{
                //    dh.TokuisakiStaff = TbxTokuisakiStaff.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiPostNo.Text))
                //{
                //    dh.TokuisakiPostNo = TbxTokuisakiPostNo.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiAddress.Text))
                //{
                //    dh.TokuisakiAddress = TbxTokuisakiAddress.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiAddress1.Text))
                //{
                //    dh.TokuisakiAddress2 = TbxTokuisakiAddress1.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiTEL.Text))
                //{
                //    dh.TokuisakiTEL = TbxTokuisakiTEL.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiFax.Text))
                //{
                //    dh.TokuisakiFAX = TbxTokuisakiFax.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxTokuisakiDepartment.Text))
                //{
                //    dh.TokuisakiDepartment = TbxTokuisakiDepartment.Text;
                //}
                //if (!string.IsNullOrEmpty(RcbTax.Text))
                //{
                //    dh.Zeikubun = RcbTax.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxKakeritsu.Text))
                //{
                //    dh.Kakeritsu = TbxKakeritsu.Text;
                //}
                //if (!string.IsNullOrEmpty(RcbShimebi.Text))
                //{
                //    dh.Shimebi = RcbShimebi.Text;
                //}
                //if (!string.IsNullOrEmpty(RadComboBox5.SelectedValue))
                //{
                //    dh.StaffNo = RadComboBox5.SelectedValue;
                //}
                //if (!string.IsNullOrEmpty(RcbSeikyusaki.Text))
                //{
                //    dh.SeikyusakiName = RcbSeikyusaki.Text;
                //}
                //if (!string.IsNullOrEmpty(RcbNouhinsakiMei.Text))
                //{
                //    dh.TyokusosakiName = RcbNouhinsakiMei.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
                //{
                //    dh.FacilityCode = TbxFacilityCode.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
                //{
                //    dh.FacilityRowCode = TbxFacilityRowCode.Text;
                //}
                //if (!string.IsNullOrEmpty(RcbFacility.Text))
                //{
                //    dh.FacilityName = RcbFacility.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
                //{
                //    dh.FacilityName2 = TbxFacilityName2.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFaci.Text))
                //{
                //    dh.FacilityAbbreviation = TbxFaci.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
                //{
                //    dh.FacilityResponsible = TbxFacilityResponsible.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxYubin.Text))
                //{
                //    dh.FacilityPostNo = TbxYubin.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFaciAdress1.Text))
                //{
                //    dh.FacilityAddress1 = TbxFaciAdress1.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxFaciAdress2.Text))
                //{
                //    dh.FacilityAddress2 = TbxFaciAdress2.Text;
                //}
                //if (!string.IsNullOrEmpty(RcbCity.SelectedValue))
                //{
                //    dh.FacilityCityCode = RcbCity.SelectedValue;
                //}
                //if (!string.IsNullOrEmpty(TbxTel.Text))
                //{
                //    dh.FacilityTell = TbxTel.Text;
                //}
                //if (!string.IsNullOrEmpty(TbxKeisyo.Text))
                //{
                //    dh.FacilityState = TbxKeisyo.Text;
                //}

                //if (RadComboBox3.Text != "")
                //{
                //    dl.SeikyusakiName = RadComboBox3.Text;
                //    dh.SeikyusakiName = RadComboBox3.Text;
                //}
                //if (RadComboBox2.Text != "")
                //{
                //    dl.TyokusousakiMei = RadComboBox2.Text;
                //    dh.TyokusosakiName = RadComboBox2.Text;
                //}
                //if (FacilityRad.Text != "")
                //{
                //    dl.FacilityName = FacilityRad.Text;
                //    dh.FacilityName = FacilityRad.Text;
                //}
                //if (Tantou.Text != "")
                //{
                //    dl.StaffName = Tantou.Text;
                //    dh.TantoName = Tantou.Text;
                //}
                //dl.Relay = "発注済";
                //if (RadComboBox4.Text != "")
                //{
                //    dl.Department = RadComboBox4.Text;
                //    dh.Bumon = RadComboBox4.Text;
                //}
                //if (RadZeiKubun.Text != "")
                //{
                //    dl.Zeikubun = RadZeiKubun.Text;
                //    dh.Zeikubun = RadZeiKubun.Text;

                //}
                ////if (CheckBox1.Checked == true)
                ////{
                ////    dl.KariFLG = CheckBox1.Checked.ToString();
                ////    dh.KariFLG = CheckBox1.Checked.ToString();
                ////}

                //dl.CreateDate = RadDatePicker2.SelectedDate.Value;
                //dh.CreateDate = DateTime.Now;
                //dh.SouSuryou = int.Parse(TextBox10.Text);
                //string mno = SessionManager.HACCYU_NO;
                //SessionManager.KI();
                //int ki = int.Parse(SessionManager.KII);
                //string OrderedNo = "";
                ////DataMitumori.T_MitumoriHeaderDataTable db = ClassMitumori.GetMitumoriHeader(mno, Global.GetConnection());
                //DataSet1.T_OrderedHeaderRow db = ClassOrdered.GetMaxOrdered(ki, Global.GetConnection());
                //if (db != null)
                //{
                //    OrderedNo = (db.OrderedNo + 1).ToString();
                //}
                //else
                //{
                //    OrderedNo = "3" + (ki * 100000 + 1).ToString();
                //}

                ////string arrr = OrderedNo.ToString();
                //////受注データに挿入
                ////string arr = mno.ToString().Substring(1, 7);
                ////string sub = arrr.Substring(1, 7);
                ////int su = int.Parse(sub);
                ////int max = su + 1;

                //////string no = "3" + arr;
                ////string no = "3" + max;
                ////dl.OrderedNo = int.Parse(no);
                ////string[] strAry = mno.Split('_');
                //dh.JutyuNo = int.Parse(JutyuNo.Text);
                ////ClassOrdered.InsertOrderedHeader(dp, Global.GetConnection());
                //dh.Relay = "発注済";
                ////dh.HatyuFlg = "true";
                //dh.Shimebi = Shimebi.Text;
                //dw.AddT_JutyuHeaderRow(dh);

                ////★★★★★★★★★★★★受注データの更新★★★★★★★★★★★★★★★★★★★★★
                //ClassJutyu.UpDateJutyuHeader(JutyuNo.Text, dw, Global.GetConnection());

                DataSet1.T_OrderedDataTable dg = new DataSet1.T_OrderedDataTable();
                //.T_JutyuDataTable dt = ClassJutyu.GetJutyu(Global.GetConnection());
                string cate = "";
                string shiire = "";
                int ki = int.Parse(SessionManager.KII);

                //if ((DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"] == null)
                //{
                //    dtN = new DataJutyu.T_JutyuDataTable();
                //}
                //else
                //{
                //    dtN = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
                //}
                //int NowPage = CtrlSyousai.CurrentPageIndex;
                //int kijunRow = NowPage * 10;
                //if (dtN.Count > 0)
                //{
                //    for (int d = kijunRow; d < kijunRow + CtrlSyousai.Items.Count; d++)
                //    {
                //        dtN.RemoveT_JutyuRow(dtN[kijunRow]);
                //    }
                //}
                DataJutyu.T_JutyuDataTable dtJutyu = new DataJutyu.T_JutyuDataTable();
                //for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                //{
                //    dtJutyu = dtN;
                //    DataRow ddr = dtJutyu.NewRow() as DataJutyu.T_JutyuRow;
                //    DataJutyu.T_JutyuDataTable dtNN = new DataJutyu.T_JutyuDataTable();
                //    DataJutyu.T_JutyuRow drN = dtNN.NewT_JutyuRow();
                //    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                //    string cp = SessionManager.strCp;
                //    DataJutyu.T_JutyuRow drG = CtlMitsuSyosai.ItemGet4(drN);
                //    drG.RowNo = i + (kijunRow);
                //    ddr.ItemArray = drG.ItemArray;
                //    ddr["CategoryName"] = RadComboCategory.Text;
                //    ddr["CateGory"] = (object)Session["CategoryCode"];
                //    if (ddr["SyouhinCode"] == null)
                //    {
                //        ddr["SyouhinCode"] = "";
                //    }
                //    ddr["JutyuNo"] = "";
                //    ddr["JutyuFlg"] = false;
                //    dtJutyu.Rows.InsertAt(ddr, kijunRow + i);
                //}
                dtJutyu = Session["MeisaiJutyuData"] as DataJutyu.T_JutyuDataTable;
                string strOrderNo = "";
                for (int i = 0; i < dtJutyu.Count; i++)
                {
                    DataSet1.T_OrderedRow dr = dg.NewT_OrderedRow();
                    DataSet1.T_OrderedDataTable dtC = ClassOrdered.GetOrdered(Global.GetConnection());
                    if (dtC.Count > 0)
                    {
                        string OrderedFlg = ClassOrdered.ChkOrderedData(RadComboCategory.SelectedValue, dtJutyu[i].ShiiresakiCode, Global.GetConnection());
                        if (OrderedFlg.Equals("1"))
                        {
                            string strHatyuFlg = ClassOrdered.ChkHatyuFlg(RadComboCategory.SelectedValue, dtJutyu[i].ShiiresakiCode, Global.GetConnection());
                            if (strHatyuFlg.Equals("1"))
                            {
                                string strOrderData = "";
                                strOrderData = ClassOrdered.GetOrderedNo(RadComboCategory.SelectedValue, dtJutyu[i].ShiiresakiCode, Global.GetConnection());
                                strOrderNo = strOrderData.Split(',')[0];
                                dr.RowNo = int.Parse(strOrderData.Split(',')[1]);
                            }
                            else
                            {
                                DataSet1.T_OrderedRow db = ClassOrdered.GetMaxOrdered(ki, Global.GetConnection());
                                strOrderNo = (db.OrderedNo + 1).ToString();
                                dr.RowNo = 1;
                            }
                        }
                        else
                        {
                            DataSet1.T_OrderedRow db = ClassOrdered.GetMaxOrdered(ki, Global.GetConnection());
                            strOrderNo = (db.OrderedNo + 1).ToString();
                            dr.RowNo = 1;
                        }
                    }
                    else
                    {
                        strOrderNo = "3" + (ki * 100000 + 1).ToString();
                        dr.RowNo = 1;
                    }

                    dr.OrderedNo = int.Parse(strOrderNo);
                    dr.HatyuFLG = "0";
                    dr.HatyuDay = DateTime.Now.ToString();
                    dr.JutyuSuryou = dtJutyu[i].JutyuSuryou;
                    dr.Zansu = dtJutyu[i].JutyuSuryou.ToString();
                    dr.StaffName = Tantou.Text;
                    if (RadZeiKubun.Text != "")
                    {
                        dr.Zeikubun = RadZeiKubun.Text;
                    }
                    if (!dtJutyu[i].IsShiireNameNull())
                    {
                        dr.ShiireSakiName = dtJutyu[i].ShiireName;
                    }
                    if (Session["Kakeritsu"].Equals(""))
                    {
                        dr.Kakeritsu = (string)Session["Kakeritsu"];
                    }
                    if (!dtJutyu[i].IsMekarHinbanNull())
                    {
                        dr.MekerNo = dtJutyu[i].MekarHinban;
                    }
                    if (!dtJutyu[i].IsSyouhinMeiNull())
                    {
                        dr.ProductName = dtJutyu[i].SyouhinMei;
                        cate = RadComboCategory.Text;
                        string hanni = dtJutyu[i].Range;
                        DataSet1.M_Kakaku_2DataTable dk = Class1.getproduct(dr.MekerNo, cate.Trim(), hanni, Global.GetConnection());
                        if (dk.Count > 0)
                        {
                            dr.ProductCode = int.Parse(dk[0].SyouhinCode);
                            dr.ShiiresakiCode = dk[0].ShiireCode;
                            dr.ShiireSakiName = dk[0].ShiireName;
                        }
                        else
                        {
                            Err.Text = "商品マスタに不備があります。マスタデータの調査を行ってください。" + "<br>" + "メーカー品番【" + dr.MekerNo + "】";
                            return;
                        }
                    }
                    dr.ShiiresakiCode = dtJutyu[i].ShiiresakiCode.ToString();
                    if (!dtJutyu[i].IsRangeNull())
                    {
                        dr.Range = dtJutyu[i].Range;
                    }
                    if (!dtJutyu[i].IsZasuNull())
                    {
                        dr.Zasu = dtJutyu[i].Zasu;
                    }
                    if (!dtJutyu[i].IsKeitaiMeiNull())
                    {
                        dr.Media = dtJutyu[i].KeitaiMei;
                    }
                    if (!dtJutyu[i].IsHyojunKakakuNull())
                    {
                        string hyoutan = dtJutyu[i].HyojunKakaku;
                        string r1 = hyoutan.Replace(",", "");
                        dr.HyoujyunKakaku = int.Parse(r1);
                    }
                    if (!dtJutyu[i].IsJutyuGokeiNull())
                    {
                        string kin = dtJutyu[i].JutyuGokei.ToString();
                        string r4 = kin.Replace(",", "");
                        dr.Ryoukin = r4;
                        dr.JutyuGokei = int.Parse(r4);
                    }
                    if (RadComboBox2.Text != "")
                    {
                        dr.TyokusousakiMei = TokuisakiMei.Value;
                        // dr.TyokusousakiCD = int.Parse(TyokusoCode.Value);
                    }
                    if (!dtJutyu[i].IsSiyouKaishiNull())
                    {
                        dr.SiyouKaishi = dtJutyu[i].SiyouKaishi.ToShortDateString();
                    }
                    if (!dtJutyu[i].IsSiyouOwariNull())
                    {
                        dr.SiyouiOwari = dtJutyu[i].SiyouOwari.ToShortDateString();
                    }
                    if (!dtJutyu[i].IsJutyuTankaNull())
                    {
                        string tan = dtJutyu[i].JutyuTanka.ToString();
                        string r1 = tan.Replace(",", "");
                        dr.JutyuTanka = int.Parse(r1);
                    }

                    if (!dtJutyu[i].IsShiireTankaNull())
                    {
                        string shitan = dtJutyu[i].ShiireTanka.ToString();
                        string r2 = shitan.Replace(",", "");
                        dr.ShiireTanka = int.Parse(r2);
                    }
                    if (!dtJutyu[i].IsShiireKingakuNull())
                    {
                        string shikin = dtJutyu[i].ShiireKingaku.ToString();
                        string r3 = shikin.Replace(",", "");
                        dr.ShiireKingaku = int.Parse(r3);
                    }

                    if (RadComboBox3.Text != "")
                    {
                        dr.SeikyusakiMei = RadComboBox3.Text;
                    }
                    if (RadComboCategory.Text != "")
                    {
                        dr.CategoryName = RadComboCategory.Text;
                        dr.Category = int.Parse(CategoryCode.Value);
                    }
                    if (!dtJutyu[i].IsSisetuCodeNull())
                    {
                        dr.FacilityCode = dtJutyu[i].SisetuCode;
                    }
                    if (!dtJutyu[i].IsSisetuMeiNull())
                    {
                        dr.FacilityName = dtJutyu[i].SisetuMei;
                    }
                    if (!dtJutyu[i].IsSisetuJusyo1Null())
                    {
                        dr.FacilityJusyo1 = dtJutyu[i].SisetuJusyo1;
                    }
                    if (!dtJutyu[i].IsSisetuJusyo2Null())
                    {
                        dr.FacilityJusyo2 = dtJutyu[i].SisetuJusyo2;
                    }
                    if (!dtJutyu[i].IsSisetsuTellNull())
                    {
                        dr.FacilityTel = dtJutyu[i].SisetsuTell;
                    }
                    if (RadComboBox1.SelectedValue != null)
                    {
                        dr.TokuisakiCode = RadComboBox1.SelectedValue;
                    }
                    if (!string.IsNullOrEmpty(RadComboBox1.Text))
                    {
                        dr.TokuisakiMei = RadComboBox1.Text;
                    }
                    if (!string.IsNullOrEmpty(RadComboBox3.Text))
                    {
                        dr.SeikyusakiMei = RadComboBox3.Text;
                    }
                    if (!string.IsNullOrEmpty(RadComboBox2.Text))
                    {
                        dr.TyokusosakiMei = RadComboBox2.Text;
                    }
                    if (!dtJutyu[i].IsTanTouNameNull())
                    {
                        dr.StaffName = Tantou.Text;
                    }
                    if (!dtJutyu[i].IsJutyuSuryouNull())
                    {
                        dr.JutyuSuryou = dtJutyu[i].JutyuSuryou;
                    }
                    if (!dtJutyu[i].IsTekiyou1Null())
                    {
                        dr.Tekiyou1 = dtJutyu[i].Tekiyou1;
                    }
                    if (!dtJutyu[i].IsTekiyou2Null())
                    {
                        dr.Tekiyou2 = dtJutyu[i].Tekiyou2;
                    }
                    dr.UriageFlg = false;
                    ClassOrdered.UpdateOrderedHeader(dp, dr, cate, shiire, Global.GetConnection());
                    dp = null;
                }
                DataUriage.T_UriageHeaderRow dru = ClassUriage.GetMaxNo(Global.GetConnection());
                string jNo = JutyuNo.Text;
                int noo = dru.UriageNo;
                int Uno = noo + 1;
                ClassUriage.InsertUriage(jNo, Uno, Global.GetConnection());
            }
            catch (Exception ex)
            {
                ClassMail.ErrorMail("maeda@m2m-asp.com", "エラーメール | 発注登録", ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source);
                Err.Text = "データを登録することができませんでした。";
            }
            if (Err.Text == "")
            {
                End.Text = "データを登録しました。";
            }
        }
        //発注ボタン
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Jutyu/JutyuJoho.aspx");
        }

        protected void RadComboBox5_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string[] un = RadComboBox5.Text.Split('/');
            JutyuNo.Text = un[0];
            LblTantoStaffCode.Text = RadComboBox5.SelectedValue;
            HidTantoStaffCode.Value = RadComboBox5.SelectedValue;
            DataSet1.M_TantoDataTable dtT = Class1.GetStaff2(un[0], Global.GetConnection());
            RadComboBox4.Items.Clear();
            for (int t = 0; t < dtT.Count; t++)
            {
                RadComboBox4.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
            }
            RadComboBox4.SelectedItem.Text = un[1];
            TBTokuisaki.Style["display"] = "";
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            Button13.Style["display"] = "none";
            head.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            RadComboBox5.Style["display"] = "";
            RcbTax.Style["display"] = "";
            RcbShimebi.Style["display"] = "";
        }

        protected void RadComboBox5_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetStaff(sender, e);
            }
        }

        protected void BtnToMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("Master/MsterTokuisaki.aspx");
        }
        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbxTokuisakiRyakusyo.Text != "")
                {
                    RadComboBox1.Text = TbxTokuisakiRyakusyo.Text;
                    RadComboBox3.Text = TbxTokuisakiRyakusyo.Text;
                }
                else
                {
                    ErrorSet(1);
                    return;
                }
                if (TbxKakeritsu.Text != "")
                {
                    Label3.Text = TbxKakeritsu.Text;
                }
                else
                {
                    ErrorSet(2);
                    return;
                }

                if (RcbShimebi.Text != "")
                {
                    Shimebi.Text = RcbShimebi.Text;
                }
                else
                {
                    ErrorSet(3);
                    return;
                }

                if (RcbTax.Text != "")
                {
                    RadZeiKubun.SelectedValue = RcbTax.Text.Trim();
                }
                else
                {
                    ErrorSet(4);
                    return;
                }

                if (LblTantoStaffCode.Text != "")
                {
                    string[] un = RadComboBox5.Text.Split('/');
                    JutyuNo.Text = un[0];
                    LblTantoStaffCode.Text = RadComboBox5.SelectedValue;
                    DataSet1.M_TantoDataTable dtT = Class1.GetStaff2(un[0], Global.GetConnection());
                    RadComboBox4.Items.Clear();
                    for (int t = 0; t < dtT.Count; t++)
                    {
                        RadComboBox4.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
                    }
                    RadComboBox4.SelectedItem.Text = un[1];
                }
                else
                {
                    ErrorSet(5);
                    return;
                }
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    CtrlMitsuSyousai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    Label Kakeri = (Label)Ctl.FindControl("Kakeri");
                    Label zeiku = (Label)Ctl.FindControl("zeiku");

                    if (TbxKakeritsu.Text != "")
                    {
                        Kakeri.Text = TbxKakeritsu.Text;
                    }
                    else
                    {
                        ErrorSet(2);
                        return;
                    }

                    if (RcbTax.Text != "")
                    {
                        zeiku.Text = RcbTax.Text;
                    }
                    else
                    {
                        ErrorSet(4);
                        return;
                    }
                }
                TBTokuisaki.Style["display"] = "none";
                mInput2.Style["display"] = "";
                CtrlSyousai.Style["display"] = "";
                SubMenu.Style["display"] = "";
                SubMenu2.Style["display"] = "none";
                Button13.Style["display"] = "";
                head.Style["display"] = "";

            }
            catch (Exception ex)
            {
                Err.Text = "登録時にエラーが発生しました。エラー内容は「" + ex.Message + "」";
            }
        }

        private void ErrorSet(int v)
        {
            switch (v)
            {
                case 1:
                    Err.Text = "得意先名を入力して下さい。";
                    break;

                case 2:
                    Err.Text = "掛率を入力して下さい。";
                    break;

                case 3:
                    Err.Text = "締日を選択して下さい。";
                    break;

                case 4:
                    Err.Text = "税区分を入力して下さい。";
                    break;

                case 5:
                    Err.Text = "担当スタッフを入力して下さい。";
                    break;
            }
        }

        protected void CtrlSyousai_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DataJutyu.T_JutyuDataTable dtN = null;
            if ((DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"] == null)
            {
                dtN = new DataJutyu.T_JutyuDataTable();
            }
            else
            {
                dtN = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
            }
            int NowPage = CtrlSyousai.CurrentPageIndex;
            int kijunRow = NowPage * 10;
            if (dtN.Count > 0)
            {
                for (int d = kijunRow; d < kijunRow + CtrlSyousai.Items.Count; d++)
                {
                    dtN.RemoveT_JutyuRow(dtN[kijunRow]);
                }
            }

            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                DataJutyu.T_JutyuDataTable dd = new DataJutyu.T_JutyuDataTable();
                dd = dtN;
                DataRow ddr = dd.NewRow() as DataJutyu.T_JutyuRow;
                DataJutyu.T_JutyuDataTable dtNN = new DataJutyu.T_JutyuDataTable();
                DataJutyu.T_JutyuRow drN = dtNN.NewT_JutyuRow();
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                string cp = SessionManager.strCp;
                DataJutyu.T_JutyuRow drG = CtlMitsuSyosai.ItemGet4(drN);
                drG.RowNo = i + (kijunRow);
                ddr.ItemArray = drG.ItemArray;
                ddr["CategoryName"] = RadComboCategory.Text;
                ddr["CateGory"] = (object)Session["CategoryCode"];
                if (ddr["SyouhinCode"] == null)
                {
                    ddr["SyouhinCode"] = "";
                }
                ddr["JutyuNo"] = "";
                ddr["JutyuFlg"] = false;
                dd.Rows.InsertAt(ddr, kijunRow + i);
            }

            Session["MeisaiJutyuData"] = dtN;
            CtrlSyousai.CurrentPageIndex = e.NewPageIndex;
            Create3();

        }

        private void Create3()
        {
            DataJutyu.T_JutyuDataTable dt = (DataJutyu.T_JutyuDataTable)Session["MeisaiJutyuData"];
            if (dt.Count > 0)
            {
                CtrlSyousai.VirtualItemCount = dt.Count;
                int nPageSize = CtrlSyousai.PageSize;
                int nPageCount = dt.Count / nPageSize;
                CtrlSyousai.DataSource = dt;
                CtrlSyousai.DataBind();
            }
        }

        protected void BtnPostNoSerch4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TbxTokuisakiPostNo.Text))
            {
                string strPostNo = TbxTokuisakiPostNo.Text.Replace("-", "");
                string[] AryYubinResult = GetYubinAPI(strPostNo);
                if (AryYubinResult != null)
                {
                    TbxTokuisakiAddress.Text = AryYubinResult[0] + AryYubinResult[1] + AryYubinResult[2];
                }
                else
                {
                    Err.Text = "郵便番号検索でエラーが発生しました。";
                }
            }
            else
            {
                Err.Text = "郵便番号を入力して下さい。";
            }
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            DivDataGrid.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            TBSyousais.Style["display"] = "";

        }

        private string[] GetYubinAPI(string strPostNo)
        {
            try
            {
                string[] AryReturnItems = null;
                string url = "https://zip-cloud.appspot.com/api/search?zipcode=" + strPostNo;
                WebRequest request = WebRequest.Create(url);
                Stream response_stream = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(response_stream);
                var obj_from_json = JObject.Parse(reader.ReadToEnd());


                string strItems = obj_from_json["results"][0]["address1"].ToString();
                strItems += "," + obj_from_json["results"][0]["address2"].ToString();
                strItems += "," + obj_from_json["results"][0]["address3"].ToString();
                AryReturnItems = strItems.Split(',');
                return AryReturnItems;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected void BtnCopy1_Click(object sender, EventArgs e)
        {
            string strYubin = TbxTokuisakiPostNo.Text;
            string strJusyo1 = TbxTokuisakiAddress.Text;
            string strJusyo2 = TbxTokuisakiAddress1.Text;
            string strTEL = TbxTokuisakiTEL.Text;

            TbxPostNo2.Text = TbxNouhinsakiYubin.Text = TbxYubin.Text = strYubin;
            TbxTokuisakiAddress2.Text = TbxNouhinsakiAddress.Text = TbxFaciAdress1.Text = strJusyo1;
            TbxTokuisakiAddress3.Text = TbxNouhinsakiAddress2.Text = TbxFaciAdress2.Text = strJusyo2;
            TbxTel2.Text = TbxNouhinsakiTell.Text = TbxTel.Text = strTEL;
            TBSyousais.Style["display"] = "";
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            DivDataGrid.Style["display"] = "none";
        }

        protected void BtnPostNoSerch3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TbxPostNo2.Text))
            {
                string strPostNo = TbxPostNo2.Text.Replace("-", "");
                string[] AryYubinResult = GetYubinAPI(strPostNo);
                if (AryYubinResult != null)
                {
                    TbxTokuisakiAddress2.Text = AryYubinResult[0] + AryYubinResult[1] + AryYubinResult[2];
                }
                else
                {
                    Err.Text = "郵便番号検索でエラーが発生しました。";
                }
            }
            else
            {
                Err.Text = "郵便番号を入力して下さい。";
            }
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            DivDataGrid.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            TBSyousais.Style["display"] = "";

        }

        protected void BtnCopy2_Click(object sender, EventArgs e)
        {
            string strYubin = TbxPostNo2.Text;
            string strJusyo1 = TbxTokuisakiAddress2.Text;
            string strJusyo2 = TbxTokuisakiAddress3.Text;
            string strTEL = TbxTel2.Text;

            TbxTokuisakiPostNo.Text = TbxNouhinsakiYubin.Text = TbxYubin.Text = strYubin;
            TbxTokuisakiAddress.Text = TbxNouhinsakiAddress.Text = TbxFaciAdress1.Text = strJusyo1;
            TbxTokuisakiAddress1.Text = TbxNouhinsakiAddress2.Text = TbxFaciAdress2.Text = strJusyo2;
            TbxTokuisakiTEL.Text = TbxNouhinsakiTell.Text = TbxTel.Text = strTEL;
            TBSyousais.Style["display"] = "";
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            DivDataGrid.Style["display"] = "none";

        }

        protected void BtnCopy3_Click(object sender, EventArgs e)
        {
            string strYubin = TbxNouhinsakiYubin.Text;
            string strJusyo1 = TbxNouhinsakiAddress.Text;
            string strJusyo2 = TbxNouhinsakiAddress2.Text;
            string strTEL = TbxNouhinsakiTell.Text;

            TbxTokuisakiPostNo.Text = TbxPostNo2.Text = TbxYubin.Text = strYubin;
            TbxTokuisakiAddress2.Text = TbxTokuisakiAddress.Text = TbxFaciAdress1.Text = strJusyo1;
            TbxTokuisakiAddress3.Text = TbxTokuisakiAddress1.Text = TbxFaciAdress2.Text = strJusyo2;
            TbxTel2.Text = TbxTokuisakiTEL.Text = TbxTel.Text = strTEL;
            TBSyousais.Style["display"] = "";
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            DivDataGrid.Style["display"] = "none";

        }

        protected void BtnCopy4_Click(object sender, EventArgs e)
        {
            string strYubin = TbxYubin.Text;
            string strJusyo1 = TbxFaciAdress1.Text;
            string strJusyo2 = TbxFaciAdress2.Text;
            string strTEL = TbxTel.Text;

            TbxTokuisakiPostNo.Text = TbxPostNo2.Text = TbxNouhinsakiYubin.Text = strYubin;
            TbxTokuisakiAddress2.Text = TbxNouhinsakiAddress.Text = TbxTokuisakiAddress.Text = strJusyo1;
            TbxTokuisakiAddress3.Text = TbxNouhinsakiAddress2.Text = TbxTokuisakiAddress1.Text = strJusyo2;
            TbxTel2.Text = TbxNouhinsakiTell.Text = TbxTokuisakiTEL.Text = strTEL;
            TBSyousais.Style["display"] = "";
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            DivDataGrid.Style["display"] = "none";
        }

        protected void BtnPostNoSerch2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TbxNouhinsakiYubin.Text))
            {
                string strPostNo = TbxNouhinsakiYubin.Text.Replace("-", "");
                string[] AryYubinResult = GetYubinAPI(strPostNo);
                if (AryYubinResult != null)
                {
                    TbxNouhinsakiAddress.Text = AryYubinResult[0] + AryYubinResult[1] + AryYubinResult[2];
                }
                else
                {
                    Err.Text = "郵便番号検索でエラーが発生しました。";
                }
            }
            else
            {
                Err.Text = "郵便番号を入力して下さい。";
            }
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            DivDataGrid.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            TBSyousais.Style["display"] = "";

        }

        protected void BtnPostNoSerch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TbxYubin.Text))
            {
                string strPostNo = TbxYubin.Text.Replace("-", "");
                string[] AryYubinResult = GetYubinAPI(strPostNo);
                if (AryYubinResult != null)
                {
                    TbxFaciAdress1.Text = AryYubinResult[0] + AryYubinResult[1] + AryYubinResult[2];
                }
                else
                {
                    Err.Text = "郵便番号検索でエラーが発生しました。";
                }
            }
            else
            {
                Err.Text = "郵便番号を入力して下さい。";
            }
            mInput2.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            head.Style["display"] = "none";
            TBAddRow.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            DivDataGrid.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            TBSyousais.Style["display"] = "";
        }

        protected void BtnToMasterS_Click(object sender, EventArgs e)
        {
            SessionManager.TokuisakiCode = TbxTokuisakiCode2.Text + "/" + TbxCustomerCode2.Text;
            if (SessionManager.TokuisakiCode == "0/W")
            {
                DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
                DataSet1.M_Tokuisaki2Row dr = dt.NewM_Tokuisaki2Row();
                dr.CustomerCode = TbxCustomerCode2.Text;
                dr.TokuisakiCode = int.Parse(TbxTokuisakiCode2.Text);
                dr.TokuisakiName1 = RcbSeikyusaki.Text;
                dr.TokuisakiFurigana = TbxTokuisakiFurigana2.Text;
                dr.TokuisakiRyakusyo = TbxTokuisakiRyakusyou2.Text;
                dr.TokuisakiStaff = TbxTokuisakiStaff2.Text;
                dr.TokuisakiPostNo = TbxPostNo2.Text;
                dr.TokuisakiAddress1 = TbxTokuisakiAddress2.Text;
                dr.TokuisakiTEL = TbxTel2.Text;
                dr.TokuisakiFAX = TbxFAX2.Text;
                dr.TokuisakiDepartment = TbxDepartment2.Text;
                dr.Zeikubun = RcbZeikubun2.Text;
                //dr.Kakeritsu = TbxKakeritsu2.Text;
                dr.Shimebi = RadShimebi2.Text;
                //dr.TantoStaffCode = LblTantoStaffNo2.Text;
                SessionManager.drTokuisaki = dr;
            }
            Response.Redirect("Master/MsterTokuisaki.aspx");
        }

        protected void RcbNouhinsakiMei_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTyokusouSaki2(sender, e);
            }
        }

        protected void BtnNouhinMasta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TbxNouhinSisetsu.Text))
            {
                SessionManager.NouhinsakiCode = TbxNouhinSisetsu.Text;
                Response.Redirect("Master/MasterTyokuso.aspx");
            }
            else
            {
                Err.Text = "納品先を選択して下さい";
            }

        }

        protected void RcbFacility_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetFacility(sender, e);
            }
        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}
