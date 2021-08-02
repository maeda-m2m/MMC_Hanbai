using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Uriage
{
    public partial class NyukinRireki : System.Web.UI.Page
    {
        private string VsSqlCmd
        {
            get
            {
                return Convert.ToString(this.ViewState["VsSqlCmd"]);
            }
            set
            {
                this.ViewState["VsSqlCmd"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //担当名DropDwonList
                ListSet.SetTanto(RcbUserName);

                //得意先、仕入先DDropDownList
                ListSet.SetRyakusyou2(RcbTokuisakiMei);

                Create();
            }
        }

        protected void RcbTokuisakiMei_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            //ドロップダウン検索用
            ListSet.SetKensakutRyakusyou(sender, e);
        }

        private void Create()
        {
            string sqlcmd = "";
            string daytype = "";
            Core.Type.NengappiKikan objYoukyuReleaseBi = null;

            if (this.RcbTokuisakiMei.SelectedValue != "-1" && this.RcbTokuisakiMei.SelectedValue != "")
            {
                sqlcmd = "TorihikisakiCode = '" + this.RcbTokuisakiMei.SelectedValue + "' ";
            }

            if (CtlJucyuBi.GetNengappiKikan() != null)
            {
                objYoukyuReleaseBi = CtlJucyuBi.GetNengappiKikan();

                daytype = objYoukyuReleaseBi.KikanType.ToString();

                if (sqlcmd != "")
                {
                    sqlcmd += " and ";
                }

                switch (daytype)
                {
                    case "ONEDAY":
                        sqlcmd += " NyukinBi >= '" + objYoukyuReleaseBi.From + "' and NyukinBi < '" + objYoukyuReleaseBi.From.AddDays(1) + "' ";
                        break;
                    case "BEFORE":
                        sqlcmd += " NyukinBi <= '" + objYoukyuReleaseBi.From.AddDays(1) + "' ";
                        break;
                    case "AFTER":
                        sqlcmd += " NyukinBi >= '" + objYoukyuReleaseBi.From + "' ";
                        break;
                    case "FROM":
                        sqlcmd += " NyukinBi <= '" + objYoukyuReleaseBi.To + "' and NyukinBi >= '" + objYoukyuReleaseBi.From + "' ";
                        break;
                }
            }

            if (this.TbxDenBan.Text != "")
            {
                if (sqlcmd != "")
                {
                    sqlcmd += " and ";
                }

                sqlcmd += " UriageKanriCode = '" + TbxDenBan.Text.Trim() + "' ";
            }

            if (this.RcbUserName.SelectedValue != "-1" && this.RcbUserName.SelectedValue != "")
            {
                if (sqlcmd != "")
                {
                    sqlcmd += " and ";
                }

                sqlcmd += " TantousyaCode = '" + RcbUserName.SelectedValue + "' ";
            }

            VsSqlCmd = sqlcmd;

            DataNyukin.T_NyukinDataTable dt = ClassNyukin.getT_NyukinRirekiKensakuTable(VsSqlCmd, Global.GetConnection());


            dt.DefaultView.Sort = "No DESC";

            this.RadG.VirtualItemCount = dt.Count;

            int nPageSize = this.RadG.PageSize;
            int nPageCount = dt.Count / nPageSize;
            if (0 < dt.Count % nPageSize) nPageCount++;
            if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;


            RadG.DataSource = dt;
            RadG.DataBind();

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string sqlcmd = "";
            string daytype = "";
            Core.Type.NengappiKikan objYoukyuReleaseBi = null;

            if (this.RcbTokuisakiMei.SelectedValue != "-1" && this.RcbTokuisakiMei.SelectedValue != "")
            {
                sqlcmd = "TorihikisakiCode = '" + this.RcbTokuisakiMei.SelectedValue + "' ";
            }


            if (CtlJucyuBi.GetNengappiKikan() != null)
            {
                objYoukyuReleaseBi = CtlJucyuBi.GetNengappiKikan();

                daytype = objYoukyuReleaseBi.KikanType.ToString();

                if (sqlcmd != "")
                {
                    sqlcmd += " and ";
                }

                switch (daytype)
                {
                    case "ONEDAY":
                        sqlcmd += " NyukinBi >= '" + objYoukyuReleaseBi.From + "' and NyukinBi < '" + objYoukyuReleaseBi.From.AddDays(1) + "' ";
                        break;
                    case "BEFORE":
                        sqlcmd += " NyukinBi <= '" + objYoukyuReleaseBi.From.AddDays(1) + "' ";
                        break;
                    case "AFTER":
                        sqlcmd += " NyukinBi >= '" + objYoukyuReleaseBi.From + "' ";
                        break;
                    case "FROM":
                        sqlcmd += " NyukinBi <= '" + objYoukyuReleaseBi.To + "' and NyukinBi >= '" + objYoukyuReleaseBi.From + "' ";
                        break;
                }
            }

            if (this.TbxDenBan.Text != "")
            {
                if (sqlcmd != "")
                {
                    sqlcmd += " and ";
                }

                sqlcmd += " UriageKanriCode = '" + TbxDenBan.Text.Trim() + "' ";
            }

            if (this.RcbUserName.SelectedValue != "-1" && this.RcbUserName.SelectedValue != "")
            {
                if (sqlcmd != "")
                {
                    sqlcmd += " and ";
                }

                sqlcmd += " TantousyaCode = '" + RcbUserName.SelectedValue + "' ";
            }

            DataNyukin.T_NyukinDataTable dt = ClassNyukin.getT_NyukinRirekiKensakuTable(sqlcmd, Global.GetConnection());
            dt.DefaultView.Sort = "No DESC";
            this.RadG.VirtualItemCount = dt.Count;

            int nPageSize = this.RadG.PageSize;
            int nPageCount = dt.Count / nPageSize;
            if (0 < dt.Count % nPageSize) nPageCount++;
            if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

            VsSqlCmd = sqlcmd;

            RadG.DataSource = dt;
            RadG.DataBind();
        }

        protected void RadG_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {

                DataRowView drv = (DataRowView)e.Item.DataItem;
                DataNyukin.T_NyukinRow dr = (DataNyukin.T_NyukinRow)drv.Row;
                HtmlInputButton SyuseiBtn = e.Item.FindControl("SyuseiBtn") as HtmlInputButton;

                SyuseiBtn.Attributes["onclick"] = string.Format("Syusei('{0}')", dr.UriageKanriCode);

                e.Item.Cells[RadG.Columns.FindByUniqueName("ColNo").OrderIndex].Text = dr.UriageKanriCode;

                if (!dr.IsNyukinBiNull())
                    e.Item.Cells[RadG.Columns.FindByUniqueName("NyuukinBi").OrderIndex].Text = dr.NyukinBi.ToString("yyyy/MM/dd");

                if (!dr.IsTorihikisakiCodeNull())
                {
                    string TokuisakiMei = DLL.ClassNyukin.get_TokuisakiName(dr.TorihikisakiCode, Global.GetConnection());
                    string TokuisakiTenName = DLL.ClassNyukin.get_TokuisakiTenName(dr.TorihikisakiCode, Global.GetConnection());
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiMei").OrderIndex].Text = dr.TorihikisakiCode + "<br/>" + TokuisakiMei;
                }

                //string EigyousyoName = "";
                string TantousyaName = "";

                if (!dr.IsTantousyaCodeNull())
                {
                    TantousyaName = DLL.ClassNyukin.get_TantousyaName(dr.TantousyaCode, Global.GetConnection());
                }

                //e.Item.Cells[RadG.Columns.FindByUniqueName("ColEigyousyo").OrderIndex].Text =
                //    EigyousyoName + "<br/>" + TantousyaName;

                if (!dr.IsNyukinGakuNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColNyukinGaku").OrderIndex].Text = dr.NyukinGaku.ToString("N").Replace(".00", "");
                }

                if (!dr.IsGenkinNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColGenkin").OrderIndex].Text = dr.Genkin.ToString("N").Replace(".00", "");
                }

                if (!dr.IsTegataNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTegata").OrderIndex].Text = dr.Tegata.ToString("N").Replace(".00", "");


                }

                if (!dr.IsTegataKijitsuNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTegataKijitu").OrderIndex].Text = dr.TegataKijitsu.ToShortDateString();
                    if (!dr.IsFurikomininNull())
                    {
                        e.Item.Cells[RadG.Columns.FindByUniqueName("ColTegataKijitu").OrderIndex].Text += "<br/>" + dr.Furikominin;
                    }
                }

                if (!dr.IsFurikomiNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColHurikomi").OrderIndex].Text = dr.Furikomi.ToString("N").Replace(".00", "");
                }

                if (!dr.IsKogitteNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColKogitte").OrderIndex].Text = dr.Kogitte.ToString("N").Replace(".00", "");
                }

                if (!dr.IsKogitteKijitsuNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColKogitteKijitu").OrderIndex].Text = dr.KogitteKijitsu.ToShortDateString();
                }

                if (!dr.IsCyouseiNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColCyousei").OrderIndex].Text = dr.Cyousei.ToString("N").Replace(".00", "");
                }

                if (!dr.IsSousaiNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColSousai").OrderIndex].Text = dr.Sousai.ToString("N").Replace(".00", "");
                }

                if (!dr.IsBikouNull())
                {
                    e.Item.Cells[RadG.Columns.FindByUniqueName("ColTekiyou").OrderIndex].Text = dr.Bikou;
                }
            }
        }

        protected void RadG_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }
    }
}