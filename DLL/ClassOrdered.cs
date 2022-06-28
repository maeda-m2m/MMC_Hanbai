using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassOrdered
    {
        public static DataSet1.T_OrderedHeaderDataTable GetOrderedHeader(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_OrderedHeader order by OrderedNo desc ";
            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.T_OrderedDataTable GetOrdered(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Ordered";
            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataMaster.M_HanniRow GetHanni(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Hanni where HanniCode = @ha";
            da.SelectCommand.Parameters.AddWithValue("@ha", v);
            DataMaster.M_HanniDataTable dt = new DataMaster.M_HanniDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataMaster.M_HanniRow;
            else
                return null;
        }

        public static DataSet1.T_NyukaLogDataTable GetLog(string strOrderedNo, string strMaker, string strHanni, string strMedia, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_NyukaLog where OrderedNo = @no and MakerNo = @mak and Hanni = @ha and Media = @me";
            da.SelectCommand.Parameters.AddWithValue("@no", strOrderedNo);
            da.SelectCommand.Parameters.AddWithValue("@mak", strMaker);
            da.SelectCommand.Parameters.AddWithValue("@ha", strHanni);
            da.SelectCommand.Parameters.AddWithValue("@me", strMedia);
            DataSet1.T_NyukaLogDataTable dt = new DataSet1.T_NyukaLogDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.T_OrderedDataTable GetSyuseiOrdered(string strOrderedNoAry, string strShiireAry, string strCategoryAry, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Ordered where OrderedNo = @no and ShiiresakiCode = @shi and Category = @ca";
            da.SelectCommand.Parameters.AddWithValue("@no", strOrderedNoAry);
            da.SelectCommand.Parameters.AddWithValue("@shi", strShiireAry);
            da.SelectCommand.Parameters.AddWithValue("@ca", strCategoryAry);
            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            return dt;
        }

        public static void GetUpdateordered2(DataSet1.T_OrderedRow dl, string no, string strMaker, string strProduct, string strHanni, string strMedia, int nokori, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM T_Ordered where OrderedNo = @no and MekerNo = @Me and ProductName = @pro and Range = @ha and Media = @med";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            da.SelectCommand.Parameters.AddWithValue("@Me", strMaker);
            da.SelectCommand.Parameters.AddWithValue("@pro", strProduct);
            da.SelectCommand.Parameters.AddWithValue("@ha", strHanni);
            da.SelectCommand.Parameters.AddWithValue("@med", strMedia);

            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            DataSet1.T_OrderedDataTable dd = new DataSet1.T_OrderedDataTable();

            da.Fill(dd);
            SqlTransaction sqlTran = null;
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;

                DataSet1.T_OrderedRow drThis = dd[0];


                //drThis = dd[0];

                drThis.OrderedNo = dl.OrderedNo;
                drThis.RowNo = dl.RowNo;
                if (!dl.IsTokuisakiCodeNull())
                { drThis.TokuisakiCode = dl.TokuisakiCode; }
                if (!dl.IsTokuisakiMeiNull())
                { drThis.TokuisakiMei = dl.TokuisakiMei; }
                if (!dl.IsTokuisakiMei2Null())
                { drThis.TokuisakiMei2 = dl.TokuisakiMei2; }
                if (!dl.IsSeikyusakiMeiNull())
                { drThis.SeikyusakiMei = dl.SeikyusakiMei; }
                if (!dl.IsCategoryNull())
                { drThis.Category = dl.Category; }
                if (!dl.IsCategoryNameNull())
                { drThis.CategoryName = dl.CategoryName; }
                if (!dl.IsTyokusodsakiCDNull())
                { drThis.TyokusodsakiCD = dl.TyokusodsakiCD; }
                if (!dl.IsTyokusosakiMeiNull())
                { drThis.TyokusosakiMei = dl.TyokusosakiMei; }
                if (!dl.IsJusyo1Null())
                { drThis.Jusyo1 = dl.Jusyo1; }
                if (!dl.IsJusyo2Null())
                { drThis.Jusyo2 = dl.Jusyo2; }
                if (!dl.IsFacilityCodeNull())
                { drThis.FacilityCode = dl.FacilityCode; }
                if (!dl.IsFacilityNameNull())
                { drThis.FacilityName = dl.FacilityName; }
                if (!dl.IsFacilityJusyo1Null())
                { drThis.FacilityJusyo1 = dl.FacilityJusyo1; }
                if (!dl.IsFacilityJusyo2Null())
                { drThis.FacilityJusyo2 = dl.FacilityJusyo2; }
                if (!dl.IsSiyouKaishiNull())
                { drThis.SiyouKaishi = dl.SiyouKaishi; }
                if (!dl.IsSiyouiOwariNull())
                { drThis.SiyouiOwari = dl.SiyouiOwari; }
                if (!dl.IsHyoujyunKakakuNull())
                { drThis.HyoujyunKakaku = dl.HyoujyunKakaku; }
                if (!dl.IsNumberOfOrderedNull())
                { drThis.NumberOfOrdered = dl.NumberOfOrdered; }
                if (!dl.IsOrderedPriceNull())
                { drThis.OrderedPrice = dl.OrderedPrice; }
                if (!dl.IsOrderedAmountNull())
                { drThis.OrderedAmount = dl.OrderedAmount; }
                if (!dl.IsTaxNull())
                { drThis.Tax = dl.Tax; }
                if (!dl.IsStaffNameNull())
                { drThis.StaffName = dl.StaffName; }
                if (!dl.IsDepartmentNull())
                { drThis.Department = dl.Department; }
                if (!dl.IsDeploymentNull())
                { drThis.Deployment = dl.Deployment; }
                if (!dl.IsCapaNull())
                { drThis.Capa = dl.Capa; }
                if (!dl.IsToOrderedNameNull())
                { drThis.ToOrderedName = dl.ToOrderedName; }
                if (!dl.IsTekiyou1Null())
                { drThis.Tekiyou1 = dl.Tekiyou1; }
                if (!dl.IsTekiyou2Null())
                { drThis.Tekiyou2 = dl.Tekiyou2; }
                if (!dl.IsTyokusodsakiCDNull())
                { drThis.TyokusousakiCD = dl.TyokusousakiCD; }
                if (!dl.IsTyokusosakiMeiNull())
                { drThis.TyokusousakiMei = dl.TyokusousakiMei; }
                if (!dl.IsRangeNull())
                { drThis.Range = dl.Range; }
                if (!dl.IsProductNameNull())
                { drThis.ProductName = dl.ProductName; }
                if (!dl.IsProductCodeNull())
                { drThis.ProductCode = dl.ProductCode; }
                drThis.MekerNo = dl.MekerNo;
                if (!dl.IsMediaNull())
                { drThis.Media = dl.Media; }
                if (!dl.IsPurchaseAmountNull())
                { drThis.PurchaseAmount = dl.PurchaseAmount; }
                if (!dl.IsPurchasePriceNull())
                { drThis.PurchasePrice = dl.PurchasePrice; }
                if (!dl.IsUriageFlgNull())
                { drThis.UriageFlg = dl.UriageFlg; }
                if (!dl.IsHatyuDayNull())
                { drThis.HatyuDay = dl.HatyuDay; }
                if (!dl.IsHatyusakiMeiNull())
                { drThis.HatyusakiMei = dl.HatyusakiMei; }
                if (!dl.IsJutyuTankaNull())
                { drThis.JutyuTanka = dl.JutyuTanka; }
                if (!dl.IsJutyuSuryouNull())
                { drThis.JutyuSuryou = dl.JutyuSuryou; }
                if (!dl.IsJutyuGokeiNull())
                { drThis.JutyuGokei = dl.JutyuGokei; }
                if (!dl.IsShiireTankaNull())
                { drThis.ShiireTanka = dl.ShiireTanka; }
                if (!dl.IsShiireKingakuNull())
                { drThis.ShiireKingaku = dl.ShiireKingaku; }
                if (!dl.IsWareHouseNull())
                { drThis.WareHouse = dl.WareHouse; }

                drThis.ShiireSakiName = dl.ShiireSakiName;
                if (!dl.IsShiiresakiCodeNull())
                { drThis.ShiiresakiCode = dl.ShiiresakiCode; }
                if (!dl.IsZeikubunNull())
                { drThis.Zeikubun = dl.Zeikubun; }
                if (!dl.IsKakeritsuNull())
                { drThis.Kakeritsu = dl.Kakeritsu; }
                if (!dl.IsZasuNull())
                { drThis.Zasu = dl.Zasu; }
                if (!dl.IsRyoukinNull())
                { drThis.Ryoukin = dl.Ryoukin; }
                if (!dl.IsSyokaibiNull())
                { drThis.Syokaibi = dl.Syokaibi; }
                if (!dl.IsFukusuFaciNull())
                { drThis.FukusuFaci = dl.FukusuFaci; }
                if (!dl.IsHatyuFLGNull())
                { drThis.HatyuFLG = dl.HatyuFLG; }
                if (!dl.IsZansuNull())
                { drThis.Zansu = nokori.ToString(); }

                da.Update(dd);
                sqlTran.Commit();

            }
            catch
            {
                if (null != sqlTran)
                    sqlTran.Rollback();
            }
        }


        public static DataSet1.T_OrderedHeaderRow GetOrderedH(string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_OrderedHeader where OrderedNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.T_OrderedHeaderRow;
            else
                return null;
        }

        public static DataSet1.T_OrderedRow GetUpdateordered
            (string no, string strMaker, string strProduct, string strHanni, string strMedia, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Ordered where OrderedNo = @no and ProductName = @pro and MekerNo = @ma and Range = @ha and Media = @me";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            da.SelectCommand.Parameters.AddWithValue("@pro", strProduct);
            da.SelectCommand.Parameters.AddWithValue("@ma", strMaker);
            da.SelectCommand.Parameters.AddWithValue("@ha", strHanni);
            da.SelectCommand.Parameters.AddWithValue("@me", strMedia);
            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.T_OrderedRow;
            else
                return null;

        }

        public static DataSet1.T_OrderedDataTable GetEditOrdered
            (string strOrderedNo, string strShiire, string strMakerNo, string strHanni, string strCategory, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Ordered where OrderedNo = @no and ShiiresakiCode = @shi and MakerNo = @ma and Range = @ha and Category = @ca";
            da.SelectCommand.Parameters.AddWithValue("@no", strOrderedNo);
            da.SelectCommand.Parameters.AddWithValue("@shi", strShiire);
            da.SelectCommand.Parameters.AddWithValue("@ma", strMakerNo);
            da.SelectCommand.Parameters.AddWithValue("@ha", strHanni);
            da.SelectCommand.Parameters.AddWithValue("@ca", strCategory);
            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.T_OrderedDataTable GetOrdered2(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Ordered where OrderedNo = @no";
            da.SelectCommand.Parameters.AddWithValue("@no", v);
            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            return (dt);
        }


        public static void InsertOrdered(DataSet1.T_OrderedDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_Ordered";
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
            catch
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static void InsertOrderedHeader(DataSet1.T_OrderedHeaderDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_OrderedHeader";
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
            catch
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public static DataSet1.T_OrderedHeaderRow GetMaxOrdered(int ki, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT OrderedNo FROM T_OrderedHeader where OrderedNo like @on order by OrderedNo desc";
            da.SelectCommand.Parameters.AddWithValue("@on", "%" + ki + "%");
            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.T_OrderedHeaderRow;
            else
                return null;
        }

        public static void UpdateFlg(string[] strAry, SqlConnection sqlConnection)
        {
            for (int i = 0; i < strAry.Length; i++)
            {
                SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
                da.SelectCommand.CommandText
                    = "SELECT * FROM T_OrderedHeader Where OrderedNo = @no";
                da.SelectCommand.Parameters.AddWithValue("@no", strAry[i]);
                DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
                da.Fill(dt);

                da.UpdateCommand = (new SqlCommandBuilder(da).GetUpdateCommand());

                SqlTransaction sql = null;

                try
                {
                    sqlConnection.Open();
                    sql = sqlConnection.BeginTransaction();

                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sql;
                    dt[0].InsertFlg = true;
                    da.Update(dt);

                    sql.Commit();

                }
                catch
                {
                    if (null != sql)
                        sql.Rollback();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public static void InsertLog(DataSet1.T_NyukaLogDataTable dd, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_NyukaLog";
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());

            SqlTransaction sql = null;

            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();

                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;

                da.Update(dd);

                sql.Commit();

            }
            catch
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataSet1.T_KariOrderedDataTable GetKariOrdered(string no, string shiire, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_KariOrdered OrderedNo = @no and ShiiresakiCode = @shiire";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            da.SelectCommand.Parameters.AddWithValue("@shiire", shiire);
            DataSet1.T_KariOrderedDataTable dt = new DataSet1.T_KariOrderedDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void InsertOrdered2(DataSet1.T_OrderedDataTable dt, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText
                = "SELECT * FROM T_Ordered";
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
            catch
            {
                if (null != sql)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public static DataSet1.T_OrderedHeaderDataTable GetOrederedHeader(string cate, string shiire, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_OrderedHeader where Category = @cate and ShiireSakiName = @shiire and InsertFlg = 'False' ";
            da.SelectCommand.Parameters.AddWithValue("@cate", cate);
            da.SelectCommand.Parameters.AddWithValue("@shiire", shiire);
            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static void UpdateOrderedHeader(DataSet1.T_OrderedHeaderDataTable dth, DataSet1.T_OrderedRow dr, string cate, string shiire, SqlConnection sqlConnection)
        {
            string catecode = dr.Category.ToString();
            string shiname = dr.ShiireSakiName;
            //ヘッダーのほう
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_OrderedHeader where Category = @ca and ShiiresakiName = @shi and InsertFlg = 'False' ";
            da.SelectCommand.Parameters.AddWithValue("@ca", catecode);
            da.SelectCommand.Parameters.AddWithValue("@shi", shiname);
            DataSet1.T_OrderedHeaderDataTable dd = new DataSet1.T_OrderedHeaderDataTable();
            da.UpdateCommand = (new SqlCommandBuilder(da)).GetUpdateCommand();
            da.InsertCommand = (new SqlCommandBuilder(da).GetInsertCommand());
            da.Fill(dd);
            //明細のほう
            SqlDataAdapter dda = new SqlDataAdapter("", sqlConnection);
            dda.SelectCommand.CommandText
                = "SELECT * FROM T_Ordered where Category = @ca and ShiiresakiName = @shi order by RowNo desc";
            dda.SelectCommand.Parameters.AddWithValue("@ca", catecode);
            dda.SelectCommand.Parameters.AddWithValue("@shi", shiname);
            DataSet1.T_OrderedDataTable ddd = new DataSet1.T_OrderedDataTable();
            dda.InsertCommand = (new SqlCommandBuilder(dda).GetInsertCommand());
            SqlTransaction sqlTran = null;
            dda.Fill(ddd);
            try
            {
                sqlConnection.Open();
                sqlTran = sqlConnection.BeginTransaction();
                if (dd.Count == 0)
                {
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    dda.SelectCommand.Transaction = dda.InsertCommand.Transaction = sqlTran;
                    dth[0].ShiireKingaku = dr.ShiireKingaku;
                    dth[0].InsertFlg = false;
                    da.Update(dth);
                    DataSet1.T_OrderedDataTable dts = new DataSet1.T_OrderedDataTable();
                    DataSet1.T_OrderedRow drr = dts.NewT_OrderedRow();
                    drr.ItemArray = dr.ItemArray;
                    drr.RowNo = 1;
                    dts.AddT_OrderedRow(drr);
                    dda.Update(dts);
                    sqlTran.Commit();
                    sqlConnection.Close();
                    sqlTran = null;
                }
                else
                {
                    DataSet1.T_OrderedHeaderRow drThis = dd[0];
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dda.SelectCommand.Transaction = dda.InsertCommand.Transaction = sqlTran;

                    int kizonsu = dd[0].OrderedAmount;
                    int kizonshiire = dd[0].ShiireKingaku;
                    kizonsu += dth[0].OrderedAmount;
                    kizonshiire += dth[0].ShiireKingaku;
                    drThis.OrderedAmount = kizonsu;
                    drThis.ShiireKingaku = kizonshiire;
                    da.Update(dd);
                    int no = dd[0].OrderedNo;
                    DataSet1.T_OrderedDataTable dts = new DataSet1.T_OrderedDataTable();
                    DataSet1.T_OrderedRow drr = dts.NewT_OrderedRow();
                    drr.ItemArray = dr.ItemArray;
                    drr.OrderedNo = no;
                    drr.RowNo = ddd[0].RowNo + 1;

                    dts.AddT_OrderedRow(drr);
                    dda.Update(dts);
                    sqlTran.Commit();
                    sqlConnection.Close();
                    sqlTran = null;

                }
            }
            catch
            {
                if (null != sqlTran)
                { sqlTran.Rollback(); }
            }
            finally
            {
                sqlConnection.Close();
            }
        }



        public static DataSet1.T_OrderedHeaderDataTable GetOrderedHeader(string no, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_OrderedHeader where OrderedNo = @no ";
            da.SelectCommand.Parameters.AddWithValue("@no", no);
            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            return (dt);
        }
    }
}
