using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Tokuisaki
{
    public partial class Oshirase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Create();
            }




        }






        private void Create()
        {
            string sqlCommand = "select * from T_TokuisakiOshirase order by Date desc";

            MainListView.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainListView.DataBind();
        }











        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            string oshiraseID, userKey, date, title, shousai, hidden;

            string sqlCommand;

            var IDLists = new List<string>();

            for (int i = 0; i < MainListView.Items.Count; i++)
            {
                sqlCommand = "select OshiraseID from T_TokuisakiOshirase order by OshiraseID desc";

                var row = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                for (int i2 = 0; i2 < row.Rows.Count; i2++)
                {
                    IDLists.Add(row.Rows[i2].ItemArray[0].ToString());
                }

                hidden = (MainListView.Items[i].Controls[9] as HiddenField).Value;

                if (row.Rows.Count == 0)
                {
                    oshiraseID = "1";
                }
                else
                {
                    oshiraseID = (int.Parse(row.Rows[0].ItemArray[0].ToString()) + 1).ToString();
                }



                userKey = Session["SESSION_USER_ID"].ToString();

                date = (MainListView.Items[i].Controls[5] as TextBox).Text; ;

                title = (MainListView.Items[i].Controls[3] as TextBox).Text; ;

                shousai = (MainListView.Items[i].Controls[7] as TextBox).Text;

                if (IDLists.Any(n => n.Contains(hidden)))
                {
                    //対象の行のお知らせIDを取得
                    oshiraseID = hidden;

                    UpdateItem(oshiraseID, userKey, date, title, shousai);
                }
                else
                {
                    InsertItem(oshiraseID, userKey, date, title, shousai);
                }
            }

            Create();
        }





        private void UpdateItem(string oshiraseID, string userKey, string date, string title, string shousai)
        {
            string sqlCommand;

            sqlCommand = $@"update T_TokuisakiOshirase set OshiraseID = '{oshiraseID}', UserKey = '{userKey}', Date = '{date}', Title = '{title}', Shousai = '{shousai}'  where OshiraseID = '{oshiraseID}'";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());
        }





        private void InsertItem(string oshiraseID, string userKey, string date, string title, string shousai)
        {
            string sqlCommand;

            if (string.IsNullOrWhiteSpace(date))
            {
                date = DateTime.Now.Date.ToString();
            }


            sqlCommand = $@"insert into T_TokuisakiOshirase values('{oshiraseID}','{userKey}','{date}','{title}','{shousai}')";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());



        }










        protected void RowAddButton_Click(object sender, EventArgs e)
        {
            string sqlCommand;


            sqlCommand = "select OshiraseID from T_TokuisakiOshirase order by OshiraseID desc";

            var ID = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());


            string oshiraseID;

            if (ID.Rows.Count == 0)
            {
                oshiraseID = "1";
            }
            else
            {
                oshiraseID = (int.Parse(ID.Rows[0].ItemArray[0].ToString()) + 1).ToString();
            }

            var userKey = Session["SESSION_USER_ID"].ToString();

            var date = DateTime.Now.Date.ToString();

            string title = "", shousai = "";

            InsertItem(oshiraseID, userKey, date, title, shousai);

            Create();


        }










        protected void MainListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "sakujo")
            {
                int rowNumber = int.Parse(e.CommandArgument.ToString());

                var hidden = (MainListView.Items[rowNumber].Controls[9] as HiddenField).Value;

                string sqlCommand = $"DELETE FROM T_TokuisakiOshirase where OshiraseID = '{hidden}'";

                CommonClass.TranSql(sqlCommand, Global.GetConnection());


            }

            Create();


        }







        protected void MainListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataRow dr = (e.Item.DataItem as DataRowView).Row;

                var date = e.Item.FindControl("TextBox1") as TextBox;
                date.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(dr["Date"].ToString()));


            }

        }










    }
}
