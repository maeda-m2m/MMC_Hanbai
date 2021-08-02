using DLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace Gyomu.Master
{
    public partial class CtlSyohin : System.Web.UI.UserControl
    {
        private String vsID
        {
            get
            {
                object obj = this.ViewState["vsID"];
                if (null == obj) return "";
                return Convert.ToString(obj);
            }
            set
            {
                this.ViewState["vsID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ListSet.SetShiire(RadShiire);
                Label1.Visible = false;
                Label2.Visible = false;

            }
            else
            {
            }

        }

        private enum EnumSetType
        {
            Add = 0,
            Delete = 1,
            Copy = 2,
            Register = 3,
            Create = 4,
            Mitumori = 5
        }

        private void Bind()
        {
            //DataDrop.M_CategoryDataTable Cdt =
            // ClassDrop.GetCate(Global.GetConnection());

            //DataView dv = new DataView(Cdt);
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            GetKensakuParam(p);

            //DataSet1.M_Kakaku_New1DataTable dd = ClassKensaku.GetkakakuNew(p, Global.GetConnection());
            DataSet1.V_EditSyousaiDataTable dd = ClassKensaku.GetEditSyousaiDT(p, Global.GetConnection());
            D.DataSource = dd;
            D.DataBind();

        }

        internal void Create(string userId)
        {
            vsID = userId;

            string sID = userId.Split('/')[0];
            string sName = userId.Split('/')[1];
            string sMedia = userId.Split('/')[2];

            DataMaster.M_ProductRow dr =
                ClassMaster.GetProductRow(sID, sName, sMedia, Global.GetConnection());


            TbxCode.Text = dr.SyouhinCode;
            Label1.Text = dr.SyouhinCode;
            TbxSyohinMei.Text = dr.SyouhinMei;
            //if (!dr.IsMeisaikubunNull())
            //    TbxKubun.Text = dr.Meisaikubun.ToString();
            if (!dr.IsMediaNull())
                RadBaitai.Text = dr.Media.ToString();
            if (!dr.IsShiireCodeNull())
                RadShiire.Text = dr.ShiireMei.ToString();

            if (dr.IsWarehouseNull())
            {
                ChkJotai.SelectedValue = "有効";
            }

            if (!dr.IsHanniNull())
                TbxTosyoCode.Text = dr.Hanni;
            if (!dr.IsWarehouseNull())
                Souko.Text = dr.Warehouse.ToString();

            Bind();
        }


        internal void Clear()
        {
            TbxCode.Text = TbxSyohinMei.Text = TbxTosyoCode.Text = Souko.Text = "";
            RadBaitai.SelectedValue = RadShiire.SelectedValue = "";
            ChkJotai.SelectedValue = "有効";
        }

        //protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        //{

        //}



        private void GetKensakuParam(ClassKensaku.KensakuParam p)
        {
            //商品名検索
            if (TbxCode.Text.Trim() != "")
                p.SyouhinCode = TbxCode.Text.Trim();
        }



        protected void Button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < D.Items.Count; i++)
            {

                DataMaster.M_Syohin_NewDataTable dt = new DataMaster.M_Syohin_NewDataTable();
                DataMaster.M_Syohin_NewRow dr = dt.NewM_Syohin_NewRow();

                DataSet1.M_Kakaku_New1DataTable dd = new DataSet1.M_Kakaku_New1DataTable();
                DataSet1.M_Kakaku_New1Row dl = dd.NewM_Kakaku_New1Row();


                dl.SyouhinCode = TbxCode.Text;
                dr.SyouhinCode = TbxCode.Text;
                dl.SyouhinMei = TbxSyohinMei.Text;
                dr.SyouhinCode = TbxSyohinMei.Text;
                dl.Media = RadBaitai.SelectedItem.Text;
                dr.Media = RadBaitai.SelectedItem.Text;
                dl.Hanni = TbxTosyoCode.Text;
                dr.Range = TbxTosyoCode.Text;
                dl.WareHouse = Souko.Text;
                dr.Warehouse = Souko.Text;
                if (RadShiire.SelectedValue != null)
                {
                    dl.ShiireName = RadShiire.SelectedValue;
                }
                if (RadShiire.SelectedValue != null)
                {
                    dr.ShiireMei = RadShiire.SelectedValue;
                }
                dl.Riyo = ChkJotai.Text;

                Label CategoryCode = D.Items[i].FindControl("LblCatecode") as Label;
                Label CategoryName = D.Items[i].FindControl("Lblcatename") as Label;
                TextBox Price = D.Items[i].FindControl("TextBox2") as TextBox;
                RadComboBox RadMakerRyaku = D.Items[i].FindControl("RadMakerRyaku") as RadComboBox;
                RadComboBox Hanni = D.Items[i].FindControl("Hanni") as RadComboBox;
                TextBox ShiirePrice = D.Items[i].FindControl("Shiire") as TextBox;
                RadDatePicker PermissionStart = D.Items[i].FindControl("RadDatePicker3") as RadDatePicker;
                RadDatePicker RightEnd = D.Items[i].FindControl("RadDatePicker4") as RadDatePicker;
                CheckBox Jacket = D.Items[i].FindControl("CheckBox3") as CheckBox;
                CheckBox Henkyaku = D.Items[i].FindControl("CheckBox4") as CheckBox;
                HtmlInputHidden ShiireCode = D.Items[i].FindControl("ShiireCode") as HtmlInputHidden;
                HtmlInputHidden Makernumber = D.Items[i].FindControl("MakerNumber") as HtmlInputHidden;


                if (Price.Text != "")
                {
                    if (CategoryCode.Text != "")
                    {
                        dl.CategoryCode = int.Parse(CategoryCode.Text);
                    }

                    if (CategoryName.Text != "")
                    {
                        dl.Categoryname = CategoryName.Text;
                    }

                    if (Price.Text != "")
                    {
                        dl.HyojunKakaku = int.Parse(Price.Text);
                    }

                    if (ShiirePrice.Text != "")
                    {
                        dl.ShiireKakaku = int.Parse(ShiirePrice.Text);
                    }

                    if (RadMakerRyaku.SelectedValue != "")
                    {
                        dl.ShiireName = RadMakerRyaku.SelectedItem.Text;
                    }

                    if (ShiireCode.Value != null)
                    {
                        dl.ShiireCode = ShiireCode.Value;
                    }


                    if (Makernumber.Value != "")
                    {
                        dl.Makernumber = Makernumber.Value;
                    }



                    if (Hanni.SelectedValue != "")
                    {
                        dl.Hanni = Hanni.SelectedItem.Text;
                    }


                    if (PermissionStart.SelectedDate.ToString() != "")
                    {
                        dl.PermissionStart = PermissionStart.SelectedDate.ToString();
                    }

                    if (RightEnd.SelectedDate.ToString() != "")
                    {
                        dl.RightEnd = RightEnd.SelectedDate.ToString();
                    }

                    if (Jacket.Checked == true)
                    {
                        dl.JacketPrint = true;
                    }

                    if (Henkyaku.Checked == true)
                    {
                        dl.Henkyaku = true;
                    }

                    string productcode = Label1.Text;
                    int catecode = int.Parse(CategoryCode.Text);


                    Class1.UpdateSyousai(dl, catecode, productcode, Global.GetConnection());
                }
                else
                {
                    string productcode = Label1.Text;
                    int catecode = int.Parse(CategoryCode.Text);
                    Class1.DeleteSyousai(dl, catecode, productcode, Global.GetConnection());
                }

            }
        }


        protected void RadCombo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetTyokusouSaki(sender, e);
        }

        protected void RadComboBox1_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetTyokusouSaki(sender, e);
        }

        protected void RadShiire_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }


        protected void D_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            GetKensakuParam(p);


            DataSet1.V_EditSyousaiDataTable dt = ClassKensaku.GetEditSyousaiDT(p, Global.GetConnection());

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                //20200622 改変　前田

                DataSet1.V_EditSyousaiRow dr = (e.Item.DataItem as DataRowView).Row as DataSet1.V_EditSyousaiRow;
                Label CategoryCode = e.Item.FindControl("LblCatecode") as Label;
                Label CategoryName = e.Item.FindControl("Lblcatename") as Label;
                RadComboBox Maker = e.Item.FindControl("RadMakerRyaku") as RadComboBox;
                RadComboBox Hanni = e.Item.FindControl("Hanni") as RadComboBox;
                RadComboBox radMaker = e.Item.FindControl("RadMakerRyaku") as RadComboBox;
                TextBox Price = e.Item.FindControl("TextBox2") as TextBox;
                TextBox Shiire = e.Item.FindControl("Shiire") as TextBox;
                HtmlInputHidden ShiireCode = e.Item.FindControl("ShiireCode") as HtmlInputHidden;
                HtmlInputHidden Makernumber = e.Item.FindControl("MakerNumber") as HtmlInputHidden;
                RadDatePicker PermissionStart = e.Item.FindControl("RadDatePicker3") as RadDatePicker;
                RadDatePicker RightEnd = e.Item.FindControl("RadDatePicker4") as RadDatePicker;
                CheckBox Jacket = e.Item.FindControl("CheckBox3") as CheckBox;
                CheckBox Henkyaku = e.Item.FindControl("CheckBox4") as CheckBox;

                ListSet.SetHanni(Hanni);
                ListSet.SetMaker(radMaker);

                CategoryCode.Text = dr.Category.ToString();
                Label2.Text = dr.Category.ToString();

                CategoryName.Text = dr.CategoryName;

                if (!dr.IsHyojunKakakuNull())
                { Price.Text = dr.HyojunKakaku.ToString(); }
                if (!dr.IsShiireNameNull())
                {
                    Maker.Text = dr.ShiireName;
                }

                if (!dr.IsHanniNull())
                {
                    Hanni.SelectedItem.Text = dr.Hanni;
                }

                if (!dr.IsShiireKakakuNull())
                {
                    Shiire.Text = dr.ShiireKakaku.ToString();
                }

                if (!dr.IsShiireCodeNull())
                {
                    ShiireCode.Value = dr.ShiireCode;
                }

                if (!dr.IsMakernumberNull())
                { Makernumber.Value = dr.Makernumber; }

                if (!dr.IsPermissionStartNull())
                {
                    PermissionStart.SelectedDate = DateTime.Parse(dr.PermissionStart);
                }
                if (!dr.IsRightEndNull())
                {
                    RightEnd.SelectedDate = DateTime.Parse(dr.RightEnd);
                }
                if (!dr.IsJacketPrintNull())
                {
                    Jacket.Checked = dr.JacketPrint;
                }
                if (!dr.IsHenkyakuNull())
                {
                    Henkyaku.Checked = dr.Henkyaku;
                }
            }

        }

        protected void D_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            GetKensakuParam(p);


            DataSet1.V_EditSyousaiDataTable dt = new DataSet1.V_EditSyousaiDataTable();



            int a = e.Item.ItemIndex;


            for (int i = 0; i < D.Items.Count; i++)
            {
                DataSet1.V_EditSyousaiRow dr = dt.NewV_EditSyousaiRow();

                Label CategoryCode = D.Items[i].FindControl("LblCatecode") as Label;
                Label CategoryName = D.Items[i].FindControl("Lblcatename") as Label;
                Label Maker = D.Items[i].FindControl("RadMakerRyaku") as Label;
                RadComboBox Hanni = D.Items[i].FindControl("Hanni") as RadComboBox;
                RadComboBox radMaker = e.Item.FindControl("RadMakerRyaku") as RadComboBox;
                TextBox Price = D.Items[i].FindControl("TextBox2") as TextBox;
                TextBox Shiire = D.Items[i].FindControl("Shiire") as TextBox;
                RadDatePicker PermissionStart = D.Items[i].FindControl("RadDatePicker3") as RadDatePicker;
                RadDatePicker RightEnd = D.Items[i].FindControl("RadDatePicker4") as RadDatePicker;
                CheckBox Jacket = D.Items[i].FindControl("CheckBox3") as CheckBox;
                CheckBox Henkyaku = D.Items[i].FindControl("CheckBox4") as CheckBox;

                ListSet.SetHanni(Hanni);
                ListSet.SetMaker(radMaker);


                dr.SyouhinCode = TbxCode.Text;
                dr.SyouhinCode = TbxSyohinMei.Text;
                dr.Media = RadBaitai.SelectedItem.Text;
                dr.Hanni = TbxTosyoCode.Text;
                dr.WareHouse = Souko.Text;
                dr.Riyo = ChkJotai.Text;
                dr.SyouhinCode = TbxCode.Text;
                int cc = int.Parse(CategoryCode.Text);
                dr.Category = cc;
                if (CategoryName.Text != "")
                {
                    dr.CategoryName = CategoryName.Text;
                }
                if (radMaker.Text != "")
                {
                    dr.ShiireName = radMaker.Text;
                }
                if (Hanni.SelectedValue != "")
                {
                    dr.Hanni = Hanni.Text;
                }
                if (Price.Text != "")
                {
                    int hy = int.Parse(Price.Text);
                    dr.HyojunKakaku = hy;
                }
                if (Shiire.Text != "")
                {
                    dr.ShiireKakaku = int.Parse(Shiire.Text);
                }

                if (PermissionStart.SelectedDate.ToString() != "")
                {
                    dr.PermissionStart = PermissionStart.SelectedDate.ToString();
                }
                if (RightEnd.SelectedDate.ToString() != "")
                {
                    dr.RightEnd = RightEnd.SelectedDate.ToString();
                }
                if (Jacket.Checked == true)
                {

                    dr.JacketPrint = Jacket.Checked;
                }
                if (Henkyaku.Checked == true)
                {
                    dr.Henkyaku = Henkyaku.Checked;
                }

                dt.AddV_EditSyousaiRow(dr);

                if (a == i)
                {
                    dr = dt.NewV_EditSyousaiRow();
                    dr = this.NewRow(a, dr);
                    dt.AddV_EditSyousaiRow(dr);

                }
            }


            D.DataSource = dt;
            D.DataBind();

        }

        private DataSet1.V_EditSyousaiRow NewRow(int a, DataSet1.V_EditSyousaiRow dr)
        {
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            GetKensakuParam(p);
            DataSet1.V_EditSyousaiDataTable dd = ClassKensaku.GetEditSyousaiDT(p, Global.GetConnection());
            DataSet1.V_EditSyousaiRow dl = dd[a];


            dr.SyouhinCode = dl.SyouhinCode;
            if (!dl.IsSyouhinMeiNull())
            {
                dr.SyouhinMei = dl.SyouhinMei;
            }
            if (!dl.IsMakernumberNull())
            {
                dr.Makernumber = dl.Makernumber;
            }
            if (!dl.IsHanniNull())
            {
                dr.Hanni = dl.Hanni;
            }
            if (!dl.IsWareHouseNull())
            {
                dr.WareHouse = dl.WareHouse;
            }
            if (!dl.IsShiireNameNull())
            {
                dr.ShiireName = dl.ShiireName;
            }
            if (!dl.IsRiyoNull())
            {
                dr.Riyo = dl.Riyo;
            }


            dr.Category = dl.Category;

            dr.CategoryName = dl.CategoryName;

            dr.Makernumber = dl.ShiireName;

            dr.Hanni = "";

            dr.HyojunKakaku = 0;

            dr.ShiireKakaku = 0;

            dr.TyokusouSaki = "";

            dr.PermissionStart = DateTime.Now.ToShortDateString();

            dr.RightEnd = DateTime.Now.ToShortDateString();

            dr.JacketPrint = false;

            dr.Henkyaku = false;

            return dr;

        }

        protected void RadMakerRyaku_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.GetMaker(sender, e);
        }
    }
}
