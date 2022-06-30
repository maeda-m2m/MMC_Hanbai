using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Yodokou_HanbaiKanri;

namespace Gyomu.Jutyu
{
    public partial class Jutyujoho : System.Web.UI.Page
    {
        const int LIST_ID_DL = 20;

        private string vsNo
        {
            get
            {
                object obj = this.ViewState["vsNo"];
                if (null == obj) return "";
                return Convert.ToString(obj);
            }
            set
            {
                this.ViewState["vsNo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                SessionManager.JucyuSyusei("");
                SessionManager.MitumoriType("");

                //得意先、仕入先DDropDownList
                ListSet.SetRyakusyou(RadTokuiMeisyo, RadSekyuMeisyo);

                //担当名DropDwonList
                ListSet.SetTanto(RadTanto);
                //ListSet.SetTanto(RadNyuryoku);

                //部門DropDownList
                ListSet.SetBumon(RadBumon);

                //CategoryDropDownList
                ListSet.SetCate(RadCate);

                //直送先・施設DropDownList
                ListSet.SetTyokuso(RadTyokusoMeisyo, RadSisetMeisyo);
                //ListSet.SetTyokuso(RadSisetMeisyo);

                //商品情報DropDownList
                ListSet.SetSyohin(RadSyohinmeisyou);

                //Create();
            }
        }

        private void Create()
        {
            try
            {
                ClassKensaku.KensakuParam k = SetKensakuParam();

                DataJutyu.T_JutyuHeaderDataTable dt =
                    ClassKensaku.GetMitumori(k, Global.GetConnection());

                //DataJutyu.T_JutyuHeaderDataTable dt = ClassJutyu.GetMitumori(Global.GetConnection());

                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "データがありません";
                    this.RadG.Visible = false;
                    return;
                }
                else
                {
                    this.RadG.Visible = true;
                }

                this.RadG.VirtualItemCount = dt.Count;

                int nPageSize = this.RadG.PageSize;
                int nPageCount = dt.Count / nPageSize;
                if (0 < dt.Count % nPageSize) nPageCount++;
                if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

                DataTable dtC = new DataTable();

                dtC = dt;

                this.RadG.DataSource = dtC;
                this.RadG.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        private ClassKensaku.KensakuParam SetKensakuParam()
        {
            ClassKensaku.KensakuParam k = new ClassKensaku.KensakuParam();

            if (DrpFlg.SelectedValue != "")
            {
                k.sFlg = DrpFlg.SelectedValue;
            }
            if (TbxJutyuNo.Text != "")
            {
                k.sJutyuNo = TbxJutyuNo.Text;
            }
            if (RadTokuiMeisyo.SelectedValue != "" && RadTokuiMeisyo.SelectedValue != "-1")
            {
                k.sTokuisaki = RadTokuiMeisyo.SelectedValue;
            }
            if (RadSekyuMeisyo.SelectedValue != "" && RadSekyuMeisyo.SelectedValue != "-1")
            {
                k.sSeikyu = RadSekyuMeisyo.SelectedValue;
            }
            if (RadTyokusoMeisyo.SelectedValue != "" && RadTyokusoMeisyo.SelectedValue != "-1")
            {
                k.sTyokuso = RadTyokusoMeisyo.SelectedValue;
            }
            if (RadSisetMeisyo.SelectedValue != "" && RadSisetMeisyo.SelectedValue != "-1")
            {
                k.sSisetu = RadSisetMeisyo.SelectedValue;
            }
            if (RadCate.SelectedValue != "")
            {
                k.sCate = RadCate.SelectedValue;
            }
            if (RadBumon.SelectedValue != "")
            {
                k.sBumon = RadBumon.SelectedValue;
            }

            if (RadSyohinmeisyou.SelectedValue != "" && RadSyohinmeisyou.SelectedValue != "-1")
            {
                k.sHinmei = RadSyohinmeisyou.SelectedValue;
            }
            if (RadTanto.SelectedValue != "")
            {
                k.sTanto = RadTanto.SelectedItem.Text;
            }
            //if (RadNyuryoku.SelectedValue != "")
            //{
            //    k.sNyuryoku = RadNyuryoku.SelectedItem.Text;
            //}

            Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlJucyuBi") as Common.CtlNengappiForm;
            if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
            {
                k.JutyuBi = this.CtlJucyuBi.GetNengappiKikan();
            }
            return k;
        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                DataJutyu.T_JutyuHeaderRow dr = (DataJutyu.T_JutyuHeaderRow)drv.Row;

                int rgItemIndex = e.Item.ItemIndex;

                string sKey = dr.JutyuNo.ToString();

                HtmlInputCheckBox chk = e.Item.FindControl("ChkRow") as HtmlInputCheckBox;

                chk.Value = string.Format("{0}_{1}_{2}", dr.JutyuNo, dr.FacilityName, dr.CateGory);


                //Button BtnOrder = e.Item.FindControl("BtnOrder") as Button;
                //BtnOrder.Attributes["onclick"] = string.Format("CntRow('{0}')", dr.JutyuNo);
                //BtnOrder.Text = "発注";

                //if (DrpKeijoFlg.SelectedValue != "1")
                //{
                //    //BtnKeijo.Attributes["onclick"] = string.Format("JtuRow('{0}')", dr.JutyuNo);
                //    //BtnKeijo.Text = "計上";
                //    //BtnKeijo.Visible = true;

                //    //Button BtnSyusei = e.Item.FindControl("BtnSyusei") as Button;
                //    //BtnSyusei.Attributes["onclick"] = string.Format("CntRow('{0}')", dr.JutyuNo);
                //    //BtnSyusei.Text = "修正";

                //    //Button BtnCopy = e.Item.FindControl("BtnCopy") as Button;
                //    //BtnCopy.Attributes["onclick"] = string.Format("CntRow('{0}')", dr.JutyuNo);
                //    //BtnCopy.Text = "複製";
                //}
                //else
                //{
                //    //BtnKeijo.Visible = false;

                //    //Button BtnSyusei = e.Item.FindControl("BtnSyusei") as Button;
                //    //BtnSyusei.Attributes["onclick"] = string.Format("CntRow('{0}')", dr.JutyuNo);
                //    //BtnSyusei.Text = "確認";
                //}

                e.Item.Cells[RadG.Columns.FindByUniqueName("Coljutyu").OrderIndex].Text = dr.JutyuNo.ToString();
                if (!dr.IsCategoryNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColCategori").OrderIndex].Text = dr.CategoryName;
                }
                if (!dr.IsBumonNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColBumon").OrderIndex].Text = dr.Bumon;
                }
                if (!dr.IsTokuisakiCodeNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiCode").OrderIndex].Text = dr.TokuisakiCode.ToString();
                }
                if (!dr.IsTokuisakiNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiName").OrderIndex].Text = dr.TokuisakiRyakusyo;
                }


                if (!dr.IsFacilityNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSisetu").OrderIndex].Text = dr.FacilityName;
                }
                if (!dr.IsTantoNameNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTantousya").OrderIndex].Text = dr.TantoName;

                }
                if (!dr.IsSouSuryouNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSuryo").OrderIndex].Text = dr.SouSuryou.ToString();
                }
                if (!dr.IsSoukeiGakuNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColKingaku").OrderIndex].Text = dr.SoukeiGaku.ToString("0,0");
                }

                if (!dr.IsCreateDateNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColMitumoriDay").OrderIndex].Text = dr.CreateDate.ToShortDateString() + "[" + dr.CreateDate.ToShortTimeString() + "]";
                }

            }
        }

        protected void BtnSyusei_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("JutyuInput.aspx");
        }

