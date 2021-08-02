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
    public partial class MasterKakaku : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;
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
                int[] nData = new int[]
                {
                    8, //得意先コード
                    100, //得意先フリガナ
                    100, //得意先名
                    100, //得意先名2
                    50, //得意先略称
                    5, //利用コード
                    2, //利用状態
                    10, //台帳
                    8, //得意先郵便番号
                    50, //得意先住所
                    50, //得意先住所2
                    13, //得意先電話番号
                    13, //得意先FAX
                    100, //ホームページ
                    20, //得意先担当者
                    20, //得意先担当部署
                    20, //得意先担当役職
                    13, //得意先担当電話番号
                    100, //得意先担当メール
                    1, //敬称コード
                    3, //敬称
                    1, //共用コード
                    3, //共用区分
                    1, //個別コード
                    3, //個別管理
                    1, //代理店コード
                    5, //代理店名
                    1, //地域コード
                    5, //地域名
                    1, //県コード
                    5, //県名
                    5, //市町村コード
                    50, //市町村名
                    1, //官民コード
                    5, //官民名
                    1, //売価コード
                    5, //売価
                    3, //掛率
                    3, //売価伝票区分コード
                    5, //売価伝票区分
                    4, //弊社担当コード
                    10, //弊社担当名
                    10, //弊社担当部署
                    1, //弊社カテゴリーコード
                    5, //弊社カテゴリー
                    1, //見積フォームコード
                    7, //見積フォーム
                    1, //納品フォームコード
                    10, //納品フォーム
                    1, //送状フォームコード
                    4, //送状フォーム
                    1, //見積書差出コード
                    5, //見積書差出名
                    1, //納品書差出コード
                    5, //納品書差出名
                    1, //送状差出コード
                    5, //送状差出名
                    1, //納品書発行コード
                    5, //納品書発行
                    8, //請求コード
                    50, //請求先略称
                    1, //締日区分コード
                    3, //締日区分
                    1, //税額通知コード
                    5, //税額通知
                    1, //税込区分コード
                    2, //税込区分
                    1, //売上端数コード
                    5, //売上端数方法
                    1, //売上端数額
                    1, //消費税端数コード
                    5, //消費税端数方法
                    1, //消費税端数額
                    1, // 請求発行額
                    4, //請求発行方法
                    1, //請求元コード
                    12, //請求元名
                    50, //支店名
                    10, //口座タイプ
                    10, //転送請求元番号
                    10, //転送請求送付コード
                    10, //転送請求送付方法
                    1, //請求差出名コード
                    5, //請求差出名
                    1, //与信額
                    1, //休暇回収コード
                    6, //休暇回収指定
                    1, //休暇カレンダーコード
                    10, //休暇カレンダー名
                    1, //会社銀行コード
                    5, //会社銀行名
                    10, //口座番号
                    50, //振込依頼人名
                    10, //更新日
                    10, //更新ユーザー
                    50, //直送先名
                    10, //更新日
                    10, //更新人

                };

                string[] strFieldName = new string[]
                {
                    "得意先コード",
                    "得意先フリガナ",
                    "得意先名",
                    "得意先名2",
                    "得意先略称",
                    "利用コード",
                    "利用状態",
                    "台帳",
                    "得意先郵便番号",
                    "得意先住所",
                    "得意先住所2",
                    "得意先電話番号",
                    "得意先FAX",
                    "ホームページ",
                    "得意先担当者",
                    "得意先担当部署",
                    "得意先担当役職",
                    "得意先担当電話番号",
                    "得意先担当メール",
                    "敬称コード",
                    "敬称",
                    "共用コード",
                    "共用区分",
                    "個別コード",
                    "個別管理",
                    "代理店コード",
                    "代理店名",
                    "地域コード",
                    "地域名",
                    "県コード",
                    "県名",
                    "市町村コード",
                    "市町村名",
                    "官民コード",
                    "官民名",
                    "売価コード",
                    "売価",
                    "掛率",
                    "売価伝票区分コード",
                    "売価伝票区分",
                    "弊社担当コード",
                    "弊社担当名",
                    "弊社担当部署",
                    "弊社カテゴリーコード",
                    "弊社カテゴリー",
                    "請求コード",
                    "請求先略称",
                    "締日区分コード",
                    "締日区分",
                    "税額通知コード",
                    "税額通知",
                    "税込区分コード",
                    "税込区分",
                    "売上端数コード",
                    "売上端数方法",
                    "売上端数額",
                    "消費税端数コード",
                    "消費税端数方法",
                    "消費税端数額",
                    "口座タイプ",
                    "与信額",
                    "口座番号",
                    "振込依頼人名",
                };

                int nLine = 0;
                int RowCount = 0;
                string[] str = null;
                string[] strPrevData = null;

                string tokuCode = "";

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
                    catch (Exception ex)
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