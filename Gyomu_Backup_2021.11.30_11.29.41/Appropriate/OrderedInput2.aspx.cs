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

namespace Gyomu.Order
{
    public partial class OrderedInput2 : System.Web.UI.Page
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
            try
            {
                string url = Request.RawUrl;
                string[] strAry = url.Split('=');
                string[] strKey = strAry[1].Split(',');

                string strOrderedNo = strKey[0];
                string strMaker = strKey[1];
                string strHanni = strKey[2];
                string strMedia = strKey[3];

                if (strHanni.Trim() != "")
                {
                    DataMaster.M_HanniRow dr = ClassOrdered.GetHanni(strKey[2], Global.GetConnection());
                    strHanni = dr.Hanni;
                }

                DataSet1.T_NyukaLogDataTable dt = ClassOrdered.GetLog(strOrderedNo, strMaker, strHanni, strMedia, Global.GetConnection());

                DGlog.DataSource = dt;
                DGlog.DataBind();
            }
            catch
            {
                Err.Text = "データを取得できませんでした。";
            }
        }

        protected void test_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        protected void test_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int a = e.Item.ItemIndex;
        }

        protected void test_Command(object sender, CommandEventArgs e)
        {
            string a = e.CommandName;
        }

        protected void DGlog_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    DataSet1.T_NyukaLogRow dr = (e.Item.DataItem as DataRowView).Row as DataSet1.T_NyukaLogRow;

                    Label LblZanSuryo = e.Item.FindControl("LblZanSuryo") as Label;
                    Label LblNyuko = e.Item.FindControl("LblNyuko") as Label;
                    Label LblStaff = e.Item.FindControl("LblStaff") as Label;
                    Label LblNyukaDate = e.Item.FindControl("LblNyukaDate") as Label;
                    Label LblHatyuDate = e.Item.FindControl("LblHatyuDate") as Label;

                    string staff = dr.StaffName;
                    DataMaster.M_TantoDataTable dt = ClassMaster.GetM_Tanto2(staff, Global.GetConnection());

                    LblZanSuryo.Text = dr.Zansu.Trim();
                    LblNyuko.Text = dr.NyukaSuryo.Trim();
                    LblStaff.Text = dt[0].UserName;
                    LblNyukaDate.Text = dr.NyukaDate.ToShortDateString();
                    LblHatyuDate.Text = dr.HatyuDate.ToShortDateString();
                    OrderedNo.Text = dr.OrderedNo;
                    MakerNo.Text = dr.MakerNo;
                    LblProductName.Text = dr.ProductName;
                    LblSuryo.Text = dr.OrderedSuryo.Trim();
                }
            }
            catch
            {
                Err.Text = "欠けているデータが存在します。";
            }
        }

        protected void DGlog_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }
    }
}