using DLL;
using System;
using Telerik.Web.UI;
using System.Windows.Forms;
using System.Drawing;


namespace Gyomu
{
    public partial class CtrlMitsuSyousai : System.Web.UI.UserControl
    {

        private int sTest
        {
            get
            {
                if (null == this.ViewState["sTest"])
                    return 0;
                return Convert.ToInt32(this.ViewState["sTest"]);
            }
            set
            {
                this.ViewState["sTest"] = value;
            }
        }

        public static DataMitumori.T_RowRow dataT_row;
        public static DataMitumori.T_RowDataTable dataT_RowTable;
        public static string ProductName;
        public static string strColor;
        public static string strCp;
        public static string strCpOver;
        public static string strpermission;
        public static string strCategoryName;
        public static string strCategoryCode;

        public void Create(string strCategoryCode)
        {
            this.sTest = int.Parse(strCategoryCode);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //LblHanni.Text = HidHanni.Value;
            SyouhinSyousai.Style["display"] = "none";
            SisetuSyousai.Style["display"] = "none";
            RcbHanni.Style["display"] = "none";
            err.Text = "";
            end.Text = "";
            Kakeri.Text = Kaihatsu.strKakeritsu;
            HyoujyunTanka.Attributes["onkeydown"] = string.Format("HyoujunKeyDown('{0}');", HyoujyunTanka.ClientID);
            Tanka.Attributes["onkeydown"] = string.Format("TankaKeyDown('{0}');", Tanka.ClientID);
            ShiireTanka.Attributes["onkeydown"] = string.Format("ShiireKeyDown('{0}');", ShiireTanka.ClientID);
            strColor = HidColor.Value;
            if (RcbCity.Items.Count == 0)
            {
                ListSet.SetCity(RcbCity);
            }
            if (RcbHanni.Items.Count == 0)
            {
                //ListSet.SetHanni(RcbHanni);
                //RcbHanni.Text = "席数から選択して下さい。";
            }
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(strCp))
                {
                    HidCp.Value = strCp;
                }
                if (!string.IsNullOrEmpty(strCpOver))
                {
                    HidCpOver.Value = strCpOver;
                }
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

