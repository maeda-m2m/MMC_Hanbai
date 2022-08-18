using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Order
{
    public partial class NewOrderedInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            //Gの表示
            for (int i = 0; i < 1; i++)
            {
                this.NewRow(dt);
            }
            this.CtrlSyousai.DataSource = dt;
            this.CtrlSyousai.DataBind();
        }

        private void NewRow(DataMitumori.T_RowDataTable dt)
        {
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            dt.AddT_RowRow(dr);
        }

        protected void CategoryRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                HtmlInputHidden HidCategoryCode = (HtmlInputHidden)Ctl.FindControl("HidCategoryCode");
                HtmlInputHidden HidCategoryName = (HtmlInputHidden)Ctl.FindControl("HidCategoryName");
                RadDatePicker SiyoukaishiRDP = (RadDatePicker)Ctl.FindControl("SiyoukaishiRDP");
                RadDatePicker SiyouOwariRDP = (RadDatePicker)Ctl.FindControl("SiyouOwariRDP");

                HidCategoryCode.Value = CategoryRad.SelectedValue;
                HidCateCode.Value = CategoryRad.SelectedValue;
                HidCategoryName.Value = CategoryRad.Text;
                HidCateName.Value = CategoryRad.Text;
                int cate = int.Parse(CategoryRad.SelectedValue);
                Category(cate);
            }
        }

        protected void CategoryRad_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetCategory(sender, e);
        }

        protected void ShiireRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                HtmlInputHidden HidShiireCode = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiCode");
                HtmlInputHidden HidShiireName = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiName");

                HidShiireCode.Value = ShiireRad.SelectedValue;
                HidShiCode.Value = ShiireRad.SelectedValue;
                HidShiName.Value = ShiireRad.Text;
                HidShiireName.Value = ShiireRad.Text;
            }
        }

        protected void ShiireRad_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetShiireSaki4(sender, e);
        }
        //ItemDataBound---------------------------------------------------------------------------
        protected void CtrlSyousai_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            string catecode = "";
            string catename = "";
            string shiirecode = "";
            string shiirename = "";

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;

                CtrlNewOrderedMeisai Ctl = e.Item.FindControl("Syosai") as CtrlNewOrderedMeisai;
                Label RowNo = (Label)Ctl.FindControl("RowNo");
                Label LblMakerNo = (Label)Ctl.FindControl("LblMakerNo");
                TextBox TbxMakerNo = (TextBox)Ctl.FindControl("TbxMakerNo");
                TextBox TbxProduct = (TextBox)Ctl.FindControl("TbxProduct");
                TextBox TbxSuryo = (TextBox)Ctl.FindControl("TbxSuryo");
                TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                TextBox TbxShiireKingaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                TextBox TbxSyouhinCode = (TextBox)Ctl.FindControl("TbxSyouhinCode");
                TextBox TbxSyouhinMei = (TextBox)Ctl.FindControl("TbxSyouhinMei");
                TextBox TbxMakerHinban = (TextBox)Ctl.FindControl("TbxMakerHinban");
                TextBox TbxMedia = (TextBox)Ctl.FindControl("TbxMedia");
                TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
                TextBox TbxShiireCode = (TextBox)Ctl.FindControl("TbxShiireCode");
                TextBox TbxShiiresakiMei = (TextBox)Ctl.FindControl("TbxShiiresakiMei");
                TextBox TbxCategoryCode = (TextBox)Ctl.FindControl("TbxCategoryCode");
                TextBox TbxCategoryName = (TextBox)Ctl.FindControl("TbxCategoryName");
                TextBox TbxNyuryokuSya = (TextBox)Ctl.FindControl("TbxNyuryokuSya");
                RadComboBox HanniRad = (RadComboBox)Ctl.FindControl("HanniRad");
                RadComboBox MediaRad = (RadComboBox)Ctl.FindControl("MediaRad");
                RadComboBox FacilityRad = (RadComboBox)Ctl.FindControl("FacilityRad");
                RadComboBox ZasuRad = (RadComboBox)Ctl.FindControl("ZasuRad");
                RadComboBox WareHouseRad = (RadComboBox)Ctl.FindControl("WareHouseRad");
                RadComboBox ProductRad = (RadComboBox)Ctl.FindControl("ProductRad");
                RadDatePicker SiyouKaishiRDP = (RadDatePicker)Ctl.FindControl("SiyoukaishiRDP");
                RadDatePicker SiyouOwariRDP = (RadDatePicker)Ctl.FindControl("SiyouOwariRDP");
                HtmlInputHidden HidTokuisakiCode = (HtmlInputHidden)Ctl.FindControl("HidTokuisakiCode");
                HtmlInputHidden HidTokuisakiName = (HtmlInputHidden)Ctl.FindControl("HidTokuisakiName");
                HtmlInputHidden HidCategoryCode = (HtmlInputHidden)Ctl.FindControl("HidCategoryCode");
                HtmlInputHidden HidCategoryName = (HtmlInputHidden)Ctl.FindControl("HidCategoryName");
                HtmlInputHidden HidShiiresakiCode = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiCode");
                HtmlInputHidden HidShiiresakiName = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiName");
                HtmlInputHidden HidFacilityCode = (HtmlInputHidden)Ctl.FindControl("HidFacilityCode");
                CheckBox HandChk = (CheckBox)Ctl.FindControl("ChkHand");
                Panel SyouhinSyousai = (Panel)Ctl.FindControl("SyouhinSyousai");

                if (catecode != "")
                {
                    TbxCategoryCode.Text = catecode;
                    catecode = "";
                }
                if (catename != "")
                {
                    TbxCategoryName.Text = catename;
                    catename = "";
                }
                if (shiirecode != "")
                {
                    TbxShiireCode.Text = shiirecode;
                    shiirecode = "";
                }
                if (shiirename != "")
                {
                    TbxShiiresakiMei.Text = shiirename;
                    shiirename = "";
                }

                if (CtrlSyousai.Items.Count == 0)
                {
                    RowNo.Text = 1.ToString();
                }

                for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                {
                    RowNo.Text = (i + 2).ToString();
                }

                if (dr == null)
                {
                    SyouhinSyousai.Visible = false;
                }
                else
                {
                    SyouhinSyousai.Visible = false;
                    if (!dr.IsHandNull())
                    {
                        if (dr.Hand == "True")
                        {
                            HandChk.Checked = bool.Parse(dr.Hand);
                            TbxMakerNo.Visible = true;
                            ProductRad.Visible = false;
                            TbxProduct.Visible = true;
                            if (!dr.IsMekarHinbanNull())
                            { TbxMakerNo.Text = TbxMakerHinban.Text = dr.MekarHinban; }
                            if (!dr.IsSyouhinMeiNull())
                            { TbxProduct.Text = TbxSyouhinMei.Text = dr.SyouhinMei; }
                            if (!dr.IsRangeNull())
                            { TbxHanni.Text = HanniRad.Text = dr.Range; }
                            if (!dr.IsMediaNull())
                            { MediaRad.SelectedItem.Text = TbxMedia.Text = dr.Media; }
                            if (!dr.IsHattyuSuryouNull())
                            { TbxSuryo.Text = dr.HattyuSuryou.ToString(); }
                            if (!dr.IsShiireTankaNull())
                            { TbxShiireTanka.Text = dr.ShiireTanka; }
                            if (!dr.IsShiireKingakuNull())
                            { TbxShiireKingaku.Text = dr.ShiireKingaku; }
                            if (!dr.IsSisetuMeiNull())
                            { FacilityRad.Text = dr.SisetuMei; }
                            if (!dr.IsSisetuCodeNull())
                            { HidFacilityCode.Value = dr.SisetuCode.ToString(); }
                            if (!dr.IsZasuNull())
                            { ZasuRad.Text = dr.Zasu; }
                            if (!dr.IsSiyouKaishiNull())
                            { SiyouKaishiRDP.SelectedDate = dr.SiyouKaishi; }
                            if (!dr.IsSiyouOwariNull())
                            { SiyouOwariRDP.SelectedDate = dr.SiyouOwari; }
                            if (!dr.IsWareHouseNull())
                            { WareHouseRad.SelectedItem.Text = dr.WareHouse; }
                            if (!dr.IsSyouhinCodeNull())
                            { TbxSyouhinCode.Text = dr.SyouhinCode; }
                            if (HidShiCode.Value != "")
                            { TbxShiireCode.Text = HidShiiresakiCode.Value = HidShiCode.Value; }
                            if (HidShiName.Value != "")
                            {
                                TbxShiiresakiMei.Text = HidShiiresakiName.Value = HidShiName.Value;
                            }
                            if (HidCateCode.Value != "")
                            {
                                TbxCategoryCode.Text = HidCategoryCode.Value = HidCateCode.Value;
                            }
                            if (HidCateName.Value != "")
                            {
                                TbxCategoryName.Text = HidCategoryName.Value = HidCateName.Value;
                            }
                            if (!dr.IsZeikuNull())
                            { TbxNyuryokuSya.Text = dr.Zeiku; }
                        }
                        else
                        {
                            TbxMakerNo.Visible = false;
                            ProductRad.Visible = true;
                            TbxProduct.Visible = false;

                            if (!dr.IsMekarHinbanNull())
                            { LblMakerNo.Text = TbxMakerHinban.Text = dr.MekarHinban; }
                            if (!dr.IsSyouhinMeiNull())
                            { ProductRad.Text = TbxSyouhinMei.Text = dr.SyouhinMei; }
                            if (!dr.IsRangeNull())
                            { TbxHanni.Text = HanniRad.Text = dr.Range; }
                            if (!dr.IsMediaNull())
                            { MediaRad.SelectedItem.Text = TbxMedia.Text = dr.Media; }
                            if (!dr.IsHattyuSuryouNull())
                            { TbxSuryo.Text = dr.HattyuSuryou.ToString(); }
                            if (!dr.IsShiireTankaNull())
                            { TbxShiireTanka.Text = dr.ShiireTanka; }
                            if (!dr.IsShiireKingakuNull())
                            { TbxShiireKingaku.Text = dr.ShiireKingaku; }
                            if (!dr.IsSisetuMeiNull())
                            { FacilityRad.Text = dr.SisetuMei; }
                            if (!dr.IsSisetuCodeNull())
                            { HidFacilityCode.Value = dr.SisetuCode.ToString(); }
                            if (!dr.IsZasuNull())
                            { ZasuRad.Text = dr.Zasu; }
                            if (!dr.IsSiyouKaishiNull())
                            { SiyouKaishiRDP.SelectedDate = dr.SiyouKaishi; }
                            if (!dr.IsSiyouOwariNull())
                            { SiyouOwariRDP.SelectedDate = dr.SiyouOwari; }
                            if (!dr.IsWareHouseNull())
                            { WareHouseRad.SelectedItem.Text = dr.WareHouse; }
                            if (!dr.IsSyouhinCodeNull())
                            { TbxSyouhinCode.Text = dr.SyouhinCode; }
                            if (HidShiCode.Value != "")
                            {
                                TbxShiireCode.Text = HidShiiresakiCode.Value = HidShiCode.Value;
                            }
                            if (HidShiName.Value != "")
                            {
                                TbxShiiresakiMei.Text = HidShiiresakiName.Value = HidShiName.Value;
                            }
                            if (HidCateCode.Value != "")
                            {
                                TbxCategoryCode.Text = HidCategoryCode.Value = HidCateCode.Value;
                            }
                            if (!dr.IsCategoryNameNull())
                            {
                                TbxCategoryName.Text = HidCategoryName.Value = HidCateName.Value;
                            }
                            if (!dr.IsZeikuNull())
                            { TbxNyuryokuSya.Text = dr.Zeiku; }
                        }
                    }
                    else
                    {
                        if (HidShiCode.Value != "")
                        { HidShiiresakiCode.Value = TbxShiireCode.Text = HidShiCode.Value; }
                        if (HidShiName.Value != "")
                        { HidShiiresakiName.Value = TbxShiiresakiMei.Text = HidShiName.Value; }
                        if (HidCateCode.Value != "")
                        { HidCategoryCode.Value = TbxCategoryCode.Text = HidCateCode.Value; }
                        if (HidCateName.Value != "")
                        { HidCategoryName.Value = TbxCategoryName.Text = HidCateName.Value; }
                    }
                }
            }
            if (CategoryRad.SelectedValue != "")
            {
                int categ = int.Parse(CategoryRad.SelectedValue);
                Category(categ);
            }
        }
        //itemdatabound----------------------------------------------------------------------------
        protected void CtrlSyousai_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int row = e.Item.ItemIndex;
            string cn = e.CommandName;
            if (cn == "Del")
            {
                Deleted(row);
            }
        }

        private void Deleted(int row)
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();

            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                if (i != row)
                {
                    CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                    Label RowNo = (Label)Ctl.FindControl("RowNo");
                    Label LblMakerNo = (Label)Ctl.FindControl("LblMakerNo");
                    TextBox TbxMakerNo = (TextBox)Ctl.FindControl("TbxMakerNo");
                    TextBox TbxProduct = (TextBox)Ctl.FindControl("TbxProduct");
                    TextBox TbxSuryo = (TextBox)Ctl.FindControl("TbxSuryo");
                    TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                    TextBox TbxShiireKingaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                    TextBox TbxSyouhinCode = (TextBox)Ctl.FindControl("TbxSyouhinCode");
                    TextBox TbxSyouhinMei = (TextBox)Ctl.FindControl("TbxSyouhinMei");
                    TextBox TbxMakerHinban = (TextBox)Ctl.FindControl("TbxMakerHinban");
                    TextBox TbxMedia = (TextBox)Ctl.FindControl("TbxMedia");
                    TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
                    TextBox TbxShiireCode = (TextBox)Ctl.FindControl("TbxShiireCode");
                    TextBox TbxShiiresakiMei = (TextBox)Ctl.FindControl("TbxShiiresakiMei");
                    TextBox TbxCategoryCode = (TextBox)Ctl.FindControl("TbxCategoryCode");
                    TextBox TbxCategoryName = (TextBox)Ctl.FindControl("TbxCategoryName");
                    TextBox TbxNyuryokusya = (TextBox)Ctl.FindControl("TbxNyuryokuSya");
                    RadComboBox ProductRad = (RadComboBox)Ctl.FindControl("ProductRad");
                    RadComboBox HanniRad = (RadComboBox)Ctl.FindControl("HanniRad");
                    RadComboBox MediaRad = (RadComboBox)Ctl.FindControl("MediaRad");
                    RadComboBox FacilityRad = (RadComboBox)Ctl.FindControl("FacilityRad");
                    RadComboBox ZasuRad = (RadComboBox)Ctl.FindControl("ZasuRad");
                    RadComboBox WareHouseRad = (RadComboBox)Ctl.FindControl("WareHouseRad");
                    RadDatePicker SiyouKaishiRDP = (RadDatePicker)Ctl.FindControl("SiyoukaishiRDP");
                    RadDatePicker SiyouOwariRDP = (RadDatePicker)Ctl.FindControl("SiyouOwariRDP");
                    HtmlInputHidden HidFacilityCode = (HtmlInputHidden)Ctl.FindControl("HidFacilityCode");
                    HtmlInputHidden HidCategoryCode = (HtmlInputHidden)Ctl.FindControl("HidCategoryCode");
                    HtmlInputHidden HidCategoryName = (HtmlInputHidden)Ctl.FindControl("HidCategoryName");
                    HtmlInputHidden HidShiiresakiCode = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiCode");
                    HtmlInputHidden HidShiiresakiName = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiName");

                    CheckBox HandChk = (CheckBox)Ctl.FindControl("ChkHand");

                    DataMitumori.T_RowRow dr = dt.NewT_RowRow();

                    try
                    {
                        if (LblMakerNo.Text != "")
                        { dr.MekarHinban = LblMakerNo.Text; }
                        if (TbxMakerNo.Text != "")
                        { dr.MekarHinban = TbxMakerNo.Text; }
                        if (ProductRad.Text != "")
                        { dr.SyouhinMei = ProductRad.Text; }
                        if (TbxProduct.Text != "")
                        { dr.SyouhinMei = TbxProduct.Text; }
                        if (HanniRad.Text != "")
                        { dr.Range = HanniRad.Text; }
                        if (MediaRad.Text != "")
                        { dr.Media = MediaRad.SelectedItem.Text; }
                        if (TbxSuryo.Text != "")
                        { dr.HattyuSuryou = int.Parse(TbxSuryo.Text); }
                        if (TbxShiireTanka.Text != "")
                        { dr.ShiireTanka = TbxShiireTanka.Text; }
                        if (TbxShiireKingaku.Text != "")
                        { dr.ShiireKingaku = TbxShiireKingaku.Text; }
                        if (FacilityRad.Text != "")
                        { dr.SisetuMei = FacilityRad.Text; }
                        if (HidFacilityCode.Value != "")
                        { dr.SisetuCode = int.Parse(HidFacilityCode.Value); }
                        if (ZasuRad.Text != "")
                        { dr.Zasu = ZasuRad.Text; }
                        if (SiyouKaishiRDP.SelectedDate != null)
                        { dr.SiyouKaishi = SiyouKaishiRDP.SelectedDate.Value; }
                        if (SiyouOwariRDP.SelectedDate != null)
                        { dr.SiyouOwari = SiyouOwariRDP.SelectedDate.Value; }
                        if (WareHouseRad.Text != "")
                        { dr.WareHouse = WareHouseRad.SelectedItem.Text; }
                        if (TbxSyouhinCode.Text != "")
                        { dr.SyouhinCode = TbxSyouhinCode.Text; }
                        if (TbxShiireCode.Text != "")
                        { dr.ShiiresakiCode = TbxShiireCode.Text; }
                        if (TbxShiiresakiMei.Text != "")
                        { dr.ShiiresakiMei = TbxShiiresakiMei.Text; }
                        if (TbxCategoryCode.Text != "")
                        { dr.CategoryCode = TbxCategoryCode.Text; }
                        if (TbxCategoryName.Text != "")
                        { dr.CategoryName = TbxCategoryName.Text; }
                        if (TbxNyuryokusya.Text != "")
                        { dr.Zeiku = TbxNyuryokusya.Text; }
                        dr.Hand = HandChk.Checked.ToString();

                        dt.AddT_RowRow(dr);
                    }
                    catch (Exception ex)
                    {
                        Err.Text = ex.Message;
                    }

                }
            }
            CtrlSyousai.DataSource = dt;
            CtrlSyousai.DataBind();
        }

        protected void BtnKeisan_Click(object sender, EventArgs e)
        {
            int shikin = 0;
            int shiiregoukei = 0;
            int suryogoukei = 0;
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                TextBox TbxShiireKingaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                TextBox TbxSuryo = (TextBox)Ctl.FindControl("TbxSuryo");
                TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                LblShiirekei.Text = "";
                int suryo = int.Parse(TbxSuryo.Text.Replace(",", ""));
                int shitan = int.Parse(TbxShiireTanka.Text.Replace(",", ""));
                shikin = suryo * shitan;
                TbxShiireKingaku.Text = shikin.ToString("0,0");
                TbxShiireTanka.Text = shitan.ToString("0,0");
                shiiregoukei += shikin;
                suryogoukei += suryo;
            }
            LblShiirekei.Text = shiiregoukei.ToString("0,0");
            LblSu.Text = suryogoukei.ToString();

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            int shiiireno = 0;
            DataAppropriate.T_AppropriateDataTable dt = new DataAppropriate.T_AppropriateDataTable();
            DataAppropriate.T_AppropriateHeaderDataTable dtH = new DataAppropriate.T_AppropriateHeaderDataTable();
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                Label RowNo = (Label)Ctl.FindControl("RowNo");
                Label LblMakerNo = (Label)Ctl.FindControl("LblMakerNo");
                TextBox TbxMakerNo = (TextBox)Ctl.FindControl("TbxMakerNo");
                TextBox TbxProduct = (TextBox)Ctl.FindControl("TbxProduct");
                TextBox TbxSuryo = (TextBox)Ctl.FindControl("TbxSuryo");
                TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                TextBox TbxShiireKingaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                TextBox TbxSyouhinCode = (TextBox)Ctl.FindControl("TbxSyouhinCode");
                TextBox TbxSyouhinMei = (TextBox)Ctl.FindControl("TbxSyouhinMei");
                TextBox TbxMakerHinban = (TextBox)Ctl.FindControl("TbxMakerHinban");
                TextBox TbxMedia = (TextBox)Ctl.FindControl("TbxMedia");
                TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
                TextBox TbxShiireCode = (TextBox)Ctl.FindControl("TbxShiireCode");
                TextBox TbxShiiresakiMei = (TextBox)Ctl.FindControl("TbxShiiresakiMei");
                TextBox TbxCategoryCode = (TextBox)Ctl.FindControl("TbxCategoryCode");
                TextBox TbxCategoryName = (TextBox)Ctl.FindControl("TbxCategoryName");
                TextBox TbxNyuryokusya = (TextBox)Ctl.FindControl("TbxNyuryokuSya");
                RadComboBox ProductRad = (RadComboBox)Ctl.FindControl("ProductRad");
                RadComboBox HanniRad = (RadComboBox)Ctl.FindControl("HanniRad");
                RadComboBox MediaRad = (RadComboBox)Ctl.FindControl("MediaRad");
                RadComboBox FacilityRad = (RadComboBox)Ctl.FindControl("FacilityRad");
                RadComboBox ZasuRad = (RadComboBox)Ctl.FindControl("ZasuRad");
                RadComboBox WareHouseRad = (RadComboBox)Ctl.FindControl("WareHouseRad");
                RadDatePicker SiyouKaishiRDP = (RadDatePicker)Ctl.FindControl("SiyoukaishiRDP");
                RadDatePicker SiyouOwariRDP = (RadDatePicker)Ctl.FindControl("SiyouOwariRDP");
                HtmlInputHidden HidFacilityCode = (HtmlInputHidden)Ctl.FindControl("HidFacilityCode");
                HtmlInputHidden HidTokuisakiCode = (HtmlInputHidden)Ctl.FindControl("HidTokuisakiCode");
                HtmlInputHidden HidTokuisakiName = (HtmlInputHidden)Ctl.FindControl("HidTokuisakiName");
                CheckBox HandChk = (CheckBox)Ctl.FindControl("ChkHand");

                DataAppropriate.T_AppropriateRow dr = dt.NewT_AppropriateRow();
                DataAppropriate.T_AppropriateHeaderRow drH = dtH.NewT_AppropriateHeaderRow();

                try
                {
                    if (HandChk.Checked)
                    {
                        dr.MekerNo = TbxMakerNo.Text;
                        dr.ProductName = TbxProduct.Text;
                    }
                    else
                    {
                        dr.MekerNo = LblMakerNo.Text;
                        dr.ProductName = ProductRad.Text;
                    }
                    //明細
                    dr.Range = HanniRad.Text;
                    dr.Media = MediaRad.Text;
                    dr.OrderedAmount = int.Parse(TbxSuryo.Text);
                    dr.Zansu = TbxSuryo.Text;
                    dr.ShiireTanka = int.Parse(TbxShiireTanka.Text.Replace(",", ""));
                    dr.ShiireKingaku = int.Parse(TbxShiireKingaku.Text.Replace(",", ""));
                    dr.FacilityName = FacilityRad.Text;
                    dr.Zasu = ZasuRad.Text;
                    dr.SiyouKaishi = SiyouKaishiRDP.SelectedDate.ToString();
                    dr.SiyouiOwari = SiyouOwariRDP.SelectedDate.ToString();
                    dr.WareHouse = WareHouseRad.SelectedItem.Text;
                    if (HidFacilityCode.Value != "")
                    { dr.FacilityCode = int.Parse(HidFacilityCode.Value); }
                    dr.Category = int.Parse(CategoryRad.SelectedValue);
                    dr.CategoryName = CategoryRad.Text;
                    dr.HatyuDay = DateTime.Now.ToShortDateString();
                    dr.HatyuFLG = "true";
                    if (HidTokuisakiCode.Value != "")
                    { dr.TokuisakiCode = HidTokuisakiCode.Value; }
                    if (HidTokuisakiName.Value != "")
                    { dr.TokuisakiMei = HidTokuisakiName.Value; }
                    //ヘッダ
                    string cate = CategoryRad.Text;
                    string shiire = ShiireRad.Text;
                    DataAppropriate.T_AppropriateHeaderDataTable dtHA = Class1.GetHeader(cate, shiire, Global.GetConnection());

                    if (dt.Count == 0)
                    {
                        drH.Category = int.Parse(CategoryRad.SelectedValue);
                        drH.CategoryName = CategoryRad.Text;
                        drH.ShiireAmount = int.Parse(LblSu.Text);
                        drH.SoukeiGaku = int.Parse(LblShiirekei.Text.Replace(",", ""));
                        drH.CreateDate = DateTime.Now;
                        drH.ShiiresakiCode = ShiireRad.SelectedValue;
                        drH.ShiiresakiName = ShiireRad.Text;
                        DataAppropriate.T_AppropriateHeaderDataTable drM = Class1.GetMaxShiireNo(Global.GetConnection());
                        drH.ShiireNo = drM.Count + 1;
                        dr.ShiireNo = drM.Count + 1;
                        dr.RowNo = int.Parse(RowNo.Text);
                        if (StartDate.SelectedDate != null)
                        {
                            dr.SiyouKaishi = StartDate.SelectedDate.Value.ToShortDateString();
                        }
                        if (EndDate.SelectedDate != null)
                        {
                            dr.SiyouiOwari = EndDate.SelectedDate.Value.ToShortDateString();
                        }
                        dtH.AddT_AppropriateHeaderRow(drH);
                        Class1.InsertAppropriateHeader(dtH, Global.GetConnection());
                        dt.AddT_AppropriateRow(dr);
                        shiiireno = drM.Count + 1;
                    }
                    else
                    {
                        dr.ShiireNo = shiiireno;
                        dr.RowNo = int.Parse(RowNo.Text);
                        dt.AddT_AppropriateRow(dr);
                    }
                }
                catch (Exception ex)
                {
                    Err.Text = ex.Message;
                }
            }
            Class1.InsertAppropriate(dt, Global.GetConnection());
            End.Text = "仕入データに登録しました。";
        }

        private void Category(int cate)
        {
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                RadDatePicker SiyoukaishiRDP = (RadDatePicker)Ctl.FindControl("SiyoukaishiRDP");
                RadDatePicker SiyouOwariRDP = (RadDatePicker)Ctl.FindControl("SiyouOwariRDP");

                if (cate != 0)
                {

                    switch (cate)
                    {
                        case 101:
                        case 102:
                        case 103:
                        case 199:
                            SiyoukaishiRDP.Visible = false;
                            SiyouOwariRDP.Visible = false;
                            break;

                        case 201:
                        case 202:
                        case 203:
                        case 204:
                        case 205:
                        case 206:
                        case 207:
                        case 208:
                        case 209:
                        case 299:
                        case 301:
                        case 302:
                        case 401:
                        case 402:
                            SiyoukaishiRDP.Visible = true;
                            SiyouOwariRDP.Visible = true;
                            StartDate.Focus();
                            break;
                    }
                }
            }
        }

        protected void BtnAddRow_Click(object sender, EventArgs e)
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            for (int i = 0; i < CtrlSyousai.Items.Count; i++)
            {
                CtrlNewOrderedMeisai Ctl = CtrlSyousai.Items[i].FindControl("Syosai") as CtrlNewOrderedMeisai;
                Label RowNo = (Label)Ctl.FindControl("RowNo");
                Label LblMakerNo = (Label)Ctl.FindControl("LblMakerNo");
                TextBox TbxMakerNo = (TextBox)Ctl.FindControl("TbxMakerNo");
                TextBox TbxProduct = (TextBox)Ctl.FindControl("TbxProduct");
                TextBox TbxSuryo = (TextBox)Ctl.FindControl("TbxSuryo");
                TextBox TbxShiireTanka = (TextBox)Ctl.FindControl("TbxShiireTanka");
                TextBox TbxShiireKingaku = (TextBox)Ctl.FindControl("TbxShiireKingaku");
                TextBox TbxSyouhinCode = (TextBox)Ctl.FindControl("TbxSyouhinCode");
                TextBox TbxSyouhinMei = (TextBox)Ctl.FindControl("TbxSyouhinMei");
                TextBox TbxMakerHinban = (TextBox)Ctl.FindControl("TbxMakerHinban");
                TextBox TbxMedia = (TextBox)Ctl.FindControl("TbxMedia");
                TextBox TbxHanni = (TextBox)Ctl.FindControl("TbxHanni");
                TextBox TbxShiireCode = (TextBox)Ctl.FindControl("TbxShiireCode");
                TextBox TbxShiiresakiMei = (TextBox)Ctl.FindControl("TbxShiiresakiMei");
                TextBox TbxCategoryCode = (TextBox)Ctl.FindControl("TbxCategoryCode");
                TextBox TbxCategoryName = (TextBox)Ctl.FindControl("TbxCategoryName");
                TextBox TbxNyuryokusya = (TextBox)Ctl.FindControl("TbxNyuryokuSya");
                RadComboBox ProductRad = (RadComboBox)Ctl.FindControl("ProductRad");
                RadComboBox HanniRad = (RadComboBox)Ctl.FindControl("HanniRad");
                RadComboBox MediaRad = (RadComboBox)Ctl.FindControl("MediaRad");
                RadComboBox FacilityRad = (RadComboBox)Ctl.FindControl("FacilityRad");
                RadComboBox ZasuRad = (RadComboBox)Ctl.FindControl("ZasuRad");
                RadComboBox WareHouseRad = (RadComboBox)Ctl.FindControl("WareHouseRad");
                RadDatePicker SiyouKaishiRDP = (RadDatePicker)Ctl.FindControl("SiyoukaishiRDP");
                RadDatePicker SiyouOwariRDP = (RadDatePicker)Ctl.FindControl("SiyouOwariRDP");
                HtmlInputHidden HidFacilityCode = (HtmlInputHidden)Ctl.FindControl("HidFacilityCode");
                HtmlInputHidden HidCategoryCode = (HtmlInputHidden)Ctl.FindControl("HidCategoryCode");
                HtmlInputHidden HidCategoryName = (HtmlInputHidden)Ctl.FindControl("HidCategoryName");
                HtmlInputHidden HidShiiresakiCode = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiCode");
                HtmlInputHidden HidShiiresakiName = (HtmlInputHidden)Ctl.FindControl("HidShiiresakiName");

                CheckBox HandChk = (CheckBox)Ctl.FindControl("ChkHand");

                DataMitumori.T_RowRow dr = dt.NewT_RowRow();

                try
                {
                    if (LblMakerNo.Text != "")
                    { dr.MekarHinban = LblMakerNo.Text; }
                    if (TbxMakerNo.Text != "")
                    { dr.MekarHinban = TbxMakerNo.Text; }
                    if (ProductRad.Text != "")
                    { dr.SyouhinMei = ProductRad.Text; }
                    if (TbxProduct.Text != "")
                    { dr.SyouhinMei = TbxProduct.Text; }
                    if (HanniRad.Text != "")
                    { dr.Range = HanniRad.Text; }
                    if (MediaRad.SelectedItem.Text != "")
                    { dr.Media = MediaRad.SelectedItem.Text; }
                    if (TbxSuryo.Text != "")
                    { dr.HattyuSuryou = int.Parse(TbxSuryo.Text); }
                    if (TbxShiireTanka.Text != "")
                    { dr.ShiireTanka = TbxShiireTanka.Text; }
                    if (TbxShiireKingaku.Text != "")
                    { dr.ShiireKingaku = TbxShiireKingaku.Text; }
                    if (FacilityRad.Text != "")
                    { dr.SisetuMei = FacilityRad.Text; }
                    if (HidFacilityCode.Value != "")
                    { dr.SisetuCode = int.Parse(HidFacilityCode.Value); }
                    if (ZasuRad.Text != "")
                    { dr.Zasu = ZasuRad.Text; }
                    if (SiyouKaishiRDP.SelectedDate != null)
                    { dr.SiyouKaishi = SiyouKaishiRDP.SelectedDate.Value; }
                    if (SiyouOwariRDP.SelectedDate != null)
                    { dr.SiyouOwari = SiyouOwariRDP.SelectedDate.Value; }
                    if (WareHouseRad.Text != "")
                    { dr.WareHouse = WareHouseRad.SelectedItem.Text; }
                    if (TbxSyouhinCode.Text != "")
                    { dr.SyouhinCode = TbxSyouhinCode.Text; }
                    if (TbxShiireCode.Text != "")
                    { dr.ShiiresakiCode = TbxShiireCode.Text; }
                    if (TbxShiiresakiMei.Text != "")
                    { dr.ShiiresakiMei = TbxShiiresakiMei.Text; }
                    if (TbxCategoryCode.Text != "")
                    { dr.CategoryCode = TbxCategoryCode.Text; }
                    if (TbxCategoryName.Text != "")
                    { dr.CategoryName = TbxCategoryName.Text; }
                    if (TbxNyuryokusya.Text != "")
                    { dr.Zeiku = TbxNyuryokusya.Text; }
                    dr.Hand = HandChk.Checked.ToString();

                    dt.AddT_RowRow(dr);
                }
                catch (Exception ex)
                {
                    Err.Text = ex.Message;
                }
            }
            DataMitumori.T_RowRow dl = dt.NewT_RowRow();
            dt.AddT_RowRow(dl);
            CtrlSyousai.DataSource = dt;
            CtrlSyousai.DataBind();
        }

        protected void BtnKeisan2_Click(object sender, EventArgs e)
        {

        }

        protected void Kikan_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string d = Kikan.SelectedItem.Text;
            if (d == "1日")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                EndDate.SelectedDate = dd.AddDays(1);
            }

            if (d == "2日")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                EndDate.SelectedDate = dd.AddDays(2);
            }

            if (d == "3日")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                EndDate.SelectedDate = dd.AddDays(3);
            }
            if (d == "4日")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                EndDate.SelectedDate = dd.AddDays(4);
            }
            if (d == "5日")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                EndDate.SelectedDate = dd.AddDays(5);
            }
            if (d == "1ヵ月")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddMonths(1);
            }
            if (d == "2ヵ月")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddMonths(2);
            }
            if (d == "3ヵ月")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddMonths(3);
            }
            if (d == "4ヵ月")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddMonths(4);
            }
            if (d == "5ヵ月")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                EndDate.SelectedDate = dd.AddMonths(5);
            }
            if (d == "6ヵ月")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddMonths(6);
            }
            if (d == "1年")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddYears(1);
            }
            if (d == "99年")
            {
                DateTime dd = StartDate.SelectedDate.Value;
                DateTime dt = dd.AddDays(-1);
                EndDate.SelectedDate = dt.AddYears(98);
            }

        }

        protected void ChkFuku_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFuku.Checked)
            {
                if (StartDate.SelectedDate != null)
                {
                    for (int i = 0; i < CtrlSyousai.Items.Count; i++)
                    {
                        CtrlNewOrderedMeisai ctl = (CtrlNewOrderedMeisai)CtrlSyousai.Items[i].FindControl("Syosai");
                        RadDatePicker rdps = (RadDatePicker)ctl.FindControl("SiyoukaishiRDP");
                        RadDatePicker rdpe = (RadDatePicker)ctl.FindControl("SiyouOwariRDP");
                        rdps.SelectedDate = StartDate.SelectedDate;
                        rdpe.SelectedDate = EndDate.SelectedDate;
                    }
                }
            }
            else
            {

            }
        }
    }
}