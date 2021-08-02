using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    class ClassAppropriate
    {
        public static DataAppropriate.T_AppropriateDataTable EditSyousaiDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM V_EditSyousai";
            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            da.Fill(dt);
            return (dt);
        }

    }
}
