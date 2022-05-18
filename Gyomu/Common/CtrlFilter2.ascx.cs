using System;
using Telerik.Web.UI;
using DLL;
using System.Data;
using System.Web.UI.WebControls;

namespace Gyomu.Common
{
    public partial class CtrlFilter2 : System.Web.UI.UserControl
    {
        public static string[] strKoumokuAry;
        public static DataMaster.T_ColumnListDataTable dtColumn;
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
                RadComboBox RcbKoumoku = FilterGrid.Items[i].FindControl("RcbKoumoku") as RadComboBox;
                TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                if (RcbKoumoku.Text != "")
                {
                    if (Koumoku != "")
                    {
                        Koumoku += "AND" + " " + RcbKoumoku.SelectedValue;
                    }
                    else
                    {
                        Koumoku = RcbKoumoku.SelectedValue;
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
                RadComboBox RcbKoumoku = FilterGrid.Items[i].FindControl("RcbKoumoku") as RadComboBox;
                TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                if (RcbKoumoku.Text == "")
                {
                    break;
                }
                dr.SyouhinCode = RcbKoumoku.SelectedValue;
                dr.SyouhinMei = TbxAtai.Text;
                dr.Media = DdlFilterItem.SelectedValue;
                dt.AddT_RowRow(dr);
            }
            DataMitumori.T_RowRow drN = dt.NewT_RowRow();
            dt.AddT_RowRow(drN);

            FilterGrid.DataSource = dt;
            FilterGrid.DataBind();
        }

        internal void KoumokuBind2(int intFieldNo)
        {
            DataMaster.T_ColumnListDataTable dt = ClassMaster.GetColumn(intFieldNo, Global.GetConnection());
            dtColumn = dt.Copy() as DataMaster.T_ColumnListDataTable;
        }

        protected void Filter_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMitumori.T_RowRow dr = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;
                RadComboBox RcbKoumoku = e.Item.FindControl("RcbKoumoku") as RadComboBox;
                TextBox TbxAtai = e.Item.FindControl("TbxAtai") as TextBox;
                DropDownList DdlFilterItem = e.Item.FindControl("DdlFilterItem") as DropDownList;


