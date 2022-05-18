using DLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Master
{
    public partial class CtlTanto : System.Web.UI.UserControl
    {
        public static int dtcount = 0;

        private String vsID
        {
            get
            {
                object obj = this.ViewState["vsID"];
                if (null == obj) return "";
                return Convert.ToString(obj);
            }
            set
            {
                this.ViewState["vsID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //部門DropDownList
                // ListSet.SetBumon(RadBumon);

                //NewCreate();
            }
        }

        //private void NewCreate()
        //{
        //    throw new NotImplementedException();
        //}

        internal void Create(string userId)
        {
            //前の登録データが残るので一旦clearする
            //Clear();

            DataMaster.M_TantoDataTable dt =
                ClassMaster.GetM_Tanto2(userId, Global.GetConnection());
            vsID = userId;

            for (int i = 0; i < dt.Count; i++)
            {
                if (!dt[i].IsUserNameNull())
                { TbxName.Text = dt[i].UserName; }
                TbxId.Text = dt[i].UserID.ToString();
                if (!dt[i].IsYomikataNull())
                { TbxYomi.Text = dt[i].Yomikata; }
                if (!dt[i].IsPasswordNull())
                {
                    TbxPass.Text = dt[i].Password;
                }
            }

            CtlBumon.DataSource = dt;
            CtlBumon.DataBind();
        }

        internal bool Toroku()
        {
            int count = 0;
            DataMitumori.T_RowDataTable dd = new DataMitumori.T_RowDataTable();
            for (int j = 0; j < CtlBumon.Items.Count; j++)
            {
                DataMitumori.T_RowRow dr = dd.NewT_RowRow();
                TantoBumon bumon = CtlBumon.Items[j].FindControl("Syosai") as TantoBumon;
                RadioButtonList Yuko = (RadioButtonList)bumon.FindControl("RadioButtonList1");
                if (Yuko.SelectedValue == "True")
                {
                    count += 1;
                }
            }
            if (count >= 2)
            {
                Err.Text = "登録できませんでした。表示できる部署は1つだけです。";
                return false;
            }
            else
            {
                for (int i = 0; i < CtlBumon.Items.Count; i++)
                {
                    TantoBumon bumon = CtlBumon.Items[i].FindControl("Syosai") as TantoBumon;
                    RadComboBox radBusyo = (RadComboBox)bumon.FindControl("RadBumon");
                    RadioButtonList Yuko = (RadioButtonList)bumon.FindControl("RadioButtonList1");
                    HtmlInputHidden HidRowNo = (HtmlInputHidden)bumon.FindControl("HidRowNo");
                    HtmlInputHidden HidKeyNo = (HtmlInputHidden)bumon.FindControl("HidKeyNo");
                    Err.Text = "";
                    try
                    {
                        DataMaster.M_TantoDataTable dt = new DataMaster.M_TantoDataTable();
                        DataMaster.M_TantoRow dr = dt.NewM_TantoRow();

                        if (TbxId.Text != "")
                        {
                            dr.UserID = int.Parse(TbxId.Text);

                            DataSet1.M_TantoRow Tdr =
                                ClassMaster.GetM_TantoRow(TbxId.Text, Global.GetConnection());

                            if (Tdr != null)
                            {
                                if (vsID != "")
                                {
                                    if (Tdr.UserID != int.Parse(vsID))
                                    {
                                        Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("このIDは既に存在しています。");
                                        return false;
                                    }
                                }
                                else
                                {
                                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("このIDは既に存在しています。");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("IDを登録してください。");
                            return false;
                        }

                        if (TbxYomi.Text != "")
                        { dr.Yomikata = TbxYomi.Text; }
                        else
                        { Err.Text += "読み方を入力してください。"; }


                        if (TbxName.Text != "")
                        { dr.UserName = TbxName.Text; }
                        else
                        { Err.Text += "名前を入力してください。"; }

                        if (radBusyo.Text != "")
                        {
                            dr.Busyo = radBusyo.Text;
                            dr.BumonName = radBusyo.Text;
                            DataDrop.M_BumonRow drr = ClassDrop.GetBumonCode(radBusyo.Text, Global.GetConnection());
                            dr.Bumon = drr.BumonKubun;
                        }
                        else
                        {
                            int b = int.Parse(radBusyo.SelectedValue);
                            dr.BumonKubun = b;
                        }

                        //if (RadBumon.SelectedValue != "")
                        //{
                        //    dr.Bumon = 1;
                        //    dr.BumonKubun = int.Parse(RadBumon.SelectedValue);
                        //    dr.Busyo = RadBumon.SelectedItem.Text;
                        //}
                        //else
                        //{
                        //    int b = int.Parse(RadBumon.SelectedValue);
                        //    dr.BumonKubun = b;
                        //}

                        if (Yuko.SelectedValue != "True")
                        {
                            bool bYuko = bool.Parse(Yuko.SelectedValue);
                            dr.Yuko = bYuko;
                        }
                        else
                        {
                            bool fYuko = bool.Parse(Yuko.SelectedValue);
                            dr.Yuko = fYuko;
                        }

                        if (TbxPass.Text != "")
                        { dr.Password = TbxPass.Text; }
                        else
                        { Err.Text += "パスワードを入力してください。"; }
                        string key = "";
                        if (HidKeyNo.Value != "")
                        {
                            key = HidKeyNo.Value;
                        }

                        string ui = TbxId.Text;

                        if (key != "")
                        {
                            dr.UserKey = int.Parse(key);
                            dr.RowNo = int.Parse(HidRowNo.Value);
                            dt.AddM_TantoRow(dr);
                            ClassMaster.EditTanto(vsID, dt, HidRowNo.Value, Global.GetConnection());
                        }
                        else
                        {
                            DataMaster.M_TantoRow dtM = ClassMaster.TantoKey(Global.GetConnection());
                            DataMaster.M_TantoDataTable dtU = ClassMaster.TantoRow(ui, Global.GetConnection());
                            if (dtU.Count != 0)
                            {
                                dr.UserKey = dtM.UserKey + 1;
                                dr.RowNo = dtU[0].RowNo + 1;
                                dt.AddM_TantoRow(dr);
                            }
                            else
                            {
                                dr.UserKey = dtM.UserKey + 1;
                                dr.RowNo = 1;
                                dt.AddM_TantoRow(dr);
                            }
                            ClassMaster.NewTanto(dt, Global.GetConnection());
                        }

                        //if (vsID != "")
                        //{
                        //    ClassMaster.EditTanto(vsID, dt, Global.GetConnection());
                        //}
                        //else
                        //{
                        //    ClassMaster.NewTanto(dt, Global.GetConnection());
                        //}
                        //return true;

                    }
                    catch 
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        internal void Clear()
        {
            TbxId.Text = TbxYomi.Text = TbxName.Text = TbxPass.Text = "";
            //RadBumon.SelectedValue = "";
            // LstYuko.SelectedValue = "True";
        }

        protected void CtlBumon_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMaster.M_TantoRow dr = (e.Item.DataItem as DataRowView).Row as DataMaster.M_TantoRow;
                DataMitumori.T_RowRow df = (e.Item.DataItem as DataRowView).Row as DataMitumori.T_RowRow;

                TantoBumon tb = e.Item.FindControl("Syosai") as TantoBumon;
                RadComboBox bumon = (RadComboBox)tb.FindControl("RadBumon");
                RadioButtonList Yuko = (RadioButtonList)tb.FindControl("RadioButtonList1");
                HtmlInputHidden HidRowNo = (HtmlInputHidden)tb.FindControl("HidRowNo");
                HtmlInputHidden HidKeyNo = (HtmlInputHidden)tb.FindControl("HidKeyNo");


                if (df == null)
                {
                    if (!dr.IsBumonNull())
                    {
                        bumon.SelectedItem.Text = dr.BumonName;
                        bumon.SelectedValue = dr.Bumon.ToString();
                    }
                    Yuko.SelectedValue = dr.Yuko.ToString();
                    HidRowNo.Value = dr.RowNo.ToString();
                    HidKeyNo.Value = dr.UserKey.ToString();
                }
                else
                {
                    if (!df.IsBasyoNull())
                    {
                        bumon.SelectedItem.Text = df.Basyo;
                    }

                    if (!df.IsKariFLGNull())
                    {
                        Yuko.SelectedValue = df.KariFLG;
                    }
                    if (!df.IsHattyuGokeiNull())
                    {
                        HidKeyNo.Value = df.HattyuGokei.ToString();
                    }
                    HidRowNo.Value = df.Media;
                }
            }
        }

        protected void CtlBumon_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int a = e.Item.ItemIndex;
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            int l = 0;
            for (int i = 0; i < CtlBumon.Items.Count; i++)
            {
                l++;
                TantoBumon tb = CtlBumon.Items[i].FindControl("Syosai") as TantoBumon;
                RadComboBox bumon = (RadComboBox)tb.FindControl("RadBumon");
                RadioButtonList Yuko = (RadioButtonList)tb.FindControl("RadioButtonList1");
                HtmlInputHidden HidKeyNo = (HtmlInputHidden)tb.FindControl("HidKeyNo");

                DataMitumori.T_RowRow dr = dt.NewT_RowRow();

                if (bumon.Text != "")
                {
                    dr.Basyo = bumon.Text;
                }
                if (Yuko.SelectedValue == "True")
                {
                    dr.KariFLG = "True";
                }
                else
                {
                    dr.KariFLG = "False";
                }
                dr.Media = (i + 1).ToString();
                if (HidKeyNo.Value != "")
                {
                    dr.HattyuGokei = int.Parse(HidKeyNo.Value);
                }
                dt.AddT_RowRow(dr);
                if (a == i)
                {
                    dt.RemoveT_RowRow(dr);
                    ClassMaster.DelTantoBusyo(TbxId.Text, bumon.Text, Global.GetConnection());
                }
            }
            DelCreate(dt);
        }

        private void DelCreate(DataMitumori.T_RowDataTable dt)
        {
            CtlBumon.DataSource = dt;
            CtlBumon.DataBind();
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            DataMitumori.T_RowDataTable dt = new DataMitumori.T_RowDataTable();
            int l = 0;
            for (int i = 0; i < CtlBumon.Items.Count; i++)
            {
                l++;
                TantoBumon tb = CtlBumon.Items[i].FindControl("Syosai") as TantoBumon;
                RadComboBox bumon = (RadComboBox)tb.FindControl("RadBumon");
                RadioButtonList Yuko = (RadioButtonList)tb.FindControl("RadioButtonList1");
                HtmlInputHidden HidKeyNo = (HtmlInputHidden)tb.FindControl("HidKeyNo");

                DataMitumori.T_RowRow dr = dt.NewT_RowRow();

                if (bumon.Text != "")
                {
                    dr.Basyo = bumon.Text;
                }
                if (Yuko.SelectedValue == "True")
                {
                    dr.KariFLG = "True";
                }
                else
                {
                    dr.KariFLG = "False";
                }
                dr.Media = (i + 1).ToString();
                if (HidKeyNo.Value != "")
                {
                    dr.HattyuGokei = int.Parse(HidKeyNo.Value);
                }
                dt.AddT_RowRow(dr);
            }
            DataMitumori.T_RowRow dl = dt.NewT_RowRow();
            l++;
            dl.Media = l.ToString();
            dt.AddT_RowRow(dl);

            CtlBumon.DataSource = dt;
            CtlBumon.DataBind();
        }

    }
}