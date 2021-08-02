using System;
using DLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.IO;
using System.Text;



namespace Gyomu.Closing
{
    public partial class MonthClosing : System.Web.UI.Page
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
            DataTable dtC = new DataTable();

            ClassKensaku.KensakuParam p = KensakuParam();
            if (RcbFormatSelsect.SelectedValue == "1")
            {
                DataLedger.T_Nyukin2DataTable dt = ClassKensaku.GetNyukin2(p, Global.GetConnection());
                if (dt.Rows.Count == 0)
                {
                    LblErr.Text = "データがありません";
                    this.RadG.Visible = false;
                    return;
                }
                else
                {
                    LblErr.Text = "";
                    this.RadG.Visible = true;
                }

                this.RadG.VirtualItemCount = dt.Count;

                if (dt.Count < 10)
                {
                    RadG.ClientSettings.Scrolling.AllowScroll = false;
                }
                else
                {
                    RadG.ClientSettings.Scrolling.AllowScroll = true;
                }

                int nPageSize = this.RadG.PageSize;
                int nPageCount = dt.Count / nPageSize;
                if (0 < dt.Count % nPageSize) nPageCount++;
                if (nPageCount <= this.RadG.MasterTableView.CurrentPageIndex) this.RadG.MasterTableView.CurrentPageIndex = 0;

                dtC = dt;
            }
            else
            {

            }

