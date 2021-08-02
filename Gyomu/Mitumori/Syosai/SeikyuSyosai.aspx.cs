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
    public partial class SeikyuSyousai : System.Web.UI.Page
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
            if(VsID!="")
            {
                DataMitumori.M_Customer_NewRow dr =
                    ClassMitumori.GetMTokuisaki(VsID, Global.GetConnection());

                if (dr != null)
                {
                    lblSeikyuCode.Text = VsID;
                    if (!dr.IsRequestFirstNull())
                        lblSeikyuRyajuSyo.Text = dr.Abbreviation;
                    if (dr.CutoffDate == 5)
                    {
                        lblSimebiKubun.Text = "5日締め";
                    }
                    else
                    if(dr.CutoffDate == 10)
                    {
                        lblSimebiKubun.Text = "10日締め";
                    }
                    else
                    if(dr.CutoffDate == 15)
                    {
                        lblSimebiKubun.Text = "15日締め";
                    }
                    if (dr.CutoffDate == 20)
                    {
                        lblSimebiKubun.Text = "20日締め";
                    }
                    else
                   if (dr.CutoffDate == 25)
                    {
                        lblSimebiKubun.Text = "25日締め";
                    }
                    else
                   if (dr.CutoffDate == 99)
                    {
                        lblSimebiKubun.Text = "月末締め";
                    }
                    if (dr.CutoffDate == 0)
                    {
                        lblSimebiKubun.Text = "随時締め";
                    }

                    if (dr.TaxAdvice == 0)
                    {
                        lblZeigakututi.Text = "伝票単位";
                    }
                    else
                    if(dr.TaxAdvice ==1)
                    {
                        lblZeigakututi.Text = "請求書単位";
                    }
                    else
                        if(dr.TaxAdvice==2)
                    {
                        lblZeigakututi.Text = "免税";
                    }
                    else
                        if(dr.TaxAdvice == 3)
                    {
                        lblZeigakututi.Text = "無税";
                    }
                    else
                        if (dr.TaxAdvice == 4)
                    {
                        lblZeigakututi.Text = "明細単位";
                    }
                    
                    if(dr.TaxAdviceDistinguish==1)
                    {
                        lblZeikomikubun.Text = "税抜";
                    }
                    else
                    {
                        lblSimebiKubun.Text = "税込";
                    }

                    if(dr.Bank==9)
                    {
                        lblGinkoCode.Text = "なでしこ";
                    }
                    
                    lblKaisyaKoza.Text = dr.BankCode.ToString();
                    //lblIrainin.Text = dr.HurikomiIraininMei;
                }
            }
        }

        protected void BtnMaster_Click(object sender, EventArgs e)
        {
            string url = string.Format("/Master/MsterTokuisaki.aspx");
            string url2 = string.Format("/Mitumori/Syosai/SeikyuSyosai.aspx");

            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            cs.RegisterStartupScript(cstype, "CloseWindow", "window.close('" + url2 + "',null);", true);
        }
    }
}