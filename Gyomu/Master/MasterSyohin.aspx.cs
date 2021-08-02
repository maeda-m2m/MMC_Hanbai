using System;
using DLL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.ComponentModel;
using System.Drawing;

namespace Gyomu.Master
{
    public partial class MasterSyohin : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;
        const int LIST_ID = 80;

        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = "";
            if (!this.IsPostBack)
            {
                this.F.Create(LIST_ID, 3);

                Craete(flag);
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
                Touroku2.Style["display"] = "none";
            }
        }

        private void Craete(string flag)
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

            string cmm = da.SelectCommand.CommandText;
            //DataMaster.M_ProductDataTable dd = ClassMaster.GetProduct4(cmm, Global.GetConnection());
            //DataSet1.__図書館価格マスター200720ver3DataTable dd = Class1.Tosyokan(cmm, Global.GetConnection());


            //DataMaster.M_ProductDataTable dt = ClassMaster.GetProduct(Global.GetConnection());
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

            this.D.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
            this.D.DataSource = dt;

            v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

            this.D.DataBind();

            this.ShowList(true);

            //     int nPageSize = this.D.PageSize;//PageSizeは１５
            //     int nPageCount = dt.Count / nPageSize;//全件から１５を割って総ページ数を取得1518
            //     if (0 < dt.Count % nPageSize) nPageCount++;
            //     if (nPageCount <= this.D.CurrentPageIndex) this.D.MasterTableView.CurrentPageIndex = 0;



            for (int j = 0; j < D.Items.Count; j++)
            {
                HtmlInputCheckBox ChkRow =
       (HtmlInputCheckBox)this.D.Items[j].Cells[D.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");
                ChkRow.Checked = false;
            }

            //     DataSet1.T_productDataTable dg = Class1.GetKariProduct(Global.GetConnection());

            //     if (CheckBox2.Checked && dg.Count < 1)
            //     {
            //         DataSet1.T_productDataTable dk = new DataSet1.T_productDataTable();
            //         for (int k = 0; k < dd.Count; k++)
            //         {
            //             DataSet1.T_productRow dr = dk.NewT_productRow();

            //             if (!dd[k].IsSyouhinCodeNull())
            //             { dr.SyouhinCode = dd[k].SyouhinCode.ToString(); }
            //             if (!dd[k].IsShiireMeiNull())
            //             { dr.ShiireMei = dd[k].ShiireMei; }
            //             if (!dd[k].IsMekarnumberNull())
            //             {
            //                 dr.MekarHinban = dd[k].Mekarnumber;
            //             }
            //             if (!dd[k].IsShiireCodeNull())
            //             { dr.ShiireMei = dd[k].ShiireCode.ToString(); }
            //             if (!dd[k].IsHanniNull())
            //             {
            //                 dr.Range = dd[k].Hanni;
            //             }
            //             if (!dd[k].IsMediaNull())
            //             {
            //                 dr.Media = dd[k].Media;
            //             }
            //             dk.AddT_productRow(dr);
            //             Class1.InsertKariProduct(dk, Global.GetConnection());
            //         }

            //         for (int i = 0; i < D.Items.Count; i++)
            //         {
            //             HtmlInputCheckBox ChkRow =
            //                (HtmlInputCheckBox)this.D.Items[i].Cells[D.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

            //             ChkRow.Checked = true;
            //         }
            //         //CheckBox chkb = (CheckBox)D.Items[D.Items.Count].FindControl("CheckBox1");
            //         //chk.Checked = true;
            //         //chkb.Checked = true;
            //         this.D.DataSource = dd;

            //         //v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

            //         this.D.DataBind();
            //     }
            //     else
            //     {
            //         this.D.DataSource = dd;

            //         //v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

            //         this.D.DataBind();
            //     }

            //     if (CheckBox2.Checked)
            //     {
            //         for (int j = 0; j < D.Items.Count; j++)
            //         {
            //             HtmlInputCheckBox ChkRow =
            //    (HtmlInputCheckBox)this.D.Items[j].Cells[D.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");
            //             ChkRow.Checked = true;
            //         }
            //     }
            //     else
            //     {
            //         for (int j = 0; j < D.Items.Count; j++)
            //         {
            //             HtmlInputCheckBox ChkRow =
            //    (HtmlInputCheckBox)this.D.Items[j].Cells[D.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");
            //             ChkRow.Checked = false;
            //         }
            //     }
            //     this.ShowList(true);
        }

        private void AddNerRow(DataSet1.T_productDataTable dk)
        {
            DataSet1.T_productRow dr = dk.NewT_productRow();
            dk.AddT_productRow(dr);
        }

        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            for (int i = 0; i < this.L.Controls.Count; i++)
                this.L.Controls[i].Visible = bShow;
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
            //DataRowView drv = (DataRowView)e.Item.DataItem;
            //DataMitumori.T_MitumoriRow dr = (DataMitumori.T_MitumoriRow)drv.Row;
            //DataMaster.M_ProductRow dr = (DataMaster.M_ProductRow)drv.Row;


            Button btnEdit = e.Item.FindControl("E") as Button;
            string strCode = Convert.ToString((e.Item.DataItem as DataRowView).Row["SyouhinCode"]);

            string strShiire = Convert.ToString((e.Item.DataItem as DataRowView).Row["SyouhinMei"]);
            string strMedia = Convert.ToString((e.Item.DataItem as DataRowView).Row["Media"]);
            string strRange = Convert.ToString((e.Item.DataItem as DataRowView).Row["Hanni"]);


            Button E = e.Item.FindControl("E") as Button;
            E.Attributes["onclick"] = string.Format("CntRow('{0}')", strCode + "/" + strShiire + "/" + strMedia + "/" + strRange);

            HtmlInputCheckBox ChkRow = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;
            ChkRow.Attributes["Checked"] = string.Format("CntRow('{0}')", strCode + "/" + strShiire + "/" + strMedia + "/" + strRange);
            ChkRow.Value = strCode + "/" + strShiire + "/" + strMedia + "/" + strRange;

            //e.Item.Cells[D.Columns.FindByUniqueName("ColSyouhinCode").OrderIndex].Text = dr.SyouhinCode.ToString();
            //e.Item.Cells[D.Columns.FindByUniqueName("ColTitle").OrderIndex].Text = dr.SyouhinMei;
            //e.Item.Cells[D.Columns.FindByUniqueName("ColShiiresakiMei").OrderIndex].Text = dr.ShiireMei;
            //if (!dr.IsHanniNull())
            //{
            //    if (dr.Hanni == "1")
            //    {
            //        e.Item.Cells[D.Columns.FindByUniqueName("ColRange").OrderIndex].Text = "";
            //    }
            //    else
            //    {
            //        e.Item.Cells[D.Columns.FindByUniqueName("ColRange").OrderIndex].Text = dr.Hanni;
            //    }
            //}

            //DataSet1.T_productDataTable dh = Class1.GetKariProduct(Global.GetConnection());
            //if (dh.Count > 0)
            //{
            //    DataSet1.T_productDataTable df = new DataSet1.T_productDataTable();
            //    DataSet1.T_productRow dl = df.NewT_productRow();
            //    string[] arr = ChkRow.Value.Split('/');
            //    dl.SyouhinCode = arr[0];
            //    dl.ShiireMei = arr[1];
            //    dl.Media = arr[2];
            //    dl.checkFLG = ChkRow.Checked;
            //    dl.Range = arr[3];
            //    df.AddT_productRow(dl);
            //    Class1.updateKariProduct(arr[0], arr[1], arr[2], arr[3], ChkRow.Checked, df, Global.GetConnection());
            //    df = null;
            //}
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
            string flag = "";
            D.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            this.Craete(flag);
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            //Syohin.Clear();
            //Master.Style["display"] = "none";
            //Touroku.Style["display"] = "";
            //Touroku2.Style["display"] = "none";
            Response.Redirect("MasterSyohinAdd.aspx");
        }

        string strKeys = "";
        protected void BtnIkkatu_Click(object sender, EventArgs e)
        {
            ////商品メーカーなどを修正させたい場合一括できれば便利なので作成
            int nData = 0;

            for (int i = 0; i < this.D.Items.Count; i++)
            {
                HtmlInputCheckBox ChkRow =
                   (HtmlInputCheckBox)this.D.Items[i].Cells[D.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (ChkRow.Checked && ChkRow.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += ","; }

                    this.strKeys += ChkRow.Value;
                    nData += 1;
                }
            }

            DropDownList komoku = (DropDownList)F.FindControl("I");
            Core.Web.FilterTextBox kensaku = (Core.Web.FilterTextBox)F.FindControl("V");




            if (strKeys != "")
            {
                //一括修正画面に移動
                Syohin.Clear();
                Master.Style["display"] = "none";
                Touroku.Style["display"] = "none";
                Touroku2.Style["display"] = "";

                //未作成(2020/02/03)時点
                //Syohin2.Create(strKeys, nData);
                Syohin2.Create2();
                Class1.DELkariProduct(Global.GetConnection());
            }
            else
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("チェックしてください。");
                return;
            }

            ////仕入先、許諾期間、有効か無効かを一括で更新できるようにする
            ////検索して抽出したデータを一括ボタンを押したときにそのデータを持っていく
            //DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            //for (int i = 0; i < D.Items.Count; i++)
            //{
            //    //DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            //    //dr.SyouhinCode = D.Items[i].FindControl("")
            //}

        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterSyohin.aspx");
        }

        bool _bD_PageSizeChanged = false;

        protected void D_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            string flag = "";
            if (!_bD_PageSizeChanged)
            {

                _bD_PageSizeChanged = true;
                D.MasterTableView.PageSize = e.NewPageSize;
                this.Craete(flag);
            }
        }

        protected void D_PreRender(object sender, EventArgs e)
        {
            this.D.MasterTableView.Attributes["bordercolor"] = "white";
            this.D.MasterTableView.Attributes["border"] = "1";
            this.D.MasterTableView.HeaderStyle.CssClass = "radgrid_header_def";
        }

        protected void E_Click(object sender, EventArgs e)
        {
            string UserId = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
            Syohin.Create(UserId);
        }

        //protected void BtnToroku_Click(object sender, EventArgs e)
        //{
        //    bool bo = Syohin.Toroku();

        //    if (bo == true)
        //    {
        //        Craete();

        //        lblMsg.Text = "登録しました";

        //        Master.Style["display"] = "";
        //        Touroku.Style["display"] = "none";
        //        Touroku2.Style["display"] = "none";
        //    }
        //    else
        //    {
        //        lblMsg.Text = "エラーが発生しました。";

        //        Master.Style["display"] = "";
        //        Touroku.Style["display"] = "none";
        //        Touroku2.Style["display"] = "none";
        //    }
        //}

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            string flag = "";
            lblMsg.Text = "";
            Craete(flag);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void BtnAllSelect_Click(object sender, EventArgs e)
        {
            string flag = "1";
            Craete(flag);
        }

        protected void D_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string co = count.Value;
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            string flag = "1";
            Craete(flag);
        }


        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filename = FileUpload1.PostedFile.FileName;
                byte[] v = FileUpload1.FileBytes;
                System.IO.Stream c = FileUpload1.FileContent;

                System.IO.StreamReader tabreader = null;
                Core.IO.CSVReader csvreader = null;
                Stream stream = FileUpload1.PostedFile.InputStream;
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
                    20, //商品コード
                    80, //商品名
                    20, //メーカー品番
                    10, //メディア
                    10, //範囲
                    10, //倉庫
                    15, //仕入コード
                    30, //仕入名
                    10, //利用状態
                    4, //カテゴリーコード
                    15, //カテゴリー名
                    50, //直送先名
                    10, //標準価格
                    10, //仕入価格
                    10, //キャンペーン価格
                    10, //キャンペーン仕入価格
                    20, //許諾開始
                    20, //許諾終了
                    50, //商品サブ名
                    20, //キャンペーン開始
                    20, //キャンペーン終了
                    20, //作成日
                    20, //作成者
                    10, //素材
                    6, //ジャケット印刷
                    20, //許諾書
                    6, //返却
                    6, //レンタル
                };

                string[] strFieldName = new string[]
                {
                   "商品コード",
                   "商品名",
                   "メーカー品番",
                   "メディア",
                   "範囲",
                   "倉庫",
                   "仕入コード",
                   "仕入名",
                   "利用状態",
                   "カテゴリーコード",
                   "カテゴリー名",
                   "直送先",
                   "標準価格",
                   "仕入価格",
                   "キャンペーン価格",
                   "キャンペーン仕入価格",
                   "許諾開始",
                   "許諾終了",
                   "商品サブ名",
                   "キャンペーン開始",
                   "キャンペーン終了",
                   "作成日",
                   "作成者",
                   "素材",
                   "ジャケットプリント",
                   "許諾書",
                   "返却",
                   "レンタル",
                };

                int nLine = 0;
                int RowCount = 0;
                string[] str = null;
                string[] strPrevData = null;

                string MakerHinban = "";
                string SyouhinCode = "";
                string SyouhinMei = "";
                string Media = "";
                string ShiiresakiMei = "";
                string CategoryName = "";
                string Range = "";

                while (true)
                {
                    RowCount++;
                    try
                    {
                        nLine++;
                        DataSet1.M_Kakaku_2DataTable dtN = new DataSet1.M_Kakaku_2DataTable();
                        DataSet1.M_Kakaku_2Row drN = dtN.NewM_Kakaku_2Row();
                        string strArray2 = check.ReadLine();
                        if (strArray2 == null)
                        {
                            int n = nLine;
                            break;
                        }
                        string[] str2 = strArray2.Split(',');

                        drN.ItemArray = str2;

                        dtN.AddM_Kakaku_2Row(drN);
                        Class1.UploadKakaku(dtN, Global.GetConnection());
                        lblMsg.Text = "データを取り込みました。";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "データ取込時にエラーが発生しました。";
                    }
                }
                //Class1.UploadKakaku(dtN, Global.GetConnection());
            }
        }


        protected void BtnCsvDownload_Click(object sender, EventArgs e)
        {
            UserViewManager.UserView v = SessionManager.User.GetUserView(81);
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

            D.PageSize = 10000;
            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = this.D.CurrentPageIndex * D.PageSize + 1;
            info.nEndNumber = (this.D.CurrentPageIndex + 1) * D.PageSize;

            string cmm = da.SelectCommand.CommandText;
            DataTable dt = new DataTable();
            int nRecCount = 1;
            info.LoadData(da.SelectCommand, Global.GetConnection(), dt, ref nRecCount);

            //if (0 == dt.Rows.Count)
            //{
            //    lblMsg.Text = "該当データがありません";
            //    return;
            //}

            string strData = v.SqlDataFactory.GetTextData(dt, this.D.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

            string strExt = "csv";
            string strFileName = ("商品マスタDL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
            Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                 this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                 Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
        }
    }
}