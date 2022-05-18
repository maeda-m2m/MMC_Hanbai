using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Master
{
    public partial class CtlTokuisaki : System.Web.UI.UserControl
    {
        private String VsID
        {
            get
            {
                object obj = this.ViewState["vsID"];
                if (null == obj) return "";
                return Convert.ToString(obj);
            }
            set
            {
                this.ViewState["vsID"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                RadCityCode.Style["display"] = "";
                //担当名DropDwonList
                //ListSet.SetTanto2(sender, RadTanto);
                //市町村
            }
        }

        public void Create(string userId)
        {
            VsID = userId;
            if (userId != "0/W" && userId != "" && userId != null)
            {
                RcbTanto.Style["display"] = "";
                RadCityCode.Style["display"] = "";
                string[] code = userId.Split('/');
                DataSet1.M_Tokuisaki2Row dr = Class1.GetTokuisaki3(code[0], code[1], Global.GetConnection());
                TbxCustomerCode.Text = dr.CustomerCode;
                TbxTokuiCode.Text = dr.TokuisakiCode.ToString();
                if (!dr.IsTokuisakiRyakusyoNull())
                {
                    TbxTokuisakiRyakusyo.Text = dr.TokuisakiRyakusyo;
                }
                if (!dr.IsTokuisakiFuriganaNull())
                {
                    TbxHurigana.Text = dr.TokuisakiFurigana;
                }
                if (!dr.IsTokuisakiAddress1Null())
                {
                    TbxTokuiJusyo1.Text = dr.TokuisakiAddress1;
                }
                TbxTokuiTanto.Text = dr.TantoStaffCode;
                if (!dr.IsTokuisakiKeisyoNull())
                {
                    DrpKeisyo.SelectedItem.Text = dr.TokuisakiKeisyo;
                }
                string tc = dr.TantoStaffCode;
                DataMaster.M_Tanto1DataTable dtT = ClassMaster.GetTanto1(tc, Global.GetConnection());
                if (dtT.Count >= 1)
                {
                    RcbTanto.SelectedValue = dtT[0].UserID.ToString();
                    RcbTanto.Text = dtT[0].UserName;
                }
                ChkRiyo.Checked = dr.RiyoCode;
                TbxTokuiMei1.Text = dr.TokuisakiName1;
                if (!dr.IsTokuisakiTELNull())
                {
                    TbxTokuiTell.Text = dr.TokuisakiTEL;
                }
                if (!dr.IsTokuisakiAddress2Null())
                {
                    TbxTokuiJusyo2.Text = dr.TokuisakiAddress2;
                }
                if (!dr.IsTokuisakiDepartmentNull())
                {
                    TbxTokuiBusyo.Text = dr.TokuisakiDepartment;
                }
                string cc = dr.CityCode.Trim();
                DataDrop.M_CityDataTable dtC = ClassDrop.GetCity2(cc, Global.GetConnection());
                if (dtC.Count >= 1)
                {
                    RadCityCode.Text = dtC[0].CityName.ToString();
                    RadCityCode.SelectedValue = dtC[0].CityCode.ToString();
                }
                if (!dr.IsKakeritsuNull())
                {
                    TbxKakeritu.Text = dr.Kakeritsu.ToString();
                }
                DrpSimebi.SelectedItem.Value = dr.Shimebi.ToString();
                DrpTuti.SelectedItem.Value = dr.ZeiTuti.ToString();
                if (!dr.IsBankNoNull())
                {
                    TbxKoza.Text = dr.BankNo;
                }
                if (!dr.IsTokuisakiName2Null())
                {
                    TbxTokuiMei2.Text = dr.TokuisakiName2;
                }
                if (!dr.IsTokuisakiFAXNull())
                {
                    TbxTokuiFax.Text = dr.TokuisakiFAX;
                }
                if (!dr.IsTokuisakiPostNoNull())
                {
                    TbxTokuiYubin.Text = dr.TokuisakiPostNo;
                }
            }
            else if (userId == "0/W")
            {
                RcbTanto.Style["display"] = "";
                RadCityCode.Style["display"] = "";

                DataSet1.M_Tokuisaki2Row dr = SessionManager.drTokuisaki;
                TbxCustomerCode.Text = dr.CustomerCode;
                TbxTokuiCode.Text = dr.TokuisakiCode.ToString();
                if (!dr.IsTokuisakiRyakusyoNull())
                {
                    TbxTokuisakiRyakusyo.Text = dr.TokuisakiRyakusyo;
                }
                if (!dr.IsTokuisakiFuriganaNull())
                {
                    TbxHurigana.Text = dr.TokuisakiFurigana;
                }
                if (!dr.IsTokuisakiAddress1Null())
                {
                    TbxTokuiJusyo1.Text = dr.TokuisakiAddress1;
                }
                TbxTokuiTanto.Text = dr.TantoStaffCode;
                if (!dr.IsTokuisakiKeisyoNull())
                {
                    DrpKeisyo.SelectedItem.Text = dr.TokuisakiKeisyo;
                }
                string tc = dr.TantoStaffCode;
                DataMaster.M_Tanto1DataTable dtT = ClassMaster.GetTanto1(tc, Global.GetConnection());
                if (dtT.Count >= 1)
                {
                    RcbTanto.SelectedValue = dtT[0].UserID.ToString();
                }
                ChkRiyo.Checked = false;
                TbxTokuiMei1.Text = dr.TokuisakiName1;
                if (!dr.IsTokuisakiTELNull())
                {
                    TbxTokuiTell.Text = dr.TokuisakiTEL;
                }
                if (!dr.IsTokuisakiAddress2Null())
                {
                    TbxTokuiJusyo2.Text = dr.TokuisakiAddress2;
                }
                if (!dr.IsTokuisakiDepartmentNull())
                {
                    TbxTokuiBusyo.Text = dr.TokuisakiDepartment;
                }
                //string cc = dr.CityCode.Trim();
                //DataDrop.M_CityDataTable dtC = ClassDrop.GetCity2(cc, Global.GetConnection());
                //if (dtC.Count >= 1)
                //{
                //    RadCityCode.Text = dtC[0].CityName.ToString();
                //    RadCityCode.SelectedValue = dtC[0].CityCode.ToString();
                //}
                if (!dr.IsKakeritsuNull())
                {
                    TbxKakeritu.Text = dr.Kakeritsu.ToString();
                }
                DrpSimebi.SelectedItem.Value = dr.Shimebi.ToString();
                //DrpTuti.SelectedItem.Value = dr.ZeiTuti.ToString();
                if (!dr.IsBankNoNull())
                {
                    TbxKoza.Text = dr.BankNo;
                }
                if (!dr.IsTokuisakiName2Null())
                {
                    TbxTokuiMei2.Text = dr.TokuisakiName2;
                }
                if (!dr.IsTokuisakiFAXNull())
                {
                    TbxTokuiFax.Text = dr.TokuisakiFAX;
                }
                if (!dr.IsTokuisakiPostNoNull())
                {
                    TbxTokuiYubin.Text = dr.TokuisakiPostNo;
                }

            }
            else
            {
                RcbTanto.Style["display"] = "";
                RadCityCode.Style["display"] = "";

            }
        }

        internal void Claer()
        {
            TbxTokuiCode.Text = TbxTokuiMei1.Text = TbxTokuiMei2.Text = TbxHurigana.Text = TbxTokuiTell.Text = TbxTokuiFax.Text
                = TbxTokuiJusyo1.Text = TbxTokuiJusyo2.Text = TbxTokuiYubin.Text =
                TbxTokuiTanto.Text = TbxTokuiBusyo.Text = TbxKoza.Text = "";
            DrpKeisyo.SelectedValue = DrpSimebi.SelectedValue = DrpTuti.SelectedValue = "";
            ChkRiyo.Checked = true;

        }

        internal bool Toroku()
        {
            try
            {
                DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
                DataSet1.M_Tokuisaki2Row dr = dt.NewM_Tokuisaki2Row();

                dr.CustomerCode = TbxCustomerCode.Text;
                dr.TokuisakiCode = int.Parse(TbxTokuiCode.Text);
                dr.TokuisakiName1 = TbxTokuiMei1.Text;
                dr.TokuisakiName2 = TbxTokuiMei2.Text;
                dr.TokuisakiRyakusyo = TbxTokuisakiRyakusyo.Text;
                dr.TokuisakiFurigana = TbxHurigana.Text;
                dr.TokuisakiTEL = TbxTokuiTell.Text;
                dr.TokuisakiFAX = TbxTokuiFax.Text;
                dr.TokuisakiAddress1 = TbxTokuiJusyo1.Text;
                if (TbxTokuiJusyo2.Text != "")
                {
                    dr.TokuisakiAddress2 = TbxTokuiJusyo2.Text;
                }
                dr.TokuisakiPostNo = TbxTokuiYubin.Text;
                dr.TantoStaffCode = TbxTokuiTanto.Text;
                dr.TokuisakiDepartment = TbxTokuiBusyo.Text;

                if (DrpKeisyo.SelectedValue != "")
                {
                    int nT = int.Parse(DrpKeisyo.SelectedValue);
                    dr.TokuisakiKeisyo = nT.ToString();
                }

                if (RadCityCode.SelectedValue != "")
                {
                    int nC = int.Parse(RadCityCode.SelectedValue);
                    dr.CityCode = nC.ToString();
                }
                if (TbxTokuiTanto.Text != "")
                {
                    dr.TantoStaffCode = TbxTokuiTanto.Text;
                }

                int nR = int.Parse(TbxKakeritu.Text);
                dr.Kakeritsu = nR.ToString();


                if (DrpSimebi.SelectedValue != "")
                {
                    dr.Shimebi = DrpSimebi.Text;
                }


                if (DrpTuti.SelectedValue != "")
                {
                    dr.ZeiTuti = DrpTuti.Text;
                }

                if (TbxKoza.Text != "")
                {
                    int nKZ = int.Parse(TbxKoza.Text);
                    dr.BankNo = nKZ.ToString();
                }

                if (ChkRiyo.Checked == false)
                {
                    dr.RiyoCode = false;
                }
                else
                {
                    dr.RiyoCode = true;
                }

                dr.CreateDate = DateTime.Now;
                dr.CreateUser = SessionManager.User.M_user.UserName;

                dt.AddM_Tokuisaki2Row(dr);

                if (VsID != "")
                {
                    ClassMaster.EditCustomer(VsID, dt, Global.GetConnection());
                }
                else
                {
                    ClassMaster.NewCustomer(dt, Global.GetConnection());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void RadCityCode_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetCity(sender, e);
            }
        }

        protected void RcbTanto_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RcbTanto.SelectedValue))
            {
                TbxTokuiTanto.Text = RcbTanto.SelectedValue;
            }
        }

        protected void RcbTanto_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTanto4(sender, e);
            }
        }

        protected void test_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTanto4(sender, e);
            }
        }
    }
}