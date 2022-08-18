using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Tokuisaki
{
    public class SearchTable1
    {
        public string ShouhinName { get; set; }
        public string Media { get; set; }
        public string Kakaku { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public string Siyou { get; set; }
        public string MakerNumber { get; set; }
        public string ShouhinNumber { get; set; }
        public string ShouhinCatch { get; set; }
        public string ShouhinContents { get; set; }
        public string Copyright { get; set; }
        public string ShiireName { get; set; }




    }





    public partial class ShouhinSearch : System.Web.UI.Page
    {




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                MainGridCreate();

                FirstCreate();
                FirsrDrop();
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
select distinct( M_Kakaku_2.SyouhinCode),M_Kakaku_2.SyouhinMei, M_Kakaku_2.Media 
from T_tokusyu 
inner join M_Kakaku_2 
on T_tokusyu.SyouhinCode = M_Kakaku_2.SyouhinCode and T_tokusyu.Media = M_Kakaku_2.Media
where T_tokusyu.CategoryCode = '{categoryCode}' and T_tokusyu.tokusyu_code = '{tokushuCode}'";


            SelectedGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            SelectedGrid.DataBind();
        }


        private void FirsrDrop()
        {
            TokushuCategoryDrop.SelectedValue = "205";
            TokushuCategoryDrop.DataBind();
        }









        /// <summary>
        /// 画面読み込みの最初の一回だけ
        /// </summary>
        private void MainGridCreate()
        {

            //カテゴリコード直書き
            string sqlCommand = $@"
select top(200) M_Kakaku_2.Syouhincode, M_Kakaku_2.SyouhinMei,M_Kakaku_2.Media,M_Kakaku_2.ShiireName,M_Kakaku_2.HyoujunKakaku,M_Kakaku_2.ShiireKakaku,M_TokuisakiShouhin.ShouhinAttribute,M_TokuisakiShouhin.JoueiTime,M_TokuisakiShouhin.MovieManager,M_TokuisakiShouhin.MovieActor,M_TokuisakiShouhin.Copyright
from M_Kakaku_2 
left outer join M_TokuisakiShouhin 
on M_TokuisakiShouhin.Syouhincode = M_Kakaku_2.SyouhinCode
where M_Kakaku_2.CategoryCode = '205' 
and  not (M_Kakaku_2.Syouhincode = '1005' or M_Kakaku_2.Syouhincode = '10000000' or M_Kakaku_2.Syouhincode = '1999' or M_Kakaku_2.SyouhinCode = '1001' or M_Kakaku_2.SyouhinCode = '1002' or M_Kakaku_2.SyouhinCode = '28643001')
";
            MainGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            MainGrid.DataBind();

        }








        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Sentei_Click(object sender, EventArgs e)
        {
            int count = 0;

            string categoryCode = TokushuCategoryDrop.SelectedValue;
            string tokushuCode = TokushuNameDrop.SelectedValue;

            string sqlCommand;

            for (int i = 0; i < MainGrid.Rows.Count; i++)
            {
                CheckBox check = MainGrid.Rows[i].Cells[0].Controls[1] as CheckBox;

                if (check.Checked)
                {
                    string[] vs = new string[] { "jj" };

                    //1商品コード,2メディア,4価格
                    var hidden = (MainGrid.Rows[i].Cells[2].Controls[3] as HiddenField).Value.Split(vs, StringSplitOptions.None);

                    //メディア
                    var media = hidden[2];

                    //価格
                    var kakaku = hidden[4];

                    //商品コード
                    var shouhinNumber = hidden[1].Trim();


                    sqlCommand = $"select ShouhinContents from M_TokuisakiShouhin where Syouhincode = '{shouhinNumber}'";

                    var row = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                    string contents;

                    if (row.Rows.Count == 0)
                    {
                        contents = "";
                    }
                    else
                    {
                        contents = row.Rows[0].ItemArray[0].ToString().Trim();
                    }

                    TokushuShouhinInsert(shouhinNumber.Trim(), kakaku.Trim(), media.Trim(), tokushuCode.Trim(), categoryCode.Trim(), contents);
                    count++;
                }
            }


            if (count == 0)
            {

                string script = "alert('商品が選択されていません。');";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);


            }
            else
            {
                for (int i = 0; i < MainGrid.Rows.Count; i++)
                {
                    CheckBox check = MainGrid.Rows[i].Cells[0].Controls[1] as CheckBox;

                    check.Checked = false;
                }




                sqlCommand = $@"
select distinct( M_Kakaku_2.SyouhinCode),M_Kakaku_2.SyouhinMei, M_Kakaku_2.Media 
from T_tokusyu 
inner join M_Kakaku_2 
on T_tokusyu.SyouhinCode = M_Kakaku_2.SyouhinCode and T_tokusyu.Media = M_Kakaku_2.Media
where T_tokusyu.CategoryCode = '{categoryCode}' and T_tokusyu.tokusyu_code = '{tokushuCode}'";

                SelectedGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

                if (tokushuCode == "1") SelectedGrid.DataSource = "";

                SelectedGrid.DataBind();

            }





        }











        private void TokushuShouhinInsert(string shouhinNumber, string kakaku, string media, string tokushuCode, string categoryCode, string shouhinShoukai)
        {
            string sqlCommand;

            sqlCommand = $"select SyouhinCode, Media from T_tokusyu where tokusyu_code = '{tokushuCode}' and CategoryCode ='{categoryCode}'";

            var row = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            //重複チェック
            for (int i2 = 0; i2 < row.Rows.Count; i2++)
            {
                if (row.Rows[i2].ItemArray[0].ToString() == shouhinNumber & row.Rows[i2].ItemArray[1].ToString() == media)
                {

                    return;
                }


            }



            //0商品コード、1カテゴリコード、2メディア、3仕入コード、4メーカー品番、5範囲、6価格、7仕入価格、8特集コード、9商品紹介、10ランキング
            string[] str = new string[] { shouhinNumber, categoryCode, media, "", "", "", kakaku, "", tokushuCode, shouhinShoukai, "" };

            sqlCommand = $@"insert into T_tokusyu values('{str[0]}','{str[1]}','{str[2]}','{str[3]}','{str[4]}','{str[5]}','{str[6]}','{str[7]}','{str[8]}','{str[9]}','{str[10]}')";

            CommonClass.TranSql(sqlCommand, Global.GetConnection());

        }








        protected void TokushuNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedGridCreate();
        }








        protected void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow dr = (e.Row.DataItem as DataRowView).Row;

                //これがボタンになり、ここにスクリプトを埋め込む。gird1
                Label btn = e.Row.FindControl("btn") as Label;

                HiddenField HidData = e.Row.FindControl("HidData") as HiddenField;

                btn.Text = dr["SyouhinMei"].ToString();


                HidData.Value = dr["SyouhinMei"] + "jj" + dr["SyouhinCode"] + "jj" + dr["Media"] + "jj" + dr["ShiireName"] + "jj" + dr["HyoujunKakaku"] + "jj" + dr["ShiireKakaku"] + "jj" + dr["ShouhinAttribute"] + "jj" + dr["JoueiTime"] + "jj" + dr["MovieManager"] + "jj" + dr["MovieActor"] + "jj" + dr["Copyright"];

                btn.Attributes["onclick"] = string.Format("Create('{0}'); return false;", HidData.Value); ;



            }
        }







        protected void SelectedGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "sakujo")
            {
                int row_number = int.Parse(e.CommandArgument.ToString());

                for (int i = 0; i < SelectedGrid.Rows.Count; i++)
                {
                    if (row_number == i)
                    {
                        var shouhinCode = (SelectedGrid.Rows[i].Controls[3].Controls[1] as HiddenField).Value;

                        var MediaLabel = (SelectedGrid.Rows[i].Controls[2].Controls[1] as Label).Text;

                        string categoryCode = TokushuCategoryDrop.SelectedValue;

                        string tokushuCode = TokushuNameDrop.SelectedValue;

                        string sqlcoomand = $"DELETE FROM T_tokusyu where SyouhinCode = '{shouhinCode}' and  Media = '{MediaLabel}' and CategoryCode = '{categoryCode}' and tokusyu_code = '{tokushuCode}';";

                        CommonClass.TranSql(sqlcoomand, Global.GetConnection());

                        SelectedGridCreate();

                        return;
                    }
                }
            }

        }









        protected void SearchCategoryDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDown = (DropDownList)sender;

            var dropDownValue = dropDown.SelectedValue;

            TokushuCategoryDrop.SelectedValue = dropDownValue;

            DropDownMainGridCreate(dropDownValue);
            SelectedGridCreate();


        }


        protected void TokushuCategoryDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropDown = (DropDownList)sender;

            var dropDownValue = dropDown.SelectedValue;

            SearchCategoryDrop.SelectedValue = dropDownValue;

            DropDownMainGridCreate(dropDownValue);
            SelectedGridCreate();


        }



        /// <summary>
        /// ドロップダウンのカテゴリでデータを分ける処理。
        /// </summary>
        /// <param name="dropDownValue"></param>
        private void DropDownMainGridCreate(string dropDownValue)
        {
            string sqlCommand = $@"
select M_Kakaku_2.Syouhincode, M_Kakaku_2.SyouhinMei,M_Kakaku_2.Media,M_Kakaku_2.ShiireName,M_Kakaku_2.HyoujunKakaku,M_Kakaku_2.ShiireKakaku,M_TokuisakiShouhin.ShouhinAttribute,M_TokuisakiShouhin.JoueiTime,M_TokuisakiShouhin.MovieManager,M_TokuisakiShouhin.MovieActor,M_TokuisakiShouhin.Copyright
from M_Kakaku_2 
left outer join M_TokuisakiShouhin 
on M_TokuisakiShouhin.Syouhincode = M_Kakaku_2.SyouhinCode
where M_Kakaku_2.CategoryCode = '{dropDownValue}' and  not (M_Kakaku_2.Syouhincode = '1005' or M_Kakaku_2.Syouhincode = '10000000' or M_Kakaku_2.Syouhincode = '1999' or M_Kakaku_2.SyouhinCode = '1004')
";

            MainGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainGrid.DataBind();

        }








        /// <summary>
        /// 
        /// </summary>
        private void SelectedGridCreate()
        {
            string categoryCode = TokushuCategoryDrop.SelectedValue;
            string tokushuCode = TokushuNameDrop.SelectedValue;

            TokushuNameDrop.SelectedValue = tokushuCode;
            TokushuCategoryDrop.SelectedValue = categoryCode;


            string sqlCommand = $@"
select distinct( M_Kakaku_2.SyouhinCode),M_Kakaku_2.SyouhinMei, M_Kakaku_2.Media 
from T_tokusyu 
inner join M_Kakaku_2 
on T_tokusyu.SyouhinCode = M_Kakaku_2.SyouhinCode and T_tokusyu.Media = M_Kakaku_2.Media
where T_tokusyu.CategoryCode = '{categoryCode}' and T_tokusyu.tokusyu_code = '{tokushuCode}'
";

            SelectedGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());
            SelectedGrid.DataBind();
        }







        protected void ShouhinEditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tokuisaki/ShouhinCheck.aspx");
        }








        protected void ALL_select_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MainGrid.Rows.Count; i++)
            {
                CheckBox b = MainGrid.Rows[i].Cells[0].Controls[1] as CheckBox;

                b.Checked = true;
            }
        }

        protected void ALL_uncheck_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MainGrid.Rows.Count; i++)
            {
                CheckBox b = MainGrid.Rows[i].Cells[0].Controls[1] as CheckBox;

                b.Checked = false;
            }

        }






        protected void shousai_btn_Click(object sender, EventArgs e)
        {


            string sqlCommand;

            string categoryCode = TokushuCategoryDrop.SelectedValue;
            //string tokushuCode = TokushuNameDrop.SelectedValue;


            int count = 0;

            sqlCommand = $@"
select M_Kakaku_2.Syouhincode, M_Kakaku_2.SyouhinMei,M_Kakaku_2.Media,M_Kakaku_2.ShiireName,M_Kakaku_2.HyoujunKakaku,M_Kakaku_2.ShiireKakaku,M_TokuisakiShouhin.ShouhinAttribute,M_TokuisakiShouhin.JoueiTime,M_TokuisakiShouhin.MovieManager,M_TokuisakiShouhin.MovieActor,M_TokuisakiShouhin.Copyright
from M_Kakaku_2 
left outer join M_TokuisakiShouhin 
on M_Kakaku_2.SyouhinCode = M_TokuisakiShouhin.SyouhinCode
where CategoryCode = '{categoryCode}'
and  not (M_Kakaku_2.Syouhincode = '1005' or M_Kakaku_2.Syouhincode = '10000000' or M_Kakaku_2.Syouhincode = '1999' or M_Kakaku_2.SyouhinCode = '1001' or M_Kakaku_2.SyouhinCode = '1002' or M_Kakaku_2.SyouhinCode = '28643001' or M_Kakaku_2.SyouhinCode = '1004') and";


            if (!string.IsNullOrWhiteSpace(ShouhinNameLabel.Text))
            {
                sqlCommand += $" M_Kakaku_2.SyouhinMei like '%{ShouhinNameLabel.Text}%' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(MediaDrop.SelectedValue))
            {
                sqlCommand += $" M_Kakaku_2.Media  = '{MediaDrop.SelectedValue}' and";
                count++;
            }
            if (!string.IsNullOrWhiteSpace(KakakuDropDown.Text))
            {
                var kakaku = KakakuDropDown.Text.Split(',');

                sqlCommand += $" M_Kakaku_2.HyoujunKakaku between '{kakaku[0]}' and '{kakaku[1]}' and";
                count++;
            }
            if (!string.IsNullOrWhiteSpace(DirectiorLabel.Text))
            {
                sqlCommand += $" M_TokuisakiShouhin.MovieManager  = '{DirectiorLabel.Text}' and";
                count++;
            }
            if (!string.IsNullOrWhiteSpace(ActorLabel1.Text))
            {
                sqlCommand += $" M_TokuisakiShouhin.MovieActor  = '{ActorLabel1.Text}' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(JoueiTimeDrop.Text))
            {

                var time = JoueiTimeDrop.Text.Split(',');

                sqlCommand += $" M_TokuisakiShouhin.JoueiTime between '{time[0]}' and '{time[1]}' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(ShiyouLabel.Text))
            {
                sqlCommand += $" M_TokuisakiShouhin.ShouhinAttribute  = '{ShiyouLabel.Text}' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(ShouhinCodeLabel.Text))
            {
                sqlCommand += $" M_Kakaku_2.Syouhincode  = '{ShouhinCodeLabel.Text}' and";
                count++;
            }

            if (!string.IsNullOrWhiteSpace(CompanyLabel.Text))
            {
                sqlCommand += $" M_TokuisakiShouhin.Copyright  = '{CompanyLabel.Text}' and";
                count++;
            }


            if (count != 0)
            {
                sqlCommand = sqlCommand.Remove(sqlCommand.Length - 3, 3);

                sqlCommand += "order by M_Kakaku_2.HyoujunKakaku";
            }
            else
            {
                return;
            }


            MainGrid.DataSource = CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            MainGrid.DataBind();
        }








        protected void ClearButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tokuisaki/ShouhinSearch.aspx");
        }









        protected void HederCheck_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox check = MainGrid.Rows[0].Cells[0].Controls[1] as CheckBox;
            CheckBox check = MainGrid.HeaderRow.Cells[0].Controls[1] as CheckBox;




            if (check.Checked)
            {
                for (int i = 0; i < MainGrid.Rows.Count; i++)
                {
                    CheckBox b = MainGrid.Rows[i].Cells[0].Controls[1] as CheckBox;

                    b.Checked = true;
                }
                check.Checked = true;
            }
            else
            {
                for (int i = 0; i < MainGrid.Rows.Count; i++)
                {
                    CheckBox b = MainGrid.Rows[i].Cells[0].Controls[1] as CheckBox;

                    b.Checked = false;
                }
                check.Checked = false; ;
            }
        }
































































        //****************************************
        //クラスの終わり
        //****************************************
    }
}