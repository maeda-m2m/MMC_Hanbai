using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassNyukin
    {
        public static string InsertT_NyukinRow(string userID, int type, DataNyukin.T_NyukinRow dr, SqlConnection sqlCon)
        {
            string sqlNyukin = "SELECT * FROM T_Nyukin ";
            SqlDataAdapter daNyukin = new SqlDataAdapter(sqlNyukin, sqlCon);
            daNyukin.InsertCommand = new SqlCommandBuilder(daNyukin).GetInsertCommand();
            DataNyukin.T_NyukinDataTable dtNyukinNew = new DataNyukin.T_NyukinDataTable();
            DataNyukin.T_NyukinRow drNyukinNew = dtNyukinNew.NewT_NyukinRow();
           
            string sqlMaxDenBan = "SELECT ISNULL(MAX(CONVERT(int, UriageKanriCode)),0) FROM T_Nyukin WHERE T_Nyukin.TorihikisakiType = @TorihikisakiType";
            SqlCommand cmdMaxDenBan = new SqlCommand(sqlMaxDenBan, sqlCon);
            cmdMaxDenBan.Parameters.AddWithValue("@TorihikisakiType", type);

            SqlTransaction sqlTran = null;

            string strNewDenBan = "";

            try
            {
                sqlCon.Open();
                sqlTran = sqlCon.BeginTransaction();
                daNyukin.SelectCommand.Transaction = daNyukin.InsertCommand.Transaction = cmdMaxDenBan.Transaction = sqlTran;

                int NewDenBan = Convert.ToInt32(cmdMaxDenBan.ExecuteScalar()) + 1;
                strNewDenBan = NewDenBan.ToString();

                dr.No = NewDenBan;
                dr.UriageKanriCode = strNewDenBan;
                //dr.KanriCode = strNewDenBan;
                dr.TourokuUser = userID;
                dr.TourokuBi = DateTime.Now;

                dtNyukinNew.ImportRow(dr);

                daNyukin.Update(dtNyukinNew);

                sqlTran.Commit();
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                    throw ex;
                }
            }
            finally
            {
                if (sqlCon != null) { sqlCon.Close(); }
            }

            return strNewDenBan;
        }

        public static DataNyukin.T_NyukinRow GetT_NyukinRow(string UriageKanriCode, int nTorihikisakiType, SqlConnection sqlCon)
        {
            string sql = "SELECT * FROM T_Nyukin WHERE UriageKanriCode = @UriageKanriCode AND TorihikisakiType = @TorihikisakiType";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            da.SelectCommand.Parameters.AddWithValue("@UriageKanriCode", UriageKanriCode);
            da.SelectCommand.Parameters.AddWithValue("@TorihikisakiType", nTorihikisakiType);
            DataNyukin.T_NyukinDataTable dt = new DataNyukin.T_NyukinDataTable();
            da.Fill(dt);
            if (dt.Count == 0) { return null; }
            return dt[0];
        }

        public static void UpdateT_NyukinRow(string UserId, DataNyukin.T_NyukinRow dr, SqlTransaction sqlTran)
        {
            string sqlNyukin = "SELECT * FROM T_Nyukin WHERE UriageKanriCode = @UriageKanriCode AND TorihikisakiType = @TorihikisakiType";
            SqlDataAdapter daNyukin = new SqlDataAdapter(sqlNyukin, sqlTran.Connection);
            daNyukin.SelectCommand.Parameters.AddWithValue("@UriageKanriCode", dr.UriageKanriCode);
            daNyukin.SelectCommand.Parameters.AddWithValue("@TorihikisakiType", dr.TorihikisakiType);
            daNyukin.SelectCommand.Transaction = sqlTran;
            daNyukin.UpdateCommand = new SqlCommandBuilder(daNyukin).GetUpdateCommand();
            daNyukin.UpdateCommand.Transaction = sqlTran;
            DataNyukin.T_NyukinDataTable dtThis = new DataNyukin.T_NyukinDataTable();

            daNyukin.Fill(dtThis);

            string mongon = "";
            if (dr.TorihikisakiType == TorihikisakiType.TOKUISAKI)
            {
                mongon = "入金";
            }
            if (dr.TorihikisakiType == TorihikisakiType.SHIRESAKI)
            {
                mongon = "支払";
            }

            if (dtThis.Count == 0)
            {
                throw new Exception(string.Format("{0}伝票データが存在しないため、{0}伝票の更新に失敗しました。", mongon));
            }

            if (dtThis.Count > 1)
            {
                throw new Exception(string.Format("同一の伝番が複数存在するため、{0}伝票の更新に失敗しました。", mongon));
            }

            for (int i = 0; i < dtThis.Columns.Count; i++)
            {
                string colName = dtThis.Columns[i].ColumnName;

                if (colName == "No" || colName == "KanriCode" || colName == "UriageKanriCode" || /*colName == "Year" || colName == "Month" || */colName == "RowNo" || colName == "TorihikisakiType" || colName == "TourokuBi" || colName == "TourokuUser")
                {
                    // 2013/07/17 Year,Month と入金支払日に齟齬が発生し、
                    //            エラーと成る可能性がある為、当該項目を除外
                    continue;   // キー項目と登録日、登録ユーザは更新しない。
                }

                dtThis[0][colName] = dr[colName];
            }

            dtThis[0].KoushinUser = UserId;
            dtThis[0].KoushinBi = DateTime.Now;

            daNyukin.Update(dtThis);
        }

        public static DataNyukin.T_NyukinDataTable GetT_NyukinData(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Nyukin WHERE JyuchuNo = @jNo";
            da.SelectCommand.Parameters.AddWithValue("@jNo", vsNo);

            DataNyukin.T_NyukinDataTable dt = new DataNyukin.T_NyukinDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataNyukin.T_NyukinDataTable getT_NyukinRirekiKensakuTable(string whereText, SqlConnection sql)
        {
            DataNyukin.T_NyukinDataTable dt = new DataNyukin.T_NyukinDataTable();
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText = "select * from T_Nyukin where TorihikisakiType=2";


            if (whereText != "")
            {
                da.SelectCommand.CommandText += " and " + whereText;
            }
            da.Fill(dt);
            return dt;
        }

        public static void SetNyukinRireki(string DenBan, int TorihikisakiType, DateTime dtNow, string strUpdateUserID, string RirekiMsg, SqlTransaction sqlTran)
        {
            string sqlNyukin = "SELECT * FROM T_Nyukin WHERE UriageKanriCode = @DenBan AND TorihikisakiType = @TorihikisakiType";
            SqlDataAdapter daNyukin = new SqlDataAdapter(sqlNyukin, sqlTran.Connection);
            daNyukin.SelectCommand.Transaction = sqlTran;
            daNyukin.SelectCommand.Parameters.AddWithValue("@DenBan", DenBan);
            daNyukin.SelectCommand.Parameters.AddWithValue("@TorihikisakiType", TorihikisakiType);
            DataNyukin.T_Nyukin1DataTable dtNyukin = new DataNyukin.T_Nyukin1DataTable();
            daNyukin.Fill(dtNyukin);

            string sqlRireki = "SELECT * FROM T_Nyukin_Rireki";
            SqlDataAdapter daRireki = new SqlDataAdapter(sqlRireki, sqlTran.Connection);
            daRireki.SelectCommand.Transaction = sqlTran;
            daRireki.InsertCommand = new SqlCommandBuilder(daRireki).GetInsertCommand();
            DataNyukin.T_Nyukin_RirekiDataTable dtRireki = new DataNyukin.T_Nyukin_RirekiDataTable();

            for (int i = 0; i < dtNyukin.Count; i++)
            {
                DataNyukin.T_Nyukin_RirekiRow drRireki = dtRireki.NewT_Nyukin_RirekiRow();

                for (int j = 0; j < dtNyukin.Columns.Count; j++)
                {
                    string ColName = dtNyukin.Columns[j].ColumnName;

                    if (!dtRireki.Columns.Contains(ColName)) { continue; }

                    drRireki[ColName] = dtNyukin[i][ColName];
                }

                drRireki.RirekiTourokuBi = dtNow;
                drRireki.RirekiTourokuUserID = strUpdateUserID;
                drRireki.RirekiMsg = RirekiMsg;

                dtRireki.AddT_Nyukin_RirekiRow(drRireki);

                daRireki.Update(dtRireki);
            }
        }

        public static string get_TokuisakiTenName(string TCode, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText = "select TokuisakiMei from M_Tokuisaki where TokuisakiCode=@c";
            da.SelectCommand.Parameters.AddWithValue("@c", TCode);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0][0].ToString();
            }
            else
                return "";
        }

        public static string get_TantousyaName(string TCode, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText = "select UserName from M_Tanto where UserName=@c";
            da.SelectCommand.Parameters.AddWithValue("@c", TCode);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0][0].ToString();
            }
            else
                return "";
        }

        public static string get_TokuisakiName(string TCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select TokuisakiMei from M_Tokuisaki where TokuisakiCode=@c";
            da.SelectCommand.Parameters.AddWithValue("@c", TCode);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0][0].ToString();
            }
            else
                return "";
        }

        public static int Get_LatestDenBan(int TorihikisakiType, string OrderByText, SqlConnection sqlCon)
        {
            string sql = @"
                SELECT TOP 1 UriageKanriCode 
                FROM (SELECT CONVERT(INT,UriageKanriCode) AS UriageKanriCode, NyukinBi FROM T_Nyukin WHERE TorihikisakiType = @TorihikisakiType) V  
                GROUP BY UriageKanriCode 
                ";

            if (OrderByText.Contains("NyukinBi"))
            {
                sql += " ,NyukinBi ";
            }
            sql += OrderByText;

            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            cmd.Parameters.AddWithValue("@TorihikisakiType", TorihikisakiType);

            try
            {
                sqlCon.Open();

                object o = cmd.ExecuteScalar();
                if (o == DBNull.Value) return -1;
                return Convert.ToInt32(o);
            }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Close();
                }
            }
        }

        public static void DeleteT_NyukinRow(string UriageKanriCode, int TorihikisakiType, SqlTransaction sqlTran)
        {
            string sql = "DELETE FROM T_Nyukin WHERE UriageKanriCode = @UriageKanriCode AND TorihikisakiType = @TorihikisakiType";
            SqlCommand cmd = new SqlCommand(sql, sqlTran.Connection);
            cmd.Transaction = sqlTran;
            cmd.Parameters.AddWithValue("@UriageKanriCode", UriageKanriCode);
            cmd.Parameters.AddWithValue("@TorihikisakiType", TorihikisakiType);

            cmd.ExecuteNonQuery();
        }
    }
}
