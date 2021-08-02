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
    public partial class SyouhinSyosaiaspx : System.Web.UI.Page
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
                string sSyouhin = VsID.Split(',')[0];
                string sShiire = VsID.Split(',')[1];

                DataMitumori.M_Syohin_NewRow dr =
                    ClassMitumori.GetMSyouhin(sSyouhin,sShiire, Global.GetConnection());

                if(dr!=null)
                {
                    lblSyouhinCode.Text = dr.SyouhinCode;
                    lblSyouhinMei.Text = dr.SyouhinMei;
                    //lblKubun.Text = dr.Meisaikubun.ToString();
                    lblHinban.Text = dr.MekarHinban;
                    lblKeitai.Text = dr.Media;
                    lblShiireCode.Text = dr.ShiireCode.ToString();
                    lblShiire.Text = dr.ShiireMei;
                    //lblTosyo.Text = dr.TosyokanCode;
                    lblSoko.Text = dr.Warehouse.ToString();
                    if (dr.RiyoJoutai == false)
                    {
                        lblRiyou.Text = "有効";
                    }
                    else
                    {
                        lblRiyou.Text = "無効";
                    }

                }
            }
        }

        protected void BtnMaster_Click(object sender, EventArgs e)
        {
            string url = string.Format("/Master/MasterSyohin.aspx");
            string url2 = string.Format("/Mitumori/Syosai/SyouhinSyosai.aspx");

            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            cs.RegisterStartupScript(cstype, "CloseWindow", "window.close('" + url2 + "',null);", true);
        }
    }
}