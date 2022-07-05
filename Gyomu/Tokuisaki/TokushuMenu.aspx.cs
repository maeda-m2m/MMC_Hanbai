using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Tokuisaki
{
    public partial class TokushuMenu : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                MainPanel.Visible = true;
                SubPanel.Visible = false;

                Create();

                MasterGridCreate();



            }

        }



        private void MasterGridCreate()
        {

            string sqlCommand = "SELECT tokusyu_code,tokusyu_name,CategoryCode FROM [M_tokusyu] order by CategoryCode";

            MainRadGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            MainRadGrid.DataBind();
        }







        private void Create()
        {
            CategoryDrop.SelectedValue = "203";

            string sqlCommand = $"select * from M_tokusyu where CategoryCode = {CategoryDrop.SelectedValue}";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainListView.DataSource = table;
            MainListView.DataBind();


        }









        protected void ConfirmButton_Click(object sender, EventArgs e)
        {

            if (CategoryDrop.SelectedValue == "") return;

            string sqlCommand;



            for (int i = 0; i < MainListView.Items.Count; i++)
            {


                //特集名、特集タイプ、ランキングタイトル、特集詳細、特集コード、カテゴリコード
                string[] shouhin = new string[] {
                    (MainListView.Items[i].FindControl("TokushuNameTxt") as TextBox).Text,
                    (MainListView.Items[i].FindControl("TypeDrop") as DropDownList).SelectedValue,
                    (MainListView.Items[i].FindControl("RankingTxt") as TextBox).Text,
                    (MainListView.Items[i].FindControl("ShousaiTxt") as TextBox).Text,
                    (MainListView.Items[i].FindControl("Hidden") as HiddenField).Value,
                    CategoryDrop.SelectedValue
                };


                sqlCommand = $@"
update M_tokusyu 
set tokusyu_name = '{shouhin[0]}', tokusyu_naiyou = '{shouhin[3]}', TokushuType = '{shouhin[1]}', RankingTitle = '{shouhin[2]}' 
where tokusyu_code = '{shouhin[4]}' and CategoryCode = '{shouhin[5]}'
";


                CommonClass.TranSql(sqlCommand, Global.GetConnection());



            }

            string script = "alert('登録に成功しました。')";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);


        }





        protected void CategoryDrop_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CategoryDrop.SelectedValue == "") return;

            string sqlCommand = $"select * from M_tokusyu where CategoryCode = {CategoryDrop.SelectedValue}";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //table.Rows.RemoveAt(0);



            MainListView.DataSource = table;
            MainListView.DataBind();


            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    if (table.Rows[i].ItemArray[3].ToString() == "")
            //    {

            //    }
            //}


            //for (int i = 0; i < MainListView.Items.Count; i++)
            //{
            //    string[] shouhin = new string[] {
            //    (MainListView.Items[i].FindControl("TokushuNameTxt") as TextBox).Text,
            //    (MainListView.Items[i].FindControl("TypeDrop") as DropDownList).Text,
            //    (MainListView.Items[i].FindControl("RankingTxt") as TextBox).Text,
            //    (MainListView.Items[i].FindControl("ShousaiTxt") as TextBox).Text,
            //    (MainListView.Items[i].FindControl("Hidden") as HiddenField).Value,
            //};

            //    if (shouhin[0] == "")
            //    {
            //        shouhin[1].
            //    }
            //    else
            //    {

            //    }


            //}


        }

        protected void MainListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRow dr = (e.Item.DataItem as DataRowView).Row;

                var dropDown = e.Item.FindControl("TypeDrop") as DropDownList;

                dropDown.SelectedValue = dr["TokushuType"].ToString();
            }
        }

        protected void SubButton_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            SubPanel.Visible = true;
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = true;
            SubPanel.Visible = false;
        }








        protected void MainRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string script;

            if (e.CommandName.Equals("Delete"))
            {

                var a = e.Item.Cells[3].Text;

                var b = e.Item.Cells[5].Text;

                var TokushuID = a + b;

                string physical = Request.PhysicalApplicationPath;

                string path = physical + $@"Tokuisaki\image\TokushuImage\{TokushuID}.jpg";

                if (!File.Exists(path))
                {
                    script = "alert('画像が存在しません。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    MasterGridCreate();
                    return;
                }

                File.Delete(path);

                string sqlCommand = "";


                sqlCommand = $"select * from T_TokushuImage where TokushuID = '{TokushuID}'";

                var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                if (table.Rows.Count == 0)
                {
                    sqlCommand = $"insert into T_TokushuImage values('{TokushuID}',0,1)";
                }
                else
                {
                    sqlCommand = $"update T_TokushuImage set DeleteFlag = '1' where TokushuID = '{TokushuID}'";
                }

                CommonClass.TranSql(sqlCommand, Global.GetConnection());

                script = "alert('画像を削除しました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);

                MasterGridCreate();
            }
        }








        protected void MainRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                var dr = (e.Item.DataItem as DataRowView).Row;

                var a = dr.ItemArray[0].ToString();

                var b = dr.ItemArray[2].ToString();

                var tokushuID = a + b;

                var shouhinImage = e.Item.FindControl("ShouhinImage") as Image;

                string path;


                path = $@"~\Tokuisaki\image\TokushuImage\{tokushuID}.jpg";


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

            var folderPath = physical + @"Tokuisaki\image\TokushuImage\";


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

                string sqlCommand = $"select * from T_TokushuImage where TokushuID = '{file}'";

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
            MasterGridCreate();
        }












        private void BinaryInsert(string TokushuID, MemoryStream stream, SqlConnection sql)
        {
            var da = new SqlCommand("", sql);

            da.CommandText =
               $@"insert into T_TokushuImage values('{TokushuID}',@stream,0)";

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





        private void BinaryUpdate(string TokushuID, MemoryStream stream, SqlConnection sql)
        {
            var da = new SqlCommand("", sql);

            da.CommandText =
               $@"update T_TokushuImage set DeleteFlag = '0',ShouhinBinary = @stream where TokushuID = '{TokushuID}'";

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

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }
    }
}


