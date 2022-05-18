using System;
using DLL;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

namespace Gyomu.Closing
{
    public partial class Ledger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TokuisakiKensaku.Visible = false;
                ShiiresakiKensaku.Visible = false;
                SyouhinKensaku.Visible = false;
                BtnCSV.Visible = false;
                BtnPrintOut.Visible = false;
            }
        }

        private void Create()
        {
        }

        protected void RcbTokuiShiire_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetTokuiShiire(sender, e);
            }
        }

        protected void RcbTokuiShiire_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (RcbTokuiShiire.Text != "")
            //{
            //    CreateLedger();
            //}
        }

        private void CreateLedger()
        {
            string strDate = "";
            int Karikata = 0;
            int Zandaka = 0;
            int Kashikata = 0;

            string strTable = RadLedger.Text;
            switch (strTable)
            {
                case "得意先":
                    ClassKensaku.KensakuParam k = SetKensakuParam();
                    //売上データと入金データを取得
                    DataUriage.T_UriageDataTable dtU = ClassKensaku.GetUriageLedger(k, Global.GetConnection());
                    string strTokuisaki = dtU[0].TokuisakiMei;
                    DataSet1.M_Tokuisaki2DataTable dtTokui = Class1.GetTokuisakiName(strTokuisaki, Global.GetConnection());
                    DataLedger.T_Nyukin2DataTable dt = ClassLedger.GetNyukin(dtTokui[0].BankNo, Global.GetConnection());
                    //datanyukin
                    //まずは売上データから取得。その後入金データを取得。それらをまとめて１つにし、日付順に並べ替え、
                    //月が変わるごとに～計を出す。
                    //元帳用のデータの作成を決意20210414
                    if (dtU.Count != 0)
                    {
                        string strMonthU = "";
                        DataUriage.T_UriageDataTable dtUN = new DataUriage.T_UriageDataTable();
                        for (int u = 0; u < dtU.Count; u++)
                        {
                            string[] strUriDate = dtU[u].JutyuBi.ToShortDateString().Split('/');
                            if (strDate.Contains(strUriDate[0] + "/" + strUriDate[1]))
                            {
                                Karikata += dtU[u].JutyuGokei;
                                DataUriage.T_UriageRow drU = dtUN.NewT_UriageRow();
                                drU.ItemArray = dtU[u].ItemArray;
                                dtUN.AddT_UriageRow(drU);
                            }
                            else
                            {
                                if (strDate.Length >= 1)
                                {
                                    DataUriage.T_UriageRow drU = dtUN.NewT_UriageRow();
                                    drU.UriageNo = 0;
                                    drU.RowNo1 = u;
                                    drU.SyouhinMei = strMonthU[1] + "月" + " " + "計";
                                    drU.JutyuGokei = Karikata;
                                    int Keisan = Calculate(Karikata, Zandaka, Kashikata, strTable);
                                    drU.HattyuGokei = Keisan;
                                    Zandaka = Keisan;
                                    drU.UriageFlg = true;
                                    dtUN.AddT_UriageRow(drU);
                                    DataUriage.T_UriageRow drUn = dtUN.NewT_UriageRow();
                                    drUn.ItemArray = dtU[u].ItemArray;
                                    dtUN.AddT_UriageRow(drUn);
                                    strDate = "";
                                }
                                else
                                {
                                    Karikata += dtU[u].JutyuGokei;
                                    strDate += strUriDate[0] + "/" + strUriDate[1];
                                    strMonthU += strUriDate[1] + ",";
                                    DataUriage.T_UriageRow drU = dtUN.NewT_UriageRow();
                                    drU.ItemArray = dtU[u].ItemArray;
                                    dtUN.AddT_UriageRow(drU);
                                }
                            }
                        }
                        DGLedger.DataSource = dtUN;
                        DGLedger.DataBind();
                    }
                    break;
                case "仕入先":
                    ClassKensaku.KensakuParam s = SetKensakuParam();
                    DataSet1.T_OrderedDataTable dtO = ClassKensaku.GetOrderedLedger(s, Global.GetConnection());

                    string strMonth = "";
                    DataSet1.T_OrderedDataTable dtON = new DataSet1.T_OrderedDataTable();
                    for (int u = 0; u < dtO.Count; u++)
                    {
                        string[] strUriDate = dtO[u].HatyuDay.Split('/');
                        if (strDate.Contains(strUriDate[0] + "/" + strUriDate[1]))
                        {
                            Karikata += dtO[u].JutyuGokei;
                            DataSet1.T_OrderedRow drOR = dtON.NewT_OrderedRow();
                            drOR.ItemArray = dtO[u].ItemArray;
                            dtON.AddT_OrderedRow(drOR);
                        }
                        else
                        {
                            if (strDate.Length >= 1)
                            {
                                DataSet1.T_OrderedRow drOR = dtON.NewT_OrderedRow();
                                drOR.OrderedNo = 0;
                                drOR.RowNo = u;
                                drOR.ProductName = strUriDate[1] + "月" + " " + "計";
                                drOR.OrderedPrice = Karikata;
                                drOR.UriageFlg = true;
                                int Keisan = Calculate(Karikata, Zandaka, Kashikata, strTable);
                                drOR.JutyuTanka = Keisan;
                                Zandaka = Keisan;
                                dtON.AddT_OrderedRow(drOR);
                                DataSet1.T_OrderedRow drUn = dtON.NewT_OrderedRow();
                                drUn.ItemArray = dtO[u].ItemArray;
                                dtON.AddT_OrderedRow(drUn);
                                strDate = "";
                            }
                            else
                            {
                                Karikata += dtO[u].JutyuGokei;
                                strDate += strUriDate[0] + "/" + strUriDate[1];
                                strMonth += strUriDate[1] + ",";
                                DataSet1.T_OrderedRow drU = dtON.NewT_OrderedRow();
                                drU.ItemArray = dtO[u].ItemArray;
                                dtON.AddT_OrderedRow(drU);
                            }
                        }
                    }
                    DGLedger.DataSource = dtON;
                    DGLedger.DataBind();

                    break;
                case "商品":
                    break;
            }
        }

        private int Calculate(int karikata, int zandaka, int kashikata, string strTable)
        {
            int NewZandaka = 0;
            switch (strTable)
            {
                case "得意先":
                    NewZandaka = zandaka + karikata - kashikata;
                    break;
                case "仕入先":
                    NewZandaka = kashikata + karikata - zandaka;
                    break;
            }
            return NewZandaka;
        }

        //private void RcbMonth_ItemsRequested(string v, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    RadComboBox rcb = new RadComboBox();
        //    string[] item = v.Split(',');
        //    for(int i = 0; i < item.Length; i++ )
        //    {
        //        rcb.Items.Add(new RadComboBoxItem(item[i], item[i]));
        //    }

        //}

        protected void DGLedger_ItemCommand(object source, DataGridCommandEventArgs e)
        {


        }

        protected void DGLedger_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataUriage.T_UriageRow drU = (e.Item.DataItem as DataRowView).Row as DataUriage.T_UriageRow;
                DataSet1.T_OrderedRow drO = (e.Item.DataItem as DataRowView).Row as DataSet1.T_OrderedRow;

                Label LblYear = e.Item.FindControl("LblYear") as Label;
                Label LblMonth = e.Item.FindControl("LblMonth") as Label;
                Label LblDay = e.Item.FindControl("LblDay") as Label;
                Label LblNo = e.Item.FindControl("LblNo") as Label;
                Label LblFacility = e.Item.FindControl("LblFacility") as Label;
                Label LblProduct = e.Item.FindControl("LblProduct") as Label;
                Label LblKarikata = e.Item.FindControl("LblKarikata") as Label;
                Label LblKashikata = e.Item.FindControl("LblKashikata") as Label;
                Label LblZandaka = e.Item.FindControl("LblZandaka") as Label;
                Label LblGenre = DGLedger.FindControl("LblGenre") as Label;
                if (drU != null)
                {
                    if (drU.UriageNo == 0)
                    {
                        LblProduct.Font.Bold = true;
                        LblKarikata.Font.Bold = true;
                        LblKashikata.Font.Bold = true;
                        LblZandaka.Font.Bold = true;
                        e.Item.BorderStyle = BorderStyle.Outset;
                        LblProduct.Text = drU.SyouhinMei;
                        LblKarikata.Text = drU.JutyuGokei.ToString("0,0");
                        LblZandaka.Text = drU.HattyuGokei.ToString("0,0");
                    }
                    else
                    {
                        //売上元帳
                        if (!drU.IsJutyuBiNull())
                        {
                            string[] AryDay = drU.JutyuBi.ToShortDateString().Split('/');
                            LblYear.Text = AryDay[0];
                            LblMonth.Text = AryDay[1];
                            LblDay.Text = AryDay[2];
                        }

                        LblNo.Text = drU.UriageNo.ToString();
                        if (!drU.IsSisetuMeiNull())
                        {
                            LblFacility.Text = drU.SisetuMei;
                        }
                        if (!drU.IsSyouhinMeiNull())
                        {
                            LblProduct.Text = drU.SyouhinMei;
                        }
                        if (!drU.IsJutyuGokeiNull())
                        {
                            LblKarikata.Text = drU.JutyuGokei.ToString("0,0");
                        }

                    }
                }
                else
                {
                    //仕入先元帳
                }
            }
            //RadComboBox rcbMonth = e.Item.FindControl("RcbMonth") as RadComboBox;
        }


        protected void RcbFacility_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.GetFacility(sender, e);
            }
        }

        protected void RcbCity_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetCity(sender, e);
            }
        }

        protected void RcbStaff_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetTanto3(sender, e);
        }


        protected void RcbBumon_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetBumon2(sender, e);
        }

        protected void BtnCSV_Click(object sender, EventArgs e)
        {

        }

        protected void BtnPrintOut_Click(object sender, EventArgs e)
        {

        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            CreateLedger();
        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();


            if (RcbTokuiShiire.SelectedValue != "")
            {
                k.TokuisakiC = RcbTokuiShiire.SelectedValue;
            }

            if (RcbCity.SelectedValue != "")
            {
                k.sCity = RcbCity.SelectedValue;
            }

            if (RcbBumon.SelectedValue != "")
            {
                k.sBumon = RcbBumon.SelectedItem.Text;
            }

            if (RcbStaff.SelectedValue != "")
            {
                k.sTanto = RcbStaff.SelectedItem.Text;
            }

            if (RcbFacility.SelectedValue != "")
            {
                k.FacilityCode = RcbFacility.SelectedValue;
            }

            string strTable = RadLedger.Text;
            if (strTable == "得意先")
            {
                Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlKikan") as Common.CtlNengappiForm;
                if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
                {
                    k.JucyuBi = this.CtlKikan.GetNengappiKikan();
                }
            }
            else if (strTable == "仕入先")
            {
                Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlKikan2") as Common.CtlNengappiForm;
                if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
                {
                    k.JucyuBi = this.CtlKikan2.GetNengappiKikan();
                }
            }
            else if (strTable == "商品")
            {
                Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlKikan3") as Common.CtlNengappiForm;
                if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
                {
                    k.JucyuBi = this.CtlKikan3.GetNengappiKikan();
                }
            }

            return k;
        }

        protected void RcbShiiresaki_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void RcbMaker_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void RadLedger_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string text = RadLedger.Text;
            switch (text)
            {
                case "得意先":
                    TokuisakiKensaku.Visible = true;
                    ShiiresakiKensaku.Visible = false;
                    SyouhinKensaku.Visible = false;
                    BtnCSV.Visible = true;
                    BtnPrintOut.Visible = true;
                    break;
                case "仕入先":
                    TokuisakiKensaku.Visible = false;
                    ShiiresakiKensaku.Visible = true;
                    SyouhinKensaku.Visible = false;
                    BtnCSV.Visible = true;
                    BtnPrintOut.Visible = true;
                    break;
                case "商品":
                    TokuisakiKensaku.Visible = false;
                    ShiiresakiKensaku.Visible = false;
                    SyouhinKensaku.Visible = true;
                    BtnCSV.Visible = true;
                    BtnPrintOut.Visible = true;
                    break;
            }
        }
    }

    internal class Keisan
    {
        public Keisan(int karikata, int zandaka, int kashikata)
        {
        }
    }
}