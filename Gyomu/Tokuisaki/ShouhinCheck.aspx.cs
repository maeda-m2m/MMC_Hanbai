using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Tokuisaki
{
    public partial class ShouhinCheck : System.Web.UI.Page
    {





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FirstCreate();
            }


        }








        /// <summary>
        /// 画面読み込みの最初の一回だけ
        /// </summary>
        private void FirstCreate()
        {
            //以下数字を直書きしている。
            //最初に表示されているドロップダウンの項目名。
            string categoryCode = "205";
            string tokushuCode = "2";
            string sqlCommand = $@"
select M_TokuisakiShouhin.SyouhinMei,T_tokusyu.Media,T_tokusyu.tokusyu_shouhin_shoukai,T_tokusyu.Ranking,T_tokusyu.SyouhinCode  
from T_tokusyu 
inner join M_TokuisakiShouhin 
on T_tokusyu.SyouhinCode = M_TokuisakiShouhin.Syouhincode
where T_tokusyu.tokusyu_code = '{tokushuCode}' and T_tokusyu.CategoryCode = '{categoryCode}'
order by T_tokusyu.Ranking";

            MainListView.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            MainListView.DataBind();

            if (tokushuCode == "2" | tokushuCode == "4")
            {
                for (int i = 0; i < MainListView.Items.Count; i++)
                {

                    MainListView.Items[i].Controls[1].Visible = false;
                }
            }
        }






        private void Create()
        {


            string tokushuCode = TokushuNameDrop.SelectedValue;
            string categoryCode = TokushuCategoryDrop.SelectedValue;

            string sqlCommand = $@"
select M_TokuisakiShouhin.SyouhinMei,T_tokusyu.Media,T_tokusyu.tokusyu_shouhin_shoukai,T_tokusyu.Ranking,T_tokusyu.SyouhinCode  
from T_tokusyu 
inner join M_TokuisakiShouhin 
on T_tokusyu.SyouhinCode = M_TokuisakiShouhin.Syouhincode and T_tokusyu.Media = M_TokuisakiShouhin.Media
where T_tokusyu.tokusyu_code = '{tokushuCode}' and T_tokusyu.CategoryCode = '{categoryCode}'
order by T_tokusyu.Ranking";

            MainListView.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            MainListView.DataBind();

            if (tokushuCode == "2" | tokushuCode == "4")
            {
                for (int i = 0; i < MainListView.Items.Count; i++)
                {

                    MainListView.Items[i].Controls[1].Visible = false;
                }
            }

        }

        protected void TokushuNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Create();


        }

        protected void TokushuCategoryDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Create();
        }



        /// <summary>
        /// 確定ボタンの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            string sqlCommand;

            string tokushuCode = TokushuNameDrop.SelectedValue;
            string categoryCode = TokushuCategoryDrop.SelectedValue;

            if (MainListView.Items.Count == 0)
            {
                Response.Write("商品が登録されていません。変更に失敗しました。");
                return;

            }

            for (int i = 0; i < MainListView.Items.Count; i++)
            {
                //ランキング、メディア、紹介メッセージ、商品コード
                string[] shouhin = new string[] {
                    (MainListView.Items[i].Controls[1] as TextBox).Text,
                    (MainListView.Items[i].Controls[5] as Label).Text,
                    (MainListView.Items[i].Controls[7] as TextBox).Text,
                    (MainListView.Items[i].Controls[11] as HiddenField).Value,
                };



                if (!int.TryParse(shouhin[0], out int dummy))
                {
                    Response.Write("ランキングには数字を入力してください。");
                    return;
                }


                sqlCommand = $@"
update T_tokusyu 
set tokusyu_shouhin_shoukai = '{shouhin[2]}', Ranking = '{shouhin[0]}' 
where SyouhinCode = '{shouhin[3]}' and Media = '{shouhin[1]}' and CategoryCode = '{categoryCode}' and tokusyu_code = '{tokushuCode}'";

                CommonClass.TranSql(sqlCommand, Global.GetConnection());

            }

            Create();
            Response.Write("特集商品情報が変更されました。");


        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tokuisaki/ShouhinSearch.aspx");
        }




        //protected void MainGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "sakujo")
        //    {
        //        int row_number = int.Parse(e.CommandArgument.ToString());

        //        for (int i = 0; i < MainGrid.Rows.Count; i++)
        //        {
        //            if (row_number == i)
        //            {
        //                var shouhinCode = (MainGrid.Rows[i].Controls[3].Controls[1] as HiddenField).Value;

        //                var MediaLabel = (MainGrid.Rows[i].Controls[2].Controls[1] as Label).Text;

        //                string categoryCode = TokushuCategoryDrop.SelectedValue;

        //                string tokushuCode = TokushuNameDrop.SelectedValue;

        //                string sqlcoomand = $"DELETE FROM T_tokusyu where SyouhinCode = '{shouhinCode}' and  Media = '{MediaLabel}' and CategoryCode = '{categoryCode}' and tokusyu_code = '{tokushuCode}';";

        //                CommonClass.TranSql(sqlcoomand, Global.GetConnection());

        //                //MainGrid();

        //                return;
        //            }
        //        }
        //    }

        //}






























































































    }
}