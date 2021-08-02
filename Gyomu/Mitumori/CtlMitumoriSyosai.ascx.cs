using DLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI;

namespace Gyomu.Mitumori
{
    public partial class CtlMitumoriSyosai : System.Web.UI.UserControl
    {
        private List<string> clientIdList;

        public List<string> ClientIdList
        {
            get { return clientIdList; }
            set { clientIdList = value; }
        }

        /// 受注明細コントロールのIDをListに保存する。
        public void InitClientId()
        {
            // 明細番号
            this.clientIdList = new List<string>();

            this.clientIdList.Add(DltBtn.ClientID);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                //ListSet.SetSyohin(RadSyohinmeisyou);
                //ListSet.SetHattyuMei(RadMeisyo);
                //ListSet.SetTyokuso(RadShisetu);
            }
        }

        protected void BtnSyohin_Click(object sender, EventArgs e)
        {
            if (RadSyohinmeisyou.SelectedValue != "" && lblHattyusaki.Text!="")
            {
                string sValue = RadSyohinmeisyou.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                string sValue2 = lblHattyusaki.Text;
                string QueryString2 = string.Format("Value={0}", sValue2);

                //ボタンクリック時に得意先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SyouhinSyosai.aspx?" + "&" + QueryString +"&" + QueryString2);
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
            else
            {
                //ボタンクリック時に得意先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SyouhinSyosai.aspx");
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadSyohinmeisyou_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string sSyouhin = RadSyohinmeisyou.SelectedValue;
            //string sName = RadSyohinmeisyou.Text;

            if (lblCateG.Text != "")
            {
                DataMaster.M_Kakaku_NewRow dr =
                    ClassMaster.GetM_SyohinCate(sSyouhin, lblCateG.Text, Global.GetConnection());

                if (dr != null)
                {
                    lblHattyusaki.Text = dr.ShiireName;
                    TbxJutyuTanka.Value = dr.HyojunKakaku.ToString();
                    TbxHaccyuTanka.Value = dr.ShiireKakaku.ToString();
                    lblHinmei.Text = dr.SyouhinCode;
                    TbxHyojunKakaku.Value = dr.HyojunKakaku.ToString();

                    //商品名検索
                    DataMitumori.M_Syohin_NewRow Sdr =
                        ClassMitumori.GetMSyouhin(RadSyohinmeisyou.SelectedValue, lblHattyusaki.Text, Global.GetConnection());

                    RadSyohinmeisyou.Text = Sdr.SyouhinMei;
                    lblHinban.Text = Sdr.MekarHinban;
                    lblMedia.Text = Sdr.Media;
                    lblHani.Text = Sdr.Range;
                }
                else
                {
                    TbxJutyuTanka.Value = "";
                    TbxHaccyuTanka.Value = "";
                    TbxHyojunKakaku.Value = "";

                    if (RadSyohinmeisyou.SelectedValue != "" || RadSyohinmeisyou.SelectedValue == "-1")
                    {
                        Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("この得意先には商品情報がありません。");
                        return;
                    }
                }
            }
        }

        protected void RadSyohinmeisyou_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //カテゴリーが選択されていた場合商品検索可能
            if (lblCateG.Text != "")
            {
                ListSet.SetKensakuCateSyohin(lblCateG.Text,sender, e);
            }
        }

        protected void BtnShisetuSyosai_Click(object sender, EventArgs e)
        {
            if (RadShisetuName.SelectedValue != "")
            {
                string sValue = RadShisetuName.SelectedValue;
                string QueryString = string.Format("Value={0}", sValue);

                //ボタンクリック時に得意先詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SisetuSyosai.aspx?" + "&" + QueryString);
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
            else
            {
                //ボタンクリック時に施設詳細画面へ別タブで開く
                string url = string.Format("/Mitumori/Syosai/SisetuSyosai.aspx");
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(cstype, "OpenNewWindow", "window.open('" + url + "',null);", true);
            }
        }
    }
}