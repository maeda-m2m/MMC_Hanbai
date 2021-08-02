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
            TbxShiire.Text = dr.ShiireName;
            TbxKana.Text = dr.Kana;
            TbxRyaku.Text = dr.Abbreviation;
            TbxPost.Text = dr.PostNo;
            TbxAdd1.Text = dr.Address1;
            TbxAdd2.Text = dr.Address2;
            TbxTell.Text = dr.Tell;
            TbxFax.Text = dr.Fax;
            TbxPersonal.Text = dr.Personnel;
            TbxBusyo.Text = dr.Deployment;
            DrpOff.SelectedValue = dr.CutoffDate.ToString();
            Drppay.SelectedValue = dr.PaymentDate.ToString();
            TbxOther.Text = dr.Remarks;
        }

        internal void Clear()
        {
            TbxCode.Text = TbxShiire.Text = TbxKana.Text = 
            TbxRyaku.Text = TbxPost.Text =TbxAdd1.Text =
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

                dr.ShiireName = TbxShiire.Text;
                dr.Kana = TbxKana.Text;
                dr.Abbreviation = TbxRyaku.Text;
                dr.PostNo = TbxPost.Text;
                dr.Address1 = TbxAdd1.Text;
                dr.Address2 = TbxAdd2.Text;
                dr.Tell = TbxTell.Text;
                dr.Fax = TbxFax.Text;
                dr.Personnel = TbxPersonal.Text;
                dr.Deployment = TbxBusyo.Text;
                if(DrpOff.SelectedValue!="")
                {
                    int nO = int.Parse(DrpOff.SelectedValue);
                    dr.CutoffDate = nO;
                }
                if(Drppay.SelectedValue!="")
                {
                    int nP = int.Parse(Drppay.SelectedValue);
                    dr.PaymentDate = nP;
                }
                dr.Remarks = TbxOther.Text;

                dr.CreateUser = SessionManager.User.M_user.UserName;
                dr.CreateDate = DateTime.Now;

                dt.AddM_Shiire_NewRow(dr);

                if(vsID!="")
                {
                    ClassMaster.EditShiire(dt,vsID, Global.GetConnection());
                }
                else
                {
                    ClassMaster.NewShiire(dt,Global.GetConnection());
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}