using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    class ClassSyohinInp
    {
        public static DataSet1.M_CategoryDataTable GetCategoryDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Category";
            DataSet1.M_CategoryDataTable dt = new DataSet1.M_CategoryDataTable();
            da.Fill(dt);
            return dt;
        }

        internal static DataSet1.M_CategoryRow GetCategoryRow(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Category";
            DataSet1.M_CategoryDataTable dt = new DataSet1.M_CategoryDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_CategoryRow;
            else
                return null;
        }

    }
}
