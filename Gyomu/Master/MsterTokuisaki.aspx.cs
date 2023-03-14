using System;
using DLL;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using System.IO;
using System.Text;


namespace Gyomu.Master
{
    public partial class MsterTokuisaki : System.Web.UI.Page
    {

        const int LIST_ID = 50;
        public static int intFieldNo = 100;
        public static string mail_to = "maeda@m2m-asp.com";
        public static string mail_title = "エラーメール | 得意先マスタ";


        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            if (!this.IsPostBack)
            {
                Create("", 0);
                if (!string.IsNullOrEmpty(SessionManager.TokuisakiCode))
                {
                    Touroku.Style["display"] = "";
                    L.Style["display"] = "none";
                    Masters.Style["display"] = "none";
                    Tokuisaki.Create(SessionManager.TokuisakiCode);
                    SessionManager.TokuisakiCode = "";
                    SessionManager.drTokuisaki = null;
                }
                else
                {
                    Touroku.Style["display"] = "none";
                    L.Style["display"] = "";
                    Masters.Style["display"] = "";
                }
            }
        }

        private void Create(string koumoku, int page)
        {
            try
            {
                L.Visible = true;

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM M_Tokuisaki2", Global.GetConnection());
                string cmm = "";
                if (!string.IsNullOrEmpty(koumoku))
                {
                    cmm = da.SelectCommand.CommandText + " where " + koumoku;
                }
                else
                {
                    cmm = da.SelectCommand.CommandText;
                }
                cmm += " ORDER BY CustomerCode";
                DataSet1.M_Tokuisaki2DataTable dt = Class1.GetTokuisakiMaster(cmm, Global.GetConnection());
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
                    RGTokuisakiList.CurrentPageIndex = 0;
                    lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                    return;
                }
                RGTokuisakiList.CurrentPageIndex = page;
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
                RGTokuisakiList.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
                RGTokuisakiList.DataSource = dt;
                RGTokuisakiList.DataBind();
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

            //e.TableCells["Yuko"].Text = (!string.IsNullOrEmpty(e.DataRow["Yuko"].ToString()) && Convert.ToBoolean(e.DataRow["Yuko"].ToString())) ? "有効" : "無効";

        }

        protected void E_Click(object sender, EventArgs e)
        {
            Touroku.Style["display"] = "";
            L.Style["display"] = "none";
            Masters.Style["display"] = "none";

            //修正ボタンクリック動作
            string UserId = count.Value;
            Tokuisaki.Create(UserId);
        }

        protected void K_Click(object sender, EventArgs e)
        {
            string sValue = count.Value;
            string QueryString = string.Format("Value={0}", sValue);
            this.Response.Redirect("DoukiTokuisaki.aspx?" + "&" + QueryString);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MsterTokuisaki.aspx");
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Tokuisaki.Claer();
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Tokuisaki.Toroku();

            if (bo)
            {
                Create("", 0);
                lblMsg.Text = "登録しました";
                ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | 新規登録", true, Global.GetConnection());

            }
            else
            {
                lblMsg.Text = "登録に失敗しました";
                string body = "得意先マスタ | CSVダウンロード" + "\r\n" + "" + "\r\n" + "" + "\r\n" + "";
                ClassMail.ErrorMail(mail_to, mail_title, body);
                ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | 新規登録", false, Global.GetConnection());
            }
        }

