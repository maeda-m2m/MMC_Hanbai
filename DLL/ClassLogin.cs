using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassLogin
    {
        public static DataSet1.M_TantoRow GetLoginData(string sID, string sPass, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto WHERE UserID = @ID AND Password = @Pass and Yuko = 1";
            da.SelectCommand.Parameters.AddWithValue("@ID", sID);
            da.SelectCommand.Parameters.AddWithValue("@Pass", sPass);

            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataSet1.M_TantoRow;
            else
                return null;
        }

        public static DataLogin.M_TantoRow getM_TantoRow(string userID, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto WHERE (UserID = @id) AND Yuko = 1";
            da.SelectCommand.Parameters.AddWithValue("@id", userID);

            DataLogin.M_TantoDataTable dt = new DataLogin.M_TantoDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataLogin.M_TantoRow;
            else
                return null;
        }

        public static void GetLoginlog(string Name ,string UserNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM T_Loginlog  ";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            DataLogin.T_LoginlogDataTable dt = new DataLogin.T_LoginlogDataTable();
            DataLogin.T_LoginlogRow dr = dt.NewT_LoginlogRow();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                dr.UserID = UserNo;
                dr.UserName = Name;
                dr.LoginDate = DateTime.Now;

                dt.AddT_LoginlogRow(dr);

                da.Update(dt);

                sql.Commit();
            }
            catch (Exception e)
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
