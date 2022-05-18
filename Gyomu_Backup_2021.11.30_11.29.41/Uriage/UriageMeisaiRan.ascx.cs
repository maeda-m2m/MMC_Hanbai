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
using System.Windows.Forms;

namespace Gyomu.Uriage
{
    public partial class UriageMeisaiRan : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            err.Text = "";
        }

        protected void SerchProduct_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            string cate = category.Value;
            if (cate == "")
            { err.Text = "カテゴリーを選択して下さい。"; }
            ListSet.SettingProduct(sender, e, cate);
            procd.Value = SerchProduct.SelectedValue;

        }

        protected void SerchProduct_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string s = SerchProduct.SelectedValue;
            if (s != "-1")
            {
                ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
                GetKensakuProduct(p);
            }
            else
            {
                err.Text = "商品名を選択してください。";
            }

        }

        private void GetKensakuProduct(ClassKensaku.KensakuParam p)
        {
            try
            {
                string cate = "";
                string syouhinCode = "";
                string media = "";
                string hanni = "";
                string str = SerchProduct.SelectedValue;
                string[] arr = str.Split('/');
                syouhinCode += arr[0];
                media += arr[1];
                hanni += arr[2];

                cate = category.Value;

                DataSet1.M_Kakaku_2DataTable dt = ClassKensaku.Getproduct6(syouhinCode, cate, media, hanni, Global.GetConnection());
                for (int j = 0; j < dt.Count; j++)
                {
                    DataSet1.M_Kakaku_2Row dr = dt.Rows[j] as DataSet1.M_Kakaku_2Row;
                    LblHanni.Text = dr.Hanni;
                    Baitai.Text = media;
                    //HyoujyunTanka.Text = dr.HyojunKakaku.ToString("0,0");
                    string hk = dr.HyoujunKakaku.ToString();
                    ht.Value = hk;
                    LblProduct.Text = dr.Makernumber;



                    SerchProduct.Text = dr.SyouhinMei;

                    string kakakuhyoujyun = ht.Value;
                    int h = int.Parse(ht.Value);
                    int k = int.Parse(Kakeri.Text);
                    double t = h * k / 100;
                    double tt = Math.Floor(t);
                    CtlKeisan(kakakuhyoujyun, tt);
                }
            }
            catch 
            {
                err.Text = "カテゴリーと得意先を選択してください。";
            }
        }

        private void CtlKeisan(string kakakuhyoujyun, double t)
        {
            if (zeiku.Text == "税込")
            {
                string ht = kakakuhyoujyun;
                //標準単価
                int x = int.Parse(ht);
                double xx = x * 1.1;
                HyoujyunTanka.Text = xx.ToString("0,0");
                //標準金額
                double go = xx * int.Parse(Suryo.Text);
                Kingaku.Text = go.ToString("0,0");
                //単価
                double m = t * 1.1;
                Tanka.Text = m.ToString("0,0");
                //数量も計算した売上・仕入金額
                double g = m * int.Parse(Suryo.Text);
                string tk = Tanka.Text.Replace(",", "");
                int d = int.Parse(tk) * int.Parse(Suryo.Text);
                ug.Value = d.ToString();
                zeiht.Value = g.ToString();
                Uriage.Text = g.ToString("0,0");
                zeikgk.Value = g.ToString();
            }
            else
            {
                string ht = kakakuhyoujyun;
                int x = int.Parse(ht);
                Tanka.Text = t.ToString("0,0");
                HyoujyunTanka.Text = x.ToString("0,0");
                double g = x * int.Parse(Suryo.Text);
                int d = int.Parse(t.ToString()) * int.Parse(Suryo.Text);
                Kingaku.Text = g.ToString("0,0");
                Uriage.Text = d.ToString("0,0");
                kgk.Value = g.ToString();
            }
        }

        protected void ShiyouShisetsu_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetFacility(sender, e);
        }

        protected void ShiyouShisetsu_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string fac = ShiyouShisetsu.SelectedValue;
            FacilityCode.Value = fac;
            DataSet1.M_Facility_NewDataTable dt = Class1.FacilityDT(fac, Global.GetConnection());
            for (int i = 0; i < dt.Count; i++)
            {
                FacilityAddress.Value = dt[i].Address1 + dt[i].Address2;
            }
        }
    }
}