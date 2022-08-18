using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace Gyomu.Order
{
    public partial class OrderedInput : System.Web.UI.Page
    {
        public static string strOrderedDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (SessionManager.ORDERED_DATA == null)
                    {
                        Create();
                    }
                    else
                    {
                        string session = SessionManager.ORDERED_DATA;

                        if (session != "")
                        {
                            Create2();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Err.Text = ex.Message;
                    Create();
                }
            }
        }

        private void Create2()
        {
            DataSet1.T_KariOrderedDataTable dt = new DataSet1.T_KariOrderedDataTable();

            string strOrderedNo = "";
            int CountRow = 0;


            string session = SessionManager.ORDERED_DATA;
            string[] SpRow = session.Split('_');
            for (int i = 0; i < SpRow.Length; i++)
            {
                string[] SpKey = SpRow[i].Split(',');
                strOrderedNo = SpKey[0];
                CountRow += 1;
                DataSet1.T_OrderedDataTable dd = ClassOrdered.GetOrdered2(strOrderedNo, Global.GetConnection());
                for (int u = 0; u < dd.Count; u++)
                {
                    DataSet1.T_KariOrderedRow dl = dt.NewT_KariOrderedRow();
                    dl.OrderedNo = dd[u].OrderedNo;
                    if (!dd[u].IsTokuisakiCodeNull())
                    {
                        dl.TokuisakiCode = dd[u].TokuisakiCode;
                    }
                    dl.TokuisakiMei = dd[u].TokuisakiMei;
                    dl.SeikyusakiMei = dd[u].SeikyusakiMei;
                    dl.Category = dd[u].Category;
                    dl.CategoryName = dd[u].CategoryName;
                    dl.FacilityCode = dd[u].FacilityCode;
                    dl.FacilityName = dd[u].FacilityName;
                    if (!dd[u].IsFacilityJusyo1Null())
                    {
                        dl.FacilityJusyo1 = dd[u].FacilityJusyo1;
                    }
                    if (!dd[u].IsSiyouKaishiNull())
                    { dl.SiyouKaishi = dd[u].SiyouKaishi; }
                    if (!dd[u].IsSiyouiOwariNull())
                    { dl.SiyouiOwari = dd[u].SiyouiOwari; }
                    dl.HyoujyunKakaku = dd[u].HyoujyunKakaku;
                    dl.StaffName = dd[u].StaffName;
                    dl.Department = dd[u].Department;
                    dl.Range = dd[u].Range;
                    dl.ProductCode = dd[u].ProductCode;
                    dl.ProductName = dd[u].ProductName;
                    dl.MekerNo = dd[u].MekerNo;
                    dl.Media = dd[u].Media;
                    dl.HatyuDay = dd[u].HatyuDay;
                    dl.JutyuSuryou = dd[u].JutyuSuryou;
                    dl.JutyuGokei = dd[u].JutyuGokei;
                    dl.ShiireTanka = dd[u].ShiireTanka;
                    dl.ShiireKingaku = dd[u].ShiireKingaku;
                    dl.WareHouse = dd[u].WareHouse;
                    dl.ShiireSakiName = dd[u].ShiireSakiName;
                    dl.ShiiresakiCode = dd[u].ShiiresakiCode;
                    dl.Zeikubun = dd[u].Zeikubun;
                    dl.Kakeritsu = dd[u].Kakeritsu;
                    dl.Zansu = dd[u].Zansu;
                    if (u >= 1)
                    {
                        CountRow += 1;
                    }
                    dl.RowNo = CountRow;
                    dt.AddT_KariOrderedRow(dl);
                }
            }
            CtrlSyousai.DataSource = dt;
            CtrlSyousai.DataBind();
        }

        private void Create()
        {
            DataSet1.T_KariOrderedDataTable dt = new DataSet1.T_KariOrderedDataTable();
            for (int i = 0; i < 1; i++)
            {
                DataSet1.T_KariOrderedRow dr = dt.NewT_KariOrderedRow();
                dr.OrderedNo = 1;
                dr.RowNo = 1;
                dt.AddT_KariOrderedRow(dr);
            }
            CtrlSyousai.DataSource = dt;
            CtrlSyousai.DataBind();
            strOrderedDate = LblOrderedDate.Text;
        }

        internal static void Create2(string[] strOrderedNoAry, string[] strShiireAry, string[] strMakerNoAry, string[] strHanniAry, string[] strCategoryAry)
        {
            //    DataSet1.T_KariOrderedDataTable dt = new DataSet1.T_KariOrderedDataTable();
            //    DataSet1.T_KariOrderedRow dl = dt.NewT_KariOrderedRow();
            //    for (int i = 0; i < strOrderedNoAry.Length; i++)
            //    {
            //        DataSet1.T_OrderedDataTable dt = ClassOrdered.GetSyuseiOrdered(strOrderedNoAry[i], strShiireAry[i], strCategoryAry[i], Global.GetConnection());
            //        for (int u = 0; u < dt.Count; u++)
            //        {
            //            dl.OrderedNo = dr.OrderedNo;
            //            dl.TokuisakiCode = dr.TokuisakiCode;
            //            dl.TokuisakiMei = dr.TokuisakiMei;
            //            dl.SeikyusakiMei = dr.SeikyusakiMei;
            //            dl.Category = dr.Category;
            //            dl.CategoryName = dl.CategoryName;
            //            dl.FacilityCode = dr.FacilityCode;
            //            dl.FacilityName = dr.FacilityName;
            //            dl.FacilityJusyo1 = dr.FacilityJusyo1;
            //            dl.SiyouKaishi = dr.SiyouKaishi;
            //            dl.SiyouiOwari = dr.SiyouiOwari;
            //            dl.HyoujyunKakaku = dr.HyoujyunKakaku;
            //            dl.StaffName = dr.StaffName;
            //            dl.Department = dr.Department;
            //            dl.Range = dr.Range;
            //            dl.ProductCode = dr.ProductCode;
            //            dl.ProductName = dr.ProductName;
            //            dl.MekerNo = dr.MekerNo;
            //            dl.Media = dr.Media;
            //            dl.HatyuDay = dr.HatyuDay;
            //            //dl.JutyuTanka = dr.JutyuTanka;
            //            dl.JutyuSuryou = dr.JutyuSuryou;
            //            dl.JutyuGokei = dr.JutyuGokei;
            //            dl.ShiireTanka = dr.ShiireTanka;
            //            dl.ShiireKingaku = dr.ShiireKingaku;
            //            dl.WareHouse = dr.WareHouse;
            //            dl.ShiireSakiName = dr.ShiireSakiName;
            //            dl.ShiiresakiCode = dr.ShiiresakiCode;
            //            dl.Zeikubun = dr.Zeikubun;
            //            dl.Kakeritsu = dr.Kakeritsu;
            //            //dl.Ryoukin = dr.Ryoukin;
            //            dt.AddT_KariOrderedRow(dl);
            //        }
            //    }
        }

        protected void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet1.T_KariOrderedRow dr = (e.Item.DataItem as DataRowView).Row as DataSet1.T_KariOrderedRow;

                CtlOrderedMeisai2 Ctl = e.Item.FindControl("Syosai") as CtlOrderedMeisai2;
                ///定義集//////////////////////////////////////
                Label RowNo = (Label)Ctl.FindControl("RowNo");
                TextBox TbxMaker = (TextBox)Ctl.FindControl("TbxMaker");
                Label LblProductName = (Label)Ctl.FindControl("LblProductName");
                Label LblSuryo = (Label)Ctl.FindControl("LblSuryo");
                Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                Label LblMedia = (Label)Ctl.FindControl("LblMedia");
                TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                TextBox TbxShiireKibgaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                Label LblFacility = (Label)Ctl.FindControl("LblFacility");
                Label LblSekisu = (Label)Ctl.FindControl("LblSekisu");
                Label LblSiyouKaishi = (Label)Ctl.FindControl("LblSiyouKaishi");
                Label LblSiyouOwari = (Label)Ctl.FindControl("LblSiyouOwari");
                Label LblZansu = (Label)Ctl.FindControl("LblZansu");
                Label Err = (Label)Ctl.FindControl("Err");
                DropDownList DdlWareHouse = (DropDownList)Ctl.FindControl("DdlWarehouse");
                HtmlInputHidden OrderedNo = (HtmlInputHidden)Ctl.FindControl("OrderedNo");
                HtmlInputHidden HidTokuisakiCode = (HtmlInputHidden)Ctl.FindControl("HidTokuisakiCode");
                HtmlInputHidden HidTokuisakiName = (HtmlInputHidden)Ctl.FindControl("HidTokuisakiName");
                ///定義集//////////////////////////////////////////////////

                RowNo.Text = dr.RowNo.ToString();
                OrderedNo.Value = dr.OrderedNo.ToString();
                string no = dr.OrderedNo.ToString();
                if (no != "1")
                {
                    DataSet1.T_OrderedHeaderRow drH = ClassOrdered.GetOrderedH(no, Global.GetConnection());
                    LblShiiresaki.Text = drH.ShiiresakiName.Trim();
                    LblCategory.Text = drH.CategoryName;
                    LblOrdered.Text = drH.OrderedNo.ToString();
                    LblSu.Text = drH.OrderedAmount.ToString();
                    LblShiirekei.Text = drH.ShiireKingaku.ToString("0,0");
                    LblOrderedDate.Text = drH.CreateDate.ToString();
                    if (!dr.IsMekerNoNull())
                    { TbxMaker.Text = dr.MekerNo; }
                    if (!dr.IsProductNameNull())
                    { LblProductName.Text = dr.ProductName; }
                    if (!dr.IsJutyuSuryouNull())
                    { LblSuryo.Text = dr.JutyuSuryou.ToString(); }
                    if (!dr.IsRangeNull())
                    { LblHanni.Text = dr.Range; }
                    if (!dr.IsMediaNull())
                    { LblMedia.Text = dr.Media; }
                    if (!dr.IsShiireTankaNull())
                    { TbxShiireTanka.Text = dr.ShiireTanka.ToString("0,0"); }
                    if (!dr.IsShiireKingakuNull())
                    { TbxShiireKibgaku.Text = dr.ShiireKingaku.ToString("0,0"); }
                    if (!dr.IsFacilityNameNull())
                    { LblFacility.Text = dr.FacilityName; }
                    if (!dr.IsZasuNull())
                    { LblSekisu.Text = dr.Zasu; }
                    if (!dr.IsSiyouKaishiNull())
                    { LblSiyouKaishi.Text = dr.SiyouKaishi; }
                    if (!dr.IsSiyouiOwariNull())
                    { LblSiyouOwari.Text = dr.SiyouiOwari; }
                    if (!dr.IsWareHouseNull())
                    { DdlWareHouse.SelectedItem.Text = dr.WareHouse; }
                    if (!dr.IsTokuisakiCodeNull())
                    { HidTokuisakiCode.Value = dr.TokuisakiCode.ToString(); }
                    if (!dr.IsTokuisakiMeiNull())
                    { HidTokuisakiName.Value = dr.TokuisakiMei; }

                }
                else
                {

                }
            }
        }

        private void Log(int row)
        {
            CtlOrderedMeisai Ctl = CtrlSyousai.Items[row].FindControl("Syosai") as CtlOrderedMeisai;
            HtmlInputHidden No = (HtmlInputHidden)Ctl.FindControl("OrderedNo");
            Label LblProductName = (Label)Ctl.FindControl("LblProductName");
            Label LblHanni = (Label)Ctl.FindControl("LblHanni");
            Label LblMedia = (Label)Ctl.FindControl("LblMedia");
            TextBox Maker = (TextBox)Ctl.FindControl("TbxMaker");

            DataMaster.M_HanniRow dl = ClassMaster.GetHanniCode(LblHanni.Text.Trim(), Global.GetConnection());
            string hCode = dl.HanniCode.ToString();

            string strKey = No.Value + "," + Maker.Text.Trim() + "," + hCode.Trim() + "," + LblMedia.Text.Trim();

            Response.Write("<script>");
            Response.Write("window.open('OrderedInput2.aspx?id=" + strKey + "' , 'テスト', 'width=600, height=600, toolbar=1, menubar=1, scrollbars=1')");
            Response.Write("</script>");
        }

        private void Update(int row)
        {
            try
            {
                CtlOrderedMeisai Ctl = CtrlSyousai.Items[row].FindControl("Syosai") as CtlOrderedMeisai;
                ///定義集/////////////////////////////////////////////////////////////////////////////////////
                Label RowNo = (Label)Ctl.FindControl("RowNo");
                TextBox TbxMaker = (TextBox)Ctl.FindControl("TbxMaker");
                Label LblProductName = (Label)Ctl.FindControl("LblProductName");
                Label LblSuryo = (Label)Ctl.FindControl("LblSuryo");
                Label LblHanni = (Label)Ctl.FindControl("LblHanni");
                Label LblMedia = (Label)Ctl.FindControl("LblMedia");
                TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                TextBox TbxShiireKibgaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                Label LblFacility = (Label)Ctl.FindControl("LblFacility");
                Label LblSekisu = (Label)Ctl.FindControl("LblSekisu");
                Label LblSiyouKaishi = (Label)Ctl.FindControl("LblSiyouKaishi");
                Label LblSiyouOwari = (Label)Ctl.FindControl("LblSiyouOwari");
                Label LblWareHouse = (Label)Ctl.FindControl("LblWareHouse");
                Label Err = (Label)Ctl.FindControl("Err");
                HtmlInputHidden OrderedNo = (HtmlInputHidden)Ctl.FindControl("OrderedNo");
                Label Nokori = CtrlSyousai.Items[row].FindControl("LblNokori") as Label;
                TextBox Nyuka = CtrlSyousai.Items[row].FindControl("TbxNyukasu") as TextBox;
                Button Log = CtrlSyousai.Items[row].FindControl("BtnLogList") as Button;
                Button UpdateCheck = CtrlSyousai.Items[row].FindControl("BtnUpdate") as Button;
                ///定義集///////////////////////////////////////////////////////////////////////////////

                Err.Text = "";
                int nyu = int.Parse(Nyuka.Text);
                int zan = int.Parse(Nokori.Text);
                int noko = zan - nyu;
                if (noko >= 0)
                {
                    DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
                    DataSet1.T_OrderedRow dr = dt.NewT_OrderedRow();

                    DataSet1.T_NyukaLogDataTable dd = new DataSet1.T_NyukaLogDataTable();
                    DataSet1.T_NyukaLogRow dp = dd.NewT_NyukaLogRow();
                    string strMaker = TbxMaker.Text.Trim();
                    string strProduct = LblProductName.Text.Trim();
                    string strHanni = LblHanni.Text.Trim();
                    string strMedia = LblMedia.Text.Trim();
                    string no = OrderedNo.Value;
                    DataSet1.T_OrderedRow dl = ClassOrdered.GetUpdateordered(no, strMaker, strProduct, strHanni, strMedia, Global.GetConnection());
                    ClassOrdered.GetUpdateordered2(dl, no, strMaker, strProduct, strHanni, strMedia, noko, Global.GetConnection());

                    dp.OrderedNo = dl.OrderedNo.ToString();
                    dp.NyukaDate = DateTime.Now;
                    dp.OrderedSuryo = dl.JutyuSuryou.ToString();
                    dp.NyukaSuryo = Nyuka.Text;
                    dp.StaffName = SessionManager.User.UserID;
                    dp.MakerNo = TbxMaker.Text;
                    dp.ProductName = strProduct;
                    dp.Hanni = strHanni;
                    dp.Media = strMedia;
                    dp.Zansu = noko.ToString();
                    dp.HatyuDate = DateTime.Parse(dl.HatyuDay);

                    dd.AddT_NyukaLogRow(dp);
                    ClassOrdered.InsertLog(dd, Global.GetConnection());

                    Nokori.Text = noko.ToString();
                    Nyuka.Text = "";
                }
                else
                {
                    Err.Text = "入荷個数が残数を超えています";
                }
            }
            catch (Exception ex)
            {
                Err.Text = ex.Message;
            }
        }

        protected void CtrlSyousai_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int row = e.Item.ItemIndex;

            string cmd = e.CommandName;

            if (cmd == "Update")
            { Update(row); }
            if (cmd == "Log")
            { Log(row); }
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string No = "";
                DataAppropriate.T_AppropriateHeaderDataTable dtH = new DataAppropriate.T_AppropriateHeaderDataTable();
                DataAppropriate.T_AppropriateHeaderRow drH = dtH.NewT_AppropriateHeaderRow();
                string shiire = LblShiiresaki.Text;
                string cate = LblCategory.Text;
                string Ono = LblOrdered.Text;

                DataAppropriate.T_AppropriateHeaderDataTable ddt = Class1.GetHeader(shiire, cate, Global.GetConnection());
                if (ddt.Count != 0)
                {
                    drH.ItemArray = ddt[0].ItemArray;
                    No = ddt[0].ShiireNo.ToString();
                    Class1.UpdateAppropriateHeader(drH, LblSu.Text, Global.GetConnection());
                }
                else
                {
                    DataAppropriate.T_AppropriateHeaderDataTable drM = Class1.GetMaxShiireNo(Global.GetConnection());
                    int no = drM.Count;
                    No = drM.Count.ToString();
                    drH.ShiireNo = no + 1;
                    DataSet1.T_OrderedHeaderRow dtOH = ClassOrdered.GetOrderedH(LblOrdered.Text, Global.GetConnection());
                    string[] strTokuisaki = dtOH.TokuisakiCode.Split('/');

                    drH.ItemArray = dtOH.ItemArray;
                    drH.ShiiresakiName = LblShiiresaki.Text;
                    drH.ShiireAmount = int.Parse(LblSu.Text);
                    drH.CreateDate = DateTime.Now;
                    dtH.AddT_AppropriateHeaderRow(drH);
                    Class1.InsertAppropriateHeader(dtH, Global.GetConnection());
                }

                DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();

                DataSet1.T_OrderedDataTable dtO = ClassOrdered.GetOrdered2(Ono, Global.GetConnection());
                for (int j = 0; j < dtO.Count; j++)
                {
                    DataAppropriate.T_AppropriateRow dl = dt.NewT_AppropriateRow();

                    dl.ItemArray = dtO[j].ItemArray;
                    dl.ShiireNo = int.Parse(No);
                    dl.HatyuFLG = "true";
                    dl.HatyuDay = DateTime.Now.ToShortDateString();
                    dt.AddT_AppropriateRow(dl);
                }
                DataAppropriate.T_AppropriateRow dr = dt.NewT_AppropriateRow();
                Class1.InsertAppropriate(dt, Global.GetConnection());
                End.Text = "受注No." + LblOrdered.Text + "を仕入No." + No + "に登録しました。";
            }
            catch (Exception ex)
            {
                ClassMail.ErrorMail("maeda@m2m-asp.com", "エラーメール | 発注明細→仕入登録処理", ex.Message + "<br><br>" + ex.Source);
                Err.Text = ex.Message;
            }
        }

        protected void BtnKeisan_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtlOrderedMeisai2 ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtlOrderedMeisai2;
                TextBox tst = (TextBox)ctl.FindControl("TbxShiireTanka");
                TextBox tsk = (TextBox)ctl.FindControl("TbxShiireKingaku");
                Label su = (Label)ctl.FindControl("LblSuryo");

                string clientID = tst.ClientID + "-" + tsk.ClientID + "-" + su.ClientID;
                tst.Attributes["OnBlur"] = string.Format("Keisan('{0}');", clientID);
            }
        }
    }
}
