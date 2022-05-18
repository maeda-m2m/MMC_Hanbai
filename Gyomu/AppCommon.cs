using DLL;
using ExcelCreator6;
using System;
using System.Data;
using WebSupergoo.ABCpdf6;


namespace Gyomu
{
    public class AppCommon
    {
        public byte[] theData;
        public System.IO.MemoryStream ms;
        public static string strShisetumei;

        internal void MitumoriInsatu(string type, string[] strMitumoriNo, string[] strRowAry, string[] strShisetuiAry)
        {
            Doc pdf = new Doc();

            DataMitumori.T_MitumoriDataTable dt = null;
            for (int i = 0; i < strShisetuiAry.Length; i++)
            {
                dt =
                    ClassMitumori.MitumoriInsatu(strMitumoriNo[i], Global.GetConnection());

                if (dt.Count > 0)
                {
                    string facility = "";
                    for (int f = 0; f < dt.Count; i++)
                    {
                        if (facility != "")
                        { facility += ","; }

                        facility += dt[f].SisetuMei;
                    }
                    DataMitumori.T_MitumoriDataTable dd = null;
                    string[] strAry = facility.Split(',');
                    for (int q = 0; q < strAry.Length; q++)
                    {
                        dd = ClassMitumori.MitumoriInsatu2(strMitumoriNo[i], strAry[q], Global.GetConnection());
                        int gokei = 0;
                        gokei = 0;
                        int su = 0;
                        XlsxCreator xlsxCreator;
                        xlsxCreator = new XlsxCreator();
                        for (int a = 0; a < dd.Count; a++)
                        {
                            int hk = int.Parse(dd[a].HyojunKakaku);
                            gokei += hk;
                            int ryou = dd[a].JutyuSuryou;
                            su += ryou;
                        }
                        pdf = MakePdfData(type, dd, xlsxCreator, pdf, gokei, su);
                    }
                }
            }

            theData = pdf.GetData();
        }

        int nNo = 0;
        int nGoeki = 0;
        int nSyouhi = 0;
        int nMitumori = 0;
        int nSuryo = 0;
        int nTanka = 0;
        int nKinagku = 0;
        int Suryo = 0;
        int gKingaku = 0;


