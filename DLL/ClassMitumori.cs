using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassMitumori
    {
        public static DataMitumori.M_Customer_NewRow GetMTokuisaki(string vsID, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Customer_New Where CustomerCode = @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", vsID);

            DataMitumori.M_Customer_NewDataTable dt = new DataMitumori.M_Customer_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMitumori.M_Customer_NewRow;
            else
                return null;
        }

        public static DataMitumori.T_MitumoriDataTable GetJyutyu(string mNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Mitumori Where (MitumoriNo = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", mNo);

            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.T_MitumoriDataTable MitumoriInsatu2(string v1, string v2, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Mitumori WHERE (MitumoriNo = @m) AND (SisetuMei = @s)";
            da.SelectCommand.Parameters.AddWithValue("@m", v1);
            da.SelectCommand.Parameters.AddWithValue("@s", v2);

            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.T_MitumoriDataTable MitumoriInsatu(string strMitumoriNo, string strRowNo, SqlConnection sqlConnection)
        {
            string sql =
                 string.Format("Select * From T_Mitumori WHERE (MitumoriNo = {0})",
                                strMitumoriNo);

            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConnection);

            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMitumori.T_MitumoriDataTable GetMitumoriHinban(string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Mitumori where MekarHinban = @mh Order By MitumoriNo desc";
            da.SelectCommand.Parameters.AddWithValue("@mh", text);
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMitumori.T_MitumoriHeaderBackupDataTable GetMitumoriHinban2(string text, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Mitumori where MekarHinban = @mh Order By MitumoriNo desc";
            da.SelectCommand.Parameters.AddWithValue("@mh", text);
            DataMitumori.T_MitumoriHeaderBackupDataTable dt = new DataMitumori.T_MitumoriHeaderBackupDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMitumori.T_MitumoriBackupDataTable GetMitumoriHinban3(string selectedValue, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Mitumori where MekarHinban = @mh Order By MitumoriNo desc";
            da.SelectCommand.Parameters.AddWithValue("@mh", selectedValue);
            DataMitumori.T_MitumoriBackupDataTable dt = new DataMitumori.T_MitumoriBackupDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMitumori.T_MitumoriHeaderDataTable GetMitsuHeadKensaku(ClassKensaku.KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_MitumoriHeader order by MitumoriNo DESC";
            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static string GetKensakuMitsu(string[] codeAry, string text, SqlConnection sqlConnection)
        {
            string dtN = "";
            for (int i = 0; i < codeAry.Length; i++)
            {
                string code = codeAry[i];
                SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
                da.SelectCommand.CommandText =
                    "select MitumoriNo, RowNo from T_Mitumori where MitumoriNo = '12600182' and MekarHinban = '1000629492'";
                //da.SelectCommand.Parameters.AddWithValue("@no", code);
                //da.SelectCommand.Parameters.AddWithValue("@mh", text);
                DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
                da.Fill(dt);
                for (int c = 0; c < dt.Count; c++)
                {
                    if (dtN == "")
                    {
                        dtN += dt[c].MitumoriNo;
                    }
                    else
                    {
                        dtN += "," + dt[c].MitumoriNo;
                    }
                }
            }
            return dtN;
        }

        public static DataMitumori.M_Facility_NewRow GetTyokusosaki(string vsID, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Facility_New Where FacilityName1 = @CD";
            da.SelectCommand.Parameters.AddWithValue("@CD", vsID);

            DataMitumori.M_Facility_NewDataTable dt = new DataMitumori.M_Facility_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMitumori.M_Facility_NewRow;
            else
                return null;
        }

        public static DataMitumori.M_Syohin_NewRow GetMSyouhin(string vsID, string sShiire, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Syohin_New Where MekarHinban = @Code AND ShiireMei = @Shiire";
            da.SelectCommand.Parameters.AddWithValue("@Code", vsID);
            da.SelectCommand.Parameters.AddWithValue("@Shiire", sShiire);

            DataMitumori.M_Syohin_NewDataTable dt = new DataMitumori.M_Syohin_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMitumori.M_Syohin_NewRow;
            else
                return null;
        }

        public static DataMitumori.M_TantoRow GetMTanto(string Code, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Tanto Where UserID = @ID";
            da.SelectCommand.Parameters.AddWithValue("@ID", Code);

            DataMitumori.M_TantoDataTable dt = new DataMitumori.M_TantoDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataMitumori.M_TantoRow;
            else
                return null;
        }

        public static int getMitumoriNo(SqlConnection sqlConnection)
        {
            SqlCommand command = new SqlCommand("", sqlConnection);
            command.CommandText = @"Select IsNull(Max(MitumoriNo),0)+1 From [T_Mitumori]";

            try
            {
                sqlConnection.Open();

                object obj = command.ExecuteScalar();
                if (null == obj || System.DBNull.Value == obj)
                    return 0;

                return (int)obj;
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

        public static bool InsertMitumori(DataMitumori.T_MitumoriDataTable dt, DataMitumori.T_MitumoriHeaderDataTable Hdt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_Mitumori";
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());

            SqlDataAdapter Hda = new SqlDataAdapter("", sqlConnection);
            Hda.SelectCommand.CommandText
                = "SELECT * FROM T_MitumoriHeader";
            Hda.InsertCommand = (new SqlCommandBuilder(Hda).GetInsertCommand());

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;
                Hda.SelectCommand.Transaction = Hda.InsertCommand.Transaction = sql;

                da.Update(dt);
                Hda.Update(Hdt);

                sql.Commit();

                return false;
            }
            catch
            {
                if (null != sql)
                    sql.Rollback();
                return true;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMitumori.T_MitumoriDataTable GetMitumoriTable(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Mitumori Where MitumoriNo = @mNo";
            da.SelectCommand.Parameters.AddWithValue("@mNo", vsNo);

            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool DelMitumori(string vsNo, SqlConnection sqlConnection)
        {
            SqlCommand command = new SqlCommand("", sqlConnection);
            string sql = string.Format("Delete [T_Mitumori] Where MitumoriNo IN ({0})", vsNo);
            command.CommandText = sql;

            SqlCommand command2 = new SqlCommand("", sqlConnection);
            string sql2 = string.Format("Delete [T_MitumoriHeader] Where MitumoriNo IN ({0})", vsNo);
            command2.CommandText = sql2;

            try
            {
                sqlConnection.Open();

                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMitumori.T_MitumoriBackupDataTable GETMitsumori2(string mNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_MitumoriBackup Where (MitumoriNo = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", mNo);

            DataMitumori.T_MitumoriBackupDataTable dt = new DataMitumori.T_MitumoriBackupDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.T_MitumoriHeaderBackupDataTable GETMitsumorihead2(string mNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_MitumoriHeaderBackup Where (MitumoriNo = @e) Order By MitumoriNo desc";
            da.SelectCommand.Parameters.AddWithValue("@e", mNo);

            DataMitumori.T_MitumoriHeaderBackupDataTable dt = new DataMitumori.T_MitumoriHeaderBackupDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static bool UpDateMitumori(string vsNo, DataMitumori.T_MitumoriDataTable dt, DataMitumori.T_MitumoriHeaderDataTable Hdt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "Delete T_Mitumori Where MitumoriNo = @NO";
            cmd.Parameters.AddWithValue("@NO", vsNo);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM T_Mitumori Where MitumoriNo = @NO";
            da.SelectCommand.Parameters.AddWithValue("@NO", vsNo);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlCommand cmd2 = new SqlCommand("", sqlConnection);
            cmd2.CommandText = "Delete T_MitumoriHeader Where MitumoriNo = @NO";
            cmd2.Parameters.AddWithValue("@NO", vsNo);

            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConnection);
            da2.SelectCommand.CommandText = "SELECT * FROM T_MitumoriHeader Where MitumoriNo = @NO";
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
            catch
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

        public static DataMitumori.T_MitumoriDataTable GetFacility(int mitumoriNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_Mitumori where MitumoriNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", mitumoriNo);
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_New1DataTable getProduct(string a, string c, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New WHERE (CategoryCode = @a) and (Makernumber like @c)";

            da.SelectCommand.Parameters.AddWithValue("@a", a + "%");
            da.SelectCommand.Parameters.AddWithValue("@c", c + "%");

            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_Kakaku_New1DataTable getProduct2(string a, string c, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New WHERE (Categoryname LIKE @e) and (SyouhinCode like @d)";

            da.SelectCommand.Parameters.AddWithValue("@e", c + "%");
            da.SelectCommand.Parameters.AddWithValue("@d", a + "%");

            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataMitumori.T_MitumoriDataTable MitumoriInsatu(string v1, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Mitumori Where MitumoriNo = @mNo";
            da.SelectCommand.Parameters.AddWithValue("@mNo", v1);


            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMitumori.T_MitumoriHeaderDataTable GetMitumoriHeader(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_MitumoriHeader Where MitumoriNo = @mNo";
            da.SelectCommand.Parameters.AddWithValue("@mNo", vsNo);
            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void UpDateMitumorijutyu(string type, string mitumoriNo, SqlConnection sqlConnection)
        {
            //見積テーブル
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Mitumori Where MitumoriNo = @mNo";
            da.SelectCommand.Parameters.AddWithValue("@mNo", mitumoriNo);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);

            //見積ヘッダーテーブル
            SqlDataAdapter daH = new SqlDataAdapter("", sqlConnection);
            daH.SelectCommand.CommandText =
                "Select * From T_MitumoriHeader Where MitumoriNo = @mNo";
            daH.SelectCommand.Parameters.AddWithValue("@mNo", mitumoriNo);
            daH.UpdateCommand = (new SqlCommandBuilder(daH)).GetUpdateCommand();
            DataMitumori.T_MitumoriHeaderDataTable dtH = new DataMitumori.T_MitumoriHeaderDataTable();
            daH.Fill(dtH);

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                daH.SelectCommand.Transaction = daH.UpdateCommand.Transaction = sql;
                if (dtH.Count > 0)
                {
                    dtH[0].JutyuFlg = "True";
                    daH.Update(dtH);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataMitumori.T_MitumoriRow dr = dt[i];

                    if (type == "Mitumori")
                    {
                        dr.JutyuFlg = true;
                    }
                    else
                    {
                        dr.JutyuFlg = false;
                    }
                    da.Update(dt);
                }
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

        public static DataMitumori.M_TokuisakiDataTable GetM_Tokuisaki(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tokuisaki";
            DataMitumori.M_TokuisakiDataTable dt = new DataMitumori.M_TokuisakiDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.M_TokuisakiRow GetTokuisakiRow(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tokuisaki";
            DataMitumori.M_TokuisakiDataTable dt = new DataMitumori.M_TokuisakiDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataMitumori.M_TokuisakiRow;
            else
                return null;
        }


        public static DataSet1.M_TyokusosakiDataTable GetTyokuso(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT TOP (100) * FROM M_Tyokusosaki";
            DataSet1.M_TyokusosakiDataTable dt = new DataSet1.M_TyokusosakiDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_Facility_NewDataTable GetShisetsu(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT TOP (100) * FROM M_Facility_New";
            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            return (dt);
        }


        internal static DataSet1.M_TyokusosakiRow GetTyokusouRow(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tyokusosaki";
            DataSet1.M_TyokusosakiDataTable dt = new DataSet1.M_TyokusosakiDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_TyokusosakiRow;
            else
                return null;
        }

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

        public static DataSet1.M_Kakaku_New1DataTable Getkakaku1DT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New";
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_New1Row Getkakaku1Row(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New1";
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_Kakaku_New1Row;
            else
                return null;
        }

        public static DataMitumori.T_MitumoriDataTable GetMitsumoriShiSetsu(string mNo, string faci, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Mitumori WHERE (MitumoriNo = @mNo) AND (SisetuMei = @faci) ";
            da.SelectCommand.Parameters.AddWithValue("@mNo", mNo);
            da.SelectCommand.Parameters.AddWithValue("@faci", faci);
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void InsertKakaku(DataSet1.M_Kakaku_New1DataTable dt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New";
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

        public static void DelMitumoriHeader(string mNo, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "DELETE FROM T_MitumoriHeader Where MitumoriNo = @k ";
            da.Parameters.AddWithValue("@k", mNo);
            SqlTransaction sqltra = null;

            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();

                da.Transaction = sqltra;

                da.ExecuteNonQuery();

                sqltra.Commit();

            }
            catch
            {
                if (sqltra != null)
                    sqltra.Rollback();

            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void DelMitumori3(string mNo, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "DELETE FROM T_Mitumori Where MitumoriNo = @k ";
            da.Parameters.AddWithValue("@k", mNo);
            SqlTransaction sqltra = null;

            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();

                da.Transaction = sqltra;

                da.ExecuteNonQuery();

                sqltra.Commit();

            }
            catch
            {
                if (sqltra != null)
                    sqltra.Rollback();

            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static void UpdateKakaku(DataSet1.M_Kakaku_New1Row dr, string mei, string cate, string media, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New where Syouhinmei=@a and Categoryname=@c and Media=@m ";
            da.SelectCommand.Parameters.AddWithValue("@a", mei);
            da.SelectCommand.Parameters.AddWithValue("@c", cate);
            da.SelectCommand.Parameters.AddWithValue("@m", media);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            DataSet1.M_Kakaku_New1DataTable dd = new DataSet1.M_Kakaku_New1DataTable();

            SqlTransaction sqlTran = null;


            try
            {

                sql.Open();
                sqlTran = sql.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                DataSet1.M_Kakaku_New1Row drThis = null;

                da.Fill(dd);

                drThis = dd[0];



                drThis.SyouhinCode = dr.SyouhinMei;

                drThis.SyouhinMei = dr.SyouhinMei;
                if (!dr.IsMakernumberNull())
                {
                    drThis.SyouhinMei = dr.SyouhinMei;
                }
                drThis.Media = dr.Media;
                if (!dr.IsHanniNull())
                {
                    drThis.Hanni = dr.Hanni;
                }
                if (!dr.IsShiireNameNull())
                {
                    drThis.ShiireName = dr.ShiireName;
                }
                if (!dr.IsHyojunKakakuNull())
                {
                    drThis.HyojunKakaku = dr.HyojunKakaku;
                }
                if (!dr.IsShiireKakakuNull())
                {
                    drThis.ShiireKakaku = dr.ShiireKakaku;
                }
                drThis.Categoryname = dr.Categoryname;
                if (!dr.IsRentalNull())
                {
                    drThis.Rental = dr.Rental;
                }

                da.Update(dd);
                sqlTran.Commit();

            }
            catch
            {
                if (null != sqlTran)
                    sqlTran.Rollback();

            }

            finally
            {
                sql.Close();
            }

        }

        public static DataSet1.M_TokuisakiDataTable TokuisakiMei(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT TOP (100) * FROM M_Tokuisaki";
            DataSet1.M_TokuisakiDataTable dt = new DataSet1.M_TokuisakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TokuisakiDataTable TyokusousakiMei(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT TyokusousakiMei1, TyokusousakiCode FROM M_Tokuisaki";
            DataSet1.M_TokuisakiDataTable dt = new DataSet1.M_TokuisakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Shiire_NewDataTable GetShiireDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Shiire_New";
            DataSet1.M_Shiire_NewDataTable dt = new DataSet1.M_Shiire_NewDataTable();
            da.Fill(dt);
            return dt;
        }


        public static DataSet1.M_KakakuRow SerchPriceRow(string txt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku WHERE SyouhinMei LIKE '[txt] = @name%'";
            da.SelectCommand.Parameters.AddWithValue("@name", txt);
            DataSet1.M_KakakuDataTable dt = new DataSet1.M_KakakuDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_KakakuRow;
            else
                return null;
        }

        public static DataSet1.M_HanniDataTable GetHanniDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Hanni";
            DataSet1.M_HanniDataTable dt = new DataSet1.M_HanniDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static void DeleteList(string mei, string cate, string media, SqlConnection sql)
        {
            SqlCommand da = new SqlCommand("", sql);
            da.CommandText =
                "DELETE FROM M_Kakaku_New where [SyouhinMei] = @m and [Categotyname] = @c and [Media] = @me ";//SdlNoを取ってくる
            da.Parameters.AddWithValue("@k", mei);
            da.Parameters.AddWithValue("@c", cate);
            da.Parameters.AddWithValue("@me", media);

            SqlTransaction sqltra = null;


            try
            {
                sql.Open();
                sqltra = sql.BeginTransaction();

                da.Transaction = sqltra;

                da.ExecuteNonQuery();

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

        public static DataSet1.M_TyokusosakiDataTable GetTyokusou(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT TOP 100 * FROM M_Tyokusosaki";
            DataSet1.M_TyokusosakiDataTable dt = new DataSet1.M_TyokusosakiDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_Tyokusosaki1DataTable GetTyokusou1(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT TOP 20 * FROM M_Tyokusosaki where (TyokusousakiMei1 like @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", v + "%");

            DataSet1.M_Tyokusosaki1DataTable dt = new DataSet1.M_Tyokusosaki1DataTable();
            da.Fill(dt);
            return (dt);
        }


        public static DataMitumori.T_MitumoriDataTable GetMitsumoriDT(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT  * FROM T_Mitumori";
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.T_MitumoriHeaderDataTable GETMitsumorihead(string mNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_MitumoriHeader Where (MitumoriNo = @e) Order By MitumoriNo desc";
            da.SelectCommand.Parameters.AddWithValue("@e", mNo);

            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.T_MitumoriDataTable GETMitsumori(string mNo, SqlConnection sqlConnection)
        {

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Mitumori Where (MitumoriNo = @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", mNo);

            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return (dt);

        }

        public static DataSet1.M_Kakaku_New1DataTable GetProduct(string a, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New WHERE (Categoryname LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", a + "%");
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static void InsertMitsumori(DataMitumori.T_MitumoriRow dt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Mitumori";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();

            SqlTransaction sqltra = null;

            try
            {
                sql.Open();
                sqltra = sql.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                DataMitumori.T_MitumoriDataTable dtN = new DataMitumori.T_MitumoriDataTable();
                DataMitumori.T_MitumoriRow drN = dtN.NewT_MitumoriRow();
                drN.ItemArray = dt.ItemArray;
                dtN.AddT_MitumoriRow(drN);
                da.Update(dtN);
                sqltra.Commit();
            }
            catch (Exception ex)
            {
                if (sqltra != null)
                {
                    sqltra.Rollback();
                }
                ClassMail.ErrorMail("maeda@m2m-asp.com", "エラーメール | 見積登録処理",
                    ex.Message +
                    dt.MitumoriNo + "\r\n"
                    + dt.TokuisakiCode + "\r\n"
                    + dt.TokuisakiMei + "\r\n"
                    + dt.SisetuMei + "\r\n"
                    + dt.SisetsuAbbreviration);
            }
            finally
            {
                sql.Close();
            }

        }

        public static void InsertMitsumoriHeader(DataMitumori.T_MitumoriHeaderDataTable dt, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_MitumoriHeader";
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

        public static DataMitumori.T_MitumoriHeaderDataTable GetMitsuHead(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_MitumoriHeader order by MitumoriNo DESC";
            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMitumori.T_MitumoriHeaderRow GetMaxNo(int ki, SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT MitumoriNo AS MitumoriNo  FROM T_MitumoriHeader where MitumoriNo like @ki order by MitumoriNo desc";
            da.SelectCommand.Parameters.AddWithValue("@ki", "%" + ki.ToString() + "%");
            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataMitumori.T_MitumoriHeaderRow;
            else
                return null;
        }

        public static DataSet1.M_Shiire_NewDataTable GetShiireDT(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Shiire_New WHERE (Abbreviation LIKE @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", v + "%");
            DataSet1.M_Shiire_NewDataTable dt = new DataSet1.M_Shiire_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void UpdateMitumori(string mNo, DataMitumori.T_MitumoriDataTable dt, int rn, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Mitumori Where MitumoriNo = @ID AND RowNo  = @row";
            da.SelectCommand.Parameters.AddWithValue("@ID", mNo);
            da.SelectCommand.Parameters.AddWithValue("@row", rn);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataMitumori.T_MitumoriDataTable Tdt = new DataMitumori.T_MitumoriDataTable();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                da.Fill(Tdt);

                DataMitumori.T_MitumoriRow dr = Tdt[0];
                for (int i = 0; i < dt.Count; i++)
                {
                    dr.MitumoriNo = mNo;

                    dr.RowNo = dt[i].RowNo;
                    if (!dt[i].IsTokuisakiCodeNull())
                    {
                        dr.TokuisakiCode = dt[i].TokuisakiCode;
                    }
                    if (!dt[i].IsSeikyusakiMeiNull())
                    {
                        dr.SeikyusakiMei = dt[i].SeikyusakiMei;
                    }
                    if (!dt[i].IsCateGoryNull())
                    {
                        dr.CateGory = dt[i].CateGory;
                    }
                    if (!dt[i].IsCategoryNameNull())
                    {
                        dr.CategoryName = dt[i].CategoryName;
                    }
                    if (!dt[i].IsTyokusousakiCDNull())
                    {
                        dr.TyokusousakiCD = dt[i].TyokusousakiCD;
                    }
                    if (!dt[i].IsTyokusousakiMeiNull())
                    {
                        dr.TyokusousakiMei = dt[i].TyokusousakiMei;
                    }
                    if (!dt[i].IsSisetuCodeNull())
                    {
                        dr.SisetuCode = dt[i].SisetuCode;
                    }
                    if (!dt[i].IsSisetuJusyo1Null())
                    {
                        dr.SisetuJusyo1 = dt[i].SisetuJusyo1;
                    }
                    if (!dt[i].IsSyouhinCodeNull())
                    {
                        dr.SyouhinCode = dt[i].SyouhinCode;
                    }
                    if (!dt[i].IsSyouhinMeiNull())
                    {
                        dr.SyouhinMei = dt[i].SyouhinMei;
                    }
                    if (!dt[i].IsMekarHinbanNull())
                    {
                        dr.MekarHinban = dt[i].MekarHinban;
                    }
                    if (!dt[i].IsRangeNull())
                    {
                        dr.Range = dt[i].Range;
                    }
                    if (!dt[i].IsKeitaiMeiNull())
                    {
                        dr.KeitaiMei = dt[i].KeitaiMei;
                    }
                    if (!dt[i].IsHyojunKakakuNull())
                    {
                        dr.HyojunKakaku = dt[i].HyojunKakaku;
                    }
                    if (!dt[i].IsJutyuSuryouNull())
                    {
                        dr.JutyuSuryou = dt[i].JutyuSuryou;
                    }
                    if (!dt[i].IsJutyuTankaNull())
                    {
                        dr.JutyuTanka = dt[i].JutyuTanka;
                    }
                    if (!dt[i].IsShiireTankaNull())
                    {
                        dr.ShiireTanka = dt[i].ShiireTanka;
                    }
                    if (!dt[i].IsShiireKingakuNull())
                    {
                        dr.ShiireKingaku = dt[i].ShiireKingaku;
                    }
                    if (!dt[i].IsUriageNull())
                    {
                        dr.Uriage = dt[i].Uriage;
                    }
                    if (!dt[i].IsMitumoriBiNull())
                    {
                        dr.MitumoriBi = dt[i].MitumoriBi;
                    }
                    if (!dt[i].IsTourokuNameNull())
                    {
                        dr.TourokuName = dt[i].TourokuName;
                    }
                    if (!dt[i].IsTanTouNameNull())
                    {
                        dr.TanTouName = dt[i].TanTouName;
                    }
                    if (!dt[i].IsBasyoNull())
                    {
                        dr.Busyo = dt[i].Busyo;
                    }
                    if (!dt[i].IsZeikubunNull())
                    {
                        dr.Zeikubun = dt[i].Zeikubun;
                    }
                    if (!dt[i].IsWareHouseNull())
                    {
                        dr.WareHouse = dt[i].WareHouse;
                    }
                    if (!dt[i].IsKeitaiMeiNull())
                    { dr.KeitaiMei = dt[i].KeitaiMei; }

                    if (!dt[i].IsHttyuSakiMeiNull())
                    { dr.HttyuSakiMei = dt[i].HttyuSakiMei; }
                }
                da.Update(Tdt);
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

        public static void UpdateMitumoriHeader(string no, DataMitumori.T_MitumoriHeaderDataTable dd, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_MitumoriHeader Where MitumoriNo = @ID";
            da.SelectCommand.Parameters.AddWithValue("@ID", no);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataMitumori.T_MitumoriHeaderDataTable Tdt = new DataMitumori.T_MitumoriHeaderDataTable();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                da.Fill(Tdt);

                DataMitumori.T_MitumoriHeaderRow dr = Tdt[0];
                dr.ItemArray = dd[0].ItemArray;
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

        public static void UpdateMitumoriHeader(string no, SqlConnection sqlConnection)
        {
            throw new NotImplementedException();
        }

        public static DataMitumori.T_MitumoriDataTable GetmitumoriRow(string mNo, int rn, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Mitumori where (MitumoriNo = @ID) and (RowNo = @row) ";
            da.SelectCommand.Parameters.AddWithValue("@ID", mNo);
            da.SelectCommand.Parameters.AddWithValue("@row", rn);

            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            return (dt);

        }



        public static void DelMitumori2(string mNo, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "DELETE FROM T_Mitumori where [MitumoriNo] = @k ";//SdlNoを取ってくる
            da.Parameters.AddWithValue("@k", mNo);// Class1.DeleteList(sdl, Global.GetConnection()); のsdl

            SqlTransaction sqltra = null;


            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();

                da.Transaction = sqltra;

                da.ExecuteNonQuery();

                sqltra.Commit();
            }
            catch
            {
                if (sqltra != null)
                    sqltra.Rollback();

            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataMitumori.T_MitumoriDataTable GetMitumoriTable2(string mNo, int row, SqlConnection sqlConnection)
        {
            throw new NotImplementedException();
        }

        public static void UpDateMitumori2(DataMitumori.T_MitumoriRow dg, string mno, SqlConnection sqlConnection)
        {
            string no = dg.MitumoriNo;
            string row = dg.RowNo.ToString();
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Mitumori where MitumoriNo = @no and RowNo = @row";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            da.SelectCommand.Parameters.AddWithValue("@row", row);
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            da.Fill(dt);
            SqlTransaction sqltra = null;
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                if (dt.Count == 0)
                {
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqltra;
                    DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
                    dr.ItemArray = dg.ItemArray;
                    dr.MitumoriNo = mno;
                    dt.AddT_MitumoriRow(dr);
                    da.Update(dt);
                    sqltra.Commit();
                }
                else
                {
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                    dt[0].ItemArray = dg.ItemArray;
                    dt[0].MitumoriNo = mno;
                    da.Update(dt);
                    sqltra.Commit();
                }
            }
            catch
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

        public static void UpdateFromPortaltoMitumori(DataMitumori.T_MitumoriHeaderDataTable dp, string strPortalNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from T_MitumoriHeader where MitumoriNo = @mn";
            da.SelectCommand.Parameters.AddWithValue("@mn", strPortalNo);
            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            SqlTransaction sqltra = null;
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            try
            {
                sqlConnection.Open();
                sqltra = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqltra;
                dt[0].ItemArray = dp[0].ItemArray;
                dt[0].JutyuFlg = "False";
                da.Update(dt);
                sqltra.Commit();
            }
            catch
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
    }
}