        protected void BtnCopy_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Copy");

            this.Response.Redirect("JutyuInput.aspx");
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void BtnDownlod_Click(object sender, EventArgs e)
        {
            //CsvDownLoad
            OnDownLoad();
            Create();
        }

        private void OnDownLoad()
        {
            try
            {
                lblMsg.Text = "";

                UserViewManager.UserView v = SessionManager.User.GetUserView(LIST_ID_DL);
                v.SortExpression = "JutyuNo ASC";

                SqlDataAdapter da = new SqlDataAdapter(v.SqlDataFactory.SelectCommand.CommandText, Global.GetConnection());

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (0 == dt.Rows.Count)
                {
                    lblMsg.Text = "該当データがありません";
                    return;
                }

                string strData = v.SqlDataFactory.GetTextData(dt, this.RadG.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

                string strExt = "csv";
                string strFileName = ("受注DL") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                     this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                     Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnKeijo_Click(object sender, EventArgs e)
        {
            //計上ボタンクリック時
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Uriage");
            this.Response.Redirect("JutyuInput.aspx");
        }

        protected void BtnOrder_Click(object sender, EventArgs e)
        {
            string MitumoriNo = count.Value;

            SessionManager.MitumoriSyusei(MitumoriNo);
            SessionManager.MitumoriSyuseiRow("");
            SessionManager.MitumoriType("Uriage");
            this.Response.Redirect("../Order/OrderedInput.aspx");
        }


        protected void RadG_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            Create();
        }

        protected void BtnKensaku_Click(object sender, EventArgs e)
        {
            this.RadG.MasterTableView.CurrentPageIndex = 0;
            Create();
        }

        protected void RadG_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void RadTokuiMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        protected void RadTyokusoMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakuTyokusoSaki(sender, e);
        }

        protected void RadSyohinmeisyou_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakuSyohin(sender, e);
        }

