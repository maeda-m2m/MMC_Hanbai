using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using DLL;
using System.IO;

namespace Gyomu.Master
{
    public partial class MasterBumon : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;

        const int LIST_ID = 70;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                Craete("", 0);
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
        }

        private void Craete(string koumoku, int page)
        {
            try
            {
                lblMsg.Text = "";
                L.Visible = true;

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM M_Bumon", Global.GetConnection());
                string cmm = "";
                if (!string.IsNullOrEmpty(koumoku))
                {
                    cmm = da.SelectCommand.CommandText + " where " + koumoku;
                }
                else
                {
                    cmm = da.SelectCommand.CommandText;
                }
                cmm += " ORDER BY BumonKubun";
                DataMaster.M_BumonDataTable dt = ClassMaster.GetBumonMaster(cmm, Global.GetConnection());
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
                    RGBumon.CurrentPageIndex = 0;
                    lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                    return;
                }
                RGBumon.CurrentPageIndex = page;
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
                this.Filter.KoumokuBind(strColumnAry);

                RGBumon.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
                RGBumon.DataSource = dt;
                RGBumon.DataBind();
                this.ShowList(true);
            }
            catch (Exception ex)
            {

            }
        }

        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            for (int i = 0; i < this.L.Controls.Count; i++)
                this.L.Controls[i].Visible = bShow;
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            //前の登録データが残るので一旦clearする
            Bumon.Clear();
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
        }


        private void UserView_DataBound(UserViewManager.UserViewEventArgs e)
        {
            if (e.ItemType != UserViewManager.EnumItemType.DataRow) return;

        }

        protected void E_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック動作
            string UserId = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

            Bumon.Create(UserId);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterBumon.aspx");
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Bumon.Toroku();

            if (bo == true)
            {
                Craete("", 0);

                lblMsg.Text = "登録しました";

                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
            else
            {
                Master.Style["display"] = "none";
                Touroku.Style["display"] = "";
            }
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            CreatePage();
        }

        private void CreatePage()
        {
            string Koumoku = "";
            Koumoku = Filter.GetKoumoku();
            Craete(Koumoku, RGBumon.MasterTableView.CurrentPageIndex);
        }

        protected void RGBumon_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                DataMaster.M_BumonRow dr = (e.Item.DataItem as DataRowView).Row as DataMaster.M_BumonRow;
                Label LblBumonKubun = e.Item.FindControl("LblBumonKubun") as Label;
                Label LblBusyo = e.Item.FindControl("LblBusyo") as Label;
                Label LblYubin = e.Item.FindControl("LblYubin") as Label;
                Label LblAddress = e.Item.FindControl("LblAddress") as Label;
                Label LblKoushinBi = e.Item.FindControl("LblKoushinBi") as Label;
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;

                LblBumonKubun.Text = dr.BumonKubun.ToString();
                if (!dr.IsBusyoNull())
                {
                    LblBusyo.Text = dr.Busyo;
                }
                if (!dr.IsYubinBangoNull())
                {
                    LblYubin.Text = dr.YubinBango;
                }
                if (!dr.IsBumonJusyo1Null())
                {
                    string ju = dr.BumonJusyo1;
                    if (!dr.IsBumonjusyo2Null())
                    {
                        ju += dr.Bumonjusyo2;
                        if (ju.Length > 20)
                        {
                            LblAddress.Text = ju.Substring(0, 19) + "...";
                        }
                        else
                        {
                            LblAddress.Text = ju;
                        }
                    }
                    else
                    {
                        if (ju.Length > 20)
                        {
                            LblAddress.Text = ju.Substring(0, 19) + "...";
                        }
                        else
                        {
                            LblAddress.Text = ju;
                        }
                    }
                }
                if (!dr.IsKousinBiNull())
                {
                    LblKoushinBi.Text = dr.KousinBi;
                }
                HidSyousai.Value = dr.BumonKubun.ToString();
            }
        }

        protected void RGBumon_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("BtnSyousai"))
            {
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
                string UserId = HidSyousai.Value;
                Master.Style["display"] = "none";
                Touroku.Style["display"] = "";
                Bumon.Create(UserId);
            }

        }

        protected void RGBumon_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RGBumon_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RGBumon.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreatePage();
        }

        protected void BtnCSVdownload_Click(object sender, EventArgs e)
        {
            string strWhere = "";
            UserViewManager.UserView v = SessionManager.User.GetUserView(70);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());
            strWhere = CreateCommandText(da.SelectCommand.CommandText);
            if (strWhere != "")
            {
                DataMaster.M_BumonDataTable dt = ClassMaster.GetBumonMaster(da.SelectCommand.CommandText + " where " + strWhere, Global.GetConnection());
                if (dt.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("担当者マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                    Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                    this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
            this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
            Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
                }
            }
            else
            {
                DataMaster.M_BumonDataTable dt2 = ClassMaster.GetBumonMaster(da.SelectCommand.CommandText, Global.GetConnection());
                if (dt2.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("担当者マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                    Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                    this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
            this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
            Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt2, fmt, null)))));
                }
            }
        }

        private string CreateCommandText(string commandText)
        {
            string koumoku = "";
            koumoku = Filter.GetKoumoku();
            return koumoku;
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                try
                {
                    Stream s = FileUpload.FileContent;
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
                            DataMaster.M_BumonDataTable dt = new DataMaster.M_BumonDataTable();
                            DataMaster.M_BumonRow dr = dt.NewM_BumonRow();
                            dr.ItemArray = mData;
                            dt.AddM_BumonRow(dr);
                            ClassMaster.UpdateCSVbumon(dt, Global.GetConnection());
                        }
                    }
                    else
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split(',');
                            DataMaster.M_BumonDataTable dt = new DataMaster.M_BumonDataTable();
                            DataMaster.M_BumonRow dr = dt.NewM_BumonRow();
                            dr.ItemArray = mData;
                            dt.AddM_BumonRow(dr);
                            ClassMaster.UpdateCSVbumon(dt, Global.GetConnection());
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "CSVアップロードに失敗しました。" + "<br>" + ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    lblMsg.Text = "CSVアップロードに成功しました。";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    CreatePage();
                }
            }

        }

        protected void RGBumon_PreRender(object sender, EventArgs e)
        {

        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}