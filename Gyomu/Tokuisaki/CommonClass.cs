using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gyomu.Tokuisaki
{
    public class CommonClass
    {

        /// <summary>
        /// select文でデータを取得する
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable SelectedTable(string command, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(command, sql);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        /// <summary>
        /// 追加、更新、削除を行う
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        public static void TranSql(string command, SqlConnection sql)
        {
            var da = new SqlCommand(command, sql);
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

                tran.Rollback();
            }
            finally
            {
                sql.Close();
            }
        }

    }
}