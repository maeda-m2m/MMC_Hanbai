using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using ClosedXML.Excel;

namespace Gyomu.Uriage
{
    public partial class UriageMeisai : System.Web.UI.Page
    {
        public static string strSyokaiDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            LblEnd.Text = "";
            Err.Text = "";
            if (!IsPostBack)
            {
                Create();
                RadDatePicker2.SelectedDate = DateTime.Now;
            }
        }

        private void Create()
        {
            if (SessionManager.HACCYU_NO != "")
            {
                Label94.Text = SessionManager.HACCYU_NO;
                DataUriage.T_UriageHeaderDataTable dtH = ClassUriage.GetUriageHeader(SessionManager.HACCYU_NO, Global.GetConnection());
                DataUriage.T_UriageHeaderRow drr = dtH[0];
                TextBox7.Text = drr.GokeiKingaku.ToString("0,0");
                TextBox8.Text = drr.ShiireKingaku.ToString("0,0");
                TextBox5.Text = drr.SyohiZeiGokei.ToString("0,0");
                TextBox6.Text = drr.ArariGokeigaku.ToString("0,0");
                TextBox12.Text = drr.SoukeiGaku.ToString("0,0");
                TextBox10.Text = drr.SouSuryou.ToString();
                RadComboCategory.Text = drr.CategoryName;
                RadComboCategory.SelectedValue = drr.CateGory.ToString();
                RadComboCategory.SelectedValue = drr.CateGory.ToString();
                RadComboBox1.Text = RadComboBox3.Text = drr.TokuisakiName;
                string[] tokuisaki = drr.TokuisakiCode.Split('/');
                TokuisakiCode.Value = tokuisaki[1];
                RadComboBox1.SelectedValue = drr.TokuisakiCode;

                string name = drr.TantoName;
                DataSet1.M_TantoDataTable dtM = Class1.GetStaff2(name, Global.GetConnection());
                RadComboBox4.Items.Clear();
                for (int t = 0; t < dtM.Count; t++)
                {
                    RadComboBox4.Items.Add(new RadComboBoxItem(dtM[t].BumonName, dtM[t].Bumon.ToString()));
                }

                RadComboBox4.Text = drr.Bumon;
                FacilityRad.Text = drr.FacilityName;
                DataSet1.M_Facility_NewRow drf = Class1.Getfacility(drr.FacilityName, Global.GetConnection());
                FacilityRad.SelectedValue = drf.FacilityNo.ToString();
                RadZeiKubun.SelectedValue = drr.Zeikubun.Trim();


                DataUriage.T_UriageDataTable dt = ClassUriage.GetUriageMeisai(SessionManager.HACCYU_NO, Global.GetConnection());
                CtrlSyousai.DataSource = dt;
                CtrlSyousai.DataBind();
            }
            else
            {
                return;
            }
            if (RadDatePicker1.SelectedDate != null)
            {
                strSyokaiDate = RadDatePicker1.SelectedDate.Value.ToShortDateString();
            }
        }
        //ItemDataBound
        protected void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataUriage.T_UriageRow dr = (e.Item.DataItem as DataRowView).Row as DataUriage.T_UriageRow;
                    UriageMeisaiRan Ctl = e.Item.FindControl("Syosai") as UriageMeisaiRan;
                    Label LblProduct = (Label)Ctl.FindControl("LblProduct");
                    RadComboBox SerchProduct = (RadComboBox)Ctl.FindControl("SerchProduct");
                    Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                    Label Baitai = (Label)Ctl.FindControl("Baitai");
                    TextBox Suryo = (TextBox)Ctl.FindControl("Suryo");
                    TextBox HyoujunTanka = (TextBox)Ctl.FindControl("HyoujyunTanka");
                    TextBox Kingaku = (TextBox)Ctl.FindControl("Kingaku");
                    RadComboBox ShiyouShisetsu = (RadComboBox)Ctl.FindControl("ShiyouShisetsu");
                    TextBox KariFaci = (TextBox)Ctl.FindControl("KariFaci");
                    Label Facility = (Label)Ctl.FindControl("Facility");
                    RadComboBox Zasu = (RadComboBox)Ctl.FindControl("Zasu");
                    RadDatePicker StartDate = (RadDatePicker)Ctl.FindControl("StartDate");
                    RadDatePicker EndDate = (RadDatePicker)Ctl.FindControl("EndDate");
                    Label Kakeri = (Label)Ctl.FindControl("Kakeri");
                    Label zeiku = (Label)Ctl.FindControl("zeiku");
                    TextBox Tanka = (TextBox)Ctl.FindControl("Tanka");
                    TextBox Uriage = (TextBox)Ctl.FindControl("Uriage");

