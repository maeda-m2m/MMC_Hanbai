using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;
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
        public static string strSyokaiDate;
        public static int focusRow;
        public static string mail_to = "maeda@m2m-asp.com";
        public static string mail_title = "エラーメール | 見積入力";


        protected void Page_Load(object sender, EventArgs e)
        {

            LblTantoStaffCode.Text = HidTantoStaffCode.Value;
            //LblTantoStaffNo2.Text = HidTantoStaffCode2.Value;
            Label3.Text = TbxKakeritsu.Text;
            strKakeritsu = TbxKakeritsu.Text;
            Shimebi.Text = RcbShimebi.Text;
            strZeikubun = RcbTax.Text;
            Err.Text = "";
            End.Text = "";
            if (!IsPostBack)
            {
                RadDatePicker1.SelectedDate = DateTime.Now;
                RadDatePicker2.SelectedDate = DateTime.Now;
                RadDatePicker3.SelectedDate = DateTime.Now;

                if (SessionManager.User.UserID != "72" && SessionManager.User.UserID != "83" && SessionManager.User.UserID != "33495081")
                {
                    BtnToMaster.Style["display"] = "none";
                    BtnToMasterS.Style["display"] = "none";
                    BtnNouhinMasta.Style["display"] = "none";
                    BtnFacilityMaster.Style["display"] = "none";
                }
                else
                {
                    BtnToMaster.Style["display"] = "";
                    BtnToMasterS.Style["display"] = "";
                    BtnNouhinMasta.Style["display"] = "";
                    BtnFacilityMaster.Style["display"] = "";
                }

                TBTokuisaki.Style["display"] = "none";
                TBSeikyusaki.Style["display"] = "none";
                NouhinsakiPanel.Style["display"] = "none";
                TBFacilityDetail.Style["display"] = "none";
                Button1.OnClientClick = string.Format("Meisai2('{0}'); return false;", TBTokuisaki.ClientID);
                Button2.OnClientClick = string.Format("Meisai2('{0}'); return false;", TBSeikyusaki.ClientID);
                Button6.OnClientClick = string.Format("Close2('{0}'); return false;", TBTokuisaki.ClientID);
                Button3.OnClientClick = string.Format("MeisaiNouhin('{0}'); return false;", NouhinsakiPanel.ClientID);
                BtnFacilityDetail.OnClientClick = string.Format("MeisaiFacility('{0}'); return false;", BtnFacilityDetail.ClientID);
                //BtnDetailClose.OnClientClick = string.Format("Close3('{0}'); return false;", BtnDetailClose.ClientID);
                mInput.Visible = true;
                CtrlSyousai.Visible = true;
                SubMenu.Visible = true;
                SubMenu2.Style["display"] = "none";
                ListSet.SetCategory(RadComboCategory);
                ListSet.SetCity(RcbNouhinsakiCity);
                ListSet.SetCity(RcbCity);
                if (SessionManager.HACCYU_NO != "")
                {
                    Create2();
                }
                else
                {
                    objFacility = null;
                    Create();
                }
                //CpandRightEnd();
            }
        }

        private void CpandRightEnd()
        {
            SessionManager.strCp = "";
            SessionManager.strCpOver = "";
            SessionManager.strPermission = "";
            DataSet1.M_Kakaku_2DataTable dtkakaku = Class1.GetSyouhin(Global.GetConnection());
            for (int k = 0; k < dtkakaku.Count; k++)
            {
                if (!dtkakaku[k].IsCpKaisiNull())
                {
                    if (!dtkakaku[k].IsCpOwariNull())
                    {
                        if (!string.IsNullOrEmpty(dtkakaku[k].CpKaisi) && !string.IsNullOrEmpty(dtkakaku[k].CpOwari))
                        {
                            if (RadDatePicker1.SelectedDate > DateTime.Parse(dtkakaku[k].CpKaisi) && RadDatePicker1.SelectedDate < DateTime.Parse(dtkakaku[k].CpOwari))
                            {
                                if (!string.IsNullOrEmpty(SessionManager.strCp))
                                {
                                    if (!SessionManager.strCp.Contains(dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Makernumber + "," + dtkakaku[k].Hanni))
                                    {
                                        SessionManager.strCp += dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Makernumber + "," + dtkakaku[k].Hanni + "/";
                                    }
                                }
                                else
                                {
                                    SessionManager.strCp = dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Makernumber + "," + dtkakaku[k].Hanni + "/";
                                }
                            }
                        }
                    }
                }
                if (RadDatePicker1.SelectedDate > DateTime.Parse(dtkakaku[k].RightEnd))
                {
                    if (!string.IsNullOrEmpty(SessionManager.strCpOver))
                    {
                        if (!SessionManager.strCpOver.Contains(dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Hanni))
                        {
                            SessionManager.strCpOver += "/" + dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Hanni;
                        }
                    }
                    else
                    {
                        SessionManager.strCpOver = dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Hanni;
                    }
                }
                if (!dtkakaku[k].IsPermissionStartNull())
                {
                    if (!string.IsNullOrEmpty(dtkakaku[k].PermissionStart))
                    {
                        if (RadDatePicker1.SelectedDate > DateTime.Parse(dtkakaku[k].PermissionStart))
                        {
                            if (!string.IsNullOrEmpty(SessionManager.strPermission))
                            {
                                if (!SessionManager.strPermission.Contains(dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Hanni))
                                {
                                    SessionManager.strPermission += "/" + dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Hanni;
                                }
                            }
                            else
                            {
                                SessionManager.strPermission = dtkakaku[k].SyouhinMei + "," + dtkakaku[k].CategoryCode + "," + dtkakaku[k].Media + "," + dtkakaku[k].ShiireCode + "," + dtkakaku[k].Hanni;
                            }
                        }
                    }
                }
            }

            //DataSet1.M_Kakaku_2DataTable dtCp = Class1.GetCp(RadDatePicker1.SelectedDate, Global.GetConnection());
            //for (int i = 0; i < dtCp.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(SessionManager.strCp))
            //    {
            //        if (!SessionManager.strCp.Contains(dtCp[i].SyouhinMei + "," + dtCp[i].CategoryCode + "," + dtCp[i].Media + "," + dtCp[i].ShiireCode + "," + dtCp[i].Makernumber + "," + dtCp[i].Hanni))
            //        {
            //            SessionManager.strCp += dtCp[i].SyouhinMei + "," + dtCp[i].CategoryCode + "," + dtCp[i].Media + "," + dtCp[i].ShiireCode + "," + dtCp[i].Makernumber + "," + dtCp[i].Hanni + "/";
            //        }
            //    }
            //    else
            //    {
            //        SessionManager.strCp = dtCp[i].SyouhinMei + "," + dtCp[i].CategoryCode + "," + dtCp[i].Media + "," + dtCp[i].ShiireCode + "," + dtCp[i].Makernumber + "," + dtCp[i].Hanni + "/";
            //    }
            //}
            //CtrlMitsuSyousai.strCp = SessionManager.strCp;
            //DataSet1.M_Kakaku_2DataTable dtCpOver = Class1.GetCpOver(RadDatePicker1.SelectedDate, Global.GetConnection());
            //for (int c = 0; c < dtCpOver.Count; c++)
            //{
            //    if (!string.IsNullOrEmpty(SessionManager.strCpOver))
            //    {
            //        if (!SessionManager.strCpOver.Contains(dtCpOver[c].SyouhinMei + "," + dtCpOver[c].CategoryCode + "," + dtCpOver[c].Media + "," + dtCpOver[c].ShiireCode + "," + dtCpOver[c].Hanni))
            //        {
            //            SessionManager.strCpOver += "/" + dtCpOver[c].SyouhinMei + "," + dtCpOver[c].CategoryCode + "," + dtCpOver[c].Media + "," + dtCpOver[c].ShiireCode + "," + dtCpOver[c].Hanni;
            //        }
            //    }
            //    else
            //    {
            //        SessionManager.strCpOver = dtCpOver[c].SyouhinMei + "," + dtCpOver[c].CategoryCode + "," + dtCpOver[c].Media + "," + dtCpOver[c].ShiireCode + "," + dtCpOver[c].Hanni;
            //    }
            //}
            //CtrlMitsuSyousai.strCpOver = SessionManager.strCpOver;
            //DataSet1.M_Kakaku_2DataTable dtPermi = Class1.GetPermission(RadDatePicker1.SelectedDate, Global.GetConnection());
            //for (int p = 0; p < dtPermi.Count; p++)
            //{
            //    if (!string.IsNullOrEmpty(SessionManager.strPermission))
            //    {
            //        if (!SessionManager.strPermission.Contains(dtPermi[p].SyouhinMei + "," + dtPermi[p].CategoryCode + "," + dtPermi[p].Media + "," + dtPermi[p].ShiireCode + "," + dtPermi[p].Hanni))
            //        {
            //            SessionManager.strPermission += "/" + dtPermi[p].SyouhinMei + "," + dtPermi[p].CategoryCode + "," + dtPermi[p].Media + "," + dtPermi[p].ShiireCode + "," + dtPermi[p].Hanni;
            //        }
            //    }
            //    else
            //    {
            //        SessionManager.strPermission = dtPermi[p].SyouhinMei + "," + dtPermi[p].CategoryCode + "," + dtPermi[p].Media + "," + dtPermi[p].ShiireCode + "," + dtPermi[p].Hanni;
            //    }

            // }
        }

        private void Create2()
        {
            string MNo = SessionManager.HACCYU_NO;
            Label94.Text = MNo;
            DataMitumori.T_MitumoriDataTable dt = ClassMitumori.GETMitsumori(MNo, Global.GetConnection());
            //DataMitumori.T_MitumoriBackupDataTable dt = ClassMitumori.GETMitsumori2(MNo, Global.GetConnection());
            DataMitumori.T_MitumoriHeaderDataTable dd = ClassMitumori.GETMitsumorihead(MNo, Global.GetConnection());
            //DataMitumori.T_MitumoriHeaderBackupDataTable dd = ClassMitumori.GETMitsumorihead2(MNo, Global.GetConnection());
            DataMitumori.T_MitumoriHeaderRow dr = dd[0];
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
                    objFacility = drF.ItemArray;
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
            }
            if (!dr.IsCategoryNameNull())
            {
                RadComboCategory.SelectedValue = dr.CateGory.ToString();
                strCategoryCode = dr.CateGory.ToString();
                strCategoryName = dr.CategoryName;
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
                    //if (!dr.IsSekyuTokuisakiKakeritsuNull())
                    //{
                    //    TbxKakeritsu2.Text = dr.SekyuTokuisakiKakeritsu;
                    //}
                    if (!dr.IsSekyuTokuisakiShimebiNull())
                    {
                        RadShimebi2.Text = dr.SekyuTokuisakiShimebi;
                    }
                    //if (!dr.IsSekyuTokuisakiStaffNull())
                    //{
                    //    LblTantoStaffNo2.Text = dr.SekyuTokuisakiStaff;
                    //}
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
                RadComboBox2.Text = dr.TyokusosakiName;

            }
            if (!dr.IsCreateDateNull())
            {
                RadDatePicker2.SelectedDate = dr.CreateDate;
            }
            if (!string.IsNullOrEmpty(dr.FacilityCode))
            {
                //if (!string.IsNullOrEmpty(dr.FacilityRowCode))
                //{
                //    string[] facCode = (dr.FacilityCode + "/" + dr.FacilityRowCode).Split('/');
                //    DataMaster.M_Facility_NewRow df = ClassMaster.GetFacilityRow(facCode, Global.GetConnection());
                //    objFacility = df.ItemArray;
                //}
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
            //Button13.Style["display"] = "none";
            head.Style["display"] = "none";
            SubMenu.Style["display"] = "none";
            SubMenu2.Style["display"] = "";
            RadComboBox5.Style["display"] = "";
            RcbTax.Style["display"] = "";
            RcbShimebi.Style["display"] = "";
            NouhinsakiPanel.Style["display"] = "";
            DivDataGrid.Style["display"] = "none";

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
                ListSet.GetFacility(sender, e);
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
                //RcbStaffName.Text = dt[0].UserName;
                Label1.Text = dt[0].UserName;
                for (int i = 0; i < dt.Count; i++)
                {
                    RadComboBox4.Items.Add(new RadComboBoxItem(dt[0].Busyo));
                }
                RadComboBox2.Focus();
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(mail_to, mail_title, body);
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
            string strCategoryName = RadComboCategory.SelectedItem.Text;
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
                        RadDatePicker3.Visible = DropDownList9.Visible = CheckBox4.Visible = RadDatePicker4.Visible = BtnTool5.Visible = false;
                        break;
                    default:
                        RadDatePicker3.Visible = DropDownList9.Visible = CheckBox4.Visible = RadDatePicker4.Visible = BtnTool5.Visible = true;
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
                    //DataMitumori.T_MitumoriBackupRow dl = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_MitumoriBackupRow;
                    //DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
                    //DataMitumori.T_MitumoriRow df = dt.NewT_MitumoriRow();
                    //if (dl != null)
                    //{
                    //    df.ItemArray = dl.ItemArray;
                    //}
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
                    Label LblCateCode = Ctl.FindControl("LblCateCode") as Label;
                    Label LblCategoryName = Ctl.FindControl("LblCategoryName") as Label;
                    RadDatePicker RdpPermissionstart = Ctl.FindControl("RdpPermissionstart") as RadDatePicker;
                    RadDatePicker RdpRightEnd = Ctl.FindControl("RdpRightEnd") as RadDatePicker;
                    RadDatePicker RdpCpStart = Ctl.FindControl("RdpCpStart") as RadDatePicker;
                    RadDatePicker RdpCpEnd = Ctl.FindControl("RdpCpEnd") as RadDatePicker;

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
                        if (RdpPermissionstart.SelectedDate != null)
                        {
                            if (!string.IsNullOrEmpty(RdpPermissionstart.SelectedDate.Value.ToShortDateString()))
                            {
                                if (!string.IsNullOrEmpty(RdpRightEnd.SelectedDate.Value.ToShortDateString()))
                                {
                                    if (RadDatePicker1.SelectedDate >= RdpCpStart.SelectedDate && RadDatePicker1.SelectedDate <= RdpCpEnd.SelectedDate)
                                    {
                                        SerchProduct.ForeColor = System.Drawing.Color.Orange;
                                        HyoujyunTanka.Text = int.Parse(df.CpKakaku).ToString("0,0");
                                        ShiireTanka.Text = int.Parse(df.CpShiire).ToString("0,0");
                                    }
                                }
                            }
                        }
                        if (RdpRightEnd.SelectedDate != null)
                        {
                            if (RadDatePicker1.SelectedDate > RdpRightEnd.SelectedDate)
                            {
                                SerchProduct.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        if (RdpPermissionstart.SelectedDate != null)
                        {
                            if (!string.IsNullOrEmpty(RdpPermissionstart.SelectedDate.Value.ToShortDateString()))
                            {
                                if (RadDatePicker1.SelectedDate < RdpPermissionstart.SelectedDate)
                                {
                                    SerchProduct.ForeColor = System.Drawing.Color.Blue;
                                }
                            }
                        }
                        if (SerchProduct.Text.Equals("値引き"))
                        {
                            HyoujyunTanka.ReadOnly = true;
                            Kingaku.ReadOnly = true;
                            ShiireKingaku.ReadOnly = true;
                            ShiireTanka.ReadOnly = true;

                            HyoujyunTanka.Style["background-color"] = "lightgray";
                            Kingaku.Style["background-color"] = "lightgray";
                            ShiireKingaku.Style["background-color"] = "lightgray";
                            ShiireTanka.Style["background-color"] = "lightgray";

                            Suryo.Style["display"] = "none";
                        }
                    }
                    else
                    {
                        Ctl.ItemSet(dr);
                        if (!string.IsNullOrEmpty(RadComboCategory.SelectedValue))
                        {
                            LblCateCode.Text = RadComboCategory.SelectedValue;
                            LblCategoryName.Text = RadComboCategory.SelectedItem.Text;
                        }
                        if (RdpPermissionstart.SelectedDate != null)
                        {
                            if (!string.IsNullOrEmpty(RdpPermissionstart.SelectedDate.Value.ToShortDateString()))
                            {
                                if (!string.IsNullOrEmpty(RdpRightEnd.SelectedDate.Value.ToShortDateString()))
                                {
                                    if (RadDatePicker1.SelectedDate >= RdpCpStart.SelectedDate && RadDatePicker1.SelectedDate <= RdpCpEnd.SelectedDate)
                                    {
                                        SerchProduct.ForeColor = System.Drawing.Color.Orange;
                                        HyoujyunTanka.Text = int.Parse(dr.CpKakaku).ToString("0,0");
                                        ShiireTanka.Text = int.Parse(dr.CpShiire).ToString("0,0");
                                    }
                                }
                            }
                        }
                        if (RdpRightEnd.SelectedDate != null)
                        {
                            if (RadDatePicker1.SelectedDate > RdpRightEnd.SelectedDate)
                            {
                                SerchProduct.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        if (RdpPermissionstart.SelectedDate != null)
                        {
                            if (!string.IsNullOrEmpty(RdpPermissionstart.SelectedDate.Value.ToShortDateString()))
                            {
                                if (RadDatePicker1.SelectedDate < RdpPermissionstart.SelectedDate)
                                {
                                    SerchProduct.ForeColor = System.Drawing.Color.Blue;
                                }
                            }
                        }
                        string pn = CtrlMitsuSyousai.ProductName;
                        if (focusRow + 2 == no)
                        {
                            SerchProduct.Focus();
                            focusRow = 0;
                        }

                        if (SerchProduct.Text.Equals("値引き"))
                        {
                            HyoujyunTanka.ReadOnly = true;
                            Kingaku.ReadOnly = true;
                            ShiireKingaku.ReadOnly = true;
                            ShiireTanka.ReadOnly = true;

                            HyoujyunTanka.Style["background-color"] = "lightgray";
                            Kingaku.Style["background-color"] = "lightgray";
                            ShiireKingaku.Style["background-color"] = "lightgray";
                            ShiireTanka.Style["background-color"] = "lightgray";

                            Suryo.Style["display"] = "none";
                        }

                    }//見積データがないとき(明細追加など)

                    if (CheckBox1.Checked == true)
                    {
                        KariTokui.Visible = true;
                        kariSekyu.Visible = true;
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
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(mail_to, mail_title, body);
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
            string strErr = "";
            string no = "";

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
                    else
                    {
                        strErr += "得意先コードが入力されていません" + "\r\n";
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
                    dl.FukusuFacility = "True";
                }

                if (RadDatePicker2.SelectedDate != null)
                {
                    dl.CreateDate = RadDatePicker2.SelectedDate.Value;
                }
                dl.CategoryName = RadComboCategory.Text;
                dl.CateGory = int.Parse(CategoryCode.Value);
                if (TextBox10.Text != "")
                {
                    dl.SouSuryou = int.Parse(TextBox10.Text);
                }
                if (TextBox2.Text != "")
                {
                    dl.Bikou = TextBox2.Text;
                }

                if (!TextBox7.Text.Equals("OPEN"))
                {
                    if (TextBox7.Text != "")
                    {
                        string r1 = TextBox7.Text.Replace(",", "");
                        dl.GokeiKingaku = int.Parse(r1);
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
                    if (TextBox6.Text != "")
                    {
                        string r4 = TextBox6.Text.Replace(",", "");
                        dl.ArariGokeigaku = int.Parse(r4);
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
                }
                else
                {
                    dl.GokeiKingaku = 0;
                    dl.ShiireKingaku = 0;
                    dl.SyohiZeiGokei = 0;
                    dl.ArariGokeigaku = 0;
                    dl.SoukeiGaku = 0;
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
                else
                {
                    strErr += "施設情報を登録して下さい" + "\r\n";
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
                    dl.NouhinSisetsuCode = TbxNouhinSisetsu.Text + "/" + TbxCode.Text;
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
                //if (!string.IsNullOrEmpty(TbxKakeritsu2.Text))
                //{
                //    dl.SekyuTokuisakiKakeritsu = TbxKakeritsu2.Text;
                //}
                if (!string.IsNullOrEmpty(RadShimebi2.Text))
                {
                    dl.SekyuTokuisakiShimebi = RadShimebi2.Text;
                }
                //if (!string.IsNullOrEmpty(LblTantoStaffNo2.Text))
                //{
                //    dl.SekyuTokuisakiStaff = LblTantoStaffNo2.Text;
                //}
                //施設情報

                if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
                {
                    dl.FacilityCode = TbxFacilityCode.Text;
                }
                if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
                {
                    dl.FacilityRowCode = TbxFacilityRowCode.Text;
                }
                if (!string.IsNullOrEmpty(RcbFacility.Text))
                {
                    dl.FacilityName = RcbFacility.Text;
                }
                if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
                {
                    dl.FacilityName2 = TbxFacilityName2.Text;
                }
                if (!string.IsNullOrEmpty(TbxFaci.Text))
                {
                    dl.FacilityAbbreviation = TbxFaci.Text;
                }
                if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
                {
                    dl.FacilityResponsible = TbxFacilityResponsible.Text;
                }
                if (!string.IsNullOrEmpty(TbxYubin.Text))
                {
                    dl.FacilityPostNo = TbxYubin.Text;
                }
                if (!string.IsNullOrEmpty(TbxFaciAdress1.Text))
                {
                    dl.FacilityAddress1 = TbxFaciAdress1.Text;
                }
                if (!string.IsNullOrEmpty(TbxFaciAdress2.Text))
                {
                    dl.FacilityAddress2 = TbxFaciAdress2.Text;
                }
                if (!string.IsNullOrEmpty(RcbCity.Text))
                {
                    dl.FacilityCityCode = RcbCity.SelectedValue;
                }
                if (!string.IsNullOrEmpty(TbxTel.Text))
                {
                    dl.FacilityTell = TbxTel.Text;
                }
                if (!string.IsNullOrEmpty(TbxKeisyo.Text))
                {
                    dl.FacilityTitles = TbxKeisyo.Text;
                }
                dl.Relay = "受注";

                if (strErr.Equals(""))
                {
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
                }
                else
                {
                    Err.Text = strErr;
                    return;
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
                //ヘッダー＆明細共にデータ削除
                ClassMitumori.DelMitumoriHeader(no, Global.GetConnection());
                ClassMitumori.DelMitumori(no, Global.GetConnection());
                string body = "見積登録処理" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(mail_to, mail_title, body);
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
            //Button13.Visible = true;
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
            if (!string.IsNullOrEmpty(FacilityRad.Text))
            {
                if (CheckBox5.Checked)
                {
                    DataMaster.M_Facility_NewDataTable dt = new DataMaster.M_Facility_NewDataTable();
                    DataMaster.M_Facility_NewRow dr = dt.NewM_Facility_NewRow();
                    if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
                    {
                        dr.FacilityNo = int.Parse(TbxFacilityCode.Text);
                    }
                    if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
                    {
                        dr.Code = TbxFacilityRowCode.Text;
                    }
                    if (!string.IsNullOrEmpty(RcbCity.SelectedValue))
                    {
                        dr.CityCode = int.Parse(RcbCity.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(RcbFacility.Text))
                    {
                        dr.FacilityName1 = RcbFacility.Text;
                    }
                    if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
                    {
                        dr.FacilityName2 = TbxFacilityName2.Text;
                    }
                    if (!string.IsNullOrEmpty(TbxFaci.Text))
                    {
                        dr.Abbreviation = TbxFaci.Text;
                    }
                    if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
                    {
                        dr.FacilityResponsible = TbxFacilityResponsible.Text;
                    }
                    if (!string.IsNullOrEmpty(TbxYubin.Text))
                    {
                        dr.PostNo = TbxYubin.Text;
                    }
                    if (!string.IsNullOrEmpty(TbxFaciAdress1.Text))
                    {
                        dr.Address1 = TbxFaciAdress1.Text;
                    }
                    if (!string.IsNullOrEmpty(TbxFaciAdress2.Text))
                    {
                        dr.Address2 = TbxFaciAdress2.Text;
                    }
                    if (!string.IsNullOrEmpty(RcbCity.SelectedValue))
                    {
                        dr.CityCode = int.Parse(RcbCity.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(TbxTel.Text))
                    {
                        dr.Tell = TbxTel.Text;
                    }
                    objFacility = dr.ItemArray;


                    //DataMaster.M_Facility_NewRow dr = ClassMaster.GetFacilityRow(FacilityRad.SelectedValue.Split('/'), Global.GetConnection());
                    //objFacility = dr.ItemArray;
                    if (dr != null)

                        for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                        {
                            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                            TextBox TbxFacilityCodeMeisai = CtlMitsuSyosai.FindControl("TbxFacilityCode") as TextBox;
                            TextBox TbxFacilityRowCodeMeisai = CtlMitsuSyosai.FindControl("TbxFacilityRowCode") as TextBox;
                            RadComboBox RcbCityMeisai = CtlMitsuSyosai.FindControl("RcbCity") as RadComboBox;
                            TextBox TbxFacilityNameMeisai = CtlMitsuSyosai.FindControl("TbxFacilityName") as TextBox;
                            TextBox TbxFacilityName2Meisai = CtlMitsuSyosai.FindControl("TbxFacilityName2") as TextBox;
                            TextBox TbxFaciMeisai = CtlMitsuSyosai.FindControl("TbxFaci") as TextBox;
                            TextBox TbxFacilityResponsibleMeisai = CtlMitsuSyosai.FindControl("TbxFacilityResponsible") as TextBox;
                            TextBox TbxYubinMeisai = CtlMitsuSyosai.FindControl("TbxYubin") as TextBox;
                            TextBox TbxFaciAdressMeisai = CtlMitsuSyosai.FindControl("TbxFaciAdress") as TextBox;
                            TextBox TbxTelMeisai = CtlMitsuSyosai.FindControl("TbxTel") as TextBox;
                            RadComboBox ShiyouShisetsu = CtlMitsuSyosai.FindControl("ShiyouShisetsu") as RadComboBox;

                            if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
                            {
                                TbxFacilityCodeMeisai.Text = TbxFacilityCode.Text;
                            }
                            if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
                            {
                                TbxFacilityRowCodeMeisai.Text = TbxFacilityRowCode.Text;
                            }
                            if (!string.IsNullOrEmpty(RcbCity.Text))
                            {
                                RcbCityMeisai.SelectedValue = RcbCity.SelectedValue;
                            }
                            if (!string.IsNullOrEmpty(RcbFacility.Text))
                            {
                                TbxFacilityNameMeisai.Text = RcbFacility.Text;
                            }
                            if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
                            {
                                TbxFacilityName2Meisai.Text = TbxFacilityName2.Text;
                            }
                            if (!string.IsNullOrEmpty(TbxFaci.Text))
                            {
                                TbxFaciMeisai.Text = TbxFaci.Text;
                                ShiyouShisetsu.Text = TbxFaci.Text;
                            }
                            else
                            {
                                ClassMail.ErrorMail("maeda@m2m-asp.com", "エラーメール | 見積入力", "複数施設をチェック押下時に施設略称名が記載されていません。");
                            }
                            if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
                            {
                                TbxFacilityResponsibleMeisai.Text = TbxFacilityResponsible.Text;
                            }
                            if (!string.IsNullOrEmpty(TbxYubin.Text))
                            {
                                TbxYubinMeisai.Text = TbxYubin.Text;
                            }
                            if (!string.IsNullOrEmpty(TbxFaciAdress1.Text))
                            {
                                TbxFaciAdressMeisai.Text = TbxFaciAdress1.Text;
                                if (!string.IsNullOrEmpty(TbxFaciAdress2.Text))
                                {
                                    TbxFaciAdressMeisai.Text += TbxFaciAdress2.Text;
                                }
                            }
                            if (!string.IsNullOrEmpty(TbxTel.Text))
                            {
                                TbxTelMeisai.Text = TbxTel.Text;
                            }
                        }
                }
            }
            else
            {
                Err.Text = "施設名が入力されていません";
                CheckBox5.Checked = false;
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
                string body = ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(mail_to, mail_title, body);
            }
        }

        protected void FacilityRad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Fukusu();

            string[] AryValue = FacilityRad.SelectedValue.Split('/');
            TbxFacilityCode.Text = AryValue[0];
            TbxFacilityRowCode.Text = AryValue[1];
            RcbFacility.Text = AryValue[2];
            TbxFacilityName2.Text = AryValue[3];
            TbxFaci.Text = AryValue[4];
            TbxFacilityResponsible.Text = AryValue[5];
            TbxYubin.Text = AryValue[6];
            TbxFaciAdress1.Text = AryValue[7];
            TbxFaciAdress2.Text = AryValue[8];
            TbxTel.Text = AryValue[9];
            if (!RcbFacility.Text.Equals("複数施設"))
            {
                RcbCity.SelectedValue = AryValue[10];
            }

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
            focusRow = e.Item.ItemIndex;
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            try
            {
                if (e.CommandName == "Del")
                {
                    for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                    {
                        DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                        try
                        {
                            CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                            dr = CtlMitsuSyosai.ItemGet(dr);
                            if (focusRow != i)
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
                        string cp = SessionManager.strCp;
                        DataMitumori.T_RowRow drG = CtlMitsuSyosai.ItemGet();
                        drN.ItemArray = drG.ItemArray;
                        dtN.AddT_RowRow(drN);
                        if (focusRow == i)
                        {
                            for (int r = 0; r < int.Parse(TbxAddRow.Text); r++)
                            {
                                //新規の空の明細を追加
                                DataMitumori.T_RowRow drN2 = dtN.NewT_RowRow();
                                drN2.CategoryCode = CtrlMitsuSyousai.strCategoryCode;
                                drN2.CategoryName = CtrlMitsuSyousai.strCategoryName;
                                drN2 = AddNewRow2(drN2);
                                dtN.AddT_RowRow(drN2);
                            }
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

                        if (focusRow == i)
                        {
                            for (int r = 0; r < int.Parse(TbxAddRow.Text); r++)
                            {
                                //新規の空の明細を追加
                                DataMitumori.T_RowRow drN2 = dtN.NewT_RowRow();
                                DataMitumori.T_RowRow drG2 = CtlMitsuSyosai.ItemGet();
                                drN2.ItemArray = drG2.ItemArray;
                                dtN.AddT_RowRow(drN2);
                            }
                        }
                    }
                    CtrlSyousai.DataSource = dtN;
                    CtrlSyousai.DataBind();
                }
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
                string body = "行追加 or 行削除 or 行コピー" + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                ClassMail.ErrorMail(mail_to, mail_title, body);
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
            if (objFacility != null)
            {
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
                if (!string.IsNullOrEmpty(Label94.Text))
                {
                    string jNo = "2" + Label94.Text.Substring(1, 7);
                    DataJutyu.T_JutyuHeaderDataTable dtHJ = new DataJutyu.T_JutyuHeaderDataTable();
                    DataJutyu.T_JutyuHeaderRow drHJ = dtHJ.NewT_JutyuHeaderRow();
                    DataJutyu.T_JutyuDataTable dtJ = new DataJutyu.T_JutyuDataTable();
                    DataMitumori.T_MitumoriDataTable dtM = ClassMitumori.GetMitumoriTable(Label94.Text, Global.GetConnection());
                    DataMitumori.T_MitumoriHeaderDataTable dtHM = ClassMitumori.GetMitumoriHeader(Label94.Text, Global.GetConnection());
                    if (dtHM.Count > 0)
                    {
                        drHJ.ItemArray = dtHM[0].ItemArray;
                        drHJ.JutyuNo = int.Parse(jNo);
                        dtHJ.AddT_JutyuHeaderRow(drHJ);

                        for (int i = 0; i < dtM.Count; i++)
                        {
                            DataJutyu.T_JutyuRow drJ = dtJ.NewT_JutyuRow();
                            drJ.ItemArray = dtM[i].ItemArray;
                            drJ.JutyuNo = jNo;
                            dtJ.AddT_JutyuRow(drJ);
                        }
                        ClassJutyu.UpDateJutyu(jNo, dtJ, dtHJ, Global.GetConnection());
                        ClassMitumori.UpDateMitumorijutyu("Mitumori", Label94.Text, Global.GetConnection());
                        End.Text = "見積No." + Label94.Text + "を、受注No." + jNo + "で受注致しました。";
                    }
                    else
                    {
                        Err.Text = "見積データが存在しておりません。";
                    }
                }
                else
                {
                    Err.Text = "まずは見積登録を行って下さい。";
                    return;
                }
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
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
            //Button13.Style["display"] = "none";
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
                //Button13.Style["display"] = "";
                head.Style["display"] = "";
                DivDataGrid.Style["display"] = "";
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
            DivDataGrid.Style["display"] = "none";
            //Button13.Style["display"] = "none";
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
            //Button13.Visible = true;
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
            //Button13.Visible = true;
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
                //dr.Kakeritsu = TbxKakeritsu2.Text;
                dr.Shimebi = RadShimebi2.Text;
                //dr.TantoStaffCode = LblTantoStaffNo2.Text;
                SessionManager.drTokuisaki = dr;
            }
            Response.Redirect("Master/MsterTokuisaki.aspx");
        }

        protected void RcbFacility_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetFacility(sender, e);
            }
        }

        protected void BtnHeaderFit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RcbFacility.Text))
            {
                if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
                {
                    if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
                    {
                        if (!string.IsNullOrEmpty(TbxFaci.Text))
                        {
                            FacilityRad.Text = TbxFaci.Text;
                        }
                        else
                        {
                            FacilityRad.Text = RcbFacility.Text;
                        }
                        FacilityRad.SelectedValue = TbxFacilityCode.Text + "/" + TbxFacilityRowCode.Text;
                        if (CheckBox5.Checked)
                        {
                            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                            {
                                CtrlMitsuSyousai CtlMitsuSyosai = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlMitsuSyousai;
                                TextBox TbxFacilityCodeMeisai = CtlMitsuSyosai.FindControl("TbxFacilityCode") as TextBox;
                                TextBox TbxFacilityRowCodeMeisai = CtlMitsuSyosai.FindControl("TbxFacilityRowCode") as TextBox;
                                RadComboBox RcbCityMeisai = CtlMitsuSyosai.FindControl("RcbCity") as RadComboBox;
                                TextBox TbxFacilityNameMeisai = CtlMitsuSyosai.FindControl("TbxFacilityName") as TextBox;
                                TextBox TbxFacilityName2Meisai = CtlMitsuSyosai.FindControl("TbxFacilityName2") as TextBox;
                                TextBox TbxFaciMeisai = CtlMitsuSyosai.FindControl("TbxFaci") as TextBox;
                                TextBox TbxFacilityResponsibleMeisai = CtlMitsuSyosai.FindControl("TbxFacilityResponsible") as TextBox;
                                TextBox TbxYubinMeisai = CtlMitsuSyosai.FindControl("TbxYubin") as TextBox;
                                TextBox TbxFaciAdressMeisai = CtlMitsuSyosai.FindControl("TbxFaciAdress") as TextBox;
                                TextBox TbxTelMeisai = CtlMitsuSyosai.FindControl("TbxTel") as TextBox;
                                RadComboBox ShiyouShisetsu = CtlMitsuSyosai.FindControl("ShiyouShisetsu") as RadComboBox;

                                if (!string.IsNullOrEmpty(TbxFacilityCode.Text))
                                {
                                    TbxFacilityCodeMeisai.Text = TbxFacilityCode.Text;
                                }
                                if (!string.IsNullOrEmpty(TbxFacilityRowCode.Text))
                                {
                                    TbxFacilityRowCodeMeisai.Text = TbxFacilityRowCode.Text;
                                }
                                if (!string.IsNullOrEmpty(RcbCity.Text))
                                {
                                    RcbCityMeisai.SelectedValue = RcbCity.SelectedValue;
                                }
                                if (!string.IsNullOrEmpty(RcbFacility.Text))
                                {
                                    TbxFacilityNameMeisai.Text = RcbFacility.Text;
                                }
                                if (!string.IsNullOrEmpty(TbxFacilityName2.Text))
                                {
                                    TbxFacilityName2Meisai.Text = TbxFacilityName2.Text;
                                }
                                if (!string.IsNullOrEmpty(TbxFaci.Text))
                                {
                                    TbxFaciMeisai.Text = TbxFaci.Text;
                                    ShiyouShisetsu.Text = TbxFaci.Text;
                                    FacilityRad.Text = TbxFaci.Text;
                                }
                                if (!string.IsNullOrEmpty(TbxFacilityResponsible.Text))
                                {
                                    TbxFacilityResponsibleMeisai.Text = TbxFacilityResponsible.Text;
                                }
                                if (!string.IsNullOrEmpty(TbxYubin.Text))
                                {
                                    TbxYubinMeisai.Text = TbxYubin.Text;
                                }
                                if (!string.IsNullOrEmpty(TbxFaciAdress1.Text))
                                {
                                    TbxFaciAdressMeisai.Text = TbxFaciAdress1.Text;
                                    if (!string.IsNullOrEmpty(TbxFaciAdress2.Text))
                                    {
                                        TbxFaciAdressMeisai.Text += TbxFaciAdress2.Text;
                                    }
                                }
                                if (!string.IsNullOrEmpty(TbxTel.Text))
                                {
                                    TbxTelMeisai.Text = TbxTel.Text;
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}

