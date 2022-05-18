using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace Gyomu
{

    public partial class Kaihatsu : System.Web.UI.Page
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
            KariNouhin.Visible = false;
            LblTantoStaffCode.Text = HidTantoStaffCode.Value;
            LblTantoStaffNo2.Text = HidTantoStaffCode2.Value;
            Label3.Text = TbxKakeritsu.Text;
            strKakeritsu = TbxKakeritsu.Text;
            Shimebi.Text = RcbShimebi.Text;
            strZeikubun = RcbTax.Text;
            Err.Text = "";
            End.Text = "";
            if (!IsPostBack)
            {
                TBTokuisaki.Style["display"] = "none";
                TBSeikyusaki.Style["display"] = "none";
                NouhinsakiPanel.Style["display"] = "none";
                Button1.OnClientClick = string.Format("Meisai2('{0}'); return false;", TBTokuisaki.ClientID);
                Button2.OnClientClick = string.Format("Meisai2('{0}'); return false;", TBSeikyusaki.ClientID);
                Button6.OnClientClick = string.Format("Close2('{0}'); return false;", TBTokuisaki.ClientID);
                Button3.OnClientClick = string.Format("MeisaiNouhin('{0}'); return false;", NouhinsakiPanel.ClientID);
                mInput.Visible = true;
                CtrlSyousai.Visible = true;
                SubMenu.Visible = true;
                SubMenu2.Style["display"] = "none";
                RadDatePicker1.SelectedDate = DateTime.Now;
                RadDatePicker2.SelectedDate = DateTime.Now;
                RadDatePicker3.SelectedDate = DateTime.Now;
                ListSet.SetCategory(RadComboCategory);
                ListSet.SetCity(RcbNouhinsakiCity);
                if (SessionManager.HACCYU_NO != "")
                {
                    Create2();
                }
                else
                {
                    objFacility = null;
                    Create();
                }
            }
        }

        private void Create2()
        {
            string MNo = SessionManager.HACCYU_NO;
            Label94.Text = MNo;
            DataMitumori.T_MitumoriDataTable dt = ClassMitumori.GETMitsumori(MNo, Global.GetConnection());
            DataMitumori.T_MitumoriHeaderDataTable dd = ClassMitumori.GETMitsumorihead(MNo, Global.GetConnection());
            DataMitumori.T_MitumoriHeaderRow dr = dd[0];

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
            }
            if (!dr.IsCategoryNameNull())
            {
                RadComboCategory.SelectedValue = dr.CateGory.ToString();
                strCategoryCode = dr.CateGory.ToString();
                strCategoryName = dr.CategoryName;
            }
            if (!dr.IsKariFLGNull())
            {
                if (dr.KariFLG == "True")
                {
                    CheckBox1.Checked = true;
                }
            }
            if (!dr.IsGokeiKingakuNull())
            {
                TextBox7.Text = dr.GokeiKingaku.ToString("0,0");
                urikei.Value = dr.GokeiKingaku.ToString();
            }
            if (!dr.IsShiireKingakuNull())
            {
                TextBox8.Text = dr.ShiireKingaku.ToString("0,0");
                shikei.Value = dr.ShiireKingaku.ToString();
            }

            if (!dr.IsSyohiZeiGokeiNull())
            {
                TextBox5.Text = dr.SyohiZeiGokei.ToString("0,0");
            }
            if (!dr.IsSoukeiGakuNull())
            {
                TextBox12.Text = dr.SoukeiGaku.ToString("0,0");
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
                    TbxTokuisakiName.Text = dr.TokuisakiName;
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
                        TbxTokuisakiMei2.Text = dr.SekyuTokuisakiMei;
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
                    if (!dr.IsSekyuTokuisakiKakeritsuNull())
                    {
                        TbxKakeritsu2.Text = dr.SekyuTokuisakiKakeritsu;
                    }
                    if (!dr.IsSekyuTokuisakiShimebiNull())
                    {
                        RadShimebi2.Text = dr.SekyuTokuisakiShimebi;
                    }
                    if (!dr.IsSekyuTokuisakiStaffNull())
                    {
                        LblTantoStaffNo2.Text = dr.SekyuTokuisakiStaff;
                    }
                    string[] tokucode = dr.TokuisakiCode.Trim().Split('/');
                    TbxCustomer.Text = tokucode[0];
                    TbxTokuisakiCode.Text = tokucode[1];
                    string un = Label1.Text = dr.TantoName;
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
                    strZeikubun = dr.Zeikubun;
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
            if (!string.IsNullOrEmpty(dr.FacilityCode))
            {
                if (!string.IsNullOrEmpty(dr.FacilityRowCode))
                {
                    string[] facCode = (dr.FacilityCode + "/" + dr.FacilityRowCode).Split('/');
                    DataMaster.M_Facility_NewRow df = ClassMaster.GetFacilityRow(facCode, Global.GetConnection());
                    //objFacility = df.ItemArray;
                }
            }
            if (!dr.IsNouhinSisetsuCodeNull())
            {
                TbxNouhinSisetsu.Text = dr.NouhinSisetsuCode;
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

            RadDatePicker1.SelectedDate = dr.SyokaiBi;
            RadDatePicker2.SelectedDate = dr.MitsumoriBi;
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

        //使用期間---------------------------------------------------------------------------------
        protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
        {
            string d = DropDownList9.SelectedValue;
            if (d == "1日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(0);
            }

            if (d == "2日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(1);
            }

            if (d == "3日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(2);
            }
            if (d == "4日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(3);
            }
            if (d == "5日")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddDays(4);
            }
            if (d == "1ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddMonths(1);
            }
            if (d == "2ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddMonths(2);
            }
            if (d == "3ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddMonths(3);
            }
            if (d == "4ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddMonths(4);
            }
            if (d == "5ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                RadDatePicker4.SelectedDate = dd.AddMonths(5);
            }
            if (d == "6ヵ月")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddMonths(6);
            }
            if (d == "1年")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddYears(1);
            }
            if (d == "99年")
            {
                DateTime dd = RadDatePicker3.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                RadDatePicker4.SelectedDate = dt.AddYears(98);
            }
        }
        //使用期間---------------------------------------------------------------------------------
        //得意先ボタン------------------------------------------------

        //納品先ボタン-------------------------------------------------
        protected void Button3_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Style["display"] = "none";
            mInput.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            Button13.Style["display"] = "none";
            head.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            RadComboBox5.Style["display"] = "";
            RcbTax.Style["display"] = "";
            RcbShimebi.Style["display"] = "";
            NouhinsakiPanel.Style["display"] = "";
            DivDataGrid.Style["display"] = "none";

            //TBTokuisaki.Visible = true;
            //mInput.Visible = false;
            //CtrlSyousai.Visible = false;
            ////AddBtn.Visible = false;
            //Button13.Visible = false;
            //head.Visible = false;
            //SubMenu.Visible = false;
            //SubMenu2.Visible = true;
            //ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            //KensakuParam(p);

            //DataSet1.M_Tyokusosaki1DataTable dt = ClassKensaku.GetTyokusou(p, Global.GetConnection());
            //for (int j = 0; j < dt.Count; j++)
            //{
            //    DataSet1.M_Tyokusosaki1Row dr = dt.Rows[j] as DataSet1.M_Tyokusosaki1Row;

            //    TextBox27.Text = dr.TyokusousakiCode.ToString();
            //    if (!dr.IsTyokusousakiMei1Null())
            //    {
            //        TextBox28.Text = dr.TyokusousakiMei1;
            //    }

            //    if (!dr.IsYubinbangouNull())
            //    {
            //        TextBox35.Text = dr.Yubinbangou;
            //    }
            //    if (!dr.IsJusyo1Null() && !dr.IsJusyo2Null())
            //    {
            //        TextBox29.Text = dr.Jusyo1 + dr.Jusyo2;
            //    }
            //    if (!dr.IsTellNull())
            //    {
            //        TextBox30.Text = dr.Tell;
            //    }
            //    if (!dr.IsFaxNull())
            //    {
            //        TextBox31.Text = dr.Fax;
            //    }
            //    if (!dr.IsTyokusousakiTantouNull())
            //    {
            //        TextBox32.Text = dr.TyokusousakiTantou;
            //    }
            //}
        }
        //納品先ボタン-------------------------------------------------

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
                ListSet.SetTyokusouSaki2(sender, e);
            }
        }

        protected void RadComboCategory_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetCategory(sender, e);
        }

        //得意先Rad--------------------------------------------------------------------------------------------------------------------
        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string tanto = LblTantoStaffCode.Text;
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTanto1(LblTantoStaffCode.Text, Global.GetConnection());
                RadComboBox5.Text = dt[0].UserName;
                RcbStaffName.Text = dt[0].UserName;
                Label1.Text = dt[0].UserName;
                for (int i = 0; i < dt.Count; i++)
                {
                    RadComboBox4.Items.Add(new RadComboBoxItem(dt[0].Busyo));
                }
                FacilityRad.Focus();
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
            }
        }
        //得意先rad---------------------------------------------------------------------------------------------------------------------
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

        protected void RadComboCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CategoryCode.Value = RadComboCategory.SelectedValue;
            string a = CategoryCode.Value;
            strCategoryCode = RadComboCategory.SelectedValue;
            category(a);
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                HtmlInputHidden cate = CtlMitsuSyosai.FindControl("HidCategoryCode") as HtmlInputHidden;
                CtlMitsuSyosai.Test4(strCategoryCode);
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
            //CtrlMitsuSyousai.categoryV(a);
        }

        //ItemDataBind
        public void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                string mNo = SessionManager.HACCYU_NO;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    int no = CtrlSyousai.Items.Count + 1;
                    DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                    DataMitumori.T_MitumoriRow df = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_MitumoriRow;
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
                    Button Button4 = (Button)Ctl.FindControl("Button4");
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
                    Button4.OnClientClick = string.Format("Close('{0}'); return false;", SisetuSyousai.ClientID);


                    if (df != null)
                    {
                        Ctl.ItemSet2(df);
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

                    if (CheckBox1.Checked == true)
                    {
                        KariTokui.Visible = true;
                        kariSekyu.Visible = true;
                        KariNouhin.Visible = true;
                        RadComboBox1.Visible = false;
                        RadComboBox2.Visible = false;
                        RadComboBox3.Visible = false;
                        Label3.Visible = false;
                        TbxKake.Visible = true;
                    }
                    if (RadComboCategory.SelectedValue != "")
                    {
                        string a = RadComboCategory.SelectedValue;
                        Ctl.Test4(a);
                        CategoryCode.Value = RadComboCategory.SelectedValue;
                    }
                }
                //kari();
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
            }
        }
        //ItemDataBInd

        private void AllFukusu()
        {
            Fukusu();
        }

        //登録ボタン---------------------------------------------------
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                //Keisan2();
                AllFukusu();
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録
                DataMitumori.T_MitumoriHeaderDataTable dp = new DataMitumori.T_MitumoriHeaderDataTable();
                DataMitumori.T_MitumoriHeaderRow dl = dp.NewT_MitumoriHeaderRow();
                if (!string.IsNullOrEmpty(TbxTokuisakiFurigana.Text))
                {
                    dl.TokuisakiFurigana = TbxTokuisakiFurigana.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiStaff.Text))
                {
                    dl.TokuisakiStaff = TbxTokuisakiStaff.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiPostNo.Text))
                {
                    dl.TokuisakiPostNo = TbxTokuisakiPostNo.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiAddress.Text))
                {
                    dl.TokuisakiAddress = TbxTokuisakiAddress.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiAddress1.Text))
                {
                    dl.TokuisakiAddress2 = TbxTokuisakiAddress1.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiTEL.Text))
                {
                    dl.TokuisakiTEL = TbxTokuisakiTEL.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiFax.Text))
                {
                    dl.TokuisakiFAX = TbxTokuisakiFax.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiDepartment.Text))
                {
                    dl.TokuisakiDepartment = TbxTokuisakiDepartment.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiCode.Text))
                {
                    if (!string.IsNullOrEmpty(TbxCustomer.Text))
                    {
                        dl.TokuisakiCode = TbxCustomer.Text + "/" + TbxTokuisakiCode.Text;
                    }
                }
                if (!string.IsNullOrEmpty(LblTantoStaffCode.Text))
                {
                    dl.StaffNo = LblTantoStaffCode.Text;
                }
                if (CheckBox5.Checked)
                {
                    string[] faci = FacilityRad.SelectedValue.Split('/');
                    dl.FacilityCode = faci[0];
                    dl.FacilityRowCode = faci[1];
                }

                if (RadDatePicker2.SelectedDate != null)
                {
                    dl.CreateDate = RadDatePicker2.SelectedDate.Value;
                }

                if (TextBox7.Text != "")
                {
                    string r1 = TextBox7.Text.Replace(",", "");
                    dl.GokeiKingaku = int.Parse(r1);
                }
                if (TextBox2.Text != "")
                {
                    dl.Bikou = TextBox2.Text;
                }
                if (TextBox8.Text != "")
                {
                    string r2 = TextBox8.Text.Replace(",", "");
                    dl.ShiireKingaku = int.Parse(r2);
                }

                if (TextBox5.Text != "")
                {
                    string r3 = TextBox5.Text.Replace(",", "");
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

                if (TextBox12.Text != "0")
                {
                    string sou = TextBox12.Text;
                    string r9 = sou.Replace(",", "");
                    dl.SoukeiGaku = int.Parse(r9);
                }
                else
                {
                    string r5 = TextBox7.Text.Replace(",", "");
                    dl.SoukeiGaku = int.Parse(r5);
                }

                if (CheckBox1.Checked)
                {
                    dl.TokuisakiName = KariTokui.Text;
                }
                else
                {
                    if (RadComboBox1.Text != "")
                    {
                        dl.TokuisakiCode = TbxCustomer.Text + "/" + TbxTokuisakiCode.Text;
                        dl.TokuisakiName = TbxTokuisakiName.Text;
                        dl.TokuisakiRyakusyo = TbxTokuisakiRyakusyo.Text;
                        dl.Shimebi = RcbShimebi.Text;
                        dl.Kakeritsu = TbxKakeritsu.Text.Trim();
                        dl.Zeikubun = RcbTax.Text.Trim();
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
                    string[] fac = FacilityRad.SelectedValue.Split('/');
                    dl.FacilityCode = fac[0];
                    dl.FacilityRowCode = fac[1];
                }
                if (Label1.Text != "")
                {
                    dl.TantoName = Label1.Text;
                }
                dl.JutyuFlg = "False";
                if (RadComboBox4.Text != "")
                {
                    dl.Bumon = RadComboBox4.Text;
                }
                if (CheckBox1.Checked == true)
                {
                    dl.KariFLG = CheckBox1.Checked.ToString();
                }
                dl.CreateDate = DateTime.Now;
                dl.MitsumoriBi = RadDatePicker2.SelectedDate.Value;
                dl.SyokaiBi = RadDatePicker1.SelectedDate.Value;
                if (RadDatePicker3.SelectedDate != null && RadDatePicker3.Visible)
                {
                    dl.SiyouKaisi = DateTime.Parse(RadDatePicker3.SelectedDate.Value.ToShortDateString());
                }
                if (RadDatePicker4.SelectedDate != null && RadDatePicker4.Visible)
                {
                    dl.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.Value.ToShortDateString());
                }
                if (TbxNouhinSisetsu.Text != "")
                {
                    dl.NouhinSisetsuCode = TbxNouhinSisetsu.Text;
                }
                if (RcbNouhinsakiMei.Text != "")
                {
                    dl.NouhinTyokusousakiMei1 = RcbNouhinsakiMei.Text;
                }
                if (TbxNouhinTyokusousakiMei2.Text != "")
                {
                    dl.NouhinTyokusousakiMei2 = TbxNouhinTyokusousakiMei2.Text;
                }
                if (TbxNouhinsakiRyakusyou.Text != "")
                {
                    dl.NouhinTyokusousakiRyakusyou = TbxNouhinsakiRyakusyou.Text;
                }
                if (TbxNouhinsakiTanto.Text != "")
                {
                    dl.NouhinTanto = TbxNouhinsakiTanto.Text;
                }
                if (TbxNouhinsakiYubin.Text != "")
                {
                    dl.NouhinYubin = TbxNouhinsakiYubin.Text;
                }
                if (TbxNouhinsakiAddress.Text != "")
                {
                    dl.NouhinAddress = TbxNouhinsakiAddress.Text;
                }
                if (TbxNouhinsakiAddress2.Text != "")
                {
                    dl.NouhinAddress2 = TbxNouhinsakiAddress2.Text;
                }
                if (RcbNouhinsakiCity.Text != "")
                {
                    dl.NouhinCity = RcbNouhinsakiCity.SelectedValue;
                }
                if (TbxNouhinsakiTell.Text != "")
                {
                    dl.NouhinTell = TbxNouhinsakiTell.Text;
                }
                //請求先情報
                if (!string.IsNullOrEmpty(TbxCustomerCode2.Text))
                {
                    dl.SekyuCustomerCode = TbxCustomerCode2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiCode2.Text))
                {
                    dl.SekyuTokuisakiCode = TbxTokuisakiCode2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiMei2.Text))
                {
                    dl.SekyuTokuisakiMei = TbxTokuisakiMei2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiFurigana2.Text))
                {
                    dl.SekyuTokuisakiFurigana = TbxTokuisakiFurigana2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiRyakusyou2.Text))
                {
                    dl.SekyuTokuisakiRyakusyo = TbxTokuisakiRyakusyou2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiStaff2.Text))
                {
                    dl.SekyuTokuisakiStaff = TbxTokuisakiStaff2.Text;
                }
                if (!string.IsNullOrEmpty(TbxPostNo2.Text))
                {
                    dl.SekyuTokuisakiPostNo = TbxPostNo2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiAddress2.Text))
                {
                    dl.SekyuTokuisakiAddress = TbxTokuisakiAddress2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTokuisakiAddress3.Text))
                {
                    dl.SekyuTokuisakiAddress2 = TbxTokuisakiAddress3.Text;
                }
                if (!string.IsNullOrEmpty(TbxTel2.Text))
                {
                    dl.SekyuTokuisakiTel = TbxTel2.Text;
                }
                if (!string.IsNullOrEmpty(TbxFAX2.Text))
                {
                    dl.SekyuTokuisakiFax = TbxFAX2.Text;
                }
                if (!string.IsNullOrEmpty(TbxDepartment2.Text))
                {
                    dl.SekyuTokuisakiDepartment = TbxDepartment2.Text;
                }
                if (!string.IsNullOrEmpty(RcbZeikubun2.Text))
                {
                    dl.SekyuTokuisakiZeikubun = RcbZeikubun2.Text;
                }
                if (!string.IsNullOrEmpty(TbxKakeritsu2.Text))
                {
                    dl.SekyuTokuisakiKakeritsu = TbxKakeritsu2.Text;
                }
                if (!string.IsNullOrEmpty(RadShimebi2.Text))
                {
                    dl.SekyuTokuisakiShimebi = RadShimebi2.Text;
                }
                if (!string.IsNullOrEmpty(LblTantoStaffNo2.Text))
                {
                    dl.SekyuTokuisakiStaff = LblTantoStaffNo2.Text;
                }

                string no = "";
                if (Label94.Text != "")
                {
                    string mNo = Label94.Text;

                    DataMitumori.T_MitumoriHeaderDataTable dx = ClassMitumori.GETMitsumorihead(mNo, Global.GetConnection());

                    if (dx.Count >= 1)
                    {
                        dl.MitumoriNo = int.Parse(mNo);
                        dp.AddT_MitumoriHeaderRow(dl);

                        ClassMitumori.UpdateMitumoriHeader(mNo, dp, Global.GetConnection());
                        ClassMitumori.DelMitumori3(mNo, Global.GetConnection());
                        no = dl.MitumoriNo.ToString();
                    }
                }
                else
                {
                    SessionManager.KI();
                    int ki = int.Parse(SessionManager.KII);
                    DataMitumori.T_MitumoriHeaderRow dr = ClassMitumori.GetMaxNo(ki, Global.GetConnection());
                    if (dr != null)
                    {
                        no = (dr.MitumoriNo + 1).ToString();
                    }
                    else
                    {
                        no = "1" + (ki * 10 + 1).ToString();
                    }
                    dl.MitumoriNo = int.Parse(no);
                    dp.AddT_MitumoriHeaderRow(dl);
                    ClassMitumori.InsertMitsumoriHeader(dp, Global.GetConnection());
                }

                //ここから明細////////////////////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataMitumori.T_MitumoriDataTable dg = new DataMitumori.T_MitumoriDataTable();
                    DataMitumori.T_MitumoriRow dr = dg.NewT_MitumoriRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    dr = CtlMitsuSyosai.ItemGet2(dr);
                    dr.TokuisakiCode = TbxCustomer.Text + "/" + TbxTokuisakiCode.Text;
                    dr.TokuisakiMei = TbxTokuisakiName.Text;
                    dr.SeikyusakiMei = TbxTokuisakiName.Text;
                    dr.CateGory = int.Parse(RadComboCategory.SelectedValue);
                    dr.CategoryName = RadComboCategory.Text;
                    dr.TourokuName = Label1.Text;
                    dr.TanTouName = Label1.Text;
                    dr.Busyo = RadComboBox4.Text;
                    if (CheckBox5.Checked)
                    {
                        dr.FukusuFaci = "true";
                    }
                    dr.RowNo = i + 1;
                    if (Label94.Text != "")
                    {
                        dr.MitumoriNo = Label94.Text;
                        dr.JutyuFlg = false;
                        dg.AddT_MitumoriRow(dr);
                        ClassMitumori.UpDateMitumori2(dg, Global.GetConnection());
                    }
                    else
                    {
                        dr.MitumoriNo = no;
                        dr.JutyuFlg = false;
                        dg.AddT_MitumoriRow(dr);
                        ClassMitumori.InsertMitsumori(dg, Global.GetConnection());
                        dg = null;
                    }
                }
                if (dl != null)
                {
                    Label94.Text = dl.MitumoriNo.ToString();
                }
            }
            catch (Exception ex)
            {
                Err.Text = "データを登録することができませんでした。";
            }
            if (Err.Text == "")
            {
                End.Text = "データを登録しました。";
            }
        }
        //登録ボタン---------------------------------------------------
        protected void Button6_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Style["display"] = "none";
            mInput.Visible = true;
            CtrlSyousai.Visible = true;
            //AddBtn.Visible = true;
            Button13.Visible = true;
            head.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
            NouhinsakiPanel.Style["display"] = "none";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Visible = false;
            mInput.Visible = false;
            CtrlSyousai.Visible = false;
            //AddBtn.Visible = false;
            SubMenu.Visible = false;
            SubMenu2.Visible = true;
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Master/MsterTokuisaki.aspx");
        }
        protected void Button11_Click(object sender, EventArgs e)
        {
            Response.Redirect("Master/MasterTyokuso.aspx");
        }

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

        //複数施設---------------------------------------------------------------------------------------------------
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
                    strStartDate = RadDatePicker3.SelectedDate.ToString();
                    if (RadDatePicker4.SelectedDate != null)
                    {
                        string EndDate = RadDatePicker4.SelectedDate.ToString();
                        strEndDate = RadDatePicker4.SelectedDate.ToString();
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
            int a = e.Item.ItemIndex;
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            if (e.CommandName == "Del")
            {
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                    try
                    {
                        CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                        dr = CtlMitsuSyosai.ItemGet(dr);
                        if (a != i)
                        {
                            dt.AddT_RowRow(dr);
                        }
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                //drをdtに追加
                DelCreate(dt);
            }
            if (e.CommandName.Equals("Add"))
            {
                strCategoryCode = RadComboCategory.SelectedValue;
                strCategoryName = RadComboCategory.Text;
                DataMitumori.T_RowDataTable dtN = new DataMitumori.T_RowDataTable();
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataMitumori.T_RowRow drN = dtN.NewT_RowRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    DataMitumori.T_RowRow drG = CtlMitsuSyosai.ItemGet();
                    drN.ItemArray = drG.ItemArray;
                    dtN.AddT_RowRow(drN);
                    if (a == i)
                    {
                        //新規の空の明細を追加
                        DataMitumori.T_RowRow drN2 = dtN.NewT_RowRow();
                        drN2 = AddNewRow2(drN2);
                        dtN.AddT_RowRow(drN2);
                    }
                }
                CtrlSyousai.DataSource = dtN;
                CtrlSyousai.DataBind();
            }
            if (e.CommandName.Equals("Copy"))
            {
                strCategoryCode = RadComboCategory.SelectedValue;
                strCategoryName = RadComboCategory.Text;
                DataMitumori.T_RowDataTable dtN = new DataMitumori.T_RowDataTable();
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataMitumori.T_RowRow drN = dtN.NewT_RowRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    DataMitumori.T_RowRow drG = CtlMitsuSyosai.ItemGet();
                    drN.ItemArray = drG.ItemArray;
                    dtN.AddT_RowRow(drN);
                    if (a == i)
                    {
                        //新規の空の明細を追加
                        DataMitumori.T_RowRow drN2 = dtN.NewT_RowRow();
                        DataMitumori.T_RowRow drG2 = CtlMitsuSyosai.ItemGet();
                        drN2.ItemArray = drG2.ItemArray;
                        dtN.AddT_RowRow(drN2);
                    }
                }
                CtrlSyousai.DataSource = dtN;
                CtrlSyousai.DataBind();
            }
        }

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

        //行削除---------------------------------------------------------------------------------------

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
            objFacility = null;
            Response.Redirect("Mitumori/MitumoriItiran.aspx");
        }

        //計算ボタン-----------------------------------------------
        //protected void Keisan_Click(object sender, EventArgs e)
        //{
        //    //Keisan2();
        //}
        //計算ボタン-----------------------------------------------


        //掛率-----------------------------------------------------------------
        protected void TbxKake_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                Label kakeri = (Label)CtlMitsuSyosai.FindControl("Kakeri");

                kakeri.Text = TbxKake.Text;
            }
        }
        //掛率-----------------------------------------------------------------

        protected void RadZeiKubun_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                Label zeiku = (Label)CtlMitsuSyosai.FindControl("zeiku");

                zeiku.Text = RadZeiKubun.Text;
            }
        }

        //複写------------------------------------------------------
        protected void Button7_Click(object sender, EventArgs e)
        {
            Label94.Text = "";
        }
        //複写-----------------------------------------------------

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            string mNo = Label94.Text;
            ClassMitumori.DelMitumori3(mNo, Global.GetConnection());
            ClassMitumori.DelMitumoriHeader(mNo, Global.GetConnection());
            End.Text = "見積No." + mNo + "を削除しました。";
        }

        //受注ボタン----------------------------------------------------
        protected void HatyuBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AllFukusu();
                DataMitumori.T_RowDataTable df = new DataMitumori.T_RowDataTable();
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録

                DataJutyu.T_JutyuHeaderDataTable dp = new DataJutyu.T_JutyuHeaderDataTable();
                DataJutyu.T_JutyuHeaderRow dl = dp.NewT_JutyuHeaderRow();
                DataMitumori.T_MitumoriHeaderDataTable dw = ClassMitumori.GetMitumoriHeader(Label94.Text, Global.GetConnection());
                string Mno = Label94.Text;
                if (!string.IsNullOrEmpty(Mno))
                {
                    DataMitumori.T_MitumoriHeaderDataTable dtM = ClassMitumori.GetMitumoriHeader(Mno, Global.GetConnection());
                    dl.ItemArray = dtM[0].ItemArray;
                    dl.CareateDate = DateTime.Now;
                    dl.Relay = "受注";
                }
                else
                {
                    Err.Text = "まずは見積登録からお願い致します。";
                    return;
                }
                //if (RadDatePicker2.SelectedDate != null)
                //{
                //    dl.CareateDate = RadDatePicker2.SelectedDate.Value;
                //}

                //if (TextBox7.Text != "")
                //{
                //    string r1 = TextBox7.Text.Replace(",", "");
                //    dl.SoukeiGaku = int.Parse(r1);
                //}
                //if (TextBox8.Text != "")
                //{
                //    string r2 = TextBox8.Text.Replace(",", "");
                //    dl.ShiireKingaku = int.Parse(r2);
                //}
                //if (TextBox5.Text != "")
                //{
                //    string r3 = TextBox5.Text.Replace(",", "");
                //    dl.SyohiZeiGokei = int.Parse(r3);
                //}
                //dl.CategoryName = RadComboCategory.Text;
                //dl.CateGory = int.Parse(CategoryCode.Value);

                //if (TextBox6.Text != "")
                //{
                //    string r4 = TextBox6.Text.Replace(",", "");
                //    dl.ArariGokeigaku = int.Parse(r4);
                //}
                //if (TextBox10.Text != "")
                //{
                //    dl.SouSuryou = int.Parse(TextBox10.Text);
                //}
                //if (TextBox12.Text != "0")
                //{
                //    string sou = TextBox12.Text;
                //    string r9 = sou.Replace(",", "");
                //    dl.SoukeiGaku = int.Parse(r9);
                //}
                //else
                //{
                //    string r5 = TextBox7.Text.Replace(",", "");
                //    dl.SoukeiGaku = int.Parse(r5);
                //}

                //if (CheckBox1.Checked == true)
                //{
                //    dl.TokuisakiName = KariTokui.Text;
                //    //dl.TokuisakiCode = TextBox19.Text;
                //}
                //else
                //{
                //    if (RadComboBox1.Text != "")
                //    {
                //        dl.TokuisakiName = RadComboBox1.Text;
                //        dl.TokuisakiCode = CustomerCode.Value + "/" + TokuisakiCode.Value;
                //    }
                //}
                //if (RadComboBox3.Text != "")
                //{
                //    dl.SeikyusakiName = RadComboBox3.Text;
                //}
                //if (RadComboBox2.Text != "")
                //{
                //    dl.TyokusosakiName = RadComboBox2.Text;
                //}
                //if (FacilityRad.Text != "")
                //{
                //    dl.FacilityName = FacilityRad.Text;
                //}
                //if (Label1.Text != "")
                //{
                //    dl.TantoName = Label1.Text;
                //}
                //if (RadComboBox4.Text != "")
                //{
                //    dl.Bumon = RadComboBox4.Text;
                //}
                //if (RadZeiKubun.Text != "")
                //{
                //    dl.ZeiKubun = RadZeiKubun.Text;
                //}
                //if (CheckBox1.Checked == true)
                //{
                //    dl.KariFLG = CheckBox1.Checked.ToString();
                //}

                //if (Label3.Text != "")
                //{
                //    dl.Kakeritsu = Label3.Text;
                //}

                //if (TbxKake.Text != "")
                //{
                //    dl.Kakeritsu = TbxKake.Text;
                //}

                //if (RadDatePicker3.SelectedDate != null && RadDatePicker3.Visible)
                //{
                //    dl.SiyouKaisi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                //}

                //if (RadDatePicker4.SelectedDate != null && RadDatePicker4.Visible)
                //{
                //    dl.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                //}

                //if (TextBox2.Text != "")
                //{
                //    dl.Bikou = TextBox2.Text;
                //}

                //dl.CareateDate = RadDatePicker2.SelectedDate.Value;

                //見積データに受注フラグをtrueに更新する
                string mno = "";
                if (SessionManager.HACCYU_NO != "")
                {
                    mno = SessionManager.HACCYU_NO;
                }
                else
                {
                    mno = Label94.Text;
                }

                //受注データに挿入
                string arr = mno.ToString().Substring(1, 7);
                string no = "2" + arr;
                dl.Shimebi = Shimebi.Text;
                dl.JutyuNo = int.Parse(no);
                Label94.Text = no;
                dl.HatyuFlg = "false";
                dl.CareateDate = DateTime.Now;
                dp.AddT_JutyuHeaderRow(dl);
                ClassJutyu.InsertJutyuH2(dp, Global.GetConnection());
                dw[0].JutyuFlg = true.ToString();
                dw[0].Relay = "受注";
                ClassMitumori.UpdateMitumoriHeader(mno, dw, Global.GetConnection());

                DataJutyu.T_JutyuDataTable dg = new DataJutyu.T_JutyuDataTable();
                DataMitumori.T_MitumoriDataTable dtMs = ClassMitumori.GETMitsumori(Mno, Global.GetConnection());
                for (int i = 0; i < dtMs.Count; i++)
                {
                    DataJutyu.T_JutyuRow drN = dg.NewT_JutyuRow();
                    drN.ItemArray = dtMs[i].ItemArray;
                    dg.AddT_JutyuRow(drN);
                }
                //DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu(Global.GetConnection());
                //for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                //{
                //    DataJutyu.T_JutyuRow dr = dg.NewT_JutyuRow();
                //    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                //    RadComboBox Zaseki = (RadComboBox)CtlMitsuSyosai.FindControl("Zasu");
                //    Label Media = (Label)CtlMitsuSyosai.FindControl("Baitai");
                //    TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
                //    TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
                //    RadComboBox ShiyouShisetsu = (RadComboBox)CtlMitsuSyosai.FindControl("ShiyouShisetsu");
                //    RadDatePicker StartDate = (RadDatePicker)CtlMitsuSyosai.FindControl("StartDate");
                //    RadDatePicker EndDate = (RadDatePicker)CtlMitsuSyosai.FindControl("EndDate");
                //    TextBox Tanka = (TextBox)CtlMitsuSyosai.FindControl("Tanka");
                //    TextBox Uriage = (TextBox)CtlMitsuSyosai.FindControl("Uriage");
                //    RadComboBox Hachu = (RadComboBox)CtlMitsuSyosai.FindControl("Hachu");
                //    TextBox ShiireTanka = (TextBox)CtlMitsuSyosai.FindControl("ShiireTanka");
                //    TextBox ShiireKingaku = (TextBox)CtlMitsuSyosai.FindControl("ShiireKingaku");
                //    HtmlInputHidden FacilityCode = (HtmlInputHidden)CtlMitsuSyosai.FindControl("FacilityCode");
                //    HtmlInputHidden procd = (HtmlInputHidden)CtlMitsuSyosai.FindControl("procd");
                //    Label Facility = (Label)CtlMitsuSyosai.FindControl("Facility");
                //    Label end = (Label)CtlMitsuSyosai.FindControl("Label12");
                //    Label start = (Label)CtlMitsuSyosai.FindControl("Label11");
                //    Label RowNo = CtrlSyousai.Items[i].FindControl("RowNo") as Label;
                //    RadComboBox Tekiyou = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                //    TextBox Suryo = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
                //    HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
                //    HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
                //    HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
                //    HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
                //    HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
                //    HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
                //    TextBox KariFaci = (TextBox)CtlMitsuSyosai.FindControl("KariFaci");
                //    HtmlInputHidden FacAdd = (HtmlInputHidden)CtlMitsuSyosai.FindControl("FacilityAddress");
                //    Label WareHouse = (Label)CtlMitsuSyosai.FindControl("WareHouse");
                //    Label kakeri = (Label)CtlMitsuSyosai.FindControl("Kakeri");
                //    Label Lblproduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                //    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                //    Label LblHanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");
                //    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                //    TextBox TbxFacilityName = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName");
                //    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                //    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                //    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                //    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                //    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                //    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                //    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                //    RadComboBox RcbHanni = (RadComboBox)CtlMitsuSyosai.FindControl("RcbHanni");

                //    if (TbxFaciAdress.Text != "")
                //    {
                //        dr.SisetuJusyo1 = TbxFaciAdress.Text;
                //    }
                //    if (TbxYubin.Text != "")
                //    {
                //        dr.SisetuPost = TbxYubin.Text;
                //    }
                //    if (RcbCity.SelectedValue != "")
                //    {
                //        dr.SisetuCityCode = RcbCity.SelectedValue;
                //    }
                //    if (TbxFacilityCode.Text != "")
                //    {
                //        dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
                //    }
                //    if (TbxFacilityResponsible.Text != "")
                //    {
                //        dr.SisetuTanto = TbxFacilityResponsible.Text;
                //    }
                //    if (TbxFaci.Text != "")
                //    {
                //        dr.SisetsuAbbreviration = TbxFaci.Text;
                //    }
                //    if (TbxTel.Text != "")
                //    {
                //        dr.SisetsuTell = TbxTel.Text;
                //    }

                //    if (ShiyouShisetsu.Text != "")
                //    {
                //        dr.SisetuMei = ShiyouShisetsu.Text;
                //    }

                //    dr.JutyuSuryou = int.Parse(Suryo.Text);
                //    dr.TanTouName = Label1.Text;
                //    dr.RowNo = i + 1;

                //    if (RadZeiKubun.Text != "")
                //    {
                //        dr.ZeiKubun = RadZeiKubun.Text;
                //    }
                //    if (kakeri.Text != "")
                //    {
                //        dr.Kakeritsu = kakeri.Text;
                //    }
                //    if (Lblproduct.Text != "")
                //    {
                //        dr.MekarHinban = Lblproduct.Text;
                //    }
                //    if (SerchProduct.Text != "")
                //    {
                //        string cate = RadComboCategory.Text;
                //        string hanni = LblHanni.Text;
                //        dr.SyouhinMei = SerchProduct.Text;
                //        //dr.SyouhinCode = arr[1];
                //        DataSet1.M_Kakaku_2DataTable dk = Class1.getproduct(SerchProduct.Text, cate, hanni, Global.GetConnection());
                //        for (int p = 0; p < dk.Count; p++)
                //        {
                //            dr.SyouhinCode = dk[p].SyouhinCode;
                //        }
                //    }
                //    if (LblHanni.Text != "")
                //    {
                //        dr.Range = LblHanni.Text;
                //    }
                //    if (RcbHanni.Text != "")
                //    {
                //        dr.Range = RcbHanni.Text;
                //    }
                //    if (Zaseki.Text != "")
                //    {
                //        dr.Zasu = Zaseki.Text;
                //    }
                //    if (Media.Text != "")
                //    {
                //        dr.KeitaiMei = Media.Text;
                //    }
                //    if (HyoujyunTanka.Text != "")
                //    {
                //        if (HyoujyunTanka.Text != "OPEN")
                //        {
                //            string hyoutan = HyoujyunTanka.Text;
                //            string r1 = hyoutan.Replace(",", "");
                //            dr.HyojunKakaku = r1;
                //        }
                //    }
                //    if (Kingaku.Text != "")
                //    {
                //        string kin = Kingaku.Text;
                //        string r4 = kin.Replace(",", "");
                //        dr.Ryoukin = r4;
                //        dr.JutyuGokei = int.Parse(r4);
                //    }
                //    if (RadComboBox2.Text != "")
                //    {
                //        dr.TyokusousakiMei = CustomerCode.Value;
                //        // dr.TyokusousakiCD = int.Parse(TyokusoCode.Value);
                //    }
                //    if (CheckBox4.Checked == true)
                //    {
                //        dr.SiyouKaishi = StartDate.SelectedDate.Value;
                //        dr.SiyouOwari = EndDate.SelectedDate.Value;
                //    }
                //    if (CheckBox4.Checked == false)
                //    {
                //        if (StartDate.SelectedDate != null)
                //        {
                //            dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString());
                //        }
                //        if (EndDate.SelectedDate != null)
                //        {
                //            dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString());
                //        }
                //    }
                //    if (Tanka.Text != "")
                //    {
                //        string tan = Tanka.Text;
                //        string r1 = tan.Replace(",", "");
                //        dr.JutyuTanka = int.Parse(r1);
                //    }
                //    if (Uriage.Text != "")
                //    {
                //        string uri = Uriage.Text;
                //        string r1 = uri.Replace(",", "");
                //        dr.JutyuGokei = int.Parse(r1);
                //    }

                //    if (ShiireTanka.Text != "")
                //    {
                //        string shitan = ShiireTanka.Text;
                //        string r2 = shitan.Replace(",", "");
                //        dr.ShiireTanka = int.Parse(r2);
                //    }
                //    if (ShiireKingaku.Text != "")
                //    {
                //        string shikin = ShiireKingaku.Text;
                //        string r3 = shikin.Replace(",", "");
                //        dr.ShiireKingaku = int.Parse(r3);
                //    }

                //    if (RadDatePicker1.SelectedDate != null)
                //    {
                //        dr.StartDate = RadDatePicker1.SelectedDate.Value;
                //    }

                //    if (RadComboBox1.Text != "")
                //    {
                //        dr.TokuisakiMei = RadComboBox1.Text;
                //        dr.TokuisakiCode = TokuisakiCode.Value;
                //    }
                //    if (RadComboBox3.Text != "")
                //    {
                //        dr.SeikyusakiMei = RadComboBox3.Text;
                //    }
                //    if (RadComboCategory.Text != "")
                //    {
                //        dr.CategoryName = RadComboCategory.Text;
                //        dr.CateGory = int.Parse(CategoryCode.Value);
                //    }

                //    if (CheckBox1.Checked == true)
                //    {
                //        dr.SisetuMei = KariFaci.Text;
                //        dr.SisetuCode = int.Parse(FacilityCode.Value);
                //        dr.SisetuJusyo1 = FacAdd.Value;
                //        dr.TokuisakiMei = KariTokui.Text;
                //        //dr.TokuisakiCode = TextBox19.Text;
                //    }

                //    if (Hachu.Text != "")
                //    {
                //        dr.HattyuSakiMei = Hachu.Text;
                //    }
                //    if (Label1.Text != "")
                //    {
                //        dr.TanTouName = Label1.Text;
                //    }
                //    if (RadComboBox4.Text != "")
                //    {
                //        dr.Busyo = RadComboBox4.Text;
                //    }
                //    if (Tekiyou.Text != "")
                //    {
                //        dr.Tekiyou1 = Tekiyou.Text;
                //    }

                //    if (TextBox10.Text != "")
                //    {
                //        dr.JutyuSuryou = int.Parse(Suryo.Text);
                //    }
                //    if (FacilityAddress.Value != "")
                //    {
                //        dr.SisetuJusyo1 = FacilityAddress.Value;
                //    }
                //    if (WareHouse.Text != "")
                //    {
                //        dr.WareHouse = WareHouse.Text;
                //    }

                //    dr.JutyuNo = int.Parse(no);
                //    dr.UriageFlg = false;
                //    dg.AddT_JutyuRow(dr);
                //}
                ClassJutyu.InsertJutyu2(dg, Global.GetConnection());
            }
            catch
            {
                Err.Text = "データを登録することができませんでした。";
            }
            if (Err.Text == "")
            {
                End.Text = "データを登録しました。";
            }
        }

        protected void Button10_Click1(object sender, EventArgs e)
        {

        }

        protected void RCBTEST_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetStaff(sender, e);
            }
        }

        protected void raddo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetStaff(sender, e);
            }
        }

        protected void RadComboBox5_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetStaff(sender, e);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Style["display"] = "";
            mInput.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            Button13.Style["display"] = "none";
            head.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            RadComboBox5.Style["display"] = "";
            RcbTax.Style["display"] = "";
            RcbShimebi.Style["display"] = "";
        }

        protected void BtnToMaster_Click(object sender, EventArgs e)
        {
            SessionManager.TokuisakiCode = TbxTokuisakiCode.Text + "/" + TbxCustomer.Text;
            if (SessionManager.TokuisakiCode == "0/W")
            {
                DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
                DataSet1.M_Tokuisaki2Row dr = dt.NewM_Tokuisaki2Row();
                dr.CustomerCode = TbxCustomer.Text;
                dr.TokuisakiCode = int.Parse(TbxTokuisakiCode.Text);
                dr.TokuisakiName1 = TbxTokuisakiName.Text;
                dr.TokuisakiFurigana = TbxTokuisakiFurigana.Text;
                dr.TokuisakiRyakusyo = TbxTokuisakiRyakusyo.Text;
                dr.TokuisakiStaff = TbxTokuisakiStaff.Text;
                dr.TokuisakiPostNo = TbxTokuisakiPostNo.Text;
                dr.TokuisakiAddress1 = TbxTokuisakiAddress.Text;
                dr.TokuisakiTEL = TbxTokuisakiTEL.Text;
                dr.TokuisakiFAX = TbxTokuisakiFax.Text;
                dr.TokuisakiDepartment = TbxTokuisakiDepartment.Text;
                dr.Zeikubun = RcbTax.Text;
                dr.Kakeritsu = TbxKakeritsu.Text;
                dr.Shimebi = RcbShimebi.Text;
                dr.TantoStaffCode = LblTantoStaffCode.Text;
                SessionManager.drTokuisaki = dr;
            }
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
                    Label1.Text = un[0];
                    LblTantoStaffCode.Text = RadComboBox5.SelectedValue;
                    DataSet1.M_TantoDataTable dtT = Class1.GetStaff2(un[0], Global.GetConnection());
                    RadComboBox4.Items.Clear();
                    for (int t = 0; t < dtT.Count; t++)
                    {
                        RadComboBox4.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
                    }
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
                mInput.Style["display"] = "";
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

        protected void RadComboBox5_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string[] un = RadComboBox5.Text.Split('/');
            Label1.Text = un[0];
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
            mInput.Style["display"] = "none";
            CtrlSyousai.Style["display"] = "none";
            Button13.Style["display"] = "none";
            head.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            RadComboBox5.Style["display"] = "";
            RcbTax.Style["display"] = "";
            RcbShimebi.Style["display"] = "";
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

        protected void BtnNouhisakiTekiou_Click(object sender, EventArgs e)
        {
            RadComboBox2.Text = RcbNouhinsakiMei.Text;
            TBTokuisaki.Style["display"] = "none";
            mInput.Visible = true;
            CtrlSyousai.Visible = true;
            //AddBtn.Visible = true;
            Button13.Visible = true;
            head.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
            NouhinsakiPanel.Style["display"] = "none";
        }

        protected void BtnSekyu_Click(object sender, EventArgs e)
        {
            RadComboBox3.Text = TbxTokuisakiMei2.Text;
            TBTokuisaki.Style["display"] = "none";
            mInput.Visible = true;
            CtrlSyousai.Visible = true;
            //AddBtn.Visible = true;
            Button13.Visible = true;
            head.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
            NouhinsakiPanel.Style["display"] = "none";
            TBSeikyusaki.Style["display"] = "none";
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
                dr.TokuisakiName1 = TbxTokuisakiMei2.Text;
                dr.TokuisakiFurigana = TbxTokuisakiFurigana2.Text;
                dr.TokuisakiRyakusyo = TbxTokuisakiRyakusyou2.Text;
                dr.TokuisakiStaff = TbxTokuisakiStaff2.Text;
                dr.TokuisakiPostNo = TbxPostNo2.Text;
                dr.TokuisakiAddress1 = TbxTokuisakiAddress2.Text;
                dr.TokuisakiTEL = TbxTel2.Text;
                dr.TokuisakiFAX = TbxFAX2.Text;
                dr.TokuisakiDepartment = TbxDepartment2.Text;
                dr.Zeikubun = RcbZeikubun2.Text;
                dr.Kakeritsu = TbxKakeritsu2.Text;
                dr.Shimebi = RadShimebi2.Text;
                dr.TantoStaffCode = LblTantoStaffNo2.Text;
                SessionManager.drTokuisaki = dr;
            }
            Response.Redirect("Master/MsterTokuisaki.aspx");
        }
    }
}

