using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class CtlShiire : System.Web.UI.UserControl
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

        }

        internal void Create(string ShiireCode)
        {
            vsID = ShiireCode;

            DataMaster.M_Shiire_NewRow dr =
                ClassMaster.GetShiire(vsID, Global.GetConnection());

            TbxCode.Text = dr.ShiireCode.ToString();
            if (!dr.IsShiireNameNull())
            {
                TbxShiire.Text = dr.ShiireName;
            }
            if (!dr.IsKanaNull())
            {
                TbxKana.Text = dr.Kana;
            }
            if (!dr.IsAbbreviationNull())
            {
                TbxRyaku.Text = dr.Abbreviation;
            }
            if (!dr.IsPostNoNull())
            {
                TbxPost.Text = dr.PostNo;
            }
            if (!dr.IsAddress1Null())
            {
                TbxAdd1.Text = dr.Address1;
            }
            if (!dr.IsAddress2Null())
            {
                TbxAdd2.Text = dr.Address2;
            }
            if (!dr.IsTellNull())
            {
                TbxTell.Text = dr.Tell;
            }
            if (!dr.IsFaxNull())
            {
                TbxFax.Text = dr.Fax;
            }
            if (!dr.IsPersonnelNull())
            {
                TbxPersonal.Text = dr.Personnel;
            }
            if (!dr.IsDeploymentNull())
            {
                TbxBusyo.Text = dr.Deployment;
            }
            if (!dr.IsCutoffDateNull())
            {
                DrpOff.SelectedValue = dr.CutoffDate.ToString();
            }
            if (!dr.IsPaymentDateNull())
            {
                Drppay.SelectedValue = dr.PaymentDate.ToString();
            }
            if (!dr.IsRemarksNull())
            {
                TbxOther.Text = dr.Remarks;
            }
        }

        internal void Clear()
        {
            TbxCode.Text = TbxShiire.Text = TbxKana.Text =
            TbxRyaku.Text = TbxPost.Text = TbxAdd1.Text =
            TbxAdd2.Text = TbxTell.Text = TbxFax.Text =
            TbxPersonal.Text = TbxOther.Text = "";

            DrpOff.SelectedValue = Drppay.SelectedValue = "";

        }

        internal bool Add()
        {
            try
            {
                DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
                DataMaster.M_Shiire_NewRow dr = dt.NewM_Shiire_NewRow();

                int nC = int.Parse(TbxCode.Text);
                dr.ShiireCode = nC;

                if (!string.IsNullOrEmpty(TbxShiire.Text))
                {
                    dr.ShiireName = TbxShiire.Text;
                }
                if (!string.IsNullOrEmpty(TbxKana.Text))
                {
                    dr.Kana = TbxKana.Text;
                }
                if (!string.IsNullOrEmpty(TbxRyaku.Text))
                {
                    dr.Abbreviation = TbxRyaku.Text;
                }
                if (!string.IsNullOrEmpty(TbxPost.Text))
                {
                    dr.PostNo = TbxPost.Text;
                }
                if (!string.IsNullOrEmpty(TbxAdd1.Text))
                {
                    dr.Address1 = TbxAdd1.Text;
                }
                if (!string.IsNullOrEmpty(TbxAdd2.Text))
                {
                    dr.Address2 = TbxAdd2.Text;
                }
                if (!string.IsNullOrEmpty(TbxTell.Text))
                {
                    dr.Tell = TbxTell.Text;
                }
                if (!string.IsNullOrEmpty(TbxFax.Text))
                {
                    dr.Fax = TbxFax.Text;
                }
                if (!string.IsNullOrEmpty(TbxPersonal.Text))
                {
                    dr.Personnel = TbxPersonal.Text;
                }
                if (!string.IsNullOrEmpty(TbxBusyo.Text))
                {
                    dr.Deployment = TbxBusyo.Text;
                }
                if (DrpOff.SelectedValue != "")
                {
                    int nO = int.Parse(DrpOff.SelectedValue);
                    dr.CutoffDate = nO;
                }
                if (Drppay.SelectedValue != "")
                {
                    int nP = int.Parse(Drppay.SelectedValue);
                    dr.PaymentDate = nP;
                }
                dr.Remarks = TbxOther.Text;

                dr.CreateUser = SessionManager.User.M_user.UserName;
                dr.CreateDate = DateTime.Now;

                dt.AddM_Shiire_NewRow(dr);

                if (vsID != "")
                {
                    ClassMaster.EditShiire(dt, vsID, Global.GetConnection());
                }
                else
                {
                    ClassMaster.NewShiire(dt, Global.GetConnection());
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}