                    //明細のほう
                    LblProduct.Text = dr.MekarHinban;
                    SerchProduct.Text = dr.SyouhinMei;
                    //SerchProduct.SelectedValue = dr.SyouhinCode;
                    LblHanni.Text = dr.Range;
                    Baitai.Text = dr.KeitaiMei;
                    Suryo.Text = dr.JutyuSuryou.ToString();
                    HyoujunTanka.Text = dr.HyojunKakaku.ToString("0,0");
                    Kingaku.Text = int.Parse(dr.Ryoukin).ToString("0,0");
                    ShiyouShisetsu.Text = dr.SisetuMei;
                    if (!dr.IsZasuNull())
                    {
                        Zasu.Text = dr.Zasu;
                    }
                    if (!dr.IsSiyouKaishiNull())
                    {
                        StartDate.SelectedDate = dr.SiyouKaishi;
                        EndDate.SelectedDate = dr.SiyouOwari;
                        RadDatePicker3.SelectedDate = dr.SiyouKaishi;
                        RadDatePicker4.SelectedDate = dr.SiyouOwari;
                    }
                    Kakeri.Text = dr.Kakeritsu;
                    zeiku.Text = dr.ZeiKubun;
                    Tanka.Text = dr.JutyuTanka.ToString("0,0");
                    Uriage.Text = dr.JutyuGokei.ToString("0,0");
                    Label1.Text = dr.TanTouName;
                    RadComboBox4.Text = dr.Busyo;
                    //ヘッダーの方

                    ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
                    KensakuPara(p);

                    DataSet1.M_Tokuisaki2DataTable dtT = ClassKensaku.GetTokuisakiSyousai(p, Global.GetConnection());
                    TokuisakiMei.Value = dtT[0].TokuisakiName1;
                    TokuisakiCode.Value = dtT[0].TokuisakiCode.ToString();
                    Label3.Text = dtT[0].Kakeritsu.ToString();
                    Shimebi.Text = dtT[0].Shimebi;

