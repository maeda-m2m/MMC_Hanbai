using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;
using Yodokou_HanbaiKanri.Common;
using System.Windows;
using System.Windows.Forms;

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
                this.F.Create(LIST_ID, 3);

                Craete();
                Master.Style["display"] = "";
                Touroku.Style["display"] = "none";
            }
        }

        private void Craete()
        {
            lblMsg.Text = "";
            L.Visible = true;

            UserViewManager.UserView v = SessionManager.User.GetUserView(LIST_ID);

            SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

            Core.Sql.RowNumberInfo info = new Core.Sql.RowNumberInfo();
            info.nStartNumber = this.D.CurrentPageIndex * D.PageSize + 1;
            info.nEndNumber = (this.D.CurrentPageIndex + 1) * D.PageSize;

            string strWhere = "";
            try
            {
                strWhere = this.F.GetFilter(da.SelectCommand);
                if (!string.IsNullOrEmpty(strWhere))
                    da.SelectCommand.CommandText += " where " + strWhere;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                return;
            }

            //DataTable dt = new DataTable();
            DataSet1.M_TantoDataTable dt = Class1.GetStaff(Global.GetConnection());
            //int nRecCount = 0;
            //info.LoadData(da.SelectCommand, Global.GetConnection(), dt, ref nRecCount);


            //if (0 == nRecCount)
            //{
            //    L.Visible = false;
            //    lblMsg.Text = "データがありません。";
            //    return;
            //}
            if (0 == dt.Rows.Count)
            {
                L.Visible = false;
                this.D.CurrentPageIndex = 0;
                lblMsg.Text = "このページのデータが取得できませんでした。再検索してください。";	// データはあるか取得ページがおかしい
                return;
            }

            //this.D.VirtualItemCount = nRecCount;
            this.D.VirtualItemCount = dt.Count;
            int nPageSize = this.D.PageSize;
            //int nPageCount = nRecCount / nPageSize;
            int nPageCount = dt.Count / nPageSize;
            //if (0 < nRecCount % nPageSize) nPageCount++;
            if (0 < dt.Count % nPageSize) nPageCount++;
            if (nPageCount <= this.D.CurrentPageIndex) this.D.MasterTableView.CurrentPageIndex = 0;

            this.D.DataSource = dt;

            //v.CreateRadGrid(this.D, 1, new UserViewManager.UserView.DataBoundEventHandler(UserView_DataBound));

            this.D.DataBind();

            this.ShowList(true);

        }

        private void ShowList(bool bShow)
        {
            this.L.Style["width"] = L.Style["height"] = (bShow) ? "" : "200px";
            for (int i = 0; i < this.L.Controls.Count; i++)
                this.L.Controls[i].Visible = bShow;
        }

        protected void D_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {

                DataRowView drv = (DataRowView)e.Item.DataItem;

                DataSet1.M_TantoRow dr = (DataSet1.M_TantoRow)drv.Row;

                if (!(Telerik.Web.UI.GridItemType.Item == e.Item.ItemType ||
                    Telerik.Web.UI.GridItemType.AlternatingItem == e.Item.ItemType))
                    return;

                System.Web.UI.WebControls.Button btnEdit = e.Item.FindControl("E") as System.Web.UI.WebControls.Button;
                string strCode = Convert.ToString((e.Item.DataItem as DataRowView).Row["UserID"]) + "," +
                    Convert.ToString((e.Item.DataItem as DataRowView).Row["UserName"]) + "," +
                    Convert.ToString((e.Item.DataItem as DataRowView).Row["Busyo"]) +","+
                    Convert.ToString((e.Item.DataItem as DataRowView).Row["Password"]);

                System.Web.UI.WebControls.Button E = e.Item.FindControl("E") as System.Web.UI.WebControls.Button;
                E.Attributes["onclick"] = string.Format("CntRow('{0}')", strCode);

                System.Web.UI.WebControls.Button btnDel = e.Item.FindControl("Del") as System.Web.UI.WebControls.Button;
                //string strcode = Convert.ToString((e.Item.DataItem as DataRowView).Row["UserID"]) + Convert.ToString((e.Item.DataItem as DataRowView).Row["ColBusyo"]);


                System.Web.UI.WebControls.Button Del = e.Item.FindControl("Del") as System.Web.UI.WebControls.Button;
                Del.Attributes["onclick"] = string.Format("CntRow('{0}')", strCode);


                e.Item.Cells[D.Columns.FindByUniqueName("ColID").OrderIndex].Text = dr.UserID.ToString();
                if (!dr.IsYomikataNull())
                {
                    e.Item.Cells[D.Columns.FindByUniqueName("ColYomi").OrderIndex].Text = dr.Yomikata.ToString();
                }
                if (!dr.IsUserNameNull())
                {
                    e.Item.Cells[D.Columns.FindByUniqueName("ColName").OrderIndex].Text = dr.UserName.ToString();
                }

                e.Item.Cells[D.Columns.FindByUniqueName("ColBusyo").OrderIndex].Text = dr.Busyo.ToString();

                if (!dr.IsPasswordNull())
                {
                    e.Item.Cells[D.Columns.FindByUniqueName("ColPass").OrderIndex].Text = dr.Password.ToString();
                }
            }
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

                Craete();

                lblMsg.Text = "登録しました";
            }
        }

        protected void D_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Craete();
        }

        protected void D_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            D.MasterTableView.CurrentPageIndex = e.NewPageIndex;
            this.Craete();
        }

        protected void D_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string UserId = count.Value;
            int id = int.Parse(UserId);
        }

        protected void BtnHihyouji_Click(object sender, EventArgs e)
        {
            DataSet1.M_TantoDataTable dt = Class1.GetNoStaff(Global.GetConnection());
            D.DataSource = dt;
            D.DataBind();
        }

        protected void Del_Click(object sender, EventArgs e)
        {

            string[] key = count.Value.Split(',');
            
            int id = int.Parse(key[0]);
            string UserName = key[1];
            string Busyo = key[2];
            string PassWord = key[3];

            Class1.DeleteStaff(id, UserName, Busyo, PassWord, Global.GetConnection());

            DataSet1.M_TantoDataTable dt = Class1.GetStaff(Global.GetConnection());
            D.DataSource = dt;
            D.DataBind();

            lblMsg.Text = "UserID(" + id + ")を削除しました。";



        }
    }
}