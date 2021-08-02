using System;
using DLL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Master
{
    public partial class MasterOshirase : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Create();
            }
        }

        private void Create()
        {
            DataMaster.T_OshiraseDataTable dtB = ClassMaster.GetOshirase(Global.GetConnection());
            SessionManager.SessionData(dtB);
            ClassKensaku.KensakuParam p = KensakuParam();
            DataMaster.T_OshiraseDataTable dt = ClassKensaku.GetOshirase(p, Global.GetConnection());
            if (dt.Count > 0)
            {
                DGOshirase.DataSource = dt;
                DGOshirase.DataBind();
            }
            else
            {
                ErrorSet(1);
            }
        }


        private void ErrorSet(int v)
        {
            switch (v)
            {
                case 1:
                    LblErr.Text = "お知らせデータがありませんでした。";
                    break;

                case 2:
                    LblErr.Text = "お知らせ内容が入力されていません";
                    break;

                case 3:
                    LblErr.Text = "お知らせの登録ができませんでした。";
                    break;

                case 4:
                    LblErr.Text = "お知らせの削除に失敗しました。";
                    break;
            }
        }

        private ClassKensaku.KensakuParam KensakuParam()
        {
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();
            if (TbxUserName.Text != "")
            {
                p.CreateUser = TbxUserName.Text;
            }
            if (RdpCreateDate.SelectedDate != null)
            {
                p.CreateOshiraseDate = RdpCreateDate.SelectedDate.Value.ToShortDateString().Replace("/", "-");
            }
            if (TbxNaiyou.Text != "")
            {
                p.OshiraseNaiyou = TbxNaiyou.Text;
            }
            return p;
        }

        protected void DGOshirase_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataMaster.T_OshiraseRow dr = (e.Item.DataItem as DataRowView).Row as DataMaster.T_OshiraseRow;
                Label LblOshirase = e.Item.FindControl("LblOshirase") as Label;
                Label LblUserName = e.Item.FindControl("LblUserName") as Label;
                Label LblCreateDate = e.Item.FindControl("LblCreateDate") as Label;
                Label LblAccept = e.Item.FindControl("LblAccept") as Label;
                HtmlInputHidden HidOshiraseNoRow = e.Item.FindControl("HidOshiraseNoRow") as HtmlInputHidden;


                LblOshirase.Text = dr.OshiraseNaiyou;
                LblUserName.Text = dr.CreateUser;
                LblCreateDate.Text = dr.CreateDate.ToString();
                HidOshiraseNoRow.Value = dr.OshiraseNo;
                if (!dr.IsacceptNull())
                {
                    if (dr.accept == "1")
                    {
                        LblAccept.Text = "✓";
                    }
                }
            }
        }

        protected void DGOshirase_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            HtmlInputHidden HidOshiraseNoRow = e.Item.FindControl("HidOshiraseNoRow") as HtmlInputHidden;
            if (e.CommandName == "Edit")
            {
                LblStatus.Text = "編集";
                DivList.Style["display"] = "none";
                DivCreate.Style["display"] = "block";
                DataMaster.T_OshiraseRow dr = ClassMaster.GetOshiraseRow(HidOshiraseNoRow.Value, Global.GetConnection());
                if (dr != null)
                {
                    TbxOshiraseNaiyou.Text = dr.OshiraseNaiyou.Replace("<br>", "\r\n");
                    HidOshiraseNo.Value = dr.OshiraseNo;
                    if (!dr.IsacceptNull())
                    {
                        if (dr.accept == "1")
                        {
                            ChkAccept.Checked = true;
                        }
                        if (dr.accept == "")
                        {
                            ChkAccept.Checked = false;
                        }
                    }
                }
            }
            if (e.CommandName == "Delete")
            {
                try
                {
                    ClassMaster.DeleteOshirase(HidOshiraseNoRow.Value, Global.GetConnection());
                }
                catch (Exception ex)
                {
                    ErrorSet(4);
                }
                Create();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Create();
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            DivList.Style["display"] = "none";
            DivCreate.Style["display"] = "block";
            DataMaster.T_OshiraseDataTable dt = SessionManager.SESSION_DT as DataMaster.T_OshiraseDataTable;
            HidOshiraseNo.Value = (int.Parse(dt[0].OshiraseNo) + 1).ToString();
            LblStatus.Text = "新規投稿";
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            DataMaster.T_OshiraseDataTable dt = new DataMaster.T_OshiraseDataTable();
            DataMaster.T_OshiraseRow dr = dt.NewT_OshiraseRow();
            if (TbxOshiraseNaiyou.Text != "")
            {
                try
                {
                    dr.OshiraseNaiyou = TbxOshiraseNaiyou.Text.Replace("\r\n", "<br>");
                    dr.CreateDate = DateTime.Now;
                    dr.CreateUser = SessionManager.User.UserName;
                    dr.OshiraseNo = HidOshiraseNo.Value;
                    if (ChkAccept.Checked)
                    {
                        dr.accept = "1";
                    }
                    else
                    {
                        dr.accept = "";
                    }
                    dt.AddT_OshiraseRow(dr);
                    ClassMaster.UpdateOshirase(dt, Global.GetConnection());
                    LblEnd.Text = "お知らせを登録しました。";
                }
                catch
                {
                    ErrorSet(3);
                }
            }
            else
            {
                ErrorSet(2);
            }
        }

        protected void BtnList_Click(object sender, EventArgs e)
        {
            LblEnd.Text = "";
            DivCreate.Style["display"] = "none";
            DivList.Style["display"] = "block";
            Create();
        }
    }
}