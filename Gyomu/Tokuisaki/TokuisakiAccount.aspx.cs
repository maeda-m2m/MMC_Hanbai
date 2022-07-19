using DLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Tokuisaki
{









    public partial class TokuisakiAccount : System.Web.UI.Page
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

            sqlCommand = @"
select M_Facility_NewBackup.FacilityNo,M_Facility_NewBackup.FacilityName1,M_tokuisaki_account.ID,
M_tokuisaki_account.password,M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.Address1,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.FacilityName1 = M_tokuisaki_account.FacilityName1
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







        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }











        protected void MainRadGrid_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            Create();
        }











        protected void MainRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Shousai"))
            {

                var ShisetsuNo = e.Item.Cells[3].Text;

                MainPanel.Visible = false;
                EditPanel.Visible = true;


                TourokuCreate(ShisetsuNo);


            }
        }



        private void TourokuCreate(string ShisetsuNo)
        {



            string sqlCommand = "";

            sqlCommand = $@"
select M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.FacilityName1,M_Facility_NewBackup.FacilityName2,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo,M_tokuisaki_account.mailaddress,M_Facility_NewBackup.Tell,M_tokuisaki_account.password,M_Facility_NewBackup.FacilityNo, M_tokuisaki_account.ID,M_Facility_NewBackup.Address1 ,M_Facility_NewBackup.Address2, M_tokuisaki_account.Available  
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.FacilityName1 = M_tokuisaki_account.FacilityName1 
where M_Facility_NewBackup.FacilityNo = '{ShisetsuNo}'";


            //カテゴリコード、施設名、シセツメイ、責任者、郵便番号、メールアドレス、電話番号、パスワード、施設コード、ID,住所1、住所2、有効無効
            var row = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());


            for (int i = 0; i < row.Rows.Count; i++)
            {
                EditCheckHidden.Value = "true";

                //ShisetsuCodeHidden.Value = row.Rows[0].ItemArray[8].ToString();
                //AccountIDHidden.Value = row.Rows[0].ItemArray[9].ToString();


                CategoryDropTouroku.SelectedValue = row.Rows[0].ItemArray[0].ToString();
                CompanyText.Text = row.Rows[0].ItemArray[1].ToString();
                CompanykanaText.Text = row.Rows[0].ItemArray[2].ToString();
                TantouText.Text = row.Rows[0].ItemArray[3].ToString();
                PostNumberText.Text = row.Rows[0].ItemArray[4].ToString();
                CityText.Text = row.Rows[0].ItemArray[10].ToString();
                AddressText2.Text = row.Rows[0].ItemArray[11].ToString();

                mailText.Text = row.Rows[0].ItemArray[5].ToString();
                PhoneNumberText.Text = row.Rows[0].ItemArray[6].ToString();

                IDText.Text = row.Rows[0].ItemArray[9].ToString();
                PWText.Text = row.Rows[0].ItemArray[7].ToString();

                AvailableDrop.SelectedValue = row.Rows[0].ItemArray[12].ToString();



            }

        }







        protected void MainRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }












        protected void TourokuButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(CompanyText.Text))
            {

                string testScript = "alert('事業所名が未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }
            if (string.IsNullOrWhiteSpace(TantouText.Text))
            {

                string testScript = "alert('担当者名が未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }
            if (string.IsNullOrWhiteSpace(PostNumberText.Text))
            {

                string testScript = "alert('郵便番号が未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }

            if (string.IsNullOrWhiteSpace(CityText.Text))
            {

                string testScript = "alert('住所が未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }
            if (string.IsNullOrWhiteSpace(mailText.Text))
            {

                string testScript = "alert('メールアドレスが未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }
            if (string.IsNullOrWhiteSpace(PhoneNumberText.Text))
            {

                string testScript = "alert('電話番号が未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }
            if (string.IsNullOrWhiteSpace(PWText.Text))
            {

                string testScript = "alert('パスワードが未入力です。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                return;
            }



            string script, sqlCommand;


            //0メールアドレス、1パスワード、2事業者名、3ジギョウシャ名、4郵便番号、5住所、6建物名、7電話番号、8都市コード、9担当者名、10認証ID、11カテゴリ、12有効無効
            string[] word = new string[] { mailText.Text, PWText.Text, CompanyText.Text, CompanykanaText.Text, PostNumberText.Text, CityText.Text, AddressText2.Text, PhoneNumberText.Text, "", TantouText.Text, "", CategoryDropTouroku.SelectedValue, AvailableDrop.SelectedValue };



            if (EditCheckHidden.Value == "true")
            {

                UpdateTable(word);
                Update(word);
            }
            else
            {
                string facility = ConfigurationManager.AppSettings["M_Facility"];

                sqlCommand = $"select FacilityName1 from {facility}";

                var rows = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                var faciliryLists = new List<string>();


                for (int i = 0; i < rows.Rows.Count; i++)
                {
                    faciliryLists.Add(rows.Rows[i].ItemArray[0].ToString());
                }



                if (faciliryLists.Any(n => n.Contains(CompanyText.Text)))
                {
                    script = "alert('その事業所名は既に登録されています。別の事業所名をご利用ください。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                    return;
                }


                InsertTable(word);

            }

            sqlCommand = $"select ID from M_tokuisaki_account where FacilityName1 = '{word[2]}'";

            var data = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            IDText.Text = data.Rows[0].ItemArray[0].ToString();

            script = $"alert('登録に成功しました。')";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);






        }






        private void InsertTable(string[] word)
        {

            string sqlCommand, newID;


            if (CategoryDropTouroku.SelectedValue == "209") //キッズ・BGV
            {
                sqlCommand = "select ID from M_tokuisaki_account where ID like 'KB%' order by ID desc";

                var ID = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                string oldID;

                if (ID.Rows.Count == 0)
                {
                    oldID = "0";
                }
                else
                {
                    oldID = ID.Rows[0].ItemArray[0].ToString().Replace("KB", "");
                }



                var count = (int.Parse(oldID) + 1).ToString();

                for (; count.Length < 5;)
                {
                    count = count.Insert(0, "0");
                }

                newID = "KB" + count;
            }
            else
            {
                sqlCommand = "select ID from M_tokuisaki_account where ID like 'BS%' order by ID desc";

                var ID = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                string oldID;

                if (ID.Rows.Count == 0)
                {
                    oldID = "0";
                }
                else
                {
                    oldID = ID.Rows[0].ItemArray[0].ToString().Replace("BS", "");
                }

                var count = (int.Parse(oldID) + 1).ToString();

                for (; count.Length < 5;)
                {
                    count = count.Insert(0, "0");
                }

                newID = "BS" + count;
            }




            //二つのテーブルで施設ナンバーを統一する
            int facilityNo = GetFacilityNo();


            //0メールアドレス、1パスワード、2事業所名、3ジギョウショメイ、4郵便番号、5住所、6施設名、7電話番号、8都市コード、9担当者名、10認証番号
            string[] row = word;

            var accountTable = new Mtokuisaki_account
            {
                FacilityNo = facilityNo.ToString(),
                FacilityName1 = row[2],
                ID = newID,
                password = row[1],
                mailaddress = row[0],
                account_ninshou_ID = row[9],
                CategoryCode = CategoryDropTouroku.SelectedValue,
                Available = word[12]
            };

            sqlCommand = $@"
insert into M_tokuisaki_account
(FacilityNo,FacilityName1,ID,password,mailaddress,account_ninshou_ID,CategoryCode,Available) 
values('{accountTable.FacilityNo}','{accountTable.FacilityName1}','{accountTable.ID}','{accountTable.password}','{accountTable.mailaddress}','{accountTable.account_ninshou_ID}','{accountTable.CategoryCode}','{accountTable.Available}')";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());



            var facilityTable = new MFacility_New
            {
                FacilityNo = facilityNo.ToString(),
                FacilityName1 = row[2],
                Code = "1",
                FacilityName2 = row[3],
                Abbreviation = row[2],
                PostNo = row[4],
                Address1 = row[5],
                Address2 = row[6],
                Tell = row[7],
                CityCode = "0",
                Capacity = "",
                FacilityResponsible = row[9],
                Titles = "",
                State = "1",
                UpDateDay = null,
                UpDateUser = null,
            };

            string facility = ConfigurationManager.AppSettings["M_Facility"];

            sqlCommand = $@"
insert into {facility}
(FacilityNo,FacilityName1,Code,FacilityName2,Abbreviation,FacilityResponsible,PostNo,Address1,Address2,Tell,CityCode,Capacity,Titles,State,UpDateUser,UpDateDay) 
values('{facilityTable.FacilityNo}','{facilityTable.FacilityName1}','{facilityTable.Code}','{facilityTable.FacilityName2}','{facilityTable.Abbreviation}','{facilityTable.FacilityResponsible}','{facilityTable.PostNo}','{facilityTable.Address1}','{facilityTable.Address2}','{facilityTable.Tell}','{facilityTable.CityCode}','{facilityTable.Capacity}','{facilityTable.Titles}','{facilityTable.State}','{facilityTable.UpDateUser}','{facilityTable.UpDateDay}')";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());

            //string testScript = $"alert('登録に成功しました。IDは{newID}です。');";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);

        }








        private void UpdateTable(string[] word)
        {
            string sqlCommand = "";

            string facilityNo, newID;




            sqlCommand = $"select FacilityNo from M_Facility_NewBackup where FacilityName1 = '{word[2]}'";

            var dataFacilityNo = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            facilityNo = dataFacilityNo.Rows[0].ItemArray[0].ToString();


            sqlCommand = $"select ID from M_tokuisaki_account where FacilityName1 = '{word[2]}'";

            var dataID = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            newID = dataID.Rows[0].ItemArray[0].ToString();






            string[] row = word;


            var accountTable = new Mtokuisaki_account
            {
                FacilityNo = facilityNo.ToString(),
                FacilityName1 = row[2],
                ID = newID,
                password = row[1],
                mailaddress = row[0],
                account_ninshou_ID = row[9],
                CategoryCode = word[11],
                Available = word[12]
            };

            var connection = Global.GetConnection();


            var da = new SqlCommand("", connection)
            {
                CommandText = @"
update M_tokuisaki_account set FacilityNo = @a0, FacilityName1 = @a1, ID = @a2, password = @a3, mailaddress = @a4, account_ninshou_ID = @a5,CategoryCode = @a6, Available = @a7 where FacilityNo = @a0
"
            };
            da.Parameters.AddWithValue("@a0", accountTable.FacilityNo);
            da.Parameters.AddWithValue("@a1", accountTable.FacilityName1);
            da.Parameters.AddWithValue("@a2", accountTable.ID);
            da.Parameters.AddWithValue("@a3", accountTable.password);
            da.Parameters.AddWithValue("@a4", accountTable.mailaddress);
            da.Parameters.AddWithValue("@a5", accountTable.account_ninshou_ID);
            da.Parameters.AddWithValue("@a6", accountTable.CategoryCode);
            da.Parameters.AddWithValue("@a7", accountTable.Available);




            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                da.Transaction = transaction;

                da.ExecuteNonQuery();

                transaction.Commit();


            }
            catch (Exception ex)
            {
                transaction.Rollback();

            }
            finally
            {
                connection.Close();

            }


        }









        private void Update(string[] word)
        {
            string sqlCommand = "";

            string facilityNo;


            sqlCommand = $"select FacilityNo from M_Facility_NewBackup where FacilityName1 = '{word[2]}'";

            var data = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            facilityNo = data.Rows[0].ItemArray[0].ToString();






            string[] row = word;


            var facilityTable = new MFacility_New
            {
                FacilityNo = facilityNo.ToString(),
                FacilityName1 = row[2],
                Code = "1",
                FacilityName2 = row[3],
                Abbreviation = row[2],
                PostNo = row[4],
                Address1 = row[5],
                Address2 = row[6],
                Tell = row[7],
                CityCode = "0",
                Capacity = "",
                FacilityResponsible = row[9],
                Titles = "",
                State = "1",
                UpDateDay = "",
                UpDateUser = "",
            };

            string facility = ConfigurationManager.AppSettings["M_Facility"];

            var connection = Global.GetConnection();

            var da = new SqlCommand("", connection)
            {
                CommandText = @"
update M_Facility_NewBackup 
set FacilityNo = @a0,Code = @a1,FacilityName1 = @a2, FacilityName2 = @a3, Abbreviation = @a4,FacilityResponsible = @a5, PostNo = @a6,Address1 = @a7, Address2 = @a8,Tell = @a9, CityCode = @a10,Capacity = @a11,Titles = @a12,State = @a13,UpDateUser = @a14,UpDateDay= @a15
where FacilityNo = @a0
"
            };
            da.Parameters.AddWithValue("@a0", facilityTable.FacilityNo);
            da.Parameters.AddWithValue("@a1", facilityTable.Code);
            da.Parameters.AddWithValue("@a2", facilityTable.FacilityName1);
            da.Parameters.AddWithValue("@a3", facilityTable.FacilityName2);
            da.Parameters.AddWithValue("@a4", facilityTable.Abbreviation);
            da.Parameters.AddWithValue("@a5", facilityTable.FacilityResponsible);
            da.Parameters.AddWithValue("@a6", facilityTable.PostNo);
            da.Parameters.AddWithValue("@a7", facilityTable.Address1);
            da.Parameters.AddWithValue("@a8", facilityTable.Address2);
            da.Parameters.AddWithValue("@a9", facilityTable.Tell);
            da.Parameters.AddWithValue("@a10", facilityTable.CityCode);
            da.Parameters.AddWithValue("@a11", facilityTable.Capacity);
            da.Parameters.AddWithValue("@a12", facilityTable.Titles);
            da.Parameters.AddWithValue("@a13", facilityTable.State);
            da.Parameters.AddWithValue("@a14", facilityTable.UpDateUser);
            da.Parameters.AddWithValue("@a15", facilityTable.UpDateDay);






            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                da.Transaction = transaction;

                da.ExecuteNonQuery();

                transaction.Commit();


            }
            catch (Exception ex)
            {
                transaction.Rollback();

            }
            finally
            {
                connection.Close();

            }
        }













        /// <summary>
        /// 施設マスタの施設ナンバーの最大値を取得する処理
        /// </summary>
        /// <returns></returns>
        private int GetFacilityNo()
        {
            string facility = ConfigurationManager.AppSettings["M_Facility"];

            string sqlCommand = $"select FacilityNo from M_Facility_NewBackup order by FacilityNo desc";

            int Number = int.Parse(CommonClass.SelectedTable(sqlCommand, Global.GetConnection()).Rows[0].ItemArray[0].ToString());

            return Number + 1;




        }












        protected void BackButton_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = true;
            EditPanel.Visible = false;
            Create();
        }





















        protected void NewTouroku_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            EditPanel.Visible = true;

            EditCheckHidden.Value = "false";
            //ShisetsuCodeHidden.Value = "";
            //AccountIDHidden.Value = "";

            CategoryDropTouroku.SelectedValue = "203";
            CompanyText.Text = "";
            CompanykanaText.Text = "";
            TantouText.Text = "";
            PostNumberText.Text = "";
            CityText.Text = "";
            AddressText2.Text = "";
            mailText.Text = "";
            PhoneNumberText.Text = "";
            PWText.Text = "";
            IDText.Text = "";
            AvailableDrop.SelectedValue = "True";
        }
















        protected void SearchButton_Click(object sender, EventArgs e)
        {
            int count = 0;

            string sqlCommand;

            sqlCommand = @"
select M_Facility_NewBackup.FacilityNo,M_Facility_NewBackup.FacilityName1,M_tokuisaki_account.ID,
M_tokuisaki_account.password,M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.Address1,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.FacilityName1 = M_tokuisaki_account.FacilityName1 where M_Facility_NewBackup.FacilityNo like '%' and ";



            if (!string.IsNullOrWhiteSpace(CategoryDropSearch.SelectedValue))
            {
                sqlCommand += $"M_tokuisaki_account.CategoryCode  = '{CategoryDropSearch.SelectedValue}' and ";
                count++;
            }


            if (!string.IsNullOrWhiteSpace(ShisetsuNoCombo.Text))
            {
                var item = ShisetsuNoCombo.Text.Split('/');

                sqlCommand += $"M_Facility_NewBackup.FacilityNo = '{item[0]}' and ";

                count++;
            }



            if (!string.IsNullOrWhiteSpace(YuubinCombo.Text))
            {
                sqlCommand += $"M_Facility_NewBackup.PostNo  = '{YuubinCombo.SelectedValue}' and ";
                count++;
            }




            if (!string.IsNullOrWhiteSpace(AddressCombo.Text))
            {
                sqlCommand += $"M_Facility_NewBackup.Address1  = '{AddressCombo.SelectedValue}' and ";
                count++;
            }




            if (!string.IsNullOrWhiteSpace(TantouCombo.Text))
            {
                sqlCommand += $"M_Facility_NewBackup.FacilityResponsible  = '{TantouCombo.SelectedValue}' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(IDCombo.Text))
            {
                sqlCommand += $"M_tokuisaki_account.ID  = '{IDCombo.SelectedValue}' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(AvailableDropSearch.SelectedValue))
            {
                sqlCommand += $"M_tokuisaki_account.Available  = '{AvailableDropSearch.SelectedValue}' and";
                count++;
            }



            if (count != 0)
            {
                sqlCommand = sqlCommand.Remove(sqlCommand.Length - 4, 4);
            }
            else
            {
                string script = "alert('一致するアカウントは見つかりませんでした。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
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













        protected void DownLoadButton_Click(object sender, EventArgs e)
        {
            string sqlCommand = @"
select M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.FacilityName1,M_Facility_NewBackup.FacilityName2,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo,M_Facility_NewBackup.Address1,M_Facility_NewBackup.Address2,M_tokuisaki_account.mailaddress,M_Facility_NewBackup.Tell,M_tokuisaki_account.ID,M_tokuisaki_account.password,M_tokuisaki_account.Available
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.FacilityName1 = M_tokuisaki_account.FacilityName1
";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            string rows = "カテゴリコード,事業所名,ジギョウショメイ,御担当者名,郵便番号,住所,建物名,メールアドレス,電話番号,ID,パスワード,有効/無効" + "\r";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                rows += $"{table.Rows[i].ItemArray[0]},{table.Rows[i].ItemArray[1]},{table.Rows[i].ItemArray[2]},{table.Rows[i].ItemArray[3]},{table.Rows[i].ItemArray[4]},{table.Rows[i].ItemArray[5]},{table.Rows[i].ItemArray[6]},{table.Rows[i].ItemArray[7]},{table.Rows[i].ItemArray[8]},{table.Rows[i].ItemArray[9]},{table.Rows[i].ItemArray[10]},{table.Rows[i].ItemArray[11]}" + "\r";
            }

            string strFileName = ("得意先アカウントマスタ") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + "csv";

            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + Common.DownloadDataForm.GetQueryString4Text(strFileName, rows))));
        }
















        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string script;

            if (!FileUpload.HasFile)
            {
                script = "alert('アップロードするファイルを選択してください。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);

                return;
            }



            Stream s = FileUpload.FileContent;

            Encoding enc = Encoding.GetEncoding(932);

            StreamReader check = new StreamReader(s, enc);

            string strCheck = check.ReadLine();





            if (strCheck == null)
            {
                script = "alert('ファイルが読み込めませんでした。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);

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

                        //0カテゴリ、
                        string[] mData = strLineData.Split('\t');

                        string[] word = new string[] { mData[7], mData[10], mData[1], mData[2], mData[4], mData[5], mData[6], mData[8], "", mData[3], "", mData[0], mData[11] };

                        //0メールアドレス、1パスワード、2事業所名、3ジギョウショメイ、4郵便番号、5住所、6建物名、7電話番号、8都市コード、9担当者名、10認証番号、11カテゴリコード、12有効/無効
                        UpdateCSV(word);
                    }
                }
                else
                {

                    while (check.EndOfStream == false)
                    {
                        string strLineData = check.ReadLine();

                        string[] mData = strLineData.Split(',');

                        string[] word = new string[] { mData[7], mData[10], mData[1], mData[2], mData[4], mData[5], mData[6], mData[8], "", mData[3], "", mData[0], mData[11] };

                        UpdateCSV(word);
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
            Create();

        }









        private void UpdateCSV(string[] word)
        {
            string facility = ConfigurationManager.AppSettings["M_Facility"];

            string sqlCommand = $"select * from M_Facility_NewBackup where FacilityName1 = '{word[2]}'";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());


            if (table.Rows.Count != 0)
            {

                UpdateTable(word);
                Update(word);
            }
            else
            {
                InsertTable(word);

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















        protected void ShisetsuNoCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select FacilityNo, FacilityName1 from M_Facility_NewBackup where FacilityNo like '{e.Text.Trim()}%' or FacilityName1 like '{e.Text.Trim()}%'";

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



        protected void YuubinCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select PostNo from M_Facility_NewBackup where PostNo like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void AddressCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select Address1 from M_Facility_NewBackup where Address1 like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void TantouCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select FacilityResponsible from M_Facility_NewBackup where FacilityResponsible like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void IDCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select ID from M_tokuisaki_account where ID like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }
    }














    public class Mtokuisaki_account
    {
        public string FacilityNo { get; set; }
        public string FacilityName1 { get; set; }
        public string ID { get; set; }
        public string password { get; set; }
        public string mailaddress { get; set; }
        public string account_ninshou_ID { get; set; }
        public string CategoryCode { get; set; }
        public string Available { get; set; }
    }
    public class MFacility_New
    {
        public string FacilityNo { get; set; }
        public string FacilityName1 { get; set; }
        public string Code { get; set; }
        public string FacilityName2 { get; set; }
        public string Abbreviation { get; set; }
        public string FacilityResponsible { get; set; }
        public string PostNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Tell { get; set; }
        public string CityCode { get; set; }
        public string Capacity { get; set; }
        public string Titles { get; set; }
        public string State { get; set; }
        public string UpDateUser { get; set; }
        public string UpDateDay { get; set; }

    }
    //public class Post
    //{
    //    public string CityCode { get; set; }
    //    public string PostNumber { get; set; }
    //    public string Prefecture { get; set; }
    //    public string City { get; set; }
    //    public string Address1 { get; set; }

    //}
}