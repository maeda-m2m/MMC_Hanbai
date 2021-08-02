using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Master
{
    public partial class CtlBumon : System.Web.UI.UserControl
    {
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

        }

        internal void Create(string Kubun)
        {

            //修正ボタンクリック時にデータ取得
            DataMaster.M_BumonRow dr =
                ClassMaster.GetM_BumonRow(Kubun, Global.GetConnection());

            vsID = Kubun;

            TbxKubun.Text = dr.BumonKubun.ToString();
            TbxBusyo.Text = dr.Busyo;

            if (dr.YubinBango != "")
            {
                TbxYubin1.Text = dr.YubinBango.Substring(1, 3);
                TbxYubin2.Text = dr.YubinBango.Substring(5, 4);
            }

            TbxJusyo1.Text = dr.BumonJusyo1;
            TbxJusyo2.Text = dr.Bumonjusyo2;
            TbxBumon.Text = dr.Bumonbusyo;
        }

        internal bool Toroku()
        {
            //登録データ所得
            try
            {
                DataMaster.M_BumonDataTable dt = new DataMaster.M_BumonDataTable();
                DataMaster.M_BumonRow dr = dt.NewM_BumonRow();

                if(TbxKubun.Text!="")
                {
                    int nKubun = int.Parse(TbxKubun.Text);
                    dr.BumonKubun = nKubun;

                    DataMaster.M_BumonRow Bdr =
                        ClassMaster.GetM_BumonRow(TbxKubun.Text, Global.GetConnection());

                    if(Bdr!=null)
                    {
                        if (vsID != "")
                        {
                            if (vsID != Bdr.BumonKubun.ToString())
                            {
                                Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("この区分は存在しています。");
                                return false;
                            }
                        }
                        else
                        {
                            Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("この区分は存在しています。");
                            return false;
                        }
                    }
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("部門区分を登録してください。");
                    return false;
                }

                if(TbxBusyo.Text!="")
                {
                    dr.Busyo = TbxBusyo.Text;
                }
                else
                {
                    Telerik.Web.UI.RadAjaxManager.GetCurrent(this.Page).Alert("部署名を登録してください。");
                    return false;
                }

                if (TbxJusyo1.Text != "" && TbxJusyo2.Text != "")
                {
                    string sYubin = "〒" + TbxYubin1.Text + "-" + TbxYubin2.Text;
                    dr.YubinBango = sYubin;
                }
                else
                {
                    dr.YubinBango = "";
                }

                dr.Kari = 1;
                dr.Kuuhaku = "";

                if (TbxJusyo1.Text != "")
                {
                    dr.BumonJusyo1 = TbxJusyo1.Text;
                }
                else
                {
                    dr.BumonJusyo1 = "";
                }

                if (TbxJusyo2.Text != "")
                {
                    dr.Bumonjusyo2 = TbxJusyo2.Text;
                }
                else
                {
                    dr.Bumonjusyo2 = "";
                }

                if (TbxBumon.Text != "")
                {
                    dr.Bumonbusyo = TbxBumon.Text;
                }
                else
                {
                    dr.Bumonbusyo = "";
                }

                dr.Kaisyamei = "株式会社ムービーマネジメントカンパニー";

                dr.KousinBi = DateTime.Now;

                dr.RiyoCode = true;

                dt.AddM_BumonRow(dr);

                if (vsID != "")
                {
                    ClassMaster.EditBumon(vsID, dt, Global.GetConnection());
                }
                else
                {
                    ClassMaster.NewBumon(dt, Global.GetConnection());
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        internal void Clear()
        {
            TbxKubun.Text = TbxBusyo.Text = TbxYubin1.Text = TbxYubin2.Text =
            TbxJusyo1.Text = TbxJusyo2.Text = TbxBumon.Text = "";
        }

        protected void Ram_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }
    }
}