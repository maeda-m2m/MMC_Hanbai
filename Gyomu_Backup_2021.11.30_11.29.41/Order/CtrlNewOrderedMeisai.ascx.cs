using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Order
{
    public partial class CtrlNewOrderedMeisai : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SyouhinSyousai.Visible = false;
            }
            if (!ChkHand.Checked)
            {
                TbxMakerNo.Visible = false;
                TbxProduct.Visible = false;
            }
            else
            {
                TbxMakerNo.Visible = true;
                TbxProduct.Visible = true;
            }
            Create();
            string s = HidCategoryCode.Value;
        }

        private void Create()
        {
            TbxNyuryokuSya.Text = SessionManager.User.M_user.UserName;
            TbxShiiresakiMei.Text = HidShiiresakiName.Value;
            TbxShiireCode.Text = HidShiiresakiCode.Value;
            TbxCategoryCode.Text = HidCategoryCode.Value;
            TbxCategoryName.Text = HidCategoryName.Value;

            if (HidCategoryCode.Value != "")
            {
                switch (int.Parse(HidCategoryCode.Value))
                {
                    case 101:

                        break;

                }
            }

        }

        protected void ProductRad_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string cate = "";
            string shiire = "";
            if (HidCategoryCode.Value != "")
            {
                cate += HidCategoryCode.Value;
            }
            if (HidShiiresakiCode.Value != "")
            {
                shiire += HidShiiresakiCode.Value;
            }
            ListSet.SetProduct2(sender, e, cate, shiire);
        }

        protected void ProductRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ProductRad.SelectedValue != "")
            {
                string[] strAry = ProductRad.SelectedValue.Split('/');
                TbxSyouhinCode.Text = strAry[0];
                TbxSyouhinMei.Text = ProductRad.Text = strAry[1];
                LblMakerNo.Text = TbxMakerHinban.Text = TbxMakerNo.Text = strAry[2];
                MediaRad.SelectedItem.Text = TbxMedia.Text = strAry[3];
                HanniRad.Text = TbxHanni.Text = strAry[4];
                HidShiiresakiCode.Value = TbxShiireCode.Text = strAry[5];
                TbxShiiresakiMei.Text = strAry[6];
                HidCategoryCode.Value = TbxCategoryCode.Text = strAry[7];
                TbxCategoryName.Text = strAry[8];

                WareHouseRad.SelectedItem.Text = strAry[10];

                int shitan = int.Parse(strAry[9]);
                TbxShiireTanka.Text = shitan.ToString("0,0");
                int suryo = int.Parse(TbxSuryo.Text);
                TbxShiireKingaku.Text = (shitan * suryo).ToString("0,0");
                TbxNyuryokuSya.Text = SessionManager.User.M_user.UserName;
            }
        }

        internal static void CateChange(int cate)
        {
        }

        protected void HanniRad_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetHanni(sender, e);
        }

        protected void HanniRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TbxHanni.Text = HanniRad.Text;
        }

        protected void FaclityRad_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetFacility(sender, e);
        }

        protected void FaclityRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            HidFacilityCode.Value = FacilityRad.SelectedValue;
        }

        protected void ChkHand_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHand.Checked)
            {
                LblMakerNo.Visible = false;
                TbxMakerNo.Visible = true;
                ProductRad.Visible = false;
                TbxProduct.Visible = true;
            }
            else
            {
                LblMakerNo.Visible = true;
                TbxMakerNo.Visible = false;
                ProductRad.Visible = true;
                TbxProduct.Visible = false;
            }
        }

        protected void BtnClode_Click(object sender, EventArgs e)
        {
            SyouhinSyousai.Visible = false;
        }

        protected void BtnSyouhinSyousai_Click(object sender, EventArgs e)
        {
            SyouhinSyousai.Visible = true;
        }

        protected void WareHouseRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void MediaRad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TbxMedia.Text = MediaRad.Text;
        }

        protected void TbxMakerNo_TextChanged(object sender, EventArgs e)
        {
            TbxMakerHinban.Text = TbxMakerNo.Text;
        }

        protected void TbxProduct_TextChanged(object sender, EventArgs e)
        {
            TbxSyouhinMei.Text = TbxProduct.Text;
        }
    }
}