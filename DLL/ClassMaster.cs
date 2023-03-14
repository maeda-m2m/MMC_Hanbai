using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace DLL
{
    public class ClassMaster
    {
        public static DataMaster.M_TantoDataTable GetM_Tanto(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto";

            DataMaster.M_TantoDataTable dt = new DataMaster.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.T_OshiraseDataTable GetOshirase(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_Oshirase where accept = '1' Order by CreateDate desc";
            DataMaster.T_OshiraseDataTable dt = new DataMaster.T_OshiraseDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable test(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from V_ProductList";
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_Shiire_NewRow GetShiireCode2(string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Shiire_New where Abbreviation = @abb";
            da.SelectCommand.Parameters.AddWithValue("@abb", text);
            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dt);
            if (dt.Count > 0)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static DataSet1.M_Kakaku_2DataTable SerchSyouhinCode(string pc, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kakaku_2 where SyouhinCode = @sc";
            da.SelectCommand.Parameters.AddWithValue("@sc", pc);
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable SerchSyouhinName(string pn, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kakaku_2 where SyouhinMei = @sm";
            da.SelectCommand.Parameters.AddWithValue("@sm", pn);
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_BumonDataTable GetBumonMaster(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataMaster.M_BumonDataTable dt = new DataMaster.M_BumonDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_JoueiKakakuDataTable GetJoueiList(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_JoueiKakaku order by ShiiresakiCode asc, Capacity asc";
            DataMaster.M_JoueiKakakuDataTable dt = new DataMaster.M_JoueiKakakuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_Tanto1DataTable GetTantoMaster(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_Shiire_NewDataTable GetShiireMaster(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_ProductDataTable GetProduct(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Product where SyouhinMei != '' and SyouhinCode != '' order by SyouhinCode asc";

            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_ProductDataTable GetProduct3(string pn, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Product where SyouhinMei = @pn";
            da.SelectCommand.Parameters.AddWithValue("@pn", pn);

            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable GetKakaku2(string sID, string sName, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kakaku_2 where SyouhinCode = @id and SyouhinMei = @nm ";
            da.SelectCommand.Parameters.AddWithValue("@id", sID);
            da.SelectCommand.Parameters.AddWithValue("@nm", sName);
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.T_ColumnListDataTable GetColumn(int intFieldNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_ColumnList where FieldNo = @vcn order by ColumnNo";
            da.SelectCommand.Parameters.AddWithValue("@vcn", intFieldNo);
            DataMaster.T_ColumnListDataTable dt = new DataMaster.T_ColumnListDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.T_ProductListDataTable GetSyouhinData(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataSet1.T_ProductListDataTable dt = new DataSet1.T_ProductListDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_ProductDataTable GetProduct2(string pc, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Product where SyouhinCode = @pc";
            da.SelectCommand.Parameters.AddWithValue("@pc", pc);

            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_TantoDataTable GetM_Tanto2(string un, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto where UserID = @pc";
            da.SelectCommand.Parameters.AddWithValue("@pc", un);
            DataMaster.M_TantoDataTable dt = new DataMaster.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.T_OshiraseRow GetOshiraseRow(string value, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Oshirase where OshiraseNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", value);
            DataMaster.T_OshiraseDataTable dt = new DataMaster.T_OshiraseDataTable();
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

        public static void InsertLog(string title, string userID, string userName, DateTime now, string logName, bool success, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_Log";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            DataMaster.T_LogDataTable dt = new DataMaster.T_LogDataTable();
            DataMaster.T_LogRow dr = dt.NewT_LogRow();
            SqlTransaction sql = null;
            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                dr.UserID = userID;
                dr.UserName = userName;
                dr.LogDate = now.ToString();
                dr.LogName = logName;
                dr.Success = success.ToString();

                dt.AddT_LogRow(dr);

                da.Update(dt);

                sql.Commit();
            }
            catch (Exception ex)
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void DeleteOshirase(string value, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "Delete  from T_Oshirase where OshiraseNo = @no";
            da.Parameters.AddWithValue("@no", value);
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.Transaction = sql;
                da.ExecuteNonQuery();
                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.V_ProductListDataTable GetList(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from V_ProductList";
            DataMaster.V_ProductListDataTable dt = new DataMaster.V_ProductListDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void CreateProductList(DataMaster.V_ProductListDataTable dtN, SqlConnection sqlConnection)
        {
            SqlCommand sc = new SqlCommand("", sqlConnection);
            sc.CommandText = "insert T_ProductList select * from V_ProductList";
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                sc.Transaction = sql;
                sc.ExecuteNonQuery();
                sql.Commit();
            }
            catch
            {
                if (sql != null)
                {
                    sql.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void DeleteProductList(SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "Delete  from T_ProductList";
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.Transaction = sql;
                da.ExecuteNonQuery();
                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void UpdateCSVtanto(DataMaster.M_Tanto1DataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Tanto where UserID = @u";
            da.SelectCommand.Parameters.AddWithValue("@u", dt[0].UserID);
            DataMaster.M_Tanto1DataTable dtN = new DataMaster.M_Tanto1DataTable();
            da.Fill(dtN);

            SqlTransaction sqlTran = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void UpdateCSVshiire(DataMaster.M_Shiire_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Shiire_New where ShiireCode = @sc";
            da.SelectCommand.Parameters.AddWithValue("@sc", dt[0].ShiireCode);
            DataMaster.M_Shiire_NewDataTable dtN = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dtN);

            SqlTransaction sqlTran = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void UpdateCSVbumon(DataMaster.M_BumonDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Bumon where BumonKubun = @bk ";
            da.SelectCommand.Parameters.AddWithValue("@bk", dt[0].BumonKubun);
            DataMaster.M_BumonDataTable dtN = new DataMaster.M_BumonDataTable();
            da.Fill(dtN);

            SqlTransaction sqlTran = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void UpdateCSVfacility(DataSet1.M_Facility_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Facility_New where FacilityNo = @fn and Code = @c";
            da.SelectCommand.Parameters.AddWithValue("@fn", dt[0].FacilityNo);
            da.SelectCommand.Parameters.AddWithValue("@c", dt[0].Code);
            DataSet1.M_Facility_NewDataTable dtN = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dtN);

            SqlTransaction sqlTran = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void DeleteShiiresaki(string userId, SqlConnection sqlConnection)
        {
            SqlCommand sc = new SqlCommand("", sqlConnection);
            sc.CommandText = "delete from M_Shiire_New where ShiireCode = @s";
            sc.Parameters.AddWithValue("@s", userId);
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                sc.Transaction = sql;
                sc.ExecuteNonQuery();
                sql.Commit();
            }
            catch (Exception ex)
            {
                if (sql != null)
                {
                    sql.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void DeleteTokuisaki(string userId, SqlConnection sqlConnection)
        {
            string[] strID = userId.Split('/');
            SqlCommand sc = new SqlCommand("", sqlConnection);
            sc.CommandText = "Delete from M_Tokuisaki2 where CustomerCode = @cc and TokuisakiCode = @tc";
            sc.Parameters.AddWithValue("@cc", strID[1]);
            sc.Parameters.AddWithValue("@tc", strID[0]);
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                sc.Transaction = sql;
                sc.ExecuteNonQuery();
                sql.Commit();
            }
            catch (Exception ex)
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void UpdateCSVtokuisaki(DataSet1.M_Tokuisaki2DataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tokuisaki2 where CustomerCode = @cc and TokuisakiCode = @tc";
            da.SelectCommand.Parameters.AddWithValue("@cc", dt[0].CustomerCode);
            da.SelectCommand.Parameters.AddWithValue("@tc", dt[0].TokuisakiCode);
            DataSet1.M_Tokuisaki2DataTable dtN = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dtN);

            SqlTransaction sqlTran = null;
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.V_Jouei_KakakuDataTable GetJoueiKakakuView(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select *  from V_Jouei_Kakaku where SyouhinMei like @syouhinmei";
            da.SelectCommand.Parameters.AddWithValue("@syouhinmei", "%" + v + "%");
            DataMaster.V_Jouei_KakakuDataTable dt = new DataMaster.V_Jouei_KakakuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_ProductRow GetProductRow(string userId, string name, string media, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * From M_Product Where SyouhinCode = @ID";
            da.SelectCommand.Parameters.AddWithValue("@ID", userId);
            da.SelectCommand.Parameters.AddWithValue("@Name", name);
            da.SelectCommand.Parameters.AddWithValue("@Media", media);
            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_ProductRow;
            else
                return null;
        }

        public static void UpdateOshirase(DataMaster.T_OshiraseDataTable dt, SqlConnection sqlConnection)
        {
            string no = dt[0].OshiraseNo;
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_Oshirase where OshiraseNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            DataMaster.T_OshiraseDataTable dtN = new DataMaster.T_OshiraseDataTable();
            da.Fill(dtN);

            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();
            SqlTransaction sql = null;
            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                if (dtN.Count == 0)
                {
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;
                    da.Update(dt);
                    sql.Commit();
                }
                else
                {
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                    dtN[0].OshiraseNaiyou = dt[0].OshiraseNaiyou;
                    dtN[0].CreateUser = dt[0].CreateUser;
                    dtN[0].accept = dt[0].accept;
                    da.Update(dtN);
                    sql.Commit();
                }
            }
            catch
            {
                if (sql != null)
                {
                    sql.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void InsertProduct2(DataSet1.M_Kakaku_2DataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Kakaku_2";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            SqlTransaction sql = null;
            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch (Exception ex)
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void DelProduct(string strSyouhinCode, string strSyouhinMei, SqlConnection sqlConnection)
        {
            SqlCommand sc = new SqlCommand("", sqlConnection);
            sc.CommandText = "Delete from M_Kakaku_2 where SyouhinCode = @c and SyouhinMei = @m";
            sc.Parameters.AddWithValue("@c", strSyouhinCode);
            sc.Parameters.AddWithValue("@m", strSyouhinMei);
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                sc.Transaction = sql;
                sc.ExecuteNonQuery();
                sql.Commit();
            }
            catch (Exception ex)
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static DataMaster.M_ProductDataTable GetProduct4(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);

            da.SelectCommand.CommandText = cmm;

            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TantoRow GetM_TantoRow(string userId, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * From M_Tanto Where UserID = @ID";
            da.SelectCommand.Parameters.AddWithValue("@ID", userId);

            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataSet1.M_TantoRow;
            else
                return null;
        }

        public static DataMaster.M_Shiire_NewRow GetShiire(string vsID, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Shiire_New Where ShiireCode = @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", vsID);

            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_Shiire_NewRow;
            else
                return null;
        }

        public static DataMaster.M_Customer_NewRow GetCustomer(string vsID, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Customer_New Where CustomerCode = @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", vsID);

            DataMaster.M_Customer_NewDataTable dt = new DataMaster.M_Customer_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_Customer_NewRow;
            else
                return null;
        }

        public static void UpdateProductList(DataMaster.V_ProductListDataTable dt, SqlConnection sqlConnection)
        {
            DataSet1.M_Kakaku_2DataTable dtN = new DataSet1.M_Kakaku_2DataTable();
            for (int i = 0; i < dt.Count; i++)
            {

                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }
                if (dt[i]["公共図書館"].ToString() == "1")
                {

                }

            }
        }

        public static DataMaster.M_CityRow GetCity(string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_City where CityCode = @cc ";
            da.SelectCommand.Parameters.AddWithValue("@cc", int.Parse(no.Trim()));
            DataMaster.M_CityDataTable dt = new DataMaster.M_CityDataTable();
            da.Fill(dt);
            return dt[0];
        }

        public static DataMaster.M_Kakaku_NewDataTable GetKakaku(string sID, string sName, string smedia, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New Where SyouhinCode = @ID AND ShiireName = @Name AND Media = @M";
            da.SelectCommand.Parameters.AddWithValue("@ID", sID);
            da.SelectCommand.Parameters.AddWithValue("@Name", sName);
            da.SelectCommand.Parameters.AddWithValue("@M", smedia);

            DataMaster.M_Kakaku_NewDataTable dt = new DataMaster.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TantoRow GetM_TantoRow2(string ui, string value, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto where UserID = @UI and RowNo = @row";
            da.SelectCommand.Parameters.AddWithValue("@UI", ui);
            da.SelectCommand.Parameters.AddWithValue("@row", value);

            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_TantoRow;
            else
                return null;
        }

        public static DataMaster.M_TantoDataTable TantoRow(string ui, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tanto where UserID = @id order by RowNo desc";
            da.SelectCommand.Parameters.AddWithValue("@id", ui);
            DataMaster.M_TantoDataTable dt = new DataMaster.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_TantoRow TantoKey(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tanto order by UserKey desc";
            DataMaster.M_TantoDataTable dt = new DataMaster.M_TantoDataTable();
            da.Fill(dt);
            if (dt.Count >= 1)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static DataMaster.V_ProductListDataTable GetProduct5(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from V_ProductList";
            DataMaster.V_ProductListDataTable dt = new DataMaster.V_ProductListDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_ProductDataTable KensakuProduct(string p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Product where SyouhinCode = @p";
            da.SelectCommand.Parameters.AddWithValue("@p", p);
            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            return dt;
        }


        public static DataSet1.M_Kakaku_NewRow GetKakakuRow(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New";
            DataSet1.M_Kakaku_NewDataTable dt = new DataSet1.M_Kakaku_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_Kakaku_NewRow;
            else
                return null;
        }



        public static DataSet1.M_Kakaku_New1DataTable GetM_Kakaku(string sSyouhin, string sCate, string sShiire, int nData, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "Select * From M_Kakaku_New";
            for (int i = 0; i < nData; i++)
            {
                string sSyo = "";
                string sMe = "";
                string sShi = "";
                if (i == 0)
                {
                    sSyo = sSyouhin.Split(',')[i];
                    sMe = sCate.Split(',')[i];
                    sShi = sShiire.Split(',')[i];

                    da.SelectCommand.CommandText +=
                        " Where (SyouhinCode = @Syouhin AND Media = @Medi  AND ShiireName = @Shiire)";
                    da.SelectCommand.Parameters.AddWithValue("@Syouhin", sSyo);
                    da.SelectCommand.Parameters.AddWithValue("@Medi", sMe);
                    da.SelectCommand.Parameters.AddWithValue("@Shiire", sShi);

                }
                else
                {
                    sSyo = sSyouhin.Split(',')[i];
                    sMe = sCate.Split(',')[i];
                    sShi = sShiire.Split(',')[i];

                    da.SelectCommand.CommandText +=
                        " or (SyouhinCode = @Syouhin" + i + " AND Media = @Medi" + i + " AND ShiireName = @Shiire" + i + ")";
                    da.SelectCommand.Parameters.AddWithValue("@Syouhin" + i, sSyo);
                    da.SelectCommand.Parameters.AddWithValue("@Medi" + i, sMe);
                    da.SelectCommand.Parameters.AddWithValue("@Shiire" + i, sShi);

                }
            }

            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);
            return dt;
        }

        public static void NewShiire(DataMaster.M_Shiire_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Shiire_New";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_HanniRow GetHanniCode(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Hanni where Hanni = @ha";
            da.SelectCommand.Parameters.AddWithValue("@ha", v);
            DataMaster.M_HanniDataTable dt = new DataMaster.M_HanniDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataMaster.M_HanniRow;
            else
                return null;
        }

        public static void DelTantoBusyo(string id, string busyo, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "Delete  from M_Tanto where UserID = @id and Busyo = @bu";
            da.Parameters.AddWithValue("@id", id);
            da.Parameters.AddWithValue("@bu", busyo);
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.Transaction = sql;
                da.ExecuteNonQuery();
                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void InsertProduct(DataMaster.M_ProductDataTable dt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Product";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sqltra = null;

            try
            {
                sql.Open();
                sqltra = sql.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;

                da.Update(dt);
                sqltra.Commit();
            }
            catch
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sql.Close();
            }

        }


        public static void EditShiire(DataMaster.M_Shiire_NewDataTable dt, string vsID, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "delete M_Shiire_New Where ShiireCode = @ID";
            cmd.Parameters.AddWithValue("@ID", vsID);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Shiire_New Where ShiireCode = @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", vsID);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                cmd.ExecuteNonQuery();

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_JoueiKakaku2DataTable GetJouei(string shiireName, string media, string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_JoueiKakaku2 where ShiiresakiCode = @sm and Media = @me and Capacity = @ca ";
            da.SelectCommand.Parameters.AddWithValue("@sm", shiireName);
            da.SelectCommand.Parameters.AddWithValue("@me", media);
            da.SelectCommand.Parameters.AddWithValue("@ca", text);
            DataMaster.M_JoueiKakaku2DataTable dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_Kakaku_NewRow GetM_SyohinCate(string sSyouhin, string Cate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New Where Makernumber = @ID AND CategoryCode = @Cate";
            da.SelectCommand.Parameters.AddWithValue("@ID", sSyouhin);
            da.SelectCommand.Parameters.AddWithValue("@Cate", Cate);

            DataMaster.M_Kakaku_NewDataTable dt = new DataMaster.M_Kakaku_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_Kakaku_NewRow;
            else
                return null;
        }

        public static DataMaster.M_Syohin_NewRow GetM_Syohin(string sID, string sName, string sMedia, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Syohin_New Where SyouhinCode = @ID AND ShiireMei = @Name AND Media = @M ";
            da.SelectCommand.Parameters.AddWithValue("@ID", sID);
            da.SelectCommand.Parameters.AddWithValue("@Name", sName);
            da.SelectCommand.Parameters.AddWithValue("@M", sMedia);

            DataMaster.M_Syohin_NewDataTable dt = new DataMaster.M_Syohin_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_Syohin_NewRow;
            else
                return null;
        }

        public static DataMaster.M_BumonRow GetM_BumonRow(string kubun, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * From M_Bumon Where BumonKubun = @ID";
            da.SelectCommand.Parameters.AddWithValue("@ID", kubun);

            DataMaster.M_BumonDataTable dt = new DataMaster.M_BumonDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_BumonRow;
            else
                return null;
        }


        public static DataMaster.M_Facility_NewRow GetM_TyokusoRow(string tyokusoCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Facility_New Where FacilityName1 = @Name";
            da.SelectCommand.Parameters.AddWithValue("@Name", tyokusoCode);

            DataMaster.M_Facility_NewDataTable dt = new DataMaster.M_Facility_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_Facility_NewRow;
            else
                return null;
        }

        public static DataMaster.M_ProductDataTable CSVdl(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataMaster.M_ProductDataTable dt = new DataMaster.M_ProductDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void NewCustomer(DataSet1.M_Tokuisaki2DataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Tokuisaki2";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void EditCustomer(string vsID, DataSet1.M_Tokuisaki2DataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tokuisaki2 where TokuisakiCode = @tc and CustomerCode = @cc ";
            string[] code = vsID.Split('/');
            da.SelectCommand.Parameters.AddWithValue("@tc", code[0]);
            da.SelectCommand.Parameters.AddWithValue("@cc", code[1]);
            DataSet1.M_Tokuisaki2DataTable dtU = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dtU);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                dtU[0].ItemArray = dt[0].ItemArray;
                da.Update(dtU);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void NewTanto(DataMaster.M_TantoDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Tanto";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_Syohin_NewRow GetM_Syohin2(string sCode, string sSyohin, string sHinban, string sShiire, string sBaitai, string sJotai, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                @"SELECT * FROM M_Syohin_New 
                  Where SyouhinCode = @ID AND SyouhinMei = @Syohin AND
                  MekarHinban = @Hinban AND KeitaiMei = @Keitai AND 
                  ShiiresakiMei =@Shiire AND RiyoJoutai = @sRiyo";
            da.SelectCommand.Parameters.AddWithValue("@ID", sCode);
            da.SelectCommand.Parameters.AddWithValue("@Syohin", sSyohin);
            da.SelectCommand.Parameters.AddWithValue("@Hinban", sHinban);
            da.SelectCommand.Parameters.AddWithValue("@Keitai", sBaitai);
            da.SelectCommand.Parameters.AddWithValue("@Shiire", sShiire);
            da.SelectCommand.Parameters.AddWithValue("@sRiyo", sJotai);

            DataMaster.M_Syohin_NewDataTable dt = new DataMaster.M_Syohin_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_Syohin_NewRow;
            else
                return null;
        }

        public static void DelKakaku(string sCode, string sBaitai, string sCate, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "delete M_Kakaku_New Where Makernumber=@Code AND Media = @media AND CategoryCode=@Cate";
            cmd.Parameters.AddWithValue("@Code", sCode);
            cmd.Parameters.AddWithValue("@media", sBaitai);
            cmd.Parameters.AddWithValue("@Cate", sCate);

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                cmd.ExecuteNonQuery();

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void NewBumon(DataMaster.M_BumonDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Bumon";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_Shiire_NewDataTable GetShiireCode(string abb, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Shiire_New where abbreviation = @p";
            da.SelectCommand.Parameters.AddWithValue("@p", abb);
            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void NewSyohin(DataMaster.M_Syohin_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Syohin_New";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_JoueiKakaku2DataTable GetJouei4(string shiirecode, string media, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_JoueiKakaku2 where ShiiresakiCode = @shi and Media = @md";
            da.SelectCommand.Parameters.AddWithValue("@shi", shiirecode);
            da.SelectCommand.Parameters.AddWithValue("@md", media);
            DataMaster.M_JoueiKakaku2DataTable dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_ShiiresakiDataTable GetShiiresaki(string abb, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Shiiresaki where ShiiresakiRyakusyou like @p";
            da.SelectCommand.Parameters.AddWithValue("@p", abb + "%");
            DataMaster.M_ShiiresakiDataTable dt = new DataMaster.M_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void NewKakaku(DataMaster.M_Kakaku_NewDataTable kdt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(kdt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void UPdateProduct(string makerHinban, string syouhinCode, string media, string range, DataMaster.M_ProductDataTable dtN, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Product Where MekarHinban = @m and SyouhinCode = @s and Media = @me and Range = @r";
            da.SelectCommand.Parameters.AddWithValue("@m", makerHinban);
            da.SelectCommand.Parameters.AddWithValue("@s", syouhinCode);
            da.SelectCommand.Parameters.AddWithValue("@me", media);
            da.SelectCommand.Parameters.AddWithValue("@r", range);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataMaster.M_ProductDataTable Tdt = new DataMaster.M_ProductDataTable();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                da.Fill(Tdt);

                DataMaster.M_ProductRow dr = Tdt[0];

                if (makerHinban == "")
                { dr.Makernumber = dtN[0].Makernumber; }
                if (syouhinCode == "")
                { dr.SyouhinCode = dtN[0].SyouhinCode; }
                if (media == "")
                { dr.Media = dtN[0].Media; }
                if (range == "")
                { dr.Hanni = dtN[0].Hanni; }

                da.Update(Tdt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();

            }
            finally
            {
                sqlConnection.Close();

            }
        }

        public static void EditSyohin(string vsID, string sName, string sMedia, DataMaster.M_Syohin_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "delete M_Syohin_New Where SyouhinCode=@ID AND ShiireMei = @Code AND Media = @M";
            cmd.Parameters.AddWithValue("@ID", vsID);
            cmd.Parameters.AddWithValue("@Code", sName);
            cmd.Parameters.AddWithValue("@M", sMedia);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Syohin_New Where SyouhinCode=@ID AND ShiireMei = @Code AND Media = @M";
            da.SelectCommand.Parameters.AddWithValue("@ID", vsID);
            da.SelectCommand.Parameters.AddWithValue("@Code", sName);
            da.SelectCommand.Parameters.AddWithValue("@M", sMedia);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                cmd.ExecuteNonQuery();

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_Tanto1Row GetTantoRow2(string hcode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Tanto where UserID = @id";
            da.SelectCommand.Parameters.AddWithValue("@id", hcode);
            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
            da.Fill(dt);
            return dt[0];
        }

        public static DataMaster.M_ShiiresakiRow GetShiireName(string shiCode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Shiiresaki where ShiiresakiRyakusyou = @p";
            da.SelectCommand.Parameters.AddWithValue("@p", shiCode);
            DataMaster.M_ShiiresakiDataTable dt = new DataMaster.M_ShiiresakiDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMaster.M_ShiiresakiRow;
            else
                return null;
        }

        public static void EditBumon(string vsID, DataMaster.M_BumonDataTable dt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "delete M_Bumon Where BumonKubun=@ID";
            cmd.Parameters.AddWithValue("@ID", vsID);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Bumon Where BumonKubun = @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", vsID);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                cmd.ExecuteNonQuery();

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_Kakaku_NewRow GetKakakuData(string sSyouhin, string sShiire, string sBaitai, string sCate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                @"Select * From M_Kakaku_New
                  Where SyouhinMei = @SMei AND ShiireName = @SSaki AND Media = @KMei AND Categoryname = @CName";
            da.SelectCommand.Parameters.AddWithValue("@SMei", sSyouhin);
            da.SelectCommand.Parameters.AddWithValue("@SSaki", sShiire);
            da.SelectCommand.Parameters.AddWithValue("@KMei", sBaitai);
            da.SelectCommand.Parameters.AddWithValue("@CName", sCate);

            DataMaster.M_Kakaku_NewDataTable dt = new DataMaster.M_Kakaku_NewDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataMaster.M_Kakaku_NewRow;
            else
                return null;
        }

        public static void KakakuMaster(DataMaster.M_Kakaku_NewDataTable dt, string sSyouhin, string sShiire, string sBaitai, string sCate, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = @"delete M_Kakaku_New 
                  Where SyouhinMei = @SMei AND ShiireName = @SSaki AND Media = @KMei AND Categoryname = @CName";
            cmd.Parameters.AddWithValue("@SMei", sSyouhin);
            cmd.Parameters.AddWithValue("@SSaki", sShiire);
            cmd.Parameters.AddWithValue("@KMei", sBaitai);
            cmd.Parameters.AddWithValue("@CName", sCate);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                @"Select * From M_Kakaku_New 
                  Where SyouhinMei = @SMei AND ShiireName = @SSaki AND Media = @KMei AND Categoryname = @CName";
            da.SelectCommand.Parameters.AddWithValue("@SMei", sSyouhin);
            da.SelectCommand.Parameters.AddWithValue("@SSaki", sShiire);
            da.SelectCommand.Parameters.AddWithValue("@KMei", sBaitai);
            da.SelectCommand.Parameters.AddWithValue("@CName", sCate);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                cmd.ExecuteNonQuery();

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_JoueiKakaku2DataTable GetJoueiKakaku(string strShiireCode, string strMedia, string strHanni, string strSeki, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_JoueiKakaku2 where ShiiresakiCode = @sc and Media = @m and Range = @ra";
            da.SelectCommand.Parameters.AddWithValue("@sc", strShiireCode);
            da.SelectCommand.Parameters.AddWithValue("@m", strMedia);
            da.SelectCommand.Parameters.AddWithValue("@ra", strHanni);
            DataMaster.M_JoueiKakaku2DataTable dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            if (dt.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public static DataMaster.M_Tanto1DataTable GetStaff(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "select * from M_Tanto where UserName like @un";
            da.SelectCommand.Parameters.AddWithValue("@un", "%" + v + "%");
            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_CityDataTable GetCity2(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "select * from M_City where CityName like @cn";
            da.SelectCommand.Parameters.AddWithValue("@cn", "%" + v + "%");
            DataMaster.M_CityDataTable dt = new DataMaster.M_CityDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void EditTanto(string vsID, DataMaster.M_TantoDataTable dt, string value, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Tanto Where UserID = @ID and RowNo = @rw";
            da.SelectCommand.Parameters.AddWithValue("@ID", vsID);
            da.SelectCommand.Parameters.AddWithValue("@rw", value);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            DataMaster.M_TantoDataTable Tdt = new DataMaster.M_TantoDataTable();
            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                da.Fill(Tdt);
                Tdt[0].ItemArray = dt[0].ItemArray;
                da.Update(Tdt);
                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void EditTyokuso(string vsID, DataMaster.M_Facility_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "delete M_Facility_New Where FacilityName1=@Name";
            cmd.Parameters.AddWithValue("@Name", vsID);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Facility_New Where FacilityName1 = @Name";
            da.SelectCommand.Parameters.AddWithValue("@Name", vsID);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                cmd.Transaction = sql;

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                cmd.ExecuteNonQuery();

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_JoueiRangeDataTable GetJoueiRange(string strShiiresakiCode, string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_JoueiRange where ShiiresakiCode = @sc and Media = @m ";
            da.SelectCommand.Parameters.AddWithValue("@sc", strShiiresakiCode);
            da.SelectCommand.Parameters.AddWithValue("@m", text);
            DataMaster.M_JoueiRangeDataTable dt = new DataMaster.M_JoueiRangeDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_JoueiKakakuDataTable GetJouei3(string strShiiresakiCode, string media, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_JoueiKakaku where ShiiresakiCode = @sc and Media = @m";
            da.SelectCommand.Parameters.AddWithValue("@sc", strShiiresakiCode);
            da.SelectCommand.Parameters.AddWithValue("@m", media);
            DataMaster.M_JoueiKakakuDataTable dt = new DataMaster.M_JoueiKakakuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void NewTyokuso(DataMaster.M_Facility_NewDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Facility_New";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();
            }
            catch
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMaster.M_AppSettingRow GetM_AppSettingRow(string SettingKey, SqlConnection sqlConn)
        {
            string sql = "SELECT * FROM M_AppSetting WHERE SettingKey = @SettingKey";
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConn);
            da.SelectCommand.Parameters.AddWithValue("@SettingKey", SettingKey);
            DataMaster.M_AppSettingDataTable dt = new DataMaster.M_AppSettingDataTable();
            da.Fill(dt);

            if (dt.Count == 0) { return null; }

            return dt[0];
        }

        public static DataMaster.M_Tanto1Row GetTantoRow(string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tanto where UserName = @pn ";
            da.SelectCommand.Parameters.AddWithValue("@pn", text);
            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataMaster.M_Tanto1Row;
            else
                return null;
        }

        public static DataMaster.M_Tanto1DataTable GetTanto1(string id, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tanto where UserID = @id";
            da.SelectCommand.Parameters.AddWithValue("@id", id);
            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_Facility_NewRow GetFacilityRow(string[] faccode, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Facility_New where FacilityNo = @fn and Code = @cd";
            da.SelectCommand.Parameters.AddWithValue("@fn", faccode[0]);
            da.SelectCommand.Parameters.AddWithValue("@cd", faccode[1]);
            DataMaster.M_Facility_NewDataTable dt = new DataMaster.M_Facility_NewDataTable();
            da.Fill(dt);
            if (dt.Count > 0)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static DataMaster.M_JoueiKakakuDataTable GetJouei2(string shiiresakiCode, string media, string zasu, string range, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_JoueiKakaku where ShiiresakiCode = @sm and Media = @me and Capacity = @ca and Range = @ra group by Range, ShiiresakiCode, Media, Capacity, ShiiresakiName, HyoujunKakaku, ShiireKakaku";
            da.SelectCommand.Parameters.AddWithValue("@sm", shiiresakiCode);
            da.SelectCommand.Parameters.AddWithValue("@me", media);
            da.SelectCommand.Parameters.AddWithValue("@ca", zasu);
            da.SelectCommand.Parameters.AddWithValue("@ra", range);
            DataMaster.M_JoueiKakakuDataTable dt = new DataMaster.M_JoueiKakakuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_AccountPageRow GetDaihyo(string flg, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_AccountPage where RowNumber = '0'";
            da.SelectCommand.Parameters.AddWithValue("@flg", flg);
            DataMaster.M_AccountPageDataTable dt = new DataMaster.M_AccountPageDataTable();
            da.Fill(dt);
            if (dt.Count > 0)
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }

        public static DataMaster.M_JoueiKakaku2DataTable GetJouei5(string shiirecode, string selectedValue, string media, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_JoueiKakaku2 where ShiiresakiCode = @sc and Range = @r and Media = @m";
            da.SelectCommand.Parameters.AddWithValue("@sc", shiirecode);
            da.SelectCommand.Parameters.AddWithValue("@r", selectedValue);
            da.SelectCommand.Parameters.AddWithValue("@m", media);
            DataMaster.M_JoueiKakaku2DataTable dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMaster.M_Shiire_NewRow GetShiire_New(string shiireName, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Shiire_New where Abbreviation = @sn";
            da.SelectCommand.Parameters.AddWithValue("@sn", shiireName);
            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dt);
            if (dt.Count.Equals(1))
            {
                return dt[0];
            }
            else
            {
                return null;
            }
        }
    }
}
