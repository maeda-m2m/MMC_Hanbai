using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Create();
                SessionManager.Logout();
                LblCopyright.Text = "Copyright(c) Movie Management Company CORPORATION,2019 Production Planning Dept. ";
            }
        }

        private void Create()
        {
            DataMaster.T_OshiraseDataTable dt = ClassMaster.GetOshirase(Global.GetConnection());
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    DataMaster.T_OshiraseRow dr = dt[i];
                    TbxInfomation.Text += dr.OshiraseNaiyou.Replace("<br>", "\r\n");
                    TbxInfomation.Text += "\r\n" + "------------------------------------------------" + dr.CreateDate + "------------------------------------------------" + "\r\n";
                }
            }
            else
            {
                TbxInfomation.Text = "お知らせがありません";
            }
        }

        protected void BtnRogin_Click(object sender, EventArgs e)
        {
            if (TbxLogin.Text != "")
            {
                int a = 0;
                if (TbxPass.Text != "")
                {
                    try
                    {
                        a = int.Parse(TbxLogin.Text);
                    }
                    catch
                    {
                        Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("ログインID又は、パスワードが違います。");
                    }
                    if (a != 0)
                    {
                        DataSet1.M_TantoRow dr =
                            ClassLogin.GetLoginData(a.ToString(), TbxPass.Text, Global.GetConnection());

                        if (dr == null)
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("ログインID又は、パスワードが違います。");
                        }
                        else
                        {

                            SessionManager.Login(dr);
                            if (TbxLogin.Text != "m2m")
                                ClassLogin.GetLoginlog(dr.UserName, TbxLogin.Text, Global.GetConnection());
                            Response.Redirect("~/Mitumori/MitumoriItiran.aspx");
                        }
                    }
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("パスワードが空欄です。");
                }
            }
            else
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("ログインIDが空欄です。");
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }
    }
}