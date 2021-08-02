using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu
{
    public partial class CtlMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblName.Text = SessionManager.User.M_user.UserName;
        }

        protected override void OnPreRender(EventArgs e)
        {
            Telerik.Web.UI.RadMenuItem item2 = this.RadMenu1.FindItemByValue("ログアウト");
            if (item2 != null)
            {
                item2.Attributes["onclick"] = "if (!confirm('" + ("ログアウトします。よろしいですか？") + "')) return false;";
            }
        }
    }
}