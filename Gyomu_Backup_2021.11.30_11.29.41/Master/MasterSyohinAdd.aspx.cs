using DLL;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Master
{
    public partial class MasterSyohinAdd : System.Web.UI.Page
    {
        public static string strBaseSyouhinCode;
        public static string strBaseSyouhinMei;
        public static DataTable dtM_kakaku_2;
        public static DataGridColumn[] DGICArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
            dr.SyouhinCode = "0";
            dr.SyouhinMei = "商品名を入力して下さい";
            dr.CategoryCode = 101;
            dr.Categoryname = "公共図書館";
            dr.Makernumber = "";
            dr.ShiireCode = "";
            dr.Media = "DVD";
            dr.Hanni = "";
            dr.HyoujunKakaku = 0;
            dt.AddM_Kakaku_2Row(dr);
            D.DataSource = dt;
            D.DataBind();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string pc = TbxCode.Text;
            string pn = TbxSyohinMei.Text;
            DataMaster.M_ProductDataTable dt = ClassMaster.GetProduct2(pc, Global.GetConnection());
            if (dt.Count >= 1)
            {
                LblStatus.Text = "この商品コードは既に登録されています。";
                LblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }
            DataMaster.M_ProductDataTable dd = ClassMaster.GetProduct3(pn, Global.GetConnection());
            if (dd.Count >= 1)
            {
                LblStatus.Text = "この商品名は既に登録されています。";
                LblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                DataSet1.M_Kakaku_2DataTable dtN = new DataSet1.M_Kakaku_2DataTable();
                string strSyouhinCode = TbxCode.Text;
                strBaseSyouhinCode = TbxCode.Text;
                string strSyouhinMei = TbxSyohinMei.Text;
                strBaseSyouhinMei = TbxSyohinMei.Text;
                for (int i = 0; i < D.Items.Count; i++)
                {
                    DataSet1.M_Kakaku_2Row dr = dtN.NewM_Kakaku_2Row();
                    RadComboBox Maker = D.Items[i].FindControl("RadMakerRyaku") as RadComboBox;
                    TextBox TbxHanni = D.Items[i].FindControl("TbxHanni") as TextBox;
                    RadDatePicker RdpPermissionStart = D.Items[i].FindControl("RdpPermissionStart") as RadDatePicker;
                    RadDatePicker RdpRightEnd = D.Items[i].FindControl("RdpRightEnd") as RadDatePicker;
                    RadComboBox RcbCategory = D.Items[i].FindControl("RcbCategory") as RadComboBox;
                    CheckBox ChkJacket = D.Items[i].FindControl("ChkJacket") as CheckBox;
                    CheckBox ChkKyodakusyo = D.Items[i].FindControl("ChkKyodakusyo") as CheckBox;
                    CheckBox ChkReturn = D.Items[i].FindControl("ChkReturn") as CheckBox;
                    CheckBox ChkHoukokusyo = D.Items[i].FindControl("ChkHoukokusyo") as CheckBox;
                    RadComboBox RcbMedia = D.Items[i].FindControl("RcbMedia") as RadComboBox;
                    TextBox TbxMakerHinban = D.Items[i].FindControl("TbxMakerHinban") as TextBox;
                    RadComboBox RcbWareHouse = D.Items[i].FindControl("RcbWareHouse") as RadComboBox;
                    TextBox TbxHyoujunKakaku = D.Items[i].FindControl("TbxHyoujunKakaku") as TextBox;
                    TextBox TbxShiireKakaku = D.Items[i].FindControl("TbxShiireKakaku") as TextBox;
                    RadDatePicker RdpCpKaishi = D.Items[i].FindControl("RdpCpKaishi") as RadDatePicker;
                    RadDatePicker RdpCpOwari = D.Items[i].FindControl("RdpCpOwari") as RadDatePicker;
                    TextBox TbxCpKakaku = D.Items[i].FindControl("TbxCpKakaku") as TextBox;
                    TextBox TbxCpShiire = D.Items[i].FindControl("TbxCpShiire") as TextBox;
                    try
                    {
                        if (RcbCategory.Text != "")
                        {
                            dr.SyouhinCode = strSyouhinCode;
                            dr.SyouhinMei = strSyouhinMei;
                            if (RdpPermissionStart.SelectedDate != null)
                            {
                                dr.PermissionStart = RdpPermissionStart.SelectedDate.Value.ToShortDateString();
                            }
                            if (RdpRightEnd.SelectedDate != null)
                            {
                                dr.RightEnd = RdpRightEnd.SelectedDate.Value.ToShortDateString();
                            }
                            dr.CategoryCode = int.Parse(RcbCategory.SelectedValue);
                            dr.Categoryname = RcbCategory.Text;
                            if (ChkJacket.Checked)
                            {
                                dr.JacketPrint = "1";
                            }
                            else
                            {
                                dr.JacketPrint = "0";
                            }
                            if (ChkKyodakusyo.Checked)
                            {
                                dr.PermissionPrint = "1";
                            }
                            else
                            {
                                dr.PermissionPrint = "0";
                            }
                            if (ChkReturn.Checked)
                            {
                                dr.Henkyaku = "1";
                            }
                            else
                            {
                                dr.Henkyaku = "0";
                            }
                            if (ChkHoukokusyo.Checked)
                            {
                                dr.Houkokusyo = "1";
                            }
                            else
                            {
                                dr.Houkokusyo = "0";
                            }
                            dr.Media = RcbMedia.Text;
                            dr.ShiireCode = Maker.SelectedValue;
                            dr.ShiireName = Maker.Text;
                            dr.Makernumber = TbxMakerHinban.Text;
                            dr.Hanni = TbxHanni.Text;
                            if (RcbWareHouse.Text != "")
                            {
                                dr.WareHouse = RcbWareHouse.Text;
                            }
                            dr.HyoujunKakaku = int.Parse(TbxHyoujunKakaku.Text.Replace(",", ""));
                            dr.ShiireKakaku = int.Parse(TbxShiireKakaku.Text.Replace(",", ""));
                            if (!string.IsNullOrEmpty(TbxCpKakaku.Text))
                            {
                                dr.CpKakaku = TbxCpKakaku.Text.Replace(",", "");
                            }
                            if (!string.IsNullOrEmpty(TbxCpShiire.Text))
                            {
                                dr.CpShiire = TbxCpShiire.Text.Replace(",", "");
                            }
                            if (RdpCpKaishi.SelectedDate != null)
                            {
                                dr.CpKaisi = RdpCpKaishi.SelectedDate.Value.ToShortDateString();
                            }
                            if (RdpCpOwari.SelectedDate != null)
                            {
                                dr.CpOwari = RdpCpOwari.SelectedDate.Value.ToShortDateString();
                            }
                            dtN.AddM_Kakaku_2Row(dr);
                        }
                    }
                    catch
                    {
                        LblStatus.Text = "登録がスキップされた行がありました。";
                        LblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                try
                {
                    ClassMaster.DelProduct(strBaseSyouhinCode, strBaseSyouhinMei, Global.GetConnection());
                    ClassMaster.InsertProduct2(dtN, Global.GetConnection());
                    LblStatus.Text = "登録が完了しました。";
                    LblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    LblStatus.Text = "登録処理でエラーが発生しました。";
                    LblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch
            { ERRMESSAGE(); }


        }

        private void ERRMESSAGE()
        {
            string Errr = "";
            if (TbxCode.Text == "")
            { Errr += "商品コードを記載して下さい。"; }
            if (TbxSyohinMei.Text == "")
            { Errr += "商品名を記載して下さい。"; }
        }

        protected void RadShiire_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            { ListSet.SetShiireSaki(sender, e); }
        }



        protected void RadShiire_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void Souko_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RadBaitai_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void D_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DataSet1.M_Kakaku_2Row dr = (e.Item.DataItem as DataRowView).Row as DataSet1.M_Kakaku_2Row;
                Label CategoryName = e.Item.FindControl("Lblcatename") as Label;
                RadComboBox Maker = e.Item.FindControl("RadMakerRyaku") as RadComboBox;
                TextBox TbxHanni = e.Item.FindControl("TbxHanni") as TextBox;
                RadDatePicker RdpPermissionStart = e.Item.FindControl("RdpPermissionStart") as RadDatePicker;
                RadDatePicker RdpRightEnd = e.Item.FindControl("RdpRightEnd") as RadDatePicker;
                RadComboBox RcbCategory = e.Item.FindControl("RcbCategory") as RadComboBox;
                CheckBox ChkJacket = e.Item.FindControl("ChkJacket") as CheckBox;
                CheckBox ChkKyodakusyo = e.Item.FindControl("ChkKyodakusyo") as CheckBox;
                CheckBox ChkReturn = e.Item.FindControl("ChkReturn") as CheckBox;
                CheckBox ChkHoukokusyo = e.Item.FindControl("ChkHoukokusyo") as CheckBox;
                RadComboBox RcbMedia = e.Item.FindControl("RcbMedia") as RadComboBox;
                TextBox TbxMakerHinban = e.Item.FindControl("TbxMakerHinban") as TextBox;
                RadComboBox RcbWareHouse = e.Item.FindControl("RcbWareHouse") as RadComboBox;
                TextBox TbxHyoujunKakaku = e.Item.FindControl("TbxHyoujunKakaku") as TextBox;
                TextBox TbxShiireKakaku = e.Item.FindControl("TbxShiireKakaku") as TextBox;
                RadDatePicker RdpCpKaishi = e.Item.FindControl("RdpCpKaishi") as RadDatePicker;
                RadDatePicker RdpCpOwari = e.Item.FindControl("RdpCpOwari") as RadDatePicker;
                TextBox TbxCpKakaku = e.Item.FindControl("TbxCpKakaku") as TextBox;
                TextBox TbxCpShiire = e.Item.FindControl("TbxCpShiire") as TextBox;
                Button Delete = e.Item.FindControl("Delete") as Button;
                ListSet.SetCate(RcbCategory);
                ListSet.SetShiire2(Maker);
                if (!dr.IsPermissionStartNull())
                {
                    if (dr.PermissionStart != "")
                    {
                        RdpPermissionStart.SelectedDate = DateTime.Parse(dr.PermissionStart);
                    }
                }
                if (!dr.IsRightEndNull())
                {
                    if (dr.RightEnd != "")
                    {
                        RdpRightEnd.SelectedDate = DateTime.Parse(dr.RightEnd);
                    }
                }
                if (!dr.IsCategorynameNull())
                {
                    RcbCategory.SelectedValue = dr.CategoryCode.ToString();
                    if (RcbCategory.SelectedValue == "205")
                    {
                        TbxHyoujunKakaku.Text = "0";
                        TbxShiireKakaku.Text = "0";
                        TbxHyoujunKakaku.ReadOnly = true;
                        TbxShiireKakaku.ReadOnly = true;
                        TbxHyoujunKakaku.BackColor = System.Drawing.Color.DarkGray;
                        TbxShiireKakaku.BackColor = System.Drawing.Color.DarkGray;
                    }
                    else
                    {
                        TbxHyoujunKakaku.ReadOnly = false;
                        TbxShiireKakaku.ReadOnly = false;
                        TbxHyoujunKakaku.BackColor = System.Drawing.Color.White;
                        TbxShiireKakaku.BackColor = System.Drawing.Color.White;
                    }

                }
                if (!dr.IsJacketPrintNull())
                {
                    ChkJacket.Checked = false;
                    if (dr.JacketPrint == "1")
                    {
                        ChkJacket.Checked = true;
                    }
                    if (dr.JacketPrint == "0")
                    {
                        ChkJacket.Checked = false;
                    }
                }
                if (!dr.IsPermissionPrintNull())
                {
                    ChkKyodakusyo.Checked = false;
                    if (dr.PermissionPrint == "1")
                    {
                        ChkKyodakusyo.Checked = true;
                    }
                    if (dr.PermissionPrint == "0")
                    {
                        ChkKyodakusyo.Checked = false;
                    }
                }
                if (!dr.IsHenkyakuNull())
                {
                    ChkReturn.Checked = false;
                    if (dr.Henkyaku == "1")
                    {
                        ChkReturn.Checked = true;
                    }
                    if (dr.Henkyaku == "0")
                    {
                        ChkReturn.Checked = false;
                    }
                }
                if (!dr.IsHoukokusyoNull())
                {
                    ChkHoukokusyo.Checked = false;
                    if (dr.Houkokusyo == "1")
                    {
                        ChkHoukokusyo.Checked = true;
                    }
                    if (dr.Houkokusyo == "0")
                    {
                        ChkHoukokusyo.Checked = false;
                    }
                }
                RcbMedia.SelectedValue = dr.Media;
                if (!dr.IsShiireNameNull())
                {
                    Maker.SelectedValue = dr.ShiireCode;
                }
                TbxMakerHinban.Text = dr.Makernumber;
                TbxHanni.Text = dr.Hanni;
                if (!dr.IsWareHouseNull())
                {
                    RcbWareHouse.SelectedValue = dr.WareHouse.Trim();
                }

                TbxHyoujunKakaku.Text = dr.HyoujunKakaku.ToString("0,0");
                if (!dr.IsShiireKakakuNull())
                {
                    TbxShiireKakaku.Text = dr.ShiireKakaku.ToString("0,0");
                }
                if (!dr.IsCpKaisiNull())
                {
                    if (dr.CpKaisi != "")
                    {
                        RdpCpKaishi.SelectedDate = DateTime.Parse(dr.CpKaisi);
                    }
                }
                if (!dr.IsCpOwariNull())
                {
                    if (dr.CpOwari != "")
                    {
                        RdpCpOwari.SelectedDate = DateTime.Parse(dr.CpOwari);
                    }
                }
                if (!dr.IsCpKakakuNull())
                {
                    TbxCpKakaku.Text = int.Parse(dr.CpKakaku).ToString("0,0");
                }
                if (!dr.IsCpShiireNull())
                {
                    TbxCpShiire.Text = int.Parse(dr.CpShiire).ToString("0,0");
                }
            }

        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            DataMaster.M_ProductDataTable dt = ClassMaster.GetProduct(Global.GetConnection());

            for (int i = 0; i < D.Items.Count; i++)
            {
                Label ProducutCode = D.Items[i].FindControl("LblSyouhinCode") as Label;
                Label ProductName = D.Items[i].FindControl("LblSyouhinMei") as Label;
                Label MakerNo = D.Items[i].FindControl("LblMakerHinban") as Label;
                Label Media = D.Items[i].FindControl("LblMedia") as Label;
                Label Range = D.Items[i].FindControl("LblRange") as Label;
                Label ShiiiresakiCode = D.Items[i].FindControl("LblShiiresakiCode") as Label;
                Label HyojunKakaku = D.Items[i].FindControl("LblHyojunKakaku") as Label;
                Label PermissionStart = D.Items[i].FindControl("LblPermissionStart") as Label;
                Label PermissionEnd = D.Items[i].FindControl("LblPermissionEnd") as Label;
                Label WareHouse = D.Items[i].FindControl("LblWarehouse") as Label;
                Label ShiiresakiMei = D.Items[i].FindControl("LblShiireMei") as Label;

                DataMaster.M_ProductRow dr = dt.NewM_ProductRow();

                if (ProducutCode.Text != "")
                {
                    string p = ProducutCode.Text;
                    DataMaster.M_ProductDataTable dd = ClassMaster.KensakuProduct(p, Global.GetConnection());
                    if (dd.Count > 0)
                    {
                        LblStatus.Text = "この商品コード(" + p + ")は既に登録されています。";
                        LblStatus.ForeColor = System.Drawing.Color.Red;
                        break;
                    }
                    else
                    {
                        dr.SyouhinCode = ProducutCode.Text;
                    }
                }
                if (ProductName.Text != "")
                { dr.SyouhinMei = ProductName.Text; }
                if (MakerNo.Text != "")
                { dr.Makernumber = MakerNo.Text; }
                if (Media.Text != "")
                { dr.Media = Media.Text; }
                if (Range.Text != "")
                { dr.Hanni = Range.Text; }
                if (ShiiiresakiCode.Text != "")
                { dr.ShiireCode = int.Parse(ShiiiresakiCode.Text); }
                if (ShiiresakiMei.Text != "")
                { dr.ShiireMei = ShiiresakiMei.Text; }
                if (HyojunKakaku.Text != "")
                { dr.HyoujyunPrice = HyojunKakaku.Text; }
                if (PermissionStart.Text != "")
                { dr.PermissionStart = PermissionStart.Text; }
                if (PermissionEnd.Text != "")
                { dr.RightEnd = PermissionEnd.Text; }
                if (WareHouse.Text != "")
                { dr.Warehouse = WareHouse.Text; }

                dt.AddM_ProductRow(dr);
                ClassMaster.InsertProduct(dt, Global.GetConnection());
                LblStatus.Text = "登録しました。";
                LblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }

        protected void RcbShiireName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                ListSet.SetShiireSaki4(sender, e);
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            for (int i = 0; i < D.Items.Count; i++)
            {
                DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
                RadDatePicker RdpPermissionStart = D.Items[i].FindControl("RdpPermissionStart") as RadDatePicker;
                RadDatePicker RdpRightEnd = D.Items[i].FindControl("RdpRightEnd") as RadDatePicker;
                RadComboBox RcbCategory = D.Items[i].FindControl("RcbCategory") as RadComboBox;
                CheckBox ChkJacket = D.Items[i].FindControl("ChkJacket") as CheckBox;
                CheckBox ChkKyodakusyo = D.Items[i].FindControl("ChkKyodakusyo") as CheckBox;
                CheckBox ChkReturn = D.Items[i].FindControl("ChkReturn") as CheckBox;
                CheckBox ChkHoukokusyo = D.Items[i].FindControl("ChkHoukokusyo") as CheckBox;
                RadComboBox RcbMedia = D.Items[i].FindControl("RcbMedia") as RadComboBox;
                RadComboBox RadMakerRyaku = D.Items[i].FindControl("RadMakerRyaku") as RadComboBox;
                TextBox TbxMakerHinban = D.Items[i].FindControl("TbxMakerHinban") as TextBox;
                TextBox TbxHanni = D.Items[i].FindControl("TbxHanni") as TextBox;
                RadComboBox RcbWareHouse = D.Items[i].FindControl("RcbWareHouse") as RadComboBox;
                TextBox TbxHyoujunKakaku = D.Items[i].FindControl("TbxHyoujunKakaku") as TextBox;
                TextBox TbxShiireKakaku = D.Items[i].FindControl("TbxShiireKakaku") as TextBox;
                RadDatePicker RdpCpKaishi = D.Items[i].FindControl("RdpCpKaishi") as RadDatePicker;
                RadDatePicker RdpCpOwari = D.Items[i].FindControl("RdpCpOwari") as RadDatePicker;
                TextBox TbxCpKakaku = D.Items[i].FindControl("TbxCpKakaku") as TextBox;
                TextBox TbxCpShiire = D.Items[i].FindControl("TbxCpShiire") as TextBox;

                try
                {
                    dr.CategoryCode = int.Parse(RcbCategory.SelectedValue);
                    dr.Media = RcbMedia.SelectedValue;
                    dr.ShiireCode = RadMakerRyaku.SelectedValue;
                    dr.Makernumber = TbxMakerHinban.Text;
                    dr.Hanni = TbxHanni.Text;
                    dr.HyoujunKakaku = int.Parse(TbxHyoujunKakaku.Text.Replace(",", ""));
                    dr.SyouhinCode = TbxCode.Text;
                    dr.SyouhinMei = TbxSyohinMei.Text;
                }
                catch (Exception ex)
                {
                    LblStatus.Text = "必要なデータを入力して下さい。";
                    LblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                if (RdpPermissionStart.SelectedDate != null)
                {
                    dr.PermissionStart = RdpPermissionStart.SelectedDate.Value.ToShortDateString();
                }
                if (RdpRightEnd.SelectedDate != null)
                {
                    dr.RightEnd = RdpRightEnd.SelectedDate.Value.ToShortDateString();
                }
                dr.Categoryname = RcbCategory.Text;

                if (ChkJacket.Checked)
                {
                    dr.JacketPrint = "1";
                }
                else
                {
                    dr.JacketPrint = "0";
                }
                if (ChkKyodakusyo.Checked)
                {
                    dr.PermissionPrint = "1";
                }
                else
                {
                    dr.PermissionPrint = "0";
                }
                if (ChkReturn.Checked)
                {
                    dr.Henkyaku = "1";
                }
                else
                {
                    dr.Henkyaku = "0";
                }
                if (ChkHoukokusyo.Checked)
                {
                    dr.Houkokusyo = "1";
                }
                else
                {
                    dr.Houkokusyo = "0";
                }
                dr.ShiireName = RadMakerRyaku.Text;
                dr.WareHouse = RcbWareHouse.Text;
                if (!string.IsNullOrEmpty(TbxShiireKakaku.Text))
                {
                    dr.ShiireKakaku = int.Parse(TbxShiireKakaku.Text.Replace(",", ""));
                }
                if (RdpCpKaishi.SelectedDate != null)
                {
                    dr.CpKaisi = RdpCpKaishi.SelectedDate.Value.ToShortDateString();
                }
                if (RdpCpOwari.SelectedDate != null)
                {
                    dr.CpOwari = RdpCpOwari.SelectedDate.Value.ToShortDateString();
                }
                if (!string.IsNullOrEmpty(TbxCpKakaku.Text))
                {
                    dr.CpKakaku = TbxCpKakaku.Text.Replace(",", "");
                }
                if (!string.IsNullOrEmpty(TbxCpShiire.Text))
                {
                    dr.CpShiire = TbxCpShiire.Text.Replace(",", "");
                }
                dt.AddM_Kakaku_2Row(dr);
            }
            DataSet1.M_Kakaku_2Row drN = dt.NewM_Kakaku_2Row();
            drN.SyouhinCode = TbxCode.Text;
            drN.SyouhinMei = TbxSyohinMei.Text;
            drN.CategoryCode = 101;
            drN.Categoryname = "公共図書館";
            drN.Makernumber = "";
            drN.ShiireCode = "";
            drN.Media = "DVD";
            drN.Hanni = "";
            drN.HyoujunKakaku = 0;
            dt.AddM_Kakaku_2Row(drN);
            D.DataSource = dt;
            D.DataBind();
        }

        protected void D_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int row = e.Item.ItemIndex;
            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
            for (int i = 0; i < D.Items.Count; i++)
            {
                RadDatePicker RdpPermissionStart = D.Items[i].FindControl("RdpPermissionStart") as RadDatePicker;
                RadDatePicker RdpRightEnd = D.Items[i].FindControl("RdpRightEnd") as RadDatePicker;
                RadComboBox RcbCategory = D.Items[i].FindControl("RcbCategory") as RadComboBox;
                CheckBox ChkJacket = D.Items[i].FindControl("ChkJacket") as CheckBox;
                CheckBox ChkKyodakusyo = D.Items[i].FindControl("ChkKyodakusyo") as CheckBox;
                CheckBox ChkReturn = D.Items[i].FindControl("ChkReturn") as CheckBox;
                CheckBox ChkHoukokusyo = D.Items[i].FindControl("ChkHoukokusyo") as CheckBox;
                RadComboBox RcbMedia = D.Items[i].FindControl("RcbMedia") as RadComboBox;
                RadComboBox RadMakerRyaku = D.Items[i].FindControl("RadMakerRyaku") as RadComboBox;
                TextBox TbxMakerHinban = D.Items[i].FindControl("TbxMakerHinban") as TextBox;
                TextBox TbxHanni = D.Items[i].FindControl("TbxHanni") as TextBox;
                RadComboBox RcbWareHouse = D.Items[i].FindControl("RcbWareHouse") as RadComboBox;
                TextBox TbxHyoujunKakaku = D.Items[i].FindControl("TbxHyoujunKakaku") as TextBox;
                TextBox TbxShiireKakaku = D.Items[i].FindControl("TbxShiireKakaku") as TextBox;
                RadDatePicker RdpCpKaishi = D.Items[i].FindControl("RdpCpKaishi") as RadDatePicker;
                RadDatePicker RdpCpOwari = D.Items[i].FindControl("RdpCpOwari") as RadDatePicker;
                TextBox TbxCpKakaku = D.Items[i].FindControl("TbxCpKakaku") as TextBox;
                TextBox TbxCpShiire = D.Items[i].FindControl("TbxCpShiire") as TextBox;
                if (e.CommandName == "Del")
                {
                    if (i != row)
                    {
                        DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
                        if (!string.IsNullOrEmpty(RdpPermissionStart.SelectedDate.ToString()))
                        {
                            dr.PermissionStart = RdpPermissionStart.SelectedDate.Value.ToShortDateString();
                        }
                        if (!string.IsNullOrEmpty(RdpRightEnd.SelectedDate.ToString()))
                        {
                            dr.RightEnd = RdpRightEnd.SelectedDate.Value.ToShortDateString();
                        }
                        if (!string.IsNullOrEmpty(RcbCategory.Text))
                        {
                            dr.Categoryname = RcbCategory.Text;
                            dr.CategoryCode = int.Parse(RcbCategory.SelectedValue);
                        }
                        if (ChkJacket.Checked)
                        {
                            dr.JacketPrint = "1";
                        }
                        else
                        {
                            dr.JacketPrint = "0";
                        }
                        if (ChkKyodakusyo.Checked)
                        {
                            dr.PermissionPrint = "1";
                        }
                        else
                        {
                            dr.PermissionPrint = "0";
                        }
                        if (ChkReturn.Checked)
                        {
                            dr.Henkyaku = "1";
                        }
                        else
                        {
                            dr.Henkyaku = "0";
                        }
                        if (ChkHoukokusyo.Checked)
                        {
                            dr.Houkokusyo = "1";
                        }
                        else
                        {
                            dr.Houkokusyo = "0";
                        }
                        if (!string.IsNullOrEmpty(RcbMedia.SelectedValue))
                        {
                            dr.Media = RcbMedia.SelectedValue;
                        }
                        if (!string.IsNullOrEmpty(RadMakerRyaku.Text))
                        {
                            dr.ShiireName = RadMakerRyaku.Text;
                            dr.ShiireCode = RadMakerRyaku.SelectedValue;
                        }
                        if (!string.IsNullOrEmpty(TbxMakerHinban.Text))
                        {
                            dr.Makernumber = TbxMakerHinban.Text;
                        }
                        dr.Hanni = TbxHanni.Text;

                        if (!string.IsNullOrEmpty(RcbWareHouse.SelectedValue))
                        {
                            dr.WareHouse = RcbWareHouse.SelectedValue;
                        }
                        if (!string.IsNullOrEmpty(TbxHyoujunKakaku.Text))
                        {
                            dr.HyoujunKakaku = int.Parse(TbxHyoujunKakaku.Text.Replace(",", ""));
                        }
                        if (!string.IsNullOrEmpty(TbxShiireKakaku.Text))
                        {
                            dr.ShiireKakaku = int.Parse(TbxShiireKakaku.Text.Replace(",", ""));
                        }
                        if (!string.IsNullOrEmpty(RdpCpKaishi.SelectedDate.ToString()))
                        {
                            dr.CpKaisi = RdpCpKaishi.SelectedDate.Value.ToShortDateString();
                        }
                        if (!string.IsNullOrEmpty(RdpCpOwari.SelectedDate.ToString()))
                        {
                            dr.CpOwari = RdpCpOwari.SelectedDate.Value.ToShortDateString();
                        }
                        if (!string.IsNullOrEmpty(TbxCpKakaku.Text))
                        {
                            dr.CpKakaku = TbxCpKakaku.Text.Replace(",", "");
                        }
                        if (!string.IsNullOrEmpty(TbxCpShiire.Text))
                        {
                            dr.CpShiire = TbxCpShiire.Text.Replace(",", "");
                        }
                        dr.SyouhinCode = TbxCode.Text;
                        dr.SyouhinMei = TbxSyohinMei.Text;
                        dt.AddM_Kakaku_2Row(dr);
                    }
                }
            }
            D.DataSource = dt;
            D.DataBind();
        }

        protected void RcbCategory_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < D.Items.Count; i++)
            {
                RadComboBox RcbCategory = D.Items[i].FindControl("RcbCategory") as RadComboBox;
                TextBox TbxHyoujunKakaku = D.Items[i].FindControl("TbxHyoujunKakaku") as TextBox;
                TextBox TbxShiireKakaku = D.Items[i].FindControl("TbxShiireKakaku") as TextBox;
                if (RcbCategory.SelectedValue == "205")
                {
                    TbxHyoujunKakaku.Text = "0";
                    TbxShiireKakaku.Text = "0";
                    TbxHyoujunKakaku.ReadOnly = true;
                    TbxShiireKakaku.ReadOnly = true;
                    TbxHyoujunKakaku.BackColor = System.Drawing.Color.DarkGray;
                    TbxShiireKakaku.BackColor = System.Drawing.Color.DarkGray;
                }
                else
                {
                    TbxHyoujunKakaku.ReadOnly = false;
                    TbxShiireKakaku.ReadOnly = false;
                    TbxHyoujunKakaku.BackColor = System.Drawing.Color.White;
                    TbxShiireKakaku.BackColor = System.Drawing.Color.White;
                }
            }

        }

        protected void RadMakerRyaku_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
    }
}