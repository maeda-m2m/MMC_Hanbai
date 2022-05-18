using System;
using DLL;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Yodokou_HanbaiKanri;
using System.IO;
using System.Text;

namespace Gyomu.Master
{
    public partial class Test : System.Web.UI.Page
    {
        public static DataTable dtV;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {
            int intPagesize = 20;
            int intPageIndex = testG.CurrentPageIndex + 1;
            int intPageCount = testG.PageCount;
            if (dtV == null)
            {
                DataTable dt = ClassMaster.test(Global.GetConnection());
                dtV = dt;
                intPageCount = dtV.Rows.Count / 20;
               
            }
            DataTable dtN = new DataTable();
            dtN = dtV.Copy();
            dtN.Clear();
            for (int i = intPagesize * intPageIndex - 20; i < intPagesize * intPageIndex; i++)
            {
                DataRow dr = dtN.NewRow();
                dr.ItemArray = dtV.Rows[i].ItemArray;
                dtN.Rows.Add(dr);
            }

            testG.DataSource = dtN;
            testG.DataBind();
        }

        protected void testG_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow dr = (e.Item.DataItem as DataRowView).Row as DataRow;
                Label LblSyouhinCode = e.Item.FindControl("LblSyouhinCode") as Label;
                Label LblSyouhinName = e.Item.FindControl("LblSyouhinName") as Label;

                LblSyouhinCode.Text = dr["SyouhinCode"].ToString();
                LblSyouhinName.Text = dr["SyouhinMei"].ToString();
            }
        }
        bool _bD_PageSizeChanged = false;

        protected void testG_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            if (!_bD_PageSizeChanged)
            {
                _bD_PageSizeChanged = true;
                testG.CurrentPageIndex = e.NewPageIndex;
                Create();
            }
        }

        protected void Btn_Click(object sender, EventArgs e)
        {

        }
    }

}