        internal void MitumoriInsatu2(string type, Doc pdf, string[] strMitumoriAry, string flg, string bDate)
        {
            for (int i = 0; i < strMitumoriAry.Length; i++)
            {
                DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
                DataMitumori.T_MitumoriHeaderDataTable dtH = new DataMitumori.T_MitumoriHeaderDataTable();
                DataUriage.T_UriageDataTable du = new DataUriage.T_UriageDataTable();
                DataUriage.T_UriageHeaderDataTable duH = new DataUriage.T_UriageHeaderDataTable();
                int mno = int.Parse(strMitumoriAry[i]);
                if (mno < 15000000)
                {
                    dt = ClassMitumori.GetMitumoriTable(strMitumoriAry[i], Global.GetConnection());
                    dtH = ClassMitumori.GetMitumoriHeader(strMitumoriAry[i], Global.GetConnection());
                    XlsxCreator xlsxCreator;
                    xlsxCreator = new XlsxCreator();
                    if (dtH[0].Shimebi.Trim() == "都度")
                    {
                        try
                        {
                            pdf = MakePdfData4(type, dt, xlsxCreator, pdf, flg, bDate, dtH);
                        }
                        catch (Exception ex)
                        {
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            pdf = MakePdfData6(type, dt, xlsxCreator, pdf, flg, bDate, dtH);
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
                else
                {
                    du = ClassUriage.GetUriage(strMitumoriAry[i], Global.GetConnection());
                    duH = ClassUriage.GetUriageHeader(strMitumoriAry[i], Global.GetConnection());
                    XlsxCreator xlsxCreator;
                    xlsxCreator = new XlsxCreator();
                    string[] tokuisaki = duH[0].TokuisakiCode.Split('/');
                    DataSet1.M_Tokuisaki2DataTable dtT = Class1.GetTokuisaki2(tokuisaki[0], tokuisaki[1], Global.GetConnection());

                    if (dtT[0].Shimebi.Trim() == "都度")
                    {
                        try
                        {
                            pdf = MakePdfData4u(type, du, xlsxCreator, pdf, flg, bDate, dtH);
                        }
                        catch
                        {
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            pdf = MakePdfData6u(type, du, xlsxCreator, pdf, flg, bDate, dtH);
                        }
                        catch
                        {
                            return;
                        }
                    }

                }
            }
            theData = pdf.GetData();
        }

        private Doc MakePdfData6u(string type, DataUriage.T_UriageDataTable du, XlsxCreator xlsxCreator, Doc pdf, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            int gokei = 0;
            //一枚に表示出来る最大行数 
            int iViewCount = 20;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (du.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)du.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成

            for (int i = 0; i < du.Rows.Count; i++)
            {

                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {

                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();

                        iGyousu = 1;

                    }
                    iPdfCnt++;

                    // テンプレートの読み込み
                    if (type == "Mitumori")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["MitsumoriFormatGetsuji"], "");
                    }
                    if (type == "Nouhin")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatNoTax"], "");
                    }
                    if (type == "Sekyu")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuForamatNoTax"], "");
                    }
                    if (type == "Uchiwake")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["UchiwakeFormatNoTax"], "");
                    }
                    if (type == "Denpyou")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SyukkaFormatGetsuji"], "");
                    }
                }
                int SouGokei = gokei;

                if (type == "Sekyu")
                {
                    xlsxCreator = StatementsDataSet4u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                if (type == "Mitumori")
                {
                    xlsxCreator = StatementsDataSet8u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                if (type == "Nouhin")
                {
                    xlsxCreator = StatementsDataSet5u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                if (type == "Uchiwake")
                {
                    xlsxCreator = StatementsDataSet6u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, bDate, dtH);
                }
                if (type == "Denpyou")
                {
                    xlsxCreator = StatementsDataSet7u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                // 行数カウントアップ
                iGyousu++;
            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;
        }

        private XlsxCreator StatementsDataSet7u(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            {
                DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
                DataUriage.T_UriageRow dr = dt.NewT_UriageRow();
                dr.ItemArray = dl.ItemArray;

                int kijuncell = 11;
                int plus1 = kijuncell + iGyousu * 2;
                int plus2 = kijuncell + iGyousu * 2 + 1;

                if (flg == "0")
                {
                    xlsxCreator.Cell("I9").Value = dr.Busyo + " " + dr.TanTouName;
                }
                if (bDate == "false")
                {
                    xlsxCreator.Cell("I1").Value = DateTime.Now.ToShortDateString();
                }
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
                xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.KeitaiMei;
                xlsxCreator.Cell("I" + plus1.ToString()).Value = dr.JutyuSuryou;
                xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.MekarHinban;
                xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.Range;
                if (!dr.IsTekiyou1Null())
                {
                    xlsxCreator.Cell("F" + plus2.ToString()).Value = dr.Tekiyou1;
                }
                if (!dr.IsSiyouKaishiNull() || !dr.IsSiyouOwariNull())
                {
                    xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SiyouKaishi.ToShortDateString() + " ~ " + dr.SiyouOwari.ToShortDateString();
                }
                xlsxCreator.Cell("D" + plus2.ToString()).Value = dr.SisetuMei;

                Suryo += dr.JutyuSuryou;
                xlsxCreator.Cell("I42").Value = Suryo;

                return xlsxCreator;
            }
        }

        private XlsxCreator StatementsDataSet8u(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            {

                DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
                DataUriage.T_UriageRow dr = dt.NewT_UriageRow();
                dr.ItemArray = dl.ItemArray;

                Suryo += dr.JutyuSuryou;
                gKingaku += dr.JutyuTanka;

                if (flg == "0")
                {
                    xlsxCreator.Cell("I13").Value = dr.Busyo + " " + dr.TanTouName;
                }

                int kijuncell = 15;
                int plus1 = kijuncell + iGyousu * 3;
                int plus2 = kijuncell + iGyousu * 3 + 1;
                int plus3 = kijuncell + iGyousu * 3 + 2;
                xlsxCreator.Cell("B4").Value = dr.TokuisakiMei;
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
                xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.KeitaiMei;
                xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.JutyuSuryou;
                xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.HyojunKakaku;
                xlsxCreator.Cell("I" + plus1.ToString()).Value = dr.JutyuTanka;
                xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.MekarHinban;
                xlsxCreator.Cell("D" + plus2.ToString()).Value = dr.Range;
                if (!dr.IsTekiyou1Null())
                {
                    xlsxCreator.Cell("E" + plus2.ToString()).Value = dr.Tekiyou1;
                }
                if (!dr.IsSiyouKaishiNull() || !dr.IsSiyouOwariNull())
                {
                    xlsxCreator.Cell("B" + plus3.ToString()).Value = dr.SiyouKaishi.ToShortDateString() + " ~ " + dr.SiyouOwari.ToShortDateString();
                }

                xlsxCreator.Cell("D" + plus3.ToString()).Value = dr.SisetuMei;
                xlsxCreator.Cell("B11").Value = Suryo;
                xlsxCreator.Cell("C11").Value = gKingaku;

                return xlsxCreator;

            }
        }

        private XlsxCreator StatementsDataSet4u(XlsxCreator xlsxCreator, int iGyousu, DataRow dr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            {

                if (bDate == "false")
                {
                    xlsxCreator.Cell("J1").Value = DateTime.Now.ToShortDateString();
                }
                else
                {
                    xlsxCreator.Cell("J1").Value = "年" + "        " + "月" + "        " + "日";
                }
                xlsxCreator.Cell("J2").Value = dr["UriageNo"];
                xlsxCreator.Cell("B4").Value = dr["TokuisakiMei"] + " " + "様";
                xlsxCreator.Cell("B14").Value = "使用施設名:" + dr["SisetuMei"];

                string s = dr["JutyuSuryou"].ToString();
                nSuryo += int.Parse(s);
                xlsxCreator.Cell("B9").Value = nSuryo;
                int iKijun = 16;

                xlsxCreator.Cell("A" + (iKijun + iGyousu).ToString()).Value = iGyousu;

                //品目
                if (!dr.IsNull("SyouhinMei"))
                {
                    string sm = dr["SyouhinMei"].ToString();
                    xlsxCreator.Cell("B" + (iKijun + iGyousu).ToString()).Value = sm;
                }

                if (flg == "0")
                {
                    xlsxCreator.Cell("J8").Value = dr["Busyo"] + " " + dr["TanTouName"];
                    xlsxCreator.Cell("J8").Value = "";
                }

                //品番
                if (!dr.IsNull("MekarHinban"))
                {
                    xlsxCreator.Cell("F" + (iKijun + iGyousu).ToString()).Value = Convert.ToString(dr["MekarHinban"]);
                }

                //媒体
                if (!dr.IsNull("KeitaiMei"))
                {
                    xlsxCreator.Cell("G" + (iKijun + iGyousu).ToString()).Value = Convert.ToString(dr["KeitaiMei"]);
                }

                //標準価格
                if (!dr.IsNull("HyojunKakaku"))
                {
                    int hk = int.Parse(dr["HyojunKakaku"].ToString());
                    xlsxCreator.Cell("I" + (iKijun + iGyousu).ToString()).Value = "￥" + hk.ToString("0,0");
                    nGoeki += hk;
                }

                //数量
                if (!dr.IsNull("JutyuSuryou"))
                {
                    xlsxCreator.Cell("H" + (iKijun + iGyousu).ToString()).Value = Convert.ToInt16(dr["JutyuSuryou"]);
                }

                if (!dr.IsNull("JutyuTanka"))
                {
                    int uri = int.Parse(dr["JutyuTanka"].ToString());
                    xlsxCreator.Cell("J" + (iKijun + iGyousu).ToString()).Value = "￥" + uri.ToString("0,0");
                    int h = int.Parse(dr["HyojunKakaku"].ToString());
                    gKingaku += h;
                    nSyouhi += int.Parse((h * 1.1).ToString());
                }
                xlsxCreator.Cell("C9").Value = gKingaku;
                xlsxCreator.Cell("D9").Value = nSyouhi - nGoeki;
                if (dr["Zeikubun"].ToString().Trim() == "税抜")
                {
                    xlsxCreator.Cell("E9").Value = "￥" + (gKingaku + nSyouhi - nGoeki);
                }


                return xlsxCreator;
            }
        }

        private Doc MakePdfData4u(string type, DataUriage.T_UriageDataTable du, XlsxCreator xlsxCreator, Doc pdf, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            int gokei = 0;
            //一枚に表示出来る最大行数 
            int iViewCount = 20;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (du.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)du.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成

            for (int i = 0; i < du.Rows.Count; i++)
            {

                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {

                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();

                        iGyousu = 1;

                    }
                    iPdfCnt++;

                    // テンプレートの読み込み
                    if (type == "Mitumori")
                    {
                        if (du[i].ZeiKubun == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["MitsumoriFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["MitsumoriFormatNoTax"], "");
                        }
                    }
                    if (type == "Nouhin")
                    {
                        if (du[i].ZeiKubun == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatNoTax"], "");
                        }
                    }
                    if (type == "Sekyu")
                    {
                        if (du[i].ZeiKubun == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuForamatNoTax"], "");
                        }
                    }
                    if (type == "Uchiwake")
                    {
                        if (du[i].ZeiKubun == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["UchiwakeFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["UchiwakeFormatNoTax"], "");
                        }
                    }
                    if (type == "Denpyou")
                    {
                        if (du[i].ZeiKubun == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SyukkaFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SyukkaFormatNoTax"], "");
                        }
                    }
                }
                int SouGokei = gokei;

                if (type == "Sekyu")
                {
                    xlsxCreator = StatementsDataSet4u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                if (type == "Mitumori")
                {
                    xlsxCreator = StatementsDataSetu(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                if (type == "Nouhin")
                {
                    xlsxCreator = StatementsDataSet5u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                if (type == "Uchiwake")
                {
                    xlsxCreator = StatementsDataSet6u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, bDate, dtH);
                }
                if (type == "Denpyou")
                {
                    xlsxCreator = StatementsDataSet9u(xlsxCreator, iGyousu, du.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH);
                }
                // 行数カウントアップ
                iGyousu++;
            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;

        }


        private XlsxCreator StatementsDataSet9u(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
            DataUriage.T_UriageRow dr = dt.NewT_UriageRow();
            dr.ItemArray = dl.ItemArray;

            int kijuncell = 11;
            int plus1 = kijuncell + iGyousu * 2;
            int plus2 = kijuncell + iGyousu * 2 + 1;

            if (flg == "0")
            {
                xlsxCreator.Cell("I9").Value = dr.Busyo + " " + dr.TanTouName;
                xlsxCreator.Cell("I9").Value = "";
            }
            if (bDate == "false")
            {
                xlsxCreator.Cell("I1").Value = DateTime.Now.ToShortDateString();
            }
            xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.KeitaiMei;
            xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.JutyuSuryou;
            xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.MekarHinban;
            xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.Range;
            if (!dr.IsTekiyou1Null())
            {
                xlsxCreator.Cell("I" + plus2.ToString()).Value = dr.Tekiyou1;
            }
            Suryo += dr.JutyuSuryou;
            xlsxCreator.Cell("B9").Value = "使用施設:" + " " + dr.SisetuMei;
            xlsxCreator.Cell("H54").Value = Suryo;

            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet6u(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            {
                DataUriage.T_UriageDataTable dt = new DataUriage.T_UriageDataTable();
                DataUriage.T_UriageRow dr = dt.NewT_UriageRow();
                dr.ItemArray = dl.ItemArray;

                int kijuncell = 6;
                int plus1 = kijuncell + iGyousu * 2;
                int plus2 = kijuncell + iGyousu * 2 + 1;
                if (bDate == "false")
                {
                    xlsxCreator.Cell("I1").Value = DateTime.Now.ToShortDateString();
                }
                else
                {
                    xlsxCreator.Cell("I1").Value = "年" + "        " + "月" + "        " + "日";
                }
                xlsxCreator.Cell("B4").Value = "使用施設名：" + dr.SisetuMei;
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
                xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.KeitaiMei;
                xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.JutyuSuryou;
                Suryo += dr.JutyuSuryou;
                xlsxCreator.Cell("G49").Value = Suryo;
                xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.HyojunKakaku;
                xlsxCreator.Cell("I" + plus1.ToString()).Value = dr.JutyuTanka;
                nGoeki += dr.JutyuTanka;
                xlsxCreator.Cell("I49").Value = nGoeki;
                xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.MekarHinban;
                xlsxCreator.Cell("D" + plus2.ToString()).Value = dr.Range;
                if (!dr.IsTekiyou1Null())
                {
                    xlsxCreator.Cell("E" + plus2.ToString()).Value = dr.Tekiyou1;
                }
                return xlsxCreator;
            }
        }

        private XlsxCreator StatementsDataSet5u(XlsxCreator xlsxCreator, int iGyousu, DataRow dr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            nNo++;

            if (iGyousu == 1)
            {
                nNo = 0;
                nGoeki = 0;
                nSyouhi = 0;
                nMitumori = 0;
                nSuryo = 0;
                nTanka = 0;
                nKinagku = 0;

                xlsxCreator.Cell("A1:I2").Drawing.AddImage(System.Configuration.ConfigurationManager.AppSettings["HeaderImage"]);
                xlsxCreator.Cell("A1:I2").Drawing.Init();
                if (bDate == "false")
                {
                    xlsxCreator.Cell("I2").Value = DateTime.Now.ToShortDateString();
                }
                else
                {
                    xlsxCreator.Cell("I2").Value = "年" + "        " + "月" + "        " + "日";
                }
                if (flg == "0")
                {
                    //xlsxCreator.Cell("I11").Value = dr["Busyo"] + " " + dr["TanTouName"];
                    xlsxCreator.Cell("I11").Value = "";

                }
                //見積No
                string strMitumoriNo = dr["UriageNo"].ToString();
                //得意先
                string sTokuisaki = dr["TokuisakiMei"].ToString();
                //使用施設名
                string strShisetumei = dr["SisetuMei"].ToString();
                //使用期間
                string strSiyoukikan =
                     dr["SiyouKaishi"].ToString();
                if (strSiyoukikan != "")
                {
                    DateTime dateKaisi = DateTime.Parse(strSiyoukikan);
                    string sDate = dateKaisi.ToString("yyy/MM/dd");
                }

                string strOwarikikan =
                     dr["SiyouOwari"].ToString();
                if (strOwarikikan != "")
                {
                    DateTime dateOwari = DateTime.Parse(strOwarikikan);
                    string oDate = dateOwari.ToString("yyy/MM/dd");
                }

                xlsxCreator.Cell("I1").Value = strMitumoriNo;
                xlsxCreator.Cell("B4").Value = sTokuisaki + "  様";
                xlsxCreator.Cell("C12").Value = strShisetumei;
            }

            string strSuryou = dr["JutyuSuryou"].ToString();

            int nSS = int.Parse(strSuryou);

            nSuryo += nSS;
            xlsxCreator.Cell("B9").Value = nSuryo;

            // 各明細のセット
            int iKijunCell = 15;
            int iPlus = ((2 * iGyousu) - 1);
            int iPlusUltra = 2 * iGyousu;
            int nPageCnt = 0; //ページ数分増やす 20180312

            nPageCnt = (iPdfCnt - 1) * 10;

            string syou = dr["SyouhinMei"].ToString();
            string[] arr = syou.Split('/');
            string x = arr[0];
            //品目
            if (!dr.IsNull("SyouhinMei"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus).ToString()).Value = arr[0];
            }

            //品番
            if (!dr.IsNull("MekarHinban"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(dr["MekarHinban"]);
            }

            //媒体
            if (!dr.IsNull("KeitaiMei"))
            {
                xlsxCreator.Cell("F" + (iKijunCell + iPlus).ToString()).Value = Convert.ToString(dr["KeitaiMei"]);
            }

            //標準価格
            if (!dr.IsNull("HyojunKakaku"))
            {
                int hk = int.Parse(dr["HyojunKakaku"].ToString());
                xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = "￥" + hk.ToString("0,0");
            }

            //数量
            if (!dr.IsNull("JutyuSuryou"))
            {
                xlsxCreator.Cell("G" + (iKijunCell + iPlus).ToString()).Value = Convert.ToInt16(dr["JutyuSuryou"]);
            }

            if (!dr.IsNull("Range"))
            {
                xlsxCreator.Cell("D" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(dr["Range"]);
            }


            //見積単価
            int goukei = 0;
            int kazu = 0;
            string t = dr["JutyuTanka"].ToString();
            goukei = int.Parse(t);
            string k = dr["JutyuSuryou"].ToString();
            kazu = int.Parse(k);
            int gokeikingaku = goukei * kazu;
            xlsxCreator.Cell("I" + (iKijunCell + iPlus).ToString()).Value = "￥" + gokeikingaku.ToString("0,0");
            nKinagku += gokeikingaku;


            //数量
            int sgokei = nKinagku;
            string zeiku = dr["ZeiKubun"].ToString().Trim();
            if (zeiku == "税抜")
            {
                xlsxCreator.Cell("C9").Value = "￥" + sgokei;
                int zei = sgokei * 1 / 10;
                xlsxCreator.Cell("D9").Value = "￥" + zei;
                xlsxCreator.Cell("E9").Value = "￥" + zei + sgokei;
            }
            if (zeiku == "税込")
            {
                xlsxCreator.Cell("C9").Value = "￥" + sgokei;
                double gokei = sgokei;
                double zeinuki = sgokei / 1.1;
                double zei = gokei - zeinuki;
                xlsxCreator.Cell("D9").Value = "￥" + zei;
            }
            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSetu(XlsxCreator xlsxCreator, int iGyousu, DataRow Mdr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            {
                nNo++;

                if (iGyousu == 1)
                {
                    nNo = 0;
                    nGoeki = 0;
                    nSyouhi = 0;
                    nMitumori = 0;
                    nSuryo = 0;
                    nTanka = 0;
                    nKinagku = 0;

                    xlsxCreator.Cell("A1:I2").Drawing.AddImage(System.Configuration.ConfigurationManager.AppSettings["HeaderImage"]);
                    xlsxCreator.Cell("A1:I2").Drawing.Init();
                    if (bDate == "false")
                    {
                        xlsxCreator.Cell("I2").Value = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        xlsxCreator.Cell("I2").Value = "年" + "        " + "月" + "        " + "日";
                    }
                    if (flg == "0")
                    {
                        xlsxCreator.Cell("I11").Value = Mdr["Busyo"] + " " + Mdr["TanTouName"];
                    }
                    //見積No
                    string strMitumoriNo = Mdr["UriageNo"].ToString();
                    //得意先
                    string sTokuisaki = Mdr["TokuisakiMei"].ToString();
                    //使用施設名
                    string strShisetumei = Mdr["SisetuMei"].ToString();
                    //使用期間
                    string strSiyoukikan =
                         Mdr["SiyouKaishi"].ToString();
                    if (strSiyoukikan != "")
                    {
                        DateTime dateKaisi = DateTime.Parse(strSiyoukikan);
                        string sDate = dateKaisi.ToString("yyy/MM/dd");
                    }

                    string strOwarikikan =
                         Mdr["SiyouOwari"].ToString();
                    if (strOwarikikan != "")
                    {
                        DateTime dateOwari = DateTime.Parse(strOwarikikan);
                        string oDate = dateOwari.ToString("yyy/MM/dd");
                    }

                    xlsxCreator.Cell("I1").Value = strMitumoriNo;
                    xlsxCreator.Cell("B4").Value = sTokuisaki + "  様";
                    xlsxCreator.Cell("C12").Value = strShisetumei;
                }

                string strSuryou = Mdr["JutyuSuryou"].ToString();

                int nSS = int.Parse(strSuryou);

                nSuryo += nSS;
                xlsxCreator.Cell("B9").Value = nSuryo;

                // 各明細のセット
                int iKijunCell = 16;
                int iPlus = ((2 * iGyousu) - 1);
                int iPlusUltra = 2 * iGyousu;
                int nPageCnt = 0; //ページ数分増やす 20180312

                nPageCnt = (iPdfCnt - 1) * 10;

                string syou = Mdr["SyouhinMei"].ToString();
                string[] arr = syou.Split('/');
                string x = arr[0];
                //品目
                if (!Mdr.IsNull("SyouhinMei"))
                {
                    xlsxCreator.Cell("B" + (iKijunCell + iPlus).ToString()).Value = arr[0];
                }

                //品番
                if (!Mdr.IsNull("MekarHinban"))
                {
                    xlsxCreator.Cell("B" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(Mdr["MekarHinban"]);
                }

                //媒体
                if (!Mdr.IsNull("KeitaiMei"))
                {
                    xlsxCreator.Cell("F" + (iKijunCell + iPlus).ToString()).Value = Convert.ToString(Mdr["KeitaiMei"]);
                }

                //標準価格
                if (!Mdr.IsNull("HyojunKakaku"))
                {
                    int hk = int.Parse(Mdr["HyojunKakaku"].ToString());
                    xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = "￥" + hk.ToString("0,0");
                }

                //数量
                if (!Mdr.IsNull("JutyuSuryou"))
                {
                    xlsxCreator.Cell("G" + (iKijunCell + iPlus).ToString()).Value = Convert.ToInt16(Mdr["JutyuSuryou"]);
                }

                if (!Mdr.IsNull("Range"))
                {
                    xlsxCreator.Cell("D" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(Mdr["Range"]);
                }


                //見積単価
                int goukei = 0;
                int kazu = 0;
                string t = Mdr["JutyuTanka"].ToString();
                goukei = int.Parse(t);
                string k = Mdr["JutyuSuryou"].ToString();
                kazu = int.Parse(k);
                int gokeikingaku = goukei * kazu;
                xlsxCreator.Cell("I" + (iKijunCell + iPlus).ToString()).Value = gokeikingaku.ToString("0,0");
                nKinagku += gokeikingaku;


                //数量
                int sgokei = nKinagku;
                string zeiku = Mdr["ZeiKubun"].ToString();
                if (zeiku.Trim() == "税抜")
                {
                    xlsxCreator.Cell("C9").Value = sgokei;
                    int zei = sgokei * 1 / 10;
                    xlsxCreator.Cell("D9").Value = "￥" + zei;
                    xlsxCreator.Cell("E9").Value = "￥" + zei + sgokei;
                }
                if (zeiku.Trim() == "税込")
                {
                    xlsxCreator.Cell("C9").Value = "￥" + sgokei;
                    double gokei = sgokei;
                    double zeinuki = sgokei / 1.1;
                    double zei = gokei - zeinuki;
                    xlsxCreator.Cell("D9").Value = "￥" + zei;
                }
                return xlsxCreator;
            }
        }

        private Doc MakePdfData6(string type, DataMitumori.T_MitumoriDataTable dt, XlsxCreator xlsxCreator, Doc pdf, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            int gokei = 0;
            //一枚に表示出来る最大行数 
            int iViewCount = 20;
            int iViewCountMitsumori = 15;
            int iPdfCntAllMitumori = 0;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dt.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }
            if (dt.Rows.Count <= iViewCountMitsumori)
            {
                iPdfCntAllMitumori = 1;
            }
            else
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAllMitumori = (int)Math.Ceiling(dTemp);
            }


            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成
            int row = 1;
            int intKisaiSu = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {
                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();
                        iGyousu = 1;
                    }
                    iPdfCnt++;
                }
                // テンプレートの読み込み
                if (type == "Mitumori")
                {
                    if (iGyousu == iViewCountMitsumori + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCountMitsumori + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                    }
                    if (row >= 15)
                    {
                        string check = "";
                    }
                    if (row <= 15)
                    {
                        //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022"], "");
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022-kai"], "");
                    }
                    else
                    {
                        iViewCountMitsumori = 20;
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022-2"], "");
                    }

                    //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["MitsumoriFormatGetsuji"], "");
                }
                if (type == "Nouhin")
                {
                    if (iGyousu == iViewCount + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                    }
                    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatGetsuji"], "");
                }
                if (type == "Sekyu")
                {
                    iViewCount = 30;
                    if (iGyousu == iViewCount + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                    }
                    //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuFormatGetsuji"], "");
                    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuForamatNoTax"], "");
                }
                if (type == "Uchiwake")
                {
                    iViewCount = 20;
                    if (iGyousu == iViewCount + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                    }
                    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["UchiwakeFormatNoTax"], "");
                }
                if (type == "Denpyou")
                {
                    iViewCount = 20;
                    if (iGyousu == iViewCount + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                    }
                    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SyukkaFormatGetsuji"], "");
                }
                if (type == "Jutyu")
                {
                    if (iGyousu == iViewCountMitsumori + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCountMitsumori + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                    }
                    if (row <= 15)
                    {
                        //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022"], "");
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["JutyuFormat"], "");
                    }
                    else
                    {
                        iViewCountMitsumori = 20;
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["JutyuFormat2"], "");
                    }
                }

                int SouGokei = gokei;
                //記載
                if (type == "Sekyu")
                {
                    //xlsxCreator = StatementsDataSet4x(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                    xlsxCreator = StatementsDataSet4(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                }
                if (type == "Mitumori")
                {
                    if (row >= 16)
                    {
                        intKisaiSu++;
                        xlsxCreator = StatementsDataSet82(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count, intKisaiSu);
                        if (intKisaiSu == 20)
                        {
                            intKisaiSu = 1;
                        }
                    }
                    else
                    {
                        xlsxCreator = StatementsDataSet83(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                        //xlsxCreator = StatementsDataSet8(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                    }

                    //xlsxCreator = StatementsDataSet4(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                }
                if (type == "Nouhin")
                {
                    xlsxCreator = StatementsDataSet5(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                }
                if (type == "Uchiwake")
                {
                    xlsxCreator = StatementsDataSet6(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, bDate, dtH, row);
                }
                if (type == "Denpyou")
                {
                    xlsxCreator = StatementsDataSet7(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                }
                if (type == "Jutyu")
                {
                    if (row > 15)
                    {
                        intKisaiSu++;
                        xlsxCreator = StatementsDataSet102(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count, intKisaiSu);
                        if (intKisaiSu == 20)
                        {
                            intKisaiSu = 1;
                        }
                    }
                    else
                    {
                        xlsxCreator = StatementsDataSet10(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                        //xlsxCreator = StatementsDataSet8(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                    }
                }
                // 行数カウントアップ
                iGyousu++;
                row++;
            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;
        }

        private XlsxCreator StatementsDataSet102(XlsxCreator xlsxCreator, int iGyousu, DataRow dataRow, int iViewCountMitsumori, int iPdfCnt, int iPdfCntAllMitumori, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row, int count, int intKisaiSu)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dataRow.ItemArray;

            xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
            //商品カウント

            //代表者名
            //基準セル
            int kijuncell = 12;
            int plus1 = kijuncell + iGyousu * 4;
            int plus2 = kijuncell + iGyousu * 4 + 2;
            xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;

            if (!dr.IsSyouhinMeiNull())
            {
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            }
            if (!dr.IsMekarHinbanNull())
            {
                xlsxCreator.Cell("U" + plus2.ToString()).Value = dr.MekarHinban;
            }
            if (!dr.IsRangeNull())
            {
                xlsxCreator.Cell("Y" + plus1.ToString()).Value = dr.Range;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                xlsxCreator.Cell("Y" + plus2.ToString()).Value = dr.KeitaiMei;
            }
            if (!dr.IsJutyuSuryouNull())
            {
                string mn = dr.MekarHinban;
                if (mn != "NEBIKI" && mn != "SOURYOU" && mn != "KIZAI" && mn != "HOSYOU")
                {
                    xlsxCreator.Cell("AE" + plus2.ToString()).Value = dr.JutyuSuryou;
                }
            }
            if (!dr.IsHyojunKakakuNull())
            {
                xlsxCreator.Cell("AB" + plus2.ToString()).Value = dr.ShiireTanka.ToString("0,0");
            }
            if (!dr.IsJutyuTankaNull())
            {
                xlsxCreator.Cell("AB" + plus1.ToString()).Value = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsUriageNull())
            {
                xlsxCreator.Cell("AE" + plus1.ToString()).Value = dr.Uriage.ToString("0,0");
            }
            if (!dr.IsSisetuMeiNull())
            {
                if (!strShisetumei.Equals(dr.SisetuMei))
                {
                    xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SisetuMei;
                }
            }

            if (!dr.IsSiyouKaishiNull())
            {
                string[] Arydate = dr.SiyouKaishi.ToShortDateString().Split('/');
                string date = @"'" + Arydate[0].Substring(2, 2) + "/" + Arydate[1] + "/" + Arydate[2];
                if (!dr.IsSiyouOwariNull())
                {
                    string[] Arydate2 = dr.SiyouOwari.ToShortDateString().Split('/');
                    date += "～" + "'" + Arydate2[0].Substring(2, 2) + "/" + Arydate2[1] + "/" + Arydate2[2];
                }
                xlsxCreator.Cell("N" + plus2.ToString()).Value = date;
            }

            if (row == count)
            {

                int page = 0;
                row = row - 15;
                page = (row / 20) + 2;
                xlsxCreator.Cell("O97").Value = page + "ページ目";
                xlsxCreator.Cell("V97").Value = "計";
                xlsxCreator.Cell("X97").Value = dtH[0].SouSuryou.ToString("0,0");
                if (dtH[0].Zeikubun == "税込")
                {
                    xlsxCreator.Cell("AD97").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                }
                else
                {
                    xlsxCreator.Cell("AD97").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                }
                xlsxCreator.Cell("U97:U98").Attr.LineLeft(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U97:AH97").Attr.LineTop(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("AH97:AH98").Attr.LineRight(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U98:AH98").Attr.LineBottom(xlBorderStyle.xbsThin, xlColor.xclBlack);
            }
            if (intKisaiSu == 20)
            {
                int page = 0;
                row = row - 15;
                page = (row / 20) + 1;
                xlsxCreator.Cell("O97").Value = page + "ページ目";
            }
            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet4x(XlsxCreator xlsxCreator, int iGyousu, DataRow dataRow, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, bool bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {

            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet8(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row, int count)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dl.ItemArray;

            //代表者名
            //基準セル
            int kijuncell = 38;
            int plus1 = kijuncell + iGyousu * 4;
            int plus2 = kijuncell + iGyousu * 4 + 2;
            //商品数
            xlsxCreator.Cell("B" + (plus1).ToString()).Value = row;
            //見積書　ヘッダ
            if (iGyousu == 1)
            {
                if (flg == "0")
                {
                    DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                    xlsxCreator.Cell("W19").Value = "代表取締役" + "　" + drD.Delegetion;
                }
                else
                {
                    xlsxCreator.Cell("W19").Value = dr.Busyo + "　" + dr.TanTouName;
                }
                //日付
                if (bDate == "false")
                {
                    string[] date = DateTime.Now.ToShortDateString().Split('/');
                    xlsxCreator.Cell("Y4").Value = date[0];
                    xlsxCreator.Cell("AC4").Value = date[1];
                    xlsxCreator.Cell("AF4").Value = date[2];
                }
                if (bDate.Contains("年"))
                {
                    string[] date = bDate.Replace("年", "/").Replace("月", "/").Replace("日", "/").Split('/');
                    xlsxCreator.Cell("Y4").Value = date[0];
                    xlsxCreator.Cell("AC4").Value = date[1];
                    xlsxCreator.Cell("AF4").Value = date[2];
                }

                if (!dtH[0].IsTokuisakiAddressNull())
                {
                    xlsxCreator.Cell("A6").Value = dtH[0].TokuisakiAddress;
                }
                if (!dtH[0].IsTokuisakiAddress2Null())
                {
                    xlsxCreator.Cell("A8").Value += dtH[0].TokuisakiAddress2;
                }
                if (!dtH[0].IsTokuisakiPostNoNull())
                {
                    xlsxCreator.Cell("A4").Value = dtH[0].TokuisakiPostNo;
                }
                if (!dtH[0].IsTokuisakiNameNull())
                {
                    xlsxCreator.Cell("A10").Value = dtH[0].TokuisakiName;
                }
                if (!dtH[0].IsTokuisakiCodeNull())
                {
                    xlsxCreator.Cell("E14").Value = dtH[0].TokuisakiCode;
                }
                xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
                if (!dtH[0].IsCategoryNameNull())
                {
                    xlsxCreator.Cell("F18").Value = dtH[0].CategoryName;
                }
                if (!dtH[0].IsFacilityNameNull())
                {
                    xlsxCreator.Cell("F20").Value = dtH[0].FacilityName;
                }
                if (!dtH[0].IsFacilityAddress1Null())
                {
                    string address = dtH[0].FacilityAddress1;
                    if (!dtH[0].IsFacilityAddress2Null())
                    {
                        address += dtH[0].FacilityAddress2;
                    }
                    xlsxCreator.Cell("F22").Value = address;
                }
                if (!dtH[0].IsSouSuryouNull())
                {
                    xlsxCreator.Cell("A27").Value = dtH[0].SouSuryou;
                }
                //税抜
                if (!dtH[0].IsZeikubunNull())
                {
                    if (dtH[0].Zeikubun == "税抜")
                    {
                        if (!dtH[0].IsGokeiKingakuNull())
                        {
                            xlsxCreator.Cell("D27").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                        }
                        if (!dtH[0].IsSyohiZeiGokeiNull())
                        {
                            xlsxCreator.Cell("J27").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        }
                        if (!dtH[0].IsSoukeiGakuNull())
                        {
                            xlsxCreator.Cell("P27").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                        }
                        if (!dtH[0].IsBikouNull())
                        {
                            xlsxCreator.Cell("D30").Value = "￥" + dtH[0].Bikou;
                        }
                    }
                    else
                    {
                        if (!dtH[0].IsGokeiKingakuNull())
                        {
                            xlsxCreator.Cell("D27").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                        }
                        xlsxCreator.Cell("J25").Value = "消費税相当額";
                        if (!dtH[0].IsSyohiZeiGokeiNull())
                        {
                            xlsxCreator.Cell("J27").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        }
                        xlsxCreator.Cell("P25").Value = "";
                        xlsxCreator.Cell("P25:U26").Attr.Joint = false;
                        xlsxCreator.Cell("P27:U28").Attr.Joint = false;
                        xlsxCreator.Cell("P25:U26").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                        xlsxCreator.Cell("P27:U28").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                    }
                }
            }
            if (!dr.IsSyouhinMeiNull())
            {
                xlsxCreator.Cell("C" + plus1.ToString()).Value = dr.SyouhinMei;
            }
            if (!dr.IsMekarHinbanNull())
            {
                xlsxCreator.Cell("Q" + plus2.ToString()).Value = dr.MekarHinban;
            }
            if (!dr.IsRangeNull())
            {
                xlsxCreator.Cell("U" + plus1.ToString()).Value = dr.Range;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                xlsxCreator.Cell("U" + plus2.ToString()).Value = dr.KeitaiMei;
            }
            if (!dr.IsJutyuSuryouNull())
            {
                xlsxCreator.Cell("X" + plus2.ToString()).Value = dr.JutyuSuryou;
            }
            if (!dr.IsHyojunKakakuNull())
            {
                xlsxCreator.Cell("AA" + plus1.ToString()).Value = int.Parse(dr.HyojunKakaku).ToString("0,0");
            }
            if (!dr.IsJutyuTankaNull())
            {
                xlsxCreator.Cell("AA" + plus2.ToString()).Value = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsUriageNull())
            {
                xlsxCreator.Cell("AE" + plus2.ToString()).Value = dr.Uriage.ToString("0,0");
            }

            if (count != row)
            {
                xlsxCreator.Cell("U103:AH104").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                xlsxCreator.Cell("V103").Value = "";
            }
            else
            {
                xlsxCreator.Cell("V103").Value = "計";
                xlsxCreator.Cell("X103").Value = dtH[0].SouSuryou;
                xlsxCreator.Cell("AD103").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                xlsxCreator.Cell("U103:U104").Attr.LineLeft(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U103:AH103").Attr.LineTop(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("AH103:AH104").Attr.LineRight(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U104:AH104").Attr.LineBottom(xlBorderStyle.xbsThin, xlColor.xclBlack);

            }

            //if (flg == "0")
            //{
            //    DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
            //    xlsxCreator.Cell("I13").Value = "代表取締役" + "  " + drD.Delegetion;
            //}
            //else
            //{
            //    xlsxCreator.Cell("I13").Value = dr["Busyo"] + "  " + dr["TanTouName"];
            //}

            //if (bDate == "false")
            //{
            //    xlsxCreator.Cell("I1").Value = DateTime.Now.ToLongDateString();
            //}
            //if (bDate == "true")
            //{
            //    xlsxCreator.Cell("I1").Value = "年" + "        " + "月" + "        " + "日";
            //}
            //if (bDate.Contains("年"))
            //{
            //    xlsxCreator.Cell("I1").Value = bDate;
            //}

            //int kijuncell = 15;
            //int plus1 = kijuncell + iGyousu * 3;
            //int plus2 = kijuncell + iGyousu * 3 + 1;
            //int plus3 = kijuncell + iGyousu * 3 + 2;

            //xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;
            //if (iGyousu == 1)
            //{
            //    if (!dtH[0].IsBikouNull())
            //    {
            //        xlsxCreator.Cell("C14").Value = dtH[0].Bikou;
            //    }
            //    if (dtH[0].SouSuryou.ToString().Length >= 3)
            //    {
            //        xlsxCreator.Cell("B11").Attr.FontPoint = 10;
            //        xlsxCreator.Cell("B11").Value = dtH[0].SouSuryou;
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("B11").Value = dtH[0].SouSuryou;
            //    }
            //    if (dtH[0].SoukeiGaku.ToString().Length >= 9)
            //    {
            //        xlsxCreator.Cell("C11").Attr.FontPoint = 10;
            //        xlsxCreator.Cell("C11").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("C11").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
            //    }
            //    xlsxCreator.Cell("I2").Value = dr.MitumoriNo;
            //    xlsxCreator.Cell("B4").Value = dr.TokuisakiMei + " " + "様";
            //    if (dr.Zeikubun == "税込")
            //    {
            //        xlsxCreator.Cell("D11").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
            //    }
            //    else//税抜
            //    {
            //        xlsxCreator.Cell("D11").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
            //        xlsxCreator.Cell("D9").Value = "消費税";
            //        xlsxCreator.Cell("E9").Value = "税込合計額";
            //        xlsxCreator.Cell("E11").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
            //        xlsxCreator.Cell("E9").Attr.Box(xlBoxType.xbtBox, xlBorderStyle.xbsThin, System.Drawing.Color.Black);
            //        xlsxCreator.Cell("E11:E12").Attr.Box(xlBoxType.xbtBox, xlBorderStyle.xbsThin, System.Drawing.Color.Black);
            //    }
            //}
            //xlsxCreator.Cell("D" + plus3.ToString()).Value = dr.SisetuMei;
            //xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            //xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.KeitaiMei;
            //xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.JutyuSuryou;
            //xlsxCreator.Cell("H" + plus1.ToString()).Value = int.Parse(dr.HyojunKakaku).ToString("0,0");//2022/01/24 JutyuTankaからHyoujunTankaに変更
            //xlsxCreator.Cell("I" + plus1.ToString()).Value = dr.Uriage.ToString("0,0");
            //xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.MekarHinban;
            //xlsxCreator.Cell("D" + plus2.ToString()).Value = dr.Range;
            //if (!dr.IsTekiyou1Null())
            //{
            //    xlsxCreator.Cell("E" + plus2.ToString()).Value = dr.Tekiyou1;
            //}
            //if (!dr.IsSiyouKaishiNull() || !dr.IsSiyouOwariNull())
            //{
            //    xlsxCreator.Cell("B" + plus3.ToString()).Value = dr.SiyouKaishi.ToShortDateString() + " ~ " + dr.SiyouOwari.ToShortDateString();
            //}
            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet7(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dl.ItemArray;

            int kijuncell = 11;
            int plus1 = kijuncell + iGyousu * 2;
            int plus2 = kijuncell + iGyousu * 2 + 1;

            if (iGyousu == 1)
            {
                xlsxCreator.Cell("G42").Value = dtH[0].SouSuryou;
                xlsxCreator.Cell("I42").Value = dtH[0].SoukeiGaku;
            }
            xlsxCreator.Cell("A" + plus1.ToString()).Value = row;

            if (flg == "0")
            {
                DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                xlsxCreator.Cell("I9").Value = "代表取締役" + "  " + drD.Delegetion;
            }
            else
            {
                xlsxCreator.Cell("I9").Value = dr["Busyo"] + "  " + dr["TanTouName"];
            }
            if (bDate == "false")
            {
                xlsxCreator.Cell("J1").Value = DateTime.Now.ToLongDateString();
            }
            if (bDate == "true")
            {
                xlsxCreator.Cell("J1").Value = "年" + "        " + "月" + "        " + "日";
            }
            if (bDate.Contains("年"))
            {
                xlsxCreator.Cell("J1").Value = bDate;
            }
            xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.KeitaiMei;
            xlsxCreator.Cell("I" + plus1.ToString()).Value = dr.JutyuSuryou;
            xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.MekarHinban;
            xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.Range;
            if (!dr.IsTekiyou1Null())
            {
                xlsxCreator.Cell("F" + plus2.ToString()).Value = dr.Tekiyou1;
            }
            if (!dr.IsSiyouKaishiNull() || !dr.IsSiyouOwariNull())
            {
                xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SiyouKaishi.ToShortDateString() + " ~ " + dr.SiyouOwari.ToShortDateString();
            }
            xlsxCreator.Cell("D" + plus2.ToString()).Value = dr.SisetuMei;

            Suryo += dr.JutyuSuryou;
            xlsxCreator.Cell("I42").Value = Suryo;

            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet6(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dl.ItemArray;


            int kijuncell = 6;
            int plus1 = kijuncell + iGyousu * 2;
            int plus2 = kijuncell + iGyousu * 2 + 1;

            //列番号
            xlsxCreator.Cell("A" + plus1.ToString()).Value = row;
            if (iGyousu == 1)
            {
                if (bDate == "false")
                {
                    xlsxCreator.Cell("I1").Value = DateTime.Now.ToLongDateString();
                }
                if (bDate == "true")
                {
                    xlsxCreator.Cell("I1").Value = "年" + "        " + "月" + "        " + "日";
                }
                if (bDate.Contains("年"))
                {
                    xlsxCreator.Cell("I1").Value = bDate;
                }
                xlsxCreator.Cell("B4").Value = "使用施設名：" + dr.SisetuMei;
            }
            xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.KeitaiMei;
            xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.JutyuSuryou;
            Suryo += dr.JutyuSuryou;
            xlsxCreator.Cell("G49").Value = Suryo;


            if (dr.JutyuTanka.ToString() != "OPEN")
            {
                int hk = dr.JutyuTanka;
                xlsxCreator.Cell("H" + plus1.ToString()).Value = hk.ToString("0,0");
            }
            else
            {
                xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.JutyuTanka;
            }
            xlsxCreator.Cell("I" + plus1.ToString()).Value = dr.Uriage.ToString("0,0");
            xlsxCreator.Cell("I49").Value = dtH[0].GokeiKingaku.ToString("0,0");
            xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.MekarHinban;
            xlsxCreator.Cell("D" + plus2.ToString()).Value = dr.Range;
            if (!dr.IsTekiyou1Null())
            {
                xlsxCreator.Cell("E" + plus2.ToString()).Value = dr.Tekiyou1;
            }
            return xlsxCreator;
        }

        private Doc MakePdfData(string type, DataMitumori.T_MitumoriDataTable dt, XlsxCreator xlsxCreator, Doc pdf, int gokei, int su)
        {
            //20200729 前田　施設名によってページを分けるという処理
            string facility = "";
            for (int i = 0; i < dt.Count; i++)
            {
                if (facility != "")
                { facility += ","; }

                facility += dt[i].SisetuMei;
            }
            string[] strAry = facility.Split(',');
            for (int q = 0; q < strAry.Length; q++)
            {

            }

            //一枚に表示出来る最大行数 
            int iViewCount = 20;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dt.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {

                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();

                        iGyousu = 1;

                    }
                    iPdfCnt++;

                    // テンプレートの読み込み
                    if (type == "Mitumori")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["MitumoriFormat4"], "");
                    }
                    if (type == "Nouhin")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinForm2"], "");
                    }
                    if (type == "Sekyu")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuForm"], "");
                    }
                }
                int SouGokei = gokei;
                //xlsxCreator = StatementsDataSet(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei);

                // 行数カウントアップ
                iGyousu++;
            }
            //もしNo分明細がなければ追加で処理 20180312
            //if (dt.Rows.Count % 10 != 0)
            //    for (int i = dt.Rows.Count % 10; i < 10; i++)
            //    {
            //        // 各明細のセット
            //        int iKijunCell = 21;
            //        //int iPlus = (iGyousu - 1) * 2;
            //        int nPageCnt = 0; //ページ数分増やす 20180312

            //        // No
            //        nPageCnt = (iPdfCnt - 1) * 10;
            //        xlsxCreator.Cell("B" + (iKijunCell).ToString()).Value = (iGyousu + nPageCnt).ToString();

            //        // 行数カウントアップ
            //        iGyousu++;
            //    }

            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;
        }

        internal void ShiireDenpyou(string type, Doc pdf, string[] strShiire)
        {
            XlsxCreator xlsxCreator;
            xlsxCreator = new XlsxCreator();

            DataSet1.T_DenpyouDataTable dt = new DataSet1.T_DenpyouDataTable();
            for (int i = 0; i < strShiire.Length; i++)
            {

                DataAppropriate.T_AppropriateDataTable dd = Class1.GetAppropriate2(strShiire[i], Global.GetConnection());
                for (int j = 0; j < dd.Count; j++)
                {
                    DataSet1.T_DenpyouRow dr = dt.NewT_DenpyouRow();

                    dr.ItemArray = dd[j].ItemArray;
                    dt.AddT_DenpyouRow(dr);
                }
            }
            pdf = MakePdfData5(type, dt, xlsxCreator, pdf);

            theData = pdf.GetData();
        }

        string tokuisaki = "";
        string category = "";


        private Doc MakePdfData5(string type, DataSet1.T_DenpyouDataTable dt, XlsxCreator xlsxCreator, Doc pdf)
        {
            int SouGokei = 0;
            int gokei = 0;
            //一枚に表示出来る最大行数 
            int iViewCount = 50;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dt.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();//追加ページ作成

            string strKey = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!dt[i].IsTokuisakiCodeNull() || !dt[i].IsCategoryNull())
                {
                    if (strKey == "")
                    {
                        strKey += dt[i].Category + "/" + dt[i].ShiireNo;
                    }
                    else
                    {
                        if (strKey.Contains(dt[i].Category + "/" + dt[i].ShiireNo))
                        {

                        }
                        else//含まれてないほう
                        {
                            strKey += ",";
                            strKey += dt[i].Category + "/" + dt[i].ShiireNo;
                        }
                    }
                }
            }
            string[] strData = strKey.Split(',');
            for (int d = 0; d < strData.Length; d++)
            {
                string[] data = strData[d].Split('/');
                string cate = data[0];
                string no = data[1];

                DataAppropriate.T_AppropriateDataTable ddH = Class1.GetDenpyo(cate, no, Global.GetConnection());
                DataAppropriate.T_AppropriateDataTable dd = new DataAppropriate.T_AppropriateDataTable();
                for (int i = 0; i < ddH.Count; i++)
                {
                    // 改ページ条件は明細の行数が最大行数に達した時
                    if (iGyousu == iViewCount + 1 || iGyousu == 1)
                    {
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();

                            iGyousu = 1;
                        }
                        iPdfCnt++;
                        // テンプレートの読み込み
                        if (type == "DenPyou")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Test"], "");
                        }
                    }

                    SouGokei = gokei;

                    xlsxCreator = StatementsDataSet3(xlsxCreator, strKey, iViewCount, iPdfCnt, iPdfCntAll, SouGokei, ddH[i]);
                    iGyousu++; iGyousu++; iGyousu++; iGyousu++; iGyousu++;
                }
            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加
            return pdf;
        }

        int iGyousu = 1;

        private XlsxCreator StatementsDataSet3(XlsxCreator xlsxCreator, string strKey, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, DataAppropriate.T_AppropriateRow ddH)
        {
            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            DataAppropriate.T_AppropriateRow dr = dt.NewT_AppropriateRow();
            dr.ItemArray = ddH.ItemArray;
            //category = ddH.Category.ToString();

            int Kijun1 = iGyousu + 1;
            int Kijun2 = iGyousu + 2;
            int Kijun3 = iGyousu + 3;
            int Kijun4 = iGyousu + 4;

            if (!category.Contains(dr.Category + "/"))
            {
                xlsxCreator.Cell("A" + iGyousu.ToString()).Value = dr.CategoryName;
                xlsxCreator.Cell("A" + iGyousu.ToString()).Attr.FontPoint = 20;
                //xlsxCreator.Cell("A" + iGyousu.ToString() + ":L" + iGyousu.ToString()).Attr.LineBottom(xlBorderStyle.xbsThin, ExcelCreator6.xlColor.xclBlack);
                xlsxCreator.Cell("A" + Kijun1.ToString() + ":L" + Kijun1.ToString()).Attr.Box(ExcelCreator6.xlBoxType.xbtBox, xlBorderStyle.xbsThin, ExcelCreator6.xlColor.xclBlack);//罫線
                xlsxCreator.Cell("A" + Kijun1.ToString()).Value = dr.TokuisakiMei;
                xlsxCreator.Cell("A" + Kijun2.ToString()).Value = dr.FacilityCode;
                xlsxCreator.Cell("B" + Kijun2.ToString()).Value = dr.FacilityName;
                xlsxCreator.Cell("D" + Kijun3.ToString()).Value = "処理日";
                xlsxCreator.Cell("A" + Kijun4.ToString()).Value = dr.ShiireSakiName;
                xlsxCreator.Cell("D" + Kijun4.ToString()).Value = dr.HatyuDay.Replace("/", "");
                xlsxCreator.Cell("E" + Kijun4.ToString()).Value = dr.Media;
                xlsxCreator.Cell("F" + Kijun4.ToString()).Value = dr.MekerNo;
                xlsxCreator.Cell("G" + Kijun4.ToString()).Value = dr.ProductName;
                xlsxCreator.Cell("L" + Kijun4.ToString()).Value = dr.JutyuSuryou;
                tokuisaki += dr.TokuisakiMei + "/";
                category += dr.Category + "/";
                iGyousu++; iGyousu++; iGyousu++; iGyousu++; iGyousu++;
            }
            else
            {
                if (!tokuisaki.Contains(dr.TokuisakiMei))
                {
                    xlsxCreator.Cell("A" + iGyousu.ToString() + ":L" + iGyousu.ToString()).Attr.Box(ExcelCreator6.xlBoxType.xbtBox, xlBorderStyle.xbsThin, ExcelCreator6.xlColor.xclBlack);//罫線
                    xlsxCreator.Cell("A" + iGyousu.ToString()).Value = dr.TokuisakiMei;
                    xlsxCreator.Cell("A" + Kijun1.ToString()).Value = dr.FacilityCode;
                    xlsxCreator.Cell("B" + Kijun1.ToString()).Value = dr.FacilityName;
                    xlsxCreator.Cell("D" + Kijun2.ToString()).Value = "処理日";
                    xlsxCreator.Cell("A" + Kijun3.ToString()).Value = dr.ShiireSakiName;
                    xlsxCreator.Cell("D" + Kijun3.ToString()).Value = dr.HatyuDay.Replace("/", "");
                    xlsxCreator.Cell("E" + Kijun3.ToString()).Value = dr.Media;
                    xlsxCreator.Cell("F" + Kijun3.ToString()).Value = dr.MekerNo;
                    xlsxCreator.Cell("G" + Kijun3.ToString()).Value = dr.ProductName;
                    xlsxCreator.Cell("L" + Kijun3.ToString()).Value = dr.JutyuSuryou;
                    iGyousu++; iGyousu++; iGyousu++; iGyousu++;
                    tokuisaki += dr.TokuisakiMei + "/";
                }
                else
                {
                    xlsxCreator.Cell("A" + iGyousu.ToString() + ":L" + iGyousu.ToString()).Attr.LineTop(xlBorderStyle.xbsThin, ExcelCreator6.xlColor.xclBlack);
                    xlsxCreator.Cell("A" + iGyousu.ToString()).Value = dr.FacilityCode;
                    xlsxCreator.Cell("B" + iGyousu.ToString()).Value = dr.FacilityName;
                    xlsxCreator.Cell("D" + Kijun1.ToString()).Value = "処理日";
                    xlsxCreator.Cell("A" + Kijun2.ToString()).Value = dr.ShiireSakiName;
                    xlsxCreator.Cell("D" + Kijun2.ToString()).Value = dr.HatyuDay.Replace("/", "");
                    xlsxCreator.Cell("E" + Kijun2.ToString()).Value = dr.Media;
                    xlsxCreator.Cell("F" + Kijun2.ToString()).Value = dr.MekerNo;
                    xlsxCreator.Cell("G" + Kijun2.ToString()).Value = dr.ProductName;
                    xlsxCreator.Cell("L" + Kijun2.ToString()).Value = dr.JutyuSuryou;
                    iGyousu++; iGyousu++; iGyousu++;
                }
            }
            return xlsxCreator;
        }


        private XlsxCreator StatementsDataSet(XlsxCreator xlsxCreator, int iGyousu, DataRow Mdr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, bool bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {
            nNo++;
            //使用施設名

            if (iGyousu == 1)
            {
                nNo = 0;
                nGoeki = 0;
                nSyouhi = 0;
                nMitumori = 0;
                nSuryo = 0;
                nTanka = 0;
                nKinagku = 0;

                //xlsxCreator.Cell("A1:I2").Drawing.AddImage(System.Configuration.ConfigurationManager.AppSettings["HeaderImage"]);
                //xlsxCreator.Cell("A1:I2").Drawing.Init();
                if (!bDate)
                {
                    xlsxCreator.Cell("I2").Value = DateTime.Now.ToShortDateString();
                }
                else
                {
                    xlsxCreator.Cell("I2").Value = "年" + "        " + "月" + "        " + "日";
                }
                if (flg == "0")
                {
                    DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                    xlsxCreator.Cell("I11").Value = "代表取締役" + "  " + drD.Delegetion;
                }
                else
                {
                    xlsxCreator.Cell("I11").Value = Mdr["Busyo"] + "  " + Mdr["TanTouName"];
                }
                //見積No
                string strMitumoriNo = Mdr["MitumoriNo"].ToString();
                //得意先
                string sTokuisaki = Mdr["TokuisakiMei"].ToString();
                //使用期間
                string strSiyoukikan =
                     Mdr["SiyouKaishi"].ToString();
                if (strSiyoukikan != "")
                {
                    DateTime dateKaisi = DateTime.Parse(strSiyoukikan);
                    string sDate = dateKaisi.ToString("yyy/MM/dd");
                }

                string strOwarikikan =
                     Mdr["SiyouOwari"].ToString();
                if (strOwarikikan != "")
                {
                    DateTime dateOwari = DateTime.Parse(strOwarikikan);
                    string oDate = dateOwari.ToString("yyy/MM/dd");
                }

                xlsxCreator.Cell("I1").Value = strMitumoriNo;
                xlsxCreator.Cell("B4").Value = sTokuisaki + "  様";
                int suryo = dtH[0].SouSuryou;
                int gokei = dtH[0].GokeiKingaku;
                int syohizei = dtH[0].SyohiZeiGokei;
                int zeikomi = dtH[0].SoukeiGaku;

                if (suryo.ToString().Length >= 3)
                {
                    xlsxCreator.Cell("B9").Attr.FontPoint = 10;
                    xlsxCreator.Cell("B9").Value = suryo.ToString();
                }
                else
                {
                    xlsxCreator.Cell("B9").Value = suryo.ToString();
                }
                if (gokei.ToString().Length >= 9)
                {
                    xlsxCreator.Cell("C9").Attr.FontPoint = 10;
                    xlsxCreator.Cell("C9").Value = "￥" + gokei.ToString("0,0");
                }
                else
                {
                    string g = gokei.ToString("0,0");
                    xlsxCreator.Cell("C9").Value = "￥" + g;
                }
                if (syohizei.ToString().Length >= 7)
                {
                    xlsxCreator.Cell("D9").Attr.FontPoint = 10;
                    string s = syohizei.ToString("0,0");
                    xlsxCreator.Cell("D9").Value = "￥" + s;
                }
                else
                {
                    string s = syohizei.ToString("0,0");
                    xlsxCreator.Cell("D9").Value = "￥" + s;
                }
                if (dtH[0].Zeikubun == "税抜")
                {
                    if (zeikomi.ToString().Length >= 9)
                    {
                        xlsxCreator.Cell("E9").Attr.FontPoint = 10;
                        xlsxCreator.Cell("E9").Value = "￥" + (gokei + syohizei).ToString("0,0");
                    }
                    else
                    {
                        xlsxCreator.Cell("E9").Value = "￥" + (gokei + syohizei).ToString("0,0");
                    }
                }
            }
            if (string.IsNullOrEmpty(strShisetumei))
            {
                strShisetumei = Mdr["SisetuMei"].ToString();
            }
            else
            {
                if (!strShisetumei.Contains(Mdr["SisetuMei"].ToString()))
                {
                    strShisetumei += "/" + Mdr["SisetuMei"].ToString();
                }
            }
            xlsxCreator.Cell("C12").Value = strShisetumei;
            // 各明細のセット
            int iKijunCell = 16;
            int iPlus = ((2 * iGyousu) - 1);
            int iPlusUltra = 2 * iGyousu;
            int nPageCnt = 0; //ページ数分増やす 20180312

            nPageCnt = (iPdfCnt - 1) * 10;

            string syou = Mdr["SyouhinMei"].ToString();
            string[] arr = syou.Split('/');
            string x = arr[0];

            //列番号
            xlsxCreator.Cell("A" + (iKijunCell + iPlus).ToString()).Value = row;

            //品目
            if (!Mdr.IsNull("SyouhinMei"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus).ToString()).Value = arr[0];
            }

            //品番
            if (!Mdr.IsNull("MekarHinban"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(Mdr["MekarHinban"]);
            }

            //媒体
            if (!Mdr.IsNull("KeitaiMei"))
            {
                xlsxCreator.Cell("F" + (iKijunCell + iPlus).ToString()).Value = Convert.ToString(Mdr["KeitaiMei"]);
            }

            //標準価格
            if (!Mdr.IsNull("HyojunKakaku"))
            {
                if (Mdr["MekarHinban"].ToString() != "0")
                {
                    if (Mdr["JutyuTanka"].ToString() != "OPEN")
                    {
                        int hk = int.Parse(Mdr["JutyuTanka"].ToString());
                        xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = hk.ToString("0,0");
                    }
                    else
                    {
                        xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = Mdr["JutyuTanka"];
                    }
                }
            }

            //数量
            if (!Mdr.IsNull("JutyuSuryou"))
            {
                if (Mdr["MekarHinban"].ToString() != "0")
                {
                    xlsxCreator.Cell("G" + (iKijunCell + iPlus).ToString()).Value = Convert.ToInt16(Mdr["JutyuSuryou"]);
                }
            }

            if (!Mdr.IsNull("Range"))
            {
                xlsxCreator.Cell("D" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(Mdr["Range"]);
            }

            if (!Mdr.IsNull("Uriage"))
            {
                int JG = int.Parse(Mdr["Uriage"].ToString());
                xlsxCreator.Cell("I" + (iKijunCell + iPlus).ToString()).Value = JG.ToString("0,0");
            }
            return xlsxCreator;
        }

        internal void MitumoriInsatu(string type, string[] strMitumoriAry, string[] strShisetuiAry)
        {
            Doc pdf = new Doc();

            DataMitumori.T_MitumoriDataTable dt = null;
            for (int i = 0; i < strShisetuiAry.Length; i++)
            {
                dt =
                    ClassMitumori.MitumoriInsatu(strMitumoriAry[i], Global.GetConnection());

                if (dt.Count > 0)
                {
                    string facility = "";
                    for (int f = 0; f < dt.Count; f++)
                    {
                        if (facility != "")
                        { facility += ","; }

                        facility += dt[f].SisetuMei;
                    }
                    DataMitumori.T_MitumoriDataTable dd = null;
                    string[] strAry = facility.Split(',');
                    string usedfaci = "";
                    for (int q = 0; q < strAry.Length; q++)
                    {
                        if (usedfaci != "")
                        { usedfaci += ","; }
                        string target = strAry[q];
                        if (usedfaci.Contains(target))
                        { }
                        else
                        {
                            dd = ClassMitumori.MitumoriInsatu2(strMitumoriAry[i], strAry[q], Global.GetConnection());
                            int gokei = 0;
                            gokei = 0;
                            int su = 0;
                            XlsxCreator xlsxCreator;
                            xlsxCreator = new XlsxCreator();
                            for (int a = 0; a < dd.Count; a++)
                            {
                                int hk = int.Parse(dd[a].HyojunKakaku);
                                gokei += hk;
                                int ryou = dd[a].JutyuSuryou;
                                su += ryou;
                            }
                            pdf = MakePdfData(type, dd, xlsxCreator, pdf, gokei, su);
                            usedfaci += strAry[q];
                        }
                    }
                }
            }
            theData = pdf.GetData();
        }

        internal void JutyuInsatsu(string type, string[] strJutyuAry)
        {
            Doc pdf = new Doc();
            for (int j = 0; j < strJutyuAry.Length; j++)
            {
                DataJutyu.T_JutyuDataTable dt = ClassJutyu.GetJutyu1(strJutyuAry[j], Global.GetConnection());
                for (int i = 0; i < dt.Count; i++)
                {
                    string facility = "";
                    string category = "";

                    if (facility != "")
                    { facility += ","; }
                    if (category != "")
                    { category += ","; }
                    facility += dt[i].SisetuMei;
                    category += dt[i].CategoryName;
                    string[] strAry = facility.Split(',');
                    string[] strAry2 = category.Split(',');
                    string usedfaci = "";
                    for (int q = 0; q < strAry.Length; q++)
                    {
                        if (usedfaci != "")
                        { usedfaci += ","; }
                        string target = strAry[q];
                        if (usedfaci.Contains(target))
                        { }
                        else
                        {
                            DataJutyu.T_JutyuDataTable dd = ClassJutyu.GetJutyu2(strJutyuAry[j], strAry[q], Global.GetConnection());
                            int gokei = 0;
                            gokei = 0;
                            XlsxCreator xlsxCreator;
                            xlsxCreator = new XlsxCreator();
                            for (int a = 0; a < dd.Count; a++)
                            {
                                int hk = int.Parse(dd[a].HyojunKakaku);
                                gokei += hk;
                            }
                            pdf = MakePdfData2(type, dd, xlsxCreator, pdf, gokei);
                            usedfaci += strAry[q];

                        }
                    }
                }
            }
            theData = pdf.GetData();

        }

        internal void OrderPrint2(string type, string[] strAry)
        {
            Doc pdf = new Doc();
            DataSet1.T_OrderedDataTable dt = null;

            for (int x = 0; x < strAry.Length; x++)
            {
                dt = ClassOrdered.GetOrdered2(strAry[x], Global.GetConnection());
                XlsxCreator xlsxCreator;
                xlsxCreator = new XlsxCreator();

                pdf = MakePdfDataOrdered(type, dt, xlsxCreator, pdf);
            }
            theData = pdf.GetData();
        }

        internal void MitumoriInsatu2(string type, string[] strMitumoriAry)
        {
            string facility = "";

            Doc pdf = new Doc();

            for (int i = 0; i < strMitumoriAry.Length; i++)
            {
                DataMitumori.T_MitumoriDataTable dt = ClassMitumori.GetMitumoriTable(strMitumoriAry[i], Global.GetConnection());
                XlsxCreator xlsxCreator;
                xlsxCreator = new XlsxCreator();
                //pdf = MakePdfData4(type, dt, xlsxCreator, pdf);

            }
            theData = pdf.GetData();
        }

        private Doc MakePdfData4(string type, DataMitumori.T_MitumoriDataTable dt, XlsxCreator xlsxCreator, Doc pdf, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH)
        {
            int gokei = 0;
            //一枚に表示出来る最大行数 
            int iViewCount = 20;
            int iViewCountMitsumori = 15;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            int iPdfCntAllMitumori = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dt.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            if (dt.Rows.Count <= iViewCountMitsumori)
            {
                iPdfCntAllMitumori = 1;
            }
            else
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAllMitumori = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1; //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成
            int row = 1;
            int intKisaiSu = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCountMitsumori + 1 || iGyousu == 1)
                {
                    // テンプレートの読み込み
                    if (type == "Mitumori")
                    {
                        if (iGyousu == iViewCountMitsumori + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                        if (row >= 15)
                        {
                            string check = "";
                        }

                        if (row < 15)
                        {
                            //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022"], "");
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022-kai"], "");
                        }
                        else
                        {
                            iViewCountMitsumori = 20;
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022-2"], "");
                        }
                    }
                    if (type == "Jutyu")
                    {
                        if (i == 35)
                        {

                        }
                        if (iGyousu == iViewCountMitsumori + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                        if (row < 15)
                        {
                            //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["Mitsumori2022"], "");
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["JutyuFormat"], "");
                        }
                        else
                        {
                            iViewCountMitsumori = 20;
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["JutyuFormat2"], "");
                        }
                    }
                }
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {
                    if (type == "Nouhin")
                    {
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();

                            iGyousu = 1;

                        }
                        iPdfCnt++;
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatGetsuji"], "");
                        //if (dt[i].Zeikubun.Trim() == "税込")
                        //{
                        //    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatAddTax"], "");
                        //}
                        //else
                        //{
                        //    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatNoTax"], "");
                        //}
                    }
                    if (type == "Sekyu")
                    {
                        iViewCount = 30;
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                        //xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinFormatGetsuji"], "");
                        if (dt[i].Zeikubun.Trim() == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuForamatNoTax"], "");
                        }
                    }
                    if (type == "Uchiwake")
                    {
                        iViewCount = 20;
                        if (iGyousu == iViewCount + 1)
                        {

                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;

                        if (dt[i].Zeikubun.Trim() == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["UchiwakeFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["UchiwakeFormatNoTax"], "");
                        }
                    }
                    if (type == "Denpyou")
                    {
                        iViewCount = 20;
                        if (iGyousu == iViewCount + 1)
                        {
                            xlsxCreator.CloseBook(true, this.ms, false);
                            page1.Read(this.ms.ToArray());
                            pdf.Append(page1);

                            //ページを追加したら初期化
                            this.ms = new System.IO.MemoryStream();
                            page1 = new Doc();
                            xlsxCreator = new ExcelCreator6.XlsxCreator();
                            iGyousu = 1;
                        }
                        iPdfCnt++;
                        if (dt[i].Zeikubun.Trim() == "税込")
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SyukkaFormatAddTax"], "");
                        }
                        else
                        {
                            xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SyukkaFormatNoTax"], "");
                        }
                    }
                }
                int SouGokei = gokei;
                //記載
                if (type == "Sekyu")
                {
                    //xlsxCreator = StatementsDataSet4x(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                    xlsxCreator = StatementsDataSet4(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                }
                if (type == "Mitumori")
                {
                    if (row > 15)
                    {
                        intKisaiSu++;
                        xlsxCreator = StatementsDataSet82(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count, intKisaiSu);
                        if (intKisaiSu == 20)
                        {
                            intKisaiSu = 1;
                        }
                    }
                    else
                    {
                        xlsxCreator = StatementsDataSet83(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                        //xlsxCreator = StatementsDataSet8(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                    }
                }
                if (type == "Nouhin")
                {
                    xlsxCreator = StatementsDataSet5(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                }
                if (type == "Uchiwake")
                {
                    xlsxCreator = StatementsDataSet6(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, bDate, dtH, row);
                }
                if (type == "Denpyou")
                {
                    xlsxCreator = StatementsDataSet9(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei, flg, bDate, dtH, row);
                }
                if (type == "Jutyu")
                {
                    if (row > 15)
                    {
                        intKisaiSu++;
                        xlsxCreator = StatementsDataSet102(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count, intKisaiSu);
                        if (intKisaiSu == 20)
                        {
                            intKisaiSu = 1;
                        }
                    }
                    else
                    {
                        xlsxCreator = StatementsDataSet10(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                        //xlsxCreator = StatementsDataSet8(xlsxCreator, iGyousu, dt.Rows[i], iViewCountMitsumori, iPdfCnt, iPdfCntAllMitumori, SouGokei, flg, bDate, dtH, row, dt.Rows.Count);
                    }
                }
                // 行数カウントアップ
                iGyousu++;
                row++;
            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;

        }

        private XlsxCreator StatementsDataSet10(XlsxCreator xlsxCreator, int iGyousu, DataRow dataRow, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row, int count)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dataRow.ItemArray;

            xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
            //基準セル
            int kijuncell = 38;
            int plus1 = kijuncell + iGyousu * 4;
            int plus2 = kijuncell + iGyousu * 4 + 2;
            xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;

            //見積書　ヘッダ
            if (iGyousu == 1)
            {
                if (flg == "0")
                {
                    DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                    xlsxCreator.Cell("W19").Value = "代表取締役" + "　" + drD.Delegetion;
                }
                else
                {
                    xlsxCreator.Cell("W19").Value = dr.Busyo + "　" + dr.TanTouName;
                }
                //日付
                if (bDate == "false")
                {
                    string[] date = DateTime.Now.ToShortDateString().Split('/');
                    xlsxCreator.Cell("Y4").Value = date[0];
                    xlsxCreator.Cell("AC4").Value = date[1];
                    xlsxCreator.Cell("AF4").Value = date[2];
                }
                if (bDate.Contains("年"))
                {
                    string[] date = bDate.Replace("年", "/").Replace("月", "/").Replace("日", "/").Split('/');
                    xlsxCreator.Cell("Y4").Value = date[0];
                    xlsxCreator.Cell("AC4").Value = date[1];
                    xlsxCreator.Cell("AF4").Value = date[2];
                }

                if (!dtH[0].IsTokuisakiAddressNull())
                {
                    xlsxCreator.Cell("A6").Value = dtH[0].TokuisakiAddress;
                }
                if (!dtH[0].IsTokuisakiAddress2Null())
                {
                    xlsxCreator.Cell("A8").Value += dtH[0].TokuisakiAddress2;
                }
                if (!dtH[0].IsTokuisakiPostNoNull())
                {
                    xlsxCreator.Cell("A4").Value = "〒" + dtH[0].TokuisakiPostNo;
                }
                if (!dtH[0].IsTokuisakiNameNull())
                {
                    xlsxCreator.Cell("A10").Value = dtH[0].TokuisakiName;
                    if (dtH[0].TokuisakiName.Length > 20)
                    {
                        xlsxCreator.Cell("A10").Attr.FontPoint = 12;
                        xlsxCreator.Cell("A10").Attr.OverReturn = true;
                    }
                }
                if (!dtH[0].IsTokuisakiCodeNull())
                {
                    xlsxCreator.Cell("E14").Value = dtH[0].TokuisakiCode;
                }
                xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
                if (!dtH[0].IsCategoryNameNull())
                {
                    xlsxCreator.Cell("F18").Value = dtH[0].CategoryName;
                }
                if (!dtH[0].IsFacilityNameNull())
                {
                    xlsxCreator.Cell("F20").Value = dtH[0].FacilityName;
                    strShisetumei = dtH[0].FacilityName;
                }
                if (!dtH[0].IsFacilityPostNoNull())
                {
                    xlsxCreator.Cell("F22").Value = dtH[0].FacilityPostNo;
                }
                if (!dtH[0].IsFacilityAddress1Null())
                {
                    string address = dtH[0].FacilityAddress1;
                    if (!dtH[0].IsFacilityAddress2Null())
                    {
                        address += dtH[0].FacilityAddress2;
                    }
                    xlsxCreator.Cell("N22").Value = address;
                }
                if (!dtH[0].IsSouSuryouNull())
                {
                    xlsxCreator.Cell("A27").Value = dtH[0].SouSuryou;
                }
                if (!dtH[0].IsBikouNull())
                {
                    xlsxCreator.Cell("D30").Value = dtH[0].Bikou;
                }

                //税抜
                if (!dtH[0].IsZeikubunNull())
                {
                    if (dtH[0].Zeikubun == "税抜")
                    {
                        if (!dtH[0].IsGokeiKingakuNull())
                        {
                            xlsxCreator.Cell("D27").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                        }
                        if (!dtH[0].IsSyohiZeiGokeiNull())
                        {
                            xlsxCreator.Cell("J27").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        }
                        if (!dtH[0].IsSoukeiGakuNull())
                        {
                            xlsxCreator.Cell("P27").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                        }
                    }
                    else
                    {
                        xlsxCreator.Cell("AE39").Value = "金額(税込)";
                        if (!dtH[0].IsGokeiKingakuNull())
                        {
                            xlsxCreator.Cell("D27").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                        }
                        xlsxCreator.Cell("J25").Value = "消費税相当額";
                        if (!dtH[0].IsSyohiZeiGokeiNull())
                        {
                            xlsxCreator.Cell("J27").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        }
                        xlsxCreator.Cell("P25").Value = "";
                        xlsxCreator.Cell("P25:U26").Attr.Joint = false;
                        xlsxCreator.Cell("P27:U28").Attr.Joint = false;
                        xlsxCreator.Cell("P25:U26").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                        xlsxCreator.Cell("P27:U28").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                    }
                }
            }

            if (!dr.IsSyouhinMeiNull())
            {
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            }
            if (!dr.IsMekarHinbanNull())
            {
                xlsxCreator.Cell("U" + plus2.ToString()).Value = dr.MekarHinban;
            }
            if (!dr.IsRangeNull())
            {
                xlsxCreator.Cell("Y" + plus1.ToString()).Value = dr.Range;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                xlsxCreator.Cell("Y" + plus2.ToString()).Value = dr.KeitaiMei;
            }
            if (!dr.IsJutyuSuryouNull())
            {
                string mn = dr.MekarHinban;
                if (mn != "NEBIKI" && mn != "SOURYOU" && mn != "KIZAI" && mn != "HOSYOU")
                {
                    xlsxCreator.Cell("AE" + plus2.ToString()).Value = dr.JutyuSuryou;
                }
            }
            if (!dr.IsHyojunKakakuNull())
            {
                xlsxCreator.Cell("AB" + plus2.ToString()).Value = dr.ShiireTanka.ToString("0,0");
            }
            if (!dr.IsJutyuTankaNull())
            {
                xlsxCreator.Cell("AB" + plus1.ToString()).Value = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsUriageNull())
            {
                xlsxCreator.Cell("AE" + plus1.ToString()).Value = dr.Uriage.ToString("0,0");
            }

            if (!dr.IsSisetuMeiNull())
            {
                if (!strShisetumei.Equals(dr.SisetuMei))
                {
                    xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SisetuMei;
                }
                //xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SisetuMei;
            }

            if (!dr.IsSiyouKaishiNull())
            {
                string[] Arydate = dr.SiyouKaishi.ToShortDateString().Split('/');
                string date = @"'" + Arydate[0].Substring(2, 2) + "/" + Arydate[1] + "/" + Arydate[2];
                if (!dr.IsSiyouOwariNull())
                {
                    string[] Arydate2 = dr.SiyouOwari.ToShortDateString().Split('/');
                    date += "～" + "'" + Arydate2[0].Substring(2, 2) + "/" + Arydate2[1] + "/" + Arydate2[2];
                }
                xlsxCreator.Cell("N" + plus2.ToString()).Value = date;
            }

            if (count != row)
            {
                xlsxCreator.Cell("U103:AH104").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                xlsxCreator.Cell("V103").Value = "";
            }
            else
            {
                xlsxCreator.Cell("V103").Value = "計";
                xlsxCreator.Cell("X103").Value = dtH[0].SouSuryou;
                if (dtH[0].Zeikubun == "税抜")
                {
                    xlsxCreator.Cell("AD103").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                }
                else
                {
                    xlsxCreator.Cell("AD103").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                }
                xlsxCreator.Cell("U103:U104").Attr.LineLeft(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U103:AH103").Attr.LineTop(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("AH103:AH104").Attr.LineRight(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U104:AH104").Attr.LineBottom(xlBorderStyle.xbsThin, xlColor.xclBlack);
            }
            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet83(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCountMitsumori, int iPdfCnt, int iPdfCntAllMitumori, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row, int count)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dl.ItemArray;

            xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
            //基準セル
            int kijuncell = 38;
            int plus1 = kijuncell + iGyousu * 4;
            int plus2 = kijuncell + iGyousu * 4 + 2;
            xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;

            //見積書　ヘッダ
            if (iGyousu == 1)
            {
                if (flg == "0")
                {
                    DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                    xlsxCreator.Cell("W19").Value = "代表取締役" + "　" + drD.Delegetion;
                }
                else
                {
                    xlsxCreator.Cell("W19").Value = dr.Busyo + "　" + dr.TanTouName;
                }
                //日付
                if (bDate == "false")
                {
                    string[] date = DateTime.Now.ToShortDateString().Split('/');
                    xlsxCreator.Cell("Y4").Value = date[0];
                    xlsxCreator.Cell("AC4").Value = date[1];
                    xlsxCreator.Cell("AF4").Value = date[2];
                }
                if (bDate.Contains("年"))
                {
                    string[] date = bDate.Replace("年", "/").Replace("月", "/").Replace("日", "/").Split('/');
                    xlsxCreator.Cell("Y4").Value = date[0];
                    xlsxCreator.Cell("AC4").Value = date[1];
                    xlsxCreator.Cell("AF4").Value = date[2];
                }

                if (!dtH[0].IsTokuisakiAddressNull())
                {
                    xlsxCreator.Cell("A6").Value = dtH[0].TokuisakiAddress;
                }
                if (!dtH[0].IsTokuisakiAddress2Null())
                {
                    xlsxCreator.Cell("A8").Value += dtH[0].TokuisakiAddress2;
                }
                if (!dtH[0].IsTokuisakiPostNoNull())
                {
                    xlsxCreator.Cell("A4").Value = "〒" + dtH[0].TokuisakiPostNo;
                }
                if (!dtH[0].IsTokuisakiNameNull())
                {
                    xlsxCreator.Cell("A10").Value = dtH[0].TokuisakiName;
                    if (dtH[0].TokuisakiName.Length > 20)
                    {
                        xlsxCreator.Cell("A10").Attr.FontPoint = 12;
                        xlsxCreator.Cell("A10").Attr.OverReturn = true;
                    }
                }
                if (!dtH[0].IsTokuisakiCodeNull())
                {
                    xlsxCreator.Cell("E14").Value = dtH[0].TokuisakiCode;
                }
                xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
                if (!dtH[0].IsCategoryNameNull())
                {
                    xlsxCreator.Cell("F18").Value = dtH[0].CategoryName;
                }
                if (!dtH[0].IsFacilityNameNull())
                {
                    xlsxCreator.Cell("F20").Value = dtH[0].FacilityName;
                    strShisetumei = dtH[0].FacilityName;
                }
                if (!dtH[0].IsFacilityAddress1Null())
                {
                    string address = dtH[0].FacilityAddress1;
                    if (!dtH[0].IsFacilityAddress2Null())
                    {
                        address += dtH[0].FacilityAddress2;
                    }
                    xlsxCreator.Cell("F22").Value = address;
                }
                if (!dtH[0].IsSouSuryouNull())
                {
                    xlsxCreator.Cell("A27").Value = dtH[0].SouSuryou;
                }
                if (!dtH[0].IsBikouNull())
                {
                    xlsxCreator.Cell("D30").Value = dtH[0].Bikou;
                }

                //税抜
                if (!dtH[0].IsZeikubunNull())
                {
                    if (dtH[0].Zeikubun == "税抜")
                    {
                        if (!dtH[0].IsGokeiKingakuNull())
                        {
                            xlsxCreator.Cell("D27").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                        }
                        if (!dtH[0].IsSyohiZeiGokeiNull())
                        {
                            xlsxCreator.Cell("J27").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        }
                        if (!dtH[0].IsSoukeiGakuNull())
                        {
                            xlsxCreator.Cell("P27").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                        }
                    }
                    else
                    {
                        xlsxCreator.Cell("AE39").Value = "金額(税込)";
                        if (!dtH[0].IsGokeiKingakuNull())
                        {
                            xlsxCreator.Cell("D27").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                        }
                        xlsxCreator.Cell("J25").Value = "消費税相当額";
                        if (!dtH[0].IsSyohiZeiGokeiNull())
                        {
                            xlsxCreator.Cell("J27").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        }
                        xlsxCreator.Cell("P25").Value = "";
                        xlsxCreator.Cell("P25:U26").Attr.Joint = false;
                        xlsxCreator.Cell("P27:U28").Attr.Joint = false;
                        xlsxCreator.Cell("P25:U26").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                        xlsxCreator.Cell("P27:U28").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                    }
                }
            }

            if (!dr.IsSyouhinMeiNull())
            {
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            }
            if (!dr.IsMekarHinbanNull())
            {
                xlsxCreator.Cell("U" + plus2.ToString()).Value = dr.MekarHinban;
            }
            if (!dr.IsRangeNull())
            {
                xlsxCreator.Cell("Y" + plus1.ToString()).Value = dr.Range;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                xlsxCreator.Cell("Y" + plus2.ToString()).Value = dr.KeitaiMei;
            }
            if (!dr.IsJutyuSuryouNull())
            {
                string mn = dr.MekarHinban;
                if (mn != "NEBIKI" && mn != "SOURYOU" && mn != "KIZAI" && mn != "HOSYOU")
                {
                    xlsxCreator.Cell("AE" + plus1.ToString()).Value = dr.JutyuSuryou;
                }
            }
            if (!dr.IsHyojunKakakuNull())
            {
                xlsxCreator.Cell("AB" + plus1.ToString()).Value = int.Parse(dr.HyojunKakaku).ToString("0,0");
            }
            if (!dr.IsJutyuTankaNull())
            {
                xlsxCreator.Cell("AB" + plus2.ToString()).Value = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsUriageNull())
            {
                xlsxCreator.Cell("AE" + plus2.ToString()).Value = dr.Uriage.ToString("0,0");
            }

            if (!dr.IsSisetuMeiNull())
            {
                if (!strShisetumei.Equals(dr.SisetuMei))
                {
                    xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SisetuMei;
                }
                //xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SisetuMei;
            }

            if (!dr.IsSiyouKaishiNull())
            {
                string[] Arydate = dr.SiyouKaishi.ToShortDateString().Split('/');
                string date = @"'" + Arydate[0].Substring(2, 2) + "/" + Arydate[1] + "/" + Arydate[2];
                if (!dr.IsSiyouOwariNull())
                {
                    string[] Arydate2 = dr.SiyouOwari.ToShortDateString().Split('/');
                    date += "～" + "'" + Arydate2[0].Substring(2, 2) + "/" + Arydate2[1] + "/" + Arydate2[2];
                }
                xlsxCreator.Cell("N" + plus2.ToString()).Value = date;
            }

            if (count != row)
            {
                xlsxCreator.Cell("U103:AH104").Attr.Box(xlBoxType.xbtLtc, xlBorderStyle.xbsNone, xlColor.xclWhite);
                xlsxCreator.Cell("V103").Value = "";
            }
            else
            {
                xlsxCreator.Cell("V103").Value = "計";
                xlsxCreator.Cell("X103").Value = dtH[0].SouSuryou;
                if (dtH[0].Zeikubun == "税抜")
                {
                    xlsxCreator.Cell("AD103").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                }
                else
                {
                    xlsxCreator.Cell("AD103").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                }
                xlsxCreator.Cell("U103:U104").Attr.LineLeft(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U103:AH103").Attr.LineTop(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("AH103:AH104").Attr.LineRight(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U104:AH104").Attr.LineBottom(xlBorderStyle.xbsThin, xlColor.xclBlack);
            }
            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet82(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCountMitsumori, int iPdfCnt, int iPdfCntAllMitumori, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row, int count, int intKisaiSu)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dl.ItemArray;

            xlsxCreator.Cell("Z2").Value = dtH[0].MitumoriNo;
            //商品カウント

            //代表者名
            //基準セル
            int kijuncell = 12;
            int plus1 = kijuncell + iGyousu * 4;
            int plus2 = kijuncell + iGyousu * 4 + 2;
            xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;

            if (!dr.IsSyouhinMeiNull())
            {
                xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            }
            if (!dr.IsMekarHinbanNull())
            {
                xlsxCreator.Cell("U" + plus2.ToString()).Value = dr.MekarHinban;
            }
            if (!dr.IsRangeNull())
            {
                xlsxCreator.Cell("Y" + plus1.ToString()).Value = dr.Range;
            }
            if (!dr.IsKeitaiMeiNull())
            {
                xlsxCreator.Cell("Y" + plus2.ToString()).Value = dr.KeitaiMei;
            }
            if (!dr.IsJutyuSuryouNull())
            {
                string mn = dr.MekarHinban;
                if (mn != "NEBIKI" && mn != "SOURYOU" && mn != "KIZAI" && mn != "HOSYOU")
                {
                    xlsxCreator.Cell("AE" + plus1.ToString()).Value = dr.JutyuSuryou;
                }
            }
            if (!dr.IsHyojunKakakuNull())
            {
                xlsxCreator.Cell("AB" + plus1.ToString()).Value = int.Parse(dr.HyojunKakaku).ToString("0,0");
            }
            if (!dr.IsJutyuTankaNull())
            {
                xlsxCreator.Cell("AB" + plus2.ToString()).Value = dr.JutyuTanka.ToString("0,0");
            }
            if (!dr.IsUriageNull())
            {
                xlsxCreator.Cell("AE" + plus2.ToString()).Value = dr.Uriage.ToString("0,0");
            }
            if (!dr.IsSisetuMeiNull())
            {
                if (!strShisetumei.Equals(dr.SisetuMei))
                {
                    xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.SisetuMei;
                }
            }

            if (!dr.IsSiyouKaishiNull())
            {
                string[] Arydate = dr.SiyouKaishi.ToShortDateString().Split('/');
                string date = @"'" + Arydate[0].Substring(2, 2) + "/" + Arydate[1] + "/" + Arydate[2];
                if (!dr.IsSiyouOwariNull())
                {
                    string[] Arydate2 = dr.SiyouOwari.ToShortDateString().Split('/');
                    date += "～" + "'" + Arydate2[0].Substring(2, 2) + "/" + Arydate2[1] + "/" + Arydate2[2];
                }
                xlsxCreator.Cell("N" + plus2.ToString()).Value = date;
            }

            if (row == count)
            {

                int page = 0;
                row = row - 15;
                page = (row / 20) + 2;
                xlsxCreator.Cell("O97").Value = page + "ページ目";
                xlsxCreator.Cell("V97").Value = "計";
                xlsxCreator.Cell("X97").Value = dtH[0].SouSuryou.ToString("0,0");
                if (dtH[0].Zeikubun == "税込")
                {
                    xlsxCreator.Cell("AD97").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                }
                else
                {
                    xlsxCreator.Cell("AD97").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                }
                xlsxCreator.Cell("U97:U98").Attr.LineLeft(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U97:AH97").Attr.LineTop(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("AH97:AH98").Attr.LineRight(xlBorderStyle.xbsThin, xlColor.xclBlack);
                xlsxCreator.Cell("U98:AH98").Attr.LineBottom(xlBorderStyle.xbsThin, xlColor.xclBlack);
            }
            if (intKisaiSu == 20)
            {
                int page = 0;
                row = row - 15;
                page = (row / 20) + 1;
                xlsxCreator.Cell("O97").Value = page + "ページ目";
            }
            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet9(XlsxCreator xlsxCreator, int iGyousu, DataRow dl, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dr = dt.NewT_MitumoriRow();
            dr.ItemArray = dl.ItemArray;

            int kijuncell = 11;
            int plus1 = kijuncell + iGyousu * 2;
            int plus2 = kijuncell + iGyousu * 2 + 1;

            xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;

            if (iGyousu == 1)
            {
                xlsxCreator.Cell("H54").Value = dtH[0].SouSuryou;
            }

            if (flg == "0")
            {
                DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                xlsxCreator.Cell("I9").Value = "代表取締役" + "  " + drD.Delegetion;
            }
            else
            {
                xlsxCreator.Cell("I9").Value = dr["Busyo"] + "  " + dr["TanTouName"];
            }
            if (bDate == "false")
            {
                xlsxCreator.Cell("I1").Value = DateTime.Now.ToLongDateString();
            }
            if (bDate == "true")
            {
                xlsxCreator.Cell("I1").Value = "年" + "        " + "月" + "        " + "日";
            }
            if (bDate.Contains("年"))
            {
                xlsxCreator.Cell("I1").Value = bDate;
            }
            xlsxCreator.Cell("B" + plus1.ToString()).Value = dr.SyouhinMei;
            xlsxCreator.Cell("F" + plus1.ToString()).Value = dr.KeitaiMei;
            xlsxCreator.Cell("H" + plus1.ToString()).Value = dr.JutyuSuryou;
            xlsxCreator.Cell("B" + plus2.ToString()).Value = dr.MekarHinban;
            xlsxCreator.Cell("G" + plus1.ToString()).Value = dr.Range;
            if (!dr.IsTekiyou1Null())
            {
                xlsxCreator.Cell("I" + plus2.ToString()).Value = dr.Tekiyou1;
            }
            xlsxCreator.Cell("B9").Value = "使用施設:" + " " + dr.SisetuMei;

            return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet5(XlsxCreator xlsxCreator, int iGyousu, DataRow dr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {
            DataMitumori.T_MitumoriDataTable dtN = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dl = dtN.NewT_MitumoriRow();
            dl.ItemArray = dr.ItemArray;

            nNo++;
            int kijuncell = 14;
            int plus1 = kijuncell + iGyousu * 3;
            int plus2 = kijuncell + iGyousu * 3 + 1;
            int plus3 = kijuncell + iGyousu * 3 + 2;

            if (iGyousu == 1)
            {

                if (bDate == "false")
                {
                    xlsxCreator.Cell("I1").Value = DateTime.Now.ToLongDateString();
                }
                if (bDate == "true")
                {
                    xlsxCreator.Cell("I1").Value = "年" + "        " + "月" + "        " + "日";
                }
                if (bDate.Contains("年"))
                {
                    xlsxCreator.Cell("I1").Value = bDate;
                }
                if (flg == "0")
                {
                    DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
                    xlsxCreator.Cell("I12").Value = "代表取締役" + "  " + drD.Delegetion;
                }
                else
                {
                    xlsxCreator.Cell("I12").Value = dr["Busyo"] + "  " + dr["TanTouName"];
                }

                if (iGyousu == 1)
                {
                    if (dtH[0].SouSuryou.ToString().Length >= 3)
                    {
                        xlsxCreator.Cell("B10").Attr.FontPoint = 10;
                        xlsxCreator.Cell("B10").Value = dtH[0].SouSuryou;
                    }
                    else
                    {
                        xlsxCreator.Cell("B10").Value = dtH[0].SouSuryou;
                    }
                    if (dtH[0].SoukeiGaku.ToString().Length >= 9)
                    {
                        xlsxCreator.Cell("C10").Attr.FontPoint = 10;
                        xlsxCreator.Cell("C10").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                    }
                    else
                    {
                        xlsxCreator.Cell("C10").Value = "￥" + dtH[0].GokeiKingaku.ToString("0,0");
                    }
                    xlsxCreator.Cell("I2").Value = dl.MitumoriNo;
                    if (!dtH[0].IsNouhinTyokusousakiMei1Null())
                    {
                        if (!string.IsNullOrEmpty(dtH[0].NouhinTyokusousakiMei1))//納品先名
                        {
                            xlsxCreator.Cell("B6").Value = dtH[0].NouhinTyokusousakiMei1 + " " + "様";
                            if (!dtH[0].IsNouhinYubinNull())
                            {
                                xlsxCreator.Cell("B4").Value = "〒" + dtH[0].NouhinYubin;
                            }
                            if (!dtH[0].IsNouhinAddressNull())
                            {
                                xlsxCreator.Cell("C4").Value = dtH[0].NouhinAddress;
                            }
                            if (!dtH[0].IsNouhinAddress2Null())
                            {
                                xlsxCreator.Cell("C5").Value = dtH[0].NouhinAddress2;
                            }
                        }
                    }
                    else//納品先が設定されていない場合は得意先の情報を記載 2022/02/17
                    {
                        if (!string.IsNullOrEmpty(dtH[0].TokuisakiName))
                        {
                            xlsxCreator.Cell("B6").Value = dtH[0].TokuisakiName + " " + "様";
                            if (!dtH[0].IsNouhinYubinNull())
                            {
                                xlsxCreator.Cell("B4").Value = "〒" + dtH[0].TokuisakiPostNo;
                            }
                            if (!dtH[0].IsNouhinAddressNull())
                            {
                                xlsxCreator.Cell("C4").Value = dtH[0].TokuisakiAddress;
                            }
                            if (!dtH[0].IsNouhinAddress2Null())
                            {
                                xlsxCreator.Cell("C5").Value = dtH[0].TokuisakiAddress2;
                            }
                        }
                    }

                    if (dl.Zeikubun == "税込")
                    {
                        xlsxCreator.Cell("D10").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                    }
                    else//税抜
                    {
                        xlsxCreator.Cell("D10").Value = "￥" + dtH[0].SyohiZeiGokei.ToString("0,0");
                        xlsxCreator.Cell("D8").Value = "消費税";
                        xlsxCreator.Cell("E8").Value = "税込合計額";
                        xlsxCreator.Cell("E10").Value = "￥" + dtH[0].SoukeiGaku.ToString("0,0");
                        xlsxCreator.Cell("E8:E9").Attr.Box(xlBoxType.xbtBox, xlBorderStyle.xbsThin, System.Drawing.Color.Black);
                        xlsxCreator.Cell("E10:E11").Attr.Box(xlBoxType.xbtBox, xlBorderStyle.xbsThin, System.Drawing.Color.Black);
                    }
                    if (!dtH[0].IsBikouNull())
                    {
                        xlsxCreator.Cell("C12").Value = dtH[0].Bikou;
                    }
                    xlsxCreator.Cell("H2").Value = dtH[0].MitumoriNo;
                }
            }

            //plus1
            xlsxCreator.Cell("A" + (plus1).ToString()).Value = row;
            xlsxCreator.Cell("B" + (plus1).ToString()).Value = dl.SyouhinMei;
            xlsxCreator.Cell("F" + (plus1).ToString()).Value = dl.KeitaiMei;
            xlsxCreator.Cell("G" + (plus1).ToString()).Value = dl.JutyuSuryou;
            if (dl.HyojunKakaku != "OPEN")
            {
                xlsxCreator.Cell("H" + (plus1).ToString()).Value = dl.JutyuTanka.ToString("0,0");
            }
            else
            {
                xlsxCreator.Cell("H" + (plus1).ToString()).Value = "OPEN";
            }
            xlsxCreator.Cell("I" + (plus1).ToString()).Value = dl.JutyuGokei.ToString("0,0");

            //plus2
            xlsxCreator.Cell("B" + (plus2).ToString()).Value = dl.MekarHinban;
            if (!dl.IsRangeNull())
            {
                xlsxCreator.Cell("D" + (plus2).ToString()).Value = dl.Range;
            }
            if (!dl.IsTekiyou1Null())
            {
                xlsxCreator.Cell("E" + (plus2).ToString()).Value = dl.Tekiyou1;
            }

            //plus3
            if (!dl.IsSiyouKaishiNull())
            {
                if (!dl.IsSiyouOwariNull())
                {
                    xlsxCreator.Cell("B" + (plus3).ToString()).Value = dl.SiyouKaishi + "　~　" + dl.SiyouOwari;
                }
            }
            if (!dl.IsSisetuMeiNull())
            {
                xlsxCreator.Cell("D" + (plus3).ToString()).Value = dl.SisetuMei;
            }
            return xlsxCreator;
            //if (iGyousu == 1)
            //{
            //    nNo = 0;
            //    nGoeki = 0;
            //    nSyouhi = 0;
            //    nMitumori = 0;
            //    nSuryo = 0;
            //    nTanka = 0;
            //    nKinagku = 0;

            //    //xlsxCreator.Cell("A1:I2").Drawing.AddImage(System.Configuration.ConfigurationManager.AppSettings["HeaderImage"]);
            //    //xlsxCreator.Cell("A1:I2").Drawing.Init();
            //    if (bDate == "false")
            //    {
            //        xlsxCreator.Cell("I1").Value = DateTime.Now.ToLongDateString();
            //    }
            //    if (bDate == "true")
            //    {
            //        xlsxCreator.Cell("I1").Value = "年" + "        " + "月" + "        " + "日";
            //    }
            //    if (bDate.Contains("年"))
            //    {
            //        xlsxCreator.Cell("I1").Value = bDate;
            //    }
            //    if (flg == "0")
            //    {
            //        DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
            //        xlsxCreator.Cell("I11").Value = "代表取締役" + "  " + drD.Delegetion;
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("I11").Value = dr["Busyo"] + "  " + dr["TanTouName"];
            //    }
            //    //見積No
            //    string strMitumoriNo = dr["MitumoriNo"].ToString();
            //    //得意先
            //    string sTokuisaki = dr["TokuisakiMei"].ToString();
            //    //使用施設名
            //    string strShisetumei = dr["SisetuMei"].ToString();
            //    //使用期間
            //    string strSiyoukikan =
            //         dr["SiyouKaishi"].ToString();
            //    if (strSiyoukikan != "")
            //    {
            //        DateTime dateKaisi = DateTime.Parse(strSiyoukikan);
            //        string sDate = dateKaisi.ToString("yyy/MM/dd");
            //    }

            //    string strOwarikikan =
            //         dr["SiyouOwari"].ToString();
            //    if (strOwarikikan != "")
            //    {
            //        DateTime dateOwari = DateTime.Parse(strOwarikikan);
            //        string oDate = dateOwari.ToString("yyy/MM/dd");
            //    }

            //    xlsxCreator.Cell("I2").Value = strMitumoriNo;
            //    xlsxCreator.Cell("B4").Value = sTokuisaki + "  様";
            //    xlsxCreator.Cell("C12").Value = strShisetumei;

            //    int suryo = dtH[0].SouSuryou;
            //    int gokei = dtH[0].GokeiKingaku;
            //    int syohizei = dtH[0].SyohiZeiGokei;
            //    int zeikomi = dtH[0].SoukeiGaku;

            //    if (suryo.ToString().Length >= 3)
            //    {
            //        xlsxCreator.Cell("B9").Attr.FontPoint = 10;
            //        xlsxCreator.Cell("B9").Value = suryo.ToString();
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("B9").Value = suryo.ToString();
            //    }
            //    if (gokei.ToString().Length >= 9)
            //    {
            //        xlsxCreator.Cell("C9").Attr.FontPoint = 10;
            //        xlsxCreator.Cell("C9").Value = "￥" + gokei.ToString("0,0");
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("C9").Value = "￥" + gokei.ToString("0,0");
            //    }
            //    if (syohizei.ToString().Length >= 7)
            //    {
            //        xlsxCreator.Cell("D9").Attr.FontPoint = 10;
            //        xlsxCreator.Cell("D9").Value = "￥" + syohizei.ToString("0,0");
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("D9").Value = "￥" + syohizei.ToString("0,0");
            //    }

            //    if (dtH[0].Zeikubun == "税抜")
            //    {
            //        if (zeikomi.ToString().Length >= 9)
            //        {
            //            xlsxCreator.Cell("E9").Attr.FontPoint = 10;
            //            xlsxCreator.Cell("E9").Value = "￥" + zeikomi.ToString("0,0");
            //        }
            //        else
            //        {
            //            xlsxCreator.Cell("E9").Value = "￥" + zeikomi.ToString("0,0");
            //        }
            //    }
            //}

            //// 各明細のセット
            //int iKijunCell = 15;
            //int iPlus = ((2 * iGyousu) - 1);
            //int iPlusUltra = 2 * iGyousu;
            //int nPageCnt = 0; //ページ数分増やす 20180312

            //nPageCnt = (iPdfCnt - 1) * 10;

            //string syou = dr["SyouhinMei"].ToString();
            //string[] arr = syou.Split('/');
            //string x = arr[0];
            ////列番号
            //xlsxCreator.Cell("A" + (iKijunCell + iPlus).ToString()).Value = row;
            ////品目
            //if (!dr.IsNull("SyouhinMei"))
            //{
            //    xlsxCreator.Cell("B" + (iKijunCell + iPlus).ToString()).Value = arr[0];
            //}

            ////品番
            //if (!dr.IsNull("MekarHinban"))
            //{
            //    xlsxCreator.Cell("B" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(dr["MekarHinban"]);
            //}

            ////媒体
            //if (!dr.IsNull("KeitaiMei"))
            //{
            //    xlsxCreator.Cell("F" + (iKijunCell + iPlus).ToString()).Value = Convert.ToString(dr["KeitaiMei"]);
            //}

            ////標準価格
            //if (!dr.IsNull("JutyuTanka"))
            //{
            //    if (dr["JutyuTanka"].ToString() != "OPEN")
            //    {
            //        int hk = int.Parse(dr["JutyuTanka"].ToString());
            //        xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = hk.ToString("0,0");
            //    }
            //    else
            //    {
            //        xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = dr["JutyuTanka"];
            //    }
            //}

            ////数量
            //if (!dr.IsNull("JutyuSuryou"))
            //{
            //    xlsxCreator.Cell("G" + (iKijunCell + iPlus).ToString()).Value = Convert.ToInt16(dr["JutyuSuryou"]);
            //}

            //if (!dr.IsNull("Range"))
            //{
            //    xlsxCreator.Cell("D" + (iKijunCell + iPlusUltra).ToString()).Value = Convert.ToString(dr["Range"]);
            //}


            ////見積単価
            //if (!dr.IsNull("Uriage"))
            //{
            //    int gokei = int.Parse(dr["Uriage"].ToString());
            //    xlsxCreator.Cell("I" + (iKijunCell + iPlus).ToString()).Value = gokei.ToString("0,0");
            //}

            ////数量
            //return xlsxCreator;
        }

        private XlsxCreator StatementsDataSet4(XlsxCreator xlsxCreator, int iGyousu, DataRow dr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei, string flg, string bDate, DataMitumori.T_MitumoriHeaderDataTable dtH, int row)
        {
            DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
            DataMitumori.T_MitumoriRow dl = dt.NewT_MitumoriRow();
            dl.ItemArray = dr.ItemArray;

            int suryo = dtH[0].SouSuryou;
            int gokei = dtH[0].GokeiKingaku;
            int syohizei = dtH[0].SyohiZeiGokei;
            int zeikomi = dtH[0].SoukeiGaku;
            if (suryo.ToString().Length >= 3)
            {
                xlsxCreator.Cell("B9").Attr.FontPoint = 10;
                xlsxCreator.Cell("B9").Value = suryo.ToString();
            }
            else
            {
                xlsxCreator.Cell("B9").Value = suryo.ToString();
            }
            if (gokei.ToString().Length >= 9)
            {
                xlsxCreator.Cell("C9").Attr.FontPoint = 10;
                xlsxCreator.Cell("C9").Value = "￥" + gokei.ToString("0,0");
            }
            else
            {
                xlsxCreator.Cell("C9").Value = "￥" + gokei.ToString("0,0");
            }
            if (syohizei.ToString().Length >= 7)
            {
                xlsxCreator.Cell("D9").Attr.FontPoint = 10;
                xlsxCreator.Cell("D9").Value = "￥" + syohizei.ToString("0,0");
            }
            else
            {
                xlsxCreator.Cell("D9").Value = "￥" + syohizei.ToString("0,0");
            }
            if (dtH[0].Zeikubun == "税抜")
            {
                xlsxCreator.Cell("D7").Value = "消費税";
                xlsxCreator.Cell("E7").Value = "税込合計額";

                if (zeikomi.ToString().Length >= 9)
                {
                    xlsxCreator.Cell("E9").Attr.FontPoint = 10;
                    xlsxCreator.Cell("E9").Value = "￥" + zeikomi.ToString("0,0");
                }
                else
                {
                    xlsxCreator.Cell("E9").Value = "￥" + zeikomi.ToString("0,0");
                }
            }
            if (bDate == "false")
            {
                xlsxCreator.Cell("J1").Value = DateTime.Now.ToLongDateString();
            }
            if (bDate == "true")
            {
                xlsxCreator.Cell("J1").Value = "年" + "        " + "月" + "        " + "日";
            }
            if (bDate.Contains("年"))
            {
                xlsxCreator.Cell("J1").Value = bDate;
            }
            xlsxCreator.Cell("J2").Value = dr["MitumoriNo"];
            xlsxCreator.Cell("B5").Value = dtH[0].SekyuTokuisakiMei + "　様";
            xlsxCreator.Cell("B14").Value = "使用施設名:" + dr["SisetuMei"];
            if (!dtH[0].IsSekyuTokuisakiPostNoNull())
            {
                xlsxCreator.Cell("B3").Value = "〒" + dtH[0].SekyuTokuisakiPostNo;
            }
            if (!dtH[0].IsSekyuTokuisakiAddressNull())
            {
                xlsxCreator.Cell("C3").Value = dtH[0].SekyuTokuisakiAddress;
            }
            if (!dtH[0].IsSekyuTokuisakiAddress2Null())
            {
                xlsxCreator.Cell("C4").Value = dtH[0].SekyuTokuisakiAddress2;
            }
            string s = dr["JutyuSuryou"].ToString();
            int iKijun = 16;

            xlsxCreator.Cell("A" + (iKijun + iGyousu).ToString()).Value = row;

            //品目
            if (!dr.IsNull("SyouhinMei"))
            {
                string sm = dr["SyouhinMei"].ToString();
                xlsxCreator.Cell("B" + (iKijun + iGyousu).ToString()).Value = sm;
            }
            DataMaster.M_AccountPageRow drD = ClassMaster.GetDaihyo(flg, Global.GetConnection());
            if (flg == "0")
            {
                xlsxCreator.Cell("J8").Value = "";
                xlsxCreator.Cell("J8").Value = "代表取締役" + "  " + drD.Delegetion;
            }
            else
            {
                xlsxCreator.Cell("J8").Value = dr["Busyo"] + "  " + dr["TanTouName"];
            }

            //品番
            if (!dr.IsNull("MekarHinban"))
            {
                xlsxCreator.Cell("F" + (iKijun + iGyousu).ToString()).Value = Convert.ToString(dr["MekarHinban"]);
            }

            //媒体
            if (!dr.IsNull("KeitaiMei"))
            {
                xlsxCreator.Cell("G" + (iKijun + iGyousu).ToString()).Value = Convert.ToString(dr["KeitaiMei"]);
            }

            //標準価格
            if (!dr.IsNull("JutyuTanka"))
            {
                if (dr["MekarHinban"].ToString() != "0")
                {
                    if (dr["JutyuTanka"].ToString() != "OPEN")
                    {
                        int hk = int.Parse(dr["JutyuTanka"].ToString());
                        xlsxCreator.Cell("I" + (iKijun + iGyousu).ToString()).Value = hk.ToString("0,0");
                        nGoeki += hk;
                    }
                    else
                    {
                        xlsxCreator.Cell("I" + (iKijun + iGyousu).ToString()).Value = dr["JutyuTanka"];
                    }
                }
            }

            //数量
            if (!dr.IsNull("JutyuSuryou"))
            {
                if (dr["MekarHinban"].ToString() != "0")
                {
                    xlsxCreator.Cell("H" + (iKijun + iGyousu).ToString()).Value = Convert.ToInt16(dr["JutyuSuryou"]);
                }
            }

            if (!dr.IsNull("Uriage"))
            {
                int uri = int.Parse(dr["Uriage"].ToString());
                xlsxCreator.Cell("J" + (iKijun + iGyousu).ToString()).Value = uri.ToString("0,0");
            }
            return xlsxCreator;
        }

        private Doc MakePdfData3(string type, DataSet1.KariMitsumoriDataTable dt, XlsxCreator xlsxCreator, Doc pdf)
        {
            int gokei = 0;
            //一枚に表示出来る最大行数 
            int iViewCount = 20;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dt.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {

                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();

                        iGyousu = 1;

                    }
                    iPdfCnt++;

                    // テンプレートの読み込み
                    if (type == "Mitumori")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["MitumoriFormat4"], "");
                    }
                    if (type == "Nouhin")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["NouhinForm2"], "");
                    }
                    if (type == "Sekyu")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["SeikyuForm"], "");
                    }
                }
                int SouGokei = gokei;
                //xlsxCreator = StatementsDataSet(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, SouGokei);

                // 行数カウントアップ
                iGyousu++;
            }
            //もしNo分明細がなければ追加で処理 20180312
            //if (dt.Rows.Count % 10 != 0)
            //    for (int i = dt.Rows.Count % 10; i < 10; i++)
            //    {
            //        // 各明細のセット
            //        int iKijunCell = 21;
            //        //int iPlus = (iGyousu - 1) * 2;
            //        int nPageCnt = 0; //ページ数分増やす 20180312

            //        // No
            //        nPageCnt = (iPdfCnt - 1) * 10;
            //        xlsxCreator.Cell("B" + (iKijunCell).ToString()).Value = (iGyousu + nPageCnt).ToString();

            //        // 行数カウントアップ
            //        iGyousu++;
            //    }

            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;
        }

        internal void OrderPrint(string type, string[] strOrderedAry, string[] strShiireAry)
        {
            Doc pdf = new Doc();
            DataSet1.T_OrderedDataTable dt = null;
            for (int x = 0; x < strShiireAry.Length; x++)
            {
                dt = ClassOrdered.GetOrdered2(strShiireAry[x], Global.GetConnection());
                XlsxCreator xlsxCreator;
                xlsxCreator = new XlsxCreator();

                pdf = MakePdfDataOrdered(type, dt, xlsxCreator, pdf);
            }
            theData = pdf.GetData();
        }

        private Doc MakePdfDataOrdered(string type, DataSet1.T_OrderedDataTable dt, XlsxCreator xlsxCreator, Doc pdf)
        {

            //一枚に表示出来る最大行数 
            int iViewCount = 8;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dt.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dt.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成


            for (int i = 0; i < dt.Count; i++)
            {
                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {
                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();
                        iGyousu = 1;
                    }
                }

                iPdfCnt++;

                if (type == "Ordered")
                {
                    xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["OrderFormat"], "");
                }

                xlsxCreator = StatementsDataSet3(xlsxCreator, iGyousu, dt.Rows[i], iViewCount, iPdfCnt, iPdfCntAll);

                // 行数カウントアップ
                iGyousu++;


            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;

        }

        private XlsxCreator StatementsDataSet3(XlsxCreator xlsxCreator, int iGyousu, DataRow dr, int iViewCount, int iPdfCnt, int iPdfCntAll)
        {
            nNo++;

            if (iGyousu == 1)
            {
                nNo = 0;
                nGoeki = 0;
                nSyouhi = 0;
                nMitumori = 0;
                nSuryo = 0;
                nTanka = 0;
                nKinagku = 0;
                Suryo = 0;
            }

            string strOrdered = dr["OrderedNo"].ToString();
            string sTokuisaki = dr["TokuisakiMei"].ToString();
            //使用区分
            string strKubun = dr["CategoryName"].ToString();
            //使用施設名
            string strShisetumei = dr["FacilityName"].ToString();
            //施設住所
            string strJusyo = dr["FacilityJusyo1"].ToString();
            string strJusyo2 = dr["FacilityJusyo2"].ToString();
            //使用期間
            string strSiyoukikan =
                 dr["SiyouKaishi"].ToString();
            if (strSiyoukikan != "")
            {
                DateTime dateKaisi = DateTime.Parse(strSiyoukikan);
                string sDate = dateKaisi.ToString("yyy/MM/dd");
            }

            string strOwarikikan =
                 dr["SiyouiOwari"].ToString();
            if (strOwarikikan != "")
            {
                DateTime dateOwari = DateTime.Parse(strOwarikikan);
                string oDate = dateOwari.ToString("yyy/MM/dd");
            }
            xlsxCreator.Cell("P1").Value = strOrdered;
            xlsxCreator.Cell("P2").Value = DateTime.Now.ToShortDateString();
            xlsxCreator.Cell("P10").Value = SessionManager.User.M_user.UserName;
            string shiCode = dr["ShiireSakiName"].ToString();
            DataMaster.M_ShiiresakiRow dl = ClassMaster.GetShiireName(shiCode, Global.GetConnection());
            xlsxCreator.Cell("B4").Value = dl.ShiiresakiMei1;
            xlsxCreator.Cell("B5").Value = dl.TantoBusyoMei + "   " + dl.TantosyaMei + " " + "様";
            xlsxCreator.Cell("B6").Value = "TEL:" + dl.ShiiresakiTell + "   " + "FAX:" + dl.ShiiresakiFax;


            int iKijunCell = 15;
            int iPlus = 4 * iGyousu;
            int iPlus2 = 4 * iGyousu + 1;
            int iPlus3 = 4 * iGyousu + 2;
            int iPlus4 = 4 * iGyousu + 3;

            if (!dr.IsNull("ProductName"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus).ToString()).Value = dr["ProductName"];
            }

            if (!dr.IsNull("CategoryName"))
            {
                xlsxCreator.Cell("B3").Value = "発注書【" + dr["CategoryName"] + "】";
            }

            if (!dr.IsNull("FacilityName"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus2).ToString()).Value = dr["FacilityName"];
            }
            if (!dr.IsNull("FacilityJusyo1"))
            {
                xlsxCreator.Cell("H" + (iKijunCell + iPlus2).ToString()).Value = dr["FacilityJusyo1"];
                if (!dr.IsNull("FacilityJusyo2"))
                {
                    string jusyo = dr["FacilityJusyo2"].ToString();
                    xlsxCreator.Cell("H" + (iKijunCell + iPlus2).ToString()).Value = dr["FacilityJusyo2"] + jusyo;
                }
            }
            if (!dr.IsNull("SiyouKaishi"))
            {
                string end = dr["SiyouiOwari"].ToString();
                xlsxCreator.Cell("B" + (iKijunCell + iPlus3).ToString()).Value = dr["SiyouKaishi"] + "～" + end;
            }
            if (!dr.IsNull("Range"))
            {
                xlsxCreator.Cell("F" + (iKijunCell + iPlus).ToString()).Value = dr["Range"];
            }
            if (!dr.IsNull("Media"))
            {
                xlsxCreator.Cell("J" + (iKijunCell + iPlus).ToString()).Value = dr["Media"];
            }
            if (!dr.IsNull("MekerNo"))
            {
                xlsxCreator.Cell("D" + (iKijunCell + iPlus).ToString()).Value = dr["MekerNo"];
            }
            if (!dr.IsNull("JutyuSuryou"))
            {
                xlsxCreator.Cell("L" + (iKijunCell + iPlus).ToString()).Value = dr["JutyuSuryou"];
                string s = dr["JutyuSuryou"].ToString();
                int ss = int.Parse(s);
                nSuryo += ss;
                Suryo = ss;
            }
            if (!dr.IsNull("ShiireTanka"))
            {
                int jt = int.Parse(dr["ShiireTanka"].ToString());
                xlsxCreator.Cell("N" + (iKijunCell + iPlus).ToString()).Value = jt.ToString("0,0");
                string sTanka = dr["ShiireTanka"].ToString();
                int nT = int.Parse(sTanka);
                nTanka = nT;
            }

            if (!dr.IsNull("FacilityTel"))
            {
                xlsxCreator.Cell("F" + (iKijunCell + iPlus2).ToString()).Value = dr["FacilityTel"];
            }

            int kingaku = nTanka * Suryo;
            gKingaku += kingaku;
            xlsxCreator.Cell("P" + (iKijunCell + iPlus).ToString()).Value = kingaku.ToString("0,0");
            xlsxCreator.Cell("N13").Value = gKingaku.ToString("0,0");
            xlsxCreator.Cell("I13").Value = nSuryo;

            return xlsxCreator;
        }


        private Doc MakePdfData2(string type, DataJutyu.T_JutyuDataTable dd, XlsxCreator xlsxCreator, Doc pdf, int gokei)
        {
            //一枚に表示出来る最大行数 
            int iViewCount = 8;

            //何枚のPDFが出力されるか算出
            int iPdfCntAll = 0;
            decimal dTemp = 0;

            // データ数が最大行数以下なら全体のページ数は1ページ
            if (dd.Rows.Count <= iViewCount)
            {
                iPdfCntAll = 1;
            }
            else    // 最大行数行以上の場合
            {
                dTemp = (decimal)dd.Rows.Count / (decimal)iViewCount;
                iPdfCntAll = (int)Math.Ceiling(dTemp);
            }

            int iPdfCnt = 0;    //PDF出力枚数(ページ数)
            int iGyousu = 1;    //行数カウント

            this.ms = new System.IO.MemoryStream();
            Doc page1 = new Doc();    //追加ページ作成

            for (int i = 0; i < dd.Rows.Count; i++)
            {

                // 改ページ条件は明細の行数が最大行数に達した時
                if (iGyousu == iViewCount + 1 || iGyousu == 1)
                {
                    if (iGyousu == iViewCount + 1)
                    {
                        xlsxCreator.CloseBook(true, this.ms, false);
                        page1.Read(this.ms.ToArray());
                        pdf.Append(page1);

                        //ページを追加したら初期化
                        this.ms = new System.IO.MemoryStream();
                        page1 = new Doc();
                        xlsxCreator = new ExcelCreator6.XlsxCreator();
                        iGyousu = 1;
                    }
                    iPdfCnt++;

                    if (type == "Order")
                    {
                        xlsxCreator.OpenBook(System.Configuration.ConfigurationManager.AppSettings["OrderForm3"], "");
                    }
                }
                int soGokei = gokei;

                xlsxCreator = StatementsDataSet2(xlsxCreator, iGyousu, dd.Rows[i], iViewCount, iPdfCnt, iPdfCntAll, soGokei);

                // 行数カウントアップ
                iGyousu++;

            }
            xlsxCreator.CloseBook(true, this.ms, false);
            page1.Read(this.ms.ToArray());
            pdf.Append(page1);        //ページ追加

            return pdf;

        }

        private XlsxCreator StatementsDataSet2(XlsxCreator xlsxCreator, int iGyousu, DataRow dr, int iViewCount, int iPdfCnt, int iPdfCntAll, int souGokei)
        {
            nNo++;

            if (iGyousu == 1)
            {
                nNo = 0;
                nGoeki = 0;
                nSyouhi = 0;
                nMitumori = 0;
                nSuryo = 0;
                nTanka = 0;
                nKinagku = 0;
                Suryo = 0;
            }

            string strOrdered = dr["JutyuNo"].ToString();
            string sTokuisaki = dr["TokuisakiMei"].ToString();
            //使用区分
            string strKubun = dr["CategoryName"].ToString();
            //使用施設名
            string strShisetumei = dr["SisetuMei"].ToString();
            //施設住所
            string strJusyo = dr["SisetuJusyo1"].ToString();
            string strJusyo2 = dr["SisetuJusyo2"].ToString();
            //使用期間
            string strSiyoukikan =
                 dr["SiyouKaishi"].ToString();
            if (strSiyoukikan != "")
            {
                DateTime dateKaisi = DateTime.Parse(strSiyoukikan);
                string sDate = dateKaisi.ToString("yyy/MM/dd");
            }

            string strOwarikikan =
                 dr["SiyouOwari"].ToString();
            if (strOwarikikan != "")
            {
                DateTime dateOwari = DateTime.Parse(strOwarikikan);
                string oDate = dateOwari.ToString("yyy/MM/dd");
            }
            xlsxCreator.Cell("I2").Value = strOrdered;
            xlsxCreator.Cell("I3").Value = DateTime.Now.ToShortDateString();

            int iKijunCell = 18;
            int iPlus = 4 * iGyousu;
            int iPlus2 = 4 * iGyousu + 1;
            int iPlus3 = 4 * iGyousu + 2;
            int iPlus4 = 4 * iGyousu + 3;

            //数量合計（G16）
            if (!dr.IsNull("JutyuSuryou"))
            {
                int jSuryo = int.Parse(dr["JutyuSuryou"].ToString());
                Suryo += jSuryo;
                xlsxCreator.Cell("G16").Value = Suryo;
                nSuryo = jSuryo;
            }

            if (!dr.IsNull("SyouhinMei"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus).ToString()).Value = dr["SyouhinMei"];
            }

            if (!dr.IsNull("SisetuMei"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus2).ToString()).Value = dr["SisetuMei"];
            }
            if (!dr.IsNull("SisetuJusyo1"))
            {
                xlsxCreator.Cell("B" + (iKijunCell + iPlus3).ToString()).Value = dr["SisetuJusyo1"];
                if (!dr.IsNull("SisetuJusyo2"))
                {
                    string jusyo = dr["SisetuJusyo2"].ToString();
                    xlsxCreator.Cell("B" + (iKijunCell + iPlus3).ToString()).Value = dr["SisetuJusyo1"] + jusyo;
                }
            }
            if (!dr.IsNull("SiyouKaishi"))
            {
                string end = dr["SiyouOwari"].ToString();
                xlsxCreator.Cell("B" + (iKijunCell + iPlus4).ToString()).Value = dr["SiyouKaishi"] + end;
            }
            if (!dr.IsNull("Range"))
            {
                xlsxCreator.Cell("D" + (iKijunCell + iPlus).ToString()).Value = dr["Range"];
            }
            if (!dr.IsNull("KeitaiMei"))
            {
                xlsxCreator.Cell("F" + (iKijunCell + iPlus).ToString()).Value = dr["KeitaiMei"];
            }
            if (!dr.IsNull("MekarHinban"))
            {
                xlsxCreator.Cell("G" + (iKijunCell + iPlus).ToString()).Value = dr["MekarHinban"];
            }
            if (!dr.IsNull("JutyuSuryou"))
            {
                xlsxCreator.Cell("H" + (iKijunCell + iPlus).ToString()).Value = dr["JutyuSuryou"];
            }
            if (!dr.IsNull("JutyuTanka"))
            {
                xlsxCreator.Cell("I" + (iKijunCell + iPlus).ToString()).Value = dr["JutyuTanka"].ToString();
                string sTanka = dr["JutyuTanka"].ToString();
                int nT = int.Parse(sTanka);
                nTanka = nT;
            }

            int kin = nSuryo * nTanka;
            xlsxCreator.Cell("J" + (iKijunCell + iPlus).ToString()).Value = kin.ToString("0,0");
            xlsxCreator.Cell("I16").Value = souGokei.ToString("0,0");

            return xlsxCreator;
        }
    }
}