                if (TbxZasu.Visible && TbxZasu.Text == "")
                {
                    TbxZasu.Focus();
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

        //private void GetKensakuProduct(ClassKensaku.KensakuParam s)
        //{
        //    TbxProductCode.Text = "";
        //    LblShiireCode.Text = "";
        //    RcbShiireName.Text = "";
        //    TbxMakerNo.Text = "";
        //    TbxShiirePrice.Text = "";
        //    RcbMedia.Text = "";
        //    TbxCpKakaku.Text = "";
        //    TbxHanni.Text = "";
        //    TbxCpShiire.Text = "";
        //    LblCateCode.Text = "";
        //    RdpCpStart.SelectedDate = null;
        //    LblCategoryName.Text = "";
        //    RdpCpEnd.SelectedDate = null;
        //    TbxTyokuso.Text = "";
        //    TbxWareHouse.Text = "";
        //    TbxHyoujun.Text = "";
        //    TbxRiyo.Text = "";
        //    RdpPermissionstart.SelectedDate = null;
        //    RdpRightEnd.SelectedDate = null;
        //    try
        //    {
        //        string cate = "";
        //        string syouhinCode = "";
        //        string media = "";
        //        string hanni = "";
        //        string str = SerchProduct.SelectedValue;
        //        string[] arr = str.Split('/');
        //        syouhinCode += arr[0];
        //        media += arr[1];
        //        hanni += arr[2];

        //        cate = category.Value;

        //        DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.Getproduct6(syouhinCode, cate, media, hanni, Global.GetConnection());
        //        for (int j = 0; j < dt.Count; j++)
        //        {
        //            DataSet1.M_Kakaku_2Row dr = dt.Rows[j] as DataSet1.M_Kakaku_2Row;
        //            TbxProductCode.Text = dr.SyouhinCode;
        //            Baitai.Text = media;
        //            RcbMedia.SelectedValue = media;
        //            LblCateCode.Text = dr.CategoryCode.ToString();
        //            LblCategoryName.Text = dr.Categoryname.ToString();
        //            //HyoujyunTanka.Text = dr.HyojunKakaku.ToString("0,0");
        //            string hk = dr.HyoujunKakaku.ToString();
        //            ht.Value = hk;
        //            LblProduct.Text = dr.Makernumber;
        //            TbxMakerNo.Text = dr.Makernumber;

        //            if (!dr.IsPermissionStartNull())
        //            {
        //                RdpPermissionstart.SelectedDate = DateTime.Parse(dr.PermissionStart);
        //            }
        //            if (!dr.IsRightEndNull() && dr.RightEnd != "")
        //            {
        //                string d = dr.RightEnd;
        //                RdpRightEnd.SelectedDate = DateTime.Parse(dr.RightEnd);
        //            }

        //            if (!dr.IsCpKaisiNull() && dr.CpKaisi != "")
        //            {
        //                RdpCpStart.SelectedDate = DateTime.Parse(dr.CpKaisi);
        //            }
        //            if (!dr.IsCpOwariNull() && dr.CpOwari != "")
        //            {
        //                RdpCpEnd.SelectedDate = DateTime.Parse(dr.CpOwari);
        //            }

        //            if (cate == 205.ToString())
        //            {
        //                DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(dr.ShiireCode, dr.Media, TbxZasu.Text, Global.GetConnection());
        //                RcbHanni.Items.Clear();
        //                for (int items = 0; items < dtJ.Count; items++)
        //                {
        //                    RcbHanni.Items.Add(dtJ[items].Range);
        //                }
        //                if (dtJ.Count > 0)
        //                {
        //                    Hachu.Text = dtJ[0].ShiiresakiName;
        //                    Hachu.SelectedValue = dtJ[0].ShiiresakiCode.ToString().Trim();
        //                    RcbShiireName.Text = dtJ[0].ShiiresakiName;
        //                    LblShiireCode.Text = dr.ShiireCode;
        //                    string kakakuhyoujyun = dtJ[0].HyoujunKakaku;
        //                    string kakakushiire = dtJ[0].ShiireKakaku;
        //                    double h = double.Parse(dtJ[0].HyoujunKakaku);
        //                    double k = double.Parse(Kakeri.Text);
        //                    double t = h * k / 100;
        //                    double tt = Math.Floor(t);
        //                    CtlKeisan(kakakuhyoujyun, kakakushiire, tt);
        //                }
        //            }
        //            else
        //            {
        //                TbxHyoujun.Text = dr.HyoujunKakaku.ToString("0,0");
        //                LblHanni.Text = dr.Hanni;
        //                TbxHanni.Text = dr.Hanni;
        //                RcbShiireName.Text = dr.ShiireName;
        //                if (!dr.IsShiireNameNull())
        //                {
        //                    Hachu.Text = dr.ShiireName;
        //                }
        //                Hachu.SelectedValue = dr.ShiireCode;
        //                LblShiireCode.Text = dr.ShiireCode;
        //                RcbMedia.SelectedValue = dr.Media;

        //                //ShiireTanka.Text = dr.ShiireKakaku.ToString("0,0");
        //                string sk = dr.ShiireKakaku.ToString();
        //                TbxShiirePrice.Text = dr.ShiireKakaku.ToString("0,0");
        //                st.Value = sk;

        //                string kakakuhyoujyun = ht.Value;
        //                string kakakushiire = st.Value;
        //                double h = double.Parse(ht.Value);
        //                double k = double.Parse(Kakeri.Text);
        //                double t = h * k / 100;
        //                double tt = Math.Floor(t);
        //                CtlKeisan(kakakuhyoujyun, kakakushiire, tt);

        //            }

        //            SerchProduct.Text = dr.SyouhinMei;
        //            TbxProductName.Text = dr.SyouhinMei;

        //            if (!dr.IsWareHouseNull())
        //            {
        //                WareHouse.Text = dr.WareHouse;
        //                TbxWareHouse.Text = dr.WareHouse;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        err.Text = "商品選択時にエラーが発生しました。";
        //    }
        //}

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
                        TBZasu.Style["display"] = "none";
                        RcbHanni.Style["display"] = "none";
                        SerchProduct.Style["display"] = "";
                        SerchProductJouei.Style["display"] = "none";
                        break;
                    case 205:
                        StartDate.Style["display"] = "";
                        EndDate.Style["display"] = "";
                        TBZasu.Style["display"] = "";
                        RcbHanni.Style["display"] = "";
                        SerchProductJouei.Style["display"] = "";
                        SerchProduct.Style["display"] = "none";
                        break;
                    default:
                        StartDate.Style["display"] = "";
                        EndDate.Style["display"] = "";
                        TBZasu.Style["display"] = "none";
                        RcbHanni.Style["display"] = "none";
                        SerchProductJouei.Style["display"] = "none";
                        SerchProduct.Style["display"] = "";
                        break;
                }
            }
            else
            {
                StartDate.Visible = EndDate.Visible = true;
                TBZasu.Visible = false;
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
            ListSet.GetFacility(sender, e);
        }

