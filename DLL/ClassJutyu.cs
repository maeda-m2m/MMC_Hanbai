using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassJutyu
    {
        public static void GetJutyu(string sMitumori, DataJutyu.T_JutyuHeaderDataTable Hdt, DataJutyu.T_JutyuDataTable dt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "Delete T_JutyuHeader Where JutyuNo = @NO";
            cmd.Parameters.AddWithValue("@NO", sMitumori);

            SqlDataAdapter Hda = new SqlDataAdapter("", sqlConnection);
            Hda.SelectCommand.CommandText = "SELECT * FROM T_JutyuHeader";
            Hda.UpdateCommand = (new SqlCommandBuilder(Hda)).GetUpdateCommand();

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM T_Jutyu ";
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

        public static DataJutyu.T_JutyuDataTable GetJutyuTable(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Jutyu WHERE JutyuNo = @jNo";
            da.SelectCommand.Parameters.AddWithValue("@jNo", vsNo);

            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataJutyu.T_JutyuHeaderDataTable GetMitumori(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_JutyuHeader Order by CareateDate DESC ";
            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataJutyu.T_JutyuHeaderDataTable GetJutyuHeader(string vsNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_JutyuHeader WHERE JutyuNo = @jNo";
            da.SelectCommand.Parameters.AddWithValue("@jNo", vsNo);

            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool InsertJutyu(DataJutyu.T_JutyuDataTable dt, DataJutyu.T_JutyuHeaderDataTable Hdt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_Jutyu";
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());

            SqlDataAdapter Hda = new SqlDataAdapter("", sqlConnection);
            Hda.SelectCommand.CommandText
                = "SELECT * FROM T_JutyuHeader";
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
            catch (Exception e)
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

        public static DataJutyu.T_JutyuDataTable GetJutyu3(string mNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Jutyu where JutyuNo = @n";
            da.SelectCommand.Parameters.AddWithValue("@n", mNo);
            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool UpDateJutyu(string vsNo, DataJutyu.T_JutyuDataTable dt, DataJutyu.T_JutyuHeaderDataTable Hdt, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("", sqlConnection);
            cmd.CommandText = "Delete T_Jutyu Where JutyuNo = @NO";
            cmd.Parameters.AddWithValue("@NO", vsNo);

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM T_Jutyu Where JutyuNo = @NO";
            da.SelectCommand.Parameters.AddWithValue("@NO", vsNo);
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            SqlCommand cmd2 = new SqlCommand("", sqlConnection);
            cmd2.CommandText = "Delete T_JutyuHeader Where JutyuNo = @NO";
            cmd2.Parameters.AddWithValue("@NO", vsNo);

            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConnection);
            da2.SelectCommand.CommandText = "SELECT * FROM T_JutyuHeader Where JutyuNo = @NO";
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

        public static bool DelJutyu(string vsNo, SqlConnection sqlConnection)
        {
            SqlCommand command = new SqlCommand("", sqlConnection);
            string sql = string.Format("Delete [T_Jutyu] Where JutyuNo IN ({0})", vsNo);
            command.CommandText = sql;

            SqlCommand command2 = new SqlCommand("", sqlConnection);
            string sql2 = string.Format("Delete [T_JutyuHeader] Where JutyuNo IN ({0})", vsNo);
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

        public static void UpDateJutyuKeijo(string type, string jutyuNo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Jutyu Where JutyuNo = @jNo";
            da.SelectCommand.Parameters.AddWithValue("@jNo", jutyuNo);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();


            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataJutyu.T_JutyuRow dr = dt[i];

                    if (type == "Jutyu")
                    {
                        dr.UriageFlg = true;
                    }
                    else
                    {
                        dr.UriageFlg = false;
                    }

                    da.Update(dt);
                }

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

        public static DataJutyu.T_JutyuHeaderRow GetMaxJutyu(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT Max(JutyuNo) AS JutyuNo  FROM T_JutyuHeader";
            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataJutyu.T_JutyuHeaderRow;
            else
                return null;
        }

        public static DataJutyu.T_JutyuDataTable GetJutyu(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Jutyu";
            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return (dt);

        }

        public static DataJutyu.T_JutyuDataTable GetJutyuHeader(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_JutyuHeader";
            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return (dt);

        }

        public static void InsertJutyu2(DataJutyu.T_JutyuDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_Jutyu";
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());


            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();

            }
            catch (Exception e)
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void InsertJutyuH2(DataJutyu.T_JutyuHeaderDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_JutyuHeader";
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());


            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dt);

                sql.Commit();

            }
            catch (Exception e)
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }


        public static DataJutyu.T_JutyuHeaderRow GetJutyuHRow(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_JutyuHeader";
            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataJutyu.T_JutyuHeaderRow;
            else
                return null;
        }

        public static DataJutyu.T_JutyuHeaderDataTable GetJutyuH(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_JutyuHeader";
            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataJutyu.T_JutyuDataTable GetJutyu2(string v, string v1, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Jutyu Where JutyuNo = @jNo AND SisetuMei = @jShi ";
            da.SelectCommand.Parameters.AddWithValue("@jNo", v);
            da.SelectCommand.Parameters.AddWithValue("@jShi", v1);

            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return dt;
        }


        public static DataJutyu.T_JutyuRow GetJutyuRow(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Jutyu";
            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataJutyu.T_JutyuRow;
            else
                return null;
        }

        public static void InsertJutyuHeader(DataJutyu.T_JutyuHeaderDataTable hdt, SqlConnection sqlConnection)
        {
            SqlDataAdapter Hda = new SqlDataAdapter("", sqlConnection);
            Hda.SelectCommand.CommandText
                = "SELECT * FROM T_JutyuHeader";
            Hda.InsertCommand = (new SqlCommandBuilder(Hda).GetInsertCommand());

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                Hda.SelectCommand.Transaction = Hda.InsertCommand.Transaction = sql;

                Hda.Update(hdt);

                sql.Commit();

            }
            catch (Exception e)
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataJutyu.T_JutyuDataTable GetJutyu1(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Jutyu WHERE JutyuNo = @jNo AND WareHouse = '発注' ";
            da.SelectCommand.Parameters.AddWithValue("@jNo", v);

            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataJutyu.T_JutyuHeaderDataTable GetJutyuHeader1(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_JutyuHeader WHERE JutyuNo = @jNo";
            da.SelectCommand.Parameters.AddWithValue("@jNo", v);

            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataJutyu.T_JutyuHeaderDataTable DelJutyuHeader(string mNo, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "DELETE FROM T_JutyuHeader Where JutyuNo = @k ";
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
            catch (Exception et)
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

        public static DataJutyu.T_JutyuDataTable DelJutyu2(string mNo, SqlConnection sqlConnection)
        {
            SqlCommand da = new SqlCommand("", sqlConnection);
            da.CommandText =
                "DELETE FROM T_Jutyu Where JutyuNo = @k ";
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
            catch (Exception et)
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

        public static void UpDateJutyuHeader(string mNo, DataJutyu.T_JutyuHeaderDataTable dp, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_JutyuHeader Where JutyuNo = @ID";
            da.SelectCommand.Parameters.AddWithValue("@ID", mNo);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataJutyu.T_JutyuHeaderDataTable Tdt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(Tdt);

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                Tdt[0].ItemArray = dp[0].ItemArray;

                da.Update(Tdt);
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

        public static DataJutyu.T_JutyuRow Getjutyu5(string jutyuNo, string facility, string category, SqlConnection sqlConnection)
        {
            throw new NotImplementedException();
        }

        public static DataJutyu.T_JutyuDataTable GetJutyu4(string mNo, int rn, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Jutyu where (JutyuNo = @ID) and (RowNo = @row) ";
            da.SelectCommand.Parameters.AddWithValue("@ID", mNo);
            da.SelectCommand.Parameters.AddWithValue("@row", rn);

            DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void UpDateJutyu2(string mNo, DataJutyu.T_JutyuDataTable dg, int rn, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_Jutyu Where JutyuNO = @ID AND RowNo  = @row";
            da.SelectCommand.Parameters.AddWithValue("@ID", mNo);
            da.SelectCommand.Parameters.AddWithValue("@row", rn);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();

            DataJutyu.T_JutyuDataTable Tdt = new DataJutyu.T_JutyuDataTable();

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;

                da.Fill(Tdt);

                DataJutyu.T_JutyuRow dr = Tdt[0];
                for (int i = 0; i < dg.Count; i++)
                {
                    dr.JutyuNo = int.Parse(mNo);

                    dr.RowNo = dg[i].RowNo;
                    if (!dg[i].IsTokuisakiCodeNull())
                    {
                        dr.TokuisakiCode = dg[i].TokuisakiCode;
                    }
                    if (!dg[i].IsSeikyusakiMeiNull())
                    {
                        dr.SeikyusakiMei = dg[i].SeikyusakiMei;
                    }
                    if (!dg[i].IsCateGoryNull())
                    {
                        dr.CateGory = dg[i].CateGory;
                    }
                    if (!dg[i].IsCategoryNameNull())
                    {
                        dr.CategoryName = dg[i].CategoryName;
                    }
                    if (!dg[i].IsTyokusousakiCDNull())
                    {
                        dr.TyokusousakiCD = dg[i].TyokusousakiCD;
                    }
                    if (!dg[i].IsTyokusousakiMeiNull())
                    {
                        dr.TyokusousakiMei = dg[i].TyokusousakiMei;
                    }
                    if (!dg[i].IsSisetuCodeNull())
                    {
                        dr.SisetuCode = dg[i].SisetuCode;
                    }
                    if (!dg[i].IsSisetuJusyo1Null())
                    {
                        dr.SisetuJusyo1 = dg[i].SisetuJusyo1;
                    }
                    if (!dg[i].IsSyouhinCodeNull())
                    {
                        dr.SyouhinCode = dg[i].SyouhinCode;
                    }
                    if (!dg[i].IsSyouhinMeiNull())
                    {
                        dr.SyouhinMei = dg[i].SyouhinMei;
                    }
                    if (!dg[i].IsMekarHinbanNull())
                    {
                        dr.MekarHinban = dg[i].MekarHinban;
                    }
                    if (!dg[i].IsRangeNull())
                    {
                        dr.Range = dg[i].Range;
                    }
                    if (!dg[i].IsKeitaiMeiNull())
                    {
                        dr.KeitaiMei = dg[i].KeitaiMei;
                    }
                    if (!dg[i].IsHyojunKakakuNull())
                    {
                        dr.HyojunKakaku = dg[i].HyojunKakaku;
                    }
                    if (!dg[i].IsJutyuSuryouNull())
                    {
                        dr.JutyuSuryou = dg[i].JutyuSuryou;
                    }
                    if (!dg[i].IsJutyuTankaNull())
                    {
                        dr.JutyuTanka = dg[i].JutyuTanka;
                    }
                    if (!dg[i].IsShiireTankaNull())
                    {
                        dr.ShiireTanka = dg[i].ShiireTanka;
                    }
                    if (!dg[i].IsShiireKingakuNull())
                    {
                        dr.ShiireKingaku = dg[i].ShiireKingaku;
                    }
                    if (!dg[i].IsUriagekeijyouNull())
                    {
                        dr.Uriagekeijyou = dg[i].Uriagekeijyou;
                    }
                    if (!dg[i].IsJutyuBiNull())
                    {
                        dr.JutyuBi = dg[i].JutyuBi;
                    }
                    if (!dg[i].IsTourokuNameNull())
                    {
                        dr.TourokuName = dg[i].TourokuName;
                    }
                    if (!dg[i].IsTanTouNameNull())
                    {
                        dr.TanTouName = dg[i].TanTouName;
                    }
                    if (!dg[i].IsBasyoNull())
                    {
                        dr.Busyo = dg[i].Busyo;
                    }
                    if (!dg[i].IsZeiKubunNull())
                    {
                        dr.ZeiKubun = dg[i].ZeiKubun;
                    }
                    if (!dg[i].IsWareHouseNull())
                    {
                        dr.WareHouse = dg[i].WareHouse;
                    }
                    if (!dg[i].IsKeitaiMeiNull())
                    { dr.KeitaiMei = dg[i].KeitaiMei; }
                }
                da.Update(Tdt);
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
    }
}

