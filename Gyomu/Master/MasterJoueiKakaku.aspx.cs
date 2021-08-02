using System;
using DLL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Gyomu.Master
{
    public partial class MasterJoueiKakaku : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
                K.Style["dispay"] = "";
            }
        }

        private void Create()
        {
            DataMaster.M_JoueiKakakuDataTable dt = ClassMaster.GetJoueiList(Global.GetConnection());
            DGJoueiKakaku.DataSource = dt;
            DGJoueiKakaku.DataBind();
        }

        protected void DGJoueiKakaku_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMaster.M_JoueiKakakuRow dr = (e.Item.DataItem as DataRowView).Row as DataMaster.M_JoueiKakakuRow;

                Label LblShiiresakiCode = e.Item.FindControl("LblShiiresakiCode") as Label;
                Label LblShiiresakiName = e.Item.FindControl("LblShiiresakiName") as Label;
                Label LblMedia = e.Item.FindControl("LblMedia") as Label;
                Label LblHanni = e.Item.FindControl("LblHanni") as Label;
                Label LblCapacity = e.Item.FindControl("LblCapacity") as Label;
                Label LblHyoujunKakaku = e.Item.FindControl("LblHyoujunKakaku") as Label;
                Label LblShiireKakaku = e.Item.FindControl("LblShiireKakaku") as Label;

                LblShiiresakiCode.Text = dr.ShiiresakiCode.ToString();
                if (!dr.IsShiiresakiNameNull())
                {
                    LblShiiresakiName.Text = dr.ShiiresakiName;
                }
                LblMedia.Text = dr.Media;
                LblHanni.Text = dr.Range;
                LblCapacity.Text = dr.Capacity;
                LblHyoujunKakaku.Text = dr.HyoujunKakaku;

                if (!dr.IsShiireKakakuNull())
                {
                    LblShiireKakaku.Text = dr.ShiireKakaku;
                }
            }
        }

        protected void DGJoueiKakaku_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }
    }
}