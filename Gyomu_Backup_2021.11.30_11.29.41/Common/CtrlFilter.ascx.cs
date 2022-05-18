using Gyomu;
using Gyomu.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Common
{
    public partial class CtrlFilter : System.Web.UI.UserControl
    {
        private int VsListID
        {
            get
            {
                return Convert.ToInt32(this.ViewState["VsListID"]);
            }
            set
            {
                this.ViewState["VsListID"] = value;
            }
        }

        public Telerik.Web.UI.RadAjaxManagerProxy RadAjaxManagerProxy
        {
            get;
            set;
        }
        public Telerik.Web.UI.RadAjaxManager RadAjaxManager
        {
            get;
            set;
        }

        public Telerik.Web.UI.RadAjaxLoadingPanel RadAjaxLoadingPanel
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            D.Attributes["bordercolor"] = "black";
            B.Style["display"] = "none";
        }

        public void Create(int nListID, int nRowCount)
        {
            this.VsListID = nListID;

            Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(nListID).SqlDataFactory;

            this.D.DataSource = new int[nRowCount];
            this.D.DataBind();

            for (int i = 0; i < this.D.Rows.Count; i++)
            {
                DropDownList ddl = D.Rows[i].FindControl("I") as DropDownList;
                ddl.Items.Add(new ListItem("選択なし", ""));
                ddl.Attributes["onchange"] = string.Format("javascript:__doPostBack('{0}','{1}');", this.B.UniqueID, i);


                for (int c = 0; c < sqlData.Columns.Count; c++)
                {
                    Core.Sql.ColumnInfo ci = sqlData.Columns[c];
                    if (!ci.Hide && ci.ColumnType == Core.Sql.EnumColumnType.DBField)
                        ddl.Items.Add(new ListItem(ci.Caption, ci.FieldName));
                }

                System.Web.UI.HtmlControls.HtmlGenericControl div = D.Rows[i].FindControl("V") as System.Web.UI.HtmlControls.HtmlGenericControl;
                div.Style["display"] = "none";
            }
        }


        public string GetFilter(SqlCommand cmd)
        {
            Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(this.VsListID).SqlDataFactory;
            Core.Sql.WhereGenerator w = new Core.Sql.WhereGenerator();
            for (int i = 0; i < D.Rows.Count; i++)
            {
                DropDownList ddl = D.Rows[i].FindControl("I") as DropDownList;
                if (0 == ddl.SelectedIndex) continue;

                Core.Sql.ColumnInfo ci = sqlData.Columns[ddl.SelectedValue];

                TypeCode tp = Type.GetTypeCode(sqlData.Columns[ddl.SelectedValue].DataColumn.DataType);

                string strWhere = "";

                switch (tp)
                {
                    case TypeCode.Boolean:
                        CheckBox chk = D.Rows[i].FindControl("CK") as CheckBox;
                        strWhere = string.Format("{0}=@{0}", ci.FieldName);
                        cmd.Parameters.AddWithValue("@" + ci.FieldName, chk.Checked);
                        break;
                    case TypeCode.DateTime:
                        CtlNengappiForm n = D.Rows[i].FindControl("N") as CtlNengappiForm;
                        if (null == n.From)
                            throw new Exception("値を入力してください。" + ":" + ddl.SelectedItem.Text);
                        if (n.KikanType == Core.Type.NengappiKikan.EnumKikanType.FROM)
                        {
                            if (null == n.To)
                                throw new Exception("値を入力してください。" + ":" + ddl.SelectedItem.Text);
                        }
                        strWhere = n.GetNengappiKikan().GenerateSQLAsDateTime(ci.FieldName);
                        break;
                    default:
                        Core.Web.FilterTextBox t = D.Rows[i].FindControl("F") as Core.Web.FilterTextBox;
                        t.TypeCode = Type.GetTypeCode(ci.DataColumn.DataType);
                        t.Caption = ci.Caption;
                        Core.Sql.FilterItem f = t.GetFilterItem();
                        if (null == f)
                        {
                            throw new Exception("値を入力してください。" + ":" + ddl.SelectedItem.Text);
                        }
                        strWhere = t.GetFilterItem().GetFilterText(ddl.SelectedValue, "@" + i.ToString(), cmd);
                        break;
                }
                w.Add(strWhere);
            }
            return w.WhereText;
        }

        protected void B_Click(object sender, EventArgs e)
        {
            Core.Sql.SqlDataFactory sqlData = SessionManager.User.GetUserView(this.VsListID).SqlDataFactory;
            int nIndex = int.Parse(this.Request.Params["__EVENTARGUMENT"]);
            DropDownList ddl = D.Rows[nIndex].FindControl("I") as DropDownList;


            System.Web.UI.HtmlControls.HtmlGenericControl div = D.Rows[nIndex].FindControl("V") as System.Web.UI.HtmlControls.HtmlGenericControl;
            div.Style["display"] = (0 < ddl.SelectedIndex) ? "" : "none";

            if (0 < ddl.SelectedIndex)
            {
                Core.Web.FilterTextBox t = D.Rows[nIndex].FindControl("F") as Core.Web.FilterTextBox;
                t.Visible = false;

                CheckBox chk = D.Rows[nIndex].FindControl("CK") as CheckBox;
                chk.Visible = false;

                CtlNengappiForm n = D.Rows[nIndex].FindControl("N") as CtlNengappiForm;
                n.Visible = false;

                TypeCode tp = Type.GetTypeCode(sqlData.Columns[ddl.SelectedValue].DataColumn.DataType);

                switch (tp)
                {
                    case TypeCode.Boolean:
                        chk.Visible = true;
                        break;
                    case TypeCode.DateTime:
                        n.KikanType = Core.Type.NengappiKikan.EnumKikanType.ONEDAY;
                        n.Visible = true;
                        n.RadDatePickerFrom.DateInput.Attributes["OnBlur"] = string.Format("DateInput('{0}');", n.RadDatePickerFrom.ClientID);
                        n.RadDatePickerFrom.DateInput.Attributes["onkeydown"] = string.Format("DateInputE('{0}');", n.RadDatePickerFrom.ClientID);
                        n.RadDatePickerTo.DateInput.Attributes["OnBlur"] = string.Format("DateInput('{0}');", n.RadDatePickerTo.ClientID);
                        n.RadDatePickerTo.DateInput.Attributes["onkeydown"] = string.Format("DateInputE('{0}');", n.RadDatePickerTo.ClientID);
                        break;
                    case TypeCode.String:
                        t.TypeCode = tp;
                        t.FilterItems.Clear();
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.EqualTo, "と等しい"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.NotEqualTo, "と等しくない"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.Contains, "を含む"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.DoesNotContain, "を含まない"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.StartsWith, "で始まる"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.EndsWith, "で終わる"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.IsEmpty, "空白"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.IsNotEmpty, "空白でない"));
                        t.Visible = true;
                        break;
                    default:
                        t.TypeCode = tp;
                        t.FilterItems.Clear();
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.EqualTo, "と等しい"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.NotEqualTo, "と等しくない"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.GreaterThanOrEqualTo, "以上"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.LessThanOrEqualTo, "以下"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.GreaterThan, "より大きい"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.LessThan, "より小さい"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.IsNull, "NULL"));
                        t.FilterItems.Add(new Core.Web.FilterItem(Core.Sql.EnumFilterType.NotIsNull, "NULLでない"));
                        t.Visible = true;
                        break;
                }
            }

            if (null != this.RadAjaxManagerProxy)
                this.RadAjaxManagerProxy.AjaxSettings.AddAjaxSetting(B, div);
            else if (null != this.RadAjaxManager)
                this.RadAjaxManager.AjaxSettings.AddAjaxSetting(B, div);
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (null != this.RadAjaxManagerProxy)
            {
                this.RadAjaxManagerProxy.AjaxSettings.AddAjaxSetting(B, B);
            }
            else if (null != this.RadAjaxManager)
            {
                this.RadAjaxManager.AjaxSettings.AddAjaxSetting(B, B);
            }

        }

        
    }
}