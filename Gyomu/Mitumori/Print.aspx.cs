using System;
using Telerik.Web.UI;
using WebSupergoo.ABCpdf6;


namespace Gyomu.Mitumori
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LblEnd.Text = "";
                err.Text = "";
                string url = Request.RawUrl;
                string[] strAry = url.Split('=');
                string mNo = strAry[1];
                Create(mNo);
            }
        }

        private void Create(string mNo)
        {
            LblMitumoriNo.Text = mNo.Trim();
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            string str = LblMitumoriNo.Text;
            string[] strMitumoriAry = str.Split(',');

            string PriFormat = "";

            for (int j = 0; j < CheckBoxList1.Items.Count; j++)
            {
                if (CheckBoxList1.Items[j].Selected)
                {
                    if (PriFormat != "")
                    {
                        PriFormat += ",";
                        PriFormat += CheckBoxList1.Items[j].Value;
                    }
                    else
                    {
                        PriFormat += CheckBoxList1.Items[j].Value;
                    }
                }
            }

            Doc pdf = new Doc();
            string flg = "";
            string bDate = "";
            if (ChkName.Checked)
            {
                flg = "0";
            }
            else
            {
                flg = "1";
            }
            if (ChkDate.Checked)
            {
                bDate = "true";
            }
            else
            {
                if (RdpDate.SelectedDate.ToString() != "")
                {
                    bDate = RdpDate.SelectedDate.Value.ToLongDateString();
                }
                else
                {
                    bDate = "false";
                }
            }

            string[] strMAry = PriFormat.Split(',');
            for (int i = 0; i < strMAry.Length; i++)
            {
                string Type = strMAry[i];
                AppCommon acApp = new AppCommon();

                acApp.MitumoriInsatu2(Type, pdf, strMitumoriAry, flg, bDate);
                AppCommon.strShisetumei = "";
                string sArg = "";
                if (acApp.theData != null)
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary(DateTime.Now + "書類印刷_" + str + "_" + PriFormat + ".pdf", acApp.theData);
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + sArg)));
                    LblEnd.Text = "印刷完了";
                }
                else
                {
                    err.Text = "入力データに不備があり、書類を作成をすることができませんでした。";
                }
            }
            AppCommon.strShisetumei = "";
        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected void ChkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDate.Checked)
            {
                DivDate.Style["display"] = "none";
            }
            else
            {
                DivDate.Style["display"] = "";
            }
        }
    }
}