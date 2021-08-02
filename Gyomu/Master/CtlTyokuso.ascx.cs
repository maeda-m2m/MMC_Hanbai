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
                ListSet.SetCity(RadCityCode);
            }
        }

        internal void Create(string Name1)
        {
            DataSet1.M_Facility_NewRow dr =
                Class1.FacilityRow2(Name1, Global.GetConnection());

            vsID = Name1;

            TbxFacility.Text = dr.FacilityNo.ToString();
            if (!dr.IsCodeNull())
            {
                TbxCode.Text = dr.Code;
            }
            if (!dr.IsCityCodeNull())
            {
                RadCityCode.SelectedValue = dr.CityCode.ToString();
            }

            TbxFacilityName1.Text = dr.FacilityName1;
            if (!dr.IsFacilityName2Null())
            {
                TbxFacilityName2.Text = dr.FacilityName2;
            }
            if (!dr.IsFacilityResponsibleNull())
            {
                TbxFacilityResponsible.Text = dr.FacilityResponsible;
            }
            if (!dr.IsAbbreviationNull())
            {
                TbxAbbreviation.Text = dr.Abbreviation;
            }
            if (!dr.IsAddress1Null())
            {
                TbxAddress1.Text = dr.Address1;
            }
            if (!dr.IsAddress2Null())
            {
                TbxAddress2.Text = dr.Address2;
            }
            if (!dr.IsTellNull())
            {
                TbxTEll.Text = dr.Tell;
            }
            if (!dr.IsTitlesNull())
            {
                TbxTitles.Text = dr.Titles;
            }
            if (!dr.IsStateNull())
            {
                if (dr.State == false)
                {
                    ChkYuko.Checked = true;
                }
            }
        }

        internal bool Toroku()
        {
            //登録
            try
            {
                DataMaster.M_Facility_NewDataTable dt = new DataMaster.M_Facility_NewDataTable();
                DataMaster.M_Facility_NewRow dr = dt.NewM_Facility_NewRow();

                if (TbxFacility.Text != "")
                {
                    int nFa = int.Parse(TbxFacility.Text);
                    dr.FacilityNo = nFa;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("施設Noを登録してください。");
                    return false;
                }

                if (TbxCode.Text != "")
                {
                    dr.Code = TbxCode.Text;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("コードを登録してください。");
                    return false;
                }

                dr.FacilityName1 = TbxFacilityName1.Text;

                dr.FacilityName2 = TbxFacilityName2.Text;

                dr.Abbreviation = TbxAbbreviation.Text;

                dr.FacilityResponsible = TbxFacilityResponsible.Text;

                dr.PostNo = TbxPost.Text;

                dr.Address1 = TbxAddress1.Text;

                dr.Address2 = TbxAddress2.Text;

                dr.Tell = TbxTEll.Text;

                if (RadCityCode.SelectedValue != "")
                {
                    int nCity = int.Parse(RadCityCode.SelectedValue);
                    dr.CityCode = nCity;
                }

                dr.Titles = TbxTitles.Text;

                if (ChkYuko.Checked == false)
                {
                    dr.State = true;
                }
                else
                {
                    dr.State = false;
                    //dr.RiyouJotai = "無効";
                }

                dr.UpDateDay = DateTime.Now;
                dr.UpDateUser = SessionManager.User.UserID;

                dt.AddM_Facility_NewRow(dr);

                if (vsID != "")
                {
                    ClassMaster.EditTyokuso(vsID, dt, Global.GetConnection());
                }
                else
                {
                    ClassMaster.NewTyokuso(dt, Global.GetConnection());
                }


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal void Clear()
        {
            RadCityCode.SelectedValue = "";
            TbxCode.Text = TbxFacility.Text = TbxFacilityName1.Text = TbxFacilityName2.Text
           = TbxAbbreviation.Text = TbxFacilityResponsible.Text = TbxPost.Text =
           TbxTEll.Text = TbxAddress1.Text = TbxAddress2.Text = TbxTitles.Text = "";

            ChkYuko.Checked = true;
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }
    }
}