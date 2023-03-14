using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Tokuisaki
{
    public partial class TestFacility : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                SearchHidden.Value = "false";
                Create();
            }

        }

        private void Create()
        {
            string sqlCommand = "select * from M_Facility_NewBackup order by FacilityNo desc";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainRadGrid.VirtualItemCount = table.Rows.Count;
            int nPageSize = MainRadGrid.PageSize;
            int nPageCount = table.Rows.Count / nPageSize;
            if (0 < table.Rows.Count % nPageSize) nPageCount++;
            if (nPageCount <= MainRadGrid.MasterTableView.CurrentPageIndex) MainRadGrid.MasterTableView.CurrentPageIndex = 0;

            MainRadGrid.DataSource = table;

            MainRadGrid.DataBind();
        }

        private void SearchCreate()
        {
            if (CategoryDrop.SelectedValue == "")
            {
                string script = $"alert('カテゴリを選択してください。')";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return;
            }

            string sqlCommand = "select * from M_Facility_NewBackup where " + CategoryDrop.SelectedValue + " like" + "'%" + SearchText.Text + "%'";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainRadGrid.VirtualItemCount = table.Rows.Count;
            int nPageSize = MainRadGrid.PageSize;
            int nPageCount = table.Rows.Count / nPageSize;
            if (0 < table.Rows.Count % nPageSize) nPageCount++;
            if (nPageCount <= MainRadGrid.MasterTableView.CurrentPageIndex) MainRadGrid.MasterTableView.CurrentPageIndex = 0;

            MainRadGrid.DataSource = table;

            MainRadGrid.DataBind();

        }

        protected void MainRadGrid_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            if (SearchHidden.Value == "true")
            {
                SearchCreate();
            }
            else
            {
                Create();
            }

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            SearchHidden.Value = "true";
            SearchCreate();

        }
    }
}