                if (dtColumn != null)
                {
                    RcbKoumoku.Items.Add(RcbKoumoku.EmptyMessage);
                    for (int i = 0; i < dtColumn.Count; i++)
                    {
                        RcbKoumoku.Items.Add(new RadComboBoxItem(dtColumn[i].DataColumnName, dtColumn[i].ViewColumnName));
                    }
                }
                if (!dr.IsSyouhinCodeNull())
                {
                    RcbKoumoku.SelectedValue = dr.SyouhinCode;
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
                        RadComboBox RcbKoumoku = FilterGrid.Items[i].FindControl("RcbKoumoku") as RadComboBox;
                        TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
                        DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

                        DataMitumori.T_RowRow dr = dt.NewT_RowRow();
                        if (RcbKoumoku.Text == "")
                        {
                            break;
                        }
                        dr.SyouhinCode = RcbKoumoku.SelectedValue;
                        dr.SyouhinMei = TbxAtai.Text;
                        dr.Media = DdlFilterItem.SelectedValue;
                        dt.AddT_RowRow(dr);
                    }
                }
                FilterGrid.DataSource = dt;
                FilterGrid.DataBind();
            }
        }

        internal string GetWhere(string strSyouhinCode)
        {
            string Koumoku = "";
            if (!string.IsNullOrEmpty(strSyouhinCode))
            {
                string[] ArySyouhinCode = strSyouhinCode.Split(',');
                for (int a = 0; a < ArySyouhinCode.Length; a++)
                {
                    if(string.IsNullOrEmpty(Koumoku))
                    {
                        Koumoku = "SyouhinCode = " + "'" + ArySyouhinCode[a] + "'";
                    }
                    else
                    {
                        Koumoku += " or " + "SyouhinCode = " + "'" + ArySyouhinCode[a] + "'";
                    }
                }
            }
            //for (int i = 0; i < FilterGrid.Items.Count; i++)
            //{
            //    string Atai = "";
            //    RadComboBox RcbKoumoku = FilterGrid.Items[i].FindControl("RcbKoumoku") as RadComboBox;
            //    TextBox TbxAtai = FilterGrid.Items[i].FindControl("TbxAtai") as TextBox;
            //    DropDownList DdlFilterItem = FilterGrid.Items[i].FindControl("DdlFilterItem") as DropDownList;

            //    if (Koumoku != "")
            //    {
            //        Koumoku += "AND" + " ";
            //    }
            //    if (RcbKoumoku.Text != "")
            //    {
            //        if (RcbKoumoku.Text == "公共図書館" ||
            //            RcbKoumoku.Text == "学校図書館" ||
            //            RcbKoumoku.Text == "防衛省" ||
            //            RcbKoumoku.Text == "その他図書館" ||
            //            RcbKoumoku.Text == "ホテル" ||
            //            RcbKoumoku.Text == "レジャーホテル" ||
            //            RcbKoumoku.Text == "バス" ||
            //            RcbKoumoku.Text == "船舶" ||
            //            RcbKoumoku.Text == "上映会" ||
            //            RcbKoumoku.Text == "カフェ" ||
            //            RcbKoumoku.Text == "健康ランド" ||
            //            RcbKoumoku.Text == "福祉施設" ||
            //            RcbKoumoku.Text == "キッズ・BGV" ||
            //            RcbKoumoku.Text == "その他" ||
            //            RcbKoumoku.Text == "VOD" ||
            //            RcbKoumoku.Text == "VOD配信" ||
            //            RcbKoumoku.Text == "DOD" ||
            //            RcbKoumoku.Text == "DEX")
            //        {
            //            //〇か×か
                        
            //            Koumoku += "CategoryCode";
            //            switch (RcbKoumoku.Text)
            //            {
            //                case "公共図書館":
            //                    Atai += "101";
            //                    break;
            //                case "学校図書館":
            //                    Atai += "102";
            //                    break;
            //                case "防衛省":
            //                    Atai += "103";
            //                    break;
            //                case "その他図書館":
            //                    Atai += "109";
            //                    break;
            //                case "ホテル":
            //                    Atai += "201";
            //                    break;
            //                case "レジャーホテル":
            //                    Atai += "202";
            //                    break;
            //                case "バス":
            //                    Atai += "203";
            //                    break;
            //                case "船舶":
            //                    Atai += "204";
            //                    break;
            //                case "上映会":
            //                    Atai += "205";
            //                    break;
            //                case "カフェ":
            //                    Atai += "206";
            //                    break;
            //                case "健康ランド":
            //                    Atai += "207";
            //                    break;
            //                case "福祉施設":
            //                    Atai += "208";
            //                    break;
            //                case "キッズ・BGV":
            //                    Atai += "209";
            //                    break;
            //                case "その他":
            //                    Atai += "299";
            //                    break;
            //                case "VOD":
            //                    Atai += "301";
            //                    break;
            //                case "VOD配信":
            //                    Atai += "302";
            //                    break;
            //                case "DOD":
            //                    Atai += "401";
            //                    break;
            //                case "DEX":
            //                    Atai += "402";
            //                    break;
            //            }
            //            switch (DdlFilterItem.Text)
            //            {
            //                case "EqualTo":
            //                    Koumoku += " =" + " " + "'" + Atai + "'";
            //                    break;
            //                case "NotEqualTo":
            //                    Koumoku += " !=" + " " + "'" + Atai + "'";
            //                    break;
            //                default:
            //                    Koumoku += " =" + " " + "'" + Atai + "'";
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            Koumoku += RcbKoumoku.SelectedValue;
            //            Atai = TbxAtai.Text.Trim();
            //            switch (DdlFilterItem.Text)
            //            {
            //                case "EqualTo":
            //                    Koumoku += " =" + " " + "'" + Atai + "'";
            //                    break;
            //                case "NotEqualTo":
            //                    Koumoku += " !=" + " " + "'" + Atai + "'";
            //                    break;
            //                case "Contains":
            //                    Koumoku += " like" + " " + "'%" + Atai + "%'";
            //                    break;
            //                case "DoesNotContain":
            //                    Koumoku += " not like" + " " + "'%" + Atai + "%'";
            //                    break;
            //                case "StartWith":
            //                    Koumoku += " like" + " " + "'" + Atai + "%'";
            //                    break;
            //                case "EndWith":
            //                    Koumoku += " like" + " " + "'%" + Atai + "'";
            //                    break;
            //            }
            //        }
            //    }
            //}
            return Koumoku;
        }
    }
}