        private void CreatePage()
        {
            string Koumoku = "";
            Koumoku = Filter.GetKoumoku();
            Create(Koumoku, RGTokuisakiList.MasterTableView.CurrentPageIndex);
        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void test_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTanto4(sender, e);
            }
        }

        protected void RadCityCode2_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTanto4(sender, e);
            }
        }

        protected void RcbTanto2_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (e.Text != "")
            {
                ListSet.SetTanto4(sender, e);
            }
        }

        protected void BtnSerch_Click1(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            CreatePage();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Touroku.Style["display"] = "";
            L.Style["display"] = "none";
            Masters.Style["display"] = "none";
        }

        protected void BtnCSVdownload_Click(object sender, EventArgs e)
        {
            string strWhere = "";
            UserViewManager.UserView v = SessionManager.User.GetUserView(50);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());
            strWhere = CreateCommandText(da.SelectCommand.CommandText);
            if (strWhere != "")
            {
                DataSet1.M_Tokuisaki2DataTable dt = Class1.GetTokuisakiMaster(da.SelectCommand.CommandText + " where " + strWhere, Global.GetConnection());
                if (dt.Count > 0)
                {
                    try
                    {
                        string strExt = "csv";
                        string strFileName = ("得意先マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                        Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                        this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
                        ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | CSVダウンロード", true, Global.GetConnection());
                    }
                    catch (Exception ex)
                    {
                        string body = "得意先マスタ | CSVダウンロード" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                        ClassMail.ErrorMail(mail_to, mail_title, body);
                        ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | CSVダウンロード", false, Global.GetConnection());
                    }
                }
            }
            else
            {
                DataSet1.M_Tokuisaki2DataTable dt2 = Class1.GetTokuisakiMaster(da.SelectCommand.CommandText, Global.GetConnection());
                if (dt2.Count > 0)
                {
                    try
                    {
                        string strExt = "csv";
                        string strFileName = ("仕入先マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                        Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                        this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt2, fmt, null)))));
                        ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | CSVダウンロード", true, Global.GetConnection());
                    }
                    catch (Exception ex)
                    {
                        string body = "得意先マスタ | CSVダウンロード" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                        ClassMail.ErrorMail(mail_to, mail_title, body);
                        ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | CSVダウンロード", false, Global.GetConnection());
                    }
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
                            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
                            DataSet1.M_Tokuisaki2Row dr = dt.NewM_Tokuisaki2Row();
                            dr.ItemArray = mData;
                            dt.AddM_Tokuisaki2Row(dr);
                            ClassMaster.UpdateCSVtokuisaki(dt, Global.GetConnection());
                        }
                    }
                    else
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split(',');
                            DataSet1.M_Tokuisaki2DataTable dt = new DataSet1.M_Tokuisaki2DataTable();
                            DataSet1.M_Tokuisaki2Row dr = dt.NewM_Tokuisaki2Row();
                            if (string.IsNullOrEmpty(mData[4]))
                            {
                                mData[4] = "0";
                            }
                            dr.ItemArray = mData;
                            dt.AddM_Tokuisaki2Row(dr);
                            ClassMaster.UpdateCSVtokuisaki(dt, Global.GetConnection());
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "CSVアップロードに失敗しました。" + "<br>" + ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    string body = "得意先マスタ | CSVアップロード" + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source;
                    ClassMail.ErrorMail(mail_to, mail_title, body);
                    ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | CSVダウンロード", false, Global.GetConnection());
                }
                finally
                {
                    lblMsg.Text = "CSVアップロードに成功しました。";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ClassMaster.InsertLog(Page.Title, SessionManager.User.UserID, SessionManager.User.UserName, DateTime.Now, "得意先マスタ | CSVアップロード", true, Global.GetConnection());
                    CreatePage();
                }
            }

        }

        protected void RGTokuisakiList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                DataSet1.M_Tokuisaki2Row dr = (e.Item.DataItem as DataRowView).Row as DataSet1.M_Tokuisaki2Row;
                Label LblTokuisakiCode = e.Item.FindControl("LblTokuisakiCode") as Label;
                Label LblTokuisakiName1 = e.Item.FindControl("LblTokuisakiName1") as Label;
                Label LblTokuisakiName2 = e.Item.FindControl("LblTokuisakiName2") as Label;
                Label LblTokuisakiRyakusyou = e.Item.FindControl("LblTokuisakiRyakusyou") as Label;
                Label LblTanto = e.Item.FindControl("LblTanto") as Label;
                Label LblAddress1 = e.Item.FindControl("LblAddress1") as Label;
                Label LblAddress2 = e.Item.FindControl("LblAddress2") as Label;
                Label LblTEL = e.Item.FindControl("LblTEL") as Label;
                Label LblFAX = e.Item.FindControl("LblFAX") as Label;
                Label LblShimebi = e.Item.FindControl("LblShimebi") as Label;
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
                LblTokuisakiCode.Text = dr.CustomerCode + dr.TokuisakiCode;

                if (dr.TokuisakiName1.Length > 15)
                {
                    LblTokuisakiName1.Text = dr.TokuisakiName1.Substring(0, 14) + "...";
                }
                else
                {
                    LblTokuisakiName1.Text = dr.TokuisakiName1;
                }
                if (!dr.IsTokuisakiName2Null())
                {
                    if (dr.TokuisakiName2.Length > 15)
                    {
                        LblTokuisakiName2.Text = dr.TokuisakiName2.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblTokuisakiName2.Text = dr.TokuisakiName2;
                    }
                }
                if (!dr.IsTokuisakiRyakusyoNull())
                {
                    if (dr.TokuisakiRyakusyo.Length > 15)
                    {
                        LblTokuisakiRyakusyou.Text = dr.TokuisakiRyakusyo.Substring(0, 14);
                    }
                    else
                    {
                        LblTokuisakiRyakusyou.Text = dr.TokuisakiRyakusyo;
                    }
                }
                if (!dr.IsTantoStaffCodeNull())
                {
                    DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTanto1(dr.TantoStaffCode.Trim(), Global.GetConnection());
                    if (dt.Count > 0)
                    {
                        LblTanto.Text = dt[0].UserName;
                    }
                }
                if (!dr.IsTokuisakiAddress1Null())
                {
                    if (dr.TokuisakiAddress1.Length > 15)
                    {
                        LblAddress1.Text = dr.TokuisakiAddress1.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblAddress1.Text = dr.TokuisakiAddress1;
                    }
                }
                if (!dr.IsTokuisakiAddress2Null())
                {
                    if (dr.TokuisakiAddress2.Length > 15)
                    {
                        LblAddress2.Text = dr.TokuisakiAddress2.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblAddress2.Text = dr.TokuisakiAddress2;
                    }
                }
                if (!dr.IsTokuisakiTELNull())
                {
                    LblTEL.Text = dr.TokuisakiTEL;
                }
                if (!dr.IsTokuisakiFAXNull())
                {
                    LblFAX.Text = dr.TokuisakiFAX;
                }
                if (!dr.IsShimebiNull())
                {
                    string cod = "";
                    switch (dr.Shimebi)
                    {
                        case "05":
                            cod = "5日締め";
                            break;
                        case "10":
                            cod = "10日締め";
                            break;
                        case "15":
                            cod = "15日締め";
                            break;
                        case "20":
                            cod = "20日締め";
                            break;
                        case "25":
                            cod = "25日締め";
                            break;
                        case "99":
                            cod = "月末締め";
                            break;
                        case "00":
                            cod = "随時締め";
                            break;
                        case "月末":
                            cod = "月末";
                            break;
                        case "都度":
                            cod = "都度";
                            break;
                    }
                    if (cod != "")
                    {
                        LblShimebi.Text = cod;
                    }
                }
                HidSyousai.Value = dr.TokuisakiCode + "/" + dr.CustomerCode;

            }
        }

        protected void RGTokuisakiList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
            if (HidSyousai != null)
            {
                string UserId = HidSyousai.Value;
                if (e.CommandName.Equals("BtnSyousai"))
                {
                    Masters.Style["display"] = "none";
                    RGTokuisakiList.Style["display"] = "none";
                    Touroku.Style["display"] = "";
                    Tokuisaki.Create(UserId);
                }
                if (e.CommandName.Equals("Delete"))
                {
                    try
                    {
                        ClassMaster.DeleteTokuisaki(UserId, Global.GetConnection());
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

        protected void RGTokuisakiList_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RGTokuisakiList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RGTokuisakiList.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreatePage();
        }

        protected void RGTokuisakiList_PreRender(object sender, EventArgs e)
        {

        }
    }
}