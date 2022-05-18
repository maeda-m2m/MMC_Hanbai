using DLL;
using System;
using Telerik.Web.UI;
using System.Windows.Forms;

namespace Gyomu
{
    public partial class CtrlMitsuSyousai : System.Web.UI.UserControl
    {
        public static DataMitumori.T_RowRow dataT_row;
        public static DataMitumori.T_RowDataTable dataT_RowTable;
        public static string ProductName;

        protected void Page_Load(object sender, EventArgs e)
        {
            SyouhinSyousai.Style["display"] = "none";
            SisetuSyousai.Style["display"] = "none";
            RcbHanni.Style["display"] = "none";
            err.Text = "";
            end.Text = "";
            Kakeri.Text = Kaihatsu.strKakeritsu;
            HyoujyunTanka.Attributes["onkeydown"] = string.Format("HyoujunKeyDown('{0}');", HyoujyunTanka.ClientID);
            Tanka.Attributes["onkeydown"] = string.Format("TankaKeyDown('{0}');", Tanka.ClientID);
            ShiireTanka.Attributes["onkeydown"] = string.Format("ShiireKeyDown('{0}');", ShiireTanka.ClientID);
            if (!IsPostBack)
            {
                if (Kaihatsu.objFacility != null)
                {
                    SetFacility(Kaihatsu.objFacility);
                }
            }
        }

        private void SetFacility(object[] objFacility)
        {
            TbxFacilityCode.Text = objFacility[0].ToString();
            TbxFacilityRowCode.Text = objFacility[1].ToString();
            TbxFacilityName.Text = objFacility[2].ToString();
            TbxFacilityName2.Text = objFacility[3].ToString();
            TbxFaci.Text = objFacility[4].ToString();
            TbxFacilityResponsible.Text = objFacility[5].ToString();
            TbxYubin.Text = objFacility[6].ToString();
            TbxFaciAdress.Text = objFacility[7].ToString() + objFacility[8].ToString();
            TbxTel.Text = objFacility[9].ToString();
            if (!string.IsNullOrEmpty(objFacility[10].ToString()))
            {
                DataMaster.M_CityRow dr = ClassMaster.GetCity(objFacility[10].ToString(), Global.GetConnection());
                if (dr != null)
                {
                    RcbCity.SelectedValue = dr.CityCode.ToString();
                    RcbCity.Text = dr.CityName;
                }
            }
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
            string syouhinCode = TbxProductCode.Text;
            if (99999000 <= int.Parse(syouhinCode) && int.Parse(syouhinCode) <= 99999999)
            {

            }
            else
            {
                HyoujyunTanka.Text = int.Parse(kakakuhyoujyun).ToString("0,0");//標準単価
                TbxHyoujun.Text = int.Parse(kakakuhyoujyun).ToString("0,0");//商品詳細の標準単価
                Kingaku.Text = (int.Parse(kakakuhyoujyun) * int.Parse(Suryo.Text)).ToString("0,0");//金額
                TbxShiirePrice.Text = int.Parse(kakakushiire).ToString("0,0");//商品詳細の仕入単価
                ShiireTanka.Text = int.Parse(kakakushiire).ToString("0,0");//仕入単価
                if (zeiku.Text.Trim() == "税込")
                {
                    double tanka = int.Parse(kakakuhyoujyun) * int.Parse(Kakeri.Text) * 1.1 / 100;
                    Tanka.Text = tanka.ToString("0,0");//単価
                    double uriage = tanka * int.Parse(Suryo.Text);
                    Uriage.Text = uriage.ToString("0,0");//売上金額
                    ShiireKingaku.Text = (int.Parse(kakakushiire) * int.Parse(Suryo.Text) * 1.1).ToString("0,0");//仕入金額 
                }
                else
                {
                    double tanka = int.Parse(kakakuhyoujyun) * int.Parse(Kakeri.Text) / 100;
                    Tanka.Text = tanka.ToString("0,0");//単価(税抜)
                    double uriage = tanka * int.Parse(Suryo.Text);
                    Uriage.Text = uriage.ToString("0,0");//売上金額(税抜)
                    ShiireKingaku.Text = (int.Parse(kakakushiire) * int.Parse(Suryo.Text)).ToString("0,0");//仕入金額 
                }
            }
        }

        internal void kari(bool ckb)
        {
            if (ckb == true)
            {
                ShiyouShisetsu.Visible = false;
            }
            else
            {
                ShiyouShisetsu.Visible = true;
            }
        }

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
                    TbxProductCode.Text = dr.SyouhinCode;
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
                        DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(dr.ShiireCode, dr.Media, Zasu.Text, Global.GetConnection());
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

