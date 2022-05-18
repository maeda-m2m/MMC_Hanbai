using System;
using DLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class AccountPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                create();
                create2();
            }
        }

        //1列番号
        //2企業名
        //3支店名
        //4企業名略称
        //5市町村コード
        //6住所1
        //7住所2（ビル名、階数）
        //8代表者名
        //9電話番号
        //10FAX
        //11銀行コード
        //12銀行名
        //13支店コード
        //14支店名（銀行）
        //15口座区分
        //16口座番号 

        private void create2()
        {
            var dt = Get_Account2(Global.GetConnection());
            DG.DataSource = dt;
            DG.DataBind();
        }

        private void create()
        {
            var dt = Get_account(Global.GetConnection());
            var dr = dt.Rows[0] as DataMaster.M_AccountPageRow;
            lbl1.Text = dr.RowNumber.ToString();
            try
            {
                tb4.Text = dr.CompanyName;
                tb7.Text = dr.abbreviation;
                tb2.Text = dr.CityCode;
                tb5.Text = dr.Address;
                tb8.Text = dr.AddressOption;
                tb9.Text = dr.Delegetion;
                tb3.Text = dr.PhoneNumber;
                tb6.Text = dr.FAX;
                tb13.Text = dr.BankCode;
                tb10.Text = dr.BankName;
                tb14.Text = dr.BranchCode;
                tb11.Text = dr.BankBranchName;
                tb12.Text = dr.AccountType;
                tb15.Text = dr.AccountNumber;
                editor_lbl.Text += dr.EditorName;
            }
            catch(Exception ex)
            {
                error_lbl1.Text = ex.Message;
            }
        }

        protected void DG_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var dr = (e.Item.DataItem as DataRowView).Row as DataMaster.M_AccountPageRow;
                Label label = e.Item.FindControl("DG_lbl1") as Label;
                TextBox a1 = e.Item.FindControl("t_tb1") as TextBox;
                TextBox a2 = e.Item.FindControl("t_tb2") as TextBox;
                TextBox a3 = e.Item.FindControl("t_tb3") as TextBox;
                TextBox a4 = e.Item.FindControl("t_tb4") as TextBox;
                TextBox a5 = e.Item.FindControl("t_tb5") as TextBox;
                TextBox a6 = e.Item.FindControl("t_tb6") as TextBox;
                label.Text = dr.RowNumber.ToString();
                try
                {
                    a1.Text = dr.CompanyName;
                    a2.Text = dr.CityCode;
                    a3.Text = dr.Address;
                    a4.Text = dr.AddressOption;
                    a5.Text = dr.PhoneNumber;
                    a6.Text = dr.FAX;
                }
                catch
                {

                }

            }
        }
        protected void DG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                int i2 = 0;
                for (int i = 1; true; i++)
                {
                    if (i != DG.Items.Count + 1)
                    {
                        var dt = new DataMaster.M_AccountPageDataTable();
                        var dr = dt.NewM_AccountPageRow();
                        TextBox a1 = DG.Items[i2].FindControl("t_tb1") as TextBox;
                        TextBox a2 = DG.Items[i2].FindControl("t_tb2") as TextBox;
                        TextBox a3 = DG.Items[i2].FindControl("t_tb3") as TextBox;
                        TextBox a4 = DG.Items[i2].FindControl("t_tb4") as TextBox;
                        TextBox a5 = DG.Items[i2].FindControl("t_tb5") as TextBox;
                        TextBox a6 = DG.Items[i2].FindControl("t_tb6") as TextBox;
                        dr.RowNumber = i;
                        dr.EditorID = i;
                        if (a1.Text != "")
                        {
                            dr.CompanyName = a1.Text;
                        }
                        if (a2.Text != "")
                        {
                            dr.CityCode = a2.Text;
                        }
                        if (a3.Text != "")
                        {
                            dr.Address = a3.Text;
                        }
                        if (a4.Text != "")
                        {
                            dr.AddressOption = a4.Text;
                        }
                        if (a5.Text != "")
                        {
                            dr.PhoneNumber = a5.Text;
                        }
                        if (a6.Text != "")
                        {
                            dr.FAX = a6.Text;
                        }
                        dt.AddM_AccountPageRow(dr);
                        UpDate(dt, Global.GetConnection());
                        i2++;
                    }
                    else if (i == DG.Items.Count + 1)
                    {
                        var dt = new DataMaster.M_AccountPageDataTable();
                        var dr = dt.NewM_AccountPageRow();
                        dr.RowNumber = i;
                        dr.EditorID = i;
                        dr.CompanyName = "";
                        dr.CityCode = "";
                        dr.Address = "";
                        dr.AddressOption = "";
                        dr.PhoneNumber = "";
                        dr.FAX = "";
                        dt.AddM_AccountPageRow(dr);
                        UpDate(dt, Global.GetConnection());
                        break;
                    }
                }
                create2();
            }
            catch (Exception ex)
            {
                error_lbl1.Text = ex.ToString();
            }
        }
        protected void DG_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int Delete = e.Item.ItemIndex;
                var dt = Get_account(Global.GetConnection());
                var dr = dt.Rows[Delete + 1] as DataMaster.M_AccountPageRow;
                int sdl = dr.RowNumber;
                if (sdl > 0)
                {
                    DeleteList(sdl, Global.GetConnection());
                }
                create2();
            }
        }
        protected void insert_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int i2 = 0;
                for (int i = 1; i <= DG.Items.Count; i++)
                {
                    var dt = new DataMaster.M_AccountPageDataTable();
                    var dr = dt.NewM_AccountPageRow();
                    TextBox a1 = DG.Items[i2].FindControl("t_tb1") as TextBox;
                    TextBox a2 = DG.Items[i2].FindControl("t_tb2") as TextBox;
                    TextBox a3 = DG.Items[i2].FindControl("t_tb3") as TextBox;
                    TextBox a4 = DG.Items[i2].FindControl("t_tb4") as TextBox;
                    TextBox a5 = DG.Items[i2].FindControl("t_tb5") as TextBox;
                    TextBox a6 = DG.Items[i2].FindControl("t_tb6") as TextBox;
                    string a7 = SessionManager.User.UserID.ToString();
                    string a8 = SessionManager.User.UserName;
                    int id = int.Parse(a7);
                    dr.RowNumber = i;
                    dr.EditorID = id;
                    dr.EditorName = a8;

                    if (a1.Text != "")
                    {
                        dr.CompanyName = a1.Text;
                    }
                    if (a2.Text != "")
                    {
                        dr.CityCode = a2.Text;
                    }
                    if (a3.Text != "")
                    {
                        dr.Address = a3.Text;
                    }
                    if (a4.Text != "")
                    {
                        dr.AddressOption = a4.Text;
                    }
                    if (a5.Text != "")
                    {
                        dr.PhoneNumber = a5.Text;
                    }
                    if (a6.Text != "")
                    {
                        dr.FAX = a6.Text;
                    }
                    dt.AddM_AccountPageRow(dr);
                    UpDate(dt, Global.GetConnection());
                    i2++;
                }

                //ここから下の処理が本店の情報
                //----------------------------------------------------------------------------------------------------------
                var dt2 = Get_account(Global.GetConnection());
                var dr1 = dt2.Rows[0] as DataMaster.M_AccountPageRow;
                string b1 = SessionManager.User.UserID.ToString();
                string b2 = SessionManager.User.UserName;
                int id2 = int.Parse(b1);
                dr1.RowNumber = 0;
                dr1.CompanyName = tb4.Text;
                dr1.abbreviation = tb7.Text;
                dr1.CityCode = tb2.Text;
                dr1.Address = tb5.Text;
                dr1.AddressOption = tb8.Text;
                dr1.Delegetion = tb9.Text;
                dr1.PhoneNumber = tb3.Text;
                dr1.FAX = tb6.Text;
                dr1.BankCode = tb13.Text;
                dr1.BankName = tb10.Text;
                dr1.BranchCode = tb14.Text;
                dr1.BankBranchName = tb11.Text;
                dr1.AccountType = tb12.Text;
                dr1.AccountNumber = tb15.Text;
                dr1.EditorID = id2;
                dr1.EditorName = b2;
                update_account(dr1, Global.GetConnection());
            }
            catch (Exception ex)
            {
                error_lbl1.Text = ex.ToString();
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        //Class一覧
        //--------------------------------------------------------------------------------------------------------------

        //databindに使う
        public static DataMaster.M_AccountPageDataTable Get_account(SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_AccountPage order by RowNumber asc";
            var dt = new DataMaster.M_AccountPageDataTable();
            da.Fill(dt);
            return dt;
        }

        //databindに使う(rowが0より大きい)
        public static DataMaster.M_AccountPageDataTable Get_Account2(SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
               "SELECT * FROM M_AccountPage WHERE RowNumber > 0";
            var dt = new DataMaster.M_AccountPageDataTable();
            da.Fill(dt);
            return dt;
        }
        //updateに使う
        public static DataMaster.M_AccountPageRow update_account(DataMaster.M_AccountPageRow dr1, SqlConnection sql)
        {
            {
                var a = new SqlCommand("", sql);
                a.CommandText =
                 "UPDATE M_AccountPage SET [CompanyName] = @companyname, " +
                 "[abbreviation] = @abbreviation," +
                 "[CityCode] = @citycode," +
                 "[Address] = @address," +
                 "[Addressoption] = @addressoption," +
                 "[Delegetion] = @delegetion," +
                 "[PhoneNumber] = @phonenumber," +
                 "[Fax] = @fax," +
                 "[BankCode] = @bankcode," +
                 "[BankName] = @bankname," +
                 "[BranchCode] = @branchcode," +
                 "[BankBranchName] = @bankbranchname," +
                 "[AccountType] = @accounttype," +
                 "[AccountNumber] = @accountnumber," +
                 "[EditorID] = @editorid," +
                 "[EditorName] = @editorname" +
                 " where [RowNumber] = @row";
                a.Parameters.AddWithValue("@row", dr1.RowNumber);
                a.Parameters.AddWithValue("@companyname", dr1.CompanyName);
                a.Parameters.AddWithValue("@abbreviation", dr1.abbreviation);
                a.Parameters.AddWithValue("@citycode", dr1.CityCode);
                a.Parameters.AddWithValue("@address", dr1.Address);
                a.Parameters.AddWithValue("@addressoption", dr1.AddressOption);
                a.Parameters.AddWithValue("@delegetion", dr1.Delegetion);
                a.Parameters.AddWithValue("@phonenumber", dr1.PhoneNumber);
                a.Parameters.AddWithValue("@fax", dr1.FAX);
                a.Parameters.AddWithValue("@bankcode", dr1.BankCode);
                a.Parameters.AddWithValue("@bankname", dr1.BankName);
                a.Parameters.AddWithValue("@branchcode", dr1.BranchCode);
                a.Parameters.AddWithValue("@bankbranchname", dr1.BankBranchName);
                a.Parameters.AddWithValue("@accounttype", dr1.AccountType);
                a.Parameters.AddWithValue("@accountnumber", dr1.AccountNumber);
                a.Parameters.AddWithValue("@editorid", dr1.EditorID);
                a.Parameters.AddWithValue("@editorname", dr1.EditorName);
                try
                {
                    sql.Open();
                    SqlTransaction sqltra = sql.BeginTransaction();
                    a.Transaction = sqltra;
                    a.ExecuteNonQuery();
                    sqltra.Commit();
                }
                finally
                {
                    sql.Close();
                }
                return dr1;
            }
        }
        public static void UpDate(DataMaster.M_AccountPageDataTable dt, SqlConnection sqlConnection)
        {
            string no = dt[0].RowNumber.ToString();
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_AccountPage where RowNumber = @Row";
            da.SelectCommand.Parameters.AddWithValue("@Row", no);
            var dt2 = new DataMaster.M_AccountPageDataTable();
            da.Fill(dt2);
            SqlTransaction sqltra = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                if (dt2.Count == 0)
                {
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                    var dr = dt2.NewM_AccountPageRow();
                    dr.ItemArray = dt[0].ItemArray;
                    dt2.AddM_AccountPageRow(dr);
                    da.Update(dt2);
                    sqltra.Commit();
                }
                else
                {
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                    dt2[0].ItemArray = dt[0].ItemArray;
                    da.Update(dt2);
                    sqltra.Commit();
                }
            }
            catch 
            {
                if (sqltra != null)
                {
                    sqltra.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        //削除ボタン
        public static void DeleteList(int sdl, SqlConnection sql)
        {
            var da = new SqlCommand("", sql);
            da.CommandText =
                "DELETE FROM M_AccountPage " +
                "where [RowNumber] = @row ";
            da.Parameters.AddWithValue("@row", sdl);
            SqlTransaction sqltra = null;
            try
            {
                sql.Open();
                sqltra = sql.BeginTransaction();
                da.Transaction = sqltra;
                da.ExecuteNonQuery();
                sqltra.Commit();
            }
            finally
            {
                sql.Close();
            }
        }
    }
}