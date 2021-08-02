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
using System.IO;
using ClosedXML.Excel;
using System.Net;
using System.ComponentModel;

namespace Gyomu.Master
{
    public partial class MasterSyohinAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string pc = TbxCode.Text;
            string pn = TbxSyohinMei.Text;
            DataMaster.M_ProductDataTable dt = ClassMaster.GetProduct2(pc, Global.GetConnection());
            if (dt.Count >= 1)
            {
                Err.Text = "この商品コードは既に登録されています。";
            }
            DataMaster.M_ProductDataTable dd = ClassMaster.GetProduct3(pn, Global.GetConnection());
            if (dd.Count >= 1)
            {
                Err.Text = "この商品名は既に登録されています。";
            }
            DataMaster.M_ProductDataTable dx = ClassMaster.GetProduct(Global.GetConnection());
            DataMaster.M_ProductRow dr = dx.NewM_ProductRow();

            try
            {
                if (TbxCode.Text != "")
                { dr.SyouhinCode = TbxCode.Text; }
                if (TbxSyohinMei.Text != "")
                { dr.SyouhinMei = TbxSyohinMei.Text; }
                if (RadBaitai.Text != "")
                { dr.Media = RadBaitai.Text; }
                if (TbxTosyoCode.Text != "")
                { dr.Hanni = TbxTosyoCode.Text; }
                if (Souko.Text != "")
                { dr.Warehouse = Souko.Text; }
                if (RadShiire.Text != "")
                { dr.ShiireMei = RadShiire.Text; }
            }
            catch (Exception ex)
            { ERRMESSAGE(); }


        }

        private void ERRMESSAGE()
        {
            string Errr = "";
            if (TbxCode.Text == "")
            { Errr += "商品コードを記載して下さい。"; }
            if (TbxSyohinMei.Text == "")
            { Errr += "商品名を記載して下さい。"; }
            if (RadBaitai.Text == "")
            { Errr += "媒体名を選択して下さい。"; }
            if (TbxTosyoCode.Text == "")
            { Errr += "範囲を記載して下さい。"; }
            if (Souko.Text == "")
            { Errr += "倉庫コードを記載して下さい。"; }
            if (RadShiire.Text == "")
            { Errr += "仕入先名を選択して下さい。"; }
            Err.Text = Errr;
        }

        protected void RadShiire_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            { ListSet.SetShiireSaki(sender, e); }
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            if (null == this.File1.PostedFile || 0 == this.File1.PostedFile.ContentLength)
            {
                Err.Text = "ファイルを指定してください。";
                return;
            }

            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);
            System.IO.Stream eam = File1.PostedFile.InputStream;

            System.IO.StreamReader tabReader = null;
            Core.IO.CSVReader csvReader = null;
            System.IO.StreamReader check = new System.IO.StreamReader(eam, enc);
            string strCheck = check.ReadLine();

            bool tab = (strCheck.Split('\t').Length > strCheck.Split(',').Length);

            eam.Position = 0;
            if (tab)
            {
                tabReader = new System.IO.StreamReader(eam, enc);
            }
            else
            {
                csvReader = new Core.IO.CSVReader(eam, enc);
            }





            if (null == strCheck)
            {
                Err.Text = "データがありません。";
                return;
            }
            int[] DataMaxLength = new int[]
            {
                20, //1:商品コード
                150, //2:商品名
                30, //３：メーカー品番
                10, //４：メディア
                10, //５：範囲
                10, //６：倉庫
                20, //７：仕入コード
                50, // 8:仕入先名
                10, //9：利用状態
                10, //１０：カテゴリーコード
                10, // １１：カテゴリー名
                20, //１２：直送先名
                10, //１３：標準価格
                10, //１４：期限開始
                10, //１５：期限終了
                10, //１６：ジャケット
                10, //１７：プリント
                10, //１８：返品
                10, //１９：作成者
                10, //２０：作成日
                
            };

            string[] strFieldMei = new string[]
            {
                "商品コード","商品名", "メーカー品番", "メディア", "範囲", "倉庫", "仕入コード", "仕入先名", "利用状態", "カテゴリーコード",
                "カテゴリー名", "直送先名", "標準価格", "期限開始", "期限終了", "ジャケット", "プリント", "返品", "作成者","作成日",
            };

            int nLine = 0;
            int RowCount = 0;
            string[] str = null;
            string SyouhinCode = "";
            string SyouhinMei = "";
            string MekarHinban = "";
            string Media = "";
            string Range = "";
            string WareHouse = "";
            string ShiireCode = "";
            string ShiireMei = "";
            string HyoujyunKakaku = "";
            string PermissionStart = "";
            string RightEnd = "";




            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();

            while (true)
            {
                RowCount++;
                try
                {
                    nLine++;
                    string strLine = null;
                    string[] strArray = null;
                    if (null != tabReader)
                    {
                        strLine = tabReader.ReadLine();
                        if (null == strLine)
                        {
                            break;
                        }
                        strArray = strLine.Split('\t');
                    }
                    else
                    {
                        strArray = csvReader.GetCSVLine(ref strLine);
                        if (string.IsNullOrEmpty(strLine))
                        {
                            break;
                        }
                        if (null == strArray || 0 == strArray.Length)
                        {
                            break;
                        }
                    }
                    str = new string[32];

                    for (int i = 0; i < 32; i++)
                    { str[i] = ""; }

                    for (int i = 0; i < strArray.Length; i++)
                    { str[i] = strArray[i]; }
                    DataMitumori.T_RowRow dr = dt.NewT_RowRow();


                    if (strArray[0] != "")
                    { dr.SyouhinCode = strArray[0]; }
                    if (strArray[1] != "")
                    { dr.SyouhinMei = strArray[1]; }
                    if (strArray[2] != "")
                    { dr.MekarHinban = strArray[2]; }
                    if (strArray[3] != "")
                    { dr.Media = strArray[3]; }
                    if (strArray[4] != "")
                    { dr.Range = strArray[4]; }
                    if (strArray[5] != "")
                    { dr.WareHouse = strArray[5]; }
                    if (strArray[6] != "")
                    { dr.ShiiresakiCode = strArray[6]; }
                    if (strArray[7] != "")
                    { dr.ShiiresakiMei = strArray[7]; }
                    if (strArray[8] != "")
                    { dr.HyojunKakaku = strArray[8]; }
                    if (strArray[9] != "")
                    { dr.PermissionStart = strArray[9]; }
                    if (strArray[10] != "")
                    { dr.PermissionEnd = strArray[10]; }

                    dt.AddT_RowRow(dr);


                }
                catch (Exception ex)
                {

                }

            }
            D.DataSource = dt;
            D.DataBind();

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

                DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                Label ProducutCode = e.Item.FindControl("LblSyouhinCode") as Label;
                Label ProductName = e.Item.FindControl("LblSyouhinMei") as Label;
                Label MakerNo = e.Item.FindControl("LblMakerHinban") as Label;
                Label Media = e.Item.FindControl("LblMedia") as Label;
                Label Range = e.Item.FindControl("LblRange") as Label;
                Label ShiiiresakiCode = e.Item.FindControl("LblShiiresakiCode") as Label;
                Label HyojunKakaku = e.Item.FindControl("LblHyojunKakaku") as Label;
                Label PermissionStart = e.Item.FindControl("LblPermissionStart") as Label;
                Label PermissionEnd = e.Item.FindControl("LblPermissionEnd") as Label;
                Label WareHouse = e.Item.FindControl("LblWarehouse") as Label;
                Label ShiiresakiMei = e.Item.FindControl("LblShiireMei") as Label;

                if (!dr.IsSyouhinCodeNull())
                { ProducutCode.Text = dr.SyouhinCode; }
                if (!dr.IsSyouhinMeiNull())
                { ProductName.Text = dr.SyouhinMei; }
                if (!dr.IsMekarHinbanNull())
                { MakerNo.Text = dr.MekarHinban; }
                if (!dr.IsMediaNull())
                { Media.Text = dr.Media; }
                if (!dr.IsRangeNull())
                { Range.Text = dr.Range; }
                if (!dr.IsShiiresakiCodeNull())
                { ShiiiresakiCode.Text = dr.ShiiresakiCode; }
                if (!dr.IsShiiresakiMeiNull())
                { ShiiresakiMei.Text = dr.ShiiresakiMei; }
                if (!dr.IsHyojunKakakuNull())
                { HyojunKakaku.Text = dr.HyojunKakaku.ToString(); }
                if (!dr.IsPermissionStartNull())
                { PermissionStart.Text = dr.PermissionStart; }
                if (!dr.IsPermissionEndNull())
                { PermissionEnd.Text = dr.PermissionEnd; }
                if (!dr.IsWareHouseNull())
                { WareHouse.Text = dr.WareHouse; }
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
                        Err.Text = "この商品コード(" + p + ")は既に登録されています。";
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
                Message.Text = "登録しました。";
            }
        }


    }
}