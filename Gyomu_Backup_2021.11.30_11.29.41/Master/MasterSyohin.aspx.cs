using System;
using DLL;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using System.IO;
using System.Text;

namespace Gyomu.Master
{
    public partial class MasterSyohin : System.Web.UI.Page
    {
        const int LIST_ID = 80;
        public static string commandtext;

        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = "";
            if (!this.IsPostBack)
            {
                Create3("", 0);
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
        }
        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            for (int i = 0; i < this.L.Controls.Count; i++)
                this.L.Controls[i].Visible = bShow;
        }

        private void UserView_DataBound(UserViewManager.UserViewEventArgs e)
        {
            if (e.ItemType != UserViewManager.EnumItemType.DataRow) return;

        }

        protected void D_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (!(Telerik.Web.UI.GridItemType.Item == e.Item.ItemType ||
                    Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType))
                return;

            Button btnEdit = e.Item.FindControl("E") as Button;
            string strCode = Convert.ToString((e.Item.DataItem as DataRowView).Row["SyouhinCode"]);

            string strSyouhinMei = Convert.ToString((e.Item.DataItem as DataRowView).Row["SyouhinMei"]);

            Button E = e.Item.FindControl("E") as Button;
            E.Attributes["onclick"] = string.Format("CntRow('{0}')", strCode + "/" + strSyouhinMei);

            HtmlInputCheckBox ChkRow = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;
            ChkRow.Attributes["Checked"] = string.Format("CntRow('{0}')", strCode + "/" + strSyouhinMei);
            ChkRow.Value = strCode + "/" + strSyouhinMei;
        }


        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterSyohinAdd.aspx");
        }

        string strKeys = "";

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterSyohin.aspx");
        }

        bool _bD_PageSizeChanged = false;


        protected void E_Click(object sender, EventArgs e)
        {
            string UserId = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
            Syohin.Create(UserId);
        }

        //protected void BtnToroku_Click(object sender, EventArgs e)
        //{
        //    bool bo = Syohin.Toroku();

        //    if (bo == true)
        //    {
        //        Craete();

        //        lblMsg.Text = "登録しました";

        //        Master.Style["display"] = "";
        //        Touroku.Style["display"] = "none";
        //        Touroku2.Style["display"] = "none";
        //    }
        //    else
        //    {
        //        lblMsg.Text = "エラーが発生しました。";

        //        Master.Style["display"] = "";
        //        Touroku.Style["display"] = "none";
        //        Touroku2.Style["display"] = "none";
        //    }
        //}

        protected void D_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string co = count.Value;
        }


        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    Stream s = FileUpload1.FileContent;
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);
                    System.IO.StreamReader check = new StreamReader(s, enc);
                    string strCheck = check.ReadLine();
                    if (strCheck == null)
                    {
                        return;
                    }
                    bool bTab = (strCheck.Split('\t').Length > strCheck.Split(',').Length);
                    int nLine = 0;
                    int RowCount = 0;
                    if (bTab)
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split('\t');
                            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
                            DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
                            dr.ItemArray = mData;
                            dt.AddM_Kakaku_2Row(dr);
                            Class1.UpdateCSVkakaku(dt, Global.GetConnection());
                        }
                    }
                    else
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split(',');
                            DataSet1.M_Kakaku_2DataTable dt = new DataSet1.M_Kakaku_2DataTable();
                            DataSet1.M_Kakaku_2Row dr = dt.NewM_Kakaku_2Row();
                            dr.ItemArray = mData;
                            dt.AddM_Kakaku_2Row(dr);
                            Class1.UpdateCSVkakaku(dt, Global.GetConnection());
                        }
                    }

                }
                catch (Exception ex)
                {
                    switch (ex.Message)
                    {

                    }
                }
                ClassMaster.DeleteProductList(Global.GetConnection());
                DataMaster.V_ProductListDataTable dtN = ClassMaster.GetList(Global.GetConnection());
                ClassMaster.CreateProductList(dtN, Global.GetConnection());
            }
            else
            {
                lblMsg.Text = "ファイルが選択されていません";
            }
            CreatePage();
        }

        protected void BtnCsvDownload_Click(object sender, EventArgs e)
        {
            string strWhere = "";
            UserViewManager.UserView v = SessionManager.User.GetUserView(81);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());
            strWhere = CreateCommandText(da.SelectCommand.CommandText);
            if (strWhere != "")
            {
                DataSet1.M_Kakaku_2DataTable dt = Class1.GetMasterCSV(da.SelectCommand.CommandText + " where " + strWhere, Global.GetConnection());
                if (dt.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("商品マスタDL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                    Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                    this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
            this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
            Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
                }
            }
            else
            {
                DataSet1.M_Kakaku_2DataTable dt2 = Class1.GetMasterCSV(da.SelectCommand.CommandText, Global.GetConnection());
                if (dt2.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("商品マスタDL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                    Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                    this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
            this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
            Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt2, fmt, null)))));
                }

            }
        }

        private string CreateCommandText(string commandText)
        {
            //CSVダウンロードする際はM_Kakaku_2のテーブルを使用する

            string koumoku = "";
            koumoku = F2.GetWhere();
            return koumoku;
        }

        protected void BtnSerch2_Click(object sender, EventArgs e)
        {
            CreatePage();
        }

        private void CreatePage()
        {
            string Koumoku = "";
            Koumoku = F2.GetKoumoku();
            Create3(Koumoku, RGproductlist.MasterTableView.CurrentPageIndex);
        }

        private void Create3(string koumoku, int page)
        {
            lblMsg.Text = "";
            L.Visible = true;

            SqlDataAdapter da = new SqlDataAdapter("SELECT SyouhinCode, SyouhinMei, 公共図書館, 学校図書館, 防衛省, その他図書館, ホテル, レジャーホテル, バス, 船舶, 上映会, カフェ, 健康ランド, 福祉施設, キッズ・BGV, その他, VOD, VOD配信, DOD, DEX FROM T_ProductList", Global.GetConnection());
            string cmm = "";
            if (!string.IsNullOrEmpty(koumoku))
            {
                cmm = da.SelectCommand.CommandText + " where " + koumoku;
            }
            else
            {
                cmm = da.SelectCommand.CommandText;
            }
            cmm += " ORDER BY SyouhinCode";
            DataSet1.T_ProductListDataTable dt = ClassMaster.GetSyouhinData(cmm, Global.GetConnection());
            //commandtext = cmm;

            int nRecCount = 0;
            nRecCount = dt.Rows.Count;

            if (0 == nRecCount)
            {
                L.Visible = false;
                lblMsg.Text = "データがありません。";
                return;
            }
            if (0 == nRecCount)
            {
                L.Visible = false;
                RGproductlist.CurrentPageIndex = 0;
                lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                return;
            }
            RGproductlist.CurrentPageIndex = page;
            string strColumnAry = "";
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (strColumnAry == "")
                {
                    strColumnAry = dt.Columns[c].ColumnName;
                }
                else
                {
                    strColumnAry += "," + dt.Columns[c].ColumnName;
                }
            }
            this.F2.KoumokuBind(strColumnAry);

            RGproductlist.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
            RGproductlist.DataSource = dt;
            RGproductlist.DataBind();
            this.ShowList(true);
        }

        protected void RGproductlist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                DataSet1.T_ProductListRow dr = (e.Item.DataItem as DataRowView).Row as DataSet1.T_ProductListRow;
                Label LblSyouhinCode = e.Item.FindControl("LblSyouhinCode") as Label;
                Label LblSyouhinMei = e.Item.FindControl("LblSyouhinMei") as Label;
                Label LblKoukyou = e.Item.FindControl("LblKoukyou") as Label;
                Label LblGakkou = e.Item.FindControl("LblGakkou") as Label;
                Label LblBouei = e.Item.FindControl("LblBouei") as Label;
                Label LblSonotaTosyokan = e.Item.FindControl("LblSonotaTosyokan") as Label;
                Label LblHotel = e.Item.FindControl("LblHotel") as Label;
                Label Lblrejahotel = e.Item.FindControl("Lblrejahotel") as Label;
                Label LblBus = e.Item.FindControl("LblBus") as Label;
                Label LblSenpaku = e.Item.FindControl("LblSenpaku") as Label;
                Label LblJouei = e.Item.FindControl("LblJouei") as Label;
                Label Lblcafe = e.Item.FindControl("Lblcafe") as Label;
                Label LblKenkou = e.Item.FindControl("LblKenkou") as Label;
                Label LblFukushi = e.Item.FindControl("LblFukushi") as Label;
                Label Lblkiz = e.Item.FindControl("Lblkiz") as Label;
                Label LblSonota = e.Item.FindControl("LblSonota") as Label;
                Label LblVOD = e.Item.FindControl("LblVOD") as Label;
                Label LblVODhaishin = e.Item.FindControl("LblVODhaishin") as Label;
                Label LblDOD = e.Item.FindControl("LblDOD") as Label;
                Label LblDEX = e.Item.FindControl("LblDEX") as Label;
                Button BtnSyousai = e.Item.FindControl("BtnSyousai") as Button;
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
                string maru = "〇";
                LblSyouhinCode.Text = dr.SyouhinCode;
                if (10 < dr.SyouhinMei.Length)
                {
                    LblSyouhinMei.Text = dr.SyouhinMei.Substring(0, 9) + "...";
                }
                else
                {
                    LblSyouhinMei.Text = dr.SyouhinMei;
                }
                if (!dr.Is公共図書館Null())
                {
                    if (dr.公共図書館 == "1")
                    {
                        LblKoukyou.Text = maru;
                    }
                }
                if (!dr.Is学校図書館Null())
                {
                    if (dr.学校図書館 == "1")
                    {
                        LblGakkou.Text = maru;
                    }
                }
                if (!dr.Is防衛省Null())
                {
                    if (dr.防衛省 == "1")
                    {
                        LblBouei.Text = maru;
                    }
                }
                if (!dr.Isその他図書館Null())
                {
                    if (dr.その他図書館 == "1")
                    {
                        LblSonotaTosyokan.Text = maru;
                    }
                }
                if (!dr.IsホテルNull())
                {
                    if (dr.ホテル == "1")
                    {
                        LblHotel.Text = maru;
                    }
                }
                if (!dr.IsレジャーホテルNull())
                {
                    if (dr.レジャーホテル == "1")
                    {
                        Lblrejahotel.Text = maru;
                    }
                }
                if (!dr.IsバスNull())
                {
                    if (dr.バス == "1")
                    {
                        LblBus.Text = maru;
                    }
                }
                if (!dr.Is船舶Null())
                {
                    if (dr.船舶 == "1")
                    {
                        LblSenpaku.Text = maru;
                    }
                }
                if (!dr.Is上映会Null())
                {
                    if (dr.上映会 == "1")
                    {
                        LblJouei.Text = maru;
                    }
                }
                if (!dr.IsカフェNull())
                {
                    if (dr.カフェ == "1")
                    {
                        Lblcafe.Text = maru;
                    }
                }
                if (!dr.Is健康ランドNull())
                {
                    if (dr.健康ランド == "1")
                    {
                        LblKenkou.Text = maru;
                    }
                }
                if (!dr.Is福祉施設Null())
                {
                    if (dr.福祉施設 == "1")
                    {
                        LblFukushi.Text = maru;
                    }
                }
                if (!dr.Is_キッズ_BGVNull())
                {
                    if (dr._キッズ_BGV == "1")
                    {
                        Lblkiz.Text = maru;
                    }
                }
                if (!dr.Isその他Null())
                {
                    if (dr.その他 == "1")
                    {
                        LblSonota.Text = maru;
                    }
                }
                if (!dr.IsVODNull())
                {
                    if (dr.VOD == "1")
                    {
                        LblVOD.Text = maru;
                    }
                }
                if (!dr.IsVOD配信Null())
                {
                    if (dr.VOD配信 == "1")
                    {
                        LblVODhaishin.Text = maru;
                    }
                }
                if (!dr.IsDODNull())
                {
                    if (dr.DOD == "1")
                    {
                        LblDOD.Text = maru;
                    }
                }
                if (!dr.IsDEXNull())
                {
                    if (dr.DEX == "1")
                    {
                        LblDEX.Text = maru;
                    }
                }
                HidSyousai.Value = dr.SyouhinCode + "/" + dr.SyouhinMei;
            }
        }

        protected void RGproductlist_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "BtnSyousai")
            {
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
                string UserId = HidSyousai.Value;
                Master.Style["display"] = "none";
                Touroku.Style["display"] = "";
                Syohin.Create(UserId);
            }
        }

        protected void RGproductlist_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RGproductlist_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RGproductlist.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreatePage();
        }

        protected void RGproductlist_PreRender(object sender, EventArgs e)
        {

        }
    }
}