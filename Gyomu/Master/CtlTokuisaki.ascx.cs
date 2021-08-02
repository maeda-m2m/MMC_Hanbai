using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class CtlTokuisaki : System.Web.UI.UserControl
    {
        private String vsID
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
            if (!this.IsPostBack)
            {
                RadCityCode.Style["display"] = "";
                //担当名DropDwonList
                ListSet.SetTanto2(sender, RadTanto);
                //市町村
                //ListSet.SetCity(RadCityCode);
            }
        }

        internal void Create(string userId)
        {
            vsID = userId;
            string[] code = vsID.Split('/');
            DataSet1.M_Tokuisaki2Row dr = Class1.GetTokuisaki3(code[0],code[1], Global.GetConnection());

            TbxCustomerCode.Text = dr.CustomerCode;
            TbxTokuiCode.Text = dr.TokuisakiCode.ToString();
            TbxHurigana.Text = dr.TokuisakiFurifana;
            TbxTokuiJusyo1.Text = dr.TokuisakiAddress1;
            TbxTokuiTanto.Text = dr.TantoStaffCode;
            //TbxTokuiTantoTell.Text = dr.TokuisakiTantoTell;
            if (!dr.IsTokuisakiKeisyoNull())
            {
                DrpKeisyo.SelectedItem.Text = dr.TokuisakiKeisyo;
            }
            //DrpKanmin.SelectedItem.Value = dr.KanminCode.ToString();
            //if (!dr.IsHeisyaTantouMeiNull())
            //{
            //    RadTanto.SelectedItem.Text = dr.HeisyaTantouMei;
            //}
            //TbxRyakusyo.Text = dr.SeikyusakiRyakusyo;
            //DrpUHasu.SelectedItem.Value = dr.UriageHasuCode.ToString();
            //drpGinko.SelectedItem.Value = dr.KaisyaGinkoCode.ToString();
            string tc = dr.TantoStaffCode;
            DataMaster.M_Tanto1DataTable dtT = ClassMaster.GetTanto1(tc, Global.GetConnection());
            if(dtT.Count >= 1)
            {
                RadTanto.SelectedValue = dtT[0].UserID.ToString();
            }
            ChkRiyo.Checked = dr.RiyoCode;
            TbxTokuiMei1.Text = dr.TokuisakiName1;
            TbxTokuiTell.Text = dr.TokuisakiTEL;
            TbxTokuiJusyo2.Text = dr.TokuisakiAddress2;
            TbxTokuiBusyo.Text = dr.TokuisakiDepartment;
            //TbxTokuiTantoMail.Text = dr.TokuisakiTantoEMail;
            string cc = dr.CityCode.Trim();
            DataDrop.M_CityDataTable dtC = ClassDrop.GetCity2(cc, Global.GetConnection());
            if(dtC.Count >= 1)
            {
                RadCityCode.Text = dtC[0].CityName.ToString();
                RadCityCode.SelectedValue = dtC[0].CityCode.ToString();
            }
            TbxKakeritu.Text = dr.Kakeritsu.ToString();
            DrpSimebi.SelectedItem.Value = dr.Shimebi.ToString();
            DrpTuti.SelectedItem.Value = dr.ZeiTuti.ToString();
            //DrpSZHasu.SelectedItem.Value = dr.SyouhizeoHasuCode.ToString();
            TbxKoza.Text = dr.BankNo;
            TbxTokuiMei2.Text = dr.TokusiakiName2;
            TbxTokuiFax.Text = dr.TokuisakiFAX;
            TbxTokuiYubin.Text = dr.TokuisakiPostNo;

        }

        internal void Claer()
        {
            TbxTokuiCode.Text = TbxTokuiMei1.Text = TbxTokuiMei2.Text = TbxHurigana.Text = TbxTokuiTell.Text = TbxTokuiFax.Text
                = TbxTokuiJusyo1.Text = TbxTokuiJusyo2.Text = TbxTokuiYubin.Text =
                TbxTokuiTanto.Text = TbxTokuiBusyo.Text = TbxKoza.Text = "";
            DrpKeisyo.SelectedValue = DrpSimebi.SelectedValue = DrpTuti.SelectedValue = "";
            ChkRiyo.Checked = true;

            RadCityCode.SelectedValue = RadTanto.SelectedValue = "";
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
                dr.TokusiakiName2 = TbxTokuiMei2.Text;
                dr.TokuisakiRyakusyo = TbxTokuisakiRyakusyo.Text;
                dr.TokuisakiFurifana = TbxHurigana.Text;
                dr.TokuisakiTEL = TbxTokuiTell.Text;
                dr.TokuisakiFAX = TbxTokuiFax.Text;
                dr.TokuisakiAddress1 = TbxTokuiJusyo1.Text;
                if (TbxTokuiJusyo2.Text != "")
                {
                    dr.TokuisakiAddress2 = TbxTokuiJusyo2.Text;
                }
                dr.TokuisakiPostNo = TbxTokuiYubin.Text;
                dr.TokuisakiStaff = TbxTokuiTanto.Text;
                dr.TokuisakiDepartment = TbxTokuiBusyo.Text;
                //dr.TokuisakiTantoTell = TbxTokuiTantoTell.Text;
                //dr.TokuisakiTantoEMail = TbxTokuiTantoMail.Text;

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

                //if (DrpKanmin.SelectedValue != "")
                //{
                //    int nK = int.Parse(DrpKanmin.SelectedValue);
                //    dr.KanminCode = nK;
                //    dr.KanminMei = DrpKanmin.Text;
                //}

                int nR = int.Parse(TbxKakeritu.Text);
                dr.Kakeritsu = nR.ToString();

                if (RadTanto.SelectedValue != "")
                {
                    int nN = int.Parse(RadTanto.SelectedValue);
                    dr.TantoStaffCode = nN.ToString();
                }

                if (DrpSimebi.SelectedValue != "")
                {
                    //int nS = int.Parse(DrpSimebi.SelectedValue);
                    //dr.ShimebikubunCode = nS;
                    dr.Shimebi = DrpSimebi.Text;
                }

                //dr.SeikyusakiRyakusyo = TbxRyakusyo.Text;

                if (DrpTuti.SelectedValue != "")
                {
                    //int nT = int.Parse(DrpTuti.SelectedValue);
                    //dr.ZeigakuTutiCode = nT;
                    dr.ZeiTuti = DrpTuti.Text;
                }

                //if (DrpUHasu.SelectedValue != "")
                //{
                //    int nH = int.Parse(DrpUHasu.SelectedValue);
                //    dr.UriageHasuCode = nH;
                //    dr.UriageHasuHouhou = DrpUHasu.Text;
                //}

                //if (DrpSZHasu.SelectedValue != "")
                //{
                //    int nS = int.Parse(DrpSZHasu.SelectedValue);
                //    dr.SyouhizeoHasuCode = nS;
                //    dr.SyouhizeoHasuHouhou = DrpSZHasu.Text;
                //}

                //if (TbxYosingaku.Text != "")
                //{
                //    int nY = int.Parse(TbxYosingaku.Text);
                //    dr.YosinGaku = nY;
                //}

                //if (drpGinko.SelectedValue != "")
                //{
                //    dr.KaisyaGinkoCode = 9;
                //}

                if (TbxKoza.Text != "")
                {
                    int nKZ = int.Parse(TbxKoza.Text);
                    dr.BankNo = nKZ.ToString();
                }

                if (ChkRiyo.Checked != false)
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

                if (vsID != "")
                {
                    ClassMaster.EditCustomer(vsID, dt, Global.GetConnection());
                }
                else
                {
                    ClassMaster.NewCustomer(dt, Global.GetConnection());
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void RadCityCode_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if(e.Text != "")
            {
                ListSet.SetCity(sender, e);
            }
        }
    }
}