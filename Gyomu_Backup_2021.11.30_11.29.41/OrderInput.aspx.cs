using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
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
            KariNouhin.Visible = false;
            LblTantoStaffCode.Text = HidTantoStaffCode.Value;
            Label3.Text = TbxKakeritsu.Text;
            strKakeritsu = TbxKakeritsu.Text;
            Shimebi.Text = RcbShimebi.Text;
            strZeikubun = RcbTax.Text;
            Err.Text = "";
            End.Text = "";
            NouhinsakiPanel.Visible = false;
            if (!IsPostBack)
            {
                TBTokuisaki.Style["display"] = "none";
                Button1.OnClientClick = string.Format("Meisai2('{0}'); return false;", TBTokuisaki.ClientID);
                Button6.OnClientClick = string.Format("Close2('{0}'); return false;", TBTokuisaki.ClientID);
                mInput2.Visible = true;
                CtrlSyousai.Visible = true;
                SubMenu.Visible = true;
                SubMenu2.Style["display"] = "none";
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


        private void Create2()
        {
            string MNo = SessionManager.HACCYU_NO;
            string[] strAry = MNo.Split('_');
            string MiNo = strAry[0];
            DataJutyu.T_JutyuHeaderDataTable dtH = ClassJutyu.GetJutyuHeader(MiNo, Global.GetConnection());
            DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu3(MiNo, Global.GetConnection());
            DataJutyu.T_JutyuHeaderRow dr = dtH[0];

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
                    RadComboBox1.Text = dr.TokuisakiRyakusyo;
                    RadComboBox3.Text = dr.TokuisakiRyakusyo;
                    TbxTokuisakiRyakusyo.Text = dr.TokuisakiRyakusyo;
                    TbxTokuisakiName.Text = dr.TokuisakiName;
                    string[] tokucode = dr.TokuisakiCode.Trim().Split('/');
                    CustomerCode.Value = tokucode[0];
                    TbxCustomer.Text = tokucode[0];
                    TokuisakiCode.Value = tokucode[1];
                    TbxTokuisakiCode.Text = tokucode[1];
                    DataSet1.M_Tokuisaki2Row drT = Class1.GetTokuisaki4(tokucode[1], tokucode[0], dr.TokuisakiName, Global.GetConnection());
                    if (drT != null)
                    {
                        if (!drT.IsTokuisakiFuriganaNull())
                        {
                            TbxTokuisakiFurigana.Text = drT.TokuisakiFurigana;
                        }
                        if (!drT.IsTokuisakiRyakusyoNull())
                        {
                            TbxTokuisakiRyakusyo.Text = drT.TokuisakiRyakusyo;
                        }
                        if (!drT.IsTokuisakiStaffNull())
                        {
                            TbxTokuisakiStaff.Text = drT.TokuisakiStaff;
                        }
                        if (!drT.IsTokuisakiPostNoNull())
                        {
                            TbxTokuisakiPostNo.Text = drT.TokuisakiPostNo;
                        }
                        if (!drT.IsTokuisakiAddress1Null())
                        {
                            TbxTokuisakiAddress.Text = drT.TokuisakiAddress1;
                        }
                        if (!drT.IsTokuisakiAddress2Null())
                        {
                            TbxTokuisakiAddress.Text += drT.TokuisakiAddress2;
                        }
                        if (!drT.IsTokuisakiTELNull())
                        {
                            TbxTokuisakiTEL.Text = drT.TokuisakiTEL;
                        }
                        if (!drT.IsTokuisakiFAXNull())
                        {
                            TbxTokuisakiFax.Text = drT.TokuisakiFAX;
                        }
                        if (!drT.IsTokuisakiDepartmentNull())
                        {
                            TbxTokuisakiDepartment.Text = drT.TokuisakiDepartment;
                        }
                    }
                    string un = Tantou.Text = dr.TantoName;
                    string bn = dr.Bumon;
                    RadComboBox5.Text = dr.TantoName;
                    DataSet1.M_TantoDataTable dtT = Class1.GetStaff3(un, bn, Global.GetConnection());
                    for (int t = 0; t < dtT.Count; t++)
                    {
                        RadComboBox4.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
                    }
                    LblTantoStaffCode.Text = dtT[0].UserID.ToString();
                    RadZeiKubun.SelectedValue = dr.ZeiKubun;
                    RcbTax.SelectedValue = dr.ZeiKubun;
                    strZeikubun = dr.ZeiKubun;
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
            if (!dr.IsCareateDateNull())
            {
                RadDatePicker2.SelectedDate = dr.CareateDate;
            }

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            TokuisakiSyousai.Visible = true;
            mInput2.Visible = false;
            CtrlSyousai.Visible = false;
            Button13.Visible = false;
            head.Visible = false;
            SubMenu.Visible = false;
            SubMenu2.Visible = true;
            string tokuimei = RadComboBox1.Text;
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            GetKensakuParam(p);

            DataSet1.M_Tyokusosaki1DataTable dt = ClassKensaku.GetTyokusou(p, Global.GetConnection());
            for (int j = 0; j < dt.Count; j++)
            {
                DataSet1.M_Tyokusosaki1Row dr = dt.Rows[j] as DataSet1.M_Tyokusosaki1Row;

                TextBox27.Text = dr.TyokusousakiCode.ToString();
                if (!dr.IsTyokusousakiMei1Null())
                {
                    TextBox28.Text = dr.TyokusousakiMei1;
                }

                if (!dr.IsYubinbangouNull())
                {
                    TextBox35.Text = dr.Yubinbangou;
                }
                if (!dr.IsJusyo1Null() && !dr.IsJusyo2Null())
                {
                    TextBox29.Text = dr.Jusyo1 + dr.Jusyo2;
                }
                if (!dr.IsTellNull())
                {
                    TextBox30.Text = dr.Tell;
                }
                if (!dr.IsFaxNull())
                {
                    TextBox31.Text = dr.Fax;
                }
                if (!dr.IsTyokusousakiTantouNull())
                {
                    TextBox32.Text = dr.TyokusousakiTantou;
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TokuisakiSyousai.Visible = true;
            mInput2.Visible = false;
            CtrlSyousai.Visible = false;
            Button13.Visible = false;
            head.Visible = false;
            SubMenu.Visible = false;
            SubMenu2.Visible = true;
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            KensakuParam(p);

            DataSet1.M_Tyokusosaki1DataTable dt = ClassKensaku.GetTyokusou(p, Global.GetConnection());
            for (int j = 0; j < dt.Count; j++)
            {
                DataSet1.M_Tyokusosaki1Row dr = dt.Rows[j] as DataSet1.M_Tyokusosaki1Row;

                TextBox27.Text = dr.TyokusousakiCode.ToString();
                if (!dr.IsTyokusousakiMei1Null())
                {
                    TextBox28.Text = dr.TyokusousakiMei1;
                }

                if (!dr.IsYubinbangouNull())
                {
                    TextBox35.Text = dr.Yubinbangou;
                }
                if (!dr.IsJusyo1Null() && !dr.IsJusyo2Null())
                {
                    TextBox29.Text = dr.Jusyo1 + dr.Jusyo2;
                }
                if (!dr.IsTellNull())
                {
                    TextBox30.Text = dr.Tell;
                }
                if (!dr.IsFaxNull())
                {
                    TextBox31.Text = dr.Fax;
                }
                if (!dr.IsTyokusousakiTantouNull())
                {
                    TextBox32.Text = dr.TyokusousakiTantou;
                }
            }
        }

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
                string tanto = LblTantoStaffCode.Text;
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTanto1(LblTantoStaffCode.Text, Global.GetConnection());
                RadComboBox5.Text = dt[0].UserName;
                Tantou.Text = dt[0].UserName;
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
        }

        //ItemDataBound
        public void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            string mNo = SessionManager.HACCYU_NO;
            if (mNo != "")
            {
                string[] strAry = mNo.Split('_');
                string MiNo = strAry[0];

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    try
                    {
                        int no = CtrlSyousai.Items.Count + 1;

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

                        //kari();

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
                    dl.ZeiKubun = RadZeiKubun.Text;
                }
                if (CheckBox1.Checked == true)
                {
                    dl.KariFLG = CheckBox1.Checked.ToString();
                }

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
                        dl.HatyuFlg = "false";
                        dl.Relay = "受注";
                        dl.CareateDate = DateTime.Now;
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

                DataJutyu.T_JutyuDataTable dg = new DataJutyu.T_JutyuDataTable();
                DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu(Global.GetConnection());
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = dg.NewT_JutyuRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    dr = CtlMitsuSyosai.ItemGet3(dr);

                    dr.TokuisakiCode = TbxCustomer.Text + "/" + TbxTokuisakiCode.Text;
                    dr.TokuisakiMei = TbxTokuisakiName.Text;
                    dr.SeikyusakiMei = TbxTokuisakiName.Text;
                    dr.CateGory = int.Parse(RadComboCategory.SelectedValue);
                    dr.CategoryName = RadComboCategory.Text;
                    dr.TourokuName = Tantou.Text;
                    dr.TanTouName = Tantou.Text;
                    dr.Busyo = RadComboBox4.Text;
                    if (JutyuNo.Text != "")
                    {
                        dr.JutyuNo = int.Parse(JutyuNo.Text);
                        dr.UriageFlg = false;
                        dg.AddT_JutyuRow(dr);
                    }
                    else
                    {
                        dr.JutyuNo = int.Parse(vsNo);
                        dr.UriageFlg = false;
                        dg.AddT_JutyuRow(dr);
                    }
                }
                ClassJutyu.InsertJutyu2(dg, Global.GetConnection());
                JutyuNo.Text = dl.JutyuNo.ToString();
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            TokuisakiSyousai.Visible = true;
            mInput2.Visible = false;
            CtrlSyousai.Visible = false;
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
            int a = e.Item.ItemIndex;
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            //int sIndex = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataMitumori.T_RowRow dr = dt.NewT_RowRow();

                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    Label RowNo = e.Item.FindControl("RowNo") as Label;
                    //TextBox MakerHinban = (TextBox)CtlMitsuSyosai.FindControl("MakerHinban");
                    //RadComboBox ProductName = (RadComboBox)CtlMitsuSyosai.FindControl("ProductName");
                    //RadComboBox Hanni = (RadComboBox)CtlMitsuSyosai.FindControl("Hanni");
                    RadComboBox Zaseki = (RadComboBox)CtlMitsuSyosai.FindControl("Zasu");
                    Label Media = (Label)CtlMitsuSyosai.FindControl("Baitai");
                    TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
                    TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
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
                    Label end = (Label)CtlMitsuSyosai.FindControl("Label12");
                    Label start = (Label)CtlMitsuSyosai.FindControl("Label11");
                    TextBox Suryo = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
                    HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
                    HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
                    HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
                    HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
                    HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
                    HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
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
                    //Fukudate();

                    if (WareHouse.Text != "")
                    { dr.WareHouse = WareHouse.Text; }
                    if (Tekiyo.Text != "")
                    { dr.JutyuTekiyo = Tekiyo.Text; }
                    if (kakeri.Text != "")
                    { dr.Kakeritsu = kakeri.Text; }
                    if (zeiku.Text != "")
                    { dr.Zeiku = zeiku.Text; }
                    //すでに入力していた文字たちをdrに入れる
                    if (Suryo.Text != "")
                    { dr.HattyuSuryou = int.Parse(Suryo.Text); }

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
                        string h = HyoujyunTanka.Text;
                        string r10 = h.Replace(",", "");
                        dr.HyojunKakaku = r10;
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
                            dr.Basyo = ShiyouShisetsu.Text;
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
                        string ry = Kingaku.Text.Replace(",", "");
                        dr.Ryoukin = ry;
                    }
                    if (Tanka.Text != "")
                    {
                        string tan = Tanka.Text.Replace(",", "");
                        dr.HattyuTanka = int.Parse(tan);
                    }
                    if (Uriage.Text != "")
                    {
                        string uri = Uriage.Text.Replace(",", "");
                        dr.Uriage = uri;
                    }
                    if (ShiireKingaku.Text != "")
                    {
                        string shi = ShiireKingaku.Text;
                        string r11 = shi.Replace(",", "");

                        dr.ShiireKingaku = r11;
                    }
                    if (ShiireTanka.Text != "")
                    {
                        string shitan = ShiireTanka.Text;
                        string r12 = shitan.Replace(",", "");
                        dr.ShiireTanka = r12;
                    }
                    dt.AddT_RowRow(dr);
                    if (a == i)
                    {
                        DataMitumori.T_RowRow dl = dt.Rows[i] as DataMitumori.T_RowRow;
                        dt.RemoveT_RowRow(dr);
                    }
                }
                //AddCreate(dt);
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

        }//
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
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuHeaderDataTable dw = new DataJutyu.T_JutyuHeaderDataTable();
                    DataJutyu.T_JutyuHeaderRow dh = dw.NewT_JutyuHeaderRow();
                    DataSet1.T_OrderedHeaderDataTable dp = new DataSet1.T_OrderedHeaderDataTable();
                    DataSet1.T_OrderedHeaderRow dl = dp.NewT_OrderedHeaderRow();

                    if (Uriagekeijyou.Text != "")
                    {
                        string r1 = Uriagekeijyou.Text.Replace(",", "");
                        dl.SoukeiGaku = int.Parse(r1);
                        dh.GokeiKingaku = int.Parse(r1);
                        dh.SoukeiGaku = int.Parse(r1);
                    }
                    if (Shiirekei.Text != "")
                    {
                        string r2 = Shiirekei.Text.Replace(",", "");
                        dl.ShiireKingaku = int.Parse(r2);
                        dh.ShiireKingaku = int.Parse(r2);
                    }
                    if (Zei.Text != "")
                    {
                        string r3 = Zei.Text.Replace(",", "");
                        dl.Tax = int.Parse(r3);
                        dh.SyohiZeiGokei = int.Parse(r3);
                    }
                    dl.CategoryName = RadComboCategory.Text;
                    dl.Category = int.Parse(CategoryCode.Value);
                    dh.CategoryName = RadComboCategory.Text;
                    dh.CateGory = int.Parse(CategoryCode.Value);

                    if (TextBox6.Text != "")
                    {
                        string r4 = TextBox6.Text.Replace(",", "");
                        dl.ArariGokeigaku = r4;
                        dh.ArariGokeigaku = int.Parse(r4);
                    }

                    if (TextBox2.Text != "")
                    {
                        dh.Bikou = TextBox2.Text;
                    }
                    if (UriageGokei.Text != "0")
                    {
                        string sou = UriageGokei.Text;
                        string r9 = sou.Replace(",", "");
                        dl.SoukeiGaku = int.Parse(r9);
                        dh.SoukeiGaku = int.Parse(r9);
                    }
                    else
                    {
                        string r5 = Uriagekeijyou.Text.Replace(",", "");
                        dl.SoukeiGaku = int.Parse(r5);
                        dh.GokeiKingaku = int.Parse(r5);
                    }

                    if (CheckBox1.Checked == true)
                    {
                        dl.TokuisakiName = KariTokui.Text;
                        dh.TokuisakiName = KariTokui.Text;
                    }
                    else
                    {
                        if (RadComboBox1.Text != "")
                        {
                            dl.TokuisakiName = RadComboBox1.Text;
                            dh.TokuisakiName = RadComboBox1.Text;

                            string[] toku = TokuisakiCode.Value.Split('/');
                            DataSet1.M_Tokuisaki2DataTable du = Class1.GetTokuisaki2(toku[0], toku[1], Global.GetConnection());
                            for (int j = 0; j < du.Count; j++)
                            {
                                dl.TokuisakiCode = du[j].CustomerCode + "/" + du[j].TokuisakiCode;
                                dh.TokuisakiCode = du[j].CustomerCode + "/" + du[j].TokuisakiCode;
                            }
                        }
                    }
                    if (RadComboBox3.Text != "")
                    {
                        dl.SeikyusakiName = RadComboBox3.Text;
                        dh.SeikyusakiName = RadComboBox3.Text;
                    }
                    if (RadComboBox2.Text != "")
                    {
                        dl.TyokusousakiMei = RadComboBox2.Text;
                        dh.TyokusosakiName = RadComboBox2.Text;
                    }
                    if (FacilityRad.Text != "")
                    {
                        dl.FacilityName = FacilityRad.Text;
                        dh.FacilityName = FacilityRad.Text;
                    }
                    if (Tantou.Text != "")
                    {
                        dl.StaffName = Tantou.Text;
                        dh.TantoName = Tantou.Text;
                    }
                    dl.Relay = "発注済";
                    if (RadComboBox4.Text != "")
                    {
                        dl.Department = RadComboBox4.Text;
                        dh.Bumon = RadComboBox4.Text;
                    }
                    if (RadZeiKubun.Text != "")
                    {
                        dl.Zeikubun = RadZeiKubun.Text;
                        dh.ZeiKubun = RadZeiKubun.Text;

                    }
                    if (CheckBox1.Checked == true)
                    {
                        dl.KariFLG = CheckBox1.Checked.ToString();
                        dh.KariFLG = CheckBox1.Checked.ToString();
                    }

                    if (Label3.Text != "")
                    {
                        dl.Kakeritsu = Label3.Text;
                        dh.Kakeritsu = Label3.Text;
                    }
                    if (TbxKake.Text != "")
                    {
                        dl.Kakeritsu = TbxKake.Text;
                        dh.Kakeritsu = TbxKake.Text;
                    }
                    dl.CreateDate = RadDatePicker2.SelectedDate.Value;
                    dh.CareateDate = DateTime.Now;
                    dh.SouSuryou = int.Parse(TextBox10.Text);
                    string mno = SessionManager.HACCYU_NO;
                    //DataMitumori.T_MitumoriHeaderDataTable db = ClassMitumori.GetMitumoriHeader(mno, Global.GetConnection());
                    DataSet1.T_OrderedHeaderRow db = ClassOrdered.GetMaxOrdered(Global.GetConnection());
                    string arrr = db.OrderedNo.ToString();
                    //受注データに挿入
                    string arr = mno.ToString().Substring(1, 7);
                    string sub = arrr.Substring(1, 7);
                    int su = int.Parse(sub);
                    int max = su + 1;

                    //string no = "3" + arr;
                    string no = "3" + max;
                    dl.OrderedNo = int.Parse(no);
                    string[] strAry = mno.Split('_');
                    dh.JutyuNo = int.Parse(strAry[0]);
                    //ClassOrdered.InsertOrderedHeader(dp, Global.GetConnection());
                    dh.Relay = "発注済";
                    dh.HatyuFlg = "true";
                    dh.Shimebi = Shimebi.Text;
                    dw.AddT_JutyuHeaderRow(dh);
                    ClassJutyu.UpDateJutyuHeader(strAry[0], dw, Global.GetConnection());

                    DataSet1.T_OrderedDataTable dg = new DataSet1.T_OrderedDataTable();
                    DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu(Global.GetConnection());
                    string cate = "";
                    string shiire = "";

                    DataSet1.T_OrderedRow dr = dg.NewT_OrderedRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    //TextBox MakerHinban = (TextBox)CtlMitsuSyosai.FindControl("MakerHinban");
                    //RadComboBox ProductName = (RadComboBox)CtlMitsuSyosai.FindControl("ProductName");
                    //RadComboBox Hanni = (RadComboBox)CtlMitsuSyosai.FindControl("Hanni");
                    RadComboBox Zaseki = (RadComboBox)CtlMitsuSyosai.FindControl("Zasu");
                    Label Media = (Label)CtlMitsuSyosai.FindControl("Baitai");
                    TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
                    TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
                    RadComboBox ShiyouShisetsu = (RadComboBox)CtlMitsuSyosai.FindControl("ShiyouShisetsu");
                    RadDatePicker StartDate = (RadDatePicker)CtlMitsuSyosai.FindControl("StartDate");
                    RadDatePicker EndDate = (RadDatePicker)CtlMitsuSyosai.FindControl("EndDate");
                    TextBox Tanka = (TextBox)CtlMitsuSyosai.FindControl("Tanka");
                    TextBox Uriage = (TextBox)CtlMitsuSyosai.FindControl("Uriage");
                    RadComboBox Hachu = (RadComboBox)CtlMitsuSyosai.FindControl("Hachu");
                    TextBox ShiireTanka = (TextBox)CtlMitsuSyosai.FindControl("ShiireTanka");
                    TextBox ShiireKingaku = (TextBox)CtlMitsuSyosai.FindControl("ShiireKingaku");
                    HtmlInputHidden FacilityCode = (HtmlInputHidden)CtlMitsuSyosai.FindControl("FacilityCode");
                    HtmlInputHidden procd = (HtmlInputHidden)CtlMitsuSyosai.FindControl("procd");
                    Label Facility = (Label)CtlMitsuSyosai.FindControl("Facility");
                    Label end = (Label)CtlMitsuSyosai.FindControl("Label12");
                    Label start = (Label)CtlMitsuSyosai.FindControl("Label11");
                    Label RowNo = CtrlSyousai.Items[i].FindControl("RowNo") as Label;
                    RadComboBox Tekiyou = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                    TextBox Suryo = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
                    HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
                    HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
                    HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
                    HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
                    HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
                    HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
                    TextBox KariFaci = (TextBox)CtlMitsuSyosai.FindControl("KariFaci");
                    HtmlInputHidden FacAdd = (HtmlInputHidden)CtlMitsuSyosai.FindControl("FacilityAddress");
                    Label WareHouse = (Label)CtlMitsuSyosai.FindControl("WareHouse");
                    Label kakeri = (Label)CtlMitsuSyosai.FindControl("Kakeri");
                    Label Lblproduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    Label LblHanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");


                    dr.HatyuDay = DateTime.Now.ToString();
                    dr.JutyuSuryou = int.Parse(Suryo.Text);
                    dr.Zansu = Suryo.Text;
                    dl.OrderedAmount = int.Parse(Suryo.Text);
                    dr.StaffName = Tantou.Text;
                    dr.RowNo = i + 1;

                    if (RadZeiKubun.Text != "")
                    {
                        dr.Zeikubun = RadZeiKubun.Text;
                    }

                    if (Hachu.Text != "")
                    {
                        dr.ShiireSakiName = Hachu.Text;
                        dl.ShiiresakiName = Hachu.Text;
                        DataMaster.M_ShiiresakiDataTable drr = ClassMaster.GetShiiresaki(Hachu.Text, Global.GetConnection());
                        dr.ShiiresakiCode = drr[0].ShiiresakiCode.ToString();
                        dl.ShiiresakiCode = drr[0].ShiiresakiCode.ToString();
                    }
                    if (kakeri.Text != "")
                    {
                        dr.Kakeritsu = kakeri.Text;
                    }
                    if (Lblproduct.Text != "")
                    {
                        dr.MekerNo = Lblproduct.Text;
                    }
                    if (SerchProduct.Text != "")
                    {
                        dr.ProductName = SerchProduct.Text;
                        cate = RadComboCategory.Text;
                        string hanni = LblHanni.Text;
                        //dr.SyouhinCode = arr[1];
                        DataSet1.M_Kakaku_2DataTable dk = Class1.getproduct(Lblproduct.Text, cate.Trim(), hanni, Global.GetConnection());
                        dr.ProductCode = int.Parse(dk[0].SyouhinCode);
                        dr.ShiiresakiCode = dk[0].ShiireCode;
                        dr.ShiireSakiName = dk[0].ShiireName;
                    }
                    if (LblHanni.Text != "")
                    {
                        dr.Range = LblHanni.Text;
                    }
                    if (Zaseki.Text != "")
                    {
                        dr.Zasu = Zaseki.Text;
                    }
                    if (Media.Text != "")
                    {
                        dr.Media = Media.Text;
                    }
                    if (HyoujyunTanka.Text != "")
                    {
                        string hyoutan = HyoujyunTanka.Text;
                        string r1 = hyoutan.Replace(",", "");
                        dr.HyoujyunKakaku = int.Parse(r1);
                    }
                    if (Kingaku.Text != "")
                    {
                        string kin = Kingaku.Text;
                        string r4 = kin.Replace(",", "");
                        dr.Ryoukin = r4;
                        dr.JutyuGokei = int.Parse(r4);
                    }
                    if (RadComboBox2.Text != "")
                    {
                        dr.TyokusousakiMei = TokuisakiMei.Value;
                        // dr.TyokusousakiCD = int.Parse(TyokusoCode.Value);
                    }
                    if (CheckBox4.Checked == true)
                    {
                        dr.SiyouKaishi = start.Text;
                        //dr.SiyouiOwari = end.Text;
                    }
                    if (CheckBox4.Checked == false)
                    {
                        if (StartDate.SelectedDate != null)
                        {
                            dr.SiyouKaishi = StartDate.SelectedDate.Value.ToShortDateString();
                        }
                        if (EndDate.SelectedDate != null)
                        {
                            dr.SiyouiOwari = EndDate.SelectedDate.Value.ToShortDateString();
                        }
                    }
                    if (Tanka.Text != "")
                    {
                        string tan = Tanka.Text;
                        string r1 = tan.Replace(",", "");
                        dr.JutyuTanka = int.Parse(r1);
                    }
                    if (Uriage.Text != "")
                    {
                        string uri = Uriage.Text;
                        string r1 = uri.Replace(",", "");
                        dr.JutyuGokei = int.Parse(r1);
                    }

                    if (ShiireTanka.Text != "")
                    {
                        string shitan = ShiireTanka.Text;
                        string r2 = shitan.Replace(",", "");
                        dr.ShiireTanka = int.Parse(r2);
                    }
                    if (ShiireKingaku.Text != "")
                    {
                        string shikin = ShiireKingaku.Text;
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

                    if (CheckBox5.Checked == true)
                    {
                        dr.FukusuFaci = "true";
                        dr.FacilityName = Facility.Text;
                        string fac = Facility.Text;

                        DataSet1.M_Facility_NewDataTable de = Class1.FacilityDT(fac, Global.GetConnection());

                        for (int d = 0; d < de.Count; d++)
                        {
                            FacilityAddress.Value = de[d].Address1 + de[d].Address2;
                            dr.FacilityCode = de[d].FacilityNo;
                            dr.TyokusousakiMei = Facility.Text;
                            dr.FacilityJusyo1 = FacilityAddress.Value;
                        }
                    }
                    else
                    {
                        if (CheckBox1.Checked == true)
                        {
                            dr.FacilityName = KariFaci.Text;
                            dr.FacilityCode = int.Parse(FacilityCode.Value);
                            dr.FacilityJusyo1 = FacAdd.Value;
                            dr.TokuisakiMei = KariTokui.Text;
                        }
                        else
                        {
                            if (RadComboBox1.Text != "")
                            {
                                dr.TokuisakiMei = TokuisakiMei.Value;
                                dr.TokuisakiCode = int.Parse(TokuisakiCode.Value);
                            }

                            if (ShiyouShisetsu.Text != "")
                            {
                                dr.FacilityName = ShiyouShisetsu.Text;
                                string fac = ShiyouShisetsu.Text;
                                DataSet1.M_Facility_NewDataTable de = Class1.FacilityDT(fac, Global.GetConnection());
                                for (int d = 0; d < de.Count; d++)
                                {
                                    FacilityAddress.Value = de[d].Address1 + de[d].Address2;
                                    FacilityCode.Value = de[d].FacilityNo.ToString();
                                }
                                dr.FacilityJusyo1 = FacilityAddress.Value;
                                dr.FacilityCode = int.Parse(FacilityCode.Value);
                            }
                        }
                    }
                    if (Tantou.Text != "")
                    {
                        dr.StaffName = Tantou.Text;
                    }
                    if (RadComboBox4.Text != "")
                    {
                        dr.Department = RadComboBox4.Text;
                    }
                    if (Tekiyou.Text != "")
                    {
                        dr.Tekiyou1 = Tekiyou.Text;
                    }

                    if (TextBox10.Text != "")
                    {
                        dr.JutyuSuryou = int.Parse(Suryo.Text);
                    }
                    if (FacilityAddress.Value != "")
                    {
                        dr.FacilityJusyo1 = FacilityAddress.Value;
                    }
                    if (WareHouse.Text != "")
                    {
                        dr.WareHouse = WareHouse.Text;
                    }

                    dr.OrderedNo = int.Parse(no);
                    dr.UriageFlg = false;
                    //dg.AddT_OrderedRow(dr);
                    dp.AddT_OrderedHeaderRow(dl);
                    ClassOrdered.UpdateOrderedHeader(dp, dr, cate, shiire, Global.GetConnection());
                    dp = null;
                }
                //ClassOrdered.InsertOrdered(dg, Global.GetConnection());

            }
            catch
            {
                Err.Text = "データを登録することができませんでした。";
            }
            if (Err.Text == "")
            {
                End.Text = "データを登録しました。";
            }
            DataUriage.T_UriageHeaderRow dru = ClassUriage.GetMaxNo(Global.GetConnection());
            string jNo = JutyuNo.Text;
            int noo = dru.UriageNo;
            int Uno = noo + 1;
            ClassUriage.InsertUriageHeader(jNo, Uno, Global.GetConnection());
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

    }
}
