using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Tokuisaki
{
    public partial class TokushuMenu : System.Web.UI.Page
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
            string sqlCommand = "select * from M_tokusyu";

            var table = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            table.Rows.RemoveAt(0);

            MainListView.DataSource = table;
            MainListView.DataBind();


        }









        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            string sqlCommand;



            for (int i = 0; i < MainListView.Items.Count; i++)
            {



                string[] shouhin = new string[] {
                    (MainListView.Items[i].FindControl("TextBox2") as TextBox).Text,
                    (MainListView.Items[i].FindControl("TextBox4") as TextBox).Text,
                    (MainListView.Items[i].FindControl("TextBox3") as TextBox).Text,
                    (MainListView.Items[i].FindControl("Hidden") as HiddenField).Value,
                };

                sqlCommand = $@"
update M_tokusyu 
set tokusyu_name = '{shouhin[0]}', tokusyu_naiyou = '{shouhin[2]}', RankingTitle = '{shouhin[1]}' 
where tokusyu_code = '{shouhin[3]}'";

                CommonClass.TranSql(sqlCommand, Global.GetConnection());

            }

            Create();
        }











    }
}