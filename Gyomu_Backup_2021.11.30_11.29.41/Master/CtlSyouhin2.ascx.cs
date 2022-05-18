using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class CtlSyouhin2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        internal void Create(string strKeys, int nData)
        {
            //チェックした商品情報を持ってくる。
            string sSyouhin = "";
            string sMedia = "";
            string sShiire = "";

            for (int i = 0; i < nData; i++)
            {
                string sData = strKeys.Split(',')[i];

                if (i != 0)
                {
                    sSyouhin = sSyouhin + "," + sData.Split('/')[0];
                    sShiire = sShiire + "," + sData.Split('/')[1];
                    sMedia = sMedia + "," + sData.Split('/')[2];
                }
                else
                {

                    sSyouhin = sData.Split('/')[0];
                    sShiire = sData.Split('/')[1];
                    sMedia = sData.Split('/')[2];
                }
            }

            DataSet1.M_Kakaku_New1DataTable dt =
                ClassMaster.GetM_Kakaku(sSyouhin, sMedia, sShiire, nData, Global.GetConnection());

            DataView dv = new DataView(dt);
            ProductList.DataSource = dv;
            ProductList.DataBind();
        }


        protected void ProductList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataSet1.M_Kakaku_New1Row dr = (e.Item.DataItem as DataRowView).Row as DataSet1.M_Kakaku_New1Row;

                Label procd = e.Item.FindControl("LblProductCode") as Label;
                Label pronm = e.Item.FindControl("LblProductName") as Label;
                Label med = e.Item.FindControl("LblMedia") as Label;
                Label shicd = e.Item.FindControl("LblShiireCode") as Label;
                Label shinm = e.Item.FindControl("LblShiireName") as Label;


                procd.Text = dr.SyouhinCode;
                pronm.Text = dr.SyouhinMei;
                med.Text = dr.Media;
                if(!dr.IsShiireCodeNull())
                { shicd.Text = dr.ShiireCode; }
                if(!dr.IsShiireNameNull())
                { shinm.Text = dr.ShiireName; }

            }
        }

        internal void Create2()
        {
            DataSet1.T_productDataTable dt = Class1.GetKariProduct(Global.GetConnection());
            ProductList.DataSource = dt;
            ProductList.DataBind();
        }
    }
}