        protected void Hachu_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetShiireSaki4(sender, e);
        }

        protected void ShiyouShisetsu_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string[] fac = ShiyouShisetsu.SelectedValue.Split('/');
            FacilityCode.Value = fac[0];
            string cc = "";
            DataSet1.M_Facility_NewRow dr = Class1.GetFacilitySyousai(fac, Global.GetConnection());
            FacilityAddress.Value = dr.Address1 + dr.Address2;
            TbxFacilityCode.Text = dr.FacilityNo.ToString();
            TbxFacilityRowCode.Text = dr.Code;
            cc = dr.CityCode.ToString();
            TbxFacilityName.Text = dr.FacilityName1;
            TbxFacilityName2.Text = dr.FacilityName2;
            TbxFaci.Text = dr.Abbreviation;
            TbxFacilityResponsible.Text = dr.FacilityResponsible;
            TbxYubin.Text = dr.PostNo;
            TbxFaciAdress.Text = dr.Address1 + dr.Address2;
            TbxTel.Text = dr.Tell;

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
            try
            {
                string cate = LblCateCode.Text;

                if (cate == "")
                {
                    ErrorSet(5);
                }
                else
                {
                    ListSet.SettingProduct(sender, e, cate, Kaihatsu.strSyokaiDate);
                    procd.Value = SerchProduct.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
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
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
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
            DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(HidShiireCode.Value, RcbMedia.Text, TbxZasu.Text, Global.GetConnection());
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
            if (TbxZasu.Text != "")
            {
                TbxHanni.Text = RcbHanni.Text;
                string strShiireCode = RcbShiireName.SelectedValue;
                string strMedia = RcbMedia.Text;
                string strHanni = RcbHanni.SelectedItem.Text;
                string strSeki = TbxZasu.Text;
                DataMaster.M_JoueiKakaku2DataTable dr = ClassMaster.GetJoueiKakaku(strShiireCode, strMedia, strHanni, strSeki, Global.GetConnection());
                if (dr != null)
                {
                    for (int i = 0; i < dr.Count; i++)
                    {
                        string[] AryCapacity = dr[i].Capacity.Split('~');
                        if (AryCapacity.Length > 1)
                        {
                            if (int.Parse(AryCapacity[1]) >= int.Parse(strSeki) || int.Parse(strSeki) >= int.Parse(AryCapacity[0]))
                            {
                                if (dr[i].HyoujunKakaku != "OPEN")
                                {
                                    int hk = int.Parse(dr[i].HyoujunKakaku);
                                    int sk = int.Parse(dr[i].ShiireKakaku);
                                    int kake = int.Parse(Kakeri.Text.Trim());
                                    double t = hk * kake / 100;
                                    double tt = Math.Floor(t);
                                    CtlKeisan(dr[i].HyoujunKakaku, dr[i].ShiireKakaku, tt);
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
                                return;
                            }
                        }
                        else
                        {
                            if (int.Parse(AryCapacity[0]) <= int.Parse(strSeki))
                            {
                                if (dr[i].HyoujunKakaku != "OPEN")
                                {
                                    int hk = int.Parse(dr[i].HyoujunKakaku);
                                    int sk = int.Parse(dr[i].ShiireKakaku);
                                    int kake = int.Parse(Kakeri.Text.Trim());
                                    double t = hk * kake / 100;
                                    double tt = Math.Floor(t);
                                    CtlKeisan(dr[i].HyoujunKakaku, dr[i].ShiireKakaku, tt);
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
                                return;
                            }
                        }

                    }
                }
                else
                {
                    ErrorSet(8);
                    return;
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
                case 8:
                    err.Text = "選択した席数と使用範囲は上映会価格表にはありません";
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

        internal void CategoryChange(string selectedValue)
        {
            category.Value = selectedValue;
            categoryV(selectedValue);
            if (!string.IsNullOrEmpty(SerchProduct.Text))
            {
                DataSet1.M_Kakaku_2Row dr = Class1.Getproduct2(TbxMakerNo.Text, RcbMedia.SelectedValue, TbxHanni.Text, selectedValue, Global.GetConnection());
                if (dr != null)
                {
                    TbxProductCode.Text = dr.SyouhinCode;
                    TbxProductName.Text = dr.SyouhinMei;
                    TbxMakerNo.Text = dr.Makernumber;
                    RcbMedia.SelectedValue = dr.Media;
                    TbxHanni.Text = dr.Hanni;
                    TbxTyokuso.Text = "";
                    TbxHyoujun.Text = dr.HyoujunKakaku.ToString("0,0");
                    if (!dr.IsPermissionStartNull())
                    {
                        RdpPermissionstart.SelectedDate = DateTime.Parse(dr.PermissionStart);
                    }
                    if (!dr.IsRightEndNull())
                    {
                        RdpRightEnd.SelectedDate = DateTime.Parse(dr.RightEnd);
                    }
                    LblShiireCode.Text = dr.ShiireCode;
                    if (!dr.IsShiireNameNull())
                    {
                        RcbShiireName.Text = dr.ShiireName;
                    }
                    TbxShiirePrice.Text = dr.ShiireKakaku.ToString("0,0");
                    if (!dr.IsCpKakakuNull())
                    {
                        if (!string.IsNullOrEmpty(dr.CpKakaku))
                        {
                            TbxCpKakaku.Text = int.Parse(dr.CpKakaku).ToString("0,0");
                        }
                    }
                    if (!dr.IsCpShiireNull())
                    {
                        if (!string.IsNullOrEmpty(dr.CpShiire))
                        {
                            TbxCpShiire.Text = int.Parse(dr.CpShiire).ToString("0,0");
                        }
                    }
                    if (!dr.IsCpKaisiNull())
                    {
                        if (!string.IsNullOrEmpty(dr.CpKaisi))
                        {
                            RdpCpStart.SelectedDate = DateTime.Parse(dr.CpKaisi);
                        }
                    }
                    if (!dr.IsCpOwariNull())
                    {
                        if (!string.IsNullOrEmpty(dr.CpOwari))
                        {
                            RdpCpEnd.SelectedDate = DateTime.Parse(dr.CpOwari);
                        }
                    }
                    if (!dr.IsWareHouseNull())
                    {
                        TbxWareHouse.Text = dr.WareHouse;
                    }
                }
                else
                {
                    err.Text = "商品を再選択して下さい。";
                }
            }
        }

        internal DataMitumori.T_RowRow ItemGet()
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            try
            {
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

                if (!string.IsNullOrEmpty(TbxCpKakaku.Text))
                {
                    dr.CpKakaku = TbxCpKakaku.Text.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TbxCpShiire.Text))
                {
                    dr.CpShiire = TbxCpShiire.Text.Replace(",", "");
                }

                if (!string.IsNullOrEmpty(TbxProductCode.Text))
                {
                    dr.SyouhinCode = TbxProductCode.Text;
                }
                if (!string.IsNullOrEmpty(TbxHyoujun.Text))
                {
                    dr.HyojunKakaku = TbxHyoujun.Text.Replace(",", "");
                }
                else if (!string.IsNullOrEmpty(HyoujyunTanka.Text))
                {
                    dr.HyojunKakaku = HyoujyunTanka.Text.Replace(",", "");
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
                if (!string.IsNullOrEmpty(TbxZasu.Text))
                {
                    dr.Zasu = TbxZasu.Text;
                }
                if (!string.IsNullOrEmpty(TbxMakerNo.Text))
                {
                    dr.MekarHinban = TbxMakerNo.Text;
                }
                //LblHanni.Text = HidHanni.Value;
                LblHanni.Text = TbxHanni.Text;
                if (!string.IsNullOrEmpty(LblHanni.Text))
                {
                    dr.Range = LblHanni.Text;
                }
                else
                {
                    dr.Range = "";
                }
                if (RcbHanni.SelectedItem != null)
                {
                    if (!string.IsNullOrEmpty(RcbHanni.SelectedItem.Text))
                    {
                        dr.Range = RcbHanni.SelectedItem.Text;
                    }
                }
                Baitai.Text = RcbMedia.Text;
                if (!string.IsNullOrEmpty(Baitai.Text))
                {
                    dr.Media = Baitai.Text;
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
                if (!string.IsNullOrEmpty(TbxWareHouse.Text))
                {
                    dr.WareHouse = TbxWareHouse.Text;
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
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
            return dr;
        }

        internal void ItemSet(DataMitumori.T_RowRow dr)
        {
            try
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
                    ListSet.SetCity(RcbCity);
                    RcbCity.SelectedValue = dr.SisetsuCityCode;
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
                    TbxProductName.Text = dr.SyouhinMei.Replace("*", "");
                    SerchProduct.Text = dr.SyouhinMei.Replace("*", "");
                    ProductName = dr.SyouhinMei.Replace("*", "");

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
                        if (!dr.Ryoukin.Equals("OPEN"))
                        {
                            Kingaku.Text = int.Parse(dr.Ryoukin).ToString("0,0");
                        }
                        else
                        {
                            Kingaku.Text = "OPEN";
                        }
                    }
                    if (!dr.IsJutyuSuryouNull())
                    {
                        Suryo.Text = dr.JutyuSuryou.ToString();
                    }
                    if (!dr.IsJutyuTankaNull())
                    {
                        if (!dr.JutyuTanka.Equals("OPEN"))
                        {
                            Tanka.Text = dr.JutyuTanka.ToString("0,0");
                        }
                        else
                        {
                            Tanka.Text = "OPEN";
                        }
                    }
                    if (!dr.IsJutyuGokeiNull())
                    {
                        if (!dr.JutyuGokei.Equals("OPEN"))
                        {
                            Uriage.Text = dr.JutyuGokei.ToString("0,0");
                        }
                        else
                        {
                            Uriage.Text = "OPEN";
                        }
                    }
                    if (!dr.IsZasuNull())
                    {
                        TbxZasu.Text = dr.Zasu;
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
                            SerchProductJouei.Text = dr.SyouhinMei;
                            SerchProduct.Style["display"] = "none";
                            string shiireCode = dr.ShiiresakiCode;
                            string media = dr.Media;
                            DataMaster.M_JoueiKakaku2DataTable dt = ClassMaster.GetJouei4(shiireCode, media, Global.GetConnection());
                            if (dt.Count > 0)
                            {
                                RcbHanni.Items.Clear();
                                string added = "";
                                for (int i = 0; i < dt.Count; i++)
                                {
                                    string check = dt[i].Range;
                                    if (!added.Contains(check))
                                    {
                                        added += "/" + check;
                                        RcbHanni.Items.Add(new RadComboBoxItem(check, check));
                                    }
                                }
                                if (!dr.IsRangeNull())
                                {
                                    RcbHanni.SelectedValue = dr.Range;
                                    TbxHanni.Text = dr.Range;
                                }
                                if (!dr.IsHyojunKakakuNull())
                                {
                                    if (!dr.HyojunKakaku.Equals("OPEN"))
                                    {
                                        HyoujyunTanka.Text = int.Parse(dr.HyojunKakaku).ToString("0,0");
                                    }
                                    else
                                    {
                                        HyoujyunTanka.Text = "OPEN";
                                    }
                                }
                            }
                        }
                        else
                        {
                            SerchProductJouei.Style["display"] = "none";
                            if (!dr.IsRangeNull())
                            {
                                LblHanni.Text = dr.Range;
                                RcbHanni.Text = dr.Range;
                                TbxHanni.Text = dr.Range;
                            }
                        }
                    }
                    else
                    {
                        SerchProductJouei.Style["display"] = "none";
                    }
                    if (!dr.IsMediaNull())
                    {
                        Baitai.Text = dr.Media;
                        RcbMedia.SelectedValue = dr.Media;
                    }
                    if (!dr.IsShiireKingakuNull())
                    {
                        if (!dr.ShiireKingaku.Equals("OPEN"))
                        {
                            ShiireKingaku.Text = int.Parse(dr.ShiireKingaku).ToString("0,0");
                        }
                        else
                        {
                            ShiireKingaku.Text = "OPEN";
                        }
                    }
                    if (!dr.IsShiireTankaNull())
                    {
                        if (!dr.ShiireTanka.Equals("OPEN"))
                        {
                            ShiireTanka.Text = int.Parse(dr.ShiireTanka).ToString("0,0");
                            TbxShiirePrice.Text = int.Parse(dr.ShiireTanka).ToString("0,0");
                        }
                        else
                        {
                            ShiireTanka.Text = "OPEN";
                            TbxShiirePrice.Text = "OPEN";
                        }
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
                    if (!dr.IsCpKakakuNull())
                    {
                        TbxCpKakaku.Text = int.Parse(dr.CpKakaku).ToString("0,0");
                    }
                    if (!dr.IsCpShiireNull())
                    {
                        TbxCpShiire.Text = int.Parse(dr.CpShiire).ToString("0,0");
                    }
                    if (dr.SyouhinCode.Equals("1999"))
                    {
                        HyoujyunTanka.ReadOnly = true;
                        Kingaku.ReadOnly = true;
                        ShiireKingaku.ReadOnly = true;
                        ShiireTanka.ReadOnly = true;

                        HyoujyunTanka.Style["backgroundcolor"] = "lightgray";
                        Kingaku.Style["backgroundcolor"] = "lightgray";
                        ShiireKingaku.Style["backgroundcolor"] = "lightgray";
                        ShiireTanka.Style["backgroundcolor"] = "lightgray";
                        Suryo.Style["display"] = "none";
                    }
                }
                else
                {
                    SerchProductJouei.Style["display"] = "none";
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
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
        }

        internal void ItemSet2(DataMitumori.T_MitumoriRow dr)
        {
            try
            {
                if (!dr.IsCpKakakuNull())
                {
                    TbxCpKakaku.Text = int.Parse(dr.CpKakaku).ToString("0,0");
                }
                if (!dr.IsCpShiireNull())
                {
                    TbxCpShiire.Text = int.Parse(dr.CpShiire).ToString("0,0");
                }
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
                    SerchProductJouei.Text = dr.SyouhinMei;
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
                    if (!dr.Ryoukin.Equals("OPEN"))
                    {
                        Kingaku.Text = int.Parse(dr.Ryoukin).ToString("0,0");
                    }
                    else
                    {
                        Kingaku.Text = dr.Ryoukin;
                    }
                }
                if (!dr.IsJutyuSuryouNull())
                {
                    Suryo.Text = dr.JutyuSuryou.ToString();
                }
                if (!dr.IsJutyuTankaNull())
                {
                    if (!dr.JutyuTanka.Equals(0))
                    {
                        Tanka.Text = dr.JutyuTanka.ToString("0,0");
                    }
                    else
                    {
                        Tanka.Text = "";
                    }
                }
                if (!dr.IsJutyuGokeiNull())
                {
                    if (!dr.JutyuGokei.Equals(0))
                    {
                        Uriage.Text = dr.JutyuGokei.ToString("0,0");
                    }
                    else
                    {
                        Uriage.Text = "";
                    }
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
                    if (!dr.ShiireKingaku.Equals(0))
                    {
                        ShiireKingaku.Text = dr.ShiireKingaku.ToString("0,0");
                    }
                    else
                    {
                        ShiireKingaku.Text = "OPEN";
                    }
                }
                if (!dr.IsShiireTankaNull())
                {
                    if (!dr.ShiireTanka.Equals(0))
                    {
                        ShiireTanka.Text = dr.ShiireTanka.ToString("0,0");
                        TbxShiirePrice.Text = dr.ShiireTanka.ToString("0,0");
                    }
                    else
                    {
                        ShiireTanka.Text = "OPEN";
                        TbxShiirePrice.Text = "OPEN";
                    }
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
                    ListSet.SetCity(RcbCity);
                    RcbCity.SelectedValue = dr.SisetuCityCode.ToString();
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
                    //DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei(dr.ShiiresakiCode.ToString(), dr.KeitaiMei, dr.Zasu, Global.GetConnection());
                    //RcbHanni.Items.Clear();
                    //if (dtJ.Count > 0)
                    //{
                    //    for (int items = 0; items < dtJ.Count; items++)
                    //    {
                    //        RcbHanni.Items.Add(dtJ[items].Range);
                    //    }
                    //    RcbHanni.SelectedItem.Text = dr.Range;
                    //    TbxHanni.Text = dr.Range;
                    //    Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
                    //    LblShiireCode.Text = dr.ShiiresakiCode.ToString();
                    //    TbxZasu.Text = dr.Zasu;
                    //}
                    string shiireCode = dr.ShiiresakiCode.ToString();
                    string media = dr.KeitaiMei;
                    DataMaster.M_JoueiKakaku2DataTable dt = ClassMaster.GetJouei4(shiireCode, media, Global.GetConnection());
                    if (dt.Count > 0)
                    {
                        RcbHanni.Items.Clear();
                        string added = "";
                        for (int i = 0; i < dt.Count; i++)
                        {
                            string check = dt[i].Range;
                            if (!added.Contains(check))
                            {
                                added += "/" + check;
                                RcbHanni.Items.Add(new RadComboBoxItem(check, check));
                            }
                        }
                        if (!dr.IsRangeNull())
                        {
                            RcbHanni.SelectedValue = dr.Range;
                            TbxHanni.Text = dr.Range;
                        }
                        TbxZasu.Text = dr.Zasu;
                        SerchProduct.Style["display"] = "none";
                    }
                }
                else
                {
                    SerchProductJouei.Style["display"] = "none";
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
                if (dr.SyouhinCode.Equals("1999"))
                {
                    HyoujyunTanka.ReadOnly = true;
                    Kingaku.ReadOnly = true;
                    ShiireKingaku.ReadOnly = true;
                    ShiireTanka.ReadOnly = true;

                    HyoujyunTanka.Style["backgroundcolor"] = "lightgray";
                    Kingaku.Style["backgroundcolor"] = "lightgray";
                    ShiireKingaku.Style["backgroundcolor"] = "lightgray";
                    ShiireTanka.Style["backgroundcolor"] = "lightgray";

                    Suryo.Style["display"] = "none";

                }
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
        }

        internal void ItemSet3(DataJutyu.T_JutyuRow dr)
        {
            try
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
                if (!dr.IsShiiresakiCodeNull() || dr.CategoryName == "上映会")
                {
                    DataMaster.M_JoueiKakaku2DataTable dtJ = ClassMaster.GetJouei4(dr.ShiiresakiCode.ToString(), dr.KeitaiMei, Global.GetConnection());
                    RcbHanni.Items.Clear();
                    string added = "";
                    for (int i = 0; i < dtJ.Count; i++)
                    {
                        string check = dtJ[i].Range;
                        if (!added.Contains(check))
                        {
                            added += "/" + check;
                            RcbHanni.Items.Add(new RadComboBoxItem(check, check));
                        }
                    }
                    SerchProductJouei.Text = dr.SyouhinMei;
                    RcbHanni.SelectedValue = dr.Range;
                    TbxHanni.Text = dr.Range;
                    Hachu.SelectedValue = dr.ShiiresakiCode.ToString();
                    LblShiireCode.Text = dr.ShiiresakiCode.ToString();
                    TbxZasu.Text = dr.Zasu;

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
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
        }

        internal DataMitumori.T_MitumoriRow ItemGet2(DataMitumori.T_MitumoriRow dr)
        {
            try
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
                if (HyoujyunTanka.Text != "")
                {
                    dr.HyojunKakaku = HyoujyunTanka.Text.Replace(",", "");
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
                if (TbxZasu.Text != "")
                {
                    dr.Zasu = TbxZasu.Text;
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
                    if (!ShiireKingaku.Text.Equals("OPEN"))
                    {
                        dr.ShiireKingaku = int.Parse(ShiireKingaku.Text.Replace(",", ""));
                    }
                    else
                    {
                        dr.ShiireKingaku = 0;
                    }
                }
                if (ShiireTanka.Text != "")
                {
                    if (!ShiireTanka.Text.Equals("OPEN"))
                    {
                        dr.ShiireTanka = int.Parse(ShiireTanka.Text.Replace(",", ""));
                    }
                    else
                    {
                        dr.ShiireTanka = 0;
                    }
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
                if (!string.IsNullOrEmpty(HidShiireCode.Value))
                {
                    dr.ShiiresakiCode = int.Parse(HidShiireCode.Value);
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
                else
                {
                    ClassMail.ErrorMail("maeda@m2m-asp.com", "エラーメール | 見積登録時", "TbxFaciに値がセットされていません。");
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
                if (!string.IsNullOrEmpty(TbxCpKakaku.Text))
                {
                    dr.CpKakaku = TbxCpKakaku.Text.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TbxCpShiire.Text))
                {
                    dr.CpShiire = TbxCpShiire.Text.Replace(",", "");
                }
                dr.MitumoriBi = DateTime.Now;
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
            return dr;
        }

        internal DataJutyu.T_JutyuRow ItemGet3(DataJutyu.T_JutyuRow dr)
        {
            try
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
                    dr.Uriage = int.Parse(Uriage.Text.Replace(",", ""));
                    dr.JutyuGokei = int.Parse(Uriage.Text.Replace(",", ""));
                }
                if (TbxZasu.Text != "")
                {
                    dr.Zasu = TbxZasu.Text;
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
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
            //dr.date = DateTime.Now;
            return dr;
        }

        internal DataMitumori.T_RowRow ItemGet(DataMitumori.T_RowRow dr)
        {
            try
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
                if (!string.IsNullOrEmpty(TbxZasu.Text))
                {
                    dr.Zasu = TbxZasu.Text;
                }
                if (!string.IsNullOrEmpty(TbxMakerNo.Text))
                {
                    dr.MekarHinban = TbxMakerNo.Text;
                }
                if (!string.IsNullOrEmpty(RcbHanni.Text))
                {
                    dr.Range = RcbHanni.Text;
                }
                dr.Range = TbxHanni.Text;
                LblHanni.Text = TbxHanni.Text;

                if (!string.IsNullOrEmpty(RcbMedia.Text))
                {
                    dr.Media = RcbMedia.Text;
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
                if (!string.IsNullOrEmpty(TbxCpKakaku.Text))
                {
                    dr.CpKakaku = TbxCpKakaku.Text.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TbxCpShiire.Text))
                {
                    dr.CpShiire = TbxCpShiire.Text.Replace(",", "");
                }
            }
            catch (Exception ex)
            {
                err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(Kaihatsu.mail_to, Kaihatsu.mail_title, body);
            }
            return dr;
        }

        protected void RcbHanni_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LblShiireCode.Text))
            {
                ListSet.SetHanni2(LblShiireCode.Text, Baitai.Text, RcbHanni);
            }
            if (RcbHanni.Items.Count == 1)
            {
                RcbHanni.Items.Add(new RadComboBoxItem("使用できる使用範囲はありません"));
            }
        }

        protected void TbxZasu_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LblShiireCode.Text))
            {
                if (!string.IsNullOrEmpty(RcbHanni.SelectedValue))
                {
                    DataMaster.M_JoueiKakaku2DataTable dt = ClassMaster.GetJouei5(LblShiireCode.Text, RcbHanni.SelectedValue, RcbMedia.SelectedValue, Global.GetConnection());
                    if (dt.Count > 0)
                    {
                        for (int i = 0; i < dt.Count; i++)
                        {
                            int Capa = int.Parse(TbxZasu.Text);
                            string[] aryCapa = dt[i].Capacity.Split('~');
                            int minCapa = int.Parse(aryCapa[0]);
                            int maxCapa = 0;
                            if (!string.IsNullOrEmpty(aryCapa[1]))
                            {
                                maxCapa = int.Parse(aryCapa[1]);
                            }
                            else
                            {
                                maxCapa = 100000000;
                            }
                            if (minCapa <= Capa && Capa <= maxCapa)
                            {
                                err.Text = "";
                                if (dt[i].HyoujunKakaku.Equals("OPEN"))
                                {
                                    TbxHyoujun.Text = HyoujyunTanka.Text = Kingaku.Text = dt[i].HyoujunKakaku;
                                    TbxShiirePrice.Text = ShiireTanka.Text = ShiireKingaku.Text = dt[i].ShiireKakaku;
                                }
                                else
                                {
                                    HyoujyunTanka.Text = int.Parse(dt[i].HyoujunKakaku).ToString("0,0");
                                    ShiireTanka.Text = int.Parse(dt[i].ShiireKakaku).ToString("0,0");

                                    if (zeiku.Text.Equals("税込"))
                                    {
                                        // 標準単価＆金額（金額 ＝　標準単価 × 数量）
                                        Kingaku.Text = (int.Parse(HyoujyunTanka.Text.Replace(",", "")) * int.Parse(Suryo.Text)).ToString("0,0");
                                        //仕入単価＆仕入金額（仕入金額　＝　仕入単価　×　数量　×　税込なので1.1）
                                        ShiireKingaku.Text = (int.Parse(ShiireTanka.Text.Replace(",", "")) * int.Parse(Suryo.Text) * 1.1).ToString("0,0");
                                        //単価＆売上金額（売上金額　＝　掛率計算済「税込」標準単価　×　数量）
                                        Tanka.Text = (int.Parse(HyoujyunTanka.Text.Replace(",", "")) * int.Parse(Kakeri.Text) * 1.1 / 100).ToString("0,0");
                                        Uriage.Text = (int.Parse(Tanka.Text.Replace(",", "")) * int.Parse(Suryo.Text)).ToString("0,0");
                                    }
                                    else
                                    {
                                        // 標準単価＆金額（金額 ＝　標準単価 × 数量）
                                        Kingaku.Text = (int.Parse(HyoujyunTanka.Text.Replace(",", "")) * int.Parse(Suryo.Text)).ToString("0,0");
                                        //仕入単価＆仕入金額（仕入金額　＝　仕入単価　×　数量　）
                                        ShiireKingaku.Text = (int.Parse(ShiireTanka.Text.Replace(",", "")) * int.Parse(Suryo.Text)).ToString("0,0");
                                        //単価＆売上金額（売上金額　＝　掛率計算済「税込」標準単価　×　数量）
                                        Tanka.Text = (int.Parse(HyoujyunTanka.Text.Replace(",", "")) * int.Parse(Kakeri.Text) / 100).ToString("0,0");
                                        Uriage.Text = (int.Parse(Tanka.Text.Replace(",", "")) * int.Parse(Suryo.Text)).ToString("0,0");
                                    }
                                }
                                break;
                            }
                            else
                            {
                                err.Text = "入力した範囲、席数では価格を設定出来ません。";
                            }
                        }
                    }
                }
                else
                {
                    err.Text = "範囲を選択して下さい";
                }
            }
            else
            {
                err.Text = "商品を選択して下さい";
            }
        }

        //上映会の商品選択radcombobox
        protected void SerchProductJouei_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text.Trim()))
            {
                string cate = Kaihatsu.strCategoryCode;
                ListSet.SettingProduct205(sender, e, Kaihatsu.strSyokaiDate);
                procd.Value = SerchProduct.SelectedValue;
            }
        }

        protected void SerchProductJouei_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string[] aryProductvalue = SerchProductJouei.SelectedValue.Split('^');
            if (aryProductvalue.Length > 1)
            {
                //syouhincode
                if (!string.IsNullOrEmpty(aryProductvalue[0]))
                {
                    TbxProductCode.Text = aryProductvalue[0];
                }
                //syouhinmei
                if (!string.IsNullOrEmpty(aryProductvalue[1]))
                {
                    TbxProductName.Text = SerchProductJouei.Text = aryProductvalue[1];
                }
                //permissionstart
                if (!string.IsNullOrEmpty(aryProductvalue[2]))
                {
                    RdpCpStart.SelectedDate = DateTime.Parse(aryProductvalue[2]);
                }
                //rightend
                if (!string.IsNullOrEmpty(aryProductvalue[3]))
                {
                    RdpCpEnd.SelectedDate = DateTime.Parse(aryProductvalue[3]);
                }
                //categorycode
                if (!string.IsNullOrEmpty(aryProductvalue[4]))
                {
                    LblCateCode.Text = aryProductvalue[4];
                }
                //categoryname
                if (!string.IsNullOrEmpty(aryProductvalue[5]))
                {
                    LblCategoryName.Text = aryProductvalue[5];
                }
                //media
                if (!string.IsNullOrEmpty(aryProductvalue[10]))
                {
                    Baitai.Text = aryProductvalue[10];
                    RcbMedia.SelectedValue = aryProductvalue[10];
                }
                //shiirecode
                if (!string.IsNullOrEmpty(aryProductvalue[11]))
                {
                    LblShiireCode.Text = aryProductvalue[11];
                    DataMaster.M_JoueiKakaku2DataTable dt = ClassMaster.GetJouei4(LblShiireCode.Text, RcbMedia.Text, Global.GetConnection());
                    if (dt.Count > 0)
                    {
                        RcbHanni.Items.Clear();
                        string added = "";
                        for (int i = 0; i < dt.Count; i++)
                        {
                            string check = dt[i].Range;
                            if (!added.Contains(check))
                            {
                                added += "/" + check;
                                RcbHanni.Items.Add(new RadComboBoxItem(check, check));
                            }
                        }
                    }
                }
                //shiirename
                if (!string.IsNullOrEmpty(aryProductvalue[12]))
                {
                    RcbShiireName.Text = aryProductvalue[12];
                    RcbShiireName.SelectedValue = aryProductvalue[11];
                }
                //makernumber
                if (!string.IsNullOrEmpty(aryProductvalue[13]))
                {
                    TbxMakerNo.Text = LblProduct.Text = aryProductvalue[13];
                }
                //warehouse
                if (!string.IsNullOrEmpty(aryProductvalue[15]))
                {
                    TbxWareHouse.Text = WareHouse.Text = aryProductvalue[15];
                }
            }
        }
    }

}