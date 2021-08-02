using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Gyomu.Master
{
    public partial class CtlKakaku : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                //7文字まで
                TBHyojun.MaxLength = 7; 
                TBShiire.MaxLength = 7;
                TBCPKakaku.MaxLength = 7;
                TBCPSiire.MaxLength = 7;
            }
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        internal void Create(string strKeys , int nData)
        {
            //価格情報データ取得
            string sSyouhin = "";
            string sCate = "";
            string sShiire = "";

            for (int i = 0; i < nData; i++)
            {
                string sData = strKeys.Split(',')[i];

                if (i!=0)
                {
                    sSyouhin = sSyouhin + ","  + sData.Split('/')[0];
                    sCate = sCate + "," + sData.Split('/')[1] ;
                    sShiire = sShiire + ","  + sData.Split('/')[2];
                }
                else
                {

                    sSyouhin = sData.Split('/')[0] ;
                    sCate =  sData.Split('/')[1];
                    sShiire =  sData.Split('/')[2];
                }
            }

            DataSet1.M_Kakaku_New1DataTable dt =
                ClassMaster.GetM_Kakaku(sSyouhin, sCate, sShiire,nData,Global.GetConnection());

            DataView dv = new DataView(dt);
            G.DataSource = dv;
            G.DataBind();
        }

        protected void G_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            DataMaster.M_Kakaku_NewRow dr = ((DataRowView)e.Row.DataItem).Row as DataMaster.M_Kakaku_NewRow;

            CheckBox C = e.Row.FindControl("C") as CheckBox;
            Label lblSyouhin = e.Row.FindControl("lblSyouhin") as Label;
            Label lblShiire = e.Row.FindControl("lblShiire") as Label;
            Label lblBaitai = e.Row.FindControl("lblBaitai") as Label;
            Label lblCate = e.Row.FindControl("lblCate") as Label;
            //TextBox TbxKubunCode = e.Row.FindControl("TbxKubunCode") as TextBox;
           // TextBox TbxCateKubun = e.Row.FindControl("TbxCateKubun") as TextBox;
            RadDatePicker RadKaishi = e.Row.FindControl("RadKaishi") as RadDatePicker;
            RadDatePicker RadOwari = e.Row.FindControl("RadOwari") as RadDatePicker;
            TextBox TbxHyojun = e.Row.FindControl("TbxHyojun") as TextBox;
            TextBox TbxShiire = e.Row.FindControl("TbxShiire") as TextBox;
            RadDatePicker RadCPKaishi = e.Row.FindControl("RadCPKaishi") as RadDatePicker;
            RadDatePicker RadCPOwari = e.Row.FindControl("RadCPOwari") as RadDatePicker;
            TextBox TbxCPKakaku = e.Row.FindControl("TbxCPKakaku") as TextBox;
            TextBox TbxCPShiire = e.Row.FindControl("TbxCPShiire") as TextBox;
            CheckBox CheckYuko = e.Row.FindControl("CheckYuko") as CheckBox;

            //初期状態は✓を入れる
            C.Checked = true;

            lblSyouhin.Text = dr.SyouhinMei;
            lblShiire.Text = dr.ShiireName;
            lblBaitai.Text = dr.Media;
            lblCate.Text = dr.Categoryname;

            //if (!dr.IsCategoryCodeNull())
            //    TbxKubunCode.Text = dr.CategoryCode.ToString();

            //if (!dr.IsCategorykubunNull())
            //    TbxCateKubun.Text = dr.Categorykubun;

            if (!dr.IsKaisiNull())
                RadKaishi.SelectedDate = dr.Kaisi;

            if (!dr.IsOwariNull())
                RadOwari.SelectedDate = dr.Owari;

            if (!dr.IsHyojunKakakuNull())
                TbxHyojun.Text = dr.HyojunKakaku.ToString();

            if (!dr.IsShiireKakakuNull())
                TbxShiire.Text = dr.ShiireKakaku.ToString();

            if (!dr.IsCpKaisiNull())
                RadCPKaishi.SelectedDate = dr.CpKaisi;

            if (!dr.IsCpOwariNull())
                RadCPOwari.SelectedDate = dr.CpOwari;

            if (!dr.IsCpKakakuNull())
                TbxCPKakaku.Text = dr.CpKakaku.ToString();

            if (!dr.IsCpShiireNull())
                TbxCPShiire.Text = dr.CpShiire.ToString();

            //if(dr.)
            //CheckYuko.Checked = dr.RiyoJoutai;
        }

        protected void TbCode_TextChanged(object sender, EventArgs e)
        {
            if (TbCode.Text != "")
            {
                string type = "KubunCode";
                Ikkatu(type);
            }
        }

        protected void TbCate_TextChanged(object sender, EventArgs e)
        {
            if (TbCate.Text != "")
            {
                //一括登録するのに必要
                string type = "Cate";
                Ikkatu(type);
            }
        }


        protected void ChRiyou_CheckedChanged(object sender, EventArgs e)
        {
            //一括登録するのに必要
            string type = "Riyou";
            Ikkatu(type);
        }

        protected void DateKaisi_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if(DateKaisi.SelectedDate!=null)
            {
                //一括登録するのに必要
                string type = "DateK";
                Ikkatu(type);
            }
        }

        protected void DateOwari_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (DateOwari.SelectedDate != null)
            {
                //一括登録するのに必要
                string type = "DateO";
                Ikkatu(type);
            }
        }

        protected void TBHyojun_TextChanged(object sender, EventArgs e)
        {
            if(TBHyojun.Text!="")
            {
                //一括登録するのに必要
                string type = "Hyojun";
                Ikkatu(type);
            }
        }

        protected void TBShiire_TextChanged(object sender, EventArgs e)
        {
            if(TBShiire.Text!="")
            {
                //一括登録するのに必要
                string type = "Shiire";
                Ikkatu(type);
            }
        }

        protected void DateCPKaishi_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if(DateCPKaishi.SelectedDate!=null)
            {
                //一括登録するのに必要
                string type = "DateCPK";
                Ikkatu(type);
            }
        }

        protected void DateCPOwari_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if(DateCPOwari.SelectedDate!=null)
            {
                //一括登録するのに必要
                string type = "DateCPO";
                Ikkatu(type);
            }
        }

        protected void TBCPKakaku_TextChanged(object sender, EventArgs e)
        {
            if(TBCPKakaku.Text!="")
            {
                //一括登録するのに必要
                string type = "CPKakaku";
                Ikkatu(type);
            }
        }

        protected void TBCPSiire_TextChanged(object sender, EventArgs e)
        {
            if(TBCPSiire.Text!="")
            {
                //一括登録するのに必要
                string type = "CPShiire";
                Ikkatu(type);
            }
        }

        private void Ikkatu(string type)
        {
            for(int i = 0;i<G.Rows.Count;i++)
            {
                CheckBox C = G.Rows[i].FindControl("C") as CheckBox;
                Label lblSyouhin = G.Rows[i].FindControl("lblSyouhin") as Label;
                Label lblShiire = G.Rows[i].FindControl("lblShiire") as Label;
                Label lblBaitai = G.Rows[i].FindControl("lblBaitai") as Label;
                Label lblCate = G.Rows[i].FindControl("lblCate") as Label;
                //TextBox TbxKubunCode = G.Rows[i].FindControl("TbxKubunCode") as TextBox;
                //TextBox TbxCateKubun = G.Rows[i].FindControl("TbxCateKubun") as TextBox;
                RadDatePicker RadKaishi = G.Rows[i].FindControl("RadKaishi") as RadDatePicker;
                RadDatePicker RadOwari = G.Rows[i].FindControl("RadOwari") as RadDatePicker;
                TextBox TbxHyojun = G.Rows[i].FindControl("TbxHyojun") as TextBox;
                TextBox TbxShiire = G.Rows[i].FindControl("TbxShiire") as TextBox;
                RadDatePicker RadCPKaishi = G.Rows[i].FindControl("RadCPKaishi") as RadDatePicker;
                RadDatePicker RadCPOwari = G.Rows[i].FindControl("RadCPOwari") as RadDatePicker;
                TextBox TbxCPKakaku = G.Rows[i].FindControl("TbxCPKakaku") as TextBox;
                TextBox TbxCPShiire = G.Rows[i].FindControl("TbxCPShiire") as TextBox;
                //CheckBox CheckYuko = G.Rows[i].FindControl("CheckYuko") as CheckBox;

                if (C.Checked == true)
                {
                    //if (type == "KubunCode")
                    //{
                    //    TbxKubunCode.Text = TbCode.Text;
                    //}

                    //if(type=="Cate")
                    //{
                    //    TbxCateKubun.Text = TbCate.Text;
                    //}

                    //if(type== "Riyou")
                    //{
                    //    CheckYuko.Checked = ChRiyou.Checked;
                    //}

                    if(type== "DateK")
                    {
                        RadKaishi.SelectedDate = DateKaisi.SelectedDate;
                    }

                    if (type == "DateO")
                    {
                        RadOwari.SelectedDate = DateOwari.SelectedDate;
                    }

                    if(type== "Hyojun")
                    {
                        TbxHyojun.Text = TBHyojun.Text;
                    }

                    if(type== "Shiire")
                    {
                        TbxShiire.Text = TBShiire.Text;
                    }

                    if (type == "DateCPK")
                    {
                        RadCPKaishi.SelectedDate = DateCPKaishi.SelectedDate;
                    }

                    if (type == "DateCPO")
                    {
                        RadCPOwari.SelectedDate = DateCPOwari.SelectedDate;
                    }

                    if(type== "CPKakaku")
                    {
                        TbxCPKakaku.Text = TBCPKakaku.Text;
                    }

                    if(type== "CPShiire")
                    {
                        TbxCPShiire.Text = TBCPSiire.Text;
                    }
                }
            }
        }

        internal void Clear()
        {
            TbCode.Text = TbCate.Text = TBHyojun.Text = TBShiire.Text = TBCPKakaku.Text = TBCPSiire.Text = "";
            DateKaisi.SelectedDate = DateOwari.SelectedDate = DateCPKaishi.SelectedDate = DateCPOwari.SelectedDate = null;
            ChRiyou.Checked = true;
        }

        internal bool Toroku()
        {
            //登録処理
            try
            {
                DataMaster.M_Kakaku_NewDataTable dt = new DataMaster.M_Kakaku_NewDataTable();

                string sSyouhin = "";
                string sShiire = "";
                string sBaitai = "";
                string sCate = "";

                for (int i=0;i<G.Rows.Count;i++)
                {
                    DataMaster.M_Kakaku_NewRow dr = dt.NewM_Kakaku_NewRow();

                    Label lblSyouhin = G.Rows[i].FindControl("lblSyouhin") as Label;
                    Label lblShiire = G.Rows[i].FindControl("lblShiire") as Label;
                    Label lblBaitai = G.Rows[i].FindControl("lblBaitai") as Label;
                    Label lblCate = G.Rows[i].FindControl("lblCate") as Label;
                   // TextBox TbxKubunCode = G.Rows[i].FindControl("TbxKubunCode") as TextBox;
                   // TextBox TbxCateKubun = G.Rows[i].FindControl("TbxCateKubun") as TextBox;
                    RadDatePicker RadKaishi = G.Rows[i].FindControl("RadKaishi") as RadDatePicker;
                    RadDatePicker RadOwari = G.Rows[i].FindControl("RadOwari") as RadDatePicker;
                    TextBox TbxHyojun = G.Rows[i].FindControl("TbxHyojun") as TextBox;
                    TextBox TbxShiire = G.Rows[i].FindControl("TbxShiire") as TextBox;
                    RadDatePicker RadCPKaishi = G.Rows[i].FindControl("RadCPKaishi") as RadDatePicker;
                    RadDatePicker RadCPOwari = G.Rows[i].FindControl("RadCPOwari") as RadDatePicker;
                    TextBox TbxCPKakaku = G.Rows[i].FindControl("TbxCPKakaku") as TextBox;
                    TextBox TbxCPShiire = G.Rows[i].FindControl("TbxCPShiire") as TextBox;
                   // CheckBox CheckYuko = G.Rows[i].FindControl("CheckYuko") as CheckBox;

                     sSyouhin = lblSyouhin.Text;
                     sShiire = lblShiire.Text;
                     sBaitai = lblBaitai.Text;
                     sCate = lblCate.Text;

                    //元々のデータを取得
                    DataMaster.M_Kakaku_NewRow Mdr =
                        ClassMaster.GetKakakuData(sSyouhin, sShiire, sBaitai, sCate, Global.GetConnection());

                    dr.SyouhinCode = Mdr.SyouhinCode;
                    dr.SyouhinMei = sSyouhin;
                    dr.ShiireCode = Mdr.ShiireCode;
                    dr.ShiireName = Mdr.ShiireName;
                    dr.Media = Mdr.Media;
                    dr.CategoryCode = Mdr.CategoryCode;
                    dr.Categoryname = Mdr.Categoryname;

                    //if (TbxKubunCode.Text != "")
                    //{
                    //    int nkubun = int.Parse(TbxKubunCode.Text);
                    //    dr.CategorykubunCode = nkubun;
                    //}

                    //if (TbxCateKubun.Text != "")
                    //    dr.Categorykubun = TbxCateKubun.Text;

                    if (RadKaishi.SelectedDate != null)
                    {
                        string sDate = RadKaishi.SelectedDate.ToString();
                        DateTime date = DateTime.Parse(sDate);
                        dr.Kaisi = date;
                    }

                    if (RadOwari.SelectedDate != null)
                    {
                        string sDate = RadOwari.SelectedDate.ToString();
                        DateTime date = DateTime.Parse(sDate);
                        dr.Owari = date;
                    }

                    if (TbxHyojun.Text != "")
                    {
                        int nHyojun = int.Parse(TbxHyojun.Text);
                        dr.HyojunKakaku = nHyojun;
                    }

                    if (TbxShiire.Text != "")
                    {
                        int nShiire = int.Parse(TbxShiire.Text);
                        dr.ShiireKakaku = nShiire;
                    }

                    if (RadCPKaishi.SelectedDate != null)
                    {
                        string sDate = RadCPKaishi.SelectedDate.ToString();
                        DateTime date = DateTime.Parse(sDate);
                        dr.CpKaisi = date;
                    }

                    if (RadCPOwari.SelectedDate != null)
                    {
                        string sDate = RadCPOwari.SelectedDate.ToString();
                        DateTime date = DateTime.Parse(sDate);
                        dr.CpOwari = date;
                    }

                    if (TbxCPKakaku.Text != "")
                    {
                        int nHyojun = int.Parse(TbxCPKakaku.Text);
                        dr.CpKakaku = nHyojun;
                    }

                    if (TbxCPShiire.Text != "")
                    {
                        int nShiire = int.Parse(TbxCPShiire.Text);
                        dr.CpShiire = nShiire;
                    }

                    //dr.RiyoJoutai = CheckYuko.Checked;

                    dt.AddM_Kakaku_NewRow(dr);
                }

                ClassMaster.KakakuMaster(dt,sSyouhin, sShiire, sBaitai, sCate, Global.GetConnection());

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}