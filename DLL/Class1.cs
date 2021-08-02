using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class Class1
    {
        public static DataSet1.V_EditSyousaiDataTable EditSyousaiDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM V_EditSyousai";
            DataSet1.V_EditSyousaiDataTable dt = new DataSet1.V_EditSyousaiDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.V_EditSyousaiRow EditSyousaiR(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM V_EditSyousai";
            DataSet1.V_EditSyousaiDataTable dt = new DataSet1.V_EditSyousaiDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.V_EditSyousaiRow;
            else
                return null;
        }

        public static DataSet1.T_OrderedDataTable GetOrderedLedger(string selectedValue, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Ordered where ShiiresakiCode = @cd";
            da.SelectCommand.Parameters.AddWithValue("@cd", selectedValue);
            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TokuisakiRow getTokusaiki2(string vsID, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Tokuisaki WHERE TokuisakiCode = @c";
            da.SelectCommand.Parameters.AddWithValue("@c", vsID);
            DataSet1.M_TokuisakiDataTable dt = new DataSet1.M_TokuisakiDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                return dt[0] as DataSet1.M_TokuisakiRow;
            }
            else
            {
                return null;
            }
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokuisakiAll(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Tokuisaki2";
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokuisakiName(string strTokuisaki, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Tokuisaki2 where TokuisakiName1 = @n";
            da.SelectCommand.Parameters.AddWithValue("@n", strTokuisaki);
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable GetProduct3(string v, string cate, string shiire, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Kakaku_2 where CategoryCode = @ca and ShiireCode = @sc and SyouhinMei like @pr";
            da.SelectCommand.Parameters.AddWithValue("@ca", cate);
            da.SelectCommand.Parameters.AddWithValue("@sc", shiire);
            da.SelectCommand.Parameters.AddWithValue("@pr", v + "%");
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataAppropriate.T_AppropriateHeaderDataTable GetAppropriateHeader(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_AppropriateHeader order by CreateDate desc";
            DataAppropriate.T_AppropriateHeaderDataTable dt = new DataAppropriate.T_AppropriateHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void UpdateSyousai(DataSet1.M_Kakaku_New1Row dl, int catecode, string productname, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New where SyouhinCode = @b and CategoryCode = @a";
            da.SelectCommand.Parameters.AddWithValue("@a", catecode);
            da.SelectCommand.Parameters.AddWithValue("@b", productname);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            DataSet1.M_Kakaku_New1DataTable dd = new DataSet1.M_Kakaku_New1DataTable();

            da.Fill(dd);
            SqlTransaction sqlTran = null;

            if (dd.Count >= 1 & dl.HyojunKakaku.ToString() == "")
            {
                SqlCommand cmdDelSave = new SqlCommand("", sql);
                cmdDelSave.CommandText =
                    "DELETE FROM M_Kakaku_New WHERE CategoryCode =@c and SyouhinCode = @s";
                cmdDelSave.Parameters.AddWithValue("@c", catecode);
                cmdDelSave.Parameters.AddWithValue("@s", productname);

                SqlTransaction sqlTra = null;
                try
                {
                    sql.Open();
                    sqlTra = sql.BeginTransaction();
                    cmdDelSave.Transaction = sqlTra;

                    cmdDelSave.ExecuteNonQuery();

                    sqlTra.Commit();

                }
                catch (Exception e)
                {
                    if (sqlTra != null)
                        sqlTra.Rollback();


                }
                finally
                {
                    sql.Close();
                }

            }



            if (dd.Count >= 1)
            {
                try
                {

                    sql.Open();
                    sqlTran = sql.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                    DataSet1.M_Kakaku_New1Row drThis = null;

                    da.Fill(dd);

                    drThis = dd[0];

                    drThis.SyouhinCode = dl.SyouhinCode;
                    drThis.SyouhinMei = dl.SyouhinMei;

                    if (!dl.IsMakernumberNull())
                    {
                        drThis.Makernumber = dl.Makernumber;
                    }

                    drThis.Media = dl.Media;

                    if (!dl.IsHanniNull())
                    {
                        drThis.Hanni = dl.Hanni;
                    }

                    if (!dl.IsWareHouseNull())
                    {
                        drThis.WareHouse = dl.WareHouse;
                    }

                    if (!dl.IsShiireNameNull())
                    {
                        drThis.ShiireName = dl.ShiireName;
                    }

                    if (!dl.IsRiyoNull())
                    {
                        drThis.Riyo = dl.Riyo;
                    }

                    drThis.CategoryCode = dl.CategoryCode;

                    drThis.Categoryname = dl.Categoryname;

                    if (!dl.IsHyojunKakakuNull())
                    {
                        drThis.HyojunKakaku = dl.HyojunKakaku;
                    }

                    if (!dl.IsTyokusouSakiNull())
                    {
                        drThis.TyokusouSaki = dl.TyokusouSaki;
                    }

                    if (!dl.IsPermissionStartNull())
                    {
                        drThis.PermissionStart = dl.PermissionStart;
                    }

                    if (!dl.IsRightEndNull())
                    {
                        drThis.RightEnd = dl.RightEnd;
                    }

                    if (!dl.IsJacketPrintNull())
                    {
                        drThis.JacketPrint = dl.JacketPrint;
                    }

                    if (!dl.IsHenkyakuNull())
                    {
                        drThis.Henkyaku = dl.Henkyaku;
                    }



                    da.Update(dd);
                    sqlTran.Commit();

                }
                catch (Exception ex)
                {
                    if (null != sqlTran)
                        sqlTran.Rollback();

                }

                finally
                {
                    sql.Close();
                }
            }

            if (dd.Count == 0)
            {
                try
                {
                    sql.Open();
                    sqlTran = sql.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;

                    DataSet1.M_Kakaku_New1Row drThis = dd.NewM_Kakaku_New1Row();

                    da.Fill(dd);


                    drThis.SyouhinCode = dl.SyouhinCode;
                    drThis.SyouhinMei = dl.SyouhinMei;

                    if (!dl.IsMakernumberNull())
                    {
                        drThis.Makernumber = dl.Makernumber;
                    }

                    drThis.Media = dl.Media;

                    if (!dl.IsHanniNull())
                    {
                        drThis.Hanni = dl.Hanni;
                    }

                    if (!dl.IsWareHouseNull())
                    {
                        drThis.WareHouse = dl.WareHouse;
                    }

                    if (!dl.IsShiireNameNull())
                    {
                        drThis.ShiireName = dl.ShiireName;
                    }

                    if (!dl.IsRiyoNull())
                    {
                        drThis.Riyo = dl.Riyo;
                    }

                    drThis.CategoryCode = dl.CategoryCode;

                    drThis.Categoryname = dl.Categoryname;

                    if (!dl.IsHyojunKakakuNull())
                    {
                        drThis.HyojunKakaku = dl.HyojunKakaku;
                    }

                    if (!dl.IsTyokusouSakiNull())
                    {
                        drThis.TyokusouSaki = dl.TyokusouSaki;
                    }

                    if (!dl.IsPermissionStartNull())
                    {
                        drThis.PermissionStart = dl.PermissionStart;
                    }

                    if (!dl.IsRightEndNull())
                    {
                        drThis.RightEnd = dl.RightEnd;
                    }

                    if (dl.JacketPrint == true)
                    {
                        drThis.JacketPrint = dl.JacketPrint;
                    }

                    if (dl.Henkyaku == true)
                    {
                        drThis.Henkyaku = dl.Henkyaku;
                    }

                    dd.AddM_Kakaku_New1Row(drThis);


                    da.Update(dd);
                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    if (sqlTran != null)
                        sqlTran.Rollback();
                }
                finally
                {
                    sql.Close();
                }
            }


        }

        public static DataSet1.M_Kakaku_2DataTable GetProduct4(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select distinct SyouhinMei from M_Kakaku_2 where SyouhinMei like @s";
            da.SelectCommand.Parameters.AddWithValue("@s", "%" + v + "%");
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TokuisakiDataTable TokuisakiCSV(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataSet1.M_TokuisakiDataTable dt = new DataSet1.M_TokuisakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataAppropriate.T_AppropriateDataTable GetDenpyo(string cate, string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate WHERE Category = @ca and ShiireNo = @shino";
            da.SelectCommand.Parameters.AddWithValue("@ca", cate);
            da.SelectCommand.Parameters.AddWithValue("@shino", no);
            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataAppropriate.T_AppropriateHeaderRow GetAppropriateHeaderRow(string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_AppropriateHeader WHERE ShiireNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            DataAppropriate.T_AppropriateHeaderDataTable dt = new DataAppropriate.T_AppropriateHeaderDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataAppropriate.T_AppropriateHeaderRow;
            else
                return null;
        }

        public static DataAppropriate.T_AppropriateDataTable GetAppropriate2(string strOrderedNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate WHERE ShiireNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", strOrderedNo);
            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataAppropriate.T_AppropriateDataTable GetAppropriateData(string tokui, string cate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate WHERE TokuisakiCode = @to and Category = @ca";
            da.SelectCommand.Parameters.AddWithValue("@to", tokui);
            da.SelectCommand.Parameters.AddWithValue("@ca", cate);

            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.__図書館価格マスター200720ver3DataTable Tosyokan(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM [★図書館価格マスター200720ver3]";
            DataSet1.__図書館価格マスター200720ver3DataTable dt = new DataSet1.__図書館価格マスター200720ver3DataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void updateKariProduct(string v, string v1, string v2, string v3, bool @checked, DataSet1.T_productDataTable df, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_product Where SyouhinCode = @sCode and ShiireMei = @sName and Media = @n and Range = @r";
            da.SelectCommand.Parameters.AddWithValue("@sCode", v);
            da.SelectCommand.Parameters.AddWithValue("@sName", v1);
            da.SelectCommand.Parameters.AddWithValue("@n", v2);
            da.SelectCommand.Parameters.AddWithValue("@r", v3);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataSet1.T_productDataTable Tdt = new DataSet1.T_productDataTable();

            SqlTransaction sql = null;
            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                da.Fill(Tdt);
                DataSet1.T_productRow dr = Tdt[0];

                dr.SyouhinCode = v;
                dr.ShiireMei = v1;
                dr.Media = v2;
                dr.checkFLG = @checked;
                dr.Range = v3;
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

        public static void UpdateAppropriate(DataAppropriate.T_AppropriateRow dl, string no, string strMaker, string strProduct, string strHanni, string strMedia, int noko, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate where ShiireNo = @no and MekerNo = @Me and ProductName = @pro and Range = @ha and Media = @med";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            da.SelectCommand.Parameters.AddWithValue("@Me", strMaker);
            da.SelectCommand.Parameters.AddWithValue("@pro", strProduct);
            da.SelectCommand.Parameters.AddWithValue("@ha", strHanni);
            da.SelectCommand.Parameters.AddWithValue("@med", strMedia);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            DataAppropriate.T_AppropriateDataTable dd = new DataAppropriate.T_AppropriateDataTable();

            da.Fill(dd);
            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                DataAppropriate.T_AppropriateRow drThis = dd[0];

                drThis.Zansu = noko.ToString();

                //dd.AddT_AppropriateRow(drThis);
                da.Update(dd);
                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
            }
        }

        public static DataSet1.M_CategoryRow GetCateList(string cate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Category where Category = @c";
            da.SelectCommand.Parameters.AddWithValue("@c", cate);
            DataSet1.M_CategoryDataTable dt = new DataSet1.M_CategoryDataTable();
            da.Fill(dt);
            return dt[0];
        }

        public static DataAppropriate.T_AppropriateRow GetUpdateAppropriate(string no, string strMaker, string strProduct, string strHanni, string strMedia, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate WHERE ShiireNo = @no AND MekerNo = @ma AND ProductName = @pro and Range = @ra and Media = @ke ";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            da.SelectCommand.Parameters.AddWithValue("@ma", strMaker);
            da.SelectCommand.Parameters.AddWithValue("@pro", strProduct);
            da.SelectCommand.Parameters.AddWithValue("@ra", strHanni);
            da.SelectCommand.Parameters.AddWithValue("@ke", strMedia);

            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
            {
                return dt[0] as DataAppropriate.T_AppropriateRow;
            }
            else
            { return null; }
        }

        public static DataSet1.M_TantoDataTable GetStaff(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto where Yuko = 1 Order by UserID ASC ";

            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void UploadTokuisaki(DataSet1.M_Tokuisaki2DataTable dtN, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tokuisaki2 where TokuisakiCode = @t and CustomerCode = @c";
            da.SelectCommand.Parameters.AddWithValue("@t", dtN[0].TokuisakiCode.ToString());
            da.SelectCommand.Parameters.AddWithValue("@c", dtN[0].CustomerCode);
            DataSet1.M_TokuisakiDataTable dt = new DataSet1.M_TokuisakiDataTable();
            da.Fill(dt);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            SqlTransaction sqltra = null;

            try
            {
                if (dt.Count == 0)
                {
                    sqlConnection.Open();
                    sqltra = sqlConnection.BeginTransaction();

                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                    da.Update(dtN);
                    sqltra.Commit();
                    sqlConnection.Close();
                }
                else
                {
                    sqlConnection.Open();
                    sqltra = sqlConnection.BeginTransaction();

                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                    dt[0].ItemArray = dtN[0].ItemArray;
                    da.Update(dt);
                    sqltra.Commit();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void UpdateAppropriateHeader(DataAppropriate.T_AppropriateHeaderRow drH, string su, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_AppropriateHeader where ShiireNo = @no and ShiiresakiCode = @Me and TokuisakiCode = @pro and Category = @ha";
            da.SelectCommand.Parameters.AddWithValue("@no", drH.ShiireNo);
            da.SelectCommand.Parameters.AddWithValue("@Me", drH.ShiiresakiCode);
            da.SelectCommand.Parameters.AddWithValue("@pro", drH.TokuisakiCode);
            da.SelectCommand.Parameters.AddWithValue("@ha", drH.Category);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataAppropriate.T_AppropriateHeaderDataTable dd = new DataAppropriate.T_AppropriateHeaderDataTable();

            da.Fill(dd);
            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                DataAppropriate.T_AppropriateHeaderRow drThis = drH;
                int suryo = drH.ShiireAmount;
                suryo += int.Parse(su);
                dd[0].ShiireAmount = suryo;
                //dd.AddT_AppropriateRow(drThis);
                da.Update(dd);
                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
            }

        }

        public static void UploadKakaku(DataSet1.M_Kakaku_2DataTable dtN, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Kakaku_2 where SyouhinCode = @sc and Makernumber = @mn and Media = @m and Hanni = @h and ShiireCode = @shi and CategoryCode = @cc and HyoujunKakaku = @hk";
            da.SelectCommand.Parameters.AddWithValue("@sc", dtN[0].SyouhinCode);
            da.SelectCommand.Parameters.AddWithValue("@mn", dtN[0].Makernumber);
            da.SelectCommand.Parameters.AddWithValue("@m", dtN[0].Media);
            da.SelectCommand.Parameters.AddWithValue("@h", dtN[0].Hanni);
            da.SelectCommand.Parameters.AddWithValue("@shi", dtN[0].ShiireCode);
            da.SelectCommand.Parameters.AddWithValue("@cc", dtN[0].CategoryCode);
            da.SelectCommand.Parameters.AddWithValue("@hk", dtN[0].HyoujunKakaku);

            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            SqlTransaction sqltra = null;

            try
            {
                if (dt.Count == 0)
                {
                    sqlConnection.Open();
                    sqltra = sqlConnection.BeginTransaction();
                    DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                    dr.ItemArray = dtN[0].ItemArray;
                    dt.AddM_Kakaku_2Row(dr);
                    da.Update(dt);
                    sqltra.Commit();

                }
                else
                {
                    sqlConnection.Open();
                    sqltra = sqlConnection.BeginTransaction();

                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                    dt[0].ItemArray = dtN[0].ItemArray;
                    da.Update(dt);
                    sqltra.Commit();
                }
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


        public static DataSet1.M_Kakaku_2DataTable CSVdl(string cmm, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = cmm;
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TantoRow DeleteStaff(int id, string userName, string busyo, string passWord, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText = "DELETE FROM M_Tanto where UserID = @id and UserName = @un and Busyo = @bu and Password = @pas";
            da.Parameters.AddWithValue("@id", id);
            da.Parameters.AddWithValue("@un", userName);
            da.Parameters.AddWithValue("@bu", busyo);
            da.Parameters.AddWithValue("@pas", passWord);
            SqlTransaction sqltra = null;
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                da.Transaction = sqltra;
                da.ExecuteNonQuery();
                sqltra.Commit();
            }
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
            return null;

        }

        public static void DELkariProduct(SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText = "DELETE FROM T_product ";
            SqlTransaction sqltra = null;
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                da.Transaction = sqltra;
                da.ExecuteNonQuery();
                sqltra.Commit();

            }
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokuisaki2(string strCustomer, string strTokuisaki, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tokuisaki2 where CustomerCode = @c and TokuisakiCode = @t";
            da.SelectCommand.Parameters.AddWithValue("@c", strCustomer);
            da.SelectCommand.Parameters.AddWithValue("@t", strTokuisaki);
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static void DeleteStaff(DataSet1.M_TantoRow dr, int id, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText = "DELETE FROM M_Tanto where UserID = @id";
            da.Parameters.AddWithValue("@id", id);
            SqlTransaction sqltra = null;
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                da.Transaction = sqltra;
                da.ExecuteNonQuery();
                sqltra.Commit();

            }
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataSet1.M_TantoDataTable GetNoStaff(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto where Yuko = 0 Order by UserID ASC ";
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static DataSet1.M_Tokuisaki2DataTable GetTokuisaki(object a, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tokuisaki2 Where (TokuisakiRyakusyo = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", a);

            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.KariMitsumoriDataTable GetKari(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM KariMitumori ";
            DataSet1.KariMitsumoriDataTable dt = new DataSet1.KariMitsumoriDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static void DeleteSyousai(DataSet1.M_Kakaku_New1Row dl, int catecode, string productname, SqlConnection sql)
        {
            SqlCommand cmdDelSave = new SqlCommand("", sql);
            cmdDelSave.CommandText =
                "DELETE FROM M_Kakaku_New WHERE CategoryCode =@c and SyouhinCode = @s";
            cmdDelSave.Parameters.AddWithValue("@c", catecode);
            cmdDelSave.Parameters.AddWithValue("@s", productname);

            SqlTransaction sqlTra = null;

            try
            {
                sql.Open();
                sqlTra = sql.BeginTransaction();

                cmdDelSave.Transaction = sqlTra;

                cmdDelSave.ExecuteNonQuery();

                sqlTra.Commit();

            }
            catch (Exception et)
            {
                if (sqlTra != null)
                    sqlTra.Rollback();

            }
            finally
            {
                sql.Close();
            }
        }

        public static void DelCtlSyousai(DataSet1.M_Kakaku_New1Row dl, int catecode, string productname, SqlConnection sql)
        {
            SqlCommand cmdDelSave = new SqlCommand("", sql);
            cmdDelSave.CommandText =
                "DELETE FROM M_Kakaku_New WHERE CategoryCode =@c and SyouhinCode = @s";
            cmdDelSave.Parameters.AddWithValue("@c", catecode);
            cmdDelSave.Parameters.AddWithValue("@s", productname);

            SqlTransaction sqlTra = null;

            try
            {
                sql.Open();
                sqlTra = sql.BeginTransaction();

                cmdDelSave.Transaction = sqlTra;

                cmdDelSave.ExecuteNonQuery();

                sqlTra.Commit();

            }
            catch (Exception et)
            {
                if (sqlTra != null)
                    sqlTra.Rollback();

            }
            finally
            {
                sql.Close();
            }
        }

        public static DataSet1.M_Kakaku_2Row Getproduct2(string makerNo, string media, string hanni, string cate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_2 WHERE (Makernumber = @ma) AND (Media = @me) AND (Hanni = @ha) and CategoryCode = @ca";
            da.SelectCommand.Parameters.AddWithValue("@ma", makerNo);
            da.SelectCommand.Parameters.AddWithValue("@me", media);
            da.SelectCommand.Parameters.AddWithValue("@ha", hanni);
            da.SelectCommand.Parameters.AddWithValue("@ca", cate);

            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            if (dt.Count == 1)
            {
                return dt[0] as DataSet1.M_Kakaku_2Row;
            }
            else
            {
                return null;
            }
        }

        public static DataAppropriate.T_AppropriateHeaderDataTable GetHeader(string shiire, string cate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_AppropriateHeader Where (ShiiresakiName = @s) and (CategoryName = @c)";
            da.SelectCommand.Parameters.AddWithValue("@s", shiire);
            da.SelectCommand.Parameters.AddWithValue("@c", cate);

            DataAppropriate.T_AppropriateHeaderDataTable dt = new DataAppropriate.T_AppropriateHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void InsertAppropriateHeader(DataAppropriate.T_AppropriateHeaderDataTable dtH, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_AppropriateHeader";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sqltra = null;

            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;

                da.Update(dtH);
                sqltra.Commit();
            }
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataSet1.M_Facility_NewDataTable FacilityDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Facility_New";
            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_Facility_NewDataTable FacilityDT(string fac, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Facility_New Where (Abbreviation = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", fac);

            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            return (dt);
        }
        public static void InsertKari(DataSet1.KariMitsumoriDataTable dt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM KariMitsumori";
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
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sql.Close();
            }

        }

        public static DataAppropriate.T_AppropriateHeaderRow GetMaxShiireNo(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT Max(ShiireNo) as ShiireNo FROM T_AppropriateHeader";
            DataAppropriate.T_AppropriateHeaderDataTable dt = new DataAppropriate.T_AppropriateHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataAppropriate.T_AppropriateHeaderRow;
            else
                return null;
        }

        public static DataAppropriate.T_AppropriateDataTable GetAppropriate(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate";
            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void InsertAppropriate(DataAppropriate.T_AppropriateDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Appropriate";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sqltra = null;

            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;

                da.Update(dt);
                sqltra.Commit();
            }
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void InsertKariProduct(DataSet1.T_productDataTable dt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_product";
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
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sql.Close();
            }

        }

        public static DataSet1.T_productDataTable GetKariProduct(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_product";
            DataSet1.T_productDataTable dt = new DataSet1.T_productDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static DataSet1.KariMitsumoriDataTable GetKari2(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM KariMitsumori Where (FacilityName = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", v);

            DataSet1.KariMitsumoriDataTable dt = new DataSet1.KariMitsumoriDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_TantoDataTable GetStaff(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto ";

            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static DataSet1.M_Kakaku_New1Row GetKakaku(string id, string name, string media, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Kakaku_New Where SyouhinCode = @Code AND SyouhinMei = @Mei AND Media = @Medi";
            da.SelectCommand.Parameters.AddWithValue("@Code", id);
            da.SelectCommand.Parameters.AddWithValue("@Mei", name);
            da.SelectCommand.Parameters.AddWithValue("@Medi", media);


            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataSet1.M_Kakaku_New1Row;
            else
                return null;
        }

        public static DataSet1.M_Facility_NewRow Getfacility(string fac, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Facility_New where Abbreviation = @fac";
            da.SelectCommand.Parameters.AddWithValue("@fac", fac);
            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataSet1.M_Facility_NewRow;
            else
                return null;
        }

        public static DataSet1.M_Kakaku_New1DataTable GetKakaku2(string mekerNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New Where (Makernumber = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", mekerNo);

            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_Kakaku_2DataTable getproduct(string v, string cate, string hanni, SqlConnection sqlConnection)
        {
            string sm = v.Trim();
            string cn = cate.Trim();
            string hn = hanni.Trim();

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_2 Where (Makernumber = @e) and (Categoryname = @s) and (Hanni = @b) ";
            da.SelectCommand.Parameters.AddWithValue("@e", sm);
            da.SelectCommand.Parameters.AddWithValue("@s", cn);
            da.SelectCommand.Parameters.AddWithValue("@b", hn);

            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_Facility_NewRow FacilityRow(string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Facility_New where FacilityNo = @fac";
            da.SelectCommand.Parameters.AddWithValue("@fac", text);
            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataSet1.M_Facility_NewRow;
            else
                return null;
        }

        public static DataSet1.M_Facility_NewRow FacilityRow2(string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Facility_New where FacilityName1 = @fac";
            da.SelectCommand.Parameters.AddWithValue("@fac", text);
            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataSet1.M_Facility_NewRow;
            else
                return null;
        }


        public static void InsertFacility(DataSet1.M_Facility_NewDataTable newdt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Facility_New";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sqltra = null;

            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;

                da.Update(newdt);
                sqltra.Commit();
            }
            catch (Exception ex)
            {
                if (sqltra != null)
                    sqltra.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataSet1.M_TantoDataTable GetStaff2(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Tanto where UserName like @un";
            da.SelectCommand.Parameters.AddWithValue("@un","%" + v + "%");
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void UpdateKakaku(string proCode, string makerNo, string cate, string hanni, DataSet1.M_Kakaku_2Row dr, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_Kakaku_2 where SyouhinCode = @sc and Makernumber = @ma and CategoryCode = @ca ";
            da.SelectCommand.Parameters.AddWithValue("@sc", proCode);
            da.SelectCommand.Parameters.AddWithValue("@ma", makerNo);
            da.SelectCommand.Parameters.AddWithValue("@ca", cate);
            //da.SelectCommand.Parameters.AddWithValue("@ha", hanni);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataSet1.M_Kakaku_2DataTable Ndt = new DataSet1.M_Kakaku_2DataTable();
            SqlTransaction sql = null;
            try
            {
                da.Fill(Ndt);
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                DataSet1.M_Kakaku_2Row dl = Ndt[0];
                dl = dr;

                da.Update(Ndt);
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

        public static void UpdateFaci(DataSet1.M_Facility_NewDataTable newdt, int no, string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Facility_New Where FacilityNo = @ID and FacilityName1 = @n";
            da.SelectCommand.Parameters.AddWithValue("@ID", no);
            da.SelectCommand.Parameters.AddWithValue("@n", text);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataSet1.M_Facility_NewDataTable Tdt = new DataSet1.M_Facility_NewDataTable();

            SqlTransaction sql = null;
            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                da.Fill(Tdt);
                DataSet1.M_Facility_NewRow dr = Tdt[0];

                dr.FacilityNo = newdt[0].FacilityNo;

                dr.FacilityName1 = newdt[0].FacilityName1;

                if (!newdt[0].IsFacilityName2Null())
                {
                    dr.FacilityName2 = newdt[0].FacilityName2;
                }
                if (!newdt[0].IsAbbreviationNull())
                { dr.Abbreviation = newdt[0].Abbreviation; }
                if (!newdt[0].IsFacilityResponsibleNull())
                { dr.FacilityResponsible = newdt[0].FacilityResponsible; }
                if (!newdt[0].IsPostNoNull())
                { dr.PostNo = newdt[0].PostNo; }
                if (!newdt[0].IsAddress1Null())
                { dr.Address1 = newdt[0].Address1; }
                if (!newdt[0].IsAddress2Null())
                { dr.Address2 = newdt[0].Address2; }
                if (!newdt[0].IsTellNull())
                { dr.Tell = newdt[0].Tell; }
                if (!newdt[0].IsCityCodeNull())
                { dr.CityCode = newdt[0].CityCode; }
                if (!newdt[0].IsTitlesNull())
                { dr.Titles = newdt[0].Titles; }
                if (!newdt[0].IsStateNull())
                { dr.State = newdt[0].State; }
                if (!newdt[0].IsUpDateUserNull())
                { dr.UpDateUser = newdt[0].UpDateUser; }
                dr.UpDateDay = DateTime.Now;

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

        public static DataSet1.M_Shiire_NewRow GetShiire(string h, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Shiire_New where Abbreviation = @name";
            da.SelectCommand.Parameters.AddWithValue("@name", h);
            DataSet1.M_Shiire_NewDataTable dt = new DataSet1.M_Shiire_NewDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataSet1.M_Shiire_NewRow;
            else
                return null;
        }

        public static DataSet1.M_Tokuisaki2Row GetTokuisaki3(string v1, string v2, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * from M_Tokuisaki2 where CustomerCode = @cc and TokuisakiCode = @tc";
            da.SelectCommand.Parameters.AddWithValue("@cc", v2);
            da.SelectCommand.Parameters.AddWithValue("@tc", v1);
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt[0];
        }
    }
}
