using System;
using DLL;
using System.Data;
using System.Web.UI.WebControls;

namespace Gyomu.Common
{
    public partial class CtrlFilter2 : System.Web.UI.UserControl
    {
        public static string[] strKoumokuAry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
            }
        }

        internal void Create()
        {
            //Dataset.T_UserListFieldDataTable dtUf = UserViewClass.GetField(code, Global.GetConnection());

            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            DataMitumori.T_RowRow dr = dt.NewT_RowRow();
            dt.AddT_RowRow(dr);
            FilterGrid.DataSource = dt;
            FilterGrid.DataBind();
        }


        internal void KoumokuBind(string strColumnAry)
        {
            if (strColumnAry != "")
            {
                strKoumokuAry = strColumnAry.Split(',');
            }
        }

        internal string GetKoumoku()
        {
            string Koumoku = "";
            string Atai = "";
            for (int i = 0; i < FilterGrid.Items.Count; i++)
            {
                DropDownList DdlKoumoku = FilterGrid.Items[i].FindControl("DdlKoumoku") as DropDownList;
                TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                if (DdlKoumoku.Text != "--選択して下さい--")
                {
                    if (Koumoku != "")
                    {
                        Koumoku += "AND" + " " + DdlKoumoku.Text;
                    }
                    else
                    {
                        Koumoku = DdlKoumoku.Text;
                    }
                    Atai = TbxAtai.Text.Trim();
                    switch (DdlFilterItem.Text)
                    {
                        case "EqualTo":
                            Koumoku += " =" + " " + "'" + Atai + "'";
                            break;
                        case "NotEqualTo":
                            Koumoku += " !=" + " " + "'" + Atai + "'";
                            break;
                        case "Contains":
                            Koumoku += " like" + " " + "'%" + Atai + "%'";
                            break;
                        case "DoesNotContain":
                            Koumoku += " not like" + " " + "'%" + Atai + "%'";
                            break;
                        case "StartWith":
                            Koumoku += " like" + " " + "'" + Atai + "%'";
                            break;
                        case "EndWith":
                            Koumoku += " like" + " " + "'%" + Atai + "'";
                            break;
                    }
                }
            }
            return Koumoku;
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            for (int i = 0; i < FilterGrid.Items.Count; i++)
            {
                DropDownList DdlKoumoku = FilterGrid.Items[i].FindControl("DdlKoumoku") as DropDownList;
                TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                if (DdlKoumoku.Text == "--選択して下さい--")
                {
                    break;
                }
                dr.SyouhinCode = DdlKoumoku.Text;
                dr.SyouhinMei = TbxAtai.Text;
                dr.Media = DdlFilterItem.SelectedValue;
                dt.AddT_RowRow(dr);
            }
            DataMitumori.T_RowRow drN = dt.NewT_RowRow();
            dt.AddT_RowRow(drN);

            FilterGrid.DataSource = dt;
            FilterGrid.DataBind();
        }


        protected void Filter_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                DropDownList DdlKoumoku = e.Item.FindControl("DdlKoumoku") as DropDownList;
                TextBox TbxAtai = e.Item.FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = e.Item.FindControl("DdlFilterItem") as DropDownList;


                if (strKoumokuAry != null)
                {
                    DdlKoumoku.Items.Add("--選択して下さい--");
                    for (int a = 0; a < strKoumokuAry.Length; a++)
                    {
                        DdlKoumoku.Items.Add(strKoumokuAry[a]);
                    }
                }
                if (!dr.IsSyouhinCodeNull())
                {
                    DdlKoumoku.SelectedItem.Text = dr.SyouhinCode;
                }
                if (!dr.IsSyouhinMeiNull())
                {
                    TbxAtai.Text = dr.SyouhinMei;
                }
                if (!dr.IsMediaNull())
                {
                    DdlFilterItem.SelectedValue = dr.Media;
                }
            }

        }

        protected void Filter_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int a = e.Item.ItemIndex;
            if (e.CommandName == "Delete")
            {
                DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
                for (int i = 0; i < FilterGrid.Items.Count; i++)
                {
                    if (i != a)
                    {
                        DropDownList DdlKoumoku = FilterGrid.Items[i].FindControl("DdlKoumoku") as DropDownList;
                        TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                        DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                        DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                        if (DdlKoumoku.Text == "--選択して下さい--")
                        {
                            break;
                        }
                        dr.SyouhinCode = DdlKoumoku.Text;
                        dr.SyouhinMei = TbxAtai.Text;
                        dr.Media = DdlFilterItem.SelectedValue;
                        dt.AddT_RowRow(dr);
                    }
                }
                FilterGrid.DataSource = dt;
                FilterGrid.DataBind();
            }
        }

        internal string GetWhere()
        {
            string Koumoku = "";
            string Atai = "";
            for (int i = 0; i < FilterGrid.Items.Count; i++)
            {
                DropDownList DdlKoumoku = FilterGrid.Items[i].FindControl("DdlKoumoku") as DropDownList;
                TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                if (Koumoku != "")
                {
                    Koumoku += "AND" + " " + DdlKoumoku.Text;
                }
                else if (DdlKoumoku.Text != "--選択して下さい--")
                {
                    if (DdlKoumoku.Text == "公共図書館" ||
                        DdlKoumoku.Text == "学校図書館" ||
                        DdlKoumoku.Text == "防衛省" ||
                        DdlKoumoku.Text == "その他図書館" ||
                        DdlKoumoku.Text == "ホテル" ||
                        DdlKoumoku.Text == "レジャーホテル" ||
                        DdlKoumoku.Text == "バス" ||
                        DdlKoumoku.Text == "船舶" ||
                        DdlKoumoku.Text == "上映会" ||
                        DdlKoumoku.Text == "カフェ" ||
                        DdlKoumoku.Text == "健康ランド" ||
                        DdlKoumoku.Text == "福祉施設" ||
                        DdlKoumoku.Text == "キッズ・BGV" ||
                        DdlKoumoku.Text == "その他" ||
                        DdlKoumoku.Text == "VOD" ||
                        DdlKoumoku.Text == "VOD配信" ||
                        DdlKoumoku.Text == "DOD" ||
                        DdlKoumoku.Text == "DEX")
                    {
                        Koumoku = "CategoryCode";
                        switch (DdlKoumoku.Text)
                        {
                            case "公共図書館":
                                Atai += "101";
                                break;
                            case "学校図書館":
                                Atai += "102";
                                break;
                            case "防衛省":
                                Atai += "103";
                                break;
                            case "その他図書館":
                                Atai += "109";
                                break;
                            case "ホテル":
                                Atai += "201";
                                break;
                            case "レジャーホテル":
                                Atai += "202";
                                break;
                            case "バス":
                                Atai += "203";
                                break;
                            case "船舶":
                                Atai += "204";
                                break;
                            case "上映会":
                                Atai += "205";
                                break;
                            case "カフェ":
                                Atai += "206";
                                break;
                            case "健康ランド":
                                Atai += "207";
                                break;
                            case "福祉施設":
                                Atai += "208";
                                break;
                            case "キッズ・BGV":
                                Atai += "209";
                                break;
                            case "その他":
                                Atai += "299";
                                break;
                            case "VOD":
                                Atai += "301";
                                break;
                            case "VOD配信":
                                Atai += "302";
                                break;
                            case "DOD":
                                Atai += "401";
                                break;
                            case "DEX":
                                Atai += "402";
                                break;
                        }
                        switch (DdlFilterItem.Text)
                        {
                            case "EqualTo":
                                Koumoku += " =" + " " + "'" + Atai + "'";
                                break;
                            case "NotEqualTo":
                                Koumoku += " !=" + " " + "'" + Atai + "'";
                                break;
                            default:
                                Koumoku += " =" + " " + "'" + Atai + "'";
                                break;
                        }
                    }
                    else
                    {
                        Koumoku = DdlKoumoku.Text;
                        Atai = TbxAtai.Text.Trim();
                        switch (DdlFilterItem.Text)
                        {
                            case "EqualTo":
                                Koumoku += " =" + " " + "'" + Atai + "'";
                                break;
                            case "NotEqualTo":
                                Koumoku += " !=" + " " + "'" + Atai + "'";
                                break;
                            case "Contains":
                                Koumoku += " like" + " " + "'%" + Atai + "%'";
                                break;
                            case "DoesNotContain":
                                Koumoku += " not like" + " " + "'%" + Atai + "%'";
                                break;
                            case "StartWith":
                                Koumoku += " like" + " " + "'" + Atai + "%'";
                                break;
                            case "EndWith":
                                Koumoku += " like" + " " + "'%" + Atai + "'";
                                break;
                        }
                    }
                }
            }
            return Koumoku;
        }
    }
}