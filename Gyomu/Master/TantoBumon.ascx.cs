using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class TantoBumon : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListSet.SetBumon(RadBumon);

        }
    }
}