            this.RadG.DataSource = dtC;
            this.RadG.DataBind();
        }

        private ClassKensaku.KensakuParam KensakuParam()
        {
            ClassKensaku.KensakuParam p = new ClassKensaku.KensakuParam();

            if (RdpCreateDate.SelectedDate != null)
            {
                p.CreateDate = RdpCreateDate.SelectedDate;
            }

            if (TbxSyoukaiNo.Text != "")
            {
                p.No = TbxSyoukaiNo.Text;
            }

            if (RcbTorihikiKBN.SelectedValue != "")
            {
                p.TorihikiKubun = RcbTorihikiKBN.SelectedValue;
            }

            if (TbxNyukinDate.Text != "")
            {
                p.NyukinDate = TbxNyukinDate.Text;
            }

            if (TbxKouzaNo.Text != "")
            {
                p.KouzaNo = TbxKouzaNo.Text;
            }

            if (TbxFurikomiIrai.Text != "")
            {
                p.FurikomiIrai = TbxFurikomiIrai.Text;
            }

            if (TbxTokuisakiName.Text != "")
            {
                p.TokuisakiName = TbxTokuisakiName.Text;
            }

            if (TbxTokuisakiCode.Text != "")
            {
                p.TokuisakiCode = TbxTokuisakiCode.Text;
            }

            if (TbxFurikomiBank.Text != "")
            {
                p.FurikomiBank = TbxFurikomiBank.Text;
            }

            if (TbxFurikomiShiten.Text != "")
            {
                p.FurikomiShiten = TbxFurikomiShiten.Text;
            }

            if (TbxNyukinKingaku.Text != "")
            {
                p.NyukinKingaku = TbxNyukinKingaku.Text;
            }
            return p;
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            {
                LblErr.Text = "";
                if (FUData.HasFile)
                {
                    string filename = FUData.PostedFile.FileName;
                    byte[] v = FUData.FileBytes;
                    System.IO.Stream c = FUData.FileContent;

                    System.IO.StreamReader tabreader = null;
                    Core.IO.CSVReader csvreader = null;
                    Stream stream = FUData.PostedFile.InputStream;
                    Encoding enc = System.Text.Encoding.GetEncoding(932);
                    System.IO.StreamReader check = new System.IO.StreamReader(stream, enc);
                    string strCheck = check.ReadLine();
                    if (strCheck == null)
                    {
                        return;
                    }
                    bool bTab = (strCheck.Split('\t').Length > strCheck.Split(',').Length);
                    if (bTab)
                    {
                        tabreader = new System.IO.StreamReader(stream, enc);
                    }
                    else
                    {
                        csvreader = new Core.IO.CSVReader(stream, enc);
                    }
                    int[] nData = new int[]
                    {

                    };

                    string[] strFieldName = new string[]
                    {
                    "顧客コード",
                    "得意先コード",
                    "得意先名1",
                    "得意先名2",
                    "市町村コード",
                    "担当営業",
                    "フリガナ",
                    "略称",
                    "〒",
                    "住所1",
                    "住所2",
                    "TEL",
                    "FAX",
                    "担当者",
                    "担当部署名",
                    "敬称",
                    "掛率",
                    "締日",
                    "税通知",
                    "税区分",
                    "銀行",
                    "口座番号",
                   "",
                    };

                    int nLine = 0;
                    int RowCount = 0;
                    //string[] str = null;
                    //string[] strPrevData = null;

                    //string tokuCode = "";

                    while (true)
                    {
                        RowCount++;
                        try
                        {
                            nLine++;
                            DataSet1.M_Tokuisaki2DataTable dtN = new DataSet1.M_Tokuisaki2DataTable();
                            DataSet1.M_Tokuisaki2Row drN = dtN.NewM_Tokuisaki2Row();
                            string strArray2 = check.ReadLine();
                            if (strArray2 == null)
                            {
                                break;
                            }

                            string[] str2 = strArray2.Split(',');

                            drN.ItemArray = str2;

                            dtN.AddM_Tokuisaki2Row(drN);
                            Class1.UploadTokuisaki(dtN, Global.GetConnection());
                            LblErr.Text = "データを取り込みました。";
                        }
                        catch (Exception ex)
                        {
                            LblErr.Text = "データ取込時にエラーが発生しました。";
                        }
                    }
                }
            }

        }

        protected void RadG_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                DataLedger.T_Nyukin2Row dr = (DataLedger.T_Nyukin2Row)drv.Row;

                Label CreateDate = e.Item.FindControl("CreateDate") as Label;
                Label Syoukai = e.Item.FindControl("Syoukai") as Label;
                Label TorihikiKBN = e.Item.FindControl("TorihikiKBN") as Label;
                Label NyukinDate = e.Item.FindControl("NyukinDate") as Label;
                Label KouzaNo = e.Item.FindControl("KouzaNo") as Label;
                Label FurikkomiIrai = e.Item.FindControl("FurikkomiIrai") as Label;
                Label TokuisakiName = e.Item.FindControl("TokuisakiName") as Label;
                Label TokuisakiCode = e.Item.FindControl("TokuisakiCode") as Label;
                Label Bank = e.Item.FindControl("Bank") as Label;
                Label FurikomiShiten = e.Item.FindControl("FurikomiShiten") as Label;
                Label Nyukingaku = e.Item.FindControl("Nyukingaku") as Label;

                if (!dr.IsCreateDateNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColCreateDate").OrderIndex].Text = dr.CreateDate.ToString();
                    CreateDate.Text = dr.CreateDate.ToString();
                }

                if (!dr.IsNoNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColSyoukai").OrderIndex].Text = dr.No.ToString();
                    Syoukai.Text = dr.No;
                }

                if (!dr.IsTorihikiKubunNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColTorihikiKBN").OrderIndex].Text = dr.TorihikiKubun.ToString();
                    TorihikiKBN.Text = dr.TorihikiKubun;
                }

                if (!dr.IsNyukinDateNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColNyukinDate").OrderIndex].Text = dr.NyukinDate.ToString();
                    NyukinDate.Text = dr.NyukinDate;
                }
                if (!dr.IsKouzaNoNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColKouzaNo").OrderIndex].Text = dr.KouzaNo.ToString();
                    KouzaNo.Text = dr.KouzaNo;
                }
                if (!dr.IsFurikomiIraiNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColFurikkomiIrai").OrderIndex].Text = dr.FurikomiIrai.ToString();
                    FurikkomiIrai.Text = dr.FurikomiIrai;
                }
                if (!dr.IsTokuisakiNameNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiName").OrderIndex].Text = dr.TokuisakiName.ToString();
                    TokuisakiName.Text = dr.TokuisakiName;
                }
                if (!dr.IsTokuisakiCodeNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColTokuisakiCode").OrderIndex].Text = dr.TokuisakiCode.ToString();
                    TokuisakiCode.Text = dr.TokuisakiCode;
                }
                if (!dr.IsFurikomiBankNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColBank").OrderIndex].Text = dr.FurikomiBank.ToString();
                    Bank.Text = dr.FurikomiBank;
                }
                if (!dr.IsFurikomiShitenNull())
                {
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColFurikomiShiten").OrderIndex].Text = dr.FurikomiShiten.ToString();
                    FurikomiShiten.Text = dr.FurikomiShiten;
                }
                if (!dr.IsNyukinKingakuNull())
                {
                    int kin = int.Parse(dr.NyukinKingaku);
                    //e.Item.Cells[RadG.Columns.FindByUniqueName("ColNyukingaku").OrderIndex].Text = dr.NyukinKingaku.ToString();
                    Nyukingaku.Text = kin.ToString("0,0");
                }
            }
        }

        protected void RadG_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            Create();
        }

        protected void RadG_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Pager)
            {
                (e.Item.Cells[0].Controls[0] as Table).Rows[0].Visible = false;
            }
        }

        protected void BtnSerch_Click(object sender, EventArgs e)
        {
            Create();
        }
    }
}