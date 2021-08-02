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
    public partial class TyokusosakiMeisai : System.Web.UI.Page
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

                lblMeg2.Text = "※マスタ新規画面に移動する場合このページは自動で閉じます。";
                lblMeg2.ForeColor = Color.Red;

                VsID = Request.QueryString["Value"];

                Creste();
            }
        }

        private void Creste()
        {
            if (VsID != "")
            {
                DataMitumori.M_Facility_NewRow dr =
                       ClassMitumori.GetTyokusosaki(VsID, Global.GetConnection());

                if (dr != null)
                {
                    lblTyokusoCode.Text = dr.FacilityNo.ToString();
                    lblTyokusoName.Text = VsID;
                    lblTyokusoTantou.Text = dr.FacilityResponsible;
                    lblJusyo.Text = dr.Address1;
                    lblTell.Text = dr.Tell;
                    //lblSpotto.Text = dr.TyokusoSupotto;
                }

            }
        }

        protected void BtnMaster_Click(object sender, EventArgs e)
        {
            string url = string.Format("/Master/MasterTyokuso.aspx");
            string url2 = string.Format("/Mitumori/Syosai/TyokusosakiSyosai.aspx");

            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            cs.RegisterStartupScript(cstype, "CloseWindow", "window.close('" + url2 + "',null);", true);

        }
    }
}