using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Tokuisaki
{
    public partial class MasterPortalImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {
            string sqlCommand;

            sqlCommand = @"
select distinct(SyouhinCode),SyouhinMei from M_Kakaku_2
where
SyouhinCode <> '10000000' and
SyouhinCode <> '1001' and
SyouhinCode <> '1002' and
SyouhinCode <> '1003' and
SyouhinCode <> '1004' and
SyouhinCode <> '1005' and
SyouhinCode <> '1006' and
SyouhinCode <> '1007'
order by SyouhinCode
";

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
            Create();
        }

        protected void MainRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string script;

            string sqlCommand;

            if (e.CommandName.Equals("Delete"))
            {
                var ShouhinCode = e.Item.Cells[3].Text;

                string physical = Request.PhysicalApplicationPath;

                string path = physical + $@"Tokuisaki\image\{ShouhinCode}.jpg";

                if (!File.Exists(path))
                {
                    script = "alert('画像が存在しません。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    Create();
                    return;
                }

                File.Delete(path);

                sqlCommand = $"select * from T_TokuisakiImage where ShouhinCode = '{ShouhinCode}'";

                var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                if (table.Rows.Count == 0)
                {
                    sqlCommand = $"insert into T_TokuisakiImage values('{ShouhinCode}',0,1)";
                }
                else
                {
                    sqlCommand = $"update T_TokuisakiImage set DeleteFlag = '1' where ShouhinCode = '{ShouhinCode}'";
                }

                CommonClass.TranSql(sqlCommand, Global.GetConnection());

                script = "alert('画像を削除しました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);

                Create();
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
        }

        protected void MainRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                var dr = (e.Item.DataItem as DataRowView).Row;

                var a = dr.ItemArray[0].ToString();

                var shouhinImage = e.Item.FindControl("ShouhinImage") as Image;

                string path;

                string anotherPath;

                //path = $@"~\image\{dr["SyouhinCode"]}_{dr["SyouhinMei"]}.jpg";

                path = $@"~\Tokuisaki\image\{a}.jpg";

                anotherPath = @"~\Tokuisaki\image\Noimage.Jpg";

                if (File.Exists(Server.MapPath(path)))
                {
                    shouhinImage.ImageUrl = path;
                }
                else
                {
                    shouhinImage.AlternateText = "画像がありません。";
                }
            }
        }

        protected void ImageUploadButton_Click(object sender, EventArgs e)
        {
            string script, sqlCommand;

            if (!FileUpload.HasFile)
            {
                script = "alert('アップロードするファイルを選択してください。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return;
            }

            sqlCommand = "select distinct(SyouhinCode) from M_Kakaku_2";

            var shouhincodeData = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            var shouhinCodeLists = new List<string>();

            for (int i2 = 0; i2 < shouhincodeData.Rows.Count; i2++)
            {
                shouhinCodeLists.Add(shouhincodeData.Rows[i2]["SyouhinCode"].ToString());
            }



            //ファイルの拡張子チェック
            //必要な場合はここに拡張子を追加する。
            var allowedExtensions = new string[] { ".jpg" };

            for (int i = 0; i < FileUpload.PostedFiles.Count; i++)
            {
                //ファイルの拡張子を取得する。
                var fileExtension = Path.GetExtension(FileUpload.PostedFiles[i].FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    script = "alert('拡張子は.jpg以外アップロードできません。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    return;
                }

                //拡張子を外したファイル名のみ取得
                string shouhinCode = FileUpload.PostedFiles[i].FileName.Replace(".jpg", "").Replace(".JPG", "");

                //価格マスタに登録されている商品コードかチェックを行う。
                if (!shouhinCodeLists.Contains(shouhinCode))
                {
                    script = "alert('無効なファイル名です。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    return;
                }

                string savefile = ConfigurationManager.AppSettings["MainFilePath"] + @"\" + shouhinCode + fileExtension;

                try
                {
                    FileUpload.PostedFiles[i].SaveAs(savefile);
                }
                catch (Exception ex)
                {
                    string mail_to, title, body, from;

                    mail_to = ConfigurationManager.AppSettings["MailTo"]; ;

                    title = "MMC販売管理画像アップロードメインエラーメール";

                    body = CommonClass.GetErrorInfo(ex);

                    from = "example@example.com";

                    CommonClass.Mail(mail_to, title, body, from);
                }

                string anotherSaveFile = ConfigurationManager.AppSettings["BackupFilePaht"] + @"\" + shouhinCode + fileExtension;

                try
                {
                    FileUpload.PostedFiles[i].SaveAs(anotherSaveFile);
                }
                catch (Exception ex)
                {
                    string mail_to, title, body, from;

                    mail_to = ConfigurationManager.AppSettings["MailTo"]; ;

                    title = "MMC販売管理画像アップロードバックアップエラーメール";

                    body = CommonClass.GetErrorInfo(ex);

                    from = "example@example.com";

                    CommonClass.Mail(mail_to, title, body, from);

                    script = "alert('アップロードに失敗しました。')";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    return;

                }

                var aryData = new byte[FileUpload.PostedFiles[i].ContentLength];

                FileUpload.PostedFiles[i].InputStream.Read(aryData, 0, FileUpload.PostedFiles[i].ContentLength);


                sqlCommand = $"select * from T_TokuisakiImage where ShouhinCode = '{shouhinCode}'";

                var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                using (MemoryStream ms = new MemoryStream(aryData))
                {
                    if (table.Rows.Count == 0)
                    {
                        BinaryInsert(shouhinCode, ms, Global.GetConnection());
                    }
                    else
                    {
                        BinaryUpdate(shouhinCode, ms, Global.GetConnection());
                    }
                }
            }

            script = "alert('アップロードに成功しました。')";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
            Create();
        }

        private void BinaryInsert(string ShouhinCode, MemoryStream stream, SqlConnection sql)
        {
            var da = new SqlCommand("", sql);

            da.CommandText =
               $@"insert into T_TokuisakiImage values('{ShouhinCode}',@stream,0)";

            da.Parameters.AddWithValue("@stream", stream);

            sql.Open();
            var tran = sql.BeginTransaction();
            try
            {
                da.Transaction = tran;
                da.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                tran.Rollback();
            }
            finally
            {
                sql.Close();
            }
        }

        private void BinaryUpdate(string ShouhinCode, MemoryStream stream, SqlConnection sql)
        {
            var da = new SqlCommand("", sql);

            da.CommandText =
               $@"update T_TokuisakiImage set DeleteFlag = '0',ShouhinBinary = @stream where ShouhinCode = '{ShouhinCode}'";

            da.Parameters.AddWithValue("@stream", stream);

            sql.Open();
            var tran = sql.BeginTransaction();
            try
            {
                da.Transaction = tran;
                da.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                tran.Rollback();
            }
            finally
            {
                sql.Close();
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            int count = 0;

            string sqlCommand;

            sqlCommand = "select distinct(SyouhinCode),SyouhinMei from M_Kakaku_2 where Syouhincode like '%' and ";

            if (!string.IsNullOrWhiteSpace(ShouhinCodeCombo.Text))
            {
                var item = ShouhinCodeCombo.Text.Split('/');

                sqlCommand += $"SyouhinCode = '{item[0]}' and ";

                count++;
            }

            if (count != 0)
            {
                sqlCommand = sqlCommand.Remove(sqlCommand.Length - 4, 4);
            }
            else
            {
                Create();
                return;
            }

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainRadGrid.VirtualItemCount = table.Rows.Count;
            int nPageSize = MainRadGrid.PageSize;
            int nPageCount = table.Rows.Count / nPageSize;
            if (0 < table.Rows.Count % nPageSize) nPageCount++;
            if (nPageCount <= MainRadGrid.MasterTableView.CurrentPageIndex) MainRadGrid.MasterTableView.CurrentPageIndex = 0;

            MainRadGrid.DataSource = table;

            MainRadGrid.DataBind();
        }

        protected void ShouhinCodeCombo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select SyouhinCode, SyouhinMei from M_TokuisakiShouhin where SyouhinCode like '{e.Text.Trim()}%' or SyouhinMei like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        private void SetCombo(object sender, RadComboBoxItemsRequestedEventArgs e, string sqlCommand)
        {
            var rad = sender as RadComboBox;

            rad.Items.Clear();

            rad.Items.Add(new RadComboBoxItem("", ""));

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            var items = new List<string>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (!items.Contains(table.Rows[i].ItemArray[0].ToString()))
                {
                    items.Add(table.Rows[i].ItemArray[0].ToString() + "/" + table.Rows[i].ItemArray[1].ToString());
                }
            }

            foreach (var additems in items)
            {
                rad.Items.Add(new RadComboBoxItem(additems, additems));
            }
        }
    }
}