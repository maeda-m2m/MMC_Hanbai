using Core.Sql;
using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;

namespace Gyomu.Appropriate
{
    public partial class AppropriateInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.ORDERED_DATA != "")
                {
                    string session = SessionManager.ORDERED_DATA;

                    if (session != "")
                    {
                        Create2();
                    }
                }
                else
                {
                    Create();
                }
            }
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
        }

        private void Create2()
        {
            DataSet1.T_KariOrderedDataTable dt = new DataSet1.T_KariOrderedDataTable();

            string strOrderedNo = "";

            string session = SessionManager.ORDERED_DATA;
            string[] SpRow = session.Split('_');
            for (int i = 0; i < SpRow.Length; i++)
            {
                string[] SpKey = SpRow[i].Split(',');
                strOrderedNo = SpKey[0];
                //strShiire = SpKey[1];
                //strCategory = SpKey[2];
                //DataSet1.T_OrderedDataTable dd = ClassOrdered.GetSyuseiOrdered
                //    (strOrderedNo, strShiire, strCategory, Global.GetConnection());
                //DataSet1.T_OrderedDataTable dd = ClassOrdered.GetOrdered2(strOrderedNo, Global.GetConnection());
                DataAppropriate.T_AppropriateDataTable dd = Class1.GetAppropriate2(strOrderedNo, Global.GetConnection());
                for (int u = 0; u < dd.Count; u++)
                {
                    DataSet1.T_KariOrderedRow dl = dt.NewT_KariOrderedRow();

                    dl.OrderedNo = dd[u].ShiireNo;
                    dl.RowNo = dd[u].RowNo;
                    dl.FacilityName = dd[u].FacilityName;
                    if (!dd[u].IsSiyouKaishiNull())
                    {
                        dl.SiyouKaishi = dd[u].SiyouKaishi.ToString();
                    }
                    if (!dd[u].IsSiyouiOwariNull())
                    {
                        dl.SiyouiOwari = dd[u].SiyouiOwari.ToString();
                    }
                    dl.ProductName = dd[u].ProductName;
                    dl.MekerNo = dd[u].MekerNo;
                    dl.Media = dd[u].Media;
                    dl.JutyuSuryou = dd[u].JutyuSuryou;
                    dl.ShiireTanka = dd[u].ShiireTanka;
                    dl.ShiireKingaku = dd[u].ShiireKingaku;
                    dl.WareHouse = dd[u].WareHouse;
                    dl.Range = dd[u].Range;
                    if (!dd[u].IsZasuNull())
                    {
                        dl.Zasu = dd[u].Zasu;
                    }
                    dl.Zansu = dd[u].Zansu;

                    dt.AddT_KariOrderedRow(dl);
                }
            }
            CtrlSyousai.DataSource = dt;
            CtrlSyousai.DataBind();
        }

        protected void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet1.T_KariOrderedRow dr = (e.Item.DataItem as DataRowView).Row as DataSet1.T_KariOrderedRow;

                Order.CtlOrderedMeisai Ctl = e.Item.FindControl("Syosai") as Order.CtlOrderedMeisai;
                ///定義集//////////////////////////////////////
                Label RowNo = (Label)Ctl.FindControl("RowNo");
                RadComboBox RcbMaker = (RadComboBox)Ctl.FindControl("RcbMaker");
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
                HtmlInputHidden HidCate = (HtmlInputHidden)Ctl.FindControl("HidCate");
                Label Err = (Label)Ctl.FindControl("Err");
                HtmlInputHidden OrderedNo = (HtmlInputHidden)Ctl.FindControl("OrderedNo");
                Label Nokori = e.Item.FindControl("LblNokori") as Label;
                TextBox Nyuka = e.Item.FindControl("TbxNyukasu") as TextBox;
                Button Log = e.Item.FindControl("BtnLogList") as Button;
                Button UpdateCheck = e.Item.FindControl("BtnUpdate") as Button;
                ///定義集//////////////////////////////////////////////////
                string clientID = TbxShiireTanka.ClientID + "-" + LblSuryo.ClientID + "-" + TbxShiireKibgaku.ClientID;
                TbxShiireTanka.Attributes["OnBlur"] = string.Format("GetData('{0}')", clientID);
                RowNo.Text = dr.RowNo.ToString();
                OrderedNo.Value = SessionManager.ORDERED_DATA;
                string no = SessionManager.ORDERED_DATA;
                DataAppropriate.T_AppropriateHeaderRow drH = Class1.GetAppropriateHeaderRow(no, Global.GetConnection());
                LblShiiresaki.Text = drH.ShiiresakiName.Trim();
                LblCategory.Text = drH.CategoryName;
                HidCate.Value = drH.Category.ToString();
                LblOrdered.Text = drH.ShiireNo.ToString();
                LblSu.Text = drH.ShiireAmount.ToString();
                LblShiirekei.Text = drH.ShiireKingaku.ToString("0,0");
                LblOrderedDate.Text = drH.CreateDate.ToString();
                if (!dr.IsMekerNoNull())
                { LblProductName.Text = dr.MekerNo; }
                if (!dr.IsProductNameNull())
                { RcbMaker.Text = dr.ProductName; }
                if (!dr.IsJutyuSuryouNull())
                {
                    LblSuryo.Text = dr.JutyuSuryou.ToString();
                }
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
                { LblWareHouse.Text = dr.WareHouse; }
                if (!dr.IsZansuNull())
                {
                    Nokori.Text = dr.Zansu;
                    Nyuka.Text = dr.Zansu;
                }
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

        private void Update(int row)
        {
            try
            {
                Order.CtlOrderedMeisai Ctl = CtrlSyousai.Items[row].FindControl("Syosai") as Order.CtlOrderedMeisai;
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
                RadComboBox RcbMaker = (RadComboBox)Ctl.FindControl("RcbMaker");
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
                    //DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();
                    //DataSet1.T_OrderedRow dr = dt.NewT_OrderedRow();

                    DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
                    DataAppropriate.T_AppropriateRow dr = dt.NewT_AppropriateRow();

                    DataSet1.T_NyukaLogDataTable dd = new DataSet1.T_NyukaLogDataTable();
                    DataSet1.T_NyukaLogRow dp = dd.NewT_NyukaLogRow();
                    string strMaker = LblProductName.Text.Trim();
                    string strProduct = RcbMaker.Text.Trim();
                    string strHanni = LblHanni.Text.Trim();
                    string strMedia = LblMedia.Text.Trim();
                    string no = OrderedNo.Value;
                    DataAppropriate.T_AppropriateRow dl = Class1.GetUpdateAppropriate(no, strMaker, strProduct, strHanni, strMedia, Global.GetConnection());
                    //ClassOrdered.GetUpdateordered2(dl, no, strMaker, strProduct, strHanni, strMedia, noko, Global.GetConnection());
                    Class1.UpdateAppropriate(dl, no, strMaker, strProduct, strHanni, strMedia, noko, Global.GetConnection());

                    dp.OrderedNo = dl.ShiireNo.ToString();
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
                    End.Text = "入荷処理を行いました。";
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

        private void Log(int row)
        {
            Order.CtlOrderedMeisai Ctl = CtrlSyousai.Items[row].FindControl("Syosai") as Order.CtlOrderedMeisai;
            HtmlInputHidden No = (HtmlInputHidden)Ctl.FindControl("OrderedNo");
            Label LblProductName = (Label)Ctl.FindControl("LblProductName");
            Label LblHanni = (Label)Ctl.FindControl("LblHanni");
            Label LblMedia = (Label)Ctl.FindControl("LblMedia");
            TextBox Maker = (TextBox)Ctl.FindControl("TbxMaker");

            string hCode = "";

            if (LblHanni.Text.Trim() != "")
            {
                DataMaster.M_HanniRow dl = ClassMaster.GetHanniCode(LblHanni.Text.Trim(), Global.GetConnection());
                hCode = dl.HanniCode.ToString();
            }


            string strKey = No.Value + "," + Maker.Text.Trim() + "," + hCode.Trim() + "," + LblMedia.Text.Trim();

            Response.Write("<script>");
            Response.Write("window.open('OrderedInput2.aspx?id=" + strKey + "' , 'テスト', 'width=600, height=600, toolbar=1, menubar=1, scrollbars=1')");
            Response.Write("</script>");
        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                Update(i);
            }
        }

        protected void BtnAddLine_Click(object sender, EventArgs e)
        {
            int c = 0;
            string aNo = LblOrdered.Text;
            DataAppropriate.T_AppropriateDataTable dt = Class1.GetAppropriate2(aNo, Global.GetConnection());
            DataSet1.T_KariOrderedDataTable kdt = new DataSet1.T_KariOrderedDataTable();
            for (int i = 0; i < dt.Count; i++)
            {
                DataSet1.T_KariOrderedRow dr = kdt.NewT_KariOrderedRow();
                dr.ItemArray = dt[i].ItemArray;
                if (!dt[i].IsFacilityTelNull())
                {
                    dr.FacilityTel = dt[i].FacilityTel.Trim();
                }
                kdt.AddT_KariOrderedRow(dr);
                c++;
            }
            DataSet1.T_KariOrderedRow drn = kdt.NewT_KariOrderedRow();
            drn.OrderedNo = int.Parse(LblOrdered.Text);
            drn.RowNo = c + 1;
            kdt.AddT_KariOrderedRow(drn);
            CtrlSyousai.DataSource = kdt;
            CtrlSyousai.DataBind();
        }
    }
}