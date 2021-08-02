using DLL;
using System;
using Telerik.Web.UI;
using System.Windows.Forms;

namespace Gyomu
{
    public partial class CtrlMitsuSyousai : System.Web.UI.UserControl
    {
        // public event System.Windows.Forms.KeyPressEventHandler KeyPress;

        protected void Page_Load(object sender, EventArgs e)
        {
            SyouhinSyousai.Style["display"] = "none";
            SisetuSyousai.Style["display"] = "none";
            RcbHanni.Style["display"] = "none";
            err.Text = "";
            end.Text = "";
        }

        private void MakerHinban_keypressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < '0' || e.KeyChar > '9')
                e.Handled = true;
        }

        internal bool FocusRoute()
        {
            if (SerchProduct.Text == "")
            {
                SerchProduct.Focus();
                return true;
            }
            else
            {

                if (Zasu.Visible && Zasu.Text == "")
                {
                    Zasu.Focus();
                    return true;
                }
                else
                {
                    if (StartDate.Visible && StartDate.SelectedDate == null)
                    {
                        StartDate.Focus();
                        return true;
                    }
                    else
                    {
                        if (EndDate.Visible && EndDate.SelectedDate == null)
                        {
                            EndDate.Focus();
                            return true;
                        }
                        else
                        {
                            if (ShiyouShisetsu.Visible && ShiyouShisetsu.Text == "")
                            {
                                ShiyouShisetsu.Focus();
                                return true;
                            }

                            if (KariFaci.Visible && KariFaci.Text == "")
                            {
                                KariFaci.Focus();
                                return true;
                            }

                        }
                    }
                }

            }
            return false;
        }

        private void Tanka_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void Uriage_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        internal void CtlKeisan(string kakakuhyoujyun, string kakakushiire, double t)
        {
            string[] syouhinCode = SerchProduct.SelectedValue.Split('/');
            if (99999000 <= int.Parse(syouhinCode[0]) && int.Parse(syouhinCode[0]) <= 99999999)
            {

            }
            else
            {
                if (zeiku.Text == "税込")
                {
                    string ht = kakakuhyoujyun;
                    TbxHyoujun.Text = int.Parse(kakakuhyoujyun).ToString("0,0");
                    string st = kakakushiire;
                    TbxShiirePrice.Text = int.Parse(kakakushiire).ToString("0,0");
                    //標準単価
                    int x = int.Parse(ht);
                    int y = int.Parse(st);
                    double xx = x * 1.1;
                    HyoujyunTanka.Text = xx.ToString("0,0");
                    //標準金額
                    double go = xx * int.Parse(Suryo.Text);
                    Kingaku.Text = go.ToString("0,0");
                    //単価
                    double m = t * 1.1;
                    Tanka.Text = m.ToString("0,0");
                    //仕入単価
                    double n = y * 1.1;
                    ShiireTanka.Text = n.ToString("0,0");
                    //数量も計算した売上・仕入金額
                    double g = m * int.Parse(Suryo.Text);
                    double h = n * int.Parse(Suryo.Text);
                    string tk = Tanka.Text.Replace(",", "");
                    int d = int.Parse(tk) * int.Parse(Suryo.Text);
                    ug.Value = d.ToString();
                    zeiht.Value = g.ToString();
                    zeist.Value = h.ToString();
                    Uriage.Text = g.ToString("0,0");
                    zeikgk.Value = g.ToString();
                    ShiireKingaku.Text = h.ToString("0,0");
                    zeisk.Value = h.ToString();
                }
                else
                {
                    string ht = kakakuhyoujyun;
                    TbxHyoujun.Text = int.Parse(kakakuhyoujyun).ToString("0,0");
                    string st = kakakushiire;
                    TbxShiirePrice.Text = int.Parse(kakakushiire).ToString("0,0");
                    int x = int.Parse(ht);
                    int y = int.Parse(st);
                    Tanka.Text = t.ToString("0,0");
                    HyoujyunTanka.Text = x.ToString("0,0");
                    ShiireTanka.Text = y.ToString("0,0");
                    double g = x * int.Parse(Suryo.Text);
                    double h = y * int.Parse(Suryo.Text);
                    int d = int.Parse(t.ToString()) * int.Parse(Suryo.Text);
                    Kingaku.Text = g.ToString("0,0");
                    Uriage.Text = d.ToString("0,0");
                    kgk.Value = g.ToString();
                    ShiireKingaku.Text = h.ToString("0,0");
                    sk.Value = h.ToString();
                }
            }
        }

        internal void kari(bool ckb)
        {
            if (ckb == true)
            {
                ShiyouShisetsu.Visible = false;
                KariFaci.Visible = true;
            }
            else
            {
                ShiyouShisetsu.Visible = true;
                KariFaci.Visible = false;
            }
        }

        //public void Test2(string s, bool s1)
        //{
        //    if (s == "公共図書館")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = false;
        //        EndDate.Visible = false;

        //    }

        //    if (s == "学校図書館")
        //    {

        //        Zasu.Visible = false;
        //        StartDate.Visible = false;
        //        EndDate.Visible = false;

        //    }

        //    if (s == "その他図書館")
        //    {

        //        Zasu.Visible = false;
        //        StartDate.Visible = false;
        //        EndDate.Visible = false;


        //    }

        //    if (s == "防衛省")
        //    {

        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }
        //    }

        //    if (s == "ホテル")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }


        //    if (s == "レジャーホテル")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "バス")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "船舶")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }
        //    if (s == "上映会")
        //    {
        //        Zasu.Visible = true;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "複合カフェ")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "健康ランド")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "福祉施設")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "キッズ・BGV")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "その他")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "VOD")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }

        //    }

        //    if (s == "VOD配信")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }


        //    }

        //    if (s == "DOD")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }


        //    }
        //    if (s == "DEX")
        //    {
        //        Zasu.Visible = false;
        //        StartDate.Visible = true;
        //        EndDate.Visible = true;
        //        if (s1 == true)
        //        {
        //            StartDate.Visible = false;
        //            EndDate.Visible = false;
        //        }


        //    }

        //}

        internal void Zeikei(string s)
        {
            zeikubun.Value = s;
        }

        internal void Hiduke(bool s)
        {
            if (s == false)
            {
                StartDate.Visible = true;
                EndDate.Visible = true;

            }
            else
            {
                StartDate.Visible = false;
                EndDate.Visible = false;
            }
        }

        private void GetKensakuProduct(ClassKensaku.KensakuParam s)
        {
            TbxProductCode.Text = "";
            LblShiireCode.Text = "";
            RcbShiireName.Text = "";
            TbxMakerNo.Text = "";
            TbxShiirePrice.Text = "";
            RcbMedia.Text = "";
            TbxCpKakaku.Text = "";
            TbxHanni.Text = "";
            TbxCpShiire.Text = "";
            LblCateCode.Text = "";
            RdpCpStart.SelectedDate = null;
            LblCategoryName.Text = "";
            RdpCpEnd.SelectedDate = null;
            TbxTyokuso.Text = "";
            TbxWareHouse.Text = "";
            TbxHyoujun.Text = "";
            TbxRiyo.Text = "";
            RdpPermissionstart.SelectedDate = null;
            RdpRightEnd.SelectedDate = null;

            try
            {
                string cate = "";
                string syouhinCode = "";
                string media = "";
                string hanni = "";
                string str = SerchProduct.SelectedValue;
                string[] arr = str.Split('/');
                syouhinCode += arr[0];
                media += arr[1];
                hanni += arr[2];

                cate = category.Value;

                DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.Getproduct6(syouhinCode, cate, media, hanni, Global.GetConnection());
                for (int j = 0; j < dt.Count; j++)
                {
                    DataSet1.M_Kakaku_2Row dr = dt.Rows[j] as DataSet1.M_Kakaku_2Row;
                    Baitai.Text = media;
                    RcbMedia.SelectedValue = media;
                    LblCateCode.Text = dr.CategoryCode.ToString();
                    LblCategoryName.Text = dr.Categoryname.ToString();
                    //HyoujyunTanka.Text = dr.HyojunKakaku.ToString("0,0");
                    string hk = dr.HyoujunKakaku.ToString();
                    ht.Value = hk;
                    LblProduct.Text = dr.Makernumber;
                    TbxMakerNo.Text = dr.Makernumber;

                    if (!dr.IsPermissionStartNull())
                    {
                        RdpPermissionstart.SelectedDate = DateTime.Parse(dr.PermissionStart);
                    }
                    if (!dr.IsRightEndNull() && dr.RightEnd != "")
                    {
                        string d = dr.RightEnd;
                        RdpRightEnd.SelectedDate = DateTime.Parse(dr.RightEnd);
                    }

                    if (!dr.IsCpKaisiNull() && dr.CpKaisi != "")
                    {
                        RdpCpStart.SelectedDate = DateTime.Parse(dr.CpKaisi);
                    }
                    if (!dr.IsCpOwariNull() && dr.CpOwari != "")
                    {
                        RdpCpEnd.SelectedDate = DateTime.Parse(dr.CpOwari);
                    }

                    if (cate == 205.ToString())
                    {
                        DataMaster.M_JoueiKakakuDataTable dtJ = ClassMaster.GetJouei(dr.ShiireCode, dr.Media, Zasu.Text, Global.GetConnection());
                        RcbHanni.Items.Clear();
                        for (int items = 0; items < dtJ.Count; items++)
                        {
                            RcbHanni.Items.Add(dtJ[items].Range);
                        }
                        if (dtJ.Count > 0)
                        {
                            Hachu.Text = dtJ[0].ShiiresakiName;
                            Hachu.SelectedValue = dtJ[0].ShiiresakiCode.ToString().Trim();
                            RcbShiireName.Text = dtJ[0].ShiiresakiName;
                            LblShiireCode.Text = dr.ShiireCode;
                            string kakakuhyoujyun = dtJ[0].HyoujunKakaku;
                            string kakakushiire = dtJ[0].ShiireKakaku;
                            double h = double.Parse(dtJ[0].HyoujunKakaku);
                            double k = double.Parse(Kakeri.Text);
                            double t = h * k / 100;
                            double tt = Math.Floor(t);
                            CtlKeisan(kakakuhyoujyun, kakakushiire, tt);
                        }
                    }
                    else
                    {
                        TbxHyoujun.Text = dr.HyoujunKakaku.ToString("0,0");
                        LblHanni.Text = dr.Hanni;
                        TbxHanni.Text = dr.Hanni;
                        RcbShiireName.Text = dr.ShiireName;
                        if (!dr.IsShiireNameNull())
                        {
                            Hachu.Text = dr.ShiireName;
                        }
                        Hachu.SelectedValue = dr.ShiireCode;
                        LblShiireCode.Text = dr.ShiireCode;
                        RcbMedia.SelectedValue = dr.Media;
                        if (!dr.IsShiireKakakuNull())
                        {
                            //ShiireTanka.Text = dr.ShiireKakaku.ToString("0,0");
                            string sk = dr.ShiireKakaku.ToString();
                            TbxShiirePrice.Text = dr.ShiireKakaku.ToString("0,0");
                            st.Value = sk;

                            string kakakuhyoujyun = ht.Value;
                            string kakakushiire = st.Value;
                            double h = double.Parse(ht.Value);
                            double k = double.Parse(Kakeri.Text);
                            double t = h * k / 100;
                            double tt = Math.Floor(t);
                            CtlKeisan(kakakuhyoujyun, kakakushiire, tt);
                        }
                    }


                    SerchProduct.Text = dr.SyouhinMei;
                    TbxProductName.Text = dr.SyouhinMei;
                    TbxProductCode.Text = dr.SyouhinCode;

                    if (!dr.IsTyokusouSakiNull())
                    {
                        ShiyouShisetsu.Text = dr.TyokusouSaki;
                        TbxTyokuso.Text = dr.TyokusouSaki;
                    }
                    if (!dr.IsWareHouseNull())
                    {
                        WareHouse.Text = dr.WareHouse;
                        TbxWareHouse.Text = dr.WareHouse;
                    }
                }
            }
            catch (Exception ex)
            {
                err.Text = "商品選択時にエラーが発生しました。";
            }
        }

        internal void Test3(DataSet1.M_Kakaku_New1DataTable dt)
        {
            for (int j = 0; j < dt.Count; j++)
            {
                DataSet1.M_Kakaku_New1Row dr = dt.Rows[j] as DataSet1.M_Kakaku_New1Row;
            }
        }

        internal void Test4(string a)
        {
            category.Value = a;
            categoryV(a);
        }

        private void categoryV(string a)
        {
            if (a != "")
            {
                switch (int.Parse(a))
                {
                    case 101:
                    case 102:
                    case 103:
                    case 199:
                        StartDate.Visible = EndDate.Visible = Zasu.Visible = false;
                        RcbHanni.Style["display"] = "none";
                        break;
                    case 205:
                        StartDate.Visible = EndDate.Visible = Zasu.Visible = true;
                        RcbHanni.Style["display"] = "";
                        break;
                    default:
                        StartDate.Visible = EndDate.Visible = true;
                        Zasu.Visible = false;
                        RcbHanni.Style["display"] = "none";
                        break;
                }
            }
            else
            {
                StartDate.Visible = EndDate.Visible = true;
                Zasu.Visible = false;
                RcbHanni.Style["display"] = "none";
            }
        }

        //public void ProductName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    string cate = category.Value;
        //    if (cate == "")
        //    { err.Text = "カテゴリーを選択して下さい。"; }
        //    else
        //    {
        //        ListSet.SetProductname(sender, e, cate);
        //    }
        //    //procd.Value = ProductName.SelectedValue;
        //}

        protected void Button6_Click(object sender, EventArgs e)
        {
            SyouhinSyousai.Style["display"] = "";
        }

        internal void FukusuCheckedTrue()
        {
            //ShiyouShisetsu.Visible = false;
            KariFaci.Visible = false;
        }

        internal void FukusuCheckedTrueFalse()
        {
            Facility.Text = null;
            ShiyouShisetsu.Visible = true;
            KariFaci.Visible = false;
        }

        internal void FukudateTrue(string startDate, string endDate)
        {
            StartDate.Visible = true;
            EndDate.Visible = true;
            Label11.Visible = false;
            Label12.Visible = false;

            string sd = startDate.Substring(0, 10);
            string ed = endDate.Substring(0, 10);

            StartDate.SelectedDate = DateTime.Parse(sd);
            EndDate.SelectedDate = DateTime.Parse(ed);

        }

        internal void FukudateFalse()
        {
            StartDate.Visible = true;
            EndDate.Visible = true;

            Label11.Text = null;
            Label12.Text = null;
        }

        protected void Suryo_TextChanged(object sender, EventArgs e)
        {
            string[] sp = SerchProduct.SelectedValue.Split('/');
            if (00000000 <= int.Parse(sp[0]) && int.Parse(sp[0]) <= 00001999)
            {

            }
            else
            {
                int kakakuhyoujyun = int.Parse(HyoujyunTanka.Text.Replace(",", ""));
                int kakakushiire = int.Parse(ShiireTanka.Text.Replace(",", ""));
                int tanka = int.Parse(Tanka.Text.Replace(",", ""));
                int su = int.Parse(Suryo.Text);

                Kingaku.Text = (kakakuhyoujyun * su).ToString("0,0");
                Uriage.Text = (tanka * su).ToString("0,0");
                ShiireKingaku.Text = (kakakushiire * su).ToString("0,0");
            }
            //CtlKeisan(kakakuhyoujyun, kakakushiire, t);
        }

        protected void ShiyouShisetsu_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetFacility(sender, e);
        }

        protected void Hachu_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetShiireSaki4(sender, e);
        }

        protected void ShiyouShisetsu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string fac = ShiyouShisetsu.SelectedValue;
            FacilityCode.Value = fac;
            string cc = "";
            DataSet1.M_Facility_NewDataTable dt = Class1.FacilityDT(fac, Global.GetConnection());
            for (int i = 0; i < dt.Count; i++)
            {
                FacilityAddress.Value = dt[i].Address1 + dt[i].Address2;
                TbxFacilityCode.Text = dt[i].FacilityNo.ToString();
                cc = dt[i].CityCode.ToString();
                TbxFacilityName.Text = dt[i].FacilityName1;
                TbxFacilityName2.Text = dt[i].FacilityName2;
                TbxFaci.Text = dt[i].Abbreviation;
                TbxFacilityResponsible.Text = dt[i].FacilityResponsible;
                TbxYubin.Text = dt[i].PostNo;
                TbxFaciAdress.Text = dt[i].Address1 + dt[i].Address2;
                TbxTel.Text = dt[i].Tell;
            }
            DataMaster.M_CityRow dc = ClassMaster.GetCity(cc, Global.GetConnection());
            RcbCity.Text = dc.CityName;
            RcbCity.SelectedValue = dc.CityCode.ToString();
        }

        //protected void MakerHinban_TextChanged(object sender, EventArgs e)
        //{
        //    string a = MakerHinban.Text;
        //    string c = category.Value;
        //    //ListSet.SetProductName2(ProductName, a, c);
        //    DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct3(a, c, Global.SqlConn);
        //    for (int i = 0; i < dt.Count; i++)
        //    {
        //        //ProductName.Text = dt[i].SyouhinMei;
        //        if (!dt[i].IsHanniNull())
        //        { Hanni.Text = dt[i].Hanni; }
        //        if (!dt[i].IsShiireNameNull())
        //        { Hachu.Text = dt[i].ShiireName; }
        //        if (!dt[i].IsHyojunKakakuNull())
        //        { HyoujyunTanka.Text = dt[i].HyojunKakaku.ToString("0,0"); }
        //        if (!dt[i].IsShiireKakakuNull())
        //        { ShiireKingaku.Text = dt[i].ShiireKakaku.ToString("0,0"); }
        //        Baitai.Text = dt[i].Media;
        //        int h = dt[i].HyojunKakaku;
        //        int k = int.Parse(Kakeri.Text);
        //        int t = h * k / 100;
        //        CtlKeisan(dt[i].HyojunKakaku.ToString(), dt[i].ShiireKakaku.ToString(), t);

        //        FocusRoute();
        //    }
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            SisetuSyousai.Style["display"] = "none";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SisetuSyousai.Style["display"] = "";
            //string fac = "";
            //RcbCity.Text = "";
            //TbxFaci.Text = "";
            //TbxFaciAdress.Text = "";
            //TbxFacilityCode.Text = "";
            //TbxFacilityName.Text = "";
            //TbxTel.Text = "";
            //TbxYubin.Text = "";
            //TbxFacilityName2.Text = "";
            //RcbTanto.Text = "";

            //if (ShiyouShisetsu.Visible)
            //{
            //    fac = ShiyouShisetsu.Text;
            //}
            //if (KariFaci.Visible)
            //{
            //    fac = KariFaci.Text;
            //}
            //if (Facility.Text != "")
            //{
            //    fac = Facility.Text;
            //}

            //DataSet1.M_Facility_NewRow dr = Class1.Getfacility(fac, Global.GetConnection());
            //if (dr != null)
            //{
            //    TbxFacilityCode.Text = dr.FacilityNo.ToString();
            //    if (!dr.IsCityCodeNull())
            //    {
            //        int cc = dr.CityCode;
            //        DataMaster.M_CityRow dc = ClassMaster.GetCity(cc.ToString(), Global.GetConnection());
            //        RcbCity.Items.Add(new RadComboBoxItem(dc.CityName, dc.CityCode.ToString()));
            //    }
            //    TbxFacilityName.Text = dr.FacilityName1;
            //    if (!dr.IsAbbreviationNull())
            //    { TbxFaci.Text = dr.Abbreviation; }
            //    if (!dr.IsPostNoNull())
            //    { TbxYubin.Text = dr.PostNo; }
            //    if (!dr.IsAddress1Null())
            //    { TbxFaciAdress.Text = dr.Address1; }
            //    if (!dr.IsTellNull())
            //    { TbxTel.Text = dr.Tell; }
            //    if (!dr.IsFacilityName2Null())
            //    { TbxFacilityName2.Text = dr.FacilityName2; }
            //    if (!dr.IsFacilityResponsibleNull())
            //    { RcbTanto.Text = dr.FacilityResponsible; }
            //}
            //else
            //{
            //    TbxFacilityName.Text = KariFaci.Text;
            //}
        }

        protected void SerchProduct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string fac = ShiyouShisetsu.Text;
            string s = SerchProduct.SelectedValue;
            if (s != "-1")
            {
                ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
                GetKensakuProduct(p);
            }
            else
            {
                ErrorSet(3);
                return;
            }
            ShiyouShisetsu.Text = fac;
        }

        protected void SerchProduct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string cate = category.Value;
            if (cate == "")
            {
                ErrorSet(5);
            }
            else
            {
                ListSet.SettingProduct(sender, e, cate);
                procd.Value = SerchProduct.SelectedValue;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            err.Text = "";
            end.Text = "";
            DataSet1.M_Facility_NewDataTable dt = Class1.FacilityDT(Global.GetConnection());
            DataSet1.M_Facility_NewDataTable newdt = new DataSet1.M_Facility_NewDataTable();
            DataSet1.M_Facility_NewRow newdr = newdt.NewM_Facility_NewRow();
            try
            {
                if (TbxFacilityCode.Text != "")
                {
                    DataSet1.M_Facility_NewRow dr = Class1.FacilityRow(TbxFacilityCode.Text, Global.GetConnection());
                    if (dr == null)
                    {
                        newdr.FacilityNo = int.Parse(TbxFacilityCode.Text);
                        if (RcbCity.Text != "")
                        {
                            newdr.CityCode = int.Parse(RcbCity.Text);
                        }
                        if (TbxFacilityName.Text != "")
                        {
                            newdr.FacilityName1 = TbxFacilityName.Text;
                        }
                        if (TbxFaci.Text != "")
                        {
                            newdr.Abbreviation = TbxFaci.Text;
                        }
                        if (TbxYubin.Text != "")
                        {
                            newdr.PostNo = TbxYubin.Text;
                        }
                        if (TbxFaciAdress.Text != "")
                        {
                            newdr.Address1 = TbxFaciAdress.Text;
                        }
                        if (TbxTel.Text != "")
                        {
                            newdr.Tell = TbxTel.Text;
                        }
                        if (TbxFacilityName2.Text != "")
                        {
                            newdr.FacilityName2 = TbxFacilityName2.Text;
                        }
                        if (TbxFacilityResponsible.Text != "")
                        {
                            newdr.FacilityResponsible = TbxFacilityResponsible.Text;
                        }
                        newdt.AddM_Facility_NewRow(newdr);
                        Class1.InsertFacility(newdt, Global.GetConnection());
                        end.Text = "施設名" + "[" + TbxFacilityName.Text + "]" + "が登録されました。";
                    }
                    else
                    {
                        err.Text = "この施設コードは既に登録されています。";
                    }
                }
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
            }
        }

        protected void BtnUpdateFaci_Click(object sender, EventArgs e)
        {
            SisetuSyousai.Style["display"] = "";
            if (TbxFacilityName.Text != "")
            {
                ShiyouShisetsu.Text = TbxFacilityName.Text;
            }
            else
            {
                err.Text = "施設名1を入力してください。";
            }
            //DataSet1.M_Facility_NewDataTable dt = Class1.FacilityDT(Global.GetConnection());
            //DataSet1.M_Facility_NewDataTable newdt = new DataSet1.M_Facility_NewDataTable();
            //DataSet1.M_Facility_NewRow newdr = newdt.NewM_Facility_NewRow();
            //try
            //{
            //    if (TbxFacilityCode.Text != "")
            //    {
            //        newdr.FacilityNo = int.Parse(TbxFacilityCode.Text);
            //        int no = int.Parse(TbxFacilityCode.Text);
            //        if (RcbCity.Text != "")
            //        {
            //            newdr.CityCode = int.Parse(RcbCity.Text);
            //        }
            //        else
            //        {
            //            newdr.CityCode = int.Parse("");
            //        }
            //        if (TbxFacilityName.Text != "")
            //        {
            //            newdr.FacilityName1 = TbxFacilityName.Text;
            //        }
            //        else
            //        {
            //            newdr.FacilityName1 = "";
            //        }
            //        if (TbxFaci.Text != "")
            //        {
            //            newdr.Abbreviation = TbxFaci.Text;
            //        }
            //        else
            //        {
            //            newdr.Abbreviation = "";
            //        }

            //        if (TbxYubin.Text != "")
            //        {
            //            newdr.PostNo = TbxYubin.Text;
            //        }
            //        else
            //        {
            //            newdr.PostNo = "";
            //        }
            //        if (TbxFaciAdress.Text != "")
            //        {
            //            newdr.Address1 = TbxFaciAdress.Text;
            //        }
            //        else
            //        {
            //            newdr.Address1 = "";
            //        }
            //        if (TbxTel.Text != "")
            //        {
            //            newdr.Tell = TbxTel.Text;
            //        }
            //        else
            //        {
            //            newdr.Tell = "";
            //        }
            //        if (TbxFacilityName2.Text != "")
            //        {
            //            newdr.FacilityName2 = TbxFacilityName2.Text;
            //        }
            //        else
            //        {
            //            newdr.FacilityName2 = "";
            //        }
            //        if (RcbTanto.Text != "")
            //        {
            //            newdr.FacilityResponsible = RcbTanto.Text;
            //        }
            //        else
            //        {
            //            newdr.FacilityResponsible = "";
            //        }
            //        newdt.AddM_Facility_NewRow(newdr);
            //        Class1.UpdateFaci(newdt, no, TbxFacilityName.Text, Global.GetConnection());
            //        end.Text = "施設名" + "[" + TbxFacilityName.Text + "]" + "更新されました。";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    err.Text = ex.Message;
            //}
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            if (RcbMedia.SelectedValue != "")
            {
                Baitai.Text = RcbMedia.SelectedValue;
            }
            if (TbxHyoujun.Text != "")
            {
                HyoujyunTanka.Text = TbxHyoujun.Text;
            }
            if (TbxProductName.Text != "")
            {
                SerchProduct.Text = TbxProductName.Text;
            }
            if (TbxShiirePrice.Text != "")
            {
                ShiireTanka.Text = TbxShiirePrice.Text;
            }
            if (TbxHanni.Text != "")
            {
                RcbHanni.Text = TbxHanni.Text;
            }
            if (TbxMakerNo.Text != "")
            {
                LblProduct.Text = TbxMakerNo.Text;
            }
            if (RcbShiireName.Text != "")
            {
                Hachu.Text = RcbShiireName.Text;
            }
            if (LblCateCode.Text != "")
            {
                HidCategoryCode.Value = LblCateCode.Text;
            }
            string kakakuhyoujyun = TbxHyoujun.Text.Replace(",", "");
            string kakakushiire = TbxShiirePrice.Text.Replace(",", "");
            if (kakakuhyoujyun != "")
            {
                int h = int.Parse(kakakuhyoujyun);
                int k = int.Parse(Kakeri.Text);
                double t = h * k / 100;
                double tt = Math.Floor(t);

                CtlKeisan(kakakuhyoujyun, kakakushiire, tt);
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {

        }

        protected void HyoujyunTanka_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ht = int.Parse(HyoujyunTanka.Text.Replace(",", ""));
                int su = int.Parse(Suryo.Text);
                int hk = ht * su;
                //標準単価・金額計算　（標準単価×数量）
                HyoujyunTanka.Text = ht.ToString("0,0");
                Kingaku.Text = hk.ToString("0,0");
                //単価・売上計算　（標準単価×掛率（小数点切捨）×数量）
                int kk = int.Parse(Kakeri.Text);
                double tk = ht * kk / 100;
                double tanka = Math.Floor(tk);
                Tanka.Text = tanka.ToString("0,0");
                Uriage.Text = (tanka * su).ToString("0,0");
            }
            catch
            {
                ErrorSet(4);
            }
        }

        protected void ShiireTanka_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int st = int.Parse(ShiireTanka.Text.Replace(",", ""));
                int su = int.Parse(Suryo.Text);
                int sk = st * su;
                ShiireKingaku.Text = sk.ToString("0,0");
            }
            catch
            {
                ErrorSet(4);
            }
        }

        protected void Teikyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void Zasu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RcbHanni.SelectedItem.Text != "")
            {
                string strShiireCode = Hachu.SelectedValue;
                string strMedia = Baitai.Text;
                string strHanni = RcbHanni.SelectedItem.Text;
                string strSeki = Zasu.SelectedValue;
                DataMaster.M_JoueiKakakuRow dr = ClassMaster.GetJoueiKakaku(strShiireCode, strMedia, strHanni, strSeki, Global.GetConnection());
                if (dr != null)
                {
                    if (dr.HyoujunKakaku != "OPEN")
                    {
                        int hk = int.Parse(dr.HyoujunKakaku);
                        int sk = int.Parse(dr.ShiireKakaku);
                        int kake = int.Parse(Kakeri.Text.Trim());
                        double t = hk * kake / 100;
                        double tt = Math.Floor(t);
                        CtlKeisan(dr.HyoujunKakaku, dr.ShiireKakaku, tt);
                    }
                    else
                    {
                        HyoujyunTanka.Text = "OPEN";
                        TbxHyoujun.Text = "OPEN";
                        Kingaku.Text = "0";
                        Tanka.Text = "0";
                        Uriage.Text = "0";
                        ShiireTanka.Text = "0";
                        ShiireKingaku.Text = "0";
                    }
                }
                else
                {
                    ErrorSet(6);
                    return;
                }
            }
            else
            {
                ErrorSet(1);
                return;
            }
        }

        protected void RcbHanni_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Zasu.SelectedValue != "")
            {
                string strShiireCode = LblShiireCode.Text;
                string strMedia = Baitai.Text;
                string strHanni = RcbHanni.SelectedItem.Text;
                string strSeki = Zasu.SelectedValue;
                DataMaster.M_JoueiKakakuRow dr = ClassMaster.GetJoueiKakaku(strShiireCode, strMedia, strHanni, strSeki, Global.GetConnection());
                if (dr != null)
                {
                    if (dr.HyoujunKakaku != "OPEN")
                    {
                        int hk = int.Parse(dr.HyoujunKakaku);
                        int sk = int.Parse(dr.ShiireKakaku);
                        int kake = int.Parse(Kakeri.Text.Trim());
                        double t = hk * kake / 100;
                        double tt = Math.Floor(t);
                        CtlKeisan(dr.HyoujunKakaku, dr.ShiireKakaku, tt);
                    }
                    else
                    {
                        HyoujyunTanka.Text = "OPEN";
                        TbxHyoujun.Text = "OPEN";
                        Kingaku.Text = "0";
                        Tanka.Text = "0";
                        Uriage.Text = "0";
                        ShiireTanka.Text = "0";
                        ShiireKingaku.Text = "0";
                    }
                }
            }
            else
            {
                ErrorSet(2);
                return;
            }
        }

        private void ErrorSet(int v)
        {
            switch (v)
            {
                case 1:
                    err.Text = "使用範囲を選択してください。";
                    break;
                case 2:
                    err.Text = "席数を選択してください。";
                    break;
                case 3:
                    err.Text = "商品名を選択してください。";
                    break;
                case 4:
                    err.Text = "数値を入力して下さい。";
                    break;
                case 5:
                    err.Text = "カテゴリーを選択して下さい。";
                    break;
                case 6:
                    err.Text = "選択した席数は適応できません";
                    break;
                case 7:
                    err.Text = "選択した仕入先は上映会価格表にありません。";
                    break;
            }
        }

        protected void RcbShiireName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetShiireSaki4(sender, e);
            }
        }

        protected void RcbShiireName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string strShiiresakiCode = RcbShiireName.SelectedValue;
            LblShiireCode.Text = RcbShiireName.SelectedValue;
            Hachu.Text = RcbShiireName.Text;
            if (category.Value == "205")
            {
                DataMaster.M_JoueiRangeDataTable dt = ClassMaster.GetJoueiRange(strShiiresakiCode, Baitai.Text, Global.GetConnection());
                if (dt.Count > 0)
                {
                    RcbHanni.Items.Clear();
                    RcbHanni.Items.Add("使用範囲を選択");
                    for (int i = 0; i < dt.Count; i++)
                    {
                        RcbHanni.Items.Add(dt[i].Range);
                    }
                }
                else
                {
                    ErrorSet(7);
                }
            }

        }

        protected void RcbCity_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetCity2(sender, e);
            }
        }

        protected void RcbTanto_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTanto4(sender, e);
            }
        }

        protected void Hachu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Zasu.Text != "")
            {
                string strShiiresakiCode = Hachu.SelectedValue;
                if (category.Value == "205")
                {
                    DataMaster.M_JoueiRangeDataTable dt = ClassMaster.GetJoueiRange(strShiiresakiCode, Baitai.Text, Global.GetConnection());
                    if (dt.Count > 0)
                    {
                        RcbHanni.Items.Clear();
                        RcbHanni.Items.Add("使用範囲を選択");
                        for (int i = 0; i < dt.Count; i++)
                        {
                            RcbHanni.Items.Add(dt[i].Range);
                        }
                        LblShiireCode.Text = Hachu.SelectedValue;
                        RcbShiireName.Text = Hachu.Text;
                    }
                    else
                    {
                        ErrorSet(7);
                    }
                }
            }
        }
    }
}