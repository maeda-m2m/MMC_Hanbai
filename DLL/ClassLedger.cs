using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DLL
{
    public class ClassLedger
    {
        public static DataLedger.M_Tokuisaki2DataTable GetTokuisakiList(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tokuisaki2 where (TokuisakiRyakusyo like @str) or (TokuisakiCode like @str)";
            da.SelectCommand.Parameters.AddWithValue("@str", "%" + v + "%" );
            DataLedger.M_Tokuisaki2DataTable dt = new DataLedger.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataLedger.T_Nyukin2DataTable GetNyukin2(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_Nyukin2";
            DataLedger.T_Nyukin2DataTable dt = new DataLedger.T_Nyukin2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataLedger.M_ShiiresakiDataTable GetShiiresaki(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Shiiresaki where ShiiresakiRyakusyou like @str or ShiiresakiCode like @str";
            da.SelectCommand.Parameters.AddWithValue("@str", v);
            DataLedger.M_ShiiresakiDataTable dt = new DataLedger.M_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataLedger.T_Nyukin2DataTable GetNyukin(string bankNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_Nyukin2 where KouzaNo like @bn";
            da.SelectCommand.Parameters.AddWithValue("@bn", "%" + bankNo.Trim() + "%");
            DataLedger.T_Nyukin2DataTable dt = new DataLedger.T_Nyukin2DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