                    //if (!drr.IsKariFLGNull())
                    //{
                    //    if (drr.KariFLG.Trim() == "True")
                    //    {
                    //        CheckBox1.Checked = true;
                    //    }
                    //    else
                    //    {
                    //        CheckBox1.Checked = false;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Err.Text = "データ反映時にエラーが発生しました。" + ex.Message;
            }
        }

        protected void CtrlSyousai_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void RadComboCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CategoryCode.Value = RadComboCategory.SelectedValue;
            string a = CategoryCode.Value;
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                HtmlInputHidden cate = CtlMitsuSyosai.FindControl("HidCategoryCode") as HtmlInputHidden;
                cate.Value = a;
                CtlMitsuSyosai.Test4(a);
            }
            RadComboBox1.Focus();
        }

        protected void RadComboCategory_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetCategory(sender, e);
        }

        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string d = RadComboBox1.Text;
                RadComboBox3.Text = d;

                ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
                KensakuPara(p);

                DataSet1.M_Tokuisaki2DataTable dt = ClassKensaku.GetTokuisakiSyousai(p, Global.GetConnection());
                TokuisakiMei.Value = dt[0].TokuisakiName1;
                TokuisakiCode.Value = dt[0].TokuisakiCode.ToString();
                for (int j = 0; j < dt.Count; j++)
                {
                    DataSet1.M_Tokuisaki2Row dr = dt.Rows[j] as DataSet1.M_Tokuisaki2Row;
                    CtrlMitsuSyousai Ctl = CtrlSyousai.Items[j].FindControl("Syosai") as CtrlMitsuSyousai;
                    Label Kakeri = (Label)Ctl.FindControl("Kakeri");
                    Label zeiku = (Label)Ctl.FindControl("zeiku");

                    string code = dr.TantoStaffCode;
                    ListSet.SetBumon2(code, RadComboBox4);


                    if (!dr.IsTantoStaffCodeNull())
                    {
                        Label1.Text = dr.TantoStaffCode;
                    }

                    if (!dr.IsZeikubunNull())
                    {
                        RadZeiKubun.SelectedItem.Text = dr.Zeikubun;
                        zeiku.Text = dr.Zeikubun;
                    }

                    if (!dr.IsKakeritsuNull())
                    {
                        Label3.Text = dr.Kakeritsu.ToString();
                        Kakeri.Text = dr.Kakeritsu.ToString();
                    }

                    if (!dr.IsShimebiNull())
                    {
                        Shimebi.Text = dr.Shimebi;
                    }
                }

            }
            catch
            {
                Err.Text = "得意先を選択してください。";
            }
            FacilityRad.Focus();
        }

        private void KensakuPara(ClassKensaku.KensakuParam p)
        {
            if (RadComboBox1.SelectedValue.Trim() != "")
            {
                p.tokuiTObumonTOtanto = TokuisakiCode.Value;
            }
        }

        protected void RadComboBox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTokuisaki(sender, e);
            }
        }

        protected void RadComboBox3_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
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

        protected void FacilityRad_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetFacility(sender, e);
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

        private void Fukusu()
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

        protected void RadZeiKubun_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                Label zeiku = (Label)CtlMitsuSyosai.FindControl("zeiku");

                zeiku.Text = RadZeiKubun.Text;
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

        protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            Fukusu();
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

        //確定ボタンを押して返却データを作成
        protected void BtnKakutei_Click(object sender, EventArgs e)
        {
            Register();
            LblEnd.Text = "売上データを確定しました";
        }

        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void Register()
        {
            int cate = 0;
            cate = int.Parse(RadComboCategory.SelectedValue);
            switch (cate)
            {
                case 101:
                case 102:
                case 103:
                case 109:
                    break;
                default:
                    {
                        try
                        {
                            string catecode = "";
                            string tokucode = "";
                            //売上ヘッダ
                            DataUriage.T_UriageHeaderDataTable dth = new DataUriage.T_UriageHeaderDataTable();
                            DataUriage.T_UriageHeaderRow drh = dth.NewT_UriageHeaderRow();
                            //返却ヘッダ
                            DataReturn.T_ReturnHeaderDataTable dtRh = new DataReturn.T_ReturnHeaderDataTable();
                            DataReturn.T_ReturnHeaderRow drRh = dtRh.NewT_ReturnHeaderRow();
                            //売上・返却ヘッダ
                            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
                            DataReturn.T_ReturnDataTable dtR = new DataReturn.T_ReturnDataTable();

                            drh.UriageNo = int.Parse(Label94.Text);
                            ClassUriage.UpdateUriageDate(Label94.Text.Trim(), Global.GetConnection());
                            drh.GokeiKingaku = int.Parse(TextBox7.Text.Replace(",", ""));
                            drh.ShiireKingaku = int.Parse(TextBox8.Text.Replace(",", ""));
                            drh.SyohiZeiGokei = int.Parse(TextBox5.Text.Replace(",", ""));
                            if (TextBox12.Text != "")
                            {
                                drh.SoukeiGaku = int.Parse(TextBox12.Text.Replace(",", ""));
                            }
                            drh.ArariGokeigaku = int.Parse(TextBox6.Text.Replace(",", ""));
                            drh.SouSuryou = int.Parse(TextBox10.Text.Trim());
                            drh.CateGory = int.Parse(RadComboCategory.SelectedValue);
                            catecode = RadComboCategory.SelectedValue;
                            drh.CategoryName = RadComboCategory.Text;
                            drh.TokuisakiCode = RadComboBox1.SelectedValue;
                            tokucode = RadComboBox1.SelectedValue;
                            drh.TokuisakiName = RadComboBox1.Text;
                            drh.SeikyusakiName = RadComboBox3.Text;
                            drh.FacilityCode = FacilityRad.SelectedValue;
                            drh.FacilityName = FacilityRad.Text;
                            drh.TantoName = Label1.Text;
                            drh.Bumon = RadComboBox4.Text;
                            drh.Relay = "売上";
                            drh.Zeikubun = RadZeiKubun.Text;
                            int no = 0;
                            DataReturn.T_ReturnHeaderRow dr = ClassReturn.GetMaxNo(Global.GetConnection());
                            if (dr == null)
                            {
                                drRh.ReturnHeaderNo = 1;
                                no = 1;
                            }
                            else
                            {
                                drRh.ReturnHeaderNo = dr.ReturnHeaderNo + 1;
                                no = dr.ReturnHeaderNo + 1;
                            }
                            drRh.GokeiKingaku = int.Parse(TextBox7.Text.Replace(",", ""));
                            drRh.ShiireKingaku = int.Parse(TextBox8.Text.Replace(",", ""));
                            drRh.SyohiZeiGokei = int.Parse(TextBox5.Text.Replace(",", ""));
                            if (TextBox12.Text != "")
                            {
                                drRh.SoukeiGaku = int.Parse(TextBox12.Text.Replace(",", ""));
                            }
                            drRh.ArariGokeigaku = int.Parse(TextBox6.Text.Replace(",", ""));
                            drRh.SouSuryou = int.Parse(TextBox10.Text.Trim());
                            int su = int.Parse(TextBox10.Text.Trim());
                            drRh.CateGory = int.Parse(RadComboCategory.SelectedValue);
                            drRh.CategoryName = RadComboCategory.Text;
                            drRh.TokuisakiCode = RadComboBox1.SelectedValue;
                            drRh.TokuisakiName = RadComboBox1.Text;
                            drRh.SeikyusakiName = RadComboBox3.Text;
                            drRh.FacilityCode = FacilityRad.SelectedValue;
                            drRh.FacilityName = FacilityRad.Text;
                            drRh.TantoName = Label1.Text;
                            drRh.Bumon = RadComboBox4.Text;
                            drRh.Relay = "返却";
                            drRh.ZeiKubun = RadZeiKubun.Text;
                            drRh.ReturnFlg = false;
                            drRh.SiyouOwari = DateTime.Parse(RadDatePicker4.SelectedDate.ToString());
                            drRh.CareateDate = DateTime.Parse(RadDatePicker2.SelectedDate.ToString());
                            dtRh.AddT_ReturnHeaderRow(drRh);
                            DataReturn.T_ReturnHeaderRow drRH = ClassReturn.SerchHeader(catecode, tokucode, Global.GetConnection());
                            if (drRH == null)
                            {
                                ClassReturn.InsertHeader(dtRh, Global.GetConnection());
                            }
                            else
                            {
                                ClassReturn.UpdateHeader(tokucode, catecode, su, Global.GetConnection());
                            }


                            for (int i = 0; CtrlSyousai.Items.Count > i; i++)
                            {
                                DataReturn.T_ReturnRow drr = dtR.NewT_ReturnRow();
                                UriageMeisaiRan Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as UriageMeisaiRan;
                                Label LblProduct = (Label)Ctl.FindControl("LblProduct");
                                RadComboBox SerchProduct = (RadComboBox)Ctl.FindControl("SerchProduct");
                                Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                                Label Baitai = (Label)Ctl.FindControl("Baitai");
                                TextBox Suryo = (TextBox)Ctl.FindControl("Suryo");
                                TextBox HyoujunTanka = (TextBox)Ctl.FindControl("HyoujyunTanka");
                                TextBox Kingaku = (TextBox)Ctl.FindControl("Kingaku");
                                RadComboBox ShiyouShisetsu = (RadComboBox)Ctl.FindControl("ShiyouShisetsu");
                                TextBox KariFaci = (TextBox)Ctl.FindControl("KariFaci");
                                Label Facility = (Label)Ctl.FindControl("Facility");
                                RadComboBox Zasu = (RadComboBox)Ctl.FindControl("Zasu");
                                RadDatePicker StartDate = (RadDatePicker)Ctl.FindControl("StartDate");
                                RadDatePicker EndDate = (RadDatePicker)Ctl.FindControl("EndDate");
                                Label Kakeri = (Label)Ctl.FindControl("Kakeri");
                                Label zeiku = (Label)Ctl.FindControl("zeiku");
                                TextBox Tanka = (TextBox)Ctl.FindControl("Tanka");
                                TextBox Uriage = (TextBox)Ctl.FindControl("Uriage");

                                DataUriage.T_UriageDataTable dtU = ClassUriage.GetUriage(no.ToString(), Global.GetConnection());
                                int rowno = 0;
                                if (dtU != null)
                                {
                                    for (int u = 0; u < dtU.Count; u++)
                                    {
                                        rowno++;
                                    }
                                }
                                drr.MekarHinban = LblProduct.Text;
                                drr.SyouhinMei = SerchProduct.Text;

                                //string str = SerchProduct.SelectedValue;
                                //string[] arr = str.Split('/');
                                //syouhinCode += arr[0];
                                //media += arr[1];
                                //hanni += arr[2];

                                //drr.SyouhinCode = syouhinCode;
                                drr.KeitaiMei = Baitai.Text;
                                drr.Range = LblHanni.Text;
                                drr.ReturnHeaderNo = no.ToString();
                                drr.ReturnNo = "HE" + (i + rowno).ToString();
                                drr.RowNo = i + rowno;
                                drr.TokuisakiCode = RadComboBox1.SelectedValue;
                                drr.TokuisakiMei = RadComboBox1.Text;
                                drr.SeikyusakiMei = RadComboBox3.Text;
                                drr.CateGory = int.Parse(RadComboCategory.SelectedValue);
                                drr.CategoryName = RadComboCategory.Text;
                                drr.SisetuCode = int.Parse(FacilityRad.SelectedValue);
                                drr.SisetuMei = FacilityRad.Text;
                                drr.TanTouName = Label1.Text;
                                drr.Busyo = RadComboBox4.Text;
                                drr.ZeiKubun = RadZeiKubun.Text;
                                if (Zasu.Text != "")
                                {
                                    drr.Zasu = Zasu.Text;
                                }
                                drr.UriageFlg = true;
                                drr.JutyuSuryou = int.Parse(Suryo.Text);
                                drr.Kakeritsu = Kakeri.Text;
                                if (StartDate.SelectedDate != null)
                                {
                                    drr.StartDate = DateTime.Parse(StartDate.SelectedDate.ToString());
                                }
                                drr.EndDate = DateTime.Parse(EndDate.SelectedDate.ToString());
                                drr.ZeiKubun = zeiku.Text;
                                drr.ReturnFlg = false;
                                dtR.AddT_ReturnRow(drr);
                            }
                            ClassReturn.InsertMeisai(dtR, Global.GetConnection());
                        }
                        catch (Exception ex)
                        {
                            Err.Text = ex.Message;
                        }
                        break;
                    }
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}