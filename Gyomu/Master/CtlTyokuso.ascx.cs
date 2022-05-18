using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class CtlTyokuso : System.Web.UI.UserControl
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
                //市町村
            }
        }

        internal void Create(string code)
        {
            ListSet.SetCity(RcbCityCode);
            string[] codeAry = code.Split('/');
            DataSet1.M_Facility_NewRow dr = Class1.GetFacilitySyousai(codeAry, Global.GetConnection());
            TbxFacility.Text = dr.FacilityNo.ToString();
            if (!dr.IsCodeNull())
            {
                TbxCode.Text = dr.Code.ToString();
            }
            TbxTyokusousakiName1.Text = dr.FacilityName1;
            if (!dr.IsFacilityName2Null())
            {
                TbxTyokusousakiName2.Text = dr.FacilityName2;
            }
            if (!dr.IsAbbreviationNull())
            {
                TbxTyokusousakiRyakusyou.Text = dr.Abbreviation;
            }
            if (!dr.IsFacilityResponsibleNull())
            {
                TbxTyokusousakiTantou.Text = dr.FacilityResponsible;
            }
            if (!dr.IsPostNoNull())
            {
                TbxTyokusousakiYubin.Text = dr.PostNo;
            }
            if (!dr.IsAddress1Null())
            {
                TbxTyokusousakiAddress1.Text = dr.Address1;
            }
            if (!dr.IsAddress2Null())
            {
                TbxTyokusousakiAddress2.Text = dr.Address2;
            }
            if (!dr.IsCityCodeNull())
            {
                RcbCityCode.SelectedValue = dr.CityCode.ToString();
            }
            if (!dr.IsTellNull())
            {
                TbxTyokusousakiTell.Text = dr.Tell;
            }
            if (!dr.IsTitlesNull())
            {
                TbxKeisyo.Text = dr.Titles;
            }
            if (!dr.IsStateNull())
            {
                if (dr.State.Equals("1"))
                {
                    RdoYuko.SelectedValue = "True";
                }
                else
                {
                    RdoYuko.SelectedValue = "False";
                }
            }
        }

        internal bool Toroku()
        {
            //登録
            try
            {
                DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
                DataSet1.M_Facility_NewRow dr = dt.NewM_Facility_NewRow();

                if (!string.IsNullOrEmpty(TbxFacility.Text))
                {
                    dr.FacilityNo = int.Parse(TbxFacility.Text);
                }
                if (!string.IsNullOrEmpty(TbxCode.Text))
                {
                    dr.Code = TbxCode.Text;
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiName1.Text))
                {
                    dr.FacilityName1 = TbxTyokusousakiName1.Text;
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiName2.Text))
                {
                    dr.FacilityName2 = TbxTyokusousakiName2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiRyakusyou.Text))
                {
                    dr.Abbreviation = TbxTyokusousakiRyakusyou.Text;
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiTantou.Text))
                {
                    dr.FacilityResponsible = TbxTyokusousakiTantou.Text;
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiAddress1.Text))
                {
                    dr.Address1 = TbxTyokusousakiAddress1.Text;
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiAddress2.Text))
                {
                    dr.Address2 = TbxTyokusousakiAddress2.Text;
                }
                if (!string.IsNullOrEmpty(RcbCityCode.Text))
                {
                    dr.CityCode = int.Parse(RcbCityCode.SelectedValue);
                }
                if (!string.IsNullOrEmpty(TbxTyokusousakiTell.Text))
                {
                    dr.Tell = TbxTyokusousakiTell.Text;
                }
                if (!string.IsNullOrEmpty(TbxKeisyo.Text))
                {
                    dr.Titles = TbxKeisyo.Text;
                }
                if (!string.IsNullOrEmpty(RdoYuko.SelectedValue))
                {
                    if (RdoYuko.SelectedValue == "True")
                    {
                        dr.State = "1";
                    }
                    else
                    {
                        dr.State = "2";
                    }
                }
                dt.AddM_Facility_NewRow(dr);
                //DataSet1.M_TyokusosakiDataTable dtC = Class1.GetNouhinsaki(TbxFacility.Text, Global.GetConnection());
                Class1.UpdateFaci(dt, TbxCode.Text, TbxFacility.Text, Global.GetConnection());

                return true;
            }
            catch
            {
                return false;
            }
        }

        internal void Clear()
        {
            TbxFacility.Text = "";
            TbxTyokusousakiName1.Text = "";
            TbxTyokusousakiName2.Text = "";
            TbxTyokusousakiRyakusyou.Text = "";
            TbxTyokusousakiTantou.Text = "";
            TbxTyokusousakiYubin.Text = "";
            TbxTyokusousakiAddress1.Text = "";
            TbxTyokusousakiAddress2.Text = "";
            RcbCityCode.Text = "";
            TbxTyokusousakiTell.Text = "";
            TbxKeisyo.Text = "";
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }
    }
}