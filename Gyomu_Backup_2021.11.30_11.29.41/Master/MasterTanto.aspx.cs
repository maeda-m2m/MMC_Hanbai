using DLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using System.IO;
namespace Gyomu.Master
{
    public partial class MasterTanto : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;

        const int LIST_ID = 19;


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

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM M_Tanto", Global.GetConnection());
                string cmm = "";
                if (!string.IsNullOrEmpty(koumoku))
                {
                    cmm = da.SelectCommand.CommandText + " where " + koumoku;
                }
                else
                {
                    cmm = da.SelectCommand.CommandText;
                }
                cmm += " ORDER BY UserID";
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTantoMaster(cmm, Global.GetConnection());
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
                    RGTanto.CurrentPageIndex = 0;
                    lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                    return;
                }
                RGTanto.CurrentPageIndex = page;
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

                RGTanto.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
                RGTanto.DataSource = dt;
                RGTanto.DataBind();
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


        private void UserView_DataBound(UserViewManager.UserViewEventArgs e)
        {
            if (e.ItemType != UserViewManager.EnumItemType.DataRow) return;

            e.TableCells["Yuko"].Text = (!string.IsNullOrEmpty(e.DataRow["Yuko"].ToString()) && Convert.ToBoolean(e.DataRow["Yuko"].ToString())) ? "有効" : "無効";

            string sBumon = e.TableCells["BumonKubun"].Text;
            DataDrop.M_BumonRow dr =
                ClassDrop.GetBusyo(sBumon, Global.GetConnection());
            e.TableCells["BumonKubun"].Text = dr.Busyo;

            { e.TableCells["Password"].Text = "******"; }
        }

        protected void E_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック動作
            string[] key = count.Value.Split(',');
            string UserID = key[0];
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

            Tanto.Create(UserID);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterTanto.aspx");
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Tanto.Clear();
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Tanto.Toroku();

            if (bo)
            {
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";

                Craete("", 0);

                lblMsg.Text = "登録しました";
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string UserId = count.Value;
            int id = int.Parse(UserId);
        }

        protected void BtnSerch_Click1(object sender, EventArgs e)
        {
            CreatePage();
        }

        private void CreatePage()
        {
            string Koumoku = "";
            Koumoku = Filter.GetKoumoku();
            Craete(Koumoku, RGTanto.MasterTableView.CurrentPageIndex);
        }

        protected void BtnCSVdownload_Click(object sender, EventArgs e)
        {
            string strWhere = "";
            UserViewManager.UserView v = SessionManager.User.GetUserView(19);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());
            strWhere = CreateCommandText(da.SelectCommand.CommandText);
            if (strWhere != "")
            {
                DataMaster.M_Tanto1DataTable dt = ClassMaster.GetTantoMaster(da.SelectCommand.CommandText + " where " + strWhere, Global.GetConnection());
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
                DataMaster.M_Tanto1DataTable dt2 = ClassMaster.GetTantoMaster(da.SelectCommand.CommandText, Global.GetConnection());
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
                            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
                            DataMaster.M_Tanto1Row dr = dt.NewM_Tanto1Row();
                            dr.ItemArray = mData;
                            dt.AddM_Tanto1Row(dr);
                            ClassMaster.UpdateCSVtanto(dt, Global.GetConnection());
                        }
                    }
                    else
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split(',');
                            DataMaster.M_Tanto1DataTable dt = new DataMaster.M_Tanto1DataTable();
                            DataMaster.M_Tanto1Row dr = dt.NewM_Tanto1Row();
                            dr.ItemArray = mData;
                            dt.AddM_Tanto1Row(dr);
                            ClassMaster.UpdateCSVtanto(dt, Global.GetConnection());
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

        protected void RGTanto_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                DataMaster.M_Tanto1Row dr = (e.Item.DataItem as DataRowView).Row as DataMaster.M_Tanto1Row;
                Label LblUserID = e.Item.FindControl("LblUserID") as Label;
                Label LblUserName = e.Item.FindControl("LblUserName") as Label;
                Label LblYomikata = e.Item.FindControl("LblYomikata") as Label;
                Label LblBusyo = e.Item.FindControl("LblBusyo") as Label;
                Label LblBumon = e.Item.FindControl("LblBumon") as Label;
                Label LblYuko = e.Item.FindControl("LblYuko") as Label;
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;

                LblUserID.Text = dr.UserID.ToString();
                if (!dr.IsUserNameNull())
                {
                    LblUserName.Text = dr.UserName;
                }
                if (!dr.IsYomikataNull())
                {
                    LblYomikata.Text = dr.Yomikata;
                }
                LblBusyo.Text = dr.Busyo;
                if (!dr.IsBumonNameNull())
                {
                    LblBumon.Text = dr.BumonName;
                }
                LblYuko.Text = dr.Yuko.ToString();
                HidSyousai.Value = dr.UserKey.ToString();


            }
        }

        protected void RGTanto_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("BtnSyousai"))
            {
                Label LblUserID = e.Item.FindControl("LblUserID") as Label;
                string UserId = LblUserID.Text;
                Master.Style["display"] = "none";
                Touroku.Style["display"] = "";
                Tanto.Create(UserId);
            }
        }

        protected void RGTanto_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RGTanto_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RGTanto.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreatePage();
        }

        protected void RGTanto_PreRender(object sender, EventArgs e)
        {

        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}