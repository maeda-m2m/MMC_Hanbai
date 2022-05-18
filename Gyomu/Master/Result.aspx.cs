using System;
using DLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class Result : System.Web.UI.Page
    {

        public static DataTable dtError;
        public static DataMaster.T_ErrorNaiyouDataTable dtNaiyou;
        public static string strBackPage;
        public static string strCounts;
        public static string strSuccessCounts;
        public static string strErrorCounts;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {

            if (!string.IsNullOrEmpty(strCounts))
            {
                LblCounts.Text = strCounts;
            }
            if (!string.IsNullOrEmpty(strSuccessCounts))
            {
                LblSuccessCount.Text = strSuccessCounts;
            }
            if (!string.IsNullOrEmpty(strErrorCounts))
            {
                LblFailedCount.Text = strErrorCounts;
            }

            if (dtNaiyou != null)
            {
                if (dtNaiyou.Rows.Count > 0)
                {
                    DGdetail.DataSource = dtNaiyou;
                    DGdetail.DataBind();
                }
            }


            if (dtError != null)
            {
                if (dtError.Rows.Count > 0)
                {
                    for (int i = 0; i < dtError.Rows.Count; i++)
                    {

                    }
                }
            }
        }

        protected void DGdetail_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMaster.T_ErrorNaiyouRow dr = (e.Item.DataItem as DataRowView).Row as DataMaster.T_ErrorNaiyouRow;
                Label LblErrorNo = e.Item.FindControl("LblErrorNo") as Label;
                Label LblErrorDatail = e.Item.FindControl("LblErrorDatail") as Label;

                if (!dr.IsnoNull())
                {
                    LblErrorNo.Text = dr.no;
                }
                if (!dr.IsnaiyouNull())
                {
                    LblErrorDatail.Text = dr.naiyou;
                }
            }
        }

        protected void DGFaliedData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strBackPage))
            {
                Response.Redirect(strBackPage);
            }
            else
            {
                Response.Redirect("MasterSyohin.aspx");
            }
        }
    }
}