                    if (!dr.IsWareHouseNull())
                    {
                        WareHouse.Text = dr.WareHouse;
                        TbxWareHouse.Text = dr.WareHouse;
                    }
                }
            }
            catch
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
                        StartDate.Style["display"] = "none";
                        EndDate.Style["display"] = "none";
                        Zasu.Style["display"] = "none";
                        RcbHanni.Style["display"] = "none";
                        break;
                    case 205:
                        StartDate.Style["display"] = "";
                        EndDate.Style["display"] = "";
                        Zasu.Style["display"] = "";
                        RcbHanni.Style["display"] = "";
                        break;
                    default:
                        StartDate.Style["display"] = "";
                        EndDate.Style["display"] = "";
                        Zasu.Style["display"] = "none";
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


        protected void Button6_Click(object sender, EventArgs e)
        {
            SyouhinSyousai.Style["display"] = "";
        }


        internal void FukusuCheckedTrueFalse()
        {
            Facility.Text = null;
            ShiyouShisetsu.Visible = true;
        }

        internal void FukudateTrue(string startDate, string endDate)
        {
            StartDate.Visible = true;
            EndDate.Visible = true;

            string sd = startDate.Substring(0, 10);
            string ed = endDate.Substring(0, 10);

            StartDate.SelectedDate = DateTime.Parse(sd);
            EndDate.SelectedDate = DateTime.Parse(ed);

        }

        internal void FukudateFalse()
        {
            StartDate.Visible = true;
            EndDate.Visible = true;

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            SisetuSyousai.Style["display"] = "none";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SisetuSyousai.Style["display"] = "";
        }
        protected void SerchProduct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string cate = Kaihatsu.strCategoryCode;
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
            if (TbxFaci.Text != "")
            {
                ShiyouShisetsu.Text = TbxFaci.Text;
            }
            else
            {
                err.Text = "施設名略称を入力してください。";
            }
            if (!string.IsNullOrEmpty(HidMedia.Value))
            {
                Baitai.Text = HidMedia.Value;
            }
            if (!string.IsNullOrEmpty(HidHanni.Value))
            {
                LblHanni.Text = HidHanni.Value;
            }
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
            if (!string.IsNullOrEmpty(HidMedia.Value))
            {
                Baitai.Text = HidMedia.Value;
            }
            if (!string.IsNullOrEmpty(HidHanni.Value))
            {
                LblHanni.Text = HidHanni.Value;
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
            LblShiireCode.Text = HidShiireCode.Value;
            Baitai.Text = HidMedia.Value;
            DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(HidShiireCode.Value, RcbMedia.Text, Zasu.Text, Global.GetConnection());
            RcbHanni.Items.Clear();
            RcbHanni.Items.Add(new RadComboBoxItem(RcbHanni.EmptyMessage, ""));
            if (dtJ.Count > 0)
            {
                for (int items = 0; items < dtJ.Count; items++)
                {
                    RcbHanni.Items.Add(dtJ[items].Range);
                }
            }
            else
            {
                RcbShiireName.Text = "該当する商品無し";
            }
        }

        protected void RcbHanni_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Zasu.SelectedValue != "")
            {
                TbxHanni.Text = RcbHanni.Text;
                string strShiireCode = HidShiireCode.Value;
                string strMedia = RcbMedia.Text;
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
            if (string.IsNullOrEmpty(Hachu.Text))
            {
                if (!string.IsNullOrEmpty(RcbShiireName.Text))
                {
                    Hachu.Text = RcbShiireName.Text;
                    Hachu.SelectedValue = RcbShiireName.SelectedValue;
                    HidShiireCode.Value = RcbShiireName.SelectedValue;
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

            if (string.IsNullOrEmpty(RcbShiireName.Text))
            {
                if (!string.IsNullOrEmpty(Hachu.Text))
                {
                    RcbShiireName.Text = Hachu.Text;
                    LblShiireCode.Text = Hachu.SelectedValue;
                }
            }
            //if (Zasu.Text != "")
            //{
            //    string strShiiresakiCode = Hachu.SelectedValue;
            //    if (category.Value == "205")
            //    {
            //        DataMaster.M_JoueiRangeDataTable dt = ClassMaster.GetJoueiRange(strShiiresakiCode, Baitai.Text, Global.GetConnection());
            //        if (dt.Count > 0)
            //        {
            //            RcbHanni.Items.Clear();
            //            RcbHanni.Items.Add("使用範囲を選択");
            //            for (int i = 0; i < dt.Count; i++)
            //            {
            //                RcbHanni.Items.Add(dt[i].Range);
            //            }
            //            LblShiireCode.Text = Hachu.SelectedValue;
            //            RcbShiireName.Text = Hachu.Text;
            //        }
            //        else
            //        {
            //            ErrorSet(7);
            //        }
            //    }
            //}
        }

        internal DataMitumori.T_RowRow ItemGet()
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            if (!string.IsNullOrEmpty(Tekiyo.Text))
            {
                dr.JutyuTekiyo = Tekiyo.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
            {
                dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
            }
            if (!string.IsNullOrEmpty(StartDate.SelectedDate.ToString()))
            {
                dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString());
            }
            if (!string.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
            {
                dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString());
            }
            if (!string.IsNullOrEmpty(TbxProductName.Text))
            {
                dr.SyouhinMei = TbxProductName.Text;
            }
            if (!string.IsNullOrEmpty(TbxProductCode.Text))
            {
                dr.SyouhinCode = TbxProductCode.Text;
            }
            if (!string.IsNullOrEmpty(TbxHyoujun.Text))
            {
                dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(Kingaku.Text))
            {
                dr.Ryoukin = Kingaku.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(Suryo.Text))
            {
                dr.JutyuSuryou = int.Parse(Suryo.Text);
            }
            if (!string.IsNullOrEmpty(Tanka.Text))
            {
                dr.JutyuTanka = int.Parse(Tanka.Text.Replace(",", ""));
            }
            if (!string.IsNullOrEmpty(Uriage.Text))
            {
                dr.JutyuGokei = int.Parse(Uriage.Text.Replace(",", ""));
            }
            if (!string.IsNullOrEmpty(Zasu.Text))
            {
                dr.Zasu = Zasu.Text;
            }
            if (!string.IsNullOrEmpty(TbxMakerNo.Text))
            {
                dr.MekarHinban = TbxMakerNo.Text;
            }
            if (!string.IsNullOrEmpty(HidHanni.Value))
            {
                dr.Range = HidHanni.Value;
            }
            if (!string.IsNullOrEmpty(RcbHanni.Text))
            {
                dr.Range = RcbHanni.Text;
            }
            if (!string.IsNullOrEmpty(HidMedia.Value))
            {
                dr.Media = HidMedia.Value;
            }
            if (!string.IsNullOrEmpty(Baitai.Text))
            {
                dr.Media = Baitai.Text;
            }
            if (!string.IsNullOrEmpty(ShiireKingaku.Text))
            {
                dr.ShiireKingaku = ShiireKingaku.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(ShiireTanka.Text))
            {
                dr.ShiireTanka = ShiireTanka.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(WareHouse.Text))
            {
                dr.WareHouse = WareHouse.Text;
            }
            if (!string.IsNullOrEmpty(RcbShiireName.Text))
            {
                dr.ShiiresakiMei = RcbShiireName.Text;
            }
            if (!string.IsNullOrEmpty(HidShiireCode.Value))
            {
                dr.ShiiresakiCode = HidShiireCode.Value;
            }
            if (!string.IsNullOrEmpty(LblShiireCode.Text))//HidShiireCodeがなかった時の保険
            {
                dr.ShiiresakiCode = LblShiireCode.Text;
            }
            if (!string.IsNullOrEmpty(Kaihatsu.strKakeritsu))
            {
                dr.Kakeritsu = Kaihatsu.strKakeritsu;
            }
            else
            {
                dr.Kakeritsu = Kakeri.Text.Trim();
            }
            if (!string.IsNullOrEmpty(Kaihatsu.strZeikubun))
            {
                dr.Zeiku = Kaihatsu.strZeikubun;
            }
            if (!string.IsNullOrEmpty(Kaihatsu.strCategoryCode))
            {
                dr.CategoryCode = Kaihatsu.strCategoryCode;
            }
            if (!string.IsNullOrEmpty(Kaihatsu.strCategoryName))
            {
                dr.CategoryName = Kaihatsu.strCategoryName;
            }
            if (!string.IsNullOrEmpty(TbxFaciAdress.Text))
            {
                dr.SisetsuJusyo = TbxFaciAdress.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
            {
                dr.SisetsuTanto = TbxFacilityResponsible.Text;
            }
            if (!string.IsNullOrEmpty(TbxYubin.Text))
            {
                dr.SisetsuPost = TbxYubin.Text;
            }
            if (!string.IsNullOrEmpty(RcbCity.SelectedValue))
            {
                dr.SisetsuCityCode = RcbCity.SelectedValue;
            }
            if (!string.IsNullOrEmpty(TbxFacilityName.Text))
            {
                dr.SisetsuMei1 = TbxFacilityName.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
            {
                dr.SisetsuMei2 = TbxFacilityName2.Text;
            }
            if (!string.IsNullOrEmpty(TbxFaci.Text))
            {
                dr.SisetsuAbbreviration = TbxFaci.Text;
            }
            if (!string.IsNullOrEmpty(TbxTel.Text))
            {
                dr.SisetsuTell = TbxTel.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
            {
                dr.SisetsuRowCode = TbxFacilityRowCode.Text;
            }
            if (!string.IsNullOrEmpty(RdpPermissionstart.SelectedDate.ToString()))
            {
                dr.PermissionStart = RdpPermissionstart.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(RdpRightEnd.SelectedDate.ToString()))
            {
                dr.PermissionEnd = RdpRightEnd.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(RdpCpStart.SelectedDate.ToString()))
            {
                dr.Cpkaishi = RdpCpStart.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(RdpCpEnd.SelectedDate.ToString()))
            {
                dr.CpOwari = RdpCpEnd.SelectedDate.ToString();
            }
            return dr;
        }


        internal void ItemSet(DataMitumori.T_RowRow dr)
        {
            if (!dr.IsJutyuTekiyoNull())
            {
                Tekiyo.Text = dr.JutyuTekiyo;
            }
            if (!dr.IsSisetuCodeNull())
            {
                TbxFacilityCode.Text = dr.SisetuCode.ToString();
            }
            if (!dr.IsSisetsuJusyoNull())
            {
                TbxFaciAdress.Text = dr.SisetsuJusyo;
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
                RcbCity.SelectedValue = dr.SisetsuCityCode;
                if (!string.IsNullOrEmpty(dr.SisetsuCityCode))
                {
                    DataMaster.M_CityRow drC = ClassMaster.GetCity(dr.SisetsuCityCode.ToString(), Global.GetConnection());
                    RcbCity.Text = drC.CityName;
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
                ShiyouShisetsu.Text = dr.SisetsuAbbreviration;
                TbxFaci.Text = dr.SisetsuAbbreviration;
            }
            if (!dr.IsSisetsuTellNull())
            {
                TbxTel.Text = dr.SisetsuTell;
            }
            if (!dr.IsSisetsuRowCodeNull())
            {
                TbxFacilityRowCode.Text = dr.SisetsuRowCode;
            }

            if (!dr.IsSiyouKaishiNull())
            {
                StartDate.SelectedDate = dr.SiyouKaishi;
            }
            if (!dr.IsSiyouOwariNull())
            {
                EndDate.SelectedDate = dr.SiyouOwari;
            }
            if (!dr.IsSyouhinMeiNull())
            {
                TbxProductName.Text = dr.SyouhinMei;
                SerchProduct.Text = dr.SyouhinMei;
                ProductName = dr.SyouhinMei;
                if (!dr.IsSyouhinCodeNull())
                {
                    TbxProductCode.Text = dr.SyouhinCode;
                }
                if (!dr.IsHyojunKakakuNull())
                {
                    if (dr.HyojunKakaku != "OPEN")
                    {
                        TbxHyoujun.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                        HyoujyunTanka.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                    }
                    else
                    {
                        TbxHyoujun.Text = dr.HyojunKakaku;
                        HyoujyunTanka.Text = dr.HyojunKakaku;
                    }
                }
                if (!dr.IsRyoukinNull())
                {
                    Kingaku.Text = int.Parse(dr.Ryoukin).ToString("0,0");
                }
                if (!dr.IsJutyuSuryouNull())
                {
                    Suryo.Text = dr.JutyuSuryou.ToString();
                }
                if (!dr.IsJutyuTankaNull())
                {
                    Tanka.Text = dr.JutyuTanka.ToString("0,0");
                }
                if (!dr.IsJutyuGokeiNull())
                {
                    Uriage.Text = dr.JutyuGokei.ToString("0,0");
                }
                if (!dr.IsZasuNull())
                {
                    Zasu.SelectedItem.Text = dr.Zasu;
                }
                if (!dr.IsMekarHinbanNull())
                {
                    TbxMakerNo.Text = dr.MekarHinban;
                    LblProduct.Text = dr.MekarHinban;
                }
                if (!dr.IsCategoryCodeNull())
                {
                    if (dr.CategoryCode == "205")
                    {
                        string shiireCode = dr.ShiiresakiCode;
                        string media = dr.Media;
                        string zasu = dr.Zasu;
                        DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(shiireCode, media, zasu, Global.GetConnection());
                        RcbHanni.Items.Clear();
                        RcbHanni.Items.Add(new RadComboBoxItem(RcbHanni.EmptyMessage, ""));
                        for (int items = 0; items < dtJ.Count; items++)
                        {
                            RcbHanni.Items.Add(dtJ[items].Range);
                        }
                        if (!dr.IsRangeNull())
                        {
                            RcbHanni.SelectedItem.Text = dr.Range;
                            TbxHanni.Text = dr.Range;
                        }
                    }
                    else
                    {
                        if (!dr.IsRangeNull())
                        {
                            LblHanni.Text = dr.Range;
                            RcbHanni.Text = dr.Range;
                            TbxHanni.Text = dr.Range;
                        }
                    }
                }
                if (!dr.IsMediaNull())
                {
                    Baitai.Text = dr.Media;
                    RcbMedia.SelectedValue = dr.Media;
                }
                if (!dr.IsShiireKingakuNull())
                {
                    ShiireKingaku.Text = int.Parse(dr.ShiireKingaku).ToString("0,0");
                }
                if (!dr.IsShiireTankaNull())
                {
                    ShiireTanka.Text = int.Parse(dr.ShiireTanka).ToString("0,0");
                    TbxShiirePrice.Text = int.Parse(dr.ShiireTanka).ToString("0,0");
                }
                if (!dr.IsWareHouseNull())
                {
                    WareHouse.Text = dr.WareHouse;
                    TbxWareHouse.Text = dr.WareHouse;
                }
                if (!dr.IsShiiresakiMeiNull())
                {
                    RcbShiireName.Text = dr.ShiiresakiMei;
                    Hachu.Text = dr.ShiiresakiMei;
                }
                if (!dr.IsShiiresakiCodeNull())
                {
                    LblShiireCode.Text = dr.ShiiresakiCode;
                    Hachu.SelectedValue = dr.ShiiresakiCode;
                }
                if (!dr.IsKakeritsuNull())
                {
                    Kakeri.Text = dr.Kakeritsu;
                }
                if (!dr.IsCategoryCodeNull())
                {
                    LblCateCode.Text = dr.CategoryCode;
                }
                if (!dr.IsCategoryNameNull())
                {
                    LblCategoryName.Text = dr.CategoryName;
                }
                if (!dr.IsPermissionStartNull())
                {
                    RdpPermissionstart.SelectedDate = DateTime.Parse(dr.PermissionStart);
                }
                if (!dr.IsPermissionEndNull())
                {
                    RdpRightEnd.SelectedDate = DateTime.Parse(dr.PermissionEnd);
                }
                if (!dr.IsCpkaishiNull())
                {
                    RdpCpStart.SelectedDate = DateTime.Parse(dr.Cpkaishi);
                }
                if (!dr.IsCpOwariNull())
                {
                    RdpCpEnd.SelectedDate = DateTime.Parse(dr.CpOwari);
                }
            }
            else
            {
                ProductName = "";
            }
            if (!dr.IsZeikuNull())
            {
                zeiku.Text = dr.Zeiku;
            }
            else
            {
                SerchProduct.Focus();
            }
        }

        internal void ItemSet2(DataMitumori.T_MitumoriRow dr)
        {
            if (!dr.IsTekiyou1Null())
            {
                Tekiyo.Text = dr.Tekiyou1;
            }
            if (!dr.IsSiyouKaishiNull())
            {
                StartDate.SelectedDate = dr.SiyouKaishi;
            }
            if (!dr.IsSiyouOwariNull())
            {
                EndDate.SelectedDate = dr.SiyouOwari;
            }
            if (!dr.IsSyouhinMeiNull())
            {
                TbxProductName.Text = dr.SyouhinMei;
                SerchProduct.Text = dr.SyouhinMei;
            }
            else
            {
                SerchProduct.Focus();
            }
            if (!dr.IsSyouhinCodeNull())
            {
                TbxProductCode.Text = dr.SyouhinCode;
            }
            if (!dr.IsHyojunKakakuNull())
            {
                if (dr.HyojunKakaku != "OPEN")
                {
                    TbxHyoujun.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                    HyoujyunTanka.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                }
                else
                {
                    TbxHyoujun.Text = dr.HyojunKakaku;
                    HyoujyunTanka.Text = dr.HyojunKakaku;
                }
            }
            if (!dr.IsRyoukinNull())
            {
                Kingaku.Text = int.Parse(dr.Ryoukin).ToString("0,0");
            }
            if (!dr.IsJutyuSuryouNull())
            {
                Suryo.Text = dr.JutyuSuryou.ToString();
            }
            if (!dr.IsJutyuTankaNull())
            {
                Tanka.Text = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsJutyuGokeiNull())
            {
                Uriage.Text = dr.JutyuGokei.ToString("0,0");
            }
            if (!dr.IsMekarHinbanNull())
            {
                TbxMakerNo.Text = dr.MekarHinban;
                LblProduct.Text = dr.MekarHinban;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                Baitai.Text = dr.KeitaiMei;
                RcbMedia.SelectedValue = dr.KeitaiMei;
            }
            if (!dr.IsShiireKingakuNull())
            {
                ShiireKingaku.Text = dr.ShiireKingaku.ToString("0,0");
            }
            if (!dr.IsShiireTankaNull())
            {
                ShiireTanka.Text = dr.ShiireTanka.ToString("0,0");
                TbxShiirePrice.Text = dr.ShiireTanka.ToString("0,0");
            }
            if (!dr.IsWareHouseNull())
            {
                WareHouse.Text = dr.WareHouse;
                TbxWareHouse.Text = dr.WareHouse;
            }
            if (!dr.IsShiireNameNull())
            {
                RcbShiireName.Text = dr.ShiireName;
                Hachu.Text = dr.ShiireName;
            }
            if (!dr.IsShiiresakiCodeNull())
            {
                Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
            }
            if (!dr.IsKakeritsuNull())
            {
                Kakeri.Text = dr.Kakeritsu;
            }
            if (!dr.IsZeikubunNull())
            {
                zeiku.Text = dr.Zeikubun;
            }
            if (!dr.IsCateGoryNull())
            {
                LblCateCode.Text = dr.CateGory.ToString();
            }

            if (!dr.IsCategoryNameNull())
            {
                LblCategoryName.Text = dr.CategoryName;
            }
            if (!dr.IsSisetuJusyo1Null())
            {
                TbxFaciAdress.Text = dr.SisetuJusyo1;
            }
            if (!dr.IsSisetuTantoNull())
            {
                TbxFacilityResponsible.Text = dr.SisetuTanto;
            }
            if (!dr.IsSisetuPostNull())
            {
                TbxYubin.Text = dr.SisetuPost;
            }
            if (!dr.IsSisetuCityCodeNull())
            {
                RcbCity.SelectedValue = dr.SisetuCityCode.ToString();
                DataMaster.M_CityRow drC = ClassMaster.GetCity(dr.SisetuCityCode.ToString(), Global.GetConnection());
                RcbCity.Text = drC.CityName;
            }
            if (!dr.IsSisetuCodeNull())
            {
                TbxFacilityCode.Text = dr.SisetuCode.ToString();
            }

            if (!dr.IsSisetuMeiNull())
            {
                TbxFacilityName.Text = dr.SisetuMei;
            }
            if (!dr.IsSisetsuMei2Null())
            {
                TbxFacilityName2.Text = dr.SisetsuMei2;
            }
            if (!dr.IsSisetsuAbbrevirationNull())
            {
                TbxFaci.Text = dr.SisetsuAbbreviration;
                ShiyouShisetsu.Text = dr.SisetsuAbbreviration;
            }
            if (!dr.IsSisetsuTellNull())
            {
                TbxTel.Text = dr.SisetsuTell;
            }
            if (!dr.IsSisetuRowCodeNull())
            {
                TbxFacilityRowCode.Text = dr.SisetuRowCode;
            }
            if (!dr.IsPermissionStartNull())
            {
                RdpPermissionstart.SelectedDate = dr.PermissionStart;
            }
            if (!dr.IsRightEndNull())
            {
                RdpRightEnd.SelectedDate = dr.RightEnd;
            }
            if (!dr.IsCpStartNull())
            {
                RdpCpStart.SelectedDate = dr.CpStart;
            }
            if (!dr.IsCpEndNull())
            {
                RdpCpEnd.SelectedDate = dr.CpEnd;
            }
            if (!dr.IsShiiresakiCodeNull() && dr.CategoryName == "上映会")
            {
                DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(dr.ShiiresakiCode.ToString(), dr.KeitaiMei, dr.Zasu, Global.GetConnection());
                RcbHanni.Items.Clear();
                if (dtJ.Count > 0)
                {
                    for (int items = 0; items < dtJ.Count; items++)
                    {
                        RcbHanni.Items.Add(dtJ[items].Range);
                    }
                    RcbHanni.SelectedItem.Text = dr.Range;
                    TbxHanni.Text = dr.Range;
                    Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
                    LblShiireCode.Text = dr.ShiiresakiCode.ToString();
                    Zasu.SelectedValue = dr.Zasu;
                }
            }
            else
            {
                if (!dr.IsShiiresakiCodeNull())
                {
                    LblShiireCode.Text = dr.ShiiresakiCode.ToString();
                }
                if (!dr.IsRangeNull())
                {
                    LblHanni.Text = dr.Range;
                    TbxHanni.Text = dr.Range;
                }
            }
        }

        internal void ItemSet3(DataJutyu.T_JutyuRow dr)
        {
            if (!dr.IsSiyouKaishiNull())
            {
                StartDate.SelectedDate = dr.SiyouKaishi;
            }
            if (!dr.IsSiyouOwariNull())
            {
                EndDate.SelectedDate = dr.SiyouOwari;
            }
            if (!dr.IsSyouhinMeiNull())
            {
                TbxProductName.Text = dr.SyouhinMei;
                SerchProduct.Text = dr.SyouhinMei;
            }
            else
            {
                SerchProduct.Focus();
            }
            if (!dr.IsSyouhinCodeNull())
            {
                TbxProductCode.Text = dr.SyouhinCode;
            }
            if (!dr.IsHyojunKakakuNull())
            {
                if (dr.HyojunKakaku != "OPEN")
                {
                    TbxHyoujun.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                    HyoujyunTanka.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                }
                else
                {
                    TbxHyoujun.Text = dr.HyojunKakaku;
                    HyoujyunTanka.Text = dr.HyojunKakaku;
                }
            }
            if (!dr.IsRyoukinNull())
            {
                Kingaku.Text = int.Parse(dr.Ryoukin).ToString("0,0");
            }
            if (!dr.IsJutyuSuryouNull())
            {
                Suryo.Text = dr.JutyuSuryou.ToString();
            }
            if (!dr.IsJutyuTankaNull())
            {
                Tanka.Text = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsJutyuGokeiNull())
            {
                Uriage.Text = dr.JutyuGokei.ToString("0,0");
            }
            if (!dr.IsMekarHinbanNull())
            {
                TbxMakerNo.Text = dr.MekarHinban;
                LblProduct.Text = dr.MekarHinban;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                Baitai.Text = dr.KeitaiMei;
                RcbMedia.SelectedValue = dr.KeitaiMei;
            }
            if (!dr.IsShiireKingakuNull())
            {
                ShiireKingaku.Text = dr.ShiireKingaku.ToString("0,0");
            }
            if (!dr.IsShiireTankaNull())
            {
                ShiireTanka.Text = dr.ShiireTanka.ToString("0,0");
                TbxShiirePrice.Text = dr.ShiireTanka.ToString("0,0");
            }
            if (!dr.IsWareHouseNull())
            {
                WareHouse.Text = dr.WareHouse;
                TbxWareHouse.Text = dr.WareHouse;
            }
            if (!dr.IsShiireNameNull())
            {
                RcbShiireName.Text = dr.ShiireName;
                Hachu.Text = dr.ShiireName;
            }
            if (!dr.IsShiiresakiCodeNull())
            {
                Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
            }
            if (!dr.IsKakeritsuNull())
            {
                Kakeri.Text = dr.Kakeritsu;
            }
            if (!dr.IsZeiKubunNull())
            {
                zeiku.Text = dr.ZeiKubun;
            }
            if (!dr.IsCateGoryNull())
            {
                LblCateCode.Text = dr.CateGory.ToString();
            }

            if (!dr.IsCategoryNameNull())
            {
                LblCategoryName.Text = dr.CategoryName;
            }
            if (!dr.IsSisetuJusyo1Null())
            {
                TbxFaciAdress.Text = dr.SisetuJusyo1;
            }
            if (!dr.IsSisetuTantoNull())
            {
                TbxFacilityResponsible.Text = dr.SisetuTanto;
            }
            if (!dr.IsSisetuPostNull())
            {
                TbxYubin.Text = dr.SisetuPost;
            }
            if (!dr.IsSisetuCityCodeNull())
            {
                RcbCity.SelectedValue = dr.SisetuCityCode.ToString();
                DataMaster.M_CityRow drC = ClassMaster.GetCity(dr.SisetuCityCode.ToString(), Global.GetConnection());
                RcbCity.Text = drC.CityName;
            }
            if (!dr.IsSisetuCodeNull())
            {
                TbxFacilityCode.Text = dr.SisetuCode.ToString();
            }

            if (!dr.IsSisetuMeiNull())
            {
                TbxFacilityName.Text = dr.SisetuMei;
            }
            if (!dr.IsSisetsuMei2Null())
            {
                TbxFacilityName2.Text = dr.SisetsuMei2;
            }
            if (!dr.IsSisetsuAbbrevirationNull())
            {
                TbxFaci.Text = dr.SisetsuAbbreviration;
                ShiyouShisetsu.Text = dr.SisetsuAbbreviration;
            }
            if (!dr.IsSisetsuTellNull())
            {
                TbxTel.Text = dr.SisetsuTell;
            }
            if (!dr.IsSisetuRowCodeNull())
            {
                TbxFacilityRowCode.Text = dr.SisetuRowCode;
            }
            if (!dr.IsPermissionStartNull())
            {
                RdpPermissionstart.SelectedDate = dr.PermissionStart;
            }
            if (!dr.IsRightEndNull())
            {
                RdpRightEnd.SelectedDate = dr.RightEnd;
            }
            if (!dr.IsCpStartNull())
            {
                RdpCpStart.SelectedDate = dr.CpStart;
            }
            if (!dr.IsCpEndNull())
            {
                RdpCpEnd.SelectedDate = dr.CpEnd;
            }
            if (!dr.IsShiiresakiCodeNull() && dr.CategoryName == "上映会")
            {
                DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(dr.ShiiresakiCode.ToString(), dr.KeitaiMei, dr.Zasu, Global.GetConnection());
                RcbHanni.Items.Clear();
                if (dtJ.Count > 0)
                {
                    for (int items = 0; items < dtJ.Count; items++)
                    {
                        RcbHanni.Items.Add(dtJ[items].Range);
                    }
                    RcbHanni.SelectedItem.Text = dr.Range;
                    TbxHanni.Text = dr.Range;
                    Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
                    LblShiireCode.Text = dr.ShiiresakiCode.ToString();
                    Zasu.SelectedValue = dr.Zasu;
                }
            }
            else
            {
                if (!dr.IsShiiresakiCodeNull())
                {
                    LblShiireCode.Text = dr.ShiiresakiCode.ToString();
                }
                if (!dr.IsRangeNull())
                {
                    LblHanni.Text = dr.Range;
                    TbxHanni.Text = dr.Range;
                }
            }
        }

        internal DataMitumori.T_MitumoriRow ItemGet2(DataMitumori.T_MitumoriRow dr)
        {
            if (Tekiyo.Text != "")
            {
                dr.Tekiyou1 = Tekiyo.Text;
            }
            if (StartDate.SelectedDate.ToString() != "")
            {
                dr.SiyouKaishi = StartDate.SelectedDate.Value;
            }
            if (EndDate.SelectedDate.ToString() != "")
            {
                dr.SiyouOwari = EndDate.SelectedDate.Value;
            }
            if (TbxProductName.Text != "")
            {
                dr.SyouhinMei = TbxProductName.Text;
            }
            if (TbxProductCode.Text != "")
            {
                dr.SyouhinCode = TbxProductCode.Text;
            }
            if (TbxHyoujun.Text != "")
            {
                dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
            }
            if (Kingaku.Text != "")
            {
                dr.Ryoukin = Kingaku.Text.Replace(",", "");
            }
            if (Suryo.Text != "")
            {
                dr.JutyuSuryou = int.Parse(Suryo.Text);
            }
            if (Tanka.Text != "")
            {
                dr.JutyuTanka = int.Parse(Tanka.Text.Replace(",", ""));
            }
            if (Uriage.Text != "")
            {
                dr.Uriage = int.Parse(Uriage.Text.Replace(",", ""));
                dr.JutyuGokei = int.Parse(Uriage.Text.Replace(",", ""));
            }
            if (Zasu.Text != "")
            {
                dr.Zasu = Zasu.Text;
            }
            if (TbxMakerNo.Text != "")
            {
                dr.MekarHinban = TbxMakerNo.Text;
            }

            dr.Range = TbxHanni.Text.Trim();

            if (RcbMedia.Text != "")
            {
                dr.KeitaiMei = RcbMedia.Text;
            }
            if (ShiireKingaku.Text != "")
            {
                dr.ShiireKingaku = int.Parse(ShiireKingaku.Text.Replace(",", ""));
            }
            if (ShiireTanka.Text != "")
            {
                dr.ShiireTanka = int.Parse(ShiireTanka.Text.Replace(",", ""));
            }
            if (TbxWareHouse.Text != "")
            {
                dr.WareHouse = TbxWareHouse.Text;
            }
            if (RcbShiireName.Text != "")
            {
                dr.ShiireName = RcbShiireName.Text;
            }
            if (LblShiireCode.Text != "")
            {
                dr.ShiiresakiCode = int.Parse(LblShiireCode.Text);
            }
            if (Kakeri.Text != "")
            {
                dr.Kakeritsu = Kaihatsu.strKakeritsu;
            }
            if (zeiku.Text != "")
            {
                dr.Zeikubun = Kaihatsu.strZeikubun.Trim();
            }
            if (LblCateCode.Text != "")
            {
                dr.CateGory = int.Parse(LblCateCode.Text);
            }
            if (LblCategoryName.Text != "")
            {
                dr.CategoryName = LblCategoryName.Text;
            }
            if (TbxFaciAdress.Text != "")
            {
                dr.SisetuJusyo1 = TbxFaciAdress.Text;
            }
            if (TbxFacilityResponsible.Text != "")
            {
                dr.SisetuTanto = TbxFacilityResponsible.Text;
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
            if (TbxFacilityName.Text != "")
            {
                dr.SisetuMei = TbxFacilityName.Text;
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
            if (TbxFacilityRowCode.Text != "")
            {
                dr.SisetuRowCode = TbxFacilityRowCode.Text;
            }
            if (RdpPermissionstart.SelectedDate.ToString() != "")
            {
                dr.PermissionStart = RdpPermissionstart.SelectedDate.Value;
            }
            if (RdpRightEnd.SelectedDate.ToString() != "")
            {
                dr.RightEnd = RdpRightEnd.SelectedDate.Value;
            }
            if (RdpCpStart.SelectedDate.ToString() != "")
            {
                dr.CpStart = RdpCpStart.SelectedDate.Value;
            }
            if (RdpCpEnd.SelectedDate.ToString() != "")
            {
                dr.CpEnd = RdpCpEnd.SelectedDate.Value;
            }
            dr.MitumoriBi = DateTime.Now;
            return dr;
        }

        internal DataJutyu.T_JutyuRow ItemGet3(DataJutyu.T_JutyuRow dr)
        {
            if (StartDate.SelectedDate.ToString() != "")
            {
                dr.SiyouKaishi = StartDate.SelectedDate.Value;
            }
            if (EndDate.SelectedDate.ToString() != "")
            {
                dr.SiyouOwari = EndDate.SelectedDate.Value;
            }
            if (TbxProductName.Text != "")
            {
                dr.SyouhinMei = TbxProductName.Text;
            }
            if (TbxProductCode.Text != "")
            {
                dr.SyouhinCode = TbxProductCode.Text;
            }
            if (TbxHyoujun.Text != "")
            {
                dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
            }
            if (Kingaku.Text != "")
            {
                dr.Ryoukin = Kingaku.Text.Replace(",", "");
            }
            if (Suryo.Text != "")
            {
                dr.JutyuSuryou = int.Parse(Suryo.Text);
            }
            if (Tanka.Text != "")
            {
                dr.JutyuTanka = int.Parse(Tanka.Text.Replace(",", ""));
            }
            if (Uriage.Text != "")
            {
                dr.Uriagekeijyou = int.Parse(Uriage.Text.Replace(",", ""));
                dr.JutyuGokei = int.Parse(Uriage.Text.Replace(",", ""));
            }
            if (Zasu.Text != "")
            {
                dr.Zasu = Zasu.Text;
            }
            if (TbxMakerNo.Text != "")
            {
                dr.MekarHinban = TbxMakerNo.Text;
            }

            dr.Range = TbxHanni.Text.Trim();

            if (RcbMedia.Text != "")
            {
                dr.KeitaiMei = RcbMedia.Text;
            }
            if (ShiireKingaku.Text != "")
            {
                dr.ShiireKingaku = int.Parse(ShiireKingaku.Text.Replace(",", ""));
            }
            if (ShiireTanka.Text != "")
            {
                dr.ShiireTanka = int.Parse(ShiireTanka.Text.Replace(",", ""));
            }
            if (TbxWareHouse.Text != "")
            {
                dr.WareHouse = TbxWareHouse.Text;
            }
            if (RcbShiireName.Text != "")
            {
                dr.ShiireName = RcbShiireName.Text;
            }
            if (LblShiireCode.Text != "")
            {
                dr.ShiiresakiCode = int.Parse(LblShiireCode.Text);
            }
            if (Kakeri.Text != "")
            {
                dr.Kakeritsu = Kaihatsu.strKakeritsu;
            }
            if (zeiku.Text != "")
            {
                dr.ZeiKubun = Kaihatsu.strZeikubun.Trim();
            }
            if (LblCateCode.Text != "")
            {
                dr.CateGory = int.Parse(LblCateCode.Text);
            }
            if (LblCategoryName.Text != "")
            {
                dr.CategoryName = LblCategoryName.Text;
            }
            if (TbxFaciAdress.Text != "")
            {
                dr.SisetuJusyo1 = TbxFaciAdress.Text;
            }
            if (TbxFacilityResponsible.Text != "")
            {
                dr.SisetuTanto = TbxFacilityResponsible.Text;
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
            if (TbxFacilityName.Text != "")
            {
                dr.SisetuMei = TbxFacilityName.Text;
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
            if (TbxFacilityRowCode.Text != "")
            {
                dr.SisetuRowCode = TbxFacilityRowCode.Text;
            }
            if (RdpPermissionstart.SelectedDate.ToString() != "")
            {
                dr.PermissionStart = RdpPermissionstart.SelectedDate.Value;
            }
            if (RdpRightEnd.SelectedDate.ToString() != "")
            {
                dr.RightEnd = RdpRightEnd.SelectedDate.Value;
            }
            if (RdpCpStart.SelectedDate.ToString() != "")
            {
                dr.CpStart = RdpCpStart.SelectedDate.Value;
            }
            if (RdpCpEnd.SelectedDate.ToString() != "")
            {
                dr.CpEnd = RdpCpEnd.SelectedDate.Value;
            }
            dr.JutyuBi = DateTime.Now;
            return dr;
        }


        internal DataMitumori.T_RowRow ItemGet(DataMitumori.T_RowRow dr)
        {
            if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
            {
                dr.SisetuCode = int.Parse(TbxFacilityCode.Text);
            }
            if (!string.IsNullOrEmpty(StartDate.SelectedDate.ToString()))
            {
                dr.SiyouKaishi = DateTime.Parse(StartDate.SelectedDate.ToString());
            }
            if (!string.IsNullOrEmpty(EndDate.SelectedDate.ToString()))
            {
                dr.SiyouOwari = DateTime.Parse(EndDate.SelectedDate.ToString());
            }
            if (!string.IsNullOrEmpty(TbxProductName.Text))
            {
                dr.SyouhinMei = TbxProductName.Text;
            }
            if (!string.IsNullOrEmpty(TbxProductCode.Text))
            {
                dr.SyouhinCode = TbxProductCode.Text;
            }
            if (!string.IsNullOrEmpty(TbxHyoujun.Text))
            {
                dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(Kingaku.Text))
            {
                dr.Ryoukin = Kingaku.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(Suryo.Text))
            {
                dr.JutyuSuryou = int.Parse(Suryo.Text);
            }
            if (!string.IsNullOrEmpty(Tanka.Text))
            {
                dr.JutyuTanka = int.Parse(Tanka.Text.Replace(",", ""));
            }
            if (!string.IsNullOrEmpty(Uriage.Text))
            {
                dr.JutyuGokei = int.Parse(Uriage.Text.Replace(",", ""));
            }
            if (!string.IsNullOrEmpty(Zasu.Text))
            {
                dr.Zasu = Zasu.Text;
            }
            if (!string.IsNullOrEmpty(TbxMakerNo.Text))
            {
                dr.MekarHinban = TbxMakerNo.Text;
            }
            if (!string.IsNullOrEmpty(HidHanni.Value))
            {
                dr.Range = HidHanni.Value;
            }
            if (!string.IsNullOrEmpty(RcbHanni.Text))
            {
                dr.Range = RcbHanni.Text;
            }
            if (!string.IsNullOrEmpty(HidMedia.Value))
            {
                dr.Media = HidMedia.Value;
            }
            if (!string.IsNullOrEmpty(Baitai.Text))
            {
                dr.Media = Baitai.Text;
            }
            if (!string.IsNullOrEmpty(ShiireKingaku.Text))
            {
                dr.ShiireKingaku = ShiireKingaku.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(ShiireTanka.Text))
            {
                dr.ShiireTanka = ShiireTanka.Text.Replace(",", "");
            }
            if (!string.IsNullOrEmpty(WareHouse.Text))
            {
                dr.WareHouse = WareHouse.Text;
            }
            if (!string.IsNullOrEmpty(RcbShiireName.Text))
            {
                dr.ShiiresakiMei = RcbShiireName.Text;
            }
            if (!string.IsNullOrEmpty(LblShiireCode.Text))
            {
                dr.ShiiresakiCode = LblShiireCode.Text;
            }
            if (!string.IsNullOrEmpty(Kakeri.Text))
            {
                dr.Kakeritsu = Kakeri.Text;
            }
            if (!string.IsNullOrEmpty(zeiku.Text))
            {
                dr.Zeiku = zeiku.Text;
            }
            if (!string.IsNullOrEmpty(LblCateCode.Text))
            {
                dr.CategoryCode = LblCateCode.Text;
            }
            if (!string.IsNullOrEmpty(LblCategoryName.Text))
            {
                dr.CategoryName = LblCategoryName.Text;
            }
            if (!string.IsNullOrEmpty(TbxFaciAdress.Text))
            {
                dr.SisetsuJusyo = TbxFaciAdress.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
            {
                dr.SisetsuTanto = TbxFacilityResponsible.Text;
            }
            if (!string.IsNullOrEmpty(TbxYubin.Text))
            {
                dr.SisetsuPost = TbxYubin.Text;
            }
            if (!string.IsNullOrEmpty(RcbCity.SelectedValue))
            {
                dr.SisetsuCityCode = RcbCity.SelectedValue;
            }
            if (!string.IsNullOrEmpty(TbxFacilityName.Text))
            {
                dr.SisetsuMei1 = TbxFacilityName.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
            {
                dr.SisetsuMei2 = TbxFacilityName2.Text;
            }
            if (!string.IsNullOrEmpty(TbxFaci.Text))
            {
                dr.SisetsuAbbreviration = TbxFaci.Text;
            }
            if (!string.IsNullOrEmpty(TbxTel.Text))
            {
                dr.SisetsuTell = TbxTel.Text;
            }
            if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
            {
                dr.SisetsuRowCode = TbxFacilityRowCode.Text;
            }
            if (!string.IsNullOrEmpty(RdpPermissionstart.SelectedDate.ToString()))
            {
                dr.PermissionStart = RdpPermissionstart.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(RdpRightEnd.SelectedDate.ToString()))
            {
                dr.PermissionEnd = RdpRightEnd.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(RdpCpStart.SelectedDate.ToString()))
            {
                dr.Cpkaishi = RdpCpStart.SelectedDate.ToString();
            }
            if (!string.IsNullOrEmpty(RdpCpEnd.SelectedDate.ToString()))
            {
                dr.CpOwari = RdpCpEnd.SelectedDate.ToString();
            }
            return dr;
        }
    }
}