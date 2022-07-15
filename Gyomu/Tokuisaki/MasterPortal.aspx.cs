using DLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Tokuisaki
{
    public partial class MasterPortal : System.Web.UI.Page
    {








        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {





                EditPanel.Visible = false;
                Create();
            }

        }

        private void Create()
        {
            string sqlCommand;

            sqlCommand = "select Syouhincode,SyouhinMei,Media,ShouhinNumber,OfficialName from M_TokuisakiShouhin";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainRadGrid.VirtualItemCount = table.Rows.Count;
            int nPageSize = MainRadGrid.PageSize;
            int nPageCount = table.Rows.Count / nPageSize;
            if (0 < table.Rows.Count % nPageSize) nPageCount++;
            if (nPageCount <= MainRadGrid.MasterTableView.CurrentPageIndex) MainRadGrid.MasterTableView.CurrentPageIndex = 0;



            MainRadGrid.DataSource = table;

            MainRadGrid.DataBind();
        }


        private void TourokuCreate(string ShouhinCode)
        {

            string sqlCommand;

            sqlCommand = $"select * from M_TokuisakiShouhin where Syouhincode = '{ShouhinCode}'";

            var row = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());


            for (int i = 0; i < row.Rows.Count; i++)
            {
                EditCheckHidden.Value = "true";
                shouhinCodeHidden.Value = row.Rows[0].ItemArray[0].ToString();
                ShouhinCodeTxt.Text = row.Rows[0].ItemArray[0].ToString();
                shouhinTxt.Text = row.Rows[0].ItemArray[1].ToString();
                MakerTxt.Text = row.Rows[0].ItemArray[2].ToString();
                CatchTxt.Text = row.Rows[0].ItemArray[3].ToString();
                NaiyouTxt.Text = row.Rows[0].ItemArray[4].ToString();
                KantokuTxt.Text = row.Rows[0].ItemArray[5].ToString();
                ActorTxt.Text = row.Rows[0].ItemArray[6].ToString();
                MakerCodeTxt.Text = row.Rows[0].ItemArray[7].ToString();
                ShiyouTxt.Text = row.Rows[0].ItemArray[8].ToString();
                CopyrightTxt.Text = row.Rows[0].ItemArray[9].ToString();
                MediaTxt.Text = row.Rows[0].ItemArray[10].ToString();
                TimeTxt.Text = row.Rows[0].ItemArray[11].ToString();

            }

        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void MainRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Shousai"))
            {

                var ShouhinCode = e.Item.Cells[2].Text;

                EditPanel.Visible = true;
                MainPanel.Visible = false;

                TourokuCreate(ShouhinCode);

            }
        }

        protected void MainRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void MainRadGrid_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            Create();
        }



        protected void NewTouroku_Click(object sender, EventArgs e)
        {
            EditPanel.Visible = true;
            MainPanel.Visible = false;
            EditCheckHidden.Value = "false";
            shouhinCodeHidden.Value = "";
            ShouhinCodeTxt.Text = "";
            shouhinTxt.Text = "";
            MakerTxt.Text = "";
            CatchTxt.Text = "";
            NaiyouTxt.Text = "";
            KantokuTxt.Text = "";
            ActorTxt.Text = "";
            MakerCodeTxt.Text = "";
            ShiyouTxt.Text = "";
            CopyrightTxt.Text = "";
            MediaTxt.Text = "";
            TimeTxt.Text = "";
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            EditPanel.Visible = false;
            MainPanel.Visible = true;
            Create();
        }







        protected void ShouhinCodeCombo_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select Syouhincode, SyouhinMei from M_TokuisakiShouhin where Syouhincode like '{e.Text.Trim()}%' or SyouhinMei like '{e.Text.Trim()}%'";

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
                    items.Add(table.Rows[i].ItemArray[0].ToString());
                }
            }

            foreach (var additems in items)
            {
                rad.Items.Add(new RadComboBoxItem(additems, additems));
            }

        }

        //protected void ShouhinNameCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    string sqlCommand;

        //    sqlCommand = $"select SyouhinMei from M_TokuisakiShouhin where SyouhinMei like '%{e.Text.Trim()}%'";

        //    SetCombo(sender, e, sqlCommand);
        //}

        protected void MakerCodeCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select ShouhinNumber from M_TokuisakiShouhin where ShouhinNumber like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void MakerCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select OfficialName from M_TokuisakiShouhin where OfficialName like '%{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }



        protected void ShouhinCodeComboTouroku_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select Syouhincode, SyouhinMei from M_Kakaku_2 where Syouhincode like '{e.Text.Trim()}%' or SyouhinMei like '{e.Text.Trim()}%'";

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






        protected void SearchButton_Click(object sender, EventArgs e)
        {
            int count = 0;

            string sqlCommand;

            sqlCommand = "select Syouhincode,SyouhinMei,Media,ShouhinNumber,OfficialName from M_TokuisakiShouhin where Syouhincode like '%' and ";



            if (!string.IsNullOrWhiteSpace(ShouhinCodeCombo.Text))
            {
                var item = ShouhinCodeCombo.Text.Split('/');

                sqlCommand += $"SyouhinCode = '{item[0]}' and ";

                //sqlCommand += $"Syouhincode = '{ShouhinCodeCombo.SelectedValue}' and ";
                count++;
            }




            //if (!string.IsNullOrWhiteSpace(ShouhinNameCombo.Text))
            //{
            //    sqlCommand += $"SyouhinMei  = '{ShouhinNameCombo.SelectedValue}' and ";
            //    count++;
            //}




            if (!string.IsNullOrWhiteSpace(MediaDrop.Text))
            {
                sqlCommand += $"Media  = '{MediaDrop.SelectedValue}' and ";
                count++;
            }




            if (!string.IsNullOrWhiteSpace(MakerCodeCombo.Text))
            {
                sqlCommand += $"ShouhinNumber  = '{MakerCodeCombo.SelectedValue}' and ";
                count++;
            }




            if (!string.IsNullOrWhiteSpace(MakerCombo.Text))
            {
                sqlCommand += $"OfficialName  = '{MakerCombo.SelectedValue}' and";
                count++;
            }




            if (count != 0)
            {
                sqlCommand = sqlCommand.Remove(sqlCommand.Length - 4, 4);
            }
            else
            {

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











        protected void TourokuButton_Click(object sender, EventArgs e)
        {


            //商品コード、商品名、メーカー、キャッチコピー、内容、監督、出演、メーカーコード、仕様、コピーライト、メディア、上映時間,商品コードhidden
            var word = new string[] { ShouhinCodeTxt.Text.Trim(), shouhinTxt.Text.Trim(), MakerTxt.Text.Trim(), CatchTxt.Text.Trim(), NaiyouTxt.Text.Trim(), KantokuTxt.Text.Trim(), ActorTxt.Text.Trim(), MakerCodeTxt.Text.Trim(), ShiyouTxt.Text.Trim(), CopyrightTxt.Text.Trim(), MediaTxt.Text.Trim(), TimeTxt.Text.Trim(), shouhinCodeHidden.Value };

            string script;

            if (string.IsNullOrWhiteSpace(word[0]))
            {
                script = "alert('商品コードが未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return;
            }

            if (string.IsNullOrWhiteSpace(word[10]))
            {

                script = "alert('メディアが未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return;
            }



            string sqlCommand;

            //sqlCommand = "select Syouhincode from M_TokuisakiShouhin";

            //var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //var syouhinCodes = new List<string>();

            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    syouhinCodes.Add(table.Rows[i].ItemArray[0].ToString());
            //}






            if (EditCheckHidden.Value == "true")
            {
                sqlCommand = @"
update M_TokuisakiShouhin 
set Syouhincode = @a0, SyouhinMei = @a1, OfficialName = @a2, ShouhinCatch = @a3, ShouhinContents = @a4, MovieManager = @a5, MovieActor = @a6, ShouhinNumber = @a7, ShouhinAttribute = @a8, Copyright = @a9, Media = @a10 , JoueiTime = @a11
where Syouhincode = @a12 
";
                InsertTable(word, sqlCommand);
            }
            else
            {
                sqlCommand = @"insert into M_TokuisakiShouhin values(@a0,@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11);";
                InsertTable(word, sqlCommand);
            }

            script = "alert('登録に成功しました。');";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);

        }







        private void InsertTable(string[] word, string sqlCommand)
        {
            var connection = Global.GetConnection();

            //商品コード、商品名、メーカー、キャッチコピー、内容、監督、出演、メーカーコード、仕様、コピーライト、メディア、上映時間
            var da = new SqlCommand("", connection)
            {
                CommandText = sqlCommand
            };
            da.Parameters.AddWithValue("@a0", word[0]);
            da.Parameters.AddWithValue("@a1", word[1]);
            da.Parameters.AddWithValue("@a2", word[2]);
            da.Parameters.AddWithValue("@a3", word[3]);
            da.Parameters.AddWithValue("@a4", word[4]);
            da.Parameters.AddWithValue("@a5", word[5]);
            da.Parameters.AddWithValue("@a6", word[6]);
            da.Parameters.AddWithValue("@a7", word[7]);
            da.Parameters.AddWithValue("@a8", word[8]);
            da.Parameters.AddWithValue("@a9", word[9]);
            da.Parameters.AddWithValue("@a10", word[10]);
            da.Parameters.AddWithValue("@a11", word[11]);
            da.Parameters.AddWithValue("@a12", word[12]);



            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                da.Transaction = transaction;

                da.ExecuteNonQuery();

                transaction.Commit();


            }
            catch
            {
                transaction.Rollback();

            }
            finally
            {
                connection.Close();

            }

        }









        protected void DownLoadButton_Click(object sender, EventArgs e)
        {
            string sqlCommand = "select * from M_TokuisakiShouhin";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            string rows = "商品コード,商品名,メーカー,キャッチコピー,内容,監督,出演,メーカーコード,仕様,コピーライト,メディア,上映時間" + "\r";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                rows += $"{table.Rows[i].ItemArray[0]},{table.Rows[i].ItemArray[1]},{table.Rows[i].ItemArray[2]},{table.Rows[i].ItemArray[3]},{table.Rows[i].ItemArray[4]},{table.Rows[i].ItemArray[5]},{table.Rows[i].ItemArray[6]},{table.Rows[i].ItemArray[7]},{table.Rows[i].ItemArray[8]},{table.Rows[i].ItemArray[9]},{table.Rows[i].ItemArray[10]},{table.Rows[i].ItemArray[11]}" + "\r";
            }

            string strFileName = ("得意先ポータルマスタ") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + "csv";

            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + Common.DownloadDataForm.GetQueryString4Text(strFileName, rows))));
        }












        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string script = "";

            if (!FileUpload.HasFile)
            {
                script = "alert('アップロードするファイルを選択してください。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                //head_lbl1.Visible = false;
                //MesseageLabel.Visible = true;
                //MesseageLabel.Text = "。";
                return;
            }

            int count = 0;

            Stream s = FileUpload.FileContent;

            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);

            StreamReader check = new StreamReader(s, enc);

            string strCheck = check.ReadLine();





            if (strCheck == null)
            {
                script = "alert('ファイルが読み込めませんでした。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                //head_lbl1.Visible = false;
                //MesseageLabel.Visible = true;
                //MesseageLabel.Text = "";

                return;
            }

            bool bTab = strCheck.Split('\t').Length > strCheck.Split(',').Length;


            try
            {



                if (bTab)
                {
                    while (check.EndOfStream == false)
                    {
                        string strLineData = check.ReadLine();

                        string[] mData = strLineData.Split('\t');

                        var dt = new DataMaster.M_TokuisakiShouhinDataTable();

                        var dr = dt.NewM_TokuisakiShouhinRow();

                        dr.ItemArray = mData;

                        dt.AddM_TokuisakiShouhinRow(dr);

                        UpdateCSVtanto(dt, Global.GetConnection());
                    }
                }
                else
                {
                    while (check.EndOfStream == false)
                    {
                        string strLineData = check.ReadLine();

                        string[] mData = strLineData.Split(',');

                        var dt = new DataMaster.M_TokuisakiShouhinDataTable();

                        var dr = dt.NewM_TokuisakiShouhinRow();

                        dr.ItemArray = mData;

                        dt.AddM_TokuisakiShouhinRow(dr);

                        UpdateCSVtanto(dt, Global.GetConnection());

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                script = $"alert('ファイルのアップロードに失敗しました。{ex.Message}');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
            }




            script = "alert('ファイルのアップロードに成功しました。');";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
            //head_lbl1.Visible = false;
            //MesseageLabel.Visible = true;
            //MesseageLabel.Text = "。";
        }











        private void UpdateCSVtanto(DataMaster.M_TokuisakiShouhinDataTable dt, SqlConnection sqlConnection)
        {

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);

            da.SelectCommand.CommandText = @"
select * from M_TokuisakiShouhin where Syouhincode = @Syouhincode";

            da.SelectCommand.Parameters.AddWithValue("@Syouhincode", dt[0].Syouhincode);


            var dtN = new DataMaster.M_TokuisakiShouhinDataTable();

            da.Fill(dtN);

            SqlTransaction sqlTran = null;

            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    string script = $"alert('ファイルのアップロードに失敗しました。{ex}');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    //head_lbl1.Visible = false;
                    //MesseageLabel.Visible = true;
                    //MesseageLabel.Text = "。";

                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }

        }


    }
}