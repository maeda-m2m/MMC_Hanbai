using System;
using DLL;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using System.IO;
using System.Text;
namespace Gyomu.Master
{
    public partial class MasterKakaku : System.Web.UI.Page
    {
        const int LIST_ID = 90;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.F.Create(LIST_ID, 3);

                lblMsg.Text = "チェックした情報の価格修正を行います。";

                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
        }

        private void Create()
        {
            lblMsg.Text = "";
            L.Visible = true;

            UserViewManager.UserView v = SessionManager.User.GetUserView(LIST_ID);

            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

            string strWhere = "";
            try
            {
                strWhere = this.F.GetFilter(da.SelectCommand);
                if (!string.IsNullOrEmpty(strWhere))
                    da.SelectCommand.CommandText += " where " + strWhere;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                return;
            }

            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = this.D.CurrentPageIndex * D.PageSize + 1;
            info.nEndNumber = (this.D.CurrentPageIndex + 1) * D.PageSize;

            DataTable dt = new DataTable();
            int nRecCount = 0;
            info.LoadData(da.SelectCommand, Global.GetConnection(), dt, ref nRecCount);


            if (0 == nRecCount)
            {
                L.Visible = false;
                lblMsg.Text = "データがありません。";
                return;
            }
            if (0 == dt.Rows.Count)
            {
                L.Visible = false;
                this.D.CurrentPageIndex = 0;
                lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";	// データはあるか取得ページがおかしい
                return;
            }

            this.D.VirtualItemCount = nRecCount;

            int nPageSize = this.D.PageSize;
            int nPageCount = nRecCount / nPageSize;
            if (0 < nRecCount % nPageSize) nPageCount++;
            if (nPageCount <= this.D.CurrentPageIndex) this.D.MasterTableView.CurrentPageIndex = 0;

            this.D.DataSource = dt;

            v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

            this.D.DataBind();
        }

        private void UserView_DataBound(UserViewManager.UserViewEventArgs e)
        {
            if (e.ItemType != UserViewManager.EnumItemType.DataRow) return;

        }

        protected void D_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (!(Telerik.Web.UI.GridItemType.Item == e.Item.ItemType ||
                    Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType))
                return;

            Button btnEdit = e.Item.FindControl("E") as Button;
            string strSyouhin = Convert.ToString((e.Item.DataItem as DataRowView).Row["SyouhinMei"]);
            string strCate = Convert.ToString((e.Item.DataItem as DataRowView).Row["CategoryName"]);
            //string strBaitai = Convert.ToString((e.Item.DataItem as DataRowView).Row["KeitaiMei"]);
            string strShiire = Convert.ToString((e.Item.DataItem as DataRowView).Row["ShiireName"]);

            //Button E = e.Item.FindControl("E") as Button;
            HtmlInputCheckBox E = e.Item.FindControl("E") as HtmlInputCheckBox;
            //E.Attributes["onclick"] = string.Format("CntRow('{0}')", strSyouhin + "/" + strCate);
            E.Value = strSyouhin + "/" + strCate + "/" + strShiire;
        }

        protected void D_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void D_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            D.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            this.Create();
        }

        protected void D_PreRender(object sender, EventArgs e)
        {
            this.D.MasterTableView.Attributes["bordercolor"] = "white";
            this.D.MasterTableView.Attributes["border"] = "1";
            this.D.MasterTableView.HeaderStyle.CssClass = "radgrid_header_def";
        }

        string strKeys = "";
        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            int nData = 0;

            for (int i = 0; i < this.D.Items.Count; i++)
            {
                HtmlInputCheckBox E =
                   (HtmlInputCheckBox)this.D.Items[i].Cells[D.Columns.FindByUniqueName("SelectButton").OrderIndex].FindControl("E");

                if (E.Checked && E.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += ","; }

                    this.strKeys += E.Value;
                    nData += 1;
                }
            }

            if (strKeys != "")
            {
                Kakaku.Clear();

                Master.Style["display"] = "none";
                Touroku.Style["display"] = "";

                Kakaku.Create(strKeys, nData);
            }
            else
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("チェックしてください。");
                return;
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterKakaku.aspx");
        }

        protected void BtnTouroku_Click(object sender, EventArgs e)
        {
            bool bo = Kakaku.Toroku();

            if (bo)
            {
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";

                Create();

                lblMsg.Text = "登録しました";
            }
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Create();
        }

        protected void CSVdownload_Click(object sender, EventArgs e)
        {
            UserViewManager.UserView v = SessionManager.User.GetUserView(91);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

            string strWhere = "";
            try
            {
                strWhere = this.F.GetFilter(da.SelectCommand);
                if (!string.IsNullOrEmpty(strWhere))
                    da.SelectCommand.CommandText += " where " + strWhere;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                return;
            }
            D.PageSize = 2000;
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = this.D.CurrentPageIndex * D.PageSize + 1;
            info.nEndNumber = (this.D.CurrentPageIndex + 1) * D.PageSize;

            string cmm = da.SelectCommand.CommandText;
            DataTable dt = new DataTable();
            int nRecCount = 0;
            info.LoadData(da.SelectCommand, Global.GetConnection(), dt, ref nRecCount);

            if (0 == dt.Rows.Count)
            {
                lblMsg.Text = "該当データがありません";
                return;
            }

            string strData = v.SqlDataFactory.GetTextData(dt, this.D.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

            string strExt = "csv";
            string strFileName = ("得意先マスタ") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
            Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;


            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                 this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                 Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));


        }

        protected void CSVupload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (Fu.HasFile)
            {
                string filename = Fu.PostedFile.FileName;
                byte[] v = Fu.FileBytes;
                System.IO.Stream c = Fu.FileContent;

                System.IO.StreamReader tabreader = null;
                Core.IO.CSVReader csvreader = null;
                Stream stream = Fu.PostedFile.InputStream;
                Encoding enc = System.Text.Encoding.GetEncoding(932);
                System.IO.StreamReader check = new System.IO.StreamReader(stream, enc);
                string strCheck = check.ReadLine();
                if (strCheck == null)
                {
                    return;
                }
                bool bTab = (strCheck.Split('\t').Length > strCheck.Split(',').Length);
                if (bTab)
                {
                    tabreader = new System.IO.StreamReader(stream, enc);
                }
                else
                {
                    csvreader = new Core.IO.CSVReader(stream, enc);
                }

                int nLine = 0;
                int RowCount = 0;
                while (true)
                {
                    RowCount++;
                    try
                    {
                        nLine++;
                        DataSet1.M_TokuisakiDataTable dtN = new DataSet1.M_TokuisakiDataTable();
                        DataSet1.M_TokuisakiRow drN = dtN.NewM_TokuisakiRow();
                        string strArray2 = check.ReadLine();
                        if (strArray2 == null)
                        {
                            break;
                        }
                        string[] str2 = strArray2.Split(',');

                        drN.ItemArray = str2;

                        dtN.AddM_TokuisakiRow(drN);
                        //Class1.UploadTokuisaki(dtN, Global.GetConnection());
                        lblMsg.Text = "データを取り込みました。";
                    }
                    catch 
                    {
                        lblMsg.Text = "データ取込時にエラーが発生しました。";
                    }
                }

            }
        }

        protected void CSVformat_Click(object sender, EventArgs e)
        {
            UserViewManager.UserView v = SessionManager.User.GetUserView(91);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

            string strWhere = "";
            try
            {
                strWhere = this.F.GetFilter(da.SelectCommand);
                if (!string.IsNullOrEmpty(strWhere))
                    da.SelectCommand.CommandText += " where " + strWhere;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                return;
            }
            D.PageSize = 1;
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = this.D.CurrentPageIndex * D.PageSize + 1;
            info.nEndNumber = (this.D.CurrentPageIndex + 1) * D.PageSize;

            string cmm = da.SelectCommand.CommandText;
            DataTable dt = new DataTable();
            int nRecCount = 0;
            info.LoadData(da.SelectCommand, Global.GetConnection(), dt, ref nRecCount);

            if (0 == dt.Rows.Count)
            {
                lblMsg.Text = "該当データがありません";
                return;
            }

            string strData = v.SqlDataFactory.GetTextData(dt, this.D.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

            string strExt = "csv";
            string strFileName = ("UploadCSVFormat") + "." + strExt;
            Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;


            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                 this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                 Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));

        }
    }
}