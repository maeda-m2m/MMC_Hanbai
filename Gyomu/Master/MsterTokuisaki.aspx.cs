using System;
using DLL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using Yodokou_HanbaiKanri.Common;
using System.IO;
using System.Text;


namespace Gyomu.Master
{
    public partial class MsterTokuisaki : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;

        const int LIST_ID = 50;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.F.Create(LIST_ID, 3);

                Craete();
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
        }

        private void Craete()
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
            //DataSet1.M_Tokuisaki2DataTable dt = Class1.GetTokuisakiAll(Global.GetConnection());
            //nRecCount = dt.Count;
            //string d = info.ToString();
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

            this.D.DataSource = dt;

            v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

            this.D.DataBind();

            this.ShowList(true);

        }

        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.lblMsg.ForeColor = (bErrorMsg) ? Color.Red : Color.Blue;
            this.lblMsg.Text = strMsg;
        }

        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            for (int i = 0; i < this.L.Controls.Count; i++)
                this.L.Controls[i].Visible = bShow;
        }

        protected void D_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (!(Telerik.Web.UI.GridItemType.Item == e.Item.ItemType ||
                Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType))
                return;

            //DataRowView drv = (DataRowView)e.Item.DataItem;
            //DataSet1.M_Tokuisaki2Row dr = (DataSet1.M_Tokuisaki2Row)drv.Row;

            Button btnEdit = e.Item.FindControl("E") as Button;
            string strCode = Convert.ToString((e.Item.DataItem as DataRowView).Row["TokuisakiCode"]);
            string strCust = Convert.ToString((e.Item.DataItem as DataRowView).Row["CustomerCode"]);
            Button E = e.Item.FindControl("E") as Button;
            E.Attributes["onclick"] = string.Format("CntRow('{0}/{1}')", strCode, strCust);

            //Button BtnDoki = e.Item.FindControl("K") as Button;

            Label CustomerCode = e.Item.FindControl("CustomerCode") as Label;
            Label TokuisakiCode = e.Item.FindControl("TokuisakiCode") as Label;
            Label TokuisakiName1 = e.Item.FindControl("TokuisakiName1") as Label;
            Label TokuisakiFurifana = e.Item.FindControl("TokuisakiFurifana") as Label;
            Label TantoStaff = e.Item.FindControl("TantoStaff") as Label;
            Label CityName = e.Item.FindControl("CityName") as Label;

            //CustomerCode.Text = dr.CustomerCode;
            //TokuisakiCode.Text = dr.TokuisakiCode.ToString();
            //TokuisakiName1.Text = dr.TokuisakiName1;
            //if (!dr.IsTokuisakiFurifanaNull())
            //{
            //    TokuisakiFurifana.Text = dr.TokuisakiFurifana;
            //}
            //if (!dr.IsTantoStaffCodeNull())
            //{
            //    string no = dr.TantoStaffCode;
            //    DataMaster.M_Tanto1Row drT = ClassMaster.GetTantoRow2(no, Global.GetConnection());
            //    TantoStaff.Text = drT.UserName;
            //}

            //if (!dr.IsCityCodeNull())
            //{
            //    string no = dr.CityCode;
            //    DataMaster.M_CityRow drC = ClassMaster.GetCity(no, Global.GetConnection());
            //    CityName.Text = drC.CityName;
            //}


        }

        private void UserView_DataBound(UserViewManager.UserViewEventArgs e)
        {
            if (e.ItemType != UserViewManager.EnumItemType.DataRow) return;

            //e.TableCells["Yuko"].Text = (!string.IsNullOrEmpty(e.DataRow["Yuko"].ToString()) && Convert.ToBoolean(e.DataRow["Yuko"].ToString())) ? "有効" : "無効";

        }

        protected void E_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック動作
            string UserId = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

            Tokuisaki.Create(UserId);
        }

        protected void K_Click(object sender, EventArgs e)
        {
            string sValue = count.Value;
            string QueryString = string.Format("Value={0}", sValue);
            this.Response.Redirect("DoukiTokuisaki.aspx?" + "&" + QueryString);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MsterTokuisaki.aspx");
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Tokuisaki.Claer();
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Tokuisaki.Toroku();

            if (bo)
            {
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";

                Craete();

                lblMsg.Text = "登録しました";
            }
        }

        protected void D_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void D_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            D.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            this.Craete();
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Craete();
        }

        protected void CSVdownload_Click(object sender, EventArgs e)
        {
            UserViewManager.UserView v = SessionManager.User.GetUserView(51);
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

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void CSVformat_Click(object sender, EventArgs e)
        {
            UserViewManager.UserView v = SessionManager.User.GetUserView(51);
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
                int[] nData = new int[]
                {
                    1, //顧客コード
                    6, //得意先コード
                    50, //得意先名1
                    50, //得意先名2
                    6, //市町村コード
                    4, //担当営業
                    100, //フリガナ
                    100, //略称
                    10, //〒
                    50, //住所1
                    50, //住所2
                    20, //TEL
                    20, //FAX
                    10, //担当者
                    10, //担当部署名
                    1, //敬称
                    3, //掛率
                    10, //締日
                    10, //税通知
                    10, //税区分
                    2, //銀行
                    8, //口座番号
                    20, //
                };

                string[] strFieldName = new string[]
                {
                    "顧客コード",
                    "得意先コード",
                    "得意先名1",
                    "得意先名2",
                    "市町村コード",
                    "担当営業",
                    "フリガナ",
                    "略称",
                    "〒",
                    "住所1",
                    "住所2",
                    "TEL",
                    "FAX",
                    "担当者",
                    "担当部署名",
                    "敬称",
                    "掛率",
                    "締日",
                    "税通知",
                    "税区分",
                    "銀行",
                    "口座番号",
                   "",
                };

                int nLine = 0;
                int RowCount = 0;
                //string[] str = null;
                //string[] strPrevData = null;

                //string tokuCode = "";

                while (true)
                {
                    RowCount++;
                    try
                    {
                        nLine++;
                        DataSet1.M_Tokuisaki2DataTable dtN = new DataSet1.M_Tokuisaki2DataTable();
                        DataSet1.M_Tokuisaki2Row drN = dtN.NewM_Tokuisaki2Row();
                        string strArray2 = check.ReadLine();
                        if (strArray2 == null)
                        {
                            break;
                        }
                        string[] str2 = strArray2.Split(',');

                        drN.ItemArray = str2;

                        dtN.AddM_Tokuisaki2Row(drN);
                        Class1.UploadTokuisaki(dtN, Global.GetConnection());
                        lblMsg.Text = "データを取り込みました。";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "データ取込時にエラーが発生しました。";
                    }
                }
            }
        }
    }
}