using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
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

            sqlCommand = "select distinct(SyouhinCode),SyouhinMei from M_Kakaku_2 order by SyouhinCode";

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

                string sqlCommand = "";
                //本番環境のみ

                sqlCommand = $"update T_TokuisakiImage set DeleteFlag = '1' where ShouhinCode = '{ShouhinCode}'";

                //sqlCommand = $"insert into T_TokuisakiImage values('{ShouhinCode}',,0)";



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
            string script = "";

            if (!FileUpload.HasFile)
            {
                script = "alert('アップロードするファイルを選択してください。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return;
            }




            // "C:\\inetpub\\wwwroot\\HanshinDw\\hanshin-dw\\"
            string physical = Request.PhysicalApplicationPath;

            var folderPath = physical + @"Tokuisaki\image\";


            //ファイルの拡張子チェック
            var allowedExtensions = new string[] { ".jpg" };

            for (int i = 0; i < FileUpload.PostedFiles.Count; i++)
            {
                var fileExtension = Path.GetExtension(FileUpload.PostedFiles[i].FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    script = "alert('拡張子は.jpg以外アップロードできません。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    return;
                }

                FileUpload.PostedFiles[i].SaveAs(folderPath + FileUpload.PostedFiles[i].FileName.ToLower());


                var aryData = new byte[FileUpload.PostedFiles[i].ContentLength];

                FileUpload.PostedFiles[i].InputStream.Read(aryData, 0, FileUpload.PostedFiles[i].ContentLength);



                //デバックだったらテーブルに商品コードを登録しない。本番環境のみ有効。


                string file = FileUpload.PostedFiles[i].FileName.Replace(".jpg", "");

                string sqlCommand = $"select * from T_TokuisakiImage where ShouhinCode = '{file}'";

                var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                if (table.Rows.Count == 0)
                {
                    using (MemoryStream ms = new MemoryStream(aryData))
                    {
                        BinaryInsert(file, ms, Global.GetConnection());
                    }
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(aryData))
                    {
                        BinaryUpdate(file, ms, Global.GetConnection());
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

                //sqlCommand += $"SyouhinMei  = '{item[1]}' and ";

                count++;
            }




            //if (!string.IsNullOrWhiteSpace(ShouhinNameCombo.Text))
            //{
            //    sqlCommand += $"SyouhinMei  = '{ShouhinNameCombo.SelectedValue}' and ";
            //    count++;
            //}





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









        //protected void ShouhinNameCombo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        //{
        //    string sqlCommand;

        //    sqlCommand = $"select SyouhinMei from M_TokuisakiShouhin where SyouhinMei like '{e.Text.Trim()}%'";

        //    SetCombo(sender, e, sqlCommand);
        //}





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