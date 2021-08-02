using Core.Sql;
using DLL;
using Gyomu.Mitumori.Syosai;
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
    public partial class CtlOrderedMeisai : System.Web.UI.UserControl
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            Err.Text = "";
        }

      

        protected void RcbMaker_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

            string cate = HidCate.Value;
            if (cate != "")
            {
                if (e.Text.Trim() != "")
                {
                    ListSet.SetProductname(sender, e, cate);
                }
            }
            else
            {
                Err.Text = "カテゴリーコードが存在していません。";
            }
        }

        protected void RcbMaker_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string cate = HidCate.Value;
                string rcbText = RcbMaker.Text;
                string[] str1 = rcbText.Split('/');
                string rcbValue = RcbMaker.SelectedValue;
                string[] str2 = rcbValue.Split('/');

                DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.Getproduct6(str2[0], cate, str2[1], str1[2], Global.GetConnection());

                for (int j = 0; j < dt.Count; j++)
                {
                    DataSet1.M_Kakaku_2Row dr = dt.Rows[j] as DataSet1.M_Kakaku_2Row;
                    LblHanni.Text = dr.Hanni;
                    LblProductName.Text = dr.Makernumber;
                    RcbMaker.Text = dr.SyouhinMei;
                    LblMedia.Text = dr.Media;
                    LblSuryo.Text = "1";
                    TbxShiireTanka.Text = dr.ShiireKakaku.ToString("0,0");
                    int shiire = dr.ShiireKakaku;
                    int su = int.Parse(LblSuryo.Text);
                    TbxShiireKingaku.Text = (shiire * su).ToString("0,0");
                    
                }
            }
            catch(Exception ex)
            {
                Err.Text = "商品名を選択してください";
            }
            }
        
    }
}