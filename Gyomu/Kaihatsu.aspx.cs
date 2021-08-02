using Core.Sql;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            KariNouhin.Visible = false;

            TextBox10.Text = "0";
            TextBox7.Text = "0";
            TextBox8.Text = "0";
            TextBox6.Text = "0";
            TextBox5.Text = "0";
            TextBox12.Text = "0";
            Err.Text = "";
            End.Text = "";
            NouhinsakiPanel.Visible = false;
            if (!IsPostBack)
            {
                TBTokuisaki.Style["display"] = "none";
                mInput.Visible = true;
                CtrlSyousai.Visible = true;
                AddBtn.Visible = true;
                SubMenu.Visible = true;
                SubMenu2.Style["display"] = "none";
                RadComboBox5.Style["display"] = "none";
                RadDatePicker1.SelectedDate = DateTime.Now;
                RadDatePicker2.SelectedDate = DateTime.Now;
                RadDatePicker3.SelectedDate = DateTime.Now;
                //Label1.Text = SessionManager.User.M_user.UserName;
                string code = SessionManager.User.M_user.UserID;
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
                //CustomerCode.Value = dd[i].TokuisakiName1;
            }
            //Keisan();
            if (CtrlSyousai.Items.Count >= 1)
            {
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    // TextBox MakerHinban = (TextBox)CtlMitsuSyosai.FindControl("MakerHinban");
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    bool bo = true;
                    bool boo = true;

                    if (SerchProduct.Text != "")
                    {
                        bo = CtlMitsuSyosai.FocusRoute();
                    }
                    //if (ProductName.Text != "")
                    //{
                    //    boo = CtlMitsuSyosai.FocusRoute2();
                    //}
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

        private void Create2()
        {
            string MNo = SessionManager.HACCYU_NO;
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

            if (!dr.IsCategoryNameNull())
            {
                RadComboCategory.SelectedValue = dr.CateGory.ToString();
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
                    RadComboBox1.Text = dr.TokuisakiRyakusyo;
                    RadComboBox3.Text = dr.TokuisakiRyakusyo;
                    TbxTokuisakiRyakusyo.Text = dr.TokuisakiRyakusyo;
                    TbxTokuisakiName.Text = dr.TokuisakiName;
                    string[] tokucode = dr.TokuisakiCode.Trim().Split('/');
                    CustomerCode.Value = tokucode[0];
                    TbxCustomer.Text = tokucode[0];
                    TokuisakiCode.Value = tokucode[1];
                    TbxTokuisakiCode.Text = tokucode[1];
                    string un = Label1.Text = dr.TantoName;
                    DataSet1.M_TantoDataTable dtT = Class1.GetStaff2(un, Global.GetConnection());
                    for (int t = 0; t < dtT.Count; t++)
                    {
                        RadComboBox4.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
                        RadComboBox5.Items.Add(new RadComboBoxItem(dtT[t].BumonName, dtT[t].Bumon.ToString()));
                        LblTantoStaffCode.Text = dtT[t].Bumon.ToString();
                    }

                    RadZeiKubun.SelectedValue = dr.Zeikubun;
                    RcbTax.SelectedValue = dr.Zeikubun;
                    Shimebi.Text = dr.Shimebi;
                    RcbShimebi.SelectedValue = dr.Shimebi;
                    Label3.Text = dr.Kakeritsu;
                    TbxKakeritsu.Text = dr.Kakeritsu;
                }
            }
            if (!dr.IsFacilityNameNull())
            {
                FacilityRad.Text = dr.FacilityName;
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
                bool ckb = CheckBox1.Checked;
                CtlMitsuSyosai.kari(ckb);
            }
        }

        private void Keisan()
        {
            try
            {
                TextBox10.Text = "0";
                TextBox7.Text = "0";
                TextBox8.Text = "0";
                TextBox6.Text = "0";
                TextBox5.Text = "0";
                TextBox12.Text = "0";
                urikei.Value = "0";
                shikei.Value = "0";

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

                    if (RadZeiKubun.Text == "税込")
                    {
                        string s = RadZeiKubun.Text;
                        CtlMitsuSyosai.Zeikei(s);
                        TextBox12.Visible = false;
                        int su = int.Parse(Suryou.Text);
                        if (HyoujyunTanka.Text != "")
                        {
                            if (Kingaku.Text != "")
                            {
                                //コントロールの計算
                                int kake = int.Parse(Label3.Text);
                                string hyoutan = HyoujyunTanka.Text;
                                string r1 = hyoutan.Replace(",", "");
                                int Ctlht = int.Parse(r1);
                                double Ctlht2 = Ctlht * kake / 100;
                                Tanka.Text = Ctlht2.ToString("0,0");
                                string shitan = ShiireTanka.Text;
                                string r2 = shitan.Replace(",", "");
                                int Ctlst = int.Parse(r2);

                                //ヘッダの計算
                                string u = TextBox7.Text;
                                string r3 = u.Replace(",", "");
                                int x = int.Parse(r3);
                                string shi = TextBox8.Text;
                                string r4 = shi.Replace(",", "");
                                int y = int.Parse(r4);

                                double urikeiForm = x + Ctlht2;
                                int shikeiForm = y + Ctlst;

                                //売上計の入力
                                TextBox7.Text = urikeiForm.ToString("0,0");
                                urikei.Value = urikeiForm.ToString();
                                //仕入計の入力
                                TextBox8.Text = shikeiForm.ToString("0,0");
                                shikei.Value = shikeiForm.ToString();
                                //粗利計の入力
                                double ararikei = urikeiForm - shikeiForm;
                                TextBox6.Text = ararikei.ToString("0,0");
                                arakei.Value = ararikei.ToString();
                                //消費税額の入力
                                double syouhi = urikeiForm / 1.1;
                                double zei = syouhi * 0.1;
                                TextBox5.Text = zei.ToString("0,0");
                                zeikei.Value = zei.ToString();
                                //  利益率の入力
                                double riekiper = ararikei * 100 / urikeiForm;
                                TextBox13.Text = riekiper.ToString();
                                //数量計算
                                int h = int.Parse(TextBox10.Text);
                                int j = int.Parse(Suryou.Text);
                                int k = h + j;
                                TextBox10.Text = k.ToString();
                            }
                        }

                    }
                    else
                    {
                        if (HyoujyunTanka.Text != "")
                        {
                            string s = RadZeiKubun.Text;
                            CtlMitsuSyosai.Zeikei(s);
                            TextBox12.Visible = true;

                            int kak = 0;
                            if (Label3.Text != "")
                            {
                                int kake = int.Parse(Label3.Text);
                                kak += kake;
                            }

                            string hyou = HyoujyunTanka.Text;
                            string r5 = hyou.Replace(",", "");
                            int Htanka = int.Parse(r5);
                            int Htanka2 = Htanka * kak / 100;
                            Tanka.Text = Htanka2.ToString("0,0");

                            string shii = ShiireTanka.Text;
                            string r6 = shii.Replace(",", "");
                            int Stanka = int.Parse(r6);
                            int suryo = int.Parse(Suryou.Text);

                            if (Tanka.Text != "")
                            {
                                string tkV = Tanka.Text;
                                string r1 = tkV.Replace(",", "");
                                int tanka = int.Parse(r1);
                                Uriage.Text = (tanka * suryo).ToString("0,0");
                                ug.Value = (tanka * suryo).ToString();
                            }

                            Kingaku.Text = (Htanka2 * suryo).ToString("0,0");
                            kgk.Value = (Htanka2 * suryo).ToString();

                            ShiireKingaku.Text = (Stanka * suryo).ToString("0,0");
                            sk.Value = (Stanka * suryo).ToString();

                            string u = TextBox7.Text;
                            string r7 = u.Replace(",", "");
                            int a = int.Parse(r7);
                            int UriageKei = a + (Htanka2 * suryo);
                            TextBox5.Text = (UriageKei * 0.1).ToString("0,0");
                            zeikei.Value = TextBox5.Text;
                            TextBox12.Text = (UriageKei * 1.1).ToString("0,0");
                            soukei.Value = TextBox12.Text;
                            int b = int.Parse(TextBox10.Text);
                            int Suryokei = b + suryo;
                            TextBox10.Text = Suryokei.ToString();
                            string f = TextBox8.Text;
                            string r8 = f.Replace(",", "");
                            int c = int.Parse(r8);
                            int ShiireKei = c + (Stanka * suryo);
                            TextBox8.Text = ShiireKei.ToString("0,0");
                            shikei.Value = ShiireKei.ToString();
                            TextBox6.Text = (UriageKei - ShiireKei).ToString("0,0");
                            arakei.Value = (UriageKei - ShiireKei).ToString();
                            int rieki = (UriageKei - ShiireKei) * 100 / UriageKei;
                            TextBox13.Text = rieki.ToString();
                            TextBox7.Text = UriageKei.ToString("0,0");
                            urikei.Value = UriageKei.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Err.Text = ex.ToString();
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

        //カテゴリー--------------
        //private void Category()
        //{
        //    ListSet.SetCategory(RadComboCategory);
        //    CategoryCode.Value = RadComboCategory.SelectedValue;
        //    string ddl = RadComboCategory.SelectedItem.Text;

        //    for (int i = 0; i < CtrlSyousai.Items.Count; i++)
        //    {
        //        CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
        //        bool s = CheckBox4.Checked;

        //        CtlMitsuSyosai.Test2(ddl, s);
        //        // CtlMitsuSyosai.Hiduke(s);

        //    }


        //    if (ddl == "公共図書館")
        //    {
        //        DropDownList9.Visible = false;
        //        RadDatePicker3.Visible = false;
        //        RadDatePicker4.Visible = false;
        //        CheckBox4.Visible = false;
        //    }

        //    if (ddl == "学校図書館")
        //    {
        //        DropDownList9.Visible = false;
        //        RadDatePicker3.Visible = false;
        //        RadDatePicker4.Visible = false;
        //        CheckBox4.Visible = false;
        //    }

        //    if (ddl == "その他図書館")
        //    {
        //        DropDownList9.Visible = false;
        //        RadDatePicker3.Visible = false;
        //        RadDatePicker4.Visible = false;
        //        CheckBox4.Visible = false;
        //    }

        //    if (ddl == "防衛省")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "ホテル")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }


        //    if (ddl == "レジャーホテル")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "バス")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "船舶")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }
        //    if (ddl == "上映会")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "複合カフェ")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "健康ランド")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "福祉施設")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "キッズ・BGV")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }


        //    if (ddl == "業務用")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "VOD")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "VOD配信")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;

        //    }

        //    if (ddl == "DOD")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;
        //    }
        //    if (ddl == "DEX")
        //    {
        //        DropDownList9.Visible = true;
        //        RadDatePicker3.Visible = true;
        //        RadDatePicker4.Visible = true;
        //    }
        //}
        //カテゴリー---------------
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
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    if (!CheckBox1.Checked)
        //    {
        //        TBTokuisaki.Visible = true;
        //        mInput.Visible = false;
        //        CtrlSyousai.Visible = false;
        //        AddBtn.Visible = false;
        //        Button13.Visible = false;
        //        head.Visible = false;
        //        SubMenu.Visible = false;
        //        SubMenu2.Visible = true;
        //        raddo.Visible = true;
        //        RadComboBox5.Visible = true;

        //        //Tokuisaki.Visible = true;
        //        //string tokuimei = RadComboBox1.Text;
        //        //string cus = CustomerCode.Value;
        //        //string toku = TokuisakiCode.Value;
        //        //DataSet1.M_Tokuisaki2DataTable dt = Class1.GetTokuisaki2(cus, toku, Global.GetConnection());
        //        //for (int j = 0; j < dt.Count; j++)
        //        //{
        //        //    DataSet1.M_Tokuisaki2Row dr = dt.Rows[j] as DataSet1.M_Tokuisaki2Row;

        //        //    TextBox19.Text = dr.TokuisakiCode.ToString();

        //        //    TextBox20.Text = dr.TokuisakiName1;

        //        //    if (!dr.IsTokuisakiAddress1Null())
        //        //    {
        //        //        TextBox21.Text = dr.TokuisakiAddress1;
        //        //    }
        //        //    if (!dr.IsTokuisakiTELNull())
        //        //    {
        //        //        TextBox22.Text = dr.TokuisakiTEL;
        //        //    }
        //        //    if (!dr.IsTokuisakiFAXNull())
        //        //    {
        //        //        TextBox23.Text = dr.TokuisakiFAX;
        //        //    }
        //        //    if (!dr.IsTokuisakiStaffNull())
        //        //    {
        //        //        TbxCusomerStaff.Text = dr.TokuisakiStaff;
        //        //    }
        //        //    if (!dr.IsTokuisakiDepartmentNull())
        //        //    {
        //        //        TextBox25.Text = dr.TokuisakiDepartment;
        //        //    }
        //        //    if (!dr.IsTokuisakiKeisyoNull())
        //        //    {
        //        //        Label20.Text = dr.TokuisakiKeisyo;
        //        //    }
        //        //    if (!dr.IsTantoStaffCodeNull())
        //        //    {
        //        //        Label20.Text = dr.TantoStaffCode;
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        TBTokuisaki.Visible = true;
        //        mInput.Visible = false;
        //        CtrlSyousai.Visible = false;
        //        AddBtn.Visible = false;
        //        Button13.Visible = false;
        //        head.Visible = false;
        //        SubMenu.Visible = false;
        //        SubMenu2.Visible = true;
        //        //TextBox19.Text = "99999999";
        //    }
        //}
        //得意先ボタン------------------------------------------------

        //納品先ボタン-------------------------------------------------
        protected void Button3_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Visible = true;
            mInput.Visible = false;
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
                ListSet.SetTyokusousaki(sender, e);
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
                        Label1.Text = drTanto.UserName;
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
            FacilityRad.Focus();
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
            category(a);
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                HtmlInputHidden cate = CtlMitsuSyosai.FindControl("HidCategoryCode") as HtmlInputHidden;
                cate.Value = a;
                CtlMitsuSyosai.Test4(a);
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
                    DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                    DataMitumori.T_MitumoriRow df = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_MitumoriRow;

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
                    HtmlInputHidden Cate = (HtmlInputHidden)Ctl.FindControl("HidCategoryCode");
                    Label WareHouse = (Label)Ctl.FindControl("WareHouse");
                    TextBox KariFaci = (TextBox)Ctl.FindControl("KariFaci");
                    Label kakeri = (Label)Ctl.FindControl("Kakeri");
                    Label zeiku = (Label)Ctl.FindControl("zeiku");
                    Label LblProduct = (Label)Ctl.FindControl("LblProduct");
                    RadComboBox SerchProduct = (RadComboBox)Ctl.FindControl("SerchProduct");
                    Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                    TextBox TbxProductCode = (TextBox)Ctl.FindControl("TbxProductCode");
                    TextBox TbxProductName = (TextBox)Ctl.FindControl("TbxProductName");
                    TextBox TbxMakerNo = (TextBox)Ctl.FindControl("TbxMakerNo");
                    RadComboBox RcbMedia = (RadComboBox)Ctl.FindControl("RcbMedia");
                    TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
                    Label LblCateCode = (Label)Ctl.FindControl("LblCateCode");
                    Label LblCategoryName = (Label)Ctl.FindControl("LblCategoryName");
                    TextBox TbxTyokuso = (TextBox)Ctl.FindControl("TbxTyokuso");
                    TextBox TbxHyoujun = (TextBox)Ctl.FindControl("TbxHyoujun");
                    Label LblShiireCode = (Label)Ctl.FindControl("LblShiireCode");
                    RadComboBox RcbShiireName = (RadComboBox)Ctl.FindControl("RcbShiireName");
                    TextBox TbxShiirePrice = (TextBox)Ctl.FindControl("TbxShiirePrice");
                    TextBox TbxCpKakaku = (TextBox)Ctl.FindControl("TbxCpKakaku");
                    TextBox TbxCpShiire = (TextBox)Ctl.FindControl("TbxCpShiire");
                    RadDatePicker RdpCpStart = (RadDatePicker)Ctl.FindControl("RdpCpStart");
                    RadDatePicker RdpCpEnd = (RadDatePicker)Ctl.FindControl("RdpCpEnd");
                    TextBox TbxWareHouse = (TextBox)Ctl.FindControl("TbxWareHouse");
                    RadDatePicker RdpPermissionstart = (RadDatePicker)Ctl.FindControl("RdpPermissionstart");
                    RadDatePicker RdpRightEnd = (RadDatePicker)Ctl.FindControl("RdpRightEnd");
                    TextBox TbxRiyo = (TextBox)Ctl.FindControl("TbxRiyo");
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

                    //読み込んだ単価テキストボックスにJavaScriptを実行させる魔法を付与
                    string strClientID = Tanka.ClientID + "-" + Suryo.ClientID + "-" + Uriage.ClientID + "-" + HyoujyunTanka.ClientID + "-" + ShiireTanka.ClientID + "-" + Kingaku.ClientID + "-" + ShiireKingaku.ClientID + "-" + TbxHyoujun.ClientID;
                    Tanka.Attributes["OnBlur"] = string.Format("Keisan('{0}');", strClientID);
                    Suryo.Attributes["OnBlur"] = string.Format("Keisan('{0}');", strClientID);
                    HyoujyunTanka.Attributes["OnBlur"] = string.Format("Keisan('{0}');", strClientID);
                    ShiireTanka.Attributes["OnBlur"] = string.Format("Keisan('{0}');", strClientID);
                    string BtnClient = AddBtn.ClientID;
                    AddBtn.OnClientClick = string.Format("AddFocus('{0}');", BtnClient);
                    string sp = SerchProduct.ClientID + "-" + BtnClient;
                    SerchProduct.Attributes["OnBlur"] = string.Format("SerchFocus('{0}');", sp);
                    string fr = SerchProduct.ClientID;
                    FacilityRad.Attributes["OnBlur"] = string.Format("FocusMeisai('{0}');", fr);
                    //kari();

                    if (df != null)
                    {
                        DataMitumori.T_MitumoriHeaderDataTable dd = ClassMitumori.GETMitsumorihead(mNo, Global.GetConnection());
                        DataMitumori.T_MitumoriHeaderRow drH = dd[0];
                        if (!drH.IsZasuNull())
                        {
                            Zaseki.SelectedItem.Text = drH.Zasu;
                        }
                        if (!drH.IsCateGoryNull())
                        {
                            Cate.Value = drH.CateGory.ToString();
                        }

                        if (!drH.IsZeikubunNull())
                        {
                            if (drH.Zeikubun == "税込")
                            {
                                TextBox12.Visible = false;
                                RadZeiKubun.SelectedItem.Text = drH.Zeikubun;
                                zeiku.Text = drH.Zeikubun;
                            }
                            else
                            {
                                TextBox12.Visible = true;
                                RadZeiKubun.SelectedItem.Text = drH.Zeikubun;
                                zeiku.Text = drH.Zeikubun;
                            }
                        }
                        if (!drH.IsKakeritsuNull())
                        {
                            if (!drH.IsKariFLGNull())
                            {
                                if (drH.KariFLG != "True")
                                {
                                    Label3.Text = drH.Kakeritsu;
                                    kakeri.Text = drH.Kakeritsu;
                                }
                                else
                                {
                                    TbxKake.Text = drH.Kakeritsu;
                                    kakeri.Text = drH.Kakeritsu;
                                }
                            }
                            Label3.Text = drH.Kakeritsu;
                        }
                        Label94.Text = mNo;
                        //DataMitumori.T_MitumoriRow dl = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_MitumoriRow;

                        if (!df.IsSiyouKaishiNull())
                        {
                            StartDate.SelectedDate = df.SiyouKaishi;
                        }

                        if (!df.IsFukusuFaciNull())
                        {
                            CheckBox5.Checked = true;
                            FacilityRad.SelectedValue = df.SisetuCode.ToString();
                        }

                        if (!df.IsTekiyou1Null())
                        {
                            Tekiyo.Text = df.Tekiyou1;
                        }

                        if (!df.IsShiireNameNull())
                        {
                            Hachu.SelectedValue = df.ShiiresakiCode.ToString();
                            Hachu.Text = df.ShiireName;
                            RcbShiireName.Text = df.ShiireName;
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
                                ShiyouShisetsu.SelectedValue = df.SisetuCode.ToString();
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
                        if (!df.IsSyouhinMeiNull())
                        {
                            //ProductName.Text = df.SyouhinMei;
                            SerchProduct.Text = df.SyouhinMei;
                            TbxProductName.Text = df.SyouhinMei;
                        }
                        if (!df.IsHyojunKakakuNull())
                        {
                            if (df.HyojunKakaku != "OPEN" && df.HyojunKakaku != "")
                            {
                                HyoujyunTanka.Text = int.Parse(df.HyojunKakaku).ToString("0,0");
                                TbxHyoujun.Text = int.Parse(df.HyojunKakaku).ToString("0,0");
                            }
                            else
                            {
                                HyoujyunTanka.Text = df.HyojunKakaku;
                                TbxHyoujun.Text = df.HyojunKakaku;
                            }
                        }
                        if (!df.IsKakeritsuNull())
                        {
                            kakeri.Text = df.Kakeritsu;
                        }
                        if (!df.IsSyouhinCodeNull())
                        {
                            TbxProductCode.Text = df.SyouhinCode;
                        }

                        if (!df.IsZasuNull())
                        {
                            Zaseki.Text = df.Zasu;
                        }
                        if (!df.IsMekarHinbanNull())
                        {
                            LblProduct.Text = df.MekarHinban;
                            TbxMakerNo.Text = df.MekarHinban;
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
                        if (!df.IsUriageNull())
                        {
                            Uriage.Text = df.Uriage.ToString("0,0");
                        }
                        //if (!df.IsTokuisakiMeiNull())
                        //{
                        //    if (CheckBox1.Checked == true)
                        //    {
                        //        KariTokui.Text = df.TokuisakiMei;
                        //        kariSekyu.Text = df.TokuisakiMei;

                        //        string tok = df.TokuisakiMei;
                        //        DataSet1.M_Tokuisaki2DataTable dg = Class1.GetTokuisakiName(tok, Global.GetConnection());
                        //        for (int j = 0; j < dg.Count; j++)
                        //        {
                        //            TokuisakiCode.Value = dg[j].TokuisakiCode.ToString();
                        //        }
                        //    }
                        //    string toku = df.TokuisakiMei;

                        //    DataSet1.M_Tokuisaki2DataTable dp = Class1.GetTokuisakiName(toku, Global.GetConnection());
                        //    for (int j = 0; j < dp.Count; j++)
                        //    {
                        //        TokuisakiCode.Value = dp[j].TokuisakiCode.ToString();
                        //        CustomerCode.Value = dp[j].CustomerCode;
                        //    }
                        //}
                        if (!df.IsKeitaiMeiNull())
                        {
                            Media.Text = df.KeitaiMei;
                            RcbMedia.SelectedValue = df.KeitaiMei;
                        }

                        if (!df.IsShiireTankaNull())
                        {
                            ShiireTanka.Text = df.ShiireTanka.ToString("0,0");
                            st.Value = df.ShiireTanka.ToString();
                            TbxShiirePrice.Text = df.ShiireTanka.ToString("0,0");
                        }
                        if (!df.IsShiireKingakuNull())
                        {
                            ShiireKingaku.Text = df.ShiireKingaku.ToString("0,0");
                            sk.Value = df.ShiireKingaku.ToString();
                        }
                        if (!df.IsWareHouseNull())
                        {
                            WareHouse.Text = df.WareHouse;
                            TbxWareHouse.Text = df.WareHouse;
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
                                //HyoujyunTanka.Text = dtJK[0].HyoujunKakaku;
                                Hachu.SelectedValue = df.ShiiresakiCode.ToString();
                                LblShiireCode.Text = df.ShiiresakiCode.ToString();
                                Zaseki.SelectedValue = df.Zasu;
                            }
                        }
                        else
                        {
                            if (!df.IsShiiresakiCodeNull())
                            {
                                LblShiireCode.Text = df.ShiiresakiCode.ToString();
                            }
                            if (!df.IsRangeNull())
                            {
                                LblHanni.Text = df.Range;
                                TbxHanni.Text = df.Range;
                            }
                        }

                    }
                    else
                    {
                        Fukudate();

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

                        if (!dr.IsSyouhinCodeNull())
                        {
                            TbxProductCode.Text = dr.SyouhinCode;
                        }

                        if (!dr.IsSisetuCodeNull())
                        {
                            FacilityCode.Value = dr.SisetuCode.ToString();
                        }
                        if (!dr.IsJutyuTekiyoNull())
                        {
                            Tekiyo.Text = dr.JutyuTekiyo;
                        }
                        if (CheckBox4.Checked)
                        {
                            start.Visible = false;
                            end.Visible = false;
                            if (!dr.IsSiyouKaishiNull())
                            {
                                StartDate.SelectedDate = dr.SiyouKaishi;
                            }
                            if (!dr.IsSiyouOwariNull())
                            {
                                EndDate.SelectedDate = dr.SiyouOwari;
                            }
                        }
                        else
                        {
                            start.Visible = false;
                            end.Visible = false;
                            if (!dr.IsSiyouKaishiNull())
                            {
                                StartDate.SelectedDate = DateTime.Parse(dr.SiyouKaishi.ToString());
                            }
                            if (!dr.IsSiyouOwariNull())
                            {
                                EndDate.SelectedDate = DateTime.Parse(dr.SiyouOwari.ToString());
                            }
                        }

                        if (!dr.IsKakeritsuNull())
                        {
                            kakeri.Text = dr.Kakeritsu;
                        }

                        if (!dr.IsSyouhinMeiNull())
                        {
                            //ProductName.Text = dr.SyouhinMei;
                            SerchProduct.Text = dr.SyouhinMei;
                            TbxProductName.Text = dr.SyouhinMei;
                        }

                        if (!dr.IsHyojunKakakuNull())
                        {
                            if (dr.HyojunKakaku != "OPEN")
                            {
                                HyoujyunTanka.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                                TbxHyoujun.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                            }
                            else
                            {
                                HyoujyunTanka.Text = dr.HyojunKakaku;
                                TbxHyoujun.Text = dr.HyojunKakaku;
                            }
                        }

                        //Ctl.FukusuCheckedTrue();
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
                        if (!dr.IsHattyuSakiMeiNull())
                        {
                            Hachu.Text = dr.HattyuSakiMei;
                            RcbShiireName.Text = dr.HattyuSakiMei;
                        }

                        if (!dr.IsZasuNull())
                        {
                            Zaseki.Text = dr.Zasu;
                        }
                        if (!dr.IsMediaNull())
                        {
                            Media.Text = dr.Media;
                            RcbMedia.SelectedValue = dr.Media;
                        }
                        if (!dr.IsMekarHinbanNull())
                        {
                            LblProduct.Text = dr.MekarHinban;
                            TbxMakerNo.Text = dr.MekarHinban;
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
                            TbxShiirePrice.Text = shitan.ToString("0,0");
                            zst.Value = dr.ShiireTanka;
                        }
                        if (!dr.IsHattyuSuryouNull())
                        {
                            Suryo.Text = dr.HattyuSuryou.ToString();
                        }
                        if (!dr.IsWareHouseNull())
                        {
                            WareHouse.Text = dr.WareHouse;
                            TbxWareHouse.Text = dr.WareHouse;
                        }
                        if (!dr.IsZeikuNull())
                        {
                            zeiku.Text = dr.Zeiku;
                        }

                        if (!dr.IsCpkaishiNull())
                        {
                            RdpCpStart.SelectedDate = DateTime.Parse(dr.Cpkaishi);
                        }
                        if (!dr.IsCpOwariNull())
                        {
                            RdpCpEnd.SelectedDate = DateTime.Parse(dr.CpOwari);
                        }
                        if (!dr.IsCategoryCodeNull())
                        {
                            LblCateCode.Text = dr.CategoryCode;
                        }
                        if (!dr.IsCategoryNameNull())
                        {
                            LblCategoryName.Text = dr.CategoryName;
                        }
                        if (!dr.IsShiiresakiCodeNull() && RadComboCategory.Text == "上映会")
                        {
                            DataMaster.M_JoueiKakakuDataTable dtJ = ClassMaster.GetJouei(dr.ShiiresakiCode, dr.Media, dr.Zasu, Global.GetConnection());
                            RcbHanni.Items.Clear();
                            if (dtJ.Count > 0)
                            {
                                for (int items = 0; items < dtJ.Count; items++)
                                {
                                    RcbHanni.Items.Add(dtJ[items].Range);
                                }
                                RcbHanni.SelectedItem.Text = dr.Range;
                                DataMaster.M_JoueiKakakuDataTable dtJK = ClassMaster.GetJouei2(dr.ShiiresakiCode, dr.Media, dr.Zasu, dr.Range, Global.GetConnection());
                                //HyoujyunTanka.Text = dtJK[0].HyoujunKakaku;
                                Hachu.SelectedValue = dr.ShiiresakiCode;
                                LblShiireCode.Text = dr.ShiiresakiCode;
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
                            if (!dr.IsShiiresakiCodeNull())
                            {
                                LblShiireCode.Text = dr.ShiiresakiCode;
                            }
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
                    bool s = CheckBox4.Checked;
                    if (CtrlSyousai.Items.Count >= 1)
                    {
                        bool bo = Ctl.FocusRoute();
                        if (bo)
                        { }
                        else
                        {
                            AddBtn.Focus();
                        }
                    }
                }

                int no = CtrlSyousai.Items.Count + 1;

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label RowNo = e.Item.FindControl("RowNo") as Label;

                    RowNo.Text = no.ToString();
                }

                //if (CheckBox5.Checked == true)
                //{
                //    Fukusu();
                //}
                //else
                //{
                kari();
                //}
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
            //Fukudate();
        }

        //登録ボタン---------------------------------------------------
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                Keisan2();
                AllFukusu();
                DataMitumori.T_RowDataTable df = new DataMitumori.T_RowDataTable();
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録
                DataMitumori.T_MitumoriHeaderDataTable dd = ClassMitumori.GetMitsuHead(Global.GetConnection());
                DataMitumori.T_MitumoriHeaderDataTable dp = new DataMitumori.T_MitumoriHeaderDataTable();
                DataMitumori.T_MitumoriHeaderRow dl = dp.NewT_MitumoriHeaderRow();

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
                        if (TokuisakiCode.Value != "0")
                        {
                            DataSet1.M_Tokuisaki2DataTable du = Class1.GetTokuisaki2(CustomerCode.Value, TokuisakiCode.Value, Global.GetConnection());

                            for (int i = 0; i < du.Count; i++)
                            {
                                dl.TokuisakiName = du[i].TokuisakiName1;
                                dl.TokuisakiRyakusyo = du[i].TokuisakiRyakusyo;
                                dl.TokuisakiCode = du[i].CustomerCode + "/" + du[i].TokuisakiCode.ToString();
                                dl.Shimebi = du[i].Shimebi;
                            }
                        }
                        else
                        {
                            dl.TokuisakiName = TbxTokuisakiName.Text;
                            dl.TokuisakiRyakusyo = TbxTokuisakiName.Text;
                            dl.TokuisakiCode = TbxCustomer.Text + "/" + TbxTokuisakiCode.Text;
                            dl.Shimebi = RcbShimebi.Text;
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
                if (Label1.Text != "")
                {
                    dl.TantoName = Label1.Text;
                }
                dl.JutyuFlg = "False";
                if (RadComboBox4.Text != "")
                {
                    dl.Bumon = RadComboBox4.Text;
                }
                if (RadZeiKubun.Text != "")
                {
                    dl.Zeikubun = RadZeiKubun.Text;
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
                dl.CreateDate = DateTime.Now;

                if (RadDatePicker3.SelectedDate != null && RadDatePicker3.Visible)
                {
                    dl.SiyouKaisi = DateTime.Parse(RadDatePicker3.SelectedDate.Value.ToShortDateString());
                }

                if (RadDatePicker4.SelectedDate != null && RadDatePicker4.Visible)
                {
                    dl.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.Value.ToShortDateString());
                }

                DataMitumori.T_MitumoriHeaderRow dm = ClassMitumori.GetMaxNo(Global.GetConnection());
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
                    string sl = dm.MitumoriNo.ToString();
                    string arr = sl.Substring(3, 5);
                    int ar = int.Parse(arr);
                    int a = ar + 1;
                    SessionManager.KI();
                    int ki = int.Parse(SessionManager.KII);
                    int f = ki + a;
                    no = "1" + f.ToString();
                    string noo = "1" + ki.ToString();
                    int nooo = int.Parse(noo);
                    if (nooo > dm.MitumoriNo)
                    {
                        int getnewNo = nooo + 1;
                        dl.MitumoriNo = getnewNo;
                    }
                    else
                    {
                        dl.MitumoriNo = int.Parse(no);
                    }
                    dp.AddT_MitumoriHeaderRow(dl);
                    ClassMitumori.InsertMitsumoriHeader(dp, Global.GetConnection());
                }

                DataMitumori.T_MitumoriDataTable dt = ClassMitumori.GetMitsumoriDT(Global.GetConnection());

                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataMitumori.T_MitumoriDataTable dg = new DataMitumori.T_MitumoriDataTable();

                    DataMitumori.T_MitumoriRow dr = dg.NewT_MitumoriRow();
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
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
                    Label zeiku = (Label)CtlMitsuSyosai.FindControl("zeiku");
                    Label LblProduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    TextBox TbxProductCode = (TextBox)CtlMitsuSyosai.FindControl("TbxProductCode");
                    TextBox TbxProductName = (TextBox)CtlMitsuSyosai.FindControl("TbxProductName");
                    TextBox TbxMakerNo = (TextBox)CtlMitsuSyosai.FindControl("TbxMakerNo");
                    RadComboBox RcbMedia = (RadComboBox)CtlMitsuSyosai.FindControl("RcbMedia");
                    TextBox TbxHanni = (TextBox)CtlMitsuSyosai.FindControl("TbxHanni");
                    Label LblCateCode = (Label)CtlMitsuSyosai.FindControl("LblCateCode");
                    Label LblCategoryName = (Label)CtlMitsuSyosai.FindControl("LblCategoryName");
                    TextBox TbxTyokuso = (TextBox)CtlMitsuSyosai.FindControl("TbxTyokuso");
                    TextBox TbxHyoujun = (TextBox)CtlMitsuSyosai.FindControl("TbxHyoujun");
                    Label LblShiireCode = (Label)CtlMitsuSyosai.FindControl("LblShiireCode");
                    RadComboBox RcbShiireName = (RadComboBox)CtlMitsuSyosai.FindControl("RcbShiireName");
                    TextBox TbxShiirePrice = (TextBox)CtlMitsuSyosai.FindControl("TbxShiirePrice");
                    TextBox TbxCpKakaku = (TextBox)CtlMitsuSyosai.FindControl("TbxCpKakaku");
                    TextBox TbxCpShiire = (TextBox)CtlMitsuSyosai.FindControl("TbxCpShiire");
                    RadDatePicker RdpCpStart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpStart");
                    RadDatePicker RdpCpEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpEnd");
                    TextBox TbxWareHouse = (TextBox)CtlMitsuSyosai.FindControl("TbxWareHouse");
                    RadDatePicker RdpPermissionstart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpPermissionstart");
                    RadDatePicker RdpRightEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpRightEnd");
                    TextBox TbxRiyo = (TextBox)CtlMitsuSyosai.FindControl("TbxRiyo");
                    RadComboBox Tekiyo = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                    RadComboBox RcbHanni = (RadComboBox)CtlMitsuSyosai.FindControl("RcbHanni");
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
                    dr.TourokuName = Label1.Text;
                    dr.RowNo = i + 1;
                    int rn = i + 1;

                    if (Tekiyou.Text != "")
                    {
                        dr.Tekiyou1 = Tekiyou.Text;
                    }

                    if (RadZeiKubun.Text != "")
                    {
                        dr.Zeikubun = RadZeiKubun.Text;
                    }
                    if (kakeri.Text != "")
                    {
                        dr.Kakeritsu = kakeri.Text;
                    }
                    if (Lblproduct.Text != "")
                    {
                        dr.MekarHinban = Lblproduct.Text;
                    }
                    if (Tekiyou.Text != "")
                    {
                        dr.Tekiyou1 = Tekiyou.Text;
                    }

                    if (SerchProduct.Text != "")
                    {
                        string cate = RadComboCategory.Text;
                        string hanni = LblHanni.Text;
                        dr.SyouhinMei = SerchProduct.Text;
                        dr.SyouhinCode = TbxProductCode.Text;
                        dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
                        if (LblHanni.Text != "")
                        {
                            dr.Range = LblHanni.Text;
                        }
                        else
                        {
                            dr.Range = "";
                        }
                        if (RcbHanni.Text != "")
                        {
                            dr.Range = RcbHanni.Text;
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

                    if (Kingaku.Text != "")
                    {
                        string kin = Kingaku.Text;
                        string r4 = kin.Replace(",", "");
                        dr.Ryoukin = r4;
                        dr.JutyuGokei = int.Parse(r4);
                    }
                    if (RadComboBox2.Text != "")
                    {
                        dr.TyokusousakiMei = TbxTokuisakiName.Text;
                    }
                    if (CheckBox4.Checked == true)
                    {
                        dr.SiyouKaishi = StartDate.SelectedDate.Value;
                        dr.SiyouOwari = EndDate.SelectedDate.Value;
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
                        dr.Uriage = int.Parse(r1);
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
                        dr.MitumoriBi = RadDatePicker2.SelectedDate.Value;
                    }
                    if (!CheckBox1.Checked)
                    {
                        if (RadComboBox1.Text != "")
                        {
                            dr.TokuisakiMei = TbxTokuisakiName.Text;
                            dr.TokuisakiMei2 = RadComboBox1.Text;
                            dr.TokuisakiCode = TokuisakiCode.Value;
                        }
                    }
                    else
                    {
                        if (KariTokui.Text != "")
                        {
                            dr.TokuisakiMei = KariTokui.Text;
                        }
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
                    if (CheckBox1.Checked == true)
                    {
                        dr.SisetuMei = KariFaci.Text;
                        dr.SisetuCode = int.Parse(FacilityCode.Value);
                        dr.SisetuJusyo1 = FacAdd.Value;
                    }
                    else
                    {
                        dr.FukusuFaci = "true";
                        dr.SisetuMei = ShiyouShisetsu.Text;
                        string fac = Facility.Text;
                        dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
                        dr.SisetuJusyo1 = TbxFaciAdress.Text;
                        dr.SisetsuMei2 = TbxFacilityName2.Text;
                        dr.SisetsuAbbreviration = TbxFaci.Text;
                        dr.SisetuTanto = TbxFacilityResponsible.Text;
                        dr.SisetuPost = TbxYubin.Text;
                        dr.SisetsuTell = TbxTel.Text;
                    }

                    if (Hachu.Text != "")
                    {
                        dr.ShiireName = Hachu.Text;
                        dr.ShiiresakiCode = int.Parse(LblShiireCode.Text);
                    }
                    if (Label1.Text != "")
                    {
                        dr.TanTouName = Label1.Text;
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
            AddBtn.Visible = true;
            Button13.Visible = true;
            head.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TBTokuisaki.Visible = true;
            mInput.Visible = false;
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

        //行追加------------------------------------------------------
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
                    Label LblProduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    Label LblHanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");
                    TextBox TbxProductCode = (TextBox)CtlMitsuSyosai.FindControl("TbxProductCode");
                    TextBox TbxProductName = (TextBox)CtlMitsuSyosai.FindControl("TbxProductName");
                    TextBox TbxMakerNo = (TextBox)CtlMitsuSyosai.FindControl("TbxMakerNo");
                    RadComboBox RcbMedia = (RadComboBox)CtlMitsuSyosai.FindControl("RcbMedia");
                    TextBox TbxHanni = (TextBox)CtlMitsuSyosai.FindControl("TbxHanni");
                    Label LblCateCode = (Label)CtlMitsuSyosai.FindControl("LblCateCode");
                    Label LblCategoryName = (Label)CtlMitsuSyosai.FindControl("LblCategoryName");
                    TextBox TbxTyokuso = (TextBox)CtlMitsuSyosai.FindControl("TbxTyokuso");
                    TextBox TbxHyoujun = (TextBox)CtlMitsuSyosai.FindControl("TbxHyoujun");
                    Label LblShiireCode = (Label)CtlMitsuSyosai.FindControl("LblShiireCode");
                    RadComboBox RcbShiireName = (RadComboBox)CtlMitsuSyosai.FindControl("RcbShiireName");
                    TextBox TbxShiirePrice = (TextBox)CtlMitsuSyosai.FindControl("TbxShiirePrice");
                    TextBox TbxCpKakaku = (TextBox)CtlMitsuSyosai.FindControl("TbxCpKakaku");
                    TextBox TbxCpShiire = (TextBox)CtlMitsuSyosai.FindControl("TbxCpShiire");
                    RadDatePicker RdpCpStart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpStart");
                    RadDatePicker RdpCpEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpEnd");
                    TextBox TbxWareHouse = (TextBox)CtlMitsuSyosai.FindControl("TbxWareHouse");
                    RadDatePicker RdpPermissionstart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpPermissionstart");
                    RadDatePicker RdpRightEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpRightEnd");
                    TextBox TbxRiyo = (TextBox)CtlMitsuSyosai.FindControl("TbxRiyo");
                    RadComboBox Tekiyo = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                    TextBox TbxFacilityName = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName");
                    RadComboBox RcbHanni = (RadComboBox)CtlMitsuSyosai.FindControl("RcbHanni");

                    if (LblShiireCode.Text != "")
                    {
                        dr.ShiiresakiCode = LblShiireCode.Text;
                    }

                    if (TbxProductCode.Text != "")
                    {
                        dr.SyouhinCode = TbxProductCode.Text;
                    }

                    if (Tekiyo.Text != "")
                    {
                        dr.JutyuTekiyo = Tekiyo.Text;
                    }

                    if (LblCateCode.Text != "")
                    {
                        dr.CategoryCode = LblCateCode.Text;
                    }
                    if (LblCategoryName.Text != "")
                    {
                        dr.CategoryName = LblCategoryName.Text;
                    }
                    if (RdpCpStart.SelectedDate != null)
                    {
                        dr.Cpkaishi = RdpCpStart.SelectedDate.Value.ToShortDateString();
                    }
                    if (RdpCpEnd.SelectedDate != null)
                    {
                        dr.CpOwari = RdpCpEnd.SelectedDate.Value.ToShortDateString();
                    }

                    //Fukusu();
                    string s = RadZeiKubun.Text;
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
                    if (CheckBox4.Checked)
                    {
                        dr.SiyouKaishi = StartDate.SelectedDate.Value;
                        dr.SiyouOwari = EndDate.SelectedDate.Value;
                    }
                    else
                    {
                        if (StartDate.SelectedDate == null)
                        { }
                        else
                        { dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString()); }
                        if (EndDate.SelectedDate == null)
                        { }
                        else
                        { dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString()); }
                    }

                    //if (ProductName.Text != "")
                    //{
                    //    dr.SyouhinMei = ProductName.Text;
                    //}

                    if (LblProduct.Text != "")
                    {
                        dr.MekarHinban = LblProduct.Text;
                    }
                    if (TbxHyoujun.Text != "")
                    {
                        dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
                    }
                    if (RcbHanni.Text != "")
                    {
                        dr.Range = RcbHanni.Text;
                    }
                    if (LblHanni.Text != "")
                    {
                        dr.Range = LblHanni.Text;
                    }
                    if (ShiyouShisetsu.Text == "")
                    {
                        if (Facility.Text != "")
                        {
                            dr.Basyo = Facility.Text;
                        }
                        else
                        {
                            if (KariFaci.Text != "")
                            {
                                dr.Basyo = KariFaci.Text;
                            }
                        }
                    }
                    else
                    {
                        dr.Basyo = ShiyouShisetsu.Text;
                        if (TbxFaciAdress.Text != "")
                        {
                            dr.SisetsuJusyo = TbxFaciAdress.Text;
                        }
                        if (TbxYubin.Text != "")
                        {
                            dr.SisetsuPost = TbxYubin.Text;
                        }
                        if (TbxFacilityCode.Text != "")
                        {
                            dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
                        }
                        if (TbxFacilityResponsible.Text != "")
                        {
                            dr.SisetsuTanto = TbxFacilityResponsible.Text;
                        }
                        if (RcbCity.SelectedValue != "")
                        {
                            dr.SisetsuCityCode = RcbCity.SelectedValue;
                        }
                        if (TbxFacilityName2.Text != "")
                        {
                            dr.SisetsuMei2 = TbxFacilityName2.Text;
                        }
                        if (TbxFaci.Text != "")
                        {
                            dr.SisetsuAbbreviration = TbxFaci.Text;
                        }
                        if (TbxTel.Text != "")
                        {
                            dr.SisetsuTell = TbxTel.Text;
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
                    if (SerchProduct.Text != "")
                    {
                        dr.SyouhinMei = SerchProduct.Text;
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
                        if (Hachu.SelectedValue != "" && Hachu.SelectedValue != "0")
                        {
                            dr.ShiiresakiCode = Hachu.SelectedValue;
                        }
                    }
                    if (WareHouse.Text != "")
                    {
                        dr.WareHouse = WareHouse.Text;
                    }
                    string a = RadComboCategory.SelectedValue;
                    CtlMitsuSyosai.Test4(a);
                    if (Label3.Text != "")
                    {
                        dr.Kakeritsu = Label3.Text;
                    }
                    else
                    {
                        dr.Kakeritsu = TbxKake.Text;
                    }
                    dr.Zeiku = RadZeiKubun.Text;
                    if (CheckBox4.Checked)
                    {
                        if (RadDatePicker3.SelectedDate != null)
                        {
                            dr.SiyouKaishi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                        }
                        if (RadDatePicker4.SelectedDate != null)
                        {
                            dr.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                        }
                    }
                    //drをdtに追加
                    dt.AddT_RowRow(dr);
                }
                DataMitumori.T_RowRow drN = dt.NewT_RowRow();
                string[] facCode = FacilityRad.SelectedValue.Split('/');
                DataMaster.M_Facility_NewRow drF = ClassMaster.GetFacilityRow(facCode, Global.GetConnection());
                if (drF != null)
                {
                    drN.SisetuCode = drF.FacilityNo;
                    drN.SisetsuMei1 = drF.FacilityName1;
                    if (!drF.IsFacilityName2Null())
                    {
                        drN.SisetsuMei2 = drF.FacilityName2;
                    }
                    if (!drF.IsAbbreviationNull())
                    {
                        drN.SisetsuAbbreviration = drF.Abbreviation;
                    }
                    if (!drF.IsPostNoNull())
                    {
                        drN.SisetsuPost = drF.PostNo;
                    }
                    if (!drF.IsAddress1Null())
                    {
                        drN.SisetsuJusyo = drF.Address1;
                    }
                    if (!drF.IsTellNull())
                    {
                        drN.SisetsuTell = drF.Tell;
                    }
                    if (!drF.IsCityCodeNull())
                    {
                        drN.SisetsuCityCode = drF.CityCode.ToString();
                    }
                    if (Label3.Text != "")
                    {
                        drN.Kakeritsu = Label3.Text;
                    }
                    else
                    {
                        drN.Kakeritsu = TbxKake.Text;
                    }
                    drN.Zeiku = RadZeiKubun.Text;
                    if (CheckBox4.Checked)
                    {
                        for (int i = 0; i < dt.Count; i++)
                        {
                            if (RadDatePicker3.SelectedDate != null)
                            {
                                drN.SiyouKaishi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                            }
                            if (RadDatePicker4.SelectedDate != null)
                            {
                                drN.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                            }
                        }
                    }
                }
                dt.AddT_RowRow(drN);
                //dtを維持しながらAddCreateに移動
                //AddCreate(dt);
                //何のデータを使うのかを指定する（この場合はdt）
                CtrlSyousai.DataSource = dt;
                //データバインドに飛ぶ
                CtrlSyousai.DataBind();

            }
            catch (Exception ex)
            {
                Err.Text = ex.ToString();
            }

        }
        //行追加------------------------------------------------------

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

                            TbxFacilityCode.Text = dr.FacilityNo.ToString();
                            ShiyouShisetsu.Text = dr.FacilityName1;
                            TbxFacilityName.Text = dr.FacilityName1;
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
                    if (FacilityRad.Text != "")
                    {
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

            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[0].FindControl("Syosai") as CtrlMitsuSyousai;
                RadComboBox serchproduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                serchproduct.Focus();
            }
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

        //行削除---------------------------------------------------------------------------------------
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
                    try
                    {
                        CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
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
                        Label LblProduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                        RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                        Label LblHanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");
                        TextBox TbxProductCode = (TextBox)CtlMitsuSyosai.FindControl("TbxProductCode");
                        TextBox TbxProductName = (TextBox)CtlMitsuSyosai.FindControl("TbxProductName");
                        TextBox TbxMakerNo = (TextBox)CtlMitsuSyosai.FindControl("TbxMakerNo");
                        RadComboBox RcbMedia = (RadComboBox)CtlMitsuSyosai.FindControl("RcbMedia");
                        TextBox TbxHanni = (TextBox)CtlMitsuSyosai.FindControl("TbxHanni");
                        Label LblCateCode = (Label)CtlMitsuSyosai.FindControl("LblCateCode");
                        Label LblCategoryName = (Label)CtlMitsuSyosai.FindControl("LblCategoryName");
                        TextBox TbxTyokuso = (TextBox)CtlMitsuSyosai.FindControl("TbxTyokuso");
                        TextBox TbxHyoujun = (TextBox)CtlMitsuSyosai.FindControl("TbxHyoujun");
                        Label LblShiireCode = (Label)CtlMitsuSyosai.FindControl("LblShiireCode");
                        RadComboBox RcbShiireName = (RadComboBox)CtlMitsuSyosai.FindControl("RcbShiireName");
                        TextBox TbxShiirePrice = (TextBox)CtlMitsuSyosai.FindControl("TbxShiirePrice");
                        TextBox TbxCpKakaku = (TextBox)CtlMitsuSyosai.FindControl("TbxCpKakaku");
                        TextBox TbxCpShiire = (TextBox)CtlMitsuSyosai.FindControl("TbxCpShiire");
                        RadDatePicker RdpCpStart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpStart");
                        RadDatePicker RdpCpEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpEnd");
                        TextBox TbxWareHouse = (TextBox)CtlMitsuSyosai.FindControl("TbxWareHouse");
                        RadDatePicker RdpPermissionstart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpPermissionstart");
                        RadDatePicker RdpRightEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpRightEnd");
                        TextBox TbxRiyo = (TextBox)CtlMitsuSyosai.FindControl("TbxRiyo");
                        RadComboBox Tekiyo = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                        TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                        TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                        RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                        TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                        TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                        TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                        TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                        TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                        TextBox TbxFacilityName = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName");
                        RadComboBox RcbHanni = (RadComboBox)CtlMitsuSyosai.FindControl("RcbHanni");
                        if (LblShiireCode.Text != "")
                        {
                            dr.ShiiresakiCode = LblShiireCode.Text;
                        }

                        if (TbxProductCode.Text != "")
                        {
                            dr.SyouhinCode = TbxProductCode.Text;
                        }

                        if (Tekiyo.Text != "")
                        {
                            dr.JutyuTekiyo = Tekiyo.Text;
                        }

                        if (LblCateCode.Text != "")
                        {
                            dr.CategoryCode = LblCateCode.Text;
                        }
                        if (LblCategoryName.Text != "")
                        {
                            dr.CategoryName = LblCategoryName.Text;
                        }
                        if (RdpCpStart.SelectedDate != null)
                        {
                            dr.Cpkaishi = RdpCpStart.SelectedDate.Value.ToShortDateString();
                        }
                        if (RdpCpEnd.SelectedDate != null)
                        {
                            dr.CpOwari = RdpCpEnd.SelectedDate.Value.ToShortDateString();
                        }

                        //Fukusu();
                        string s = RadZeiKubun.Text;
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
                        if (CheckBox4.Checked)
                        {
                            dr.SiyouKaishi = StartDate.SelectedDate.Value;
                            dr.SiyouOwari = EndDate.SelectedDate.Value;
                        }
                        else
                        {
                            if (StartDate.SelectedDate == null)
                            { }
                            else
                            { dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString()); }
                            if (EndDate.SelectedDate == null)
                            { }
                            else
                            { dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString()); }
                        }

                        //if (ProductName.Text != "")
                        //{
                        //    dr.SyouhinMei = ProductName.Text;
                        //}

                        if (LblProduct.Text != "")
                        {
                            dr.MekarHinban = LblProduct.Text;
                        }
                        if (TbxHyoujun.Text != "")
                        {
                            dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
                        }
                        if (RcbHanni.Text != "")
                        {
                            dr.Range = RcbHanni.Text;
                        }
                        if (LblHanni.Text != "")
                        {
                            dr.Range = LblHanni.Text;
                        }
                        if (ShiyouShisetsu.Text == "")
                        {
                            if (Facility.Text != "")
                            {
                                dr.Basyo = Facility.Text;
                            }
                            else
                            {
                                if (KariFaci.Text != "")
                                {
                                    dr.Basyo = KariFaci.Text;
                                }
                            }
                        }
                        else
                        {
                            dr.Basyo = ShiyouShisetsu.Text;
                            if (TbxFaciAdress.Text != "")
                            {
                                dr.SisetsuJusyo = TbxFaciAdress.Text;
                            }
                            if (TbxYubin.Text != "")
                            {
                                dr.SisetsuPost = TbxYubin.Text;
                            }
                            if (TbxFacilityCode.Text != "")
                            {
                                dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
                            }
                            if (TbxFacilityResponsible.Text != "")
                            {
                                dr.SisetsuTanto = TbxFacilityResponsible.Text;
                            }
                            if (RcbCity.SelectedValue != "")
                            {
                                dr.SisetsuCityCode = RcbCity.SelectedValue;
                            }
                            if (TbxFacilityName2.Text != "")
                            {
                                dr.SisetsuMei2 = TbxFacilityName2.Text;
                            }
                            if (TbxFaci.Text != "")
                            {
                                dr.SisetsuAbbreviration = TbxFaci.Text;
                            }
                            if (TbxTel.Text != "")
                            {
                                dr.SisetsuTell = TbxTel.Text;
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
                        if (SerchProduct.Text != "")
                        {
                            dr.SyouhinMei = SerchProduct.Text;
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
                        string b = RadComboCategory.SelectedValue;
                        CtlMitsuSyosai.Test4(b);
                        if (Label3.Text != "")
                        {
                            dr.Kakeritsu = Label3.Text;
                        }
                        else
                        {
                            dr.Kakeritsu = TbxKake.Text;
                        }
                        dr.Zeiku = RadZeiKubun.Text;
                        if (CheckBox4.Checked)
                        {
                            if (RadDatePicker3.SelectedDate != null)
                            {
                                dr.SiyouKaishi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                            }
                            if (RadDatePicker4.SelectedDate != null)
                            {
                                dr.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                            }
                        }
                        string[] facCode = FacilityRad.SelectedValue.Split('/');
                        DataMaster.M_Facility_NewRow drF = ClassMaster.GetFacilityRow(facCode, Global.GetConnection());
                        if (drF != null)
                        {
                            dr.SisetuCode = drF.FacilityNo;
                            dr.SisetsuMei1 = drF.FacilityName1;
                            if (!drF.IsFacilityName2Null())
                            {
                                dr.SisetsuMei2 = drF.FacilityName2;
                            }
                            if (!drF.IsAbbreviationNull())
                            {
                                dr.SisetsuAbbreviration = drF.Abbreviation;
                            }
                            if (!drF.IsPostNoNull())
                            {
                                dr.SisetsuPost = drF.PostNo;
                            }
                            if (!drF.IsAddress1Null())
                            {
                                dr.SisetsuJusyo = drF.Address1;
                            }
                            if (!drF.IsTellNull())
                            {
                                dr.SisetsuTell = drF.Tell;
                            }
                            if (!drF.IsCityCodeNull())
                            {
                                dr.SisetsuCityCode = drF.CityCode.ToString();
                            }
                            if (Label3.Text != "")
                            {
                                dr.Kakeritsu = Label3.Text;
                            }
                            else
                            {
                                dr.Kakeritsu = TbxKake.Text;
                            }
                            dr.Zeiku = RadZeiKubun.Text;
                            if (CheckBox4.Checked)
                            {
                                for (int u = 0; u < dt.Count; u++)
                                {
                                    if (RadDatePicker3.SelectedDate != null)
                                    {
                                        dr.SiyouKaishi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                                    }
                                    if (RadDatePicker4.SelectedDate != null)
                                    {
                                        dr.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                                    }
                                }
                            }
                        }
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
            //AddCreate(dt);
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
            Response.Redirect("Mitumori/MitumoriItiran.aspx");
        }

        //計算ボタン-----------------------------------------------
        protected void Keisan_Click(object sender, EventArgs e)
        {
            Keisan2();
        }
        //計算ボタン-----------------------------------------------

        //計算------------------
        private void Keisan2()
        {
            int t = 0;
            int s = 0;
            int a = 0;
            int r = 0;
            double t1 = 0;
            double a1 = 0;
            double s1 = 0;
            int su = 0;
            int ta = 0;
            int si = 0;
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                try
                {
                    CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                    TextBox HyoujyunTanka = (TextBox)CtlMitsuSyosai.FindControl("HyoujyunTanka");
                    TextBox Kingaku = (TextBox)CtlMitsuSyosai.FindControl("Kingaku");
                    TextBox ShiireTanka = (TextBox)CtlMitsuSyosai.FindControl("ShiireTanka");
                    TextBox ShiireKingaku = (TextBox)CtlMitsuSyosai.FindControl("ShiireKingaku");
                    TextBox Tanka = (TextBox)CtlMitsuSyosai.FindControl("Tanka");
                    TextBox Uriage = (TextBox)CtlMitsuSyosai.FindControl("Uriage");
                    TextBox Suryou = (TextBox)CtlMitsuSyosai.FindControl("Suryo");
                    Label Kakeritsu = (Label)CtlMitsuSyosai.FindControl("Kakeri");
                    HtmlInputHidden ht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ht");
                    HtmlInputHidden st = (HtmlInputHidden)CtlMitsuSyosai.FindControl("st");
                    HtmlInputHidden tk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("tk");
                    HtmlInputHidden ug = (HtmlInputHidden)CtlMitsuSyosai.FindControl("ug");
                    HtmlInputHidden kgk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("kgk");
                    HtmlInputHidden sk = (HtmlInputHidden)CtlMitsuSyosai.FindControl("sk");
                    HtmlInputHidden zeiht = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeiht");
                    HtmlInputHidden zeist = (HtmlInputHidden)CtlMitsuSyosai.FindControl("zeist");
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    Label LblProduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    if (LblProduct.Text == "0")
                    {
                        string uri = Uriage.Text.Replace(",", "");
                        t += int.Parse(uri);
                        TextBox7.Text = t.ToString("0,0");
                        if (RadZeiKubun.Text == "税込")
                        {
                            a = t - s;
                            TextBox6.Text = a.ToString("0,0");
                            int rieki = a * 100 / t;
                            double zei = t * 0.1;
                            double zei2 = Math.Floor(zei);
                            TextBox5.Text = zei2.ToString("0,0");
                            TextBox6.Text = a.ToString("0,0");
                            TextBox13.Text = rieki.ToString();
                        }
                        else
                        {
                            a = t - s;
                            TextBox6.Text = a.ToString("0,0");
                            int rieki = a * 100 / t;
                            double zei = t / 10;
                            TextBox5.Text = zei.ToString("0,0");
                            double tt = t;
                            double gokei = tt + zei;
                            TextBox12.Text = gokei.ToString("0,0");
                            TextBox6.Text = a.ToString("0,0");
                            TextBox13.Text = rieki.ToString();
                        }
                    }
                    else
                    {
                        //int hyou = int.Parse(HyoujyunTanka.Text.Replace(",", ""));
                        su += int.Parse(Suryou.Text);
                        int tan = int.Parse(Tanka.Text.Replace(",", ""));
                        if (tan != 0)
                        {
                            string uri = Uriage.Text.Replace(",", "");
                            string shi = ShiireKingaku.Text.Replace(",", "");
                            ta += int.Parse(tan.ToString()) * su;
                            Uriage.Text = ta.ToString("0,0");
                            si += int.Parse(shi);
                            if (RadZeiKubun.Text == "税込")
                            {
                                TextBox12.Visible = false;
                                t += ta;
                                s += si;
                                r += su;
                                a = t - s;
                                TextBox7.Text = t.ToString("0,0");
                                TextBox8.Text = s.ToString("0,0");
                                TextBox10.Text = r.ToString();
                                TextBox6.Text = a.ToString("0,0");
                                int rieki = a * 100 / t;
                                TextBox13.Text = rieki.ToString();
                                double zei = t * 0.1;
                                double zei2 = Math.Floor(zei);
                                TextBox5.Text = zei2.ToString("0,0");
                            }
                            else
                            {
                                r += su;
                                t += ta;
                                s += si;
                                a = t - s;
                                TextBox10.Text = r.ToString();
                                TextBox7.Text = t.ToString("0,0");
                                TextBox8.Text = s.ToString("0,0");
                                TextBox6.Text = a.ToString("0,0");
                                double zei = t / 10;
                                TextBox5.Text = zei.ToString("0,0");
                                int rieki = a * 100 / t;
                                TextBox13.Text = rieki.ToString();
                                double tt = t;
                                double gokei = tt + zei;
                                TextBox12.Text = gokei.ToString("0,0");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Err.Text = ex.Message;
                }
                Button5.Focus();
            }
        }
        //計算------------------

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
            //Create();
        }
        //複写-----------------------------------------------------

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            //        System.Windows.Forms.DialogResult result =
            //System.Windows.Forms.MessageBox.Show(
            //"見積No" + SessionManager.HACCYU_NO + "を削除しますか？", "ALERT",
            //System.Windows.Forms.MessageBoxButtons.YesNo,
            //System.Windows.Forms.MessageBoxIcon.Exclamation,
            //System.Windows.Forms.MessageBoxDefaultButton.Button2);
            //        if (System.Windows.Forms.DialogResult.No == result)
            //        {
            //            return;
            //        }
            //        if (System.Windows.Forms.DialogResult.Yes == result)
            //        {
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
                Keisan2();
                AllFukusu();
                DataMitumori.T_RowDataTable df = new DataMitumori.T_RowDataTable();

                //TokuisakiCode.Value = RadComboBox1.SelectedValue;
                TyokusoCode.Value = RadComboBox2.SelectedValue;
                //見積ヘッダー部分登録

                DataJutyu.T_JutyuHeaderDataTable dp = new DataJutyu.T_JutyuHeaderDataTable();
                DataJutyu.T_JutyuHeaderRow dl = dp.NewT_JutyuHeaderRow();
                DataMitumori.T_MitumoriHeaderDataTable dw = ClassMitumori.GetMitumoriHeader(Label94.Text, Global.GetConnection());

                if (RadDatePicker2.SelectedDate != null)
                {
                    dl.CareateDate = RadDatePicker2.SelectedDate.Value;
                }

                if (TextBox7.Text != "")
                {
                    string r1 = TextBox7.Text.Replace(",", "");
                    dl.SoukeiGaku = int.Parse(r1);
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

                if (CheckBox1.Checked == true)
                {
                    dl.TokuisakiName = KariTokui.Text;
                    //dl.TokuisakiCode = TextBox19.Text;
                }
                else
                {
                    if (RadComboBox1.Text != "")
                    {
                        dl.TokuisakiName = RadComboBox1.Text;
                        dl.TokuisakiCode = CustomerCode.Value + "/" + TokuisakiCode.Value;
                        //DataSet1.M_Tokuisaki2DataTable du = Class1.GetTokuisaki2(CustomerCode.Value, TokuisakiCode.Value, Global.GetConnection());
                        //for (int i = 0; i < du.Count; i++)
                        //{
                        //    dl.TokuisakiCode = du[i].CustomerCode +"/"+ du[i].TokuisakiCode.ToString();
                        //}
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
                if (Label1.Text != "")
                {
                    dl.TantoName = Label1.Text;
                }
                dl.Relay = "受注";
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

                if (RadDatePicker3.SelectedDate != null && RadDatePicker3.Visible)
                {
                    dl.SiyouKaisi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                }

                if (RadDatePicker4.SelectedDate != null && RadDatePicker4.Visible)
                {
                    dl.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                }

                if (TextBox2.Text != "")
                {
                    dl.Bikou = TextBox2.Text;
                }

                dl.CareateDate = RadDatePicker2.SelectedDate.Value;

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
                dl.HatyuFlg = false;
                dl.CareateDate = DateTime.Now;
                dp.AddT_JutyuHeaderRow(dl);
                ClassJutyu.InsertJutyuH2(dp, Global.GetConnection());
                dw[0].JutyuFlg = true.ToString();
                dw[0].Relay = "受注";
                ClassMitumori.UpdateMitumoriHeader(mno, dw, Global.GetConnection());

                DataJutyu.T_JutyuDataTable dg = new DataJutyu.T_JutyuDataTable();
                DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu(Global.GetConnection());
                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = dg.NewT_JutyuRow();
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
                    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                    TextBox TbxFacilityName = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName");
                    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                    RadComboBox RcbHanni = (RadComboBox)CtlMitsuSyosai.FindControl("RcbHanni");

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

                    if (ShiyouShisetsu.Text != "")
                    {
                        dr.SisetuMei = ShiyouShisetsu.Text;
                    }

                    dr.JutyuSuryou = int.Parse(Suryo.Text);
                    dr.TanTouName = Label1.Text;
                    dr.RowNo = i + 1;

                    if (RadZeiKubun.Text != "")
                    {
                        dr.ZeiKubun = RadZeiKubun.Text;
                    }
                    if (kakeri.Text != "")
                    {
                        dr.Kakeritsu = kakeri.Text;
                    }
                    if (Lblproduct.Text != "")
                    {
                        dr.MekarHinban = Lblproduct.Text;
                    }
                    if (SerchProduct.Text != "")
                    {
                        string cate = RadComboCategory.Text;
                        string hanni = LblHanni.Text;
                        dr.SyouhinMei = SerchProduct.Text;
                        //dr.SyouhinCode = arr[1];
                        DataSet1.M_Kakaku_2DataTable dk = Class1.getproduct(SerchProduct.Text, cate, hanni, Global.GetConnection());
                        for (int p = 0; p < dk.Count; p++)
                        {
                            dr.SyouhinCode = dk[p].SyouhinCode;
                        }
                    }
                    if (LblHanni.Text != "")
                    {
                        dr.Range = LblHanni.Text;
                    }
                    if (RcbHanni.Text != "")
                    {
                        dr.Range = RcbHanni.Text;
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
                        if (HyoujyunTanka.Text != "OPEN")
                        {
                            string hyoutan = HyoujyunTanka.Text;
                            string r1 = hyoutan.Replace(",", "");
                            dr.HyojunKakaku = int.Parse(r1);
                        }
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
                        dr.TyokusousakiMei = CustomerCode.Value;
                        // dr.TyokusousakiCD = int.Parse(TyokusoCode.Value);
                    }
                    if (CheckBox4.Checked == true)
                    {
                        dr.SiyouKaishi = StartDate.SelectedDate.Value;
                        dr.SiyouOwari = EndDate.SelectedDate.Value;
                    }
                    if (CheckBox4.Checked == false)
                    {
                        if (StartDate.SelectedDate != null)
                        {
                            dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString());
                        }
                        if (EndDate.SelectedDate != null)
                        {
                            dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString());
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

                    if (RadDatePicker1.SelectedDate != null)
                    {
                        dr.StartDate = RadDatePicker1.SelectedDate.Value;
                    }

                    if (RadComboBox1.Text != "")
                    {
                        dr.TokuisakiMei = RadComboBox1.Text;
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

                    if (CheckBox1.Checked == true)
                    {
                        dr.SisetuMei = KariFaci.Text;
                        dr.SisetuCode = int.Parse(FacilityCode.Value);
                        dr.SisetuJusyo1 = FacAdd.Value;
                        dr.TokuisakiMei = KariTokui.Text;
                        //dr.TokuisakiCode = TextBox19.Text;
                    }

                    if (Hachu.Text != "")
                    {
                        dr.HattyuSakiMei = Hachu.Text;
                    }
                    if (Label1.Text != "")
                    {
                        dr.TanTouName = Label1.Text;
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

                    dr.JutyuNo = int.Parse(no);
                    dr.UriageFlg = false;
                    dg.AddT_JutyuRow(dr);
                }
                ClassJutyu.InsertJutyu2(dg, Global.GetConnection());
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

        protected void Button10_Click1(object sender, EventArgs e)
        {
            //if (TbxCustomer.Text != "")
            //{
            //    CustomerCode.Value = TbxCustomer.Text;
            //}
            //if (TextBox19.Text != "")
            //{
            //    TokuisakiCode.Value = TextBox19.Text;
            //}
            //if (TextBox20.Text != "")
            //{
            //    RadComboBox1.Text = TextBox20.Text;
            //}
            //if (TbxKakeritsu.Text != "")
            //{
            //    Label3.Text = TbxKakeritsu.Text;
            //}
            //if (RadShimebi.SelectedValue != "")
            //{
            //    Shimebi.Text = RadShimebi.Text;
            //}
            //if (RadTax.SelectedValue != "")
            //{
            //    RadZeiKubun.SelectedItem.Text = RadTax.SelectedItem.Text.Trim();
            //}
            //if (rcbtest.Text != "")
            //{
            //    Label1.Text = rcbtest.Text;
            //}
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
            //Button1.Attributes["OnBlur"] = string.Format("TokuisakiSyousai('{0}');", Button1.ClientID);
            TBTokuisaki.Style["display"] = "";
            mInput.Visible = false;
            CtrlSyousai.Visible = false;
            AddBtn.Visible = false;
            Button13.Visible = false;
            head.Visible = false;
            SubMenu.Visible = false;
            SubMenu2.Style["display"] = "";
            RadComboBox5.Style["display"] = "";
            RcbTax.Style["display"] = "";
            RcbShimebi.Style["display"] = "";
            BtnCopyAdd.Style["display"] = "none";
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
                Label1.Text = dt[0].UserName;
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
            mInput.Visible = true;
            CtrlSyousai.Visible = true;
            AddBtn.Visible = true;
            SubMenu.Visible = true;
            SubMenu2.Style["display"] = "none";
            RadComboBox5.Style["display"] = "none";
            Button13.Visible = true;
            BtnCopyAdd.Style["display"] = "";
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
            LblTantoStaffCode.Text = RadComboBox5.SelectedValue;
        }

        protected void BtnCopyAdd_Click(object sender, EventArgs e)
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
                    Label LblProduct = (Label)CtlMitsuSyosai.FindControl("LblProduct");
                    RadComboBox SerchProduct = (RadComboBox)CtlMitsuSyosai.FindControl("SerchProduct");
                    Label LblHanni = (Label)CtlMitsuSyosai.FindControl("LblHanni");
                    TextBox TbxProductCode = (TextBox)CtlMitsuSyosai.FindControl("TbxProductCode");
                    TextBox TbxProductName = (TextBox)CtlMitsuSyosai.FindControl("TbxProductName");
                    TextBox TbxMakerNo = (TextBox)CtlMitsuSyosai.FindControl("TbxMakerNo");
                    RadComboBox RcbMedia = (RadComboBox)CtlMitsuSyosai.FindControl("RcbMedia");
                    TextBox TbxHanni = (TextBox)CtlMitsuSyosai.FindControl("TbxHanni");
                    Label LblCateCode = (Label)CtlMitsuSyosai.FindControl("LblCateCode");
                    Label LblCategoryName = (Label)CtlMitsuSyosai.FindControl("LblCategoryName");
                    TextBox TbxTyokuso = (TextBox)CtlMitsuSyosai.FindControl("TbxTyokuso");
                    TextBox TbxHyoujun = (TextBox)CtlMitsuSyosai.FindControl("TbxHyoujun");
                    Label LblShiireCode = (Label)CtlMitsuSyosai.FindControl("LblShiireCode");
                    RadComboBox RcbShiireName = (RadComboBox)CtlMitsuSyosai.FindControl("RcbShiireName");
                    TextBox TbxShiirePrice = (TextBox)CtlMitsuSyosai.FindControl("TbxShiirePrice");
                    TextBox TbxCpKakaku = (TextBox)CtlMitsuSyosai.FindControl("TbxCpKakaku");
                    TextBox TbxCpShiire = (TextBox)CtlMitsuSyosai.FindControl("TbxCpShiire");
                    RadDatePicker RdpCpStart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpStart");
                    RadDatePicker RdpCpEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpCpEnd");
                    TextBox TbxWareHouse = (TextBox)CtlMitsuSyosai.FindControl("TbxWareHouse");
                    RadDatePicker RdpPermissionstart = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpPermissionstart");
                    RadDatePicker RdpRightEnd = (RadDatePicker)CtlMitsuSyosai.FindControl("RdpRightEnd");
                    TextBox TbxRiyo = (TextBox)CtlMitsuSyosai.FindControl("TbxRiyo");
                    RadComboBox Tekiyo = (RadComboBox)CtlMitsuSyosai.FindControl("Tekiyo");
                    TextBox TbxFaciAdress = (TextBox)CtlMitsuSyosai.FindControl("TbxFaciAdress");
                    TextBox TbxYubin = (TextBox)CtlMitsuSyosai.FindControl("TbxYubin");
                    RadComboBox RcbCity = (RadComboBox)CtlMitsuSyosai.FindControl("RcbCity");
                    TextBox TbxFacilityCode = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityCode");
                    TextBox TbxFacilityResponsible = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityResponsible");
                    TextBox TbxFacilityName2 = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName2");
                    TextBox TbxFaci = (TextBox)CtlMitsuSyosai.FindControl("TbxFaci");
                    TextBox TbxTel = (TextBox)CtlMitsuSyosai.FindControl("TbxTel");
                    TextBox TbxFacilityName = (TextBox)CtlMitsuSyosai.FindControl("TbxFacilityName");
                    RadComboBox RcbHanni = (RadComboBox)CtlMitsuSyosai.FindControl("RcbHanni");

                    if (LblShiireCode.Text != "")
                    {
                        dr.ShiiresakiCode = LblShiireCode.Text;
                    }

                    if (TbxProductCode.Text != "")
                    {
                        dr.SyouhinCode = TbxProductCode.Text;
                    }

                    if (Tekiyo.Text != "")
                    {
                        dr.JutyuTekiyo = Tekiyo.Text;
                    }

                    if (LblCateCode.Text != "")
                    {
                        dr.CategoryCode = LblCateCode.Text;
                    }
                    if (LblCategoryName.Text != "")
                    {
                        dr.CategoryName = LblCategoryName.Text;
                    }
                    if (RdpCpStart.SelectedDate != null)
                    {
                        dr.Cpkaishi = RdpCpStart.SelectedDate.Value.ToShortDateString();
                    }
                    if (RdpCpEnd.SelectedDate != null)
                    {
                        dr.CpOwari = RdpCpEnd.SelectedDate.Value.ToShortDateString();
                    }
                    if (RcbHanni.Text != "")
                    {
                        dr.Range = RcbHanni.Text;
                    }

                    //Fukusu();
                    string s = RadZeiKubun.Text;
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
                    if (CheckBox4.Checked)
                    {
                        dr.SiyouKaishi = StartDate.SelectedDate.Value;
                        dr.SiyouOwari = EndDate.SelectedDate.Value;
                    }
                    else
                    {
                        if (StartDate.SelectedDate == null)
                        { }
                        else
                        { dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString()); }
                        if (EndDate.SelectedDate == null)
                        { }
                        else
                        { dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString()); }
                    }

                    //if (ProductName.Text != "")
                    //{
                    //    dr.SyouhinMei = ProductName.Text;
                    //}

                    if (LblProduct.Text != "")
                    {
                        dr.MekarHinban = LblProduct.Text;
                    }
                    if (HyoujyunTanka.Text != "")
                    {
                        if (HyoujyunTanka.Text != "OPEN")
                        {
                            string hyoutan = HyoujyunTanka.Text;
                            string r1 = hyoutan.Replace(",", "");
                            dr.HyojunKakaku = r1;
                        }
                    }
                    if (LblHanni.Text != "")
                    {
                        dr.Range = LblHanni.Text;
                    }
                    if (ShiyouShisetsu.Text == "")
                    {
                        if (Facility.Text != "")
                        {
                            dr.Basyo = Facility.Text;
                        }
                        else
                        {
                            if (KariFaci.Text != "")
                            {
                                dr.Basyo = KariFaci.Text;
                            }
                        }
                    }
                    else
                    {
                        dr.Basyo = ShiyouShisetsu.Text;
                        if (TbxFaciAdress.Text != "")
                        {
                            dr.SisetsuJusyo = TbxFaciAdress.Text;
                        }
                        if (TbxYubin.Text != "")
                        {
                            dr.SisetsuPost = TbxYubin.Text;
                        }
                        if (TbxFacilityCode.Text != "")
                        {
                            dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
                        }
                        if (TbxFacilityResponsible.Text != "")
                        {
                            dr.SisetsuTanto = TbxFacilityResponsible.Text;
                        }
                        if (RcbCity.SelectedValue != "")
                        {
                            dr.SisetsuCityCode = RcbCity.SelectedValue;
                        }
                        if (TbxFacilityName2.Text != "")
                        {
                            dr.SisetsuMei2 = TbxFacilityName2.Text;
                        }
                        if (TbxFaci.Text != "")
                        {
                            dr.SisetsuAbbreviration = TbxFaci.Text;
                        }
                        if (TbxTel.Text != "")
                        {
                            dr.SisetsuTell = TbxTel.Text;
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
                    if (SerchProduct.Text != "")
                    {
                        dr.SyouhinMei = SerchProduct.Text;
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
                        if (Hachu.SelectedValue != "")
                        {
                            dr.ShiiresakiCode = Hachu.SelectedValue;
                        }
                    }
                    if (WareHouse.Text != "")
                    {
                        dr.WareHouse = WareHouse.Text;
                    }
                    string a = RadComboCategory.SelectedValue;
                    CtlMitsuSyosai.Test4(a);
                    if (Label3.Text != "")
                    {
                        dr.Kakeritsu = Label3.Text;
                    }
                    else
                    {
                        dr.Kakeritsu = TbxKake.Text;
                    }
                    dr.Zeiku = RadZeiKubun.Text;
                    if (CheckBox4.Checked)
                    {
                        if (RadDatePicker3.SelectedDate != null)
                        {
                            dr.SiyouKaishi = DateTime.Parse(RadDatePicker3.SelectedDate.ToString());
                        }
                        if (RadDatePicker4.SelectedDate != null)
                        {
                            dr.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                        }
                    }
                    //drをdtに追加
                    dt.AddT_RowRow(dr);
                    if (i == CtrlSyousai.Items.Count - 1)
                    {
                        DataMitumori.T_RowRow drNC = dt.NewT_RowRow();
                        drNC.ItemArray = dr.ItemArray;
                        dt.AddT_RowRow(drNC);
                    }
                    string[] facCode = FacilityRad.SelectedValue.Split('/');
                    DataMaster.M_Facility_NewRow drF = ClassMaster.GetFacilityRow(facCode, Global.GetConnection());
                    if (drF != null)
                    {
                        dt[i].SisetuCode = drF.FacilityNo;
                        dt[i].SisetsuMei1 = drF.FacilityName1;
                        if (!drF.IsFacilityName2Null())
                        {
                            dt[i].SisetsuMei2 = drF.FacilityName2;
                        }
                        if (!drF.IsAbbreviationNull())
                        {
                            dt[i].SisetsuAbbreviration = drF.Abbreviation;
                        }
                        if (!drF.IsPostNoNull())
                        {
                            dt[i].SisetsuPost = drF.PostNo;
                        }
                        if (!drF.IsAddress1Null())
                        {
                            dt[i].SisetsuJusyo = drF.Address1;
                        }
                        if (!drF.IsTellNull())
                        {
                            dt[i].SisetsuTell = drF.Tell;
                        }
                        if (!drF.IsCityCodeNull())
                        {
                            dt[i].SisetsuCityCode = drF.CityCode.ToString();
                        }
                        if (Label3.Text != "")
                        {
                            dt[i].Kakeritsu = Label3.Text;
                        }
                        else
                        {
                            dt[i].Kakeritsu = TbxKake.Text;
                        }
                        dt[i].Zeiku = RadZeiKubun.Text;
                        if (CheckBox4.Checked)
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
                //dtを維持しながらAddCreateに移動
                //AddCreate(dt);
                //何のデータを使うのかを指定する（この場合はdt）
                CtrlSyousai.DataSource = dt;
                //データバインドに飛ぶ
                CtrlSyousai.DataBind();

            }
            catch (Exception ex)
            {
                Err.Text = ex.ToString();
            }
        }
    }
}

