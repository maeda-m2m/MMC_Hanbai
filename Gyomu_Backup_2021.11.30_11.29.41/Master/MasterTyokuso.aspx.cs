using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using DLL;
using System.IO;

namespace Gyomu.Master
{
    public partial class MasterTyokuso : System.Web.UI.Page
    {
        HtmlInputFile[] files = null;

        const int LIST_ID = 60;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                Craete("", 0);
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
                if (!string.IsNullOrEmpty(SessionManager.NouhinsakiCode))
                {
                    Master.Style["display"] = "none";
                    Touroku.Style["display"] = "";
                    Tyokuso.Create(SessionManager.NouhinsakiCode);
                }
            }
        }

        private void Craete(string koumoku, int page)
        {
            try
            {
                lblMsg.Text = "";
                L.Visible = true;

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM M_Facility_New", Global.GetConnection());
                string cmm = "";
                if (!string.IsNullOrEmpty(koumoku))
                {
                    cmm = da.SelectCommand.CommandText + " where " + koumoku;
                }
                else
                {
                    cmm = da.SelectCommand.CommandText;
                }
                cmm += " ORDER BY FacilityNo";
                DataSet1.M_Facility_NewDataTable dt = Class1.GetFacilityMaster(cmm, Global.GetConnection());

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
                    RGTyokusou.CurrentPageIndex = 0;
                    lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";    // データはあるか取得ページがおかしい
                    return;
                }
                RGTyokusou.CurrentPageIndex = page;
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

                RGTyokusou.VirtualItemCount = nRecCount;//nRecCountに取ってきたデータ全件の大きさがわかる
                RGTyokusou.DataSource = dt;
                RGTyokusou.DataBind();
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
            //修正ボタンクリック動作
            string TyokusoCode = count.Value;
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";

            Tyokuso.Create(TyokusoCode);
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterTyokuso.aspx");
        }

        protected void BtnSinki_Click(object sender, EventArgs e)
        {
            Tyokuso.Clear();
            Master.Style["display"] = "none";
            Touroku.Style["display"] = "";
        }

        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Tyokuso.Toroku();

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
            Craete(Koumoku, RGTyokusou.MasterTableView.CurrentPageIndex);
        }

        protected void BtnCSVdownload_Click(object sender, EventArgs e)
        {
            string strWhere = "";
            UserViewManager.UserView v = SessionManager.User.GetUserView(60);
            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());
            strWhere = CreateCommandText(da.SelectCommand.CommandText);
            if (strWhere != "")
            {
                DataSet1.M_Facility_NewDataTable dt = Class1.GetFacilityMaster(da.SelectCommand.CommandText + " where " + strWhere, Global.GetConnection());
                if (dt.Count > 0)
                {
                    string strExt = "csv";
                    string strFileName = ("直送先_施設マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                    Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                    this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
            this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
            Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
                }
            }
            else
            {
                DataSet1.M_Facility_NewDataTable dt2 = Class1.GetFacilityMaster(da.SelectCommand.CommandText, Global.GetConnection());
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
                            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
                            DataSet1.M_Facility_NewRow dr = dt.NewM_Facility_NewRow();
                            dr.ItemArray = mData;
                            dt.AddM_Facility_NewRow(dr);
                            ClassMaster.UpdateCSVfacility(dt, Global.GetConnection());
                        }
                    }
                    else
                    {
                        while (check.EndOfStream == false)
                        {
                            string strLineData = check.ReadLine();
                            string[] mData = strLineData.Split(',');
                            DataSet1.M_Facility_NewDataTable dt = new DataSet1.M_Facility_NewDataTable();
                            DataSet1.M_Facility_NewRow dr = dt.NewM_Facility_NewRow();
                            dr.ItemArray = mData;
                            dt.AddM_Facility_NewRow(dr);
                            ClassMaster.UpdateCSVfacility(dt, Global.GetConnection());
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

        protected void RGTyokusou_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (Telerik.Web.UI.GridItemType.Item == e.Item.ItemType || Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType)
            {
                DataSet1.M_Facility_NewRow dr = (e.Item.DataItem as DataRowView).Row as DataSet1.M_Facility_NewRow;
                Label LblFacilityNo = e.Item.FindControl("LblFacilityNo") as Label;
                Label LblCode = e.Item.FindControl("LblCode") as Label;
                Label LblFacilityName1 = e.Item.FindControl("LblFacilityName1") as Label;
                Label LblFacilityName2 = e.Item.FindControl("LblFacilityName2") as Label;
                Label LblAbbreviation = e.Item.FindControl("LblAbbreviation") as Label;
                Label LblFacilityResponsible = e.Item.FindControl("LblFacilityResponsible") as Label;
                Label LblPostNo = e.Item.FindControl("LblPostNo") as Label;
                Label LblAddress1 = e.Item.FindControl("LblAddress1") as Label;
                Label LblAddress2 = e.Item.FindControl("LblAddress2") as Label;
                Label LblTell = e.Item.FindControl("LblTell") as Label;
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;

                LblFacilityNo.Text = dr.FacilityNo.ToString();
                if (!dr.IsCodeNull())
                {
                    LblCode.Text = dr.Code.ToString();
                }
                if (dr.FacilityName1.Length > 15)
                {
                    LblFacilityName1.Text = dr.FacilityName1.Substring(0, 14) + "...";
                }
                else
                {
                    LblFacilityName1.Text = dr.FacilityName1;
                }
                if (!dr.IsFacilityName2Null())
                {
                    if (dr.FacilityName2.Length > 15)
                    {
                        LblFacilityName2.Text = dr.FacilityName2.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblFacilityName2.Text = dr.FacilityName2;
                    }
                }
                if (!dr.IsAbbreviationNull())
                {
                    if (dr.Abbreviation.Length > 15)
                    {
                        LblAbbreviation.Text = dr.Abbreviation.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblAbbreviation.Text = dr.Abbreviation;
                    }
                }
                if (!dr.IsFacilityResponsibleNull())
                {
                    LblFacilityResponsible.Text = dr.FacilityResponsible;
                }
                if (!dr.IsPostNoNull())
                {
                    LblPostNo.Text = dr.PostNo;
                }
                if (!dr.IsAddress1Null())
                {
                    if (dr.Address1.Length > 15)
                    {
                        LblAddress1.Text = dr.Address1.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblAddress1.Text = dr.Address1;
                    }
                }
                if (!dr.IsAddress2Null())
                {
                    if (dr.Address2.Length > 15)
                    {
                        LblAddress2.Text = dr.Address2.Substring(0, 14) + "...";
                    }
                    else
                    {
                        LblAddress2.Text = dr.Address2;
                    }
                }
                if (!dr.IsTellNull())
                {
                    LblTell.Text = dr.Tell;
                }
                HidSyousai.Value = dr.FacilityNo.ToString() + "/" + dr.Code;

            }
        }

        protected void RGTyokusou_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("BtnSyousai"))
            {
                HiddenField HidSyousai = e.Item.FindControl("HidSyousai") as HiddenField;
                string UserId = HidSyousai.Value;
                Master.Style["display"] = "none";
                Touroku.Style["display"] = "";
                Tyokuso.Create(UserId);
            }
        }

        protected void RGTyokusou_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RGTyokusou_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RGTyokusou.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            CreatePage();
        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}