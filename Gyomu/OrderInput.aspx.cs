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

        protected void Page_Load(object sender, EventArgs e)
        {
            KariNouhin.Visible = false;

            TextBox10.Text = "0";
            Uriagekeijyou.Text = "0";
            Shiirekei.Text = "0";
            TextBox6.Text = "0";
            Zei.Text = "0";
            UriageGokei.Text = "0";
            TextBox13.Text = "0";
            Err.Text = "";
            End.Text = "";
            NouhinsakiPanel.Visible = false;
            TokuisakiSyousai.Visible = false;
            mInput2.Visible = true;
            CtrlSyousai.Visible = true;
            AddBtn.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Visible = false;

            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {

            }
            if (!IsPostBack)
            {
                RadDatePicker1.SelectedDate = DateTime.Now;
                RadDatePicker2.SelectedDate = DateTime.Now;
                RadDatePicker3.SelectedDate = DateTime.Now;
                Tantou.Text = SessionManager.User.M_user.UserName;
                string code = SessionManager.User.M_user.UserID;
                ListSet.SetBumon2(code, RadComboBox4);
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

            string a = RadComboBox1.Text;
            DataSet1.M_Tokuisaki2DataTable dd = Class1.GetTokuisakiName(a, Global.GetConnection());
            for (int i = 0; i < dd.Count; i++)
            {
                TokuisakiMei.Value = dd[i].TokuisakiName1;
            }
            if (CheckBox5.Checked == true)
            {
                AllFukusu();
            }
            else
            {
                kari();
            }
            if (CtrlSyousai.Items.Count >= 1)
            {
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");

                    bool bo = true;
                    bool boo = true;

                    if (SerchProduct.Text != "")
                    {
                        bo = CtlMitsuSyosai.FocusRoute();
                    }
                    if (bo)
                    { }
                    else
                    {
                        AddBtn.Focus();
                    }
                    if (boo)
                    { }
                    else
                    {
                        AddBtn.Focus();
                    }
                }
            }
        }

        internal static void Create2(DataSet1.T_KariOrderedDataTable dt)
        {
            throw new NotImplementedException();
        }

        private void Create2()
        {
            string MNo = SessionManager.HACCYU_NO;
            string[] strAry = MNo.Split('_');
            string MiNo = strAry[0];
            DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu3(MiNo, Global.GetConnection());
            this.CtrlSyousai.DataSource = dt;
            this.CtrlSyousai.DataBind();
        }

        public virtual bool AutoScroll { get; set; }

        private void kari()
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                TextBox KariFaci = (TextBox)CtlMitsuSyosai.FindControl("KariFaci");
                RadComboBox ShiyouShisetsu = (RadComboBox)CtlMitsuSyosai.FindControl("ShiyouShisetsu") as RadComboBox;
                bool ckb = CheckBox1.Checked;
                if (ckb)
                {
                    ShiyouShisetsu.Visible = false;
                    KariFaci.Visible = true;
                }
                else
                {
                    ShiyouShisetsu.Visible = true;
                    KariFaci.Visible = false;
                }

                //CtlMitsuSyosai.kari(ckb);
            }
        }

        //private void Keisan()
        //{
        //    try
        //    {
        //        TextBox10.Text = "0";
        //        TextBox7.Text = "0";
        //        TextBox8.Text = "0";
        //        TextBox6.Text = "0";
        //        TextBox5.Text = "0";
        //        TextBox12.Text = "0";
        //        urikei.Value = "0";
        //        shikei.Value = "0";

        //        for (int i = 0; i < CtrlSyousai.Items.Count; i++)
        //        {
        //            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
        //            TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
        //            TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
        //            TextBox ShiireTanka = (TextBox)CtlMitsuSyosai.FindControl("ShiireTanka");
        //            TextBox ShiireKingaku = (TextBox)CtlMitsuSyosai.FindControl("ShiireKingaku");
        //            TextBox Tanka = (TextBox)CtlMitsuSyosai.FindControl("Tanka");
        //            TextBox Uriage = (TextBox)CtlMitsuSyosai.FindControl("Uriage");
        //            TextBox Suryou = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
        //            HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
        //            HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
        //            HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
        //            HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
        //            HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
        //            HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
        //            HtmlInputHidden zeiht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeiht");
        //            HtmlInputHidden zeist = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeist");

        //            if (RadZeiKubun.Text == "税込")
        //            {
        //                string s = RadZeiKubun.Text;
        //                CtlMitsuSyosai.Zeikei(s);
        //                TextBox12.Visible = false;
        //                int su = int.Parse(Suryou.Text);
        //                if (HyoujyunTanka.Text != "")
        //                {
        //                    if (Kingaku.Text != "")
        //                    {
        //                        //コントロールの計算
        //                        int kake = int.Parse(Label3.Text);
        //                        string hyoutan = HyoujyunTanka.Text;
        //                        string r1 = hyoutan.Replace(",", "");
        //                        int Ctlht = int.Parse(r1);
        //                        double Ctlht2 = Ctlht * kake / 100;
        //                        Tanka.Text = Ctlht2.ToString("0,0");
        //                        string shitan = ShiireTanka.Text;
        //                        string r2 = shitan.Replace(",", "");
        //                        int Ctlst = int.Parse(r2);

        //                        //ヘッダの計算
        //                        string u = TextBox7.Text;
        //                        string r3 = u.Replace(",", "");
        //                        int x = int.Parse(r3);
        //                        string shi = TextBox8.Text;
        //                        string r4 = shi.Replace(",", "");
        //                        int y = int.Parse(r4);

        //                        double urikeiForm = x + Ctlht2;
        //                        int shikeiForm = y + Ctlst;

        //                        //売上計の入力
        //                        TextBox7.Text = urikeiForm.ToString("0,0");
        //                        urikei.Value = urikeiForm.ToString();
        //                        //仕入計の入力
        //                        TextBox8.Text = shikeiForm.ToString("0,0");
        //                        shikei.Value = shikeiForm.ToString();
        //                        //粗利計の入力
        //                        double ararikei = urikeiForm - shikeiForm;
        //                        TextBox6.Text = ararikei.ToString("0,0");
        //                        arakei.Value = ararikei.ToString();
        //                        //消費税額の入力
        //                        double syouhi = urikeiForm / 1.1;
        //                        double zei = syouhi * 0.1;
        //                        TextBox5.Text = zei.ToString("0,0");
        //                        zeikei.Value = zei.ToString();
        //                        //  利益率の入力
        //                        double riekiper = ararikei * 100 / urikeiForm;
        //                        TextBox13.Text = riekiper.ToString();
        //                        //数量計算
        //                        int h = int.Parse(TextBox10.Text);
        //                        int j = int.Parse(Suryou.Text);
        //                        int k = h + j;
        //                        TextBox10.Text = k.ToString();
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                if (HyoujyunTanka.Text != "")
        //                {
        //                    string s = RadZeiKubun.Text;
        //                    CtlMitsuSyosai.Zeikei(s);
        //                    TextBox12.Visible = true;

        //                    int kak = 0;
        //                    if (Label3.Text != "")
        //                    {
        //                        int kake = int.Parse(Label3.Text);
        //                        kak += kake;
        //                    }

        //                    string hyou = HyoujyunTanka.Text;
        //                    string r5 = hyou.Replace(",", "");
        //                    int Htanka = int.Parse(r5);
        //                    int Htanka2 = Htanka * kak / 100;
        //                    Tanka.Text = Htanka2.ToString("0,0");

        //                    string shii = ShiireTanka.Text;
        //                    string r6 = shii.Replace(",", "");
        //                    int Stanka = int.Parse(r6);
        //                    int suryo = int.Parse(Suryou.Text);

        //                    if (Tanka.Text != "")
        //                    {
        //                        string tkV = Tanka.Text;
        //                        string r1 = tkV.Replace(",", "");
        //                        int tanka = int.Parse(r1);
        //                        Uriage.Text = (tanka * suryo).ToString("0,0");
        //                        ug.Value = (tanka * suryo).ToString();
        //                    }

        //                    Kingaku.Text = (Htanka2 * suryo).ToString("0,0");
        //                    kgk.Value = (Htanka2 * suryo).ToString();

        //                    ShiireKingaku.Text = (Stanka * suryo).ToString("0,0");
        //                    sk.Value = (Stanka * suryo).ToString();

        //                    string u = TextBox7.Text;
        //                    string r7 = u.Replace(",", "");
        //                    int a = int.Parse(r7);
        //                    int UriageKei = a + (Htanka2 * suryo);
        //                    TextBox5.Text = (UriageKei * 0.1).ToString("0,0");
        //                    zeikei.Value = TextBox5.Text;
        //                    TextBox12.Text = (UriageKei * 1.1).ToString("0,0");
        //                    soukei.Value = TextBox12.Text;
        //                    int b = int.Parse(TextBox10.Text);
        //                    int Suryokei = b + suryo;
        //                    TextBox10.Text = Suryokei.ToString();
        //                    string f = TextBox8.Text;
        //                    string r8 = f.Replace(",", "");
        //                    int c = int.Parse(r8);
        //                    int ShiireKei = c + (Stanka * suryo);
        //                    TextBox8.Text = ShiireKei.ToString("0,0");
        //                    shikei.Value = ShiireKei.ToString();
        //                    TextBox6.Text = (UriageKei - ShiireKei).ToString("0,0");
        //                    arakei.Value = (UriageKei - ShiireKei).ToString();
        //                    int rieki = (UriageKei - ShiireKei) * 100 / UriageKei;
        //                    TextBox13.Text = rieki.ToString();
        //                    TextBox7.Text = UriageKei.ToString("0,0");
        //                    urikei.Value = UriageKei.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Err.Text = ex.ToString();
        //    }
        //}

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

        private void Category()
        {
            //ListSet.SetCategory(RadComboCategory);
            CategoryCode.Value = RadComboCategory.SelectedValue;
            string ddl = RadComboCategory.Text;

            if (RadComboCategory.SelectedValue != "")
            {
                CategoryCode.Value = RadComboCategory.SelectedValue;
            }
            else
            {

            }
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                bool s = CheckBox4.Checked;

                //CtlMitsuSyosai.Test2(ddl, s);


                if (ddl == "公共図書館")
                {
                    DropDownList9.Visible = false;
                    RadDatePicker3.Visible = false;
                    RadDatePicker4.Visible = false;
                    CheckBox4.Visible = false;
                }

                if (ddl == "学校図書館")
                {
                    DropDownList9.Visible = false;
                    RadDatePicker3.Visible = false;
                    RadDatePicker4.Visible = false;
                    CheckBox4.Visible = false;
                }

                if (ddl == "その他図書館")
                {
                    DropDownList9.Visible = false;
                    RadDatePicker3.Visible = false;
                    RadDatePicker4.Visible = false;
                    CheckBox4.Visible = false;
                }

                if (ddl == "防衛省")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "ホテル")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }


                if (ddl == "レジャーホテル")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "バス")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "船舶")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }
                if (ddl == "上映会")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "複合カフェ")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "健康ランド")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "福祉施設")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;
                }

                if (ddl == "キッズ・BGV")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }


                if (ddl == "業務用")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "VOD")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "VOD配信")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;

                }

                if (ddl == "DOD")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;
                }
                if (ddl == "DEX")
                {
                    DropDownList9.Visible = true;
                    RadDatePicker3.Visible = true;
                    RadDatePicker4.Visible = true;
                }
            }

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
            AddBtn.Visible = false;
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
            AddBtn.Visible = false;
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

        protected void Button10_Click(object sender, EventArgs e)
        {
            TokuisakiSyousai.Visible = false;
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
                string[] tokuisaki = RadComboBox1.SelectedValue.Split('/');
                CustomerCode.Value = tokuisaki[0];
                TokuisakiCode.Value = tokuisaki[1];
                RadComboBox3.Text = RadComboBox1.Text;
                string code = "";
                ListSet.SetBumon2(code, RadComboBox4);
                DataSet1.M_Tokuisaki2DataTable dt = Class1.GetTokuisaki2(tokuisaki[0], tokuisaki[1], Global.GetConnection());
                for (int j = 0; j < CtrlSyousai.Items.Count; j++)
                {
                    DataSet1.M_Tokuisaki2Row dr = dt.Rows[0] as DataSet1.M_Tokuisaki2Row;
                    CtrlMitsuSyousai Ctl = CtrlSyousai.Items[j].FindControl("Syosai") as CtrlMitsuSyousai;
                    Label Kakeri = (Label)Ctl.FindControl("Kakeri");
                    Label zeiku = (Label)Ctl.FindControl("zeiku");

                    if (!dr.IsTantoStaffCodeNull())
                    {
                        string Hcode = dr.TantoStaffCode;
                        ListSet.SetBumon2(Hcode, RadComboBox4);
                        DataMaster.M_Tanto1Row drTanto = ClassMaster.GetTantoRow2(Hcode, Global.GetConnection());
                        Tantou.Text = drTanto.UserName;
                        RadComboBox5.Text = drTanto.UserName;
                    }

                    if (!dr.IsZeikubunNull())
                    {
                        RadZeiKubun.SelectedValue = dr.Zeikubun.Trim();
                        RcbTax.SelectedValue = dr.Zeikubun.Trim();
                        zeiku.Text = dr.Zeikubun;
                    }

                    if (!dr.IsKakeritsuNull())
                    {
                        Label3.Text = dr.Kakeritsu.ToString();
                        Kakeri.Text = dr.Kakeritsu.ToString();
                        TbxKakeritsu.Text = dr.Kakeritsu.Trim();
                    }

                    if (!dr.IsShimebiNull())
                    {
                        Shimebi.Text = dr.Shimebi;
                        RcbShimebi.SelectedValue = dr.Shimebi.Trim();
                    }

                    TbxCustomer.Text = dr.CustomerCode;
                    TbxTokuisakiCode.Text = dr.TokuisakiCode.ToString();
                    TbxTokuisakiName.Text = dr.TokuisakiName1;
                    if (!dr.IsTokuisakiFurifanaNull())
                    {
                        TbxTokuisakiFurigana.Text = dr.TokuisakiFurifana;
                    }
                    if (!dr.IsTokuisakiPostNoNull())
                    {
                        TbxTokuisakiPostNo.Text = dr.TokuisakiPostNo;
                    }
                    if (!dr.IsTokuisakiAddress1Null())
                    {
                        TbxTokuisakiAddress.Text = dr.TokuisakiAddress1;
                    }
                    if (!dr.IsTokuisakiTELNull())
                    {
                        TbxTokuisakiTEL.Text = dr.TokuisakiTEL;
                    }
                    if (!dr.IsTokuisakiRyakusyoNull())
                    {
                        TbxTokuisakiRyakusyo.Text = dr.TokuisakiRyakusyo;
                    }
                    if (!dr.IsTokuisakiFAXNull())
                    {
                        TbxTokuisakiFax.Text = dr.TokuisakiFAX;
                    }
                    if (!dr.IsTokuisakiStaffNull())
                    {
                        TbxTokuisakiStaff.Text = dr.TokuisakiStaff;
                    }
                    if (!dr.IsTokuisakiDepartmentNull())
                    {
                        TbxTokuisakiDepartment.Text = dr.TokuisakiDepartment;
                    }
                    if (!dr.IsKakeritsuNull())
                    {
                        TbxKakeritsu.Text = dr.Kakeritsu;
                    }
                    if (!dr.IsTantoStaffCodeNull())
                    {
                        LblTantoStaffCode.Text = dr.TantoStaffCode;
                        string tsc = dr.TantoStaffCode;
                        DataMaster.M_Tanto1Row drT = ClassMaster.GetTantoRow2(tsc, Global.GetConnection());
                        RadComboBox5.Text = drT.UserName;
                    }
                }
            }
            catch (Exception ex)
            {
                Err.Text = "得意先を選択してください。";
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
            Category();
            CategoryCode.Value = RadComboCategory.SelectedValue;
            string a = CategoryCode.Value;
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                CtlMitsuSyosai.Test4(a);
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
                        DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                        DataJutyu.T_JutyuRow df = (e.Item.DataItem as DataRowView).Row as DataJutyu.T_JutyuRow;
                        CtrlMitsuSyousai Ctl = e.Item.FindControl("Syosai") as CtrlMitsuSyousai;
                        RadComboBox Zaseki = (RadComboBox)Ctl.FindControl("Zasu");
                        Label Media = (Label)Ctl.FindControl("Baitai");
                        TextBox HyoujyunTanka = (TextBox)Ctl.FindControl("HyoujyunTanka");
                        TextBox Kingaku = (TextBox)Ctl.FindControl("Kingaku");
                        RadComboBox ShiyouShisetsu = (RadComboBox)Ctl.FindControl("ShiyouShisetsu");
                        RadDatePicker StartDate = (RadDatePicker)Ctl.FindControl("StartDate");
                        RadDatePicker EndDate = (RadDatePicker)Ctl.FindControl("EndDate");
                        TextBox Tanka = (TextBox)Ctl.FindControl("Tanka");
                        TextBox Uriage = (TextBox)Ctl.FindControl("Uriage");
                        RadComboBox Hachu = (RadComboBox)Ctl.FindControl("Hachu");
                        TextBox ShiireTanka = (TextBox)Ctl.FindControl("ShiireTanka");
                        TextBox ShiireKingaku = (TextBox)Ctl.FindControl("ShiireKingaku");
                        HtmlInputHidden ShisetsuCode = (HtmlInputHidden)Ctl.FindControl("Shisetsu");
                        HtmlInputHidden procd = (HtmlInputHidden)Ctl.FindControl("procd");
                        HtmlInputHidden FacilityCode = (HtmlInputHidden)Ctl.FindControl("FacilityCode");
                        Label Facility = (Label)Ctl.FindControl("Facility");
                        Label end = (Label)Ctl.FindControl("Label12");
                        Label start = (Label)Ctl.FindControl("Label11");
                        TextBox Suryo = (TextBox)Ctl.FindControl("Suryo");
                        HtmlInputHidden ht = (HtmlInputHidden)Ctl.FindControl("ht");
                        HtmlInputHidden st = (HtmlInputHidden)Ctl.FindControl("st");
                        HtmlInputHidden tk = (HtmlInputHidden)Ctl.FindControl("tk");
                        HtmlInputHidden ug = (HtmlInputHidden)Ctl.FindControl("ug");
                        HtmlInputHidden kgk = (HtmlInputHidden)Ctl.FindControl("kgk");
                        HtmlInputHidden sk = (HtmlInputHidden)Ctl.FindControl("sk");
                        HtmlInputHidden zht = (HtmlInputHidden)Ctl.FindControl("zeiht");
                        HtmlInputHidden zst = (HtmlInputHidden)Ctl.FindControl("zeist");
                        Label WareHouse = (Label)Ctl.FindControl("WareHouse");
                        TextBox KariFaci = (TextBox)Ctl.FindControl("KariFaci");
                        Label kakeri = (Label)Ctl.FindControl("Kakeri");
                        Label zeiku = (Label)Ctl.FindControl("zeiku");
                        Label Lblproduct = (Label)Ctl.FindControl("LblProduct");
                        RadComboBox SerchProduct = (RadComboBox)Ctl.FindControl("SerchProduct");
                        Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                        RadComboBox Tekiyo = (RadComboBox)Ctl.FindControl("Tekiyo");
                        TextBox TbxFacilityCode = (TextBox)Ctl.FindControl("TbxFacilityCode");
                        TextBox TbxFacilityName = (TextBox)Ctl.FindControl("TbxFacilityName");
                        TextBox TbxFaciAdress = (TextBox)Ctl.FindControl("TbxFaciAdress");
                        TextBox TbxYubin = (TextBox)Ctl.FindControl("TbxYubin");
                        RadComboBox RcbCity = (RadComboBox)Ctl.FindControl("RcbCity");
                        TextBox TbxFacilityResponsible = (TextBox)Ctl.FindControl("TbxFacilityResponsible");
                        TextBox TbxFacilityName2 = (TextBox)Ctl.FindControl("TbxFacilityName2");
                        TextBox TbxFaci = (TextBox)Ctl.FindControl("TbxFaci");
                        TextBox TbxTel = (TextBox)Ctl.FindControl("TbxTel");
                        RadComboBox RcbHanni = (RadComboBox)Ctl.FindControl("RcbHanni");
                        TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
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
                            //Ctl.CateCre();
                            JutyuNo.Text = MiNo;
                            //DataMitumori.T_MitumoriRow dl = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_MitumoriRow;
                            DataJutyu.T_JutyuHeaderDataTable dd = ClassJutyu.GetJutyuHeader1(MiNo, Global.GetConnection());
                            for (int i = 0; i < dd.Count; i++)
                            {
                                if (!dd[i].IsFacilityNameNull())
                                {
                                    FacilityRad.Text = dd[i].FacilityName;
                                }

                                if (!dd[i].IsCategoryNameNull())
                                {
                                    RadComboCategory.Text = dd[i].CategoryName;
                                    RadComboCategory.SelectedValue = dd[i].CateGory.ToString();
                                }
                                if (!dd[i].IsZeiKubunNull())
                                {
                                    if (dd[i].ZeiKubun == "税込")
                                    {
                                        UriageGokei.Visible = false;
                                        RadZeiKubun.SelectedItem.Text = dd[i].ZeiKubun;
                                        zeiku.Text = dd[i].ZeiKubun;
                                    }
                                    else
                                    {
                                        UriageGokei.Visible = true;
                                        RadZeiKubun.SelectedItem.Text = dd[i].ZeiKubun;
                                        zeiku.Text = dd[i].ZeiKubun;
                                    }
                                }
                                if (!dd[i].IsKakeritsuNull())
                                {
                                    if (!dd[i].IsKariFLGNull())
                                    {
                                        if (dd[i].KariFLG.Trim() == "True")
                                        {
                                            CheckBox1.Checked = true;
                                            TbxKake.Text = dd[i].Kakeritsu;
                                            kakeri.Text = dd[i].Kakeritsu;
                                        }
                                    }
                                    else
                                    {
                                        Label3.Text = dd[i].Kakeritsu;
                                        kakeri.Text = dd[i].Kakeritsu;
                                    }
                                }

                                if (!dd[i].IsBikouNull())
                                {
                                    TextBox2.Text = dd[i].Bikou;
                                }

                                if (!dd[i].IsSiyouKaisiNull())
                                {
                                    RadDatePicker3.SelectedDate = dd[i].SiyouKaisi;
                                }

                                if (!dd[i].IsSiyouOwariNull())
                                {
                                    RadDatePicker4.SelectedDate = dd[i].SiyouOwari;
                                }

                                if (!dd[i].IsGokeiKingakuNull())
                                {
                                    Uriagekeijyou.Text = dd[i].GokeiKingaku.ToString("0,0");
                                    urikei.Value = dd[i].GokeiKingaku.ToString();
                                }
                                if (!dd[i].IsShiireKingakuNull())
                                {
                                    Shiirekei.Text = dd[i].ShiireKingaku.ToString("0,0");
                                    shikei.Value = dd[i].ShiireKingaku.ToString();
                                }
                                if (!dd[i].IsSyohiZeiGokeiNull())
                                {
                                    Zei.Text = dd[i].SyohiZeiGokei.ToString("0,0");
                                }
                                if (!dd[i].IsSoukeiGakuNull())
                                {
                                    UriageGokei.Text = dd[i].SoukeiGaku.ToString("0,0");
                                    soukei.Value = dd[i].SoukeiGaku.ToString();
                                }
                                if (!dd[i].IsArariGokeigakuNull())
                                {
                                    TextBox6.Text = dd[i].ArariGokeigaku.ToString("0,0");
                                    arakei.Value = dd[i].ArariGokeigaku.ToString();
                                }
                                if (!dd[i].IsSouSuryouNull())
                                {
                                    TextBox10.Text = dd[i].SouSuryou.ToString();
                                }
                                if (!dd[i].IsCateGoryNull())
                                {
                                    CategoryCode.Value = dd[i].CateGory.ToString();
                                }
                                if (!dd[i].IsZasuNull())
                                {
                                    Zaseki.SelectedItem.Text = dd[i].Zasu;
                                }
                                if (!dd[i].IsTokuisakiNameNull())
                                {
                                    if (CheckBox1.Checked == true)
                                    {
                                        KariTokui.Text = dd[i].TokuisakiName;
                                        kariSekyu.Text = dd[i].TokuisakiName;
                                        string[] toku = dd[i].TokuisakiCode.Split('/');
                                        DataSet1.M_Tokuisaki2DataTable dg = Class1.GetTokuisaki2(toku[0], toku[1], Global.GetConnection());
                                        for (int j = 0; j < dg.Count; j++)
                                        {
                                            TokuisakiCode.Value = dg[j].CustomerCode + "/" + dg[j].TokuisakiCode;
                                        }
                                    }
                                    else
                                    {
                                        RadComboBox1.Text = dd[i].TokuisakiName;
                                        RadComboBox3.Text = dd[i].TokuisakiName;
                                        string un = Tantou.Text = dd[i].TantoName;
                                        DataSet1.M_TantoDataTable dtU = Class1.GetStaff2(un, Global.GetConnection());
                                        for (int u = 0; u < dtU.Count; u++)
                                        {
                                            RadComboBox4.Items.Add(new RadComboBoxItem(dtU[u].BumonName, dtU[u].Bumon.ToString()));
                                        }
                                        Label3.Text = dd[i].Kakeritsu;
                                        RadZeiKubun.SelectedValue = dd[i].ZeiKubun;
                                        Shimebi.Text = dd[i].Shimebi;
                                        string[] toku = dd[i].TokuisakiCode.Split('/');
                                        TokuisakiCode.Value = dd[i].TokuisakiCode;
                                        //DataSet1.M_Tokuisaki2DataTable dg = Class1.GetTokuisaki2(toku[0], toku[1], Global.GetConnection());
                                        //for (int j = 0; j < dg.Count; j++)
                                        //{
                                        //    TokuisakiCode.Value = dg[j].CustomerCode + "/" + dg[j].TokuisakiCode.ToString();
                                        //    TokuisakiMei.Value = dg[j].TokuisakiName1;
                                        //    Shimebi.Text = dg[j].Shimebi;
                                        //    string code = dg[j].TantoStaffCode;
                                        //    DataMaster.M_Tanto1Row drTanto = ClassMaster.GetTantoRow2(code, Global.GetConnection());
                                        //    Tantou.Text = drTanto.UserName;
                                        //    ListSet.SetBumon2(code, RadComboBox4);
                                        //    DataMaster.M_Tanto1Row drt = ClassMaster.GetTantoRow(Tantou.Text, Global.GetConnection());
                                        //    RadComboBox4.Text = drt.Busyo;
                                        //}
                                        //if (dg.Count == 0)
                                        //{
                                        //    TextBox19.Text = df.TokuisakiCode;
                                        //}
                                    }
                                }
                                if (!df.IsSisetuCodeNull())
                                {
                                    FacilityCode.Value = df.SisetuCode.ToString();
                                    TbxFacilityCode.Text = df.SisetuCode.ToString();
                                }
                                if (!df.IsSisetuMeiNull())
                                {
                                    if (CheckBox1.Checked == true)
                                    {
                                        KariFaci.Text = df.SisetuMei;
                                    }
                                    else
                                    {
                                        ShiyouShisetsu.Text = df.SisetuMei;
                                    }
                                    TbxFacilityName.Text = df.SisetuMei;
                                }

                                if (!df.IsSisetsuMei2Null())
                                {
                                    TbxFacilityName2.Text = df.SisetsuMei2;
                                }

                                if (!df.IsSisetuJusyo1Null())
                                {
                                    TbxFaciAdress.Text = df.SisetuJusyo1;
                                }

                                if (!df.IsSisetuTantoNull())
                                {
                                    TbxFacilityResponsible.Text = df.SisetuTanto;
                                }

                                if (!df.IsSisetuPostNull())
                                {
                                    TbxYubin.Text = df.SisetuPost;
                                }

                                if (!df.IsSisetuCityCodeNull())
                                {
                                    DataMaster.M_CityRow dc = ClassMaster.GetCity(df.SisetuCityCode, Global.GetConnection());
                                    RcbCity.Text = dc.CityName;
                                    RcbCity.SelectedValue = dc.CityCode.ToString();
                                }

                                if (!df.IsSisetsuAbbrevirationNull())
                                {
                                    TbxFaci.Text = df.SisetsuAbbreviration;
                                }

                                if (!df.IsSisetsuTellNull())
                                {
                                    TbxTel.Text = df.SisetsuTell;
                                }
                            }

                            //if (!df.IsBusyoNull())
                            //{
                            //    RadComboBox4.SelectedItem.Text = df.Busyo;
                            //}

                            if (!df.IsTekiyou1Null())
                            {
                                Tekiyo.Text = df.Tekiyou1;
                            }

                            if (!df.IsFukusuFaciNull())
                            {
                                CheckBox5.Checked = true;
                            }

                            if (!df.IsSyouhinMeiNull())
                            {
                                SerchProduct.Text = df.SyouhinMei;
                            }

                            if (!df.IsSyouhinCodeNull())
                            {
                                SerchProduct.SelectedValue = df.SyouhinCode;
                            }

                            if (!df.IsSiyouKaishiNull())
                            {
                                StartDate.SelectedDate = df.SiyouKaishi;
                            }
                            if (!df.IsHattyuSakiMeiNull())
                            {
                                Hachu.Text = df.HattyuSakiMei;
                            }
                            if (!df.IsSisetuCodeNull())
                            {
                                FacilityCode.Value = df.SisetuCode.ToString();
                                TbxFacilityCode.Text = df.SisetuCode.ToString();
                            }
                            if (!df.IsSisetuMeiNull())
                            {
                                if (CheckBox1.Checked == true)
                                {
                                    KariFaci.Text = df.SisetuMei;
                                }
                                else
                                {
                                    ShiyouShisetsu.Text = df.SisetuMei;
                                }
                                TbxFacilityName.Text = df.SisetuMei;
                            }

                            if (!df.IsSisetsuMei2Null())
                            {
                                TbxFacilityName2.Text = df.SisetsuMei2;
                            }

                            if (!df.IsSisetuJusyo1Null())
                            {
                                TbxFaciAdress.Text = df.SisetuJusyo1;
                            }

                            if (!df.IsSisetuTantoNull())
                            {
                                TbxFacilityResponsible.Text = df.SisetuTanto;
                            }

                            if (!df.IsSisetuPostNull())
                            {
                                TbxYubin.Text = df.SisetuPost;
                            }

                            if (!df.IsSisetuCityCodeNull())
                            {
                                DataMaster.M_CityRow dc = ClassMaster.GetCity(df.SisetuCityCode, Global.GetConnection());
                                RcbCity.Text = dc.CityName;
                                RcbCity.SelectedValue = dc.CityCode.ToString();
                            }

                            if (!df.IsSisetsuAbbrevirationNull())
                            {
                                TbxFaci.Text = df.SisetsuAbbreviration;
                            }

                            if (!df.IsSisetsuTellNull())
                            {
                                TbxTel.Text = df.SisetsuTell;
                            }
                            if (!df.IsSiyouOwariNull())
                            {
                                EndDate.SelectedDate = df.SiyouOwari;
                            }
                            if (!df.IsMekarHinbanNull())
                            {
                                Lblproduct.Text = df.MekarHinban;
                            }
                            if (!df.IsHyojunKakakuNull())
                            {
                                HyoujyunTanka.Text = df.HyojunKakaku.ToString("0,0");
                            }
                            if (!df.IsKakeritsuNull())
                            {
                                kakeri.Text = df.Kakeritsu;
                            }
                            if (!df.IsShiiresakiCodeNull() && RadComboCategory.Text == "上映会")
                            {
                                DataMaster.M_JoueiKakakuDataTable dtJ = ClassMaster.GetJouei(df.ShiiresakiCode.ToString(), df.KeitaiMei, df.Zasu, Global.GetConnection());
                                RcbHanni.Items.Clear();
                                if (dtJ.Count > 0)
                                {
                                    for (int items = 0; items < dtJ.Count; items++)
                                    {
                                        RcbHanni.Items.Add(dtJ[items].Range);
                                    }
                                    RcbHanni.SelectedItem.Text = df.Range;
                                    DataMaster.M_JoueiKakakuDataTable dtJK = ClassMaster.GetJouei2(df.ShiiresakiCode.ToString(), df.KeitaiMei, df.Zasu, df.Range, Global.GetConnection());
                                    HyoujyunTanka.Text = dtJK[0].HyoujunKakaku;
                                    Hachu.SelectedValue = df.ShiiresakiCode.ToString();
                                    Zaseki.SelectedValue = df.Zasu;
                                }
                            }
                            else
                            {
                                if (!df.IsRangeNull())
                                {
                                    LblHanni.Text = df.Range;
                                    TbxHanni.Text = df.Range;
                                }
                            }
                            if (!df.IsSisetuMeiNull())
                            {
                                if (CheckBox1.Checked == true)
                                {
                                    KariFaci.Text = df.SisetuMei;
                                }
                                else
                                {
                                    ShiyouShisetsu.Text = df.SisetuMei;
                                }
                            }

                            if (!df.IsZasuNull())
                            {
                                Zaseki.Text = df.Zasu;
                            }
                            if (!df.IsMekarHinbanNull())
                            {
                                Lblproduct.Text = df.MekarHinban;
                            }
                            if (!df.IsRyoukinNull())
                            {
                                int ryou = int.Parse(df.Ryoukin);
                                Kingaku.Text = ryou.ToString("0,0");
                            }
                            if (!df.IsHattyuTankaNull())
                            {
                                Tanka.Text = df.HattyuTanka.ToString("0,0");
                            }

                            if (!df.IsJutyuSuryouNull())
                            {
                                Suryo.Text = df.JutyuSuryou.ToString();
                            }
                            if (!df.IsJutyuTankaNull())
                            {
                                Tanka.Text = df.JutyuTanka.ToString("0,0");
                            }
                            if (!df.IsJutyuGokeiNull())
                            {
                                Uriage.Text = df.JutyuGokei.ToString("0,0");
                            }
                            if (!df.IsTokuisakiMeiNull())
                            {
                                if (CheckBox1.Checked == true)
                                {
                                    KariTokui.Text = df.TokuisakiMei;
                                    kariSekyu.Text = df.TokuisakiMei;

                                    string tok = df.TokuisakiMei;
                                    DataSet1.M_Tokuisaki2DataTable dg = Class1.GetTokuisakiName(tok, Global.GetConnection());
                                    for (int j = 0; j < dg.Count; j++)
                                    {
                                        TokuisakiCode.Value = dg[j].TokuisakiCode.ToString();
                                    }
                                }
                                RadComboBox1.Text = df.TokuisakiMei;
                                RadComboBox3.Text = df.TokuisakiMei;
                                string toku = df.TokuisakiMei;

                                DataSet1.M_Tokuisaki2DataTable dp = Class1.GetTokuisakiName(toku, Global.GetConnection());
                                for (int j = 0; j < dp.Count; j++)
                                {
                                    TokuisakiCode.Value = dp[j].TokuisakiCode.ToString();
                                }
                            }
                            if (!df.IsKeitaiMeiNull())
                            {
                                Media.Text = df.KeitaiMei;
                            }

                            if (!df.IsShiireTankaNull())
                            {
                                ShiireTanka.Text = df.ShiireTanka.ToString("0,0");
                                st.Value = df.ShiireTanka.ToString();
                            }
                            if (!df.IsShiireKingakuNull())
                            {
                                ShiireKingaku.Text = df.ShiireKingaku.ToString("0,0");
                                sk.Value = df.ShiireKingaku.ToString();
                            }
                            if (!df.IsWareHouseNull())
                            {
                                WareHouse.Text = df.WareHouse;
                            }

                        }
                        else
                        {
                            //Ctl.CateCre();

                            if (Label3.Text != "" || TbxKake.Text != "")//複写したときに掛率が必要になってくるので追加
                            {
                                if (Label3.Text != "")
                                {
                                    kakeri.Text = Label3.Text;
                                }
                                if (TbxKake.Text != "")
                                {
                                    kakeri.Text = TbxKake.Text;
                                }
                            }
                            if (RadZeiKubun.Text != "")//複写したときに税区分が必要になってくるので追加
                            {
                                zeiku.Text = RadZeiKubun.Text;
                            }
                            if (!dr.IsSiyouKaishiNull())
                            {
                                if (StartDate.Visible == true)
                                {
                                    StartDate.SelectedDate = dr.SiyouKaishi;
                                }
                                else
                                {
                                    start.Text = dr.SiyouKaishi.ToShortDateString();
                                }

                            }
                            if (!dr.IsBasyoNull())
                            {
                                if (CheckBox1.Checked)
                                {
                                    KariFaci.Text = dr.Basyo;
                                    TbxFacilityName.Text = dr.Basyo;
                                }
                                else
                                {
                                    ShiyouShisetsu.Text = dr.Basyo;
                                    TbxFacilityName.Text = dr.Basyo;
                                }
                            }
                            else
                            {
                                ShiyouShisetsu.Text = FacilityRad.Text;
                            }
                            if (!dr.IsSisetuCodeNull())
                            {
                                TbxFacilityCode.Text = dr.SisetuCode.ToString();
                            }
                            if (!dr.IsSisetsuTantoNull())
                            {
                                TbxFacilityResponsible.Text = dr.SisetsuTanto;
                            }
                            if (!dr.IsSisetsuPostNull())
                            {
                                TbxYubin.Text = dr.SisetsuPost;
                            }
                            if (!dr.IsSisetsuCityCodeNull())
                            {
                                DataMaster.M_CityRow dc = ClassMaster.GetCity(dr.SisetsuCityCode, Global.GetConnection());
                                if (dc != null)
                                {
                                    RcbCity.Text = dc.CityName;
                                    RcbCity.SelectedValue = dc.CityCode.ToString();
                                }
                            }
                            if (!dr.IsSisetsuMei1Null())
                            {
                                TbxFacilityName.Text = dr.SisetsuMei1;
                            }
                            if (!dr.IsSisetsuMei2Null())
                            {
                                TbxFacilityName2.Text = dr.SisetsuMei2;
                            }
                            if (!dr.IsSisetsuAbbrevirationNull())
                            {
                                TbxFaci.Text = dr.SisetsuAbbreviration;
                            }
                            if (!dr.IsSisetsuTellNull())
                            {
                                TbxTel.Text = dr.SisetsuTell;
                            }
                            if (!dr.IsSisetsuJusyoNull())
                            {
                                TbxFaciAdress.Text = dr.SisetsuJusyo;
                            }
                            if (!dr.IsSiyouOwariNull())
                            {
                                if (EndDate.Visible == true)
                                {
                                    EndDate.SelectedDate = dr.SiyouOwari;
                                }
                                else
                                {
                                    end.Text = dr.SiyouOwari.ToShortDateString();
                                }
                            }
                            if (!dr.IsKakeritsuNull())
                            {
                                kakeri.Text = dr.Kakeritsu;
                            }

                            if (!dr.IsSyouhinMeiNull())
                            {
                                SerchProduct.Text = dr.SyouhinMei;
                            }

                            if (!dr.IsSyouhinCodeNull())
                            {
                                SerchProduct.Text = dr.SyouhinCode;
                            }
                            if (!dr.IsJutyuTekiyoNull())
                            {
                                Tekiyo.Text = dr.JutyuTekiyo;
                            }
                            if (!dr.IsHyojunKakakuNull())
                            {
                                HyoujyunTanka.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                            }

                            if (!dr.IsShiiresakiCodeNull() && RadComboCategory.Text == "上映会")
                            {
                                DataMaster.M_JoueiKakakuDataTable dtJ = ClassMaster.GetJouei(dr.ShiiresakiCode.ToString(), dr.KeitaiMei, dr.Zasu, Global.GetConnection());
                                RcbHanni.Items.Clear();
                                if (dtJ.Count > 0)
                                {
                                    for (int items = 0; items < dtJ.Count; items++)
                                    {
                                        RcbHanni.Items.Add(dtJ[items].Range);
                                    }
                                    RcbHanni.SelectedItem.Text = dr.Range;
                                    DataMaster.M_JoueiKakakuDataTable dtJK = ClassMaster.GetJouei2(dr.ShiiresakiCode.ToString(), dr.KeitaiMei, dr.Zasu, dr.Range, Global.GetConnection());
                                    HyoujyunTanka.Text = dtJK[0].HyoujunKakaku;
                                    Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
                                    Zaseki.SelectedValue = dr.Zasu;
                                }
                            }
                            else
                            {
                                if (!dr.IsRangeNull())
                                {
                                    LblHanni.Text = dr.Range;
                                    TbxHanni.Text = dr.Range;
                                }
                            }
                            if (!dr.IsBasyoNull())
                            {
                                ShiyouShisetsu.Text = dr.Basyo;
                            }
                            if (!dr.IsHattyuSakiMeiNull())
                            {
                                Hachu.Text = dr.HattyuSakiMei;
                            }

                            if (!dr.IsZasuNull())
                            {
                                Zaseki.Text = dr.Zasu;
                            }
                            if (!dr.IsMediaNull())
                            {
                                Media.Text = dr.Media;
                            }
                            if (!dr.IsMekarHinbanNull())
                            {
                                Lblproduct.Text = dr.MekarHinban;
                            }
                            if (!dr.IsRyoukinNull())
                            {

                                int kk = int.Parse(dr.Ryoukin);
                                Kingaku.Text = kk.ToString("0,0");
                            }
                            if (!dr.IsHattyuTankaNull())
                            {
                                Tanka.Text = dr.HattyuTanka.ToString("0,0");
                                tk.Value = dr.HattyuTanka.ToString();
                            }
                            if (!dr.IsUriageNull())
                            {
                                int uri = int.Parse(dr.Uriage);
                                Uriage.Text = uri.ToString("0,0");
                                ug.Value = dr.Uriage;
                            }
                            if (!dr.IsShiireKingakuNull())
                            {
                                int shi = int.Parse(dr.ShiireKingaku);
                                ShiireKingaku.Text = shi.ToString("0,0");
                            }
                            if (!dr.IsShiireTankaNull())
                            {
                                if (dr.ShiireTanka == "")
                                {
                                    dr.ShiireTanka = "0";
                                }
                                int shitan = int.Parse(dr.ShiireTanka);
                                ShiireTanka.Text = shitan.ToString("0,0");
                                zst.Value = dr.ShiireTanka;
                            }
                            if (!dr.IsHattyuSuryouNull())
                            {
                                Suryo.Text = dr.HattyuSuryou.ToString();
                            }
                            if (!dr.IsWareHouseNull())
                            {
                                WareHouse.Text = dr.WareHouse;
                            }
                            if (!dr.IsZeikuNull())
                            {
                                zeiku.Text = dr.Zeiku;
                            }

                        }

                        string a = RadComboCategory.SelectedValue;
                        Ctl.Test4(a);
                        CategoryCode.Value = RadComboCategory.SelectedValue;
                        string ddl = RadComboCategory.Text;
                        bool s = CheckBox4.Checked;

                        //Ctl.Test2(ddl, s);
                        Category();
                    }
                    catch (Exception ex)
                    {
                        Err.Text = ex.Message;
                    }
                    int no = CtrlSyousai.Items.Count + 1;

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label RowNo = e.Item.FindControl("RowNo") as Label;

                        RowNo.Text = no.ToString();
                    }

                    //Keisan();
                    AddBtn.Focus();
                }
                if (CheckBox5.Checked)
                {
                    AllFukusu();
                }
                else
                {
                    kari();
                }
            }
        }
        //ItemDataBound
        private void AllFukusu()
        {
            Fukusu();
            //Fukudate();
        }

        //登録ボタン
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                Keisan2();
                AllFukusu();
                DataMitumori.T_RowDataTable df = new DataMitumori.T_RowDataTable();

                //TokuisakiCode.Value = RadComboBox1.SelectedValue;
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
                        dl.HatyuFlg = false;
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
                    DataJutyu.T_JutyuHeaderRow dm = ClassJutyu.GetMaxJutyu(Global.GetConnection());
                    string sl = dm.JutyuNo.ToString();
                    string arr = sl.Substring(3, 5);
                    int ar = int.Parse(arr);
                    int a = ar + 1;
                    SessionManager.KI();
                    int ki = int.Parse(SessionManager.KII);
                    int f = ki + a;
                    string no = "2" + f.ToString();
                    string noo = "2" + ki.ToString();
                    int nooo = int.Parse(noo);
                    if (nooo > dm.JutyuNo)
                    {
                        int getnewNo = nooo + 1;
                        dl.JutyuNo = getnewNo;
                    }
                    else
                    {
                        dl.JutyuNo = int.Parse(no);
                    }
                    dp.AddT_JutyuHeaderRow(dl);
                    ClassJutyu.InsertJutyuHeader(dp, Global.GetConnection());
                }

                DataJutyu.T_JutyuDataTable dg = new DataJutyu.T_JutyuDataTable();
                DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu(Global.GetConnection());
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = dg.NewT_JutyuRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    Label MakerHinban = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    RadComboBox ProductName = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    Label Hanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");
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
                    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");

                    if (TbxFaciAdress.Text != "")
                    {
                        dr.SisetuJusyo1 = TbxFaciAdress.Text;
                    }
                    if (TbxYubin.Text != "")
                    {
                        dr.SisetuPost = TbxYubin.Text;
                    }
                    if (RcbCity.SelectedValue != "")
                    {
                        dr.SisetuCityCode = RcbCity.SelectedValue;
                    }
                    if (TbxFacilityCode.Text != "")
                    {
                        dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
                    }
                    if (TbxFacilityResponsible.Text != "")
                    {
                        dr.SisetuTanto = TbxFacilityResponsible.Text;
                    }
                    if (TbxFaci.Text != "")
                    {
                        dr.SisetsuAbbreviration = TbxFaci.Text;
                    }
                    if (TbxTel.Text != "")
                    {
                        dr.SisetsuTell = TbxTel.Text;
                    }


                    dr.JutyuSuryou = int.Parse(Suryo.Text);
                    dr.TourokuName = Tantou.Text;
                    dr.RowNo = i + 1;
                    int rn = i + 1;

                    if (RadZeiKubun.Text != "")
                    {
                        dr.ZeiKubun = RadZeiKubun.Text;
                    }
                    if (kakeri.Text != "")
                    {
                        dr.Kakeritsu = kakeri.Text;
                    }
                    if (MakerHinban.Text != "")
                    {
                        dr.MekarHinban = MakerHinban.Text;
                    }
                    if (ProductName.Text != "")
                    {
                        string cate = RadComboCategory.Text;
                        string hanni = Hanni.Text;
                        dr.SyouhinMei = ProductName.Text;
                        //dr.SyouhinCode = arr[1];
                        DataSet1.M_Kakaku_2DataTable dk = Class1.getproduct(MakerHinban.Text, cate, hanni, Global.GetConnection());
                        for (int p = 0; p < dk.Count; p++)
                        {
                            dr.SyouhinCode = dk[p].SyouhinCode;
                            dr.HyojunKakaku = dk[p].HyoujunKakaku;
                            if (Hanni.Text != "")
                            {
                                dr.Range = dk[p].Hanni;
                            }
                            else
                            {
                                dr.Range = "";
                            }
                        }
                    }
                    if (Zaseki.Text != "")
                    {
                        dr.Zasu = Zaseki.Text;
                    }
                    if (Media.Text != "")
                    {
                        dr.KeitaiMei = Media.Text;
                    }
                    if (HyoujyunTanka.Text != "")
                    {
                        string hyoutan = HyoujyunTanka.Text;
                        string r1 = hyoutan.Replace(",", "");
                        dr.HyojunKakaku = int.Parse(r1);
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
                        dr.SiyouKaishi = DateTime.Parse(start.Text);
                        dr.SiyouOwari = DateTime.Parse(end.Text);
                    }
                    if (CheckBox4.Checked == false)
                    {
                        if (StartDate.SelectedDate != null)
                        {
                            dr.SiyouKaishi = StartDate.SelectedDate.Value;
                        }
                        if (EndDate.SelectedDate != null)
                        {
                            dr.SiyouOwari = EndDate.SelectedDate.Value;
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
                        dr.Uriagekeijyou = int.Parse(r1);
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

                    if (RadDatePicker1.SelectedDate != null)
                    {
                        dr.Syokaibi = RadDatePicker1.SelectedDate.Value;
                    }
                    if (RadDatePicker2.SelectedDate != null)
                    {
                        dr.JutyuBi = RadDatePicker2.SelectedDate.Value;
                    }
                    if (RadComboBox1.Text != "")
                    {
                        dr.TokuisakiMei = TokuisakiMei.Value;
                        dr.TokuisakiCode = TokuisakiCode.Value;
                    }
                    if (RadComboBox3.Text != "")
                    {
                        dr.SeikyusakiMei = RadComboBox3.Text;
                    }
                    if (RadComboCategory.Text != "")
                    {
                        dr.CategoryName = RadComboCategory.Text;
                        dr.CateGory = int.Parse(CategoryCode.Value);
                    }

                    if (CheckBox5.Checked == true)
                    {
                        dr.FukusuFaci = "true";
                        dr.SisetuMei = Facility.Text;
                        string fac = Facility.Text;

                        DataSet1.M_Facility_NewDataTable de = Class1.FacilityDT(fac, Global.GetConnection());

                        for (int d = 0; d < de.Count; d++)
                        {
                            FacilityAddress.Value = de[d].Address1 + de[d].Address2;
                            dr.SisetuCode = de[d].FacilityNo;
                            dr.TyokusousakiMei = Facility.Text;
                            dr.SisetuJusyo1 = FacilityAddress.Value;
                        }
                    }
                    else
                    {
                        if (CheckBox1.Checked == true)
                        {
                            dr.SisetuMei = KariFaci.Text;
                            dr.SisetuCode = int.Parse(FacilityCode.Value);
                            dr.SisetuJusyo1 = FacAdd.Value;
                        }
                        else
                        {
                            if (ShiyouShisetsu.Text != "")
                            {
                                dr.SisetuMei = ShiyouShisetsu.Text;
                                dr.SisetuCode = int.Parse(FacilityCode.Value);
                                string fac = ShiyouShisetsu.Text;
                                DataSet1.M_Facility_NewDataTable de = Class1.FacilityDT(fac, Global.GetConnection());
                                for (int d = 0; d < de.Count; d++)
                                {
                                    FacilityAddress.Value = de[d].Address1 + de[d].Address2;
                                }
                                dr.SisetuJusyo1 = FacilityAddress.Value;

                            }
                        }
                    }
                    if (Hachu.Text != "")
                    {
                        dr.HattyuSakiMei = Hachu.Text;
                    }
                    if (Tantou.Text != "")
                    {
                        dr.TanTouName = Tantou.Text;
                    }
                    if (RadComboBox4.Text != "")
                    {
                        dr.Busyo = RadComboBox4.Text;
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
                        dr.SisetuJusyo1 = FacilityAddress.Value;
                    }
                    if (WareHouse.Text != "")
                    {
                        dr.WareHouse = WareHouse.Text;
                    }
                    if (JutyuNo.Text != "")
                    {
                        dr.JutyuNo = int.Parse(JutyuNo.Text);
                        dr.UriageFlg = false;
                        dg.AddT_JutyuRow(dr);

                        //string mNo = SessionManager.HACCYU_NO;
                        //string[] strAry = mNo.Split('_');
                        //string MiNo = strAry[0];
                        //int row = i + 1;
                        //DataJutyu.T_JutyuHeaderDataTable dx = ClassJutyu.GetJutyuHeader1(MiNo, Global.GetConnection());
                        //if (dx.Count >= 1)
                        //{
                        //    //ClassMitumori.DelMitumori2(mNo, Global.GetConnection());
                        //    dr.JutyuNo = int.Parse(MiNo);
                        //    dr.UriageFlg = false;
                        //    dg.AddT_JutyuRow(dr);

                        //    //DataJutyu.T_JutyuDataTable dk = ClassJutyu.GetJutyu4(MiNo, rn, Global.GetConnection());
                        //    ClassJutyu.UpDateJutyu2(MiNo, dg, rn, Global.GetConnection());
                        //}
                    }
                    else
                    {
                        DataJutyu.T_JutyuHeaderRow dm = ClassJutyu.GetMaxJutyu(Global.GetConnection());
                        string sl = dm.JutyuNo.ToString();
                        string arr = sl.Substring(3, 5);
                        int ar = int.Parse(arr);
                        int a = ar + 1;
                        SessionManager.KI();
                        int ki = int.Parse(SessionManager.KII);
                        int f = ki + a;
                        string no = "2" + f.ToString();
                        string noo = "2" + ki.ToString();
                        int nooo = int.Parse(noo);
                        if (nooo > dm.JutyuNo)
                        {
                            int getnewNo = nooo + 1;
                            dr.JutyuNo = getnewNo;
                        }
                        else
                        {
                            dr.JutyuNo = int.Parse(no);
                        }
                        dr.UriageFlg = false;
                        dg.AddT_JutyuRow(dr);
                    }
                }
                ClassJutyu.InsertJutyu2(dg, Global.GetConnection());
                JutyuNo.Text = dl.JutyuNo.ToString();
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
        //登録ボタン

        protected void Button6_Click(object sender, EventArgs e)
        {
            TokuisakiSyousai.Visible = false;
            mInput2.Visible = true;
            CtrlSyousai.Visible = true;
            AddBtn.Visible = true;
            Button13.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Visible = false;
            NouhinsakiPanel.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TokuisakiSyousai.Visible = true;
            mInput2.Visible = false;
            CtrlSyousai.Visible = false;
            AddBtn.Visible = false;
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

            //何のデータを使うのかを指定する（この場合はdt）

            CtrlSyousai.DataSource = dt;
            //データバインドに飛ぶ
            CtrlSyousai.DataBind();
        }

        private void AddNewRow(DataMitumori.T_RowDataTable dt)
        {
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            dt.AddT_RowRow(dr);
        }

        protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            Fukusu();
        }

        protected void Fukusu()
        {
            try
            {
                if (CheckBox5.Checked == true)
                {
                    if (FacilityRad.Text != "")
                    {
                        string facility = FacilityRad.Text;
                        string facilitycode = FacilityRad.SelectedValue;
                        for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                        {
                            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                            CtlMitsuSyosai.FukusuCheckedTrue();
                        }
                    }
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
            string fac = FacilityRad.SelectedValue;
            DataSet1.M_Facility_NewDataTable dt = Class1.FacilityDT(fac, Global.GetConnection());
            for (int i = 0; i < dt.Count; i++)
            {
                FacilityAddress.Value = dt[i].Address1 + dt[i].Address2;
            }
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            //Fukudate();
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

        //削除-----------------------------------------------------------------------------------------------
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
        }//
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
            Keisan2();
            Register.Focus();
        }

        private void Keisan2()
        {
            int t = 0;
            int s = 0;
            int a = 0;
            int r = 0;
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
                TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
                TextBox ShiireTanka = (TextBox)CtlMitsuSyosai.FindControl("ShiireTanka");
                TextBox ShiireKingaku = (TextBox)CtlMitsuSyosai.FindControl("ShiireKingaku");
                TextBox Tanka = (TextBox)CtlMitsuSyosai.FindControl("Tanka");
                TextBox Uriage = (TextBox)CtlMitsuSyosai.FindControl("Uriage");
                TextBox Suryou = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
                HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
                HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
                HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
                HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
                HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
                HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
                HtmlInputHidden zeiht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeiht");
                HtmlInputHidden zeist = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeist");

                string tanka = Tanka.Text.Replace(",", "");
                string uri = Uriage.Text.Replace(",", "");
                string shi = ShiireKingaku.Text.Replace(",", "");
                int ryou = int.Parse(Suryou.Text);
                int ta = int.Parse(tanka) * ryou;
                Uriage.Text = ta.ToString("0,0");
                int si = int.Parse(shi);
                if (RadZeiKubun.Text == "税込")
                {

                    UriageGokei.Visible = false;
                    t += ta;
                    s += si;
                    r += ryou;
                    a = t - s;
                    Uriagekeijyou.Text = t.ToString("0,0");
                    Shiirekei.Text = s.ToString("0,0");
                    TextBox10.Text = r.ToString();
                    TextBox6.Text = a.ToString("0,0");
                    int rieki = a * 100 / t;
                    TextBox13.Text = rieki.ToString();
                    double zei = t * 0.1;
                    Zei.Text = zei.ToString("0,0");
                }
                else
                {
                    r += ryou;
                    t += ta;
                    s += si;
                    a = t - s;
                    TextBox10.Text = r.ToString();
                    Uriagekeijyou.Text = t.ToString("0,0");
                    Shiirekei.Text = s.ToString("0,0");
                    TextBox6.Text = a.ToString("0,0");
                    double zei = t / 10;
                    Zei.Text = zei.ToString("0,0");
                    int rieki = a * 100 / t;
                    TextBox13.Text = rieki.ToString();
                    double tt = t;
                    double gokei = tt + zei;
                    UriageGokei.Text = gokei.ToString("0,0");
                }
            }
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
            Create();
        }
        //Delete
        protected void DelBtn_Click(object sender, EventArgs e)
        {
            string mNo = JutyuNo.Text;
            ClassJutyu.DelJutyu(mNo, Global.GetConnection());
            ClassJutyu.DelJutyuHeader(mNo, Global.GetConnection());

            End.Text = "受注No." + mNo + "を削除しました。";

            string arr = mNo.ToString().Substring(1, 7);
            string no = "1" + arr;
            int noo = int.Parse(no);

            try
            {
                Keisan2();
                AllFukusu();
                DataMitumori.T_RowDataTable df = new DataMitumori.T_RowDataTable();

                //TokuisakiCode.Value = RadComboBox1.SelectedValue;
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録

                DataMitumori.T_MitumoriHeaderDataTable dw = new DataMitumori.T_MitumoriHeaderDataTable();
                DataMitumori.T_MitumoriHeaderRow dh = dw.NewT_MitumoriHeaderRow();


                if (Uriagekeijyou.Text != "")
                {
                    string r1 = Uriagekeijyou.Text.Replace(",", "");
                    dh.GokeiKingaku = int.Parse(r1);
                    dh.SoukeiGaku = int.Parse(r1);
                    dh.GokeiKingaku = int.Parse(r1);
                }
                if (Shiirekei.Text != "")
                {
                    string r2 = Shiirekei.Text.Replace(",", "");
                    dh.ShiireKingaku = int.Parse(r2);

                }
                if (Zei.Text != "")
                {
                    string r3 = Zei.Text.Replace(",", "");
                    dh.SyohiZeiGokei = int.Parse(r3);

                }
                dh.CategoryName = RadComboCategory.Text;
                dh.CateGory = int.Parse(CategoryCode.Value);

                if (TextBox6.Text != "")
                {
                    string r4 = TextBox6.Text.Replace(",", "");
                    dh.ArariGokeigaku = int.Parse(r4);
                }
                if (TextBox10.Text != "")
                {
                    dh.SouSuryou = int.Parse(TextBox10.Text);
                }

                if (UriageGokei.Text != "0")
                {
                    string sou = UriageGokei.Text;
                    string r9 = sou.Replace(",", "");
                    dh.SoukeiGaku = int.Parse(r9);
                }
                else
                {
                    string r5 = Uriagekeijyou.Text.Replace(",", "");
                    dh.GokeiKingaku = int.Parse(r5);
                }

                if (CheckBox1.Checked == true)
                {
                    dh.TokuisakiName = KariTokui.Text;
                }
                else
                {
                    if (RadComboBox1.Text != "")
                    {
                        dh.TokuisakiName = TokuisakiMei.Value;

                        string toku = TokuisakiMei.Value;
                        DataSet1.M_Tokuisaki2DataTable du = Class1.GetTokuisakiName(toku, Global.GetConnection());
                        for (int i = 0; i < du.Count; i++)
                        {
                            dh.TokuisakiCode = du[i].TokuisakiCode.ToString();
                        }
                    }
                }
                if (RadComboBox3.Text != "")
                {
                    dh.SeikyusakiName = RadComboBox3.Text;

                }
                if (RadComboBox2.Text != "")
                {
                    dh.TyokusosakiName = RadComboBox2.Text;

                }
                if (FacilityRad.Text != "")
                {
                    dh.FacilityName = FacilityRad.Text;

                }
                if (Tantou.Text != "")
                {
                    dh.TantoName = Tantou.Text;

                }
                if (RadComboBox4.Text != "")
                {
                    dh.Bumon = RadComboBox4.Text;

                }
                if (RadZeiKubun.Text != "")
                {
                    dh.Zeikubun = RadZeiKubun.Text.Trim();

                }
                if (CheckBox1.Checked == true)
                {
                    dh.KariFLG = CheckBox1.Checked.ToString();

                }

                if (Label3.Text != "")
                {
                    dh.Kakeritsu = Label3.Text;

                }

                if (TbxKake.Text != "")
                {
                    dh.Kakeritsu = TbxKake.Text;

                }


                //見積データに受注フラグをtrueに更新する
                DataMitumori.T_MitumoriHeaderDataTable db = ClassMitumori.GetMitumoriHeader(noo.ToString(), Global.GetConnection());

                //受注データに挿入

                dh.MitumoriNo = noo;
                dh.JutyuFlg = "False";
                dw.AddT_MitumoriHeaderRow(dh);
                ClassMitumori.UpdateMitumoriHeader(noo.ToString(), dw, Global.GetConnection());

            }
            catch (Exception ex)
            {
                Err.Text = "データを登録することができませんでした。";
            }
            if (Err.Text == "")
            {
                End.Text += "見積No." + noo.ToString() + "を未受注に変更致しました。";
            }
            // }

        }
        //Delete
        //発注ボタン
        protected void HatyuBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Keisan2();
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
                    dh.HatyuFlg = true;
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
            catch (Exception ex)
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
            LblTantoStaffCode.Text = RadComboBox5.SelectedValue;
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
            if (TbxTokuisakiName.Text != "")
            {
                RadComboBox1.Text = TbxTokuisakiName.Text;
                RadComboBox3.Text = TbxTokuisakiName.Text;
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
                string id = LblTantoStaffCode.Text;
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTanto1(id, Global.GetConnection());
                RadComboBox4.Items.Clear();
                for (int i = 0; i < dt.Count; i++)
                {
                    RadComboBox4.Items.Add(new RadComboBoxItem(dt[i].BumonName, dt[i].Bumon.ToString()));
                }
                Tantou.Text = dt[0].UserName;
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
            mInput2.Visible = true;
            CtrlSyousai.Visible = true;
            AddBtn.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
            Button13.Visible = true;
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