        string strkeys = "";
        string Type = "";

        private void Insatsu(string type)
        {
            for (int i = 0; i < this.RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (this.strkeys != "") { this.strkeys += "_"; }

                    this.strkeys += chk.Value;
                }
            }
            if (strkeys == "")
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("チェックしてください。");
                return;
            }
            else
            {
                string[] strAry = this.strkeys.Split('_');
                string strJutyuNo = "";
                for (int i = 0; i < strAry.Length; i++)
                {
                    string[] str = strAry[i].Split(',');

                    if (str.Length < 1) { continue; }
                    if (strJutyuNo != "")
                    {
                        strJutyuNo += ",";
                        strJutyuNo += string.Format("{0}", str[0]);
                    }
                    else
                    {
                        strJutyuNo += string.Format("{0}", str[0]);
                    }
                }
                string[] strJutyuAry = strJutyuNo.Split(',');
                AppCommon acApp = new AppCommon();
                acApp.JutyuInsatsu(Type, strJutyuAry);

                string sArg = "";

                if (Type == "Purchase")
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary("PurchaseFormat.pdf", acApp.theData);
                }

                if (Type == "Order")
                {
                    sArg = Common.DownloadDataForm.GetQueryString4Binary("OrderFormat.pdf", acApp.theData);
                }

                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + sArg)));
            }
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            string mNo = "";
            string strKeys = "";
            for (int i = 0; i < RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (strKeys != "") { strKeys += "_"; }

                    strKeys += chk.Value;
                }
            }
            if (strKeys == "")
            {
                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("チェックしてください。");
                return;
            }
            else
            {
                string[] strAry = strKeys.Split('_');
                for (int l = 0; l < strAry.Length; l++)
                {

                    string[] str = strAry[l].Split(',');
                    //mNo = str[0];
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (str[j] != "")
                        {
                            mNo = str[0];
                            DataJutyu.T_JutyuHeaderDataTable dt = ClassJutyu.DelJutyuHeader(mNo, Global.GetConnection());
                            DataJutyu.T_JutyuDataTable dd = ClassJutyu.DelJutyu2(mNo, Global.GetConnection());
                        }
                    }
                }
            }
            Create();

        }

        string strKeys = "";
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //修正ボタンクリック時処理
            string mNo = "";
            string strKeys = "";
            for (int i = 0; i < RadG.Items.Count; i++)
            {
                HtmlInputCheckBox chk =
                    (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                if (chk.Checked && chk.Visible)
                {
                    if (this.strKeys != "") { this.strKeys += "_"; }

                    this.strKeys += chk.Value;
                    string[] str = this.strKeys.Split(',');
                    mNo = str[0];
                }
            }

            SessionManager.MitumoriSyusei(mNo);
            SessionManager.MitumoriSyuseiRow("");
            //SessionManager.MitumoriType("Syusei");

            this.Response.Redirect("../OrderInput.aspx");

        }

        protected void BtnOrdered_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < RadG.Items.Count; i++)
                {
                    HtmlInputCheckBox chk =
                        (HtmlInputCheckBox)this.RadG.Items[i].Cells[RadG.Columns.FindByUniqueName("ColChk_Row").OrderIndex].FindControl("ChkRow");

                    if (chk.Checked && chk.Visible)
                    {
                        if (strKeys != "") { strKeys += ","; }

                        strKeys += chk.Value;
                    }
                }
                string JutyuNo = "";
                string Facility = "";
                string Category = "";
                string[] Row = strKeys.Split(',');
                for (int j = 0; j < Row.Length; j++)
                {
                    string[] retsu = Row[j].Split('_');
                    JutyuNo = retsu[0];
                    Facility = retsu[1];
                    Category = retsu[2];


                    DataJutyu.T_JutyuDataTable jdt = ClassJutyu.GetJutyu3(JutyuNo, Global.GetConnection());
                    DataJutyu.T_JutyuHeaderDataTable hdt = ClassJutyu.GetJutyuHeader(JutyuNo, Global.GetConnection());

                    string cate = "";
                    string shiire = "";
                    DataSet1.T_OrderedDataTable dt = new DataSet1.T_OrderedDataTable();

                    for (int i = 0; i < jdt.Count; i++)
                    {
                        DataSet1.T_OrderedHeaderDataTable dth = new DataSet1.T_OrderedHeaderDataTable();
                        DataSet1.T_OrderedHeaderRow dlh = dth.NewT_OrderedHeaderRow();
                        string no = "";
                        SessionManager.KI();
                        int ki = int.Parse(SessionManager.KII);
                        DataSet1.T_OrderedHeaderRow drr = ClassOrdered.GetMaxOrdered(ki, Global.GetConnection());
                        if (drr != null)
                        {
                            no = (drr.OrderedNo + 1).ToString();
                        }
                        else
                        {
                            no = "3" + (ki * 100000 + 1).ToString();
                        }


                        DataSet1.T_OrderedRow dl = dt.NewT_OrderedRow();
                        if (cate == "")
                        {
                            cate += jdt[i].CateGory.ToString();
                        }
                        else
                        {
                            cate += "," + jdt[i].CateGory;
                        }
                        if (shiire == "")
                        {
                            shiire += jdt[i].ShiireName;
                        }
                        else
                        {
                            shiire += "," + jdt[i].ShiireName;
                        }

                        dl.OrderedNo = int.Parse(no);
                        dlh.OrderedNo = int.Parse(no);
                        dl.RowNo = i + 1;
                        dl.TokuisakiCode = jdt[i].TokuisakiCode;
                        dlh.TokuisakiCode = jdt[i].TokuisakiCode;
                        dl.TokuisakiMei = jdt[i].TokuisakiMei;
                        dlh.TokuisakiName = jdt[i].TokuisakiMei;
                        dl.SeikyusakiMei = jdt[i].SeikyusakiMei;
                        dlh.SeikyusakiName = jdt[i].SeikyusakiMei;
                        dl.Category = jdt[i].CateGory;
                        dlh.Category = jdt[i].CateGory;
                        dl.CategoryName = jdt[i].CategoryName;
                        dlh.CategoryName = jdt[i].CategoryName;
                        if (!jdt[i].IsTyokusousakiCDNull())
                        {
                            dl.TyokusodsakiCD = jdt[i].TyokusousakiCD;
                            dlh.TyokusousakiCD = jdt[i].TyokusousakiCD;
                        }
                        if (!jdt[i].IsTyokusousakiMeiNull())
                        {
                            dl.TyokusosakiMei = jdt[i].TyokusousakiMei;
                            dlh.TyokusousakiMei = jdt[i].TyokusousakiMei;
                        }
                        if (!jdt[i].IsJusyo1Null())
                        {
                            dl.Jusyo1 = jdt[i].Jusyo1;
                        }
                        if (!jdt[i].IsJusyo2Null())
                        {
                            dl.Jusyo2 = jdt[i].Jusyo2;
                        }
                        if (!jdt[i].IsSisetuCodeNull())
                        {
                            dl.FacilityCode = jdt[i].SisetuCode;
                        }
                        if (!jdt[i].IsSisetuMeiNull())
                        {
                            dl.FacilityName = jdt[i].SisetuMei;
                        }
                        if (!jdt[i].IsSisetsuTellNull())
                        {
                            dl.FacilityTel = jdt[i].SisetsuTell;
                        }

                        //if (!jdt[i].IsSisetuCodeNull())
                        //{
                        //    dl.FacilityCode = jdt[i].SisetuCode;
                        //    string FacCode = jdt[i].SisetuCode.ToString();
                        //    DataSet1.M_Facility_NewRow dr = Class1.FacilityRow(FacCode, Global.GetConnection());
                        //}
                        //if (!jdt[i].IsSisetuMeiNull())
                        //{
                        //    dl.FacilityName = jdt[i].SisetuMei;
                        //    dlh.FacilityName = jdt[i].SisetuMei;
                        //    string FacName = jdt[i].SisetuMei;
                        //    DataSet1.M_Facility_NewRow dr = Class1.FacilityRow2(FacName, Global.GetConnection());
                        //    dl.FacilityTel = dr.Tell;
                        //}
                        if (!jdt[i].IsSisetuJusyo1Null())
                        { dl.FacilityJusyo1 = jdt[i].SisetuJusyo1; }
                        if (!jdt[i].IsSiyouKaishiNull())
                        { dl.SiyouKaishi = jdt[i].SiyouKaishi.ToShortDateString(); }
                        if (!jdt[i].IsSiyouOwariNull())
                        { dl.SiyouiOwari = jdt[i].SiyouOwari.ToShortDateString(); }
                        if (!jdt[i].IsHyojunKakakuNull())
                        { dl.HyoujyunKakaku = int.Parse(jdt[i].HyojunKakaku); }
                        if (!jdt[i].IsTanTouNameNull())
                        { dl.StaffName = jdt[i].TanTouName; }
                        if (!jdt[i].IsBusyoNull())
                        { dl.Department = jdt[i].Busyo; }
                        if (!jdt[i].IsRangeNull())
                        { dl.Range = jdt[i].Range; }
                        if (!jdt[i].IsSyouhinMeiNull())
                        { dl.ProductName = jdt[i].SyouhinMei; }
                        if (!jdt[i].IsSyouhinCodeNull())
                        { dl.ProductCode = int.Parse(jdt[i].SyouhinCode); }
                        if (!jdt[i].IsMekarHinbanNull())
                        { dl.MekerNo = jdt[i].MekarHinban; }
                        if (!jdt[i].IsKeitaiMeiNull())
                        { dl.Media = jdt[i].KeitaiMei; }
                        dl.UriageFlg = jdt[i].JutyuFlg;
                        dl.HatyuDay = DateTime.Now.ToShortDateString();
                        dlh.CreateDate = DateTime.Now;
                        dl.JutyuSuryou = jdt[i].JutyuSuryou;
                        dlh.OrderedAmount = jdt[i].JutyuSuryou;
                        dl.JutyuGokei = jdt[i].JutyuGokei;
                        dl.ShiireTanka = jdt[i].ShiireTanka;
                        dlh.ShiireKingaku = jdt[i].ShiireKingaku;
                        dl.ShiireKingaku = jdt[i].ShiireKingaku;
                        dl.WareHouse = jdt[i].WareHouse;
                        dl.ShiireSakiName = jdt[i].ShiireName;
                        dlh.ShiiresakiName = jdt[i].ShiireName;
                        string abb = dl.ShiireSakiName;
                        DataMaster.M_ShiiresakiDataTable dd = ClassMaster.GetShiiresaki(abb, Global.GetConnection());
                        for (int l = 0; l < dd.Count; l++)
                        {
                            dl.ShiiresakiCode = dd[0].ShiiresakiCode.ToString();
                            dlh.ShiiresakiCode = dd[0].ShiiresakiCode.ToString();
                        }
                        dl.Zeikubun = jdt[i].Zeikubun;
                        dl.Kakeritsu = jdt[i].Kakeritsu;
                        dl.Zansu = jdt[i].JutyuSuryou.ToString();
                        dlh.InsertFlg = false;
                        //dt.AddT_OrderedRow(dl);

                        dth.AddT_OrderedHeaderRow(dlh);

                        ClassOrdered.UpdateOrderedHeader(dth, dl, cate, shiire, Global.GetConnection());
                        ClassJutyu.UpDateJutyuHeader(JutyuNo, hdt, Global.GetConnection());
                        dth = null;
                        lblMsg.Text = "発注処理を行いました。";
                        ///売上データ作成

                        Create();
                    }
                    //ClassOrdered.InsertOrdered2(dt, Global.GetConnection());
                }
                DataUriage.T_UriageHeaderRow dru = ClassUriage.GetMaxNo(Global.GetConnection());
                string jNo = JutyuNo;
                int noo = dru.UriageNo;
                int Uno = noo + 1;
                ClassUriage.InsertUriageHeader(jNo, Uno, Global.GetConnection());
            }
            catch (Exception ex)
            {
                ClassMail.ErrorMail("maeda@m2m-asp.com", "受注一覧から発注ボタン", strKeys + "^" + ex.Message);
            }
        }

        protected void RadSisetMeisyo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                ListSet.GetFacility(sender, e);
            }
        }

        protected void BtnMeisaiCSVdownload_Click(object sender, EventArgs e)
        {
            try
            {
                UserViewManager.UserView v = SessionManager.User.GetUserView(21);
                v.SortExpression = "JutyuNo ASC";

                //ClassKensaku.KensakuParam k = SetKensakuParam();
                string strJutyuNo = "";
                string strJustyuFlg = "";
                string strTokuisaki = "";
                string strSeikyusaki = "";
                string strTyokusousaki = "";
                string strFacility = "";
                string strCategory = "";
                string strBumon = "";
                string strMakerHinban = "";
                string strSyouhinMei = "";
                string strTanto = "";
                string strMitumoriBi = "";
                bool boolWhere = false;

                SqlDataAdapter da = new SqlDataAdapter("", Global.GetConnection());
                da.SelectCommand.CommandText = "select * from T_Jutyu";

                if (!string.IsNullOrEmpty(TbxJutyuNo.Text))
                {
                    strJutyuNo = TbxJutyuNo.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where JutyuNo = @MitumoriNo";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and JutyuNo = @MitumoriNo";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@MitumoriNo", strJutyuNo);
                }
                if (!string.IsNullOrEmpty(DrpFlg.SelectedValue))
                {
                    strJustyuFlg = DrpFlg.SelectedValue;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where JutyuFlg = @JF";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and JutyuFlg = @JF";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@JF", strJustyuFlg);
                }
                if (!string.IsNullOrEmpty(RadTokuiMeisyo.Text))
                {
                    strTokuisaki = RadTokuiMeisyo.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where TokuisakiMei = @Tokuisaki";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and TokuisakiMei = @Tokuisaki";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Tokuisaki", strTokuisaki);
                }

                if (!string.IsNullOrEmpty(RadSekyuMeisyo.Text))
                {
                    strSeikyusaki = RadSekyuMeisyo.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where SeikyusakiMei = @Seikyusaki";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and SeikyusakiMei = @Seikyusaki";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Seikyusaki", strSeikyusaki);
                }

                if (!string.IsNullOrEmpty(RadTyokusoMeisyo.Text))
                {
                    strTyokusousaki = RadTyokusoMeisyo.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where TyokusousakiMei = @Tyokusosaki";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and TyokusousakiMei = @Tyokusosaki";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Tyokusosaki", strSeikyusaki);
                }

                if (!string.IsNullOrEmpty(RadSisetMeisyo.Text))
                {
                    strFacility = RadSisetMeisyo.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where SisetuMei = @Sisetu";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and SisetuMei = @Sisetu";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Sisetu", strFacility);
                }

                if (!string.IsNullOrEmpty(RadCate.Text))
                {
                    strCategory = RadCate.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where CategoryName = @Cate";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and CategoryName = @Cate";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Cate", strCategory);
                }
                if (!string.IsNullOrEmpty(RadBumon.Text))
                {
                    strBumon = RadBumon.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where Busyo = @Busyo";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and Busyo = @Busyo";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Busyo", strBumon);
                }
                if (!string.IsNullOrEmpty(TbxHinban.Text))
                {
                    strMakerHinban = TbxHinban.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where MekarHinban = @Maker";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and MekarHinban = @Maker";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Maker", strMakerHinban);
                }
                if (!string.IsNullOrEmpty(RadSyohinmeisyou.Text))
                {
                    strSyouhinMei = RadSyohinmeisyou.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where MekarHinban = @Maker";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and MekarHinban = @Maker";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Maker", strMakerHinban);
                }
                if (!string.IsNullOrEmpty(RadTanto.Text))
                {
                    strTanto = RadTanto.Text;
                    if (!boolWhere)
                    {
                        da.SelectCommand.CommandText += " Where TanTouName = @Tanto";
                        boolWhere = true;
                    }
                    else
                    {
                        da.SelectCommand.CommandText += " and TanTouName = @Tanto";
                    }
                    da.SelectCommand.Parameters.AddWithValue("@Tanto", strTanto);
                }
                Common.CtlNengappiForm CtlJucyuBi = FindControl("CtlJucyuBi") as Common.CtlNengappiForm;
                if (CtlJucyuBi.KikanType != Core.Type.NengappiKikan.EnumKikanType.NONE)
                {
                    Core.Type.NengappiKikan d = CtlJucyuBi.GetNengappiKikan();
                    if (d.KikanType.ToString() == "ONEDAY")
                    {
                        if (!boolWhere)
                        {
                            da.SelectCommand.CommandText += " Where MitumoriBi = @MitumoriBi";
                            boolWhere = true;
                        }
                        else
                        {
                            da.SelectCommand.CommandText += " and MitumoriBi = @MitumoriBi";
                        }
                        da.SelectCommand.Parameters.AddWithValue("@MitumoriBi", d.From.ToDateTime());
                    }
                    if (d.KikanType.ToString() == "BEFORE")
                    {
                        if (!boolWhere)
                        {
                            da.SelectCommand.CommandText += " Where MitumoriBi <= @MitumoriBi";
                            boolWhere = true;
                        }
                        else
                        {
                            da.SelectCommand.CommandText += " and MitumoriBi <= @MitumoriBi";
                        }
                        da.SelectCommand.Parameters.AddWithValue("@MitumoriBi", d.From.ToDateTime());
                    }
                    if (d.KikanType.ToString() == "AFTER")
                    {
                        if (!boolWhere)
                        {
                            da.SelectCommand.CommandText += " Where MitumoriBi >= @MitumoriBi";
                            boolWhere = true;
                        }
                        else
                        {
                            da.SelectCommand.CommandText += " and MitumoriBi >= @MitumoriBi";
                        }
                        da.SelectCommand.Parameters.AddWithValue("@MitumoriBi", d.From.ToDateTime());
                    }
                }
                //DataMitumori.T_MitumoriDataTable dt = new DataMitumori.T_MitumoriDataTable();
                DataJutyu.T_JutyuDataTable dt = new DataJutyu.T_JutyuDataTable();
                da.Fill(dt);

                if (0 == dt.Rows.Count)
                {
                    lblMsg.Text = "該当データがありません";
                    return;
                }

                string strData = v.SqlDataFactory.GetTextData(dt, this.RadG.MasterTableView.SortExpressions.GetSortString(), Core.Data.DataTable2Text.EnumDataFormat.Csv);

                string strExt = "csv";
                string strFileName = ("受注明細") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strExt;
                Core.Data.DataTable2Text.EnumDataFormat fmt = Core.Data.DataTable2Text.EnumDataFormat.Csv;

                this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';",
                     this.ResolveUrl("~/Common/DownloadDataForm.aspx?" +
                     Common.DownloadDataForm.GetQueryString4Text(strFileName, v.CreateTextData(dt, fmt, null)))));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                ClassMail.ErrorMail("maeda@m2m-asp.com", "受注一覧 | 明細CSVダウンロード", ex.Message);
            }
        }
    }
}
