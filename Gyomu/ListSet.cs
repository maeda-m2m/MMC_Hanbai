using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu
{
    public class ListSet
    {
        internal static void SetTanto(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataDrop.M_TantoDataTable dt =
                ClassDrop.TantoDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].UserName, dt[i].UserID));
            }
        }

        internal static void GetTokuiShiire(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            if (e.Text.Trim() != "")
            {
                DataLedger.M_Tokuisaki2DataTable dtT = ClassLedger.GetTokuisakiList(e.Text.Trim(), Global.GetConnection());
                for (int t = 0; t < dtT.Count; t++)
                {
                    rad.Items.Add(new RadComboBoxItem(dtT[t].TokuisakiRyakusyo, dtT[t].TokuisakiCode.ToString()));
                }
                DataLedger.M_ShiiresakiDataTable dtS = ClassLedger.GetShiiresaki(e.Text.Trim(), Global.GetConnection());
                for (int s = 0; s < dtS.Count; s++)
                {
                    rad.Items.Add(new RadComboBoxItem(dtS[s].ShiiresakiRyakusyou, dtS[s].ShiiresakiCode.ToString()));
                }
            }
        }

        internal static void SetProduct2(object sender, RadComboBoxItemsRequestedEventArgs e, string cate, string shiire)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_2DataTable dt =
                    Class1.GetProduct3(e.Text.Trim(), cate, shiire, Global.GetConnection());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rad.Items.Add(new RadComboBoxItem
                        (dt[i].SyouhinMei + "/" + dt[i].Hanni,
                        dt[i].SyouhinCode + "/" + dt[i].SyouhinMei + "/" + dt[i].Makernumber + "/" + dt[i].Media + "/" + dt[i].Hanni + "/" + dt[i].ShiireCode + "/" + dt[i].ShiireName + "/" + dt[i].CategoryCode + "/" + dt[i].Categoryname + "/" + dt[i].ShiireKakaku + "/" + dt[i].WareHouse));
                }
            }
        }

        internal static void SetPrdt(RadComboBox productName)
        {
            productName.Items.Clear();
            productName.Items.Add(new RadComboBoxItem(productName.EmptyMessage, ""));

            DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct1(productName.Text.Trim(), Global.SqlConn);

            for (int i = 0; i < dt.Count; i++)
            {
                productName.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei, dt[i].SyouhinCode));
            }
        }


        internal static void SetShiireSaki(object radMaker)
        {
            throw new NotImplementedException();
        }

        internal static void SetTanto2(object sender, RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataDrop.M_TantoDataTable dt =
                ClassDrop.TantoDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].UserName, dt[i].UserID));
            }
        }

        internal static void SetShiireSaki4(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataMaster.M_ShiiresakiDataTable dt = ClassMaster.GetShiiresaki(e.Text.Trim(), Global.GetConnection());

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].ShiiresakiRyakusyou, dt[i].ShiiresakiCode.ToString()));
                }
            }
        }

        internal static void SetProduct(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcb = new RadComboBox();
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct1(e.Text.Trim(), Global.SqlConn);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Categoryname, dt[i].Categoryname));
                }
            }
        }

        internal static void SetTokuisaki(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_TokuisakiDataTable dt = ClassMitumori.TokuisakiMei(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].TokuisakiRyakusyou, dt[i].TokuisakiCode.ToString()));
            }
        }

        internal static void GetProduct(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_2DataTable dt = Class1.GetProduct4(e.Text.Trim(), Global.GetConnection());
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei, dt[i].SyouhinCode.ToString()));
                }
            }
        }



        //internal static void SetBumon2(object sender, string a)
        //{
        //    rad.Items.Clear();
        //    rad.Items.Add(new RadComboBoxItem(""));

        //    DataDrop.M_Bumon_NewDataTable dt =
        //        ClassDrop.BumonNewDrop(Global.GetConnection());

        //    for (int i = 0; i < dt.Count; i++)
        //    {
        //        rad.Items.Add(new RadComboBoxItem(dt[i].BomonName, dt[i].BumonCode.ToString()));
        //    }
        //}

        internal static void SetTyokusousaki(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Facility_NewDataTable dt = ClassMitumori.GetShisetsu(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].FacilityName1, dt[i].FacilityNo.ToString()));
            }
        }


        internal static void SetCity(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataDrop.M_CityDataTable dt =
                ClassDrop.CityDrop(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CityName, dt[i].CityCode.ToString()));
            }

        }

        internal static void SetCity(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;

            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            string strCity = e.Text;
            if (e.Text.Trim() != "")
            {
                DataDrop.M_CityDataTable dt = ClassDrop.GetCity(strCity, Global.GetConnection());
                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rad.Items.Add(new RadComboBoxItem(dt[i].CityName, dt[i].CityCode.ToString()));
                }
            }
        }

        internal static void SetSyohin(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Syohin_NewDataTable dt =
                ClassDrop.SyouhinMeiDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                if (!dt[i].IsMediaNull())
                    rad.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].MekarHinban));
            }
        }

        internal static void SetCateSyohin(RadComboBox rad, string sValue)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Kakaku_NewDataTable dt =
                ClassDrop.CateSyouhinMeiDrop(sValue, Global.GetConnection());

            int nCount = 0;

            if (dt.Rows.Count < 15)
            {
                nCount = dt.Rows.Count;
            }
            else
            {
                nCount = 15;
            }

            for (int i = 0; i < nCount; i++)
            {
                if (!dt[i].IsMediaNull())
                    rad.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].Makernumber));
            }
        }


        internal static void SetKakakuShiire(RadComboBox rad, string sValue)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Kakaku_NewDataTable dt =
                ClassDrop.KakakuShiireDrop(sValue, Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].ShiireName, dt[i].ShiireCode));
            }
        }

        internal static void SetKakakuCate(RadComboBox rad, string sValue, string sShiire)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Kakaku_NewDataTable dt =
                ClassDrop.KakakuCateDrop(sValue, sShiire, Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].Categoryname, dt[i].CategoryCode.ToString()));
            }
        }

        internal static void SetKensakuSyohin(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            string name = "";
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_2DataTable dtN = ClassDrop.GetProduct(e.Text.Trim(), Global.GetConnection());
                //DataDrop.M_Kakaku_NewDataTable dt = ClassDrop.GetKensakuSyouhin(e.Text.Trim(), Global.SqlConn);
                DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
                for (int s = 0; s < dtN.Count; s++)
                {
                    DataSet1.M_Kakaku_2Row dl = dtN[s];
                    DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
                    if (name.Contains(dl.SyouhinMei))
                    {

                    }
                    else
                    {
                        name += dl.SyouhinMei + ",";
                        dr.ItemArray = dl.ItemArray;
                        dt.AddM_Kakaku_2Row(dr);
                    }
                }
                int itemOffset = e.NumberOfItems;
                //表示件数を最大15件に設定
                int endOffset = 0;
                if (dt.Rows.Count < 15)
                {
                    endOffset = dt.Rows.Count;
                }
                else
                {
                    endOffset = 15;
                }
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].Makernumber));
                }
            }
        }

        internal static void SetProductname(object sender, RadComboBoxItemsRequestedEventArgs e, string cate)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.GetProduct5(e.Text.Trim(), cate, Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media + "/" + dt[i].Hanni, dt[i].SyouhinCode + "/" + dt[i].Media));
                }
            }
        }

        internal static void setproduct(RadComboBox productName, string a, string c)
        {
            RadComboBox rad = (RadComboBox)productName;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Kakaku_New1DataTable dt =
                ClassMitumori.getProduct(a, c, Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].SyouhinCode + "/" + dt[i].Media));
            }
        }

        internal static void SetProduct(RadComboBox productName, string a)
        {
            RadComboBox rad = (RadComboBox)productName;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Kakaku_New1DataTable dt =
                ClassMitumori.GetProduct(a, Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].SyouhinCode + "/" + dt[i].Media));
            }
        }

        internal static void SetKensakuCateSyohin(string sCate, object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            DataDrop.M_Kakaku_NewDataTable dt = ClassDrop.GetKensakuCateSyouhin(sCate, e.Text.Trim(), Global.SqlConn);

            int itemOffset = e.NumberOfItems;
            //表示件数を最大15件に設定
            int endOffset = 0;
            if (dt.Rows.Count < 15)
            {
                endOffset = dt.Rows.Count;
            }
            else
            {
                endOffset = 15;
            }
            e.EndOfItems = endOffset == dt.Rows.Count;
            for (int i = itemOffset; i < endOffset; i++)
            {
                if (!dt[i].IsMediaNull())
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].Makernumber));
            }
        }

        internal static void SetShiire(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Shiire_NewDataTable dt =
                ClassDrop.ShiireDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].ShiireCode.ToString()));
            }
        }

        internal static void SetKensakuMeisyo(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            DataDrop.M_Syohin_NewDataTable dt =
                ClassDrop.KensakuMeisyu(e.Text.Trim(), Global.SqlConn);

            int itemOffset = e.NumberOfItems;
            int endOffset = dt.Rows.Count;
            e.EndOfItems = endOffset == dt.Rows.Count;
            for (int i = itemOffset; i < endOffset; i++)
            {
                rcb.Items.Add(new RadComboBoxItem(dt[i].ShiireMei, dt[i].ShiireMei));
            }
        }

        internal static void SetRyakusyou(RadComboBox rad, RadComboBox box)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));
            box.Items.Clear();
            box.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Customer_NewDataTable dt =
                ClassDrop.RyakusyoDrop(Global.GetConnection());
            //最大検索数15件
            for (int i = 0; i < 15; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CustomerCode + "/" + dt[i].Abbreviation, dt[i].CustomerCode));
                box.Items.Add(new RadComboBoxItem(dt[i].CustomerCode + "/" + dt[i].Abbreviation, dt[i].CustomerCode));
            }
        }

        internal static void SetFacility(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataDrop.M_Facility_NewDataTable dt = ClassDrop.KensakuTyokuso(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                //表示件数を最大15件に設定
                int endOffset = 0;
                if (dt.Rows.Count < 15)
                {
                    endOffset = dt.Rows.Count;
                }
                else
                {
                    endOffset = 15;
                }
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].FacilityName1, dt[i].FacilityNo.ToString()));
                }
            }
        }

        internal static void SetShiire(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Shiire_NewDataTable dt = ClassKensaku.GetMaker(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].ShiireCode.ToString()));
                }
            }

        }

        internal static void SetProductname(object sender, string a, string c)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Kakaku_New1DataTable dt =
                ClassMitumori.getProduct(a, c, Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].SyouhinCode + "/" + dt[i].Media));
            }
        }

        internal static void SetProductName2(object sender, string a, string cate)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (a != "")
            {
                DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct3(a, cate, Global.SqlConn);

                for (int i = 0; i < dt.Count; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + "/" + dt[i].Media, dt[i].SyouhinCode + "/" + dt[i].Media));
                }
            }
        }

        internal static void SetRyakusyou2(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Customer_NewDataTable dt =
                ClassDrop.RyakusyoDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CustomerCode + "/" + dt[i].Abbreviation, dt[i].CustomerCode));
            }
        }


        internal static void SetBumon(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_BumonDataTable dt =
                ClassDrop.BumonDorp(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].Busyo, dt[i].BumonKubun.ToString()));
            }
        }

        internal static void SetTyokuso(RadComboBox radT, RadComboBox radS)
        {
            radT.Items.Clear();
            radT.Items.Add(new RadComboBoxItem(""));

            radS.Items.Clear();
            radS.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Facility_NewDataTable dt =
                ClassDrop.TyokusoDrop(Global.GetConnection());

            for (int i = 0; i < 15; i++)
            {
                radT.Items.Add(new RadComboBoxItem(dt[i].FacilityName1, dt[i].FacilityName1.ToString()));
                radS.Items.Add(new RadComboBoxItem(dt[i].FacilityName1, dt[i].FacilityName1.ToString()));
            }
        }

        internal static void SetCate(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_CategoryDataTable dt =
                ClassDrop.CateDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CategoryName, dt[i].Category.ToString()));
            }
        }

        internal static void SetHattyuMei(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Syohin_NewDataTable dt =
                ClassDrop.SyouhinShiireDrop(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].ShiireMei, dt[i].ShiireMei));
            }
        }


        internal static void SETproduct(object sender, string s)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (s != "")
            {
                DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct2(s, Global.SqlConn);

                for (int i = 0; i < dt.Count; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei, dt[i].SyouhinMei));
                }
            }
        }

        internal static void SetShiireSaki3(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            if (e.Text.Trim() != "")
            {
                DataSet1.M_Shiire_NewDataTable dt =
                    ClassMitumori.GetShiireDT(e.Text.Trim(), Global.GetConnection());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rad.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].ShiireName));
                }
            }
        }

        internal static void SetKensakutRyakusyou(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            DataDrop.M_Customer_NewDataTable dt = ClassDrop.GetRyakusyou(e.Text.Trim(), Global.SqlConn);

            int itemOffset = e.NumberOfItems;
            //表示件数を最大15件に設定
            int endOffset = 0;
            if (dt.Rows.Count < 15)
            {
                endOffset = dt.Rows.Count;
            }
            else
            {
                endOffset = 15;
            }
            e.EndOfItems = endOffset == dt.Rows.Count;
            for (int i = itemOffset; i < endOffset; i++)
            {
                rcb.Items.Add(new RadComboBoxItem(dt[i].CustomerCode + "/" + dt[i].Abbreviation, dt[i].CustomerCode));
            }
        }

        internal static void SetKensakuTyokusoSaki(object sender, RadComboBoxItemsRequestedEventArgs e)

        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataDrop.M_Facility_NewDataTable dt = ClassDrop.KensakuTyokuso(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                //表示件数を最大15件に設定
                int endOffset = 0;
                if (dt.Rows.Count < 15)
                {
                    endOffset = dt.Rows.Count;
                }
                else
                {
                    endOffset = 15;
                }
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].FacilityName1, dt[i].FacilityName1));
                }
            }
        }

        internal static void SetcateSerch(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataDrop.M_CategoryDataTable dt = ClassDrop.Serchcate(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].CategoryName, dt[i].Category.ToString()));
                }
            }
        }

        internal static void SerchBumon(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataDrop.M_BumonDataTable dt = ClassDrop.SerchBumon(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Busyo, dt[i].BumonKubun.ToString()));
                }
            }
        }

        internal static void SetTanto(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_TokuisakiDataTable dt =
                ClassMitumori.TokuisakiMei(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].TokuisakiMei, dt[i].TokuisakiCode.ToString()));
            }
        }

        internal static void SetTanto3(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_TantoDataTable dt = ClassKensaku.GetBumon2(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Busyo, dt[i].Busyo));
                }
            }
        }

        internal static void SetBumon2(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataSet1.M_TantoDataTable dt = ClassKensaku.GetTanto2(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].UserName, dt[i].UserName));
                }
            }
        }

        public static void SetCategory(string s)
        {
            RadComboBox rcb = new RadComboBox();
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (s != "")
            {
                DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct1(s, Global.SqlConn);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Categoryname, dt[i].Categoryname));
                }
                rcb.DataSource = dt;
                rcb.DataBind();
            }


        }

        internal static void SetBumon2(string a, RadComboBox rad)
        {
            rad.Items.Clear();
            DataSet1.M_TantoDataTable dt = ClassKensaku.GetTanto2(a, Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].BumonName, dt[i].Busyo.ToString()));
            }
        }



        internal static void SetBumon(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_TokuisakiDataTable dt =
                ClassMitumori.TokuisakiMei(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].TokuisakiMei, dt[i].TokuisakiCode.ToString()));
            }
        }

        internal static void SetMaker(RadComboBox radMaker)
        {
            radMaker.Items.Clear();
            radMaker.Items.Add(new RadComboBoxItem(radMaker.EmptyMessage, ""));

            DataSet1.M_Shiire_NewDataTable dt = ClassMitumori.GetShiireDT(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                radMaker.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].ShiireCode.ToString()));
            }
        }

        internal static void SettingProduct(object sender, RadComboBoxItemsRequestedEventArgs e, string cate)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            //rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.GetProduct5(e.Text.Trim(), cate, Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Makernumber + "/" + dt[i].SyouhinMei + "(" + dt[i].Media + ")" + "/" + dt[i].Hanni, dt[i].SyouhinCode + "/" + dt[i].Media + "/" + dt[i].Hanni));
                }
            }
        }

        internal static void SetTyokusousaki(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Tyokusosaki1DataTable dt = ClassMitumori.GetTyokusou1(e.Text.Trim(), Global.GetConnection());


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rad.Items.Add(new RadComboBoxItem(dt[i].TyokusousakiMei1, dt[i].TyokusousakiCode.ToString()));
                }
            }

        }

        internal static void SetTokuisaki(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Tokuisaki2DataTable dt = ClassKensaku.GetTokui(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].TokuisakiRyakusyo, dt[i].CustomerCode + "/" + dt[i].TokuisakiCode.ToString()));
                }
            }
        }

        internal static void SetCategory2(object sender, string s)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (s != "")
            {
                DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetProduct1(s, Global.SqlConn);

                for (int i = 0; i < dt.Count; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Categoryname, dt[i].Categoryname));
                }
            }
        }

        internal static void SetProductName(object sender, RadComboBoxItemsRequestedEventArgs e, string cate)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.Getproduct5(e.Text.Trim(), cate, Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinMei + " / " + dt[i].Media + "/" + dt[i].Hanni, dt[i].SyouhinMei));
                }
            }

        }

        internal static void SerchTanto(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataDrop.M_TantoDataTable dt = ClassDrop.SerchTanto(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].UserName, dt[i].UserID));
                }
            }
        }

        internal static void SetShisetu(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(""));

            DataDrop.M_Facility_NewDataTable dt =
                ClassDrop.TyokusoDrop(Global.GetConnection());

            for (int i = 0; i < 15; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].FacilityName1, dt[i].FacilityNo.ToString()));
            }
        }

        internal static void SetTokuisaki(object sender, RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_TokuisakiDataTable dt =
                ClassMitumori.TokuisakiMei(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].TokuisakiMei, dt[i].TokuisakiCode.ToString()));
            }

        }

        internal static void SetTyokusouSaki(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_TyokusosakiDataTable dt = ClassKensaku.GetTyokuso(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].TyokusousakiMei1, dt[i].TyokusousakiMei1));
                }
            }
        }

        internal static void GetMaker(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Shiire_NewDataTable dt = ClassKensaku.GetMaker(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].ShiireCode.ToString()));
                }
            }
        }

        internal static void SetCity2(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataMaster.M_CityDataTable dt = ClassMaster.GetCity2(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].CityName, dt[i].CityCode.ToString()));
                }
            }
        }

        internal static void SetTanto4(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
            if (e.Text.Trim() != "")
            {
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetStaff(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].UserName, dt[i].UserID.ToString()));
                }
            }
        }

        //internal static void productcode(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    RadComboBox rcb = (RadComboBox)sender;
        //    rcb.Items.Clear();
        //    rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
        //    if (e.Text.Trim() != "")
        //    {
        //        DataSet1.M_Kakaku_New1DataTable dt = ClassKensaku.GetKakakuNew1(e.Text.Trim(), Global.SqlConn);

        //        int itemOffset = e.NumberOfItems;
        //        int endOffset = dt.Rows.Count;
        //        e.EndOfItems = endOffset == dt.Rows.Count;
        //        for (int i = itemOffset; i < endOffset; i++)
        //        {
        //            rcb.Items.Add(new RadComboBoxItem(dt[i].SyouhinCode, dt[i].SyouhinCode));
        //        }
        //    }
        //}


        //internal static void SetTyokusouSaki(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    RadComboBox rcb = (RadComboBox)sender;
        //    rcb.Items.Clear();
        //    rcb.Items.Add(new RadComboBoxItem(rcb.EmptyMessage, "-1"));
        //    if (e.Text.Trim() != "")
        //    {
        //        DataSet1.M_TyokusosakiDataTable dt = ClassKensaku.GetTyokuso(e.Text.Trim(), Global.SqlConn);

        //        int itemOffset = e.NumberOfItems;
        //        int endOffset = dt.Rows.Count;
        //        e.EndOfItems = endOffset == dt.Rows.Count;
        //        for (int i = itemOffset; i < endOffset; i++)
        //        {
        //            rcb.Items.Add(new RadComboBoxItem(dt[i].TyokusousakiMei1, dt[i].TyokusousakiMei1));
        //        }
        //    }
        //}

        internal static void SetShiireSaki(object sender, RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Shiire_NewDataTable dt =
                ClassMitumori.GetShiireDT(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].ShiireName, dt[i].ShiireName));
            }

        }

        internal static void SetShiireSaki(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Shiire_NewDataTable dt =
                ClassMitumori.GetShiireDT(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].ShiireName, dt[i].ShiireName));
            }
        }

        internal static void SetShiireSaki(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_Shiire_NewDataTable dt =
                ClassMitumori.GetShiireDT(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].ShiireName, dt[i].ShiireName));
            }
        }





        internal static void SetHanni(object sender, RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_HanniDataTable dt =
                ClassMitumori.GetHanniDT(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].Hanni, dt[i].Hanni));
            }
        }


        internal static void SetHanni(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_HanniDataTable dt =
                ClassMitumori.GetHanniDT(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].Hanni, dt[i].Hanni));
            }
        }

        internal static void SetHanni(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_HanniDataTable dt =
                ClassMitumori.GetHanniDT(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].Hanni, dt[i].HanniCode.ToString()));
            }
        }

        internal static void SetCategory(object sender, RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_CategoryDataTable dt =
                ClassMitumori.GetCategoryDT(Global.GetConnection());

            for (int i = 0; i < dt.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CategoryName, dt[i].CategoryName));
            }
        }


        internal static void SetCategory(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_CategoryDataTable dt =
                ClassMitumori.GetCategoryDT(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CategoryName, dt[i].Category.ToString()));
            }
        }

        internal static void SetCategory(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_CategoryDataTable dt =
                ClassMitumori.GetCategoryDT(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].CategoryName, dt[i].Category.ToString()));
            }
        }




        //internal static void SetTyokusouSaki(object sender, RadComboBoxItemsRequestedEventArgs e)

        //{
        //    RadComboBox rad = (RadComboBox)sender;
        //    rad.Items.Clear();
        //    rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

        //    DataSet1.M_TyokusosakiDataTable dt =
        //        ClassKensaku.GetTyokuso(p, Global.GetConnection());

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        rad.Items.Add(new RadComboBoxItem(dt[i].TyokusousakiMei1, dt[i].TyokusousakiMei1));
        //    }
        //}

        internal static void SetTyokusouSaki(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            DataSet1.M_TyokusosakiDataTable dt =
                ClassMitumori.GetTyokusou(Global.GetConnection());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rad.Items.Add(new RadComboBoxItem(dt[i].TyokusousakiMei1, dt[i].TyokusousakiMei1));
            }
        }

        internal static void GetFacility(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            rcb.Items.Clear();
            if (e.Text.Trim() != "")
            {
                DataSet1.M_Facility_NewDataTable dt = ClassKensaku.GetFacility(e.Text.Trim(), Global.SqlConn);

                int itemOffset = e.NumberOfItems;
                int endOffset = dt.Rows.Count;
                e.EndOfItems = endOffset == dt.Rows.Count;
                for (int i = itemOffset; i < endOffset; i++)
                {
                    rcb.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].FacilityNo.ToString() + "/" + dt[i].Code.ToString()));
                }
            }
        }
        internal static void SetShiireSaki2(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            if (e.Text.Trim() != "")
            {
                DataSet1.M_Shiire_NewDataTable dt =
                    ClassMitumori.GetShiireDT(e.Text.Trim(), Global.GetConnection());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rad.Items.Add(new RadComboBoxItem(dt[i].ShiireName, dt[i].ShiireName));
                }
            }
        }

        internal static void SetStaff(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));

            if (e.Text.Trim() != "")
            {
                DataSet1.M_TantoDataTable dt = Class1.GetStaff2(e.Text.Trim(), Global.GetConnection());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rad.Items.Add(new RadComboBoxItem(dt[i].UserName, dt[i].UserID.ToString()));
                }
            }
        }
    }
}