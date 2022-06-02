using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class ClassKensaku
    {
        public class WhereGenerator
        {
            private System.Collections.ArrayList m_lstWhere = new System.Collections.ArrayList();

            public string WhereText
            {
                get
                {
                    string str = "";
                    for (int i = 0; i < m_lstWhere.Count; i++)
                    {
                        str += " " + m_lstWhere[i].ToString() + " ";
                        if (i < m_lstWhere.Count - 1) str += " AND ";
                    }

                    return str;
                }
            }

            public void Add(string strWhere)
            {
                strWhere = strWhere.Trim();
                if (null != strWhere && "" != strWhere)
                    m_lstWhere.Add(strWhere);
            }

            public WhereGenerator()
            {
            }
        }

        public static DataMaster.T_OshiraseDataTable GetOshirase(KensakuParam p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "select * from T_Oshirase";
            if (p != null)
            {
                WhereGenerator w = new WhereGenerator();
                p.SetWhere(da, w);
                if (!string.IsNullOrEmpty(w.WhereText))
                {
                    da.SelectCommand.CommandText += " Where" + w.WhereText;
                }
            }
            da.SelectCommand.CommandText += " Order by CreateDate desc";
            DataMaster.T_OshiraseDataTable dt = new DataMaster.T_OshiraseDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataLedger.T_Nyukin2DataTable GetNyukin2(KensakuParam p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Nyukin2";
            if (p != null)
            {
                WhereGenerator w = new WhereGenerator();
                p.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By CreateDate desc";

            DataLedger.T_Nyukin2DataTable dt = new DataLedger.T_Nyukin2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataMitumori.T_MitumoriHeaderBackupDataTable GetMitumori2(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_MitumoriHeaderBackup";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By MitumoriNo desc";

            DataMitumori.T_MitumoriHeaderBackupDataTable dt = new DataMitumori.T_MitumoriHeaderBackupDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataReturn.T_ReturnDataTable GetReturnMeisaiSerch(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Return";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By SisetuCode desc";

            DataReturn.T_ReturnDataTable dt = new DataReturn.T_ReturnDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.T_OrderedDataTable GetOrderedLedger(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Ordered";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By SisetuCode desc";

            DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataUriage.T_UriageDataTable GetUriageLedger(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_Uriage";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By SisetuCode desc";

            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            return dt;
        }

        public class KensakuParam
        {
            //受注No
            public string sJutyuNo = null;
            public Core.Type.NengappiKikan KeijoBi = null;
            //見積検索
            public string sMitimoriNo = null;
            public string sTokuisaki = null;
            public string sSeikyu = null;
            public string sTyokuso = null;
            public string sSisetu = null;
            public string sCate = null;
            public string sBumon = null;
            public string sHinban = null;
            public string sHinmei = null;
            public string sTanto = null;
            public string sNyuryoku = null;
            public string Kikan1 = null;//表示期間用
            public string Kikan2 = null;//表示期間用
            public string sFlg = null;
            public string uFlg = null;
            public string oFlg = null;
            public string sShiire = null;
            public Core.Type.NengappiKikan JucyuBi = null;


            public string ProductCode = null;
            public string ProductName = null;
            public string SupplierCode = null;
            public string SupplierName = null;
            public string MediumName = null;
            public string CategoryNo = null;
            public string CategoryName = null;
            public string Price = null;
            public string SupplyPrice = null;

            public string SyouhinCode = null;
            public string TyokusousakiMei1 = null;
            public string tokuisakimei = null;
            public string nouhinsaki = null;
            public string tokuiTObumonTOtanto = null;
            public string Productname = null;
            public string Busyo = null;
            public string ShiyouShisetsu = null;
            public string oCate = null;
            public string sUriageNo = null;
            public string uriFlg = null;
            public string rFlg = null;
            public string RetrunHeaderNo = null;
            public string TokuisakiName = null;
            public string Seikyusaki = null;
            public string TyokusosakiMei = null;
            public string Facility = null;
            public string ReturnNo = null;
            public string sMedia = null;
            public string sTantoMei = null;
            public string sReturnDate = null;
            public string FacilityCode = null;
            public string TantoName = null;
            public string LFlg = null;
            public string sCity = null;
            public string TokuisakiC = null;
            public DateTime? CreateDate = null;
            public string No = null;
            public string TorihikiKubun = null;
            public string NyukinDate = null;
            public string KouzaNo = null;
            public string FurikomiIrai = null;
            public string TokuisakiCode = null;
            public string FurikomiBank = null;
            public string FurikomiShiten = null;
            public string NyukinKingaku = null;
            public string CreateUser = null;
            public string OshiraseNaiyou = null;
            public string CreateOshiraseDate = null;
            public string CreateDate_Oshirase = null;

            //見積検索
            internal void SetWhere(SqlDataAdapter da, WhereGenerator w)
            {
                if (CreateDate_Oshirase != null)
                {
                    w.Add("CreateDate between @cd and @cd2 ");
                    da.SelectCommand.Parameters.AddWithValue("@cd", CreateDate_Oshirase);
                    da.SelectCommand.Parameters.AddWithValue("@cd2", CreateDate_Oshirase += " 23:59:59.998");
                }


                if (CreateOshiraseDate != null)
                {
                    w.Add("CreateDate like @c");
                    da.SelectCommand.Parameters.AddWithValue("@c", "%" + CreateOshiraseDate + "%");
                }


                if (CreateUser != null)
                {
                    w.Add("CreateUser = @c");
                    da.SelectCommand.Parameters.AddWithValue("@c", CreateUser);
                }

                if (OshiraseNaiyou != null)
                {
                    w.Add("OshiraseNaiyou like @o");
                    da.SelectCommand.Parameters.AddWithValue("@o", "%" + OshiraseNaiyou + "%");
                }

                if (TokuisakiC != null)
                {
                    w.Add("TokuisakiCode = @e");
                    da.SelectCommand.Parameters.AddWithValue("@e", TokuisakiC);
                }


                if (sReturnDate != null)
                {
                    w.Add("EndDate = @e");
                    da.SelectCommand.Parameters.AddWithValue("@e", sReturnDate);
                }
                if (sTantoMei != null)
                {
                    w.Add("TanTouName = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", sTantoMei);
                }
                if (ReturnNo != null)
                {
                    w.Add("ReturnNo = @r");
                    da.SelectCommand.Parameters.AddWithValue("@r", ReturnNo);
                }
                if (Facility != null)
                {
                    w.Add("SisetuCode = @s");
                    da.SelectCommand.Parameters.AddWithValue("@s", Facility);
                }
                if (rFlg != null)
                {
                    w.Add("ReturnFlg = @flg");
                    da.SelectCommand.Parameters.AddWithValue("@flg", rFlg);
                }
                if (RetrunHeaderNo != null)
                {
                    w.Add("ReturnHeaderNo = @no");
                    da.SelectCommand.Parameters.AddWithValue("@no", RetrunHeaderNo);
                }
                if (uFlg != null)
                {
                    w.Add("Relay = @Flg");
                    da.SelectCommand.Parameters.AddWithValue("@Flg", uFlg);
                }
                if (uriFlg != null)
                {
                    w.Add("HatyuFlg = @Flg");
                    da.SelectCommand.Parameters.AddWithValue("@Flg", uriFlg);
                }
                if (oCate != null)
                {
                    w.Add("CategoryName = @cate");
                    da.SelectCommand.Parameters.AddWithValue("@cate", oCate);
                }
                if (sFlg != null)
                {
                    w.Add("JutyuFlg = @Flg");
                    da.SelectCommand.Parameters.AddWithValue("@Flg", sFlg);
                }
                if (sJutyuNo != null)
                {
                    w.Add("JutyuNo=@No");
                    da.SelectCommand.Parameters.AddWithValue("@No", sJutyuNo);
                }
                if (sUriageNo != null)
                {
                    w.Add("sUriageNo = @uri");
                    da.SelectCommand.Parameters.AddWithValue("@uri", sUriageNo);
                }
                if (sShiire != null)
                {
                    w.Add("ShiiresakiName=@shi");
                    da.SelectCommand.Parameters.AddWithValue("@shi", sShiire);
                }
                if (oFlg != null)
                {
                    w.Add("InsertFlg = @o");
                    da.SelectCommand.Parameters.AddWithValue("@o", oFlg);
                }
                if (sMitimoriNo != null)
                {
                    w.Add("MitumoriNo=@No");
                    da.SelectCommand.Parameters.AddWithValue("@No", sMitimoriNo);
                }
                if (sTokuisaki != null)
                {
                    w.Add("TokuisakiRyakusyo = @TCode");
                    da.SelectCommand.Parameters.AddWithValue("@TCode", sTokuisaki);
                }
                if (sSeikyu != null)
                {
                    w.Add("SeikyusakiName=@sName");
                    da.SelectCommand.Parameters.AddWithValue("@sName", sSeikyu);
                }
                if (sTyokuso != null)
                {
                    w.Add("TyokusosakiName = @TCD");
                    da.SelectCommand.Parameters.AddWithValue("@TCD", sTyokuso);
                }
                if (sSisetu != null)
                {
                    w.Add("FacilityName=@sMei");
                    da.SelectCommand.Parameters.AddWithValue("@sMei", sSisetu);
                }
                if (sCate != null)
                {
                    w.Add("CateGory=@Cate");
                    da.SelectCommand.Parameters.AddWithValue("@Cate", sCate);
                }
                if (sBumon != null)
                {
                    w.Add("Bumon=@Bumon");
                    da.SelectCommand.Parameters.AddWithValue("@Bumon", sBumon);
                }
                if (sHinban != null)
                {
                    w.Add("MekarHinban=@Hinban");
                    da.SelectCommand.Parameters.AddWithValue("@Hinban", sHinban);
                }
                if (sHinmei != null)
                {
                    w.Add("SyouhinMei = @Hinmei");
                    da.SelectCommand.Parameters.AddWithValue("@Hinmei", sHinmei);
                }
                if (sTanto != null)
                {
                    w.Add("TanToName = @TName");
                    da.SelectCommand.Parameters.AddWithValue("@TName", sTanto);
                }
                if (sNyuryoku != null)
                {
                    w.Add("TourokuName = @TTName");
                    da.SelectCommand.Parameters.AddWithValue("TTName", sNyuryoku);
                }
                if (JucyuBi != null)
                {
                    w.Add(JucyuBi.GenerateSQLAsDateTime("T_MitumoriHeader.CreateDate"));//変更　2012/05
                }
                if (ProductCode != null)
                {
                    w.Add("UriageFlg = @Flg");
                    da.SelectCommand.Parameters.AddWithValue("@Flg", uFlg);
                }
                if (ProductName != null)
                {
                    w.Add(string.Format("SyouhinCode = '{0}'", ProductName));
                }
                if (SyouhinCode != null)
                {
                    w.Add("SyouhinCode = @cd");
                    da.SelectCommand.Parameters.AddWithValue("@cd", SyouhinCode);
                }
                if (TyokusousakiMei1 != null)
                {
                    w.Add(string.Format("TyokusousakiMei1 LIKE '%{0}%'", TyokusousakiMei1));
                }
                if (tokuisakimei != null)
                {
                    w.Add(string.Format("TokuisakiName1 LIKE '%{0}%'", tokuisakimei));
                }
                if (nouhinsaki != null)
                {
                    w.Add(string.Format("TyokusousakiMei1 LIKE '%{0}%'", nouhinsaki));
                }
                if (tokuiTObumonTOtanto != null)
                {
                    w.Add(string.Format("TokuisakiCode = '{0}'", tokuiTObumonTOtanto));
                }
                if (ProductCode != null)
                {
                    w.Add(string.Format("MakerHinban Like '%{0}%'", ProductCode));
                }
                if (CategoryName != null)
                {
                    w.Add(string.Format("CategoryCode = '%{0}%'", CategoryName));
                }
                if (Productname != null)
                {
                    w.Add(string.Format("Categoryname = '%{0}%'", Productname));
                }
                if (Busyo != null)
                {
                    w.Add(string.Format("UserName = '%{0}%'", Busyo));
                }
                if (ShiyouShisetsu != null)
                {
                    w.Add(string.Format("FacilityName = '%{0}%'", ShiyouShisetsu));
                }
                if (TokuisakiName != null)
                {
                    w.Add("TokuisakiName = @Tokui");
                    da.SelectCommand.Parameters.AddWithValue("@Tokui", TokuisakiName);
                }
                if (FacilityCode != null)
                {
                    w.Add("FacilityCode = @s");
                    da.SelectCommand.Parameters.AddWithValue("@s", FacilityCode);
                }
                if (CategoryNo != null)
                {
                    w.Add("CateGory = @c");
                    da.SelectCommand.Parameters.AddWithValue("@c", CategoryNo);
                }
                if (TantoName != null)
                {
                    w.Add("TantoName = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", TantoName);
                }

                if (CreateDate != null)
                {
                    w.Add("CreateDate = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", CreateDate);
                }

                if (No != null)
                {
                    w.Add("No = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", No);
                }
                if (NyukinDate != null)
                {
                    w.Add("NyukinDate = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", NyukinDate);
                }
                if (KouzaNo != null)
                {
                    w.Add("KouzaNo = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", KouzaNo);
                }
                if (FurikomiIrai != null)
                {
                    w.Add("FurikomiIrai = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", FurikomiIrai);
                }
                if (TokuisakiCode != null)
                {
                    w.Add("TokuisakiCode = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", TokuisakiCode);
                }
                if (FurikomiBank != null)
                {
                    w.Add("FurikomiBank = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", FurikomiBank);
                }
                if (FurikomiShiten != null)
                {
                    w.Add("FurikomiShiten = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", FurikomiShiten);
                }
                if (NyukinKingaku != null)
                {
                    w.Add("NyukinKingaku = @t");
                    da.SelectCommand.Parameters.AddWithValue("@t", NyukinKingaku);
                }
            }
        }

        public static DataSet1.T_OrderedHeaderDataTable GetOrderedHeader2(KensakuParam s, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_ReturnHeader";
            if (s != null)
            {
                WhereGenerator w = new WhereGenerator();
                s.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By ShiiresakiCode desc";

            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataReturn.T_ReturnHeaderDataTable GetReturnHeader(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_ReturnHeader";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By TokuisakiCode desc";

            DataReturn.T_ReturnHeaderDataTable dt = new DataReturn.T_ReturnHeaderDataTable();
            da.Fill(dt);
            return dt;
        }


        public static DataUriage.T_UriageHeaderDataTable KensakuUriageHeader(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_UriageHeader";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By UriageNo desc";

            DataUriage.T_UriageHeaderDataTable dt = new DataUriage.T_UriageHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.T_OrderedHeaderDataTable GetOrderedHeader(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_OrderedHeader";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By OrderedNo desc";

            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable GetProduct5(string v, string cate, string strSyokaiDate, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT TOP 10 * FROM M_Kakaku_2 WHERE (CategoryCode = @c) and (Makernumber like @e) OR (CategoryCode = @c) AND (SyouhinMei like @e)";
            da.SelectCommand.Parameters.AddWithValue("@c", cate);
            da.SelectCommand.Parameters.AddWithValue("@e", "%" + v + "%");
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);
            sqlConn.Close();
            return dt;
        }

        public static DataSet1.M_Kakaku_New1DataTable GetkakakuNew(string a, string cate, string b, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New WHERE (SyouhinCode = @a) and (CategoryCode = @c) and (Media = @b)";

            da.SelectCommand.Parameters.AddWithValue("@a", a);
            da.SelectCommand.Parameters.AddWithValue("@c", cate);
            da.SelectCommand.Parameters.AddWithValue("@b", b);
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_TantoDataTable GetBusyo(KensakuParam p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Tanto";
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);
            return dt;

        }



        public static DataSet1.M_Kakaku_New1DataTable GetProduct2(string productName, string cate, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT TOP 10 * FROM M_Kakaku_New WHERE (SyouhinMei LIKE @e) and (CategoryCode = @c)";

            da.SelectCommand.Parameters.AddWithValue("@e", productName + "%");
            da.SelectCommand.Parameters.AddWithValue("@c", cate);

            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_KakakuDataTable GetM_KakakuDT(KensakuParam p, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku";
            DataSet1.M_KakakuDataTable dt = new DataSet1.M_KakakuDataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);
            return dt;
        }

        public static DataSet1.T_OrderedHeaderDataTable GetOrderedDL(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_OrderedHeader";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By OrderedNo desc";

            DataSet1.T_OrderedHeaderDataTable dt = new DataSet1.T_OrderedHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokuisakiSyousai(KensakuParam p, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Tokuisaki2";
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;
            da.Fill(dt);
            return dt;
        }




        public static DataSet1.M_Kakaku_New1DataTable GetkakakuNew(KensakuParam p, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New";
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokuisaki(KensakuParam p, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Tokuisaki2";
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);
            return dt;
        }


        public static DataSet1.M_Tyokusosaki1DataTable GetTyokusou(KensakuParam p, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Tyokusosaki";
            DataSet1.M_Tyokusosaki1DataTable dt = new DataSet1.M_Tyokusosaki1DataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);
            return dt;
        }



        public static DataSet1.M_Kakaku_New1Row GetkakakuNewRow(KensakuParam p, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New";
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);
            if (dt.Rows.Count >= 1)
                return dt[0] as DataSet1.M_Kakaku_New1Row;
            else
                return null;
        }


        public static DataMitumori.T_MitumoriHeaderDataTable GetMitumoriData(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * FROM T_MitumoriHeader";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }
            da.SelectCommand.CommandText += " Order By MitumoriNo desc";

            DataMitumori.T_MitumoriHeaderDataTable dt = new DataMitumori.T_MitumoriHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataJutyu.T_JutyuHeaderDataTable GetMitumori(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From T_JutyuHeader";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;

                da.SelectCommand.CommandText += "Order By CreateDate desc";
            }

            DataJutyu.T_JutyuHeaderDataTable dt = new DataJutyu.T_JutyuHeaderDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_New1DataTable GetProduct3(string a, string cate, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT * FROM M_Kakaku_New WHERE (Makernumber LIKE @e) and (CategoryCode = @c)";

            da.SelectCommand.Parameters.AddWithValue("@e", a + "%");
            da.SelectCommand.Parameters.AddWithValue("@c", cate);

            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;

        }

        public static DataUriage.T_UriageDataTable GetUriage(KensakuParam k, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM T_Uriage";
            if (k != null)
            {
                WhereGenerator w = new WhereGenerator();
                k.SetWhere(da, w);

                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
            }

            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable Getproduct6(string syouhincode, string cate, string media, string hanni, SqlConnection sqlConnection)
        {
            string sc = syouhincode.Trim();
            string ct = cate.Trim();
            string md = media.Trim();
            string hn = hanni.Trim();
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Kakaku_2 WHERE (CategoryCode = @c) AND (SyouhinCode = @s) and (Media = @m) and (Hanni = @h)";
            da.SelectCommand.Parameters.AddWithValue("@s", sc);
            da.SelectCommand.Parameters.AddWithValue("@c", ct);
            da.SelectCommand.Parameters.AddWithValue("@m", md);
            da.SelectCommand.Parameters.AddWithValue("@h", hn);

            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_Kakaku_NewDataTable GetKakakuDataTable(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New";
            DataSet1.M_Kakaku_NewDataTable dt = new DataSet1.M_Kakaku_NewDataTable();
            da.Fill(dt);
            return (dt);
        }

        public static DataSet1.M_Kakaku_New1Row GetKakakuRow(SqlConnection sql)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sql);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_Kakaku_New";
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return dt[0] as DataSet1.M_Kakaku_New1Row;
            else
                return null;

        }

        public static DataSet1.V_EditSyousaiDataTable GetEditSyousaiDT(KensakuParam p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM V_EditSyousai";
            DataSet1.V_EditSyousaiDataTable dt = new DataSet1.V_EditSyousaiDataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_TyokusosakiDataTable GetTyokuso(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 20 * FROM M_Tyokusosaki WHERE (TyokusousakiMei1 LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_TyokusosakiDataTable dt = new DataSet1.M_TyokusosakiDataTable();
            da.Fill(dt);
            return dt;

        }

        public static DataMaster.M_Shiire_NewDataTable GetMaker(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 20 * FROM M_Shiire_New WHERE (Abbreviation LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
            da.Fill(dt);
            return dt;

        }


        public static DataSet1.M_TantoDataTable GetBumon(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 20 * FROM M_Tanto WHERE (Busyo LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return dt;

        }

        public static DataSet1.M_TantoDataTable GetTanto(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 20 * FROM M_Tanto WHERE (UserName LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return dt;

        }

        public static DataSet1.M_TantoDataTable GetTanto2(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Tanto WHERE (UserID = @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e);
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }


        public static DataSet1.M_TantoDataTable GetBumon2(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT Busyo FROM M_Tanto WHERE (UserName = @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_TantoDataTable GetBumon3(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT Busyo FROM M_Tanto WHERE (UserName = @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_TantoDataTable dt = new DataSet1.M_TantoDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Tokuisaki2DataTable GetTokui(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 30 * FROM M_Tokuisaki2 WHERE (TokuisakiRyakusyo LIKE @e) or TokuisakiCode like @e";

            da.SelectCommand.Parameters.AddWithValue("@e", "%" + e + "%");
            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_Syohin_NewDataTable GetProduct(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Syohin_New WHERE (SyouhinMei LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_Syohin_NewDataTable dt = new DataSet1.M_Syohin_NewDataTable();
            da.Fill(dt);
            return dt;
        }


        public static DataSet1.M_Syohin_NewDataTable GetSyouhin(KensakuParam p, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_Syouhin_New";
            DataSet1.M_Syohin_NewDataTable dt = new DataSet1.M_Syohin_NewDataTable();

            WhereGenerator w = new WhereGenerator();

            p.SetWhere(da, w);

            if (!string.IsNullOrEmpty(w.WhereText))
                da.SelectCommand.CommandText += " WHERE " + w.WhereText;


            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_Kakaku_New1DataTable GetProduct1(string productName, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Kakaku_New WHERE (SyouhinMei LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", productName + "%");
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_Kakaku_New1DataTable GetProduct3(string e, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Kakaku_New WHERE (SyouhinMei LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", e + "%");
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }


        public static DataSet1.M_Kakaku_New1DataTable GetProduct2(string s, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Kakaku_New WHERE (Categoryname LIKE @e)";

            da.SelectCommand.Parameters.AddWithValue("@e", s + "%");
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_Facility_NewDataTable GetFacility(string e, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT TOP 10 * FROM M_Facility_New WHERE (Abbreviation LIKE @e) and State = '1' ";

            da.SelectCommand.Parameters.AddWithValue("@e", "%" + e + "%");
            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataSet1.M_BumonDataTable GetBumon4(string a, SqlConnection sqlConn)
        {
            throw new NotImplementedException();
        }

        public static DataSet1.M_Kakaku_New1DataTable GetProduct4(string v, string cate, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Kakaku_New WHERE (CategoryCode = @c) AND (Makernumber like @e) OR (CategoryCode = @c) AND (SyouhinMei like @e)";
            da.SelectCommand.Parameters.AddWithValue("@c", cate);
            da.SelectCommand.Parameters.AddWithValue("@e", v + "%");
            DataSet1.M_Kakaku_New1DataTable dt = new DataSet1.M_Kakaku_New1DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataSet1.M_Kakaku_2DataTable Getproduct5(string v, string cate, SqlConnection sqlConn)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            da.SelectCommand.CommandText = "SELECT TOP 200 * FROM M_Kakaku_2 WHERE (Categoryname = @c) AND (Makernumber like @e) OR (Categoryname = @c) AND (SyouhinMei like @e)";
            da.SelectCommand.Parameters.AddWithValue("@c", cate);
            da.SelectCommand.Parameters.AddWithValue("@e", v + "%");
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
