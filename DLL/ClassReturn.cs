using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassReturn
    {
        public static DataReturn.T_ReturnHeaderDataTable GetReturnHeader(ClassKensaku.KensakuParam k, SqlConnection sqlConnection)
        {
            throw new NotImplementedException();
        }

        public static DataReturn.T_ReturnHeaderRow GetMaxNo(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select ReturnHeaderNo, ReturnFlg from T_ReturnHeader Order by ReturnHeaderNo desc";
            DataReturn.T_ReturnHeaderDataTable dt = new DataReturn.T_ReturnHeaderDataTable();
            da.Fill(dt);
            DataReturn.T_ReturnHeaderRow dr = dt[0];
            return dr;
        }

        public static void InsertHeader(DataReturn.T_ReturnHeaderDataTable dtRh, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_ReturnHeader";
            DataReturn.T_ReturnHeaderDataTable dt = new DataReturn.T_ReturnHeaderDataTable();
            da.Fill(dt);
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            SqlTransaction sqltra = null;
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                DataReturn.T_ReturnHeaderRow dr = dt.NewT_ReturnHeaderRow();
                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                dr.ItemArray = dtRh[0].ItemArray;
                dt.AddT_ReturnHeaderRow(dr);
                da.Update(dt);
                sqltra.Commit();
            }
            catch (Exception ex)
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


        public static void InsertMeisai(DataReturn.T_ReturnDataTable dtR, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Return";
            DataReturn.T_ReturnDataTable dt = new DataReturn.T_ReturnDataTable();
            da.Fill(dt);
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            SqlTransaction sqltra = null;
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                for (int i = 0; i < dtR.Count; i++)
                {
                    DataReturn.T_ReturnRow dr = dt.NewT_ReturnRow();
                    dr.ItemArray = dtR[i].ItemArray;
                    dt.AddT_ReturnRow(dr);
                }
                da.Update(dt);
                sqltra.Commit();
            }
            catch (Exception ex)
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

        public static DataReturn.T_ReturnDataTable GetReturnMeisai(string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Return where ReturnHeaderNo = @no Order by SisetuCode asc";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            DataReturn.T_ReturnDataTable dt = new DataReturn.T_ReturnDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataReturn.T_ReturnHeaderRow SerchHeader(string catecode, string tokucode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_ReturnHeader where TokuisakiCode = @t and CateGory = @c";
            da.SelectCommand.Parameters.AddWithValue("@t", tokucode);
            da.SelectCommand.Parameters.AddWithValue("@c", catecode);
            DataReturn.T_ReturnHeaderDataTable dt = new DataReturn.T_ReturnHeaderDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static void UpdateHeader(string tokucode, string catecode, int su, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_ReturnHeader where TokuisakiCode = @t and CateGory = @c";
            da.SelectCommand.Parameters.AddWithValue("@t", tokucode);
            da.SelectCommand.Parameters.AddWithValue("@c", catecode);
            DataReturn.T_ReturnHeaderDataTable dt = new DataReturn.T_ReturnHeaderDataTable();
            da.Fill(dt);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            SqlTransaction sqltra = null;
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                dt[0].SouSuryou += su;
                da.Update(dt);
                sqltra.Commit();
            }
            catch (Exception ex)
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

        public static void UpdateReturnFlg(string[] str, SqlConnection sqlConnection)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string no = str[i];
                SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
                da.SelectCommand.CommandText =
                    "select * from T_Return where ReturnNo = @no";
                da.SelectCommand.Parameters.AddWithValue("@no", no);
                DataReturn.T_ReturnDataTable dt = new DataReturn.T_ReturnDataTable();
                da.Fill(dt);


                da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
                SqlTransaction sqltra = null;
                try
                {
                    sqlConnection.Open();
                    sqltra = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                    dt[0].ReturnFlg = true;
                    dt[0].JutyuBi = DateTime.Now;
                    da.Update(dt);
                    sqltra.Commit();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public static void UpdateReturnHeader(string[] str, SqlConnection sqlConnection)
        {
            for (int i = 0; i < str.Length; i++)
            {
                string no = str[i];
                SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
                da.SelectCommand.CommandText =
                    "select * from T_Return where ReturnNo = @no";
                da.SelectCommand.Parameters.AddWithValue("@no", no);
                DataReturn.T_ReturnDataTable dt = new DataReturn.T_ReturnDataTable();
                da.Fill(dt);

                string Hno = dt[0].ReturnHeaderNo;
                SqlDataAdapter da2 = new SqlDataAdapter("", sqlConnection);
                da2.SelectCommand.CommandText =
                    "select * from T_Return where ReturnHeaderNo = @Hno and ReturnFlg = 'False'";
                da2.SelectCommand.Parameters.AddWithValue("@Hno", Hno);
                DataReturn.T_ReturnDataTable dtr = new DataReturn.T_ReturnDataTable();
                da2.Fill(dtr);

                if (dtr.Count == 0)
                {
                    SqlDataAdapter da3 = new SqlDataAdapter("", sqlConnection);
                    da3.SelectCommand.CommandText =
                        "select * from T_ReturnHeader where ReturnHeaderNo = @Hno";
                    da3.SelectCommand.Parameters.AddWithValue("@Hno", Hno);
                    DataReturn.T_ReturnHeaderDataTable dtH = new DataReturn.T_ReturnHeaderDataTable();
                    da3.Fill(dtH);

                    da3.UpdateCommand = (new SqlCommandBuilder(da3)).GetUpdateCommand();
                    SqlTransaction sqltra = null;
                    try
                    {
                        sqlConnection.Open();
                        sqltra = sqlConnection.BeginTransaction();
                        da3.SelectCommand.Transaction = da3.UpdateCommand.Transaction = sqltra;
                        for (int j = 0; j < dtH.Count; j++)
                        {
                            dtH[j].ReturnFlg = true;
                        }
                        da3.Update(dtH);
                        sqltra.Commit();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
        }
    }
}
