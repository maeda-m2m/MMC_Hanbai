using Core.Sql;
using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;


namespace DLL
{
    public class ClassUriage
    {
        public static void GetKeijo(string jutyuNo, DataUriage.T_UriageDataTable dt, DataUriage.T_UriageHeaderDataTable Hdt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "Delete T_UriageHeader Where UriageNo = @uNo";
            cmd.Parameters.AddWithValue("@uNo", jutyuNo);

            SqlDataAdapter Hda = new SqlDataAdapter("", sqlConnection);
            Hda.SelectCommand.CommandText = "SELECT * FROM T_UriageHeader";
            Hda.UpdateCommand = (new SqlCommandBuilder(Hda)).GetUpdateCommand();

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * from T_Uriage";
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                Hda.SelectCommand.Transaction = Hda.UpdateCommand.Transaction = sql;
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                cmd.ExecuteNonQuery();

                Hda.Update(Hdt);
                da.Update(dt);

                sql.Commit();
            }
            catch (Exception e)
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static DataUriage.T_UriageDataTable GetUriageLedger(string selectedValue, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Uriage where TokuisakiCode = @cd";
            da.SelectCommand.Parameters.AddWithValue("@cd", selectedValue);
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataUriage.T_UriageDataTable GetUriageMeisai(string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "select * from T_Uriage where UriageNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataUriage.T_UriageDataTable GetUriage(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Uriage Where UriageNo = @uNo";
            da.SelectCommand.Parameters.AddWithValue("@uNo", vsNo);
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            if (dt.Count >= 1)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }


        public static DataUriage.T_UriageHeaderDataTable GetUriageHeader(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_UriageHeader Where UriageNo = @mNo";
            da.SelectCommand.Parameters.AddWithValue("@mNo", vsNo);

            DataUriage.T_UriageHeaderDataTable dt = new DataUriage.T_UriageHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool UpdateUriage(string vsNo, DataUriage.T_UriageDataTable dt, DataUriage.T_UriageHeaderDataTable Hdt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "Delete T_Uriage Where UriageNo = @NO";
            cmd.Parameters.AddWithValue("@NO", vsNo);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM T_Uriage Where UriageNo = @NO";
            da.SelectCommand.Parameters.AddWithValue("@NO", vsNo);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlCommand cmd2 = new SqlCommand("", sqlConnection);
            cmd2.CommandText = "Delete T_UriageHeader Where UriageNo = @NO";
            cmd2.Parameters.AddWithValue("@NO", vsNo);

            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConnection);
            da2.SelectCommand.CommandText = "SELECT * FROM T_UriageHeader Where UriageNo = @NO";
            da2.SelectCommand.Parameters.AddWithValue("@NO", vsNo);
            da2.UpdateCommand = (new SqlCommandBuilder(da2)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = cmd2.Transaction = sql;

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                da2.SelectCommand.Transaction = da2.UpdateCommand.Transaction = sql;


                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();


                da.Update(dt);
                da2.Update(Hdt);

                sql.Commit();

                return false;
            }
            catch (Exception e)
            {
                if (sql != null)
                    sql.Rollback();
                return true;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static bool DelUriage(string vsNo, SqlConnection sqlConnection)
        {
            SqlCommand command = new SqlCommand("", sqlConnection);
            string sql = string.Format("Delete [T_Uriage] Where UriageNo IN ({0})", vsNo);
            command.CommandText = sql;

            SqlCommand command2 = new SqlCommand("", sqlConnection);
            string sql2 = string.Format("Delete [T_UriageHeader] Where UriageNo IN ({0})", vsNo);
            command2.CommandText = sql2;

            try
            {
                sqlConnection.Open();

                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataUriage.T_UriageDataTable GetT_UriageDataTable(string TokuisakiCode, DateTime seikyuDateFrom, DateTime seikyuDateTo, SqlConnection sqlConnection)
        {
            string sql = "SELECT * FROM T_Uriage WHERE KeijoBi BETWEEN @KeijouBiFrom AND @KeijouBiTo AND TokuisakiCode = @TokuisakiCode ORDER BY UriageNo, RowNo";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConnection);
            da.SelectCommand.Parameters.AddWithValue("@TokuisakiCode", TokuisakiCode);
            da.SelectCommand.Parameters.AddWithValue("@KeijouBiFrom", seikyuDateFrom);
            da.SelectCommand.Parameters.AddWithValue("@KeijouBiTo", seikyuDateTo);
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataUriage.T_NyukinDataTable GetT_NyukinDataTable(string TorihikisakiCode, DateTime nyukinDateFrom, DateTime nyukinDateTo, SqlConnection sqlConnection)
        {
            string sql = "SELECT * FROM T_Nyukin WHERE TorihikisakiCode = @TorihikisakiCode AND  NyukinBi BETWEEN @NyukinBiFrom AND @NyukinBiTo ORDER BY UriageKanriCode";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConnection);
            da.SelectCommand.Parameters.AddWithValue("@TorihikisakiCode", TorihikisakiCode);
            da.SelectCommand.Parameters.AddWithValue("@NyukinBiFrom", nyukinDateFrom);
            da.SelectCommand.Parameters.AddWithValue("@NyukinBiTo", nyukinDateTo);
            DataUriage.T_NyukinDataTable dt = new DataUriage.T_NyukinDataTable();
            da.Fill(dt);
            return dt;
        }

        public static Dataset.M_Customer_NewRow M_TokuisakiRow(string sTokuisakiCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Customer_New WHERE CustomerCode = @TokuisakiCode ";
            da.SelectCommand.Parameters.AddWithValue("@TokuisakiCode", sTokuisakiCode);
            Dataset.M_Customer_NewDataTable dt = new Dataset.M_Customer_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt[0];
            else
                return null;
        }

        public static DataUriage.T_NyukinDataTable GetT_NyukinDataTableByKanriCode(string uriageNo, int tOKUISAKI, SqlConnection sqlConnection)
        {
            string sql = "SELECT * FROM T_Nyukin WHERE KanriCode = @KanriCode AND TorihikisakiType = @TorihikisakiType";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConnection);
            da.SelectCommand.Parameters.AddWithValue("@KanriCode", uriageNo);
            da.SelectCommand.Parameters.AddWithValue("@TorihikisakiType", tOKUISAKI);
            DataUriage.T_NyukinDataTable dt = new DataUriage.T_NyukinDataTable();
            da.Fill(dt);
            return dt;
        }

        public static string getKanriCode(SqlConnection sqlConnection)
        {
            //下を確認
            SqlCommand command = new SqlCommand("", sqlConnection);
            command.CommandText = @"Select IsNull(Max(KanriCode),0) From [T_UriageHeader]";

            try
            {
                sqlConnection.Open();

                object obj = command.ExecuteScalar();
                if (null == obj || System.DBNull.Value == obj)
                    return "";

                return (string)obj;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataUriage.T_UriageRow GetT_UriageRow(string UriageNo, SqlConnection sqlCon)
        {
            string sql = "SELECT TOP 1 * FROM T_Uriage WHERE UriageNo = @UriageNo";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
            da.SelectCommand.Parameters.AddWithValue("@UriageNo", UriageNo);
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            if (dt.Count == 0) { return null; }

            return dt[0];
        }

        public static void InsertUriageHeader(string jNo, int no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_JutyuHeader where JutyuNo = @n";
            da.SelectCommand.Parameters.AddWithValue("@n", jNo);
            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);

            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConnection);
            da2.SelectCommand.CommandText =
                "select * from T_Jutyu where JutyuNo = @nn";
            da2.SelectCommand.Parameters.AddWithValue("@nn", jNo);
            DataJutyu.T_JutyuDataTable dtt = new DataJutyu.T_JutyuDataTable();
            da2.Fill(dtt);

            SqlDataAdapter daUH = new SqlDataAdapter("", sqlConnection);
            daUH.SelectCommand.CommandText =
                "select *from T_UriageHeader";
            DataUriage.T_UriageHeaderDataTable dtuh = new DataUriage.T_UriageHeaderDataTable();
            daUH.Fill(dtuh);

            SqlDataAdapter daU = new SqlDataAdapter("", sqlConnection);
            daU.SelectCommand.CommandText =
                "select * from T_Uriage";
            DataUriage.T_UriageDataTable dtu = new DataUriage.T_UriageDataTable();
            daU.Fill(dtu);

            daUH.InsertCommand = new SqlCommandBuilder(daUH).GetInsertCommand();
            daU.InsertCommand = new SqlCommandBuilder(daU).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                DataUriage.T_UriageHeaderDataTable dth = new DataUriage.T_UriageHeaderDataTable();
                DataUriage.T_UriageHeaderRow drh = dth.NewT_UriageHeaderRow();
                DataUriage.T_UriageDataTable ddt = new DataUriage.T_UriageDataTable();

                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                daUH.SelectCommand.Transaction = daUH.InsertCommand.Transaction = sql;
                daU.SelectCommand.Transaction = daU.InsertCommand.Transaction = sql;

                drh.ItemArray = dt[0].ItemArray;
                for (int i = 0; i < dtt.Count; i++)
                {
                    DataUriage.T_UriageRow ddr = ddt.NewT_UriageRow();
                    ddr.ItemArray = dtt[i].ItemArray;
                    ddr.UriageNo = no;
                    ddr.ReturnFlg = false;
                    ddt.AddT_UriageRow(ddr);
                }
                drh.UriageNo = dt[0].JutyuNo;
                drh.UriageNo = no;
                drh.HatyuFlg = false;
                dth.AddT_UriageHeaderRow(drh);
                daUH.Update(dth);
                daU.Update(ddt);
                sql.Commit();
            }
            catch (Exception ex)
            {
                sql.Rollback();
            }
        }

        public static DataUriage.T_UriageHeaderRow GetMaxNo(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_UriageHeader order by UriageNo desc";
            DataUriage.T_UriageHeaderDataTable dt = new DataUriage.T_UriageHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static DataUriage.T_UriageHeaderRow UriHead(string uNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_UriageHeader where UriageNo = @n";
            da.SelectCommand.Parameters.AddWithValue("@n", uNo);
            DataUriage.T_UriageHeaderDataTable dt = new DataUriage.T_UriageHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static void UpdateUriageAndReturn(string uNo, DataReturn.T_ReturnHeaderDataTable rhdt, DataReturn.T_ReturnDataTable rdt, SqlConnection sqlConnection)
        {
            SqlDataAdapter daH = new SqlDataAdapter("", sqlConnection);
            daH.SelectCommand.CommandText =
                "select * from T_ReturnHeader where UriageNo = @no";
            daH.SelectCommand.Parameters.AddWithValue("@no", uNo);
            DataUriage.T_ReturnHeaderDataTable dtH = new DataUriage.T_ReturnHeaderDataTable();
            daH.Fill(dtH);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Return where UriageNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", uNo);
            DataUriage.T_ReturnDataTable dt = new DataUriage.T_ReturnDataTable();
            da.Fill(dt);

            daH.InsertCommand = new SqlCommandBuilder(daH).GetInsertCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();
            daH.UpdateCommand = new SqlCommandBuilder(daH).GetUpdateCommand();
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                DataUriage.T_UriageHeaderDataTable ndth = new DataUriage.T_UriageHeaderDataTable();
                DataUriage.T_UriageHeaderRow ndrh = ndth.NewT_UriageHeaderRow();
                DataUriage.T_UriageDataTable ndt = new DataUriage.T_UriageDataTable();
                if (dtH.Count >= 1)
                {
                    daH.SelectCommand.Transaction = daH.UpdateCommand.Transaction = sql;
                    da.SelectCommand.Transaction = daH.UpdateCommand.Transaction = sql;
                    //ヘッダーの方
                    ndrh.ItemArray = rhdt[0].ItemArray;
                    ndth.AddT_UriageHeaderRow(ndrh);
                    daH.Update(ndth);
                    //明細の方
                    for (int i = 0; rdt.Count > 1; i++)
                    {
                        DataUriage.T_UriageRow dr = ndt.NewT_UriageRow();
                        dr.ItemArray = rdt[i].ItemArray;
                        ndt.AddT_UriageRow(dr);
                    }
                    da.Update(ndt);
                }
                else
                {
                    daH.SelectCommand.Transaction = daH.InsertCommand.Transaction = sql;
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;
                    //ヘッダーの方
                    ndrh.ItemArray = rhdt[0].ItemArray;
                    ndth.AddT_UriageHeaderRow(ndrh);
                    daH.Update(ndth);
                    //明細の方
                    for (int i = 0; rdt.Count > 1; i++)
                    {
                        DataUriage.T_UriageRow dr = ndt.NewT_UriageRow();
                        dr.ItemArray = rdt[i].ItemArray;
                        ndt.AddT_UriageRow(dr);
                    }
                    da.Update(ndt);
                }
                sql.Commit();
            }
            catch (Exception ex)
            {
                sql.Rollback();
            }
        }

        public static void UpdateUriageDate(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Uriage where UriageNo = @no ";
            da.SelectCommand.Parameters.AddWithValue("@no", v);
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);

            SqlTransaction sql = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                for(int i =0; i <dt.Count; i++)
                {
                    dt[i].JutyuBi = DateTime.Now;
                    dt[i].UriageFlg = true;
                }
                da.Update(dt);
                sql.Commit();
            }
            catch (Exception ex)
            {
                sql.Rollback();
            }
        }
    }
}
