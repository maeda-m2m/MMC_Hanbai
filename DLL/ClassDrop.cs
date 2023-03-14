using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassDrop
    {
        public static DataDrop.M_TantoDataTable TantoDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto WHERE Yuko = 1";
            DataDrop.M_TantoDataTable dt = new DataDrop.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Customer_NewDataTable RyakusyoDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Customer_New";
            DataDrop.M_Customer_NewDataTable dt = new DataDrop.M_Customer_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Syohin_NewDataTable SyouhinMeiDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT SyouhinCode,SyouhinMei,Media,0 as ShiiresakiMei,MekarHinban FROM M_Syohin_New Where SyouhinCode <>''";
            DataDrop.M_Syohin_NewDataTable dt = new DataDrop.M_Syohin_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_CityDataTable CityDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_City";
            DataDrop.M_CityDataTable dt = new DataDrop.M_CityDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Kakaku_NewDataTable CateSyouhinMeiDrop(string sValue, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT SyouhinCode,SyouhinMei,Media,0 as ShiiresakiMei,CategoryCode,CategoryName,Makernumber FROM M_Kakaku_New Where CategoryCode=@Cate AND SyouhinCode <>''";
            da.SelectCommand.Parameters.AddWithValue("@Cate", sValue);
            DataDrop.M_Kakaku_NewDataTable dt = new DataDrop.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_CityDataTable GetCity2(string cc, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from M_City where CityCode = @cc";
            da.SelectCommand.Parameters.AddWithValue("@cc", cc);
            DataDrop.M_CityDataTable dt = new DataDrop.M_CityDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_CategoryDataTable GetCate(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Category";
            DataDrop.M_CategoryDataTable dt = new DataDrop.M_CategoryDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Kakaku_NewDataTable KakakuShiireDrop(string sValue, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT SyouhinCode, SyouhinMei,ShiireCode,ShiireName,Categoryname,Makernumber FROM M_Kakaku_New Where SyouhinCode=@Name";
            da.SelectCommand.Parameters.AddWithValue("@Name", sValue);
            DataDrop.M_Kakaku_NewDataTable dt = new DataDrop.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Kakaku_NewDataTable KakakuCateDrop(string sValue, string sShiire, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT SyouhinCode, SyouhinMei,ShiireName, ShiireCode ,CategoryCode,Categoryname FROM M_Kakaku_New Where SyouhinCode=@Name AND ShiireCode= @Code";
            da.SelectCommand.Parameters.AddWithValue("@Name", sValue);
            da.SelectCommand.Parameters.AddWithValue("@Code", sShiire);
            DataDrop.M_Kakaku_NewDataTable dt = new DataDrop.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Shiire_NewDataTable ShiireDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Shiire_New ";
            DataDrop.M_Shiire_NewDataTable dt = new DataDrop.M_Shiire_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_BumonRow GetBumonCode(string busyo, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Bumon Where Busyo = @Code and RiyoCode = '1' ";
            da.SelectCommand.Parameters.AddWithValue("@Code", busyo);

            DataDrop.M_BumonDataTable dt = new DataDrop.M_BumonDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataDrop.M_BumonRow;
            else
                return null;
        }

        public static DataDrop.M_Kakaku_NewDataTable GetKensakuCateSyouhin(string sCate, string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT SyouhinCode,SyouhinMei,Makernumber,Media,0 as ShiiresakiMei FROM M_Kakaku_New Where ((SyouhinMei LIKE @Name) or(Makernumber LIKE @Name))";
            da.SelectCommand.Parameters.AddWithValue("@Cate", sCate);
            da.SelectCommand.Parameters.AddWithValue("@Name", v + "%");
            DataDrop.M_Kakaku_NewDataTable dt = new DataDrop.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Kakaku_NewRow GetKakaku(string sSyouhin, string Cate, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Kakaku_New Where SyouhinCode = @Code AND CategoryCode = @Cate";
            da.SelectCommand.Parameters.AddWithValue("@Code", sSyouhin);
            da.SelectCommand.Parameters.AddWithValue("@Cate", Cate);

            DataDrop.M_Kakaku_NewDataTable dt = new DataDrop.M_Kakaku_NewDataTable();
            da.Fill(dt);
            if (dt.Count == 1)
                return dt[0] as DataDrop.M_Kakaku_NewRow;
            else
                return null;
        }

        public static DataDrop.M_Syohin_NewDataTable KensakuMeisyu(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT distinct 0 as SyouhinCode, 0 as SyouhinMei,ShiiresakiCode,ShiiresakiMei FROM M_Syohin_New Where (ShiiresakiMei Like @Name) AND  ShiiresakiMei <> '' ";
            da.SelectCommand.Parameters.AddWithValue("@Name", v + "%");
            DataDrop.M_Syohin_NewDataTable dt = new DataDrop.M_Syohin_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_BumonRow GetBumon(string Value, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Bumon Where BumonKubun = @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", Value);
            DataDrop.M_BumonDataTable dt = new DataDrop.M_BumonDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataDrop.M_BumonRow;
            else
                return null;
        }

        public static DataDrop.M_CityDataTable GetCity(string strCity, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select *from M_City where CityName like @cn";
            da.SelectCommand.Parameters.AddWithValue("@cn", "%" + strCity + "%");
            DataDrop.M_CityDataTable dt = new DataDrop.M_CityDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_CategoryDataTable CateDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Category";
            DataDrop.M_CategoryDataTable dt = new DataDrop.M_CategoryDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_BumonDataTable BumonDorp(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Bumon where RiyoCode = '1' ";

            DataDrop.M_BumonDataTable dt = new DataDrop.M_BumonDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Facility_NewDataTable TyokusoDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Facility_New";
            DataDrop.M_Facility_NewDataTable dt = new DataDrop.M_Facility_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_CategoryRow GetCateNo(string sValue, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Category Where Category = @Name";
            da.SelectCommand.Parameters.AddWithValue("@Name", sValue);
            DataDrop.M_CategoryDataTable dt = new DataDrop.M_CategoryDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataDrop.M_CategoryRow;
            else
                return null;
        }

        public static DataDrop.M_Facility_NewDataTable KensakuTyokuso(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            //da.SelectCommand.CommandText = @"SELECT TyokusousakiCD, TyokusousakiMei FROM M_Tyokusosaki WHERE (TyokusousakiMei LIKE @Name)";
            da.SelectCommand.CommandText = @"SELECT TOP 10 * FROM M_Facility_New WHERE (FacilityName1 Like @Name) or FacilityNo like @Name";
            da.SelectCommand.Parameters.AddWithValue("@Name", v + "%");
            DataDrop.M_Facility_NewDataTable dt = new DataDrop.M_Facility_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable GetProduct(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select distinct(SyouhinMei), Media, SyouhinCode, Makernumber, Hanni, CategoryCode, ShiireCode, HyoujunKakaku, ShiireKakaku     from M_Kakaku_2 where SyouhinMei like @sm or SyouhinCode like @sm";
            da.SelectCommand.Parameters.AddWithValue("@sm", "%" + v + "%");
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_BumonRow GetBusyo(string sBumon, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_Bumon Where BumonKubun = @Bumon";
            da.SelectCommand.Parameters.AddWithValue("@Bumon", sBumon);
            DataDrop.M_BumonDataTable dt = new DataDrop.M_BumonDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataDrop.M_BumonRow;
            else
                return null;
        }

        public static DataDrop.M_Syohin_NewDataTable SyouhinShiireDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT distinct 0 as SyouhinCode, 0 as SyouhinMei,ShiiresakiCode,ShiiresakiMei FROM M_Syohin_New Where ShiiresakiMei <> '' ";
            DataDrop.M_Syohin_NewDataTable dt = new DataDrop.M_Syohin_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Customer_NewDataTable GetRyakusyou(string Text, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = @"SELECT CustomerCode, CustomerName1, Abbreviation FROM M_Customer_New WHERE (Abbreviation LIKE @Name)  ";
            da.SelectCommand.Parameters.AddWithValue("@Name", Text + "%");
            DataDrop.M_Customer_NewDataTable dt = new DataDrop.M_Customer_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_CategoryDataTable Serchcate(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT* FROM M_Category Where Category Like @Code";
            da.SelectCommand.Parameters.AddWithValue("@Code", v + "%");
            DataDrop.M_CategoryDataTable dt = new DataDrop.M_CategoryDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Kakaku_NewDataTable GetKensakuSyouhin(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT DISTINCT SyouhinCode,Makernumber,SyouhinMei,Media, 0 as ShiiresakiMei FROM M_Kakaku_New Where ((SyouhinMei Like @Name) or (Makernumber LIKE @Name))";
            da.SelectCommand.Parameters.AddWithValue("@Name", v + "%");
            DataDrop.M_Kakaku_NewDataTable dt = new DataDrop.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_BumonDataTable SerchBumon(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Bumon Where (BumonKubun Like @Code)";
            da.SelectCommand.Parameters.AddWithValue("@Code", v + "%");
            DataDrop.M_BumonDataTable dt = new DataDrop.M_BumonDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Syohin_NewDataTable GetSyouhin(string Text, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = @"SELECT SyouhinCode, SyouhinMei,ShiiresakiMei FROM M_Syohin_New WHERE (SyouhinMei LIKE @Name)  ";
            da.SelectCommand.Parameters.AddWithValue("@Name", Text + "%");
            DataDrop.M_Syohin_NewDataTable dt = new DataDrop.M_Syohin_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_ShiiresakiDataTable SetShiire(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select ShiiresakiCode, ShiiresakiRyakusyou from M_Shiiresaki";
            DataDrop.M_ShiiresakiDataTable dt = new DataDrop.M_ShiiresakiDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_TantoDataTable SerchTanto(string v, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Tanto WHERE (Yuko = 1 AND UserId Like @ID)";
            da.SelectCommand.Parameters.AddWithValue("@ID", v + "%");
            DataDrop.M_TantoDataTable dt = new DataDrop.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Customer_NewRow GetSeikyu(string sText, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Customer_New WHERE Abbreviation = @Name";
            da.SelectCommand.Parameters.AddWithValue("@Name", sText);

            DataDrop.M_Customer_NewDataTable dt = new DataDrop.M_Customer_NewDataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataDrop.M_Customer_NewRow;
            else
                return null;
        }

        public static DataDrop.M_Bumon_NewDataTable BumonNewDrop(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Bumon_New ";
            DataDrop.M_Bumon_NewDataTable dt = new DataDrop.M_Bumon_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataDrop.M_Bumon_NewDataTable GetBumon2(string a, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Bumon_New where UserName = @p ";
            da.SelectCommand.Parameters.AddWithValue("@p", a);
            DataDrop.M_Bumon_NewDataTable dt = new DataDrop.M_Bumon_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokuisaki(string strTokuisaiRyaku, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "select * from M_Tokuisaki2 where TokuisakiRyakusyo like @tr";
            da.SelectCommand.Parameters.AddWithValue("@tr", "%" + strTokuisaiRyaku + "%");
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
