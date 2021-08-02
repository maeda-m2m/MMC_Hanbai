using DLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Mitumori.Syosai
{
    public partial class TokuisakiSyosai : System.Web.UI.Page
    {
        private string VsID
        {
            get { return Convert.ToString(this.ViewState["VsID"]); }
            set { this.ViewState["VsID"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblMsg.Text = "※新規登録はマスタ画面へ移動します";
                lblMsg.ForeColor = Color.Red;

                VsID = Request.QueryString["Value"];

                Create();
            }
        }

        private void Create()
        {
            if (VsID != "")
            {
                DataMitumori.M_Customer_NewRow dr =
                    ClassMitumori.GetMTokuisaki(VsID, Global.GetConnection());

                if (dr != null)
                {
                    lblCode.Text = VsID;
                    lblTokuisakiName.Text = dr.CustomerName1;
                    lblJusyo.Text = dr.Address1;
                    lblTel.Text = dr.Tell;
                    lblFax.Text = dr.Fax;
                    lblTokuisakiTanotoName.Text = dr.CustomerPersonnelName;
                    lblBusyo.Text = dr.DeploymentName;
                    //lblYakuwari.Text = dr.TokuisakiTantoYakusyo;

                    if (dr.Titles == 1)
                    {
                        lblKeisyou.Text = "";
                    }
                    //lblTanto.Text = dr.SyuTantouCode.ToString();
                    //lblTantouName.Text = dr.SyuTantouMei;
                }
            }
        }

        protected void BtnMaster_Click(object sender, EventArgs e)
        {
            string url = string.Format("/Master/MsterTokuisaki.aspx");
            string url2 = string.Format("/Mitumori/Syosai/TokuisakiSyosai.aspx");

            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            cs.RegisterStartupScript(cstype, "CloseWindow", "window.close('" + url2 + "',null);", true);
        }
    }
}