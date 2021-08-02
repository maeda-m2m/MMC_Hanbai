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
            bool bDate = false;
            if (ChkName.Checked)
            {
                flg = "1";
            }
            else
            {
                flg = "0";
            }
            if (ChkDate.Checked)
            {
                bDate = true;
            }

            string[] strMAry = PriFormat.Split(',');
            for (int i = 0; i < strMAry.Length; i++)
            {
                string Type = strMAry[i];
                AppCommon acApp = new AppCommon();

                acApp.MitumoriInsatu2(Type, pdf, strMitumoriAry, flg, bDate);

                string sArg = "";
                if (acApp.theData != null)
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary("InsatsuFormat" + DateTime.Now + ".pdf", acApp.theData);
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + sArg)));
                }
                else
                {
                    err.Text = "入力データに不備があり、書類を作成をすることができませんでした。";
                }
            }
        }

        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
    }
}