using System;
using DLL;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using System.IO;


namespace Gyomu.Master
{
    public partial class MasterShiire : System.Web.UI.Page
    {
        const int LIST_ID = 100;
        public static int intFieldNo = 110;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";

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
                L.Visible = true;

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM M_Shiire_New", Global.GetConnection());
                string cmm = "";
                if (!string.IsNullOrEmpty(koumoku))
                {
                    cmm = da.SelectCommand.CommandText + " where " + koumoku;
                }
                else
                {
                    cmm = da.SelectCommand.CommandText;
                }
                cmm += " ORDER BY ShiireCode";
                DataMaster.M_Shiire_NewDataTable dt = ClassMaster.GetShiireMaster(cmm, Global.GetConnection());
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
                    RGShiireList.CurrentPageIndex = 0;
                    lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                    return;
                }
                RGShiireList.CurrentPageIndex = page;
                string strColumnAry = "";
                //for (int c = 0; c < dt.Columns.Count; c++)
                //{
                //    if (strColumnAry == "")
                //    {
                //        strColumnAry = dt.Columns[c].ColumnName;
                //    }
                //    else
                //    {
                //        strColumnAry += "," + dt.Columns[c].ColumnName;
                //    }
                //}
                //this.Filter.KoumokuBind(strColumnAry);
                this.Filter.KoumokuBind2(intFieldNo);

                RGShiireList.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
                RGShiireList.DataSource = dt;
                RGShiireList.DataBind();
                this.ShowList(true);
            }
            catch (Exception ex)
            {

            }
        }

        private void ShowMsg(string strMsg, bool bErrorMsg)
        {
            this.lblMsg.ForeColor = (bErrorMsg) ? Color.Red : Color.Blue;
            this.lblMsg.Text = strMsg;
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

        protected void E_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック動作
            string ShiireCode = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

            Shiire.Create(ShiireCode);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterShiire.aspx");
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Shiire.Clear();
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Shiire.Add();

            if (bo)
            {
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";

                Craete("", 0);

                lblMsg.Text = "登録しました";
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
            Craete(Koumoku, RGShiireList.MasterTableView.CurrentPageIndex);
        }

        protected void BtnCSVdownload_Click(object sender, EventArgs e)//CSVダウンロード
        {
            string strWhere = "";
            UserViewManager.UserView v = SessionManager.User.GetUserView(100);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());
            strWhere = CreateCommandText(da.SelectCommand.CommandText);
            if (strWhere != "")
            {
                DataMaster.M_Shiire_NewDataTable dt = ClassMaster.GetShiireMaster(da.SelectCommand.CommandText + " where " + strWhere, Global.GetConnection());
                if (dt.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("仕入先マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                    Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                    this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
            this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
            Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
                }
            }
            else
            {
                DataMaster.M_Shiire_NewDataTable dt2 = ClassMaster.GetShiireMaster(da.SelectCommand.CommandText, Global.GetConnection());
                if (dt2.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("仕入先マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
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
                            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
                            DataMaster.M_Shiire_NewRow dr = dt.NewM_Shiire_NewRow();
                            dr.ItemArray = mData;
                            dt.AddM_Shiire_NewRow(dr);
                            try
                            {
                                ClassMaster.UpdateCSVshiire(dt, Global.GetConnection());
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message;
                            }
                        }
                    }
                    else
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split(',');
                            DataMaster.M_Shiire_NewDataTable dt = new DataMaster.M_Shiire_NewDataTable();
                            DataMaster.M_Shiire_NewRow dr = dt.NewM_Shiire_NewRow();
                            dr.ItemArray = mData;
                            dt.AddM_Shiire_NewRow(dr);
                            try
                            {
                                ClassMaster.UpdateCSVshiire(dt, Global.GetConnection());
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message;
                            }
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

        protected void RGShiireList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                DataMaster.M_Shiire_NewRow dr = (e.Item.DataItem as DataRowView).Row as DataMaster.M_Shiire_NewRow;
                Label LblShiireCode = e.Item.FindControl("LblShiireCode") as Label;
                Label LblShiireName = e.Item.FindControl("LblShiireName") as Label;
                Label LblShiireAbbreviation = e.Item.FindControl("LblShiireAbbreviation") as Label;
                Label LblShiireAddress1 = e.Item.FindControl("LblShiireAddress1") as Label;
                Label LblShiireAddress2 = e.Item.FindControl("LblShiireAddress2") as Label;
                Label LblShiireTel = e.Item.FindControl("LblShiireTel") as Label;
                Label LblShiireFAX = e.Item.FindControl("LblShiireFAX") as Label;
                Label LblPersonnel = e.Item.FindControl("LblPersonnel") as Label;
                Label LblDeployment = e.Item.FindControl("LblDeployment") as Label;
                Label LblCutoffDate = e.Item.FindControl("LblCutoffDate") as Label;
                Label LblPaymentDate = e.Item.FindControl("LblPaymentDate") as Label;
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
                LblShiireCode.Text = dr.ShiireCode.ToString();
                if (!dr.IsShiireNameNull())
                {
                    if (dr.ShiireName.Length > 10)
                    {
                        LblShiireName.Text = dr.ShiireName.Substring(0, 9) + "...";
                    }
                    else
                    {
                        LblShiireName.Text = dr.ShiireName;
                    }
                }
                if (!dr.IsAbbreviationNull())
                {
                    if (dr.Abbreviation.Length > 15)
                    {
                        LblShiireAbbreviation.Text = dr.Abbreviation.Substring(0, 9) + "...";
                    }
                    else
                    {
                        LblShiireAbbreviation.Text = dr.Abbreviation;
                    }
                }
                if (!dr.IsAddress1Null())
                {
                    if (dr.Address1.Length > 15)
                    {
                        LblShiireAddress1.Text = dr.Address1.Substring(0, 9) + "...";
                    }
                    else
                    {
                        LblShiireAddress1.Text = dr.Address1;
                    }
                }
                if (!dr.IsAddress2Null())
                {
                    if (dr.Address2.Length > 15)
                    {
                        LblShiireAddress2.Text = dr.Address2.Substring(0, 9) + "...";
                    }
                    else
                    {
                        LblShiireAddress2.Text = dr.Address2;
                    }
                }
                if (!dr.IsTellNull())
                {
                    LblShiireTel.Text = dr.Tell;
                }
                if (!dr.IsFaxNull())
                {
                    LblShiireFAX.Text = dr.Fax;
                }
                if (!dr.IsPersonnelNull())
                {
                    LblPersonnel.Text = dr.Personnel;
                }
                if (!dr.IsDeploymentNull())
                {
                    LblDeployment.Text = dr.Deployment;
                }
                if (!dr.IsCutoffDateNull())
                {
                    string cod = "";
                    switch (dr.CutoffDate)
                    {
                        case 05:
                            cod = "5日締め";
                            break;
                        case 10:
                            cod = "10日締め";
                            break;
                        case 15:
                            cod = "15日締め";
                            break;
                        case 20:
                            cod = "20日締め";
                            break;
                        case 25:
                            cod = "25日締め";
                            break;
                        case 99:
                            cod = "月末締め";
                            break;
                        case 00:
                            cod = "随時締め";
                            break;
                    }
                    if (cod != "")
                    {
                        LblCutoffDate.Text = cod;
                    }
                }
                if (!dr.IsPaymentDateNull())
                {
                    string cod = "";
                    switch (dr.PaymentDate)
                    {
                        case 05:
                            cod = "5日締め";
                            break;
                        case 10:
                            cod = "10日締め";
                            break;
                        case 15:
                            cod = "15日締め";
                            break;
                        case 20:
                            cod = "20日締め";
                            break;
                        case 25:
                            cod = "25日締め";
                            break;
                        case 99:
                            cod = "月末締め";
                            break;
                        case 00:
                            cod = "随時締め";
                            break;
                    }
                    if (cod != "")
                    {
                        LblPaymentDate.Text = cod;
                    }
                }

                HidSyousai.Value = dr.ShiireCode.ToString();
            }
        }

        protected void RGShiireList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
            if (HidSyousai != null)
            {
                string UserId = HidSyousai.Value;
                if (e.CommandName.Equals("BtnSyousai"))
                {
                    Master.Style["display"] = "none";
                    Touroku.Style["display"] = "";
                    Shiire.Create(UserId);
                }
                if (e.CommandName.Equals("Delete"))
                {
                    try
                    {
                        ClassMaster.DeleteShiiresaki(UserId, Global.GetConnection());
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                    }
                    finally
                    {
                        CreatePage();
                    }

                }
            }
        }

        protected void RGShiireList_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RGShiireList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RGShiireList.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreatePage();
        }

        protected void RGShiireList_PreRender(object sender, EventArgs e)
        {

        }

        protected void BtnSyousai_Click(object sender, EventArgs e)
        {

        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

    }
}