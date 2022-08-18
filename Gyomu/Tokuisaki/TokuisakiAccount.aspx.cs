using DLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
M_tokuisaki_account.password,M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.Address1,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo, M_Facility_NewBackup.Code
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.Code = M_tokuisaki_account.Code
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







        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
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
                var code = e.Item.Cells[4].Text;

                MainPanel.Visible = false;
                EditPanel.Visible = true;


                TourokuCreate(ShisetsuNo, code);


            }
        }



        private void TourokuCreate(string ShisetsuNo, string code)
        {



            string sqlCommand = "";

            sqlCommand = $@"
select M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.FacilityNo,M_Facility_NewBackup.Code, M_Facility_NewBackup.FacilityName1,
M_Facility_NewBackup.FacilityName2,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo,M_Facility_NewBackup.CityCode,M_Facility_NewBackup.Address1,M_Facility_NewBackup.Address2,M_tokuisaki_account.mailaddress,M_Facility_NewBackup.Tell,
M_tokuisaki_account.ID, M_tokuisaki_account.password, M_tokuisaki_account.Available   
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.Code = M_tokuisaki_account.Code 
where M_Facility_NewBackup.FacilityNo = '{ShisetsuNo}' and M_Facility_NewBackup.Code = '{code}'";


            //カテゴリコード、施設コード、子コード、施設名1、施設名カナ、担当者、郵便番号、都市コード、住所1、建物名、メールアドレス、電話番号、ID、PW、有効無効
            var row = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());


            for (int i = 0; i < row.Rows.Count; i++)
            {
                EditCheckHidden.Value = "true";

                CategoryDropTouroku.SelectedValue =
                    row.Rows[0].ItemArray[0].ToString();
                ShisetsuCode.Text =
                    row.Rows[0].ItemArray[1].ToString();
                Code.Text =
                    row.Rows[0].ItemArray[2].ToString();
                CompanyText.Text =
                    row.Rows[0].ItemArray[3].ToString();
                CompanykanaText.Text =
                    row.Rows[0].ItemArray[4].ToString();
                TantouText.Text =
                    row.Rows[0].ItemArray[5].ToString();
                PostNumberText.Text =
                    row.Rows[0].ItemArray[6].ToString();
                CityCode.Text =
                    row.Rows[0].ItemArray[7].ToString();
                CityText.Text =
                    row.Rows[0].ItemArray[8].ToString();
                AddressText2.Text =
                    row.Rows[0].ItemArray[9].ToString();
                PhoneNumberText.Text =
                    row.Rows[0].ItemArray[11].ToString();
                mailText.Text =
                    row.Rows[0].ItemArray[10].ToString();

                IDText.Text = row.Rows[0].ItemArray[12].ToString();
                PWText.Text = row.Rows[0].ItemArray[13].ToString();

                AvailableDrop.SelectedValue = row.Rows[0].ItemArray[14].ToString();



            }

        }













        protected void NewTouroku_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            EditPanel.Visible = true;

            EditCheckHidden.Value = "false";



            CategoryDropTouroku.SelectedValue = "203";
            ShisetsuCode.Text = "";
            Code.Text = "";
            CompanyText.Text = "";
            CompanykanaText.Text = "";
            TantouText.Text = "";
            PostNumberText.Text = "";
            CityCode.Text = "";
            CityText.Text = "";
            AddressText2.Text = "";
            PhoneNumberText.Text = "";
            mailText.Text = "";
            PWText.Text = "";
            IDText.Text = "";
            AvailableDrop.SelectedValue = "True";
        }













        /// <summary>
        /// 新規アカウント作成ボタンからの登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TourokuButton_Click(object sender, EventArgs e)
        {
            //参照元
            //0カテゴリコード,1施設No,2コード,3事業所名,4ジギョウショメイ,5御担当者名,6郵便番号,7住所,8建物名,9電話番号,10メールアドレス,11ID,12パスワード,13有効/無効14,CSV
            var word = new string[] { CategoryDropTouroku.SelectedValue, ShisetsuCode.Text, Code.Text, CompanyText.Text, CompanykanaText.Text, TantouText.Text, PostNumberText.Text, CityText.Text, AddressText2.Text, PhoneNumberText.Text, mailText.Text, IDText.Text, PWText.Text, AvailableDrop.SelectedValue, "" };

            UpdateCSV(word);

        }















        private void UpdateCSV(string[] vs)
        {

            string sqlCommand, script;

            //値が正しいのかチェックを行う。
            var dataCheck = CheckData(vs);

            if (!dataCheck)
            {
                script = $"alert('登録に失敗しました。')";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return;
            }


            string[] word = vs;

            //参照元
            //0施設コード、1施設名、2ID、3PW、4メールアドレス、5認証ID、6カテゴリコード、7有効無効、8子コード
            string[] accountMasterStr = new string[] { word[1], word[3], word[11], word[12], word[10], "", word[0], word[13], word[2] };

            string cityCode = "";

            var post = SearchPostNumber(false);

            foreach (var item in post)
            {
                cityCode = item.CityCode;
            }



            //参照元
            //0施設コード、1子コード、2施設名、3施設名カナ、4略称、5担当者、6郵便番号、7住所1、8建物名、9電話番号、10都市コード、11キャパ、12タイトル、13状態、14最終更新ユーザー、15最終更新日
            string[] facilityStr = new string[] { word[1], word[2], word[3], word[4], word[3], word[5], word[6], word[7], word[8], word[9], cityCode, "", "", "", "", "" };

            //施設マスタの重複チェック
            sqlCommand = $"select * from M_Facility_NewBackup where FacilityNo = '{facilityStr[0]}' and Code = '{facilityStr[1]}'";
            var facilityNo = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //アカウントマスタの重複チェック
            sqlCommand = $"select * from M_tokuisaki_account where FacilityNo = '{facilityStr[0]}' and Code = '{facilityStr[1]}'";
            var dataID = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            if (word[14] == "CSV")//CSVの処理
            {

                if (facilityNo.Rows.Count == 0)//既に施設マスタに登録されていない、新規登録
                {

                    AccountMasterInsert(accountMasterStr);
                    FacilityInsert(facilityStr);
                }
                else//施設マスタに登録されている
                {

                    if (dataID.Rows.Count == 0)//アカウントマスタには登録されていない
                    {
                        AccountMasterInsert(accountMasterStr);
                        FacilityUpdate(facilityStr);

                    }
                    else//アカウントマスタにも登録されている
                    {
                        AccountMasterUpdate(accountMasterStr);
                        FacilityUpdate(facilityStr);
                    }
                }
            }
            else//新規登録ページからの処理
            {
                //このフラグで新規登録か登録情報の変更可をチェックしている。
                if (EditCheckHidden.Value == "true")
                {
                    AccountMasterUpdate(accountMasterStr);
                    FacilityUpdate(facilityStr);
                }
                else
                {
                    if (facilityNo.Rows.Count == 0)//既に施設マスタに登録されていない、新規登録
                    {
                        AccountMasterInsert(accountMasterStr);
                        FacilityInsert(facilityStr);
                    }
                    else//施設マスタに登録されている
                    {
                        if (CategoryDropTouroku.SelectedValue == "209")//キッズ・BGV
                        {
                            accountMasterStr[0] = facilityStr[0] = GetFacilityNo().ToString();
                            AccountMasterInsert(accountMasterStr);
                            FacilityInsert(facilityStr);

                        }
                        else if (dataID.Rows.Count == 0)//アカウントマスタには登録されていない、バス
                        {
                            AccountMasterInsert(accountMasterStr);
                            FacilityUpdate(facilityStr);

                        }
                        else//アカウントマスタ二も登録されている、バス
                        {
                            AccountMasterUpdate(accountMasterStr);
                            FacilityUpdate(facilityStr);
                        }
                    }

                }
            }
            //IDのテキストボックスに値を入れる処理
            sqlCommand = $"select ID from M_tokuisaki_account where FacilityNo = '{facilityStr[0]}' and Code = '{facilityStr[1]}'";
            var data = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            IDText.Text = data.Rows[0].ItemArray[0].ToString();


            script = $"alert('登録に成功しました。')";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);




        }









        private bool CheckData(string[] word)
        {
            string script;

            //"0カテゴリコード,1施設No,2コード,3事業所名,4ジギョウショメイ,5御担当者名,6郵便番号,7住所,8建物名,9電話番号,10メールアドレス,11ID,12パスワード,13有効/無効,14CSV判定" + "\r";

            if (string.IsNullOrWhiteSpace(word[1]))
            {
                script = "alert('施設コードに空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }

            if (string.IsNullOrWhiteSpace(word[10]))
            {
                script = "alert('メールアドレスに空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(word[12]))
            {
                script = "alert('パスワードに空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(word[3]))
            {
                script = "alert('事業所名に空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }

            else if (string.IsNullOrWhiteSpace(word[6]))
            {
                script = "alert('郵便番号に空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(word[7]))
            {
                script = "alert('住所に空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }

            else if (string.IsNullOrWhiteSpace(word[9]))
            {
                script = "alert('電話番号に空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }

            else if (string.IsNullOrWhiteSpace(word[5]))
            {
                script = "alert('御担当者名に空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }

            else if (string.IsNullOrWhiteSpace(word[0]))
            {
                script = "alert('カテゴリコードに空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(word[13]))
            {
                script = "alert('有効/無効に空白の行がありました。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
                return false;
            }
            else
            {
                return true;
            }


        }




        private string CreateID(string categoryCode)
        {
            string sqlCommand;

            if (categoryCode == "209") //キッズ・BGV
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

                return "KB" + count;
            }
            else //バス
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

                return "BS" + count;
            }
        }







        private void AccountMasterInsert(string[] word)
        {
            string sqlCommand, newID;

            newID = CreateID(word[6]);

            var accountTable = new Mtokuisaki_account
            {
                FacilityNo =
                word[0],
                FacilityName1 =
                word[1],
                ID =
                newID,
                password =
                word[3],
                mailaddress =
                word[4],
                account_ninshou_ID =
                word[5],
                CategoryCode =
                word[6],
                Available =
                word[7],
                Code =
                word[8]
            };

            sqlCommand = $@"
insert into M_tokuisaki_account
(FacilityNo,FacilityName1,ID,password,mailaddress,account_ninshou_ID,CategoryCode,Available,Code) 
values('{accountTable.FacilityNo}','{accountTable.FacilityName1}','{accountTable.ID}','{accountTable.password}','{accountTable.mailaddress}','{accountTable.account_ninshou_ID}','{accountTable.CategoryCode}','{accountTable.Available}','{accountTable.Code}')";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());

        }








        private void FacilityInsert(string[] word)
        {
            string sqlCommand;

            var facilityTable = new MFacility_New
            {
                FacilityNo =
                word[0],
                FacilityName1 =
                word[2],
                Code =
                word[1],
                FacilityName2 =
                word[3],
                Abbreviation =
                word[2],
                PostNo =
                word[6],
                Address1 =
                word[7],
                Address2 =
                word[8],
                Tell =
                word[9],
                CityCode =
                word[10],
                Capacity =
                "",
                FacilityResponsible =
                word[5],
                Titles =
                "",
                State =
                "1",
                UpDateDay =
                "",
                UpDateUser =
                "",
            };



            sqlCommand = $@"
insert into M_Facility_NewBackup
(FacilityNo,FacilityName1,Code,FacilityName2,Abbreviation,FacilityResponsible,PostNo,Address1,Address2,Tell,CityCode,Capacity,Titles,State,UpDateUser,UpDateDay) 
values('{facilityTable.FacilityNo}','{facilityTable.FacilityName1}','{facilityTable.Code}','{facilityTable.FacilityName2}','{facilityTable.Abbreviation}','{facilityTable.FacilityResponsible}','{facilityTable.PostNo}','{facilityTable.Address1}','{facilityTable.Address2}','{facilityTable.Tell}','{facilityTable.CityCode}','{facilityTable.Capacity}','{facilityTable.Titles}','{facilityTable.State}','{facilityTable.UpDateUser}','{facilityTable.UpDateDay}')";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());



        }








        private void AccountMasterUpdate(string[] word)
        {
            //string sqlCommand;

            //string facilityNo, newID;

            //sqlCommand = $"select FacilityNo from M_Facility_NewBackup where FacilityNo = ''";

            //var dataFacilityNo = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //facilityNo = dataFacilityNo.Rows[0].ItemArray[0].ToString();

            //sqlCommand = $"select ID from M_tokuisaki_account where FacilityNo = ''";

            //var dataID = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //newID = dataID.Rows[0].ItemArray[0].ToString();

            //string[] row = word;



            //0施設コード、1施設名、2ID、3PW、4メールアドレス、5認証ID、6カテゴリコード、7有効無効、8子コード
            var accountTable = new Mtokuisaki_account
            {
                FacilityNo =
                word[0],
                FacilityName1 =
                word[1],
                ID =
                word[2],
                password =
                word[3],
                mailaddress =
                word[4],
                account_ninshou_ID =
                word[5],
                CategoryCode =
                word[6],
                Available =
                word[7],
                Code =
                word[8]
            };

            var connection = Global.GetConnection();


            var da = new SqlCommand("", connection)
            {
                CommandText = @"
update M_tokuisaki_account set FacilityNo = @a0, FacilityName1 = @a1, ID = @a2, password = @a3, mailaddress = @a4, account_ninshou_ID = @a5,CategoryCode = @a6, Available = @a7, Code = @a8 
where FacilityNo = @a0 and Code = @a8
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
            da.Parameters.AddWithValue("@a8", accountTable.Code);


            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                da.Transaction = transaction;
                da.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
        }









        private void FacilityUpdate(string[] word)
        {
            //string sqlCommand = "";

            //string facilityNo;


            //sqlCommand = $"select FacilityNo from M_Facility_NewBackup where FacilityNo = ''";

            //var data = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //facilityNo = data.Rows[0].ItemArray[0].ToString();






            //string[] row = word;

            //0施設コード、1子コード、2施設名、3施設名カナ、4略称、5担当者、6郵便番号、7住所1、8建物名、9電話番号、10都市コード、11キャパ、12タイトル、13状態、14最終更新ユーザー、15最終更新日
            var facilityTable = new MFacility_New
            {
                FacilityNo =
                word[0],
                FacilityName1 =
                word[2],
                Code =
                word[1],
                FacilityName2 =
                word[3],
                Abbreviation =
                word[2],
                PostNo =
                word[6],
                Address1 =
                word[7],
                Address2 =
                word[8],
                Tell =
                word[9],
                CityCode =
                word[10],
                Capacity =
                "",
                FacilityResponsible =
                word[5],
                Titles =
                "",
                State =
                "1",
                UpDateDay =
                "",
                UpDateUser =
                "",
            };



            var connection = Global.GetConnection();

            var da = new SqlCommand("", connection)
            {
                CommandText = @"
update M_Facility_NewBackup 
set FacilityNo = @a0,Code = @a1,FacilityName1 = @a2, FacilityName2 = @a3, Abbreviation = @a4,FacilityResponsible = @a5, PostNo = @a6,Address1 = @a7, Address2 = @a8,Tell = @a9, CityCode = @a10,Capacity = @a11,Titles = @a12,State = @a13,UpDateUser = @a14,UpDateDay= @a15
where FacilityNo = @a0 and Code = @a1
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
            catch (Exception)
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
select M_tokuisaki_account.CategoryCode,M_Facility_NewBackup.FacilityNo,M_Facility_NewBackup.Code,M_Facility_NewBackup.FacilityName1,M_Facility_NewBackup.FacilityName2,M_Facility_NewBackup.FacilityResponsible,M_Facility_NewBackup.PostNo,M_Facility_NewBackup.Address1,M_Facility_NewBackup.Address2,M_Facility_NewBackup.Tell,M_tokuisaki_account.mailaddress,M_tokuisaki_account.ID,M_tokuisaki_account.password,M_tokuisaki_account.Available
from M_tokuisaki_account 
inner join M_Facility_NewBackup 
on M_tokuisaki_account.FacilityNo = M_Facility_NewBackup.FacilityNo and M_Facility_NewBackup.Code = M_tokuisaki_account.Code
";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            string rows = "カテゴリコード,施設No,コード,事業所名,ジギョウショメイ,御担当者名,郵便番号,住所,建物名,電話番号,メールアドレス,ID,パスワード,有効/無効" + "\r";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                rows += $"{table.Rows[i].ItemArray[0]},{table.Rows[i].ItemArray[1]},{table.Rows[i].ItemArray[2]},{table.Rows[i].ItemArray[3]},{table.Rows[i].ItemArray[4]},{table.Rows[i].ItemArray[5]},{table.Rows[i].ItemArray[6]},{table.Rows[i].ItemArray[7]},{table.Rows[i].ItemArray[8]},{table.Rows[i].ItemArray[9]},{table.Rows[i].ItemArray[10]},{table.Rows[i].ItemArray[11]},{table.Rows[i].ItemArray[12]},{table.Rows[i].ItemArray[13]}" + "\r";
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
                        string strLineData = check.ReadLine() + $"{'\t'}CSV";

                        string[] mData = strLineData.Split('\t');

                        //string[] word = new string[] { mData[7], mData[10], mData[1], mData[2], mData[4], mData[5], mData[6], mData[8], "", mData[3], "", mData[0], mData[11] };
                        //"0カテゴリコード,1施設No,2コード,3事業所名,4ジギョウショメイ,5御担当者名,6郵便番号,7住所,8建物名,9電話番号,10メールアドレス,11ID,12パスワード,13有効/無効,14CSV判定" + "\r";



                        UpdateCSV(mData);
                    }
                }
                else
                {

                    while (check.EndOfStream == false)
                    {
                        string strLineData = check.ReadLine() + $"{','}CSV";

                        string[] mData = strLineData.Split(',');



                        UpdateCSV(mData);
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

            sqlCommand = $"select M_Facility_NewBackup.FacilityNo, M_Facility_NewBackup.FacilityName1 from M_Facility_NewBackup inner join M_tokuisaki_account on M_Facility_NewBackup.FacilityNo = M_tokuisaki_account.FacilityNo where M_tokuisaki_account.FacilityNo like '{e.Text.Trim()}%' or M_tokuisaki_account.FacilityName1 like '{e.Text.Trim()}%'";

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

            sqlCommand = $"select PostNo from M_Facility_NewBackup inner join M_tokuisaki_account on M_Facility_NewBackup.FacilityNo = M_tokuisaki_account.FacilityNo where PostNo like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void AddressCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select Address1 from M_Facility_NewBackup inner join M_tokuisaki_account on M_Facility_NewBackup.FacilityNo = M_tokuisaki_account.FacilityNo where Address1 like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void TantouCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select FacilityResponsible from M_Facility_NewBackup inner join M_tokuisaki_account on M_Facility_NewBackup.FacilityNo = M_tokuisaki_account.FacilityNo where FacilityResponsible like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }

        protected void IDCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlCommand;

            sqlCommand = $"select ID from M_tokuisaki_account where ID like '{e.Text.Trim()}%'";

            SetCombo(sender, e, sqlCommand);
        }


















        private bool IsOnlyAlphanumeric3(string text)
        {
            //123-4567
            return Regex.IsMatch(text, @"^[0-9]{3}-[0-9]{4}$");
        }












        private List<Post> SearchPostNumber(bool checkButton)
        {
            var text = PostNumberText.Text.Trim();

            if (!IsOnlyAlphanumeric3(text))
            {
                if (checkButton)
                {
                    string testScript = "alert('半角7桁の数字、半角ハイフンで郵便番号を検索することができます。例）123-4567')";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                    return new List<Post>();
                }
                else
                {
                    return new List<Post>();
                }

            }

            text = text.Replace("-", "");



            //パスを変更
            string filePath = @"C:\Users\yanag\Source\Repos\MMC_CustomerPortal\Document\KEN_ALL.CSV";

            filePath = @"C:\Users\yanag\source\repos\MMC_Hanbai\Gyomu\Tokuisaki\Format\KEN_ALL.CSV";

            // "C:\\inetpub\\wwwroot\\HanshinDw\\hanshin-dw\\"
            filePath = Request.PhysicalApplicationPath + @"Tokuisaki\Format\KEN_ALL.CSV";


            string[] line = File.ReadAllLines(filePath, Encoding.GetEncoding("Shift-JIS"));

            // 配列からリストに格納する
            List<string> lists = new List<string>();

            List<Post> PostLists = new List<Post>();

            foreach (var item in line)
            {
                lists.Add(item);
            }


            foreach (var item in lists)
            {
                string[] strs = item.Split(',', '/', '"', '"');

                var table = new Post
                {
                    CityCode = strs[0],
                    PostNumber = strs[5],
                    Prefecture = strs[17],
                    City = strs[20],
                    Address1 = strs[23],
                };

                PostLists.Add(table);

            }


            if (PostLists.Any(n => n.PostNumber == text))
            {

                var row = PostLists.Where(n => n.PostNumber == text);

                return row.ToList();




            }
            else
            {
                if (checkButton)
                {
                    string testScript = "alert('該当する住所がありませんでした。');";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", testScript, true);
                    return new List<Post>();
                }
                else
                {
                    return new List<Post>();
                }


            }
        }

        protected void SearchPostNumberButton_Click(object sender, EventArgs e)
        {
            var post = SearchPostNumber(true);

            foreach (var item in post)
            {

                CityCode.Text = item.CityCode;

                CityText.Text = item.City + item.Address1;
            }

        }
    }





    public class Post
    {
        public string CityCode { get; set; }
        public string PostNumber { get; set; }
        public string Prefecture { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }

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
        public string Code { get; set; }
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