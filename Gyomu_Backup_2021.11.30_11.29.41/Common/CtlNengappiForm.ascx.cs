using Core.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Common
{
    public partial class CtlNengappiForm : System.Web.UI.UserControl
    {

        public Telerik.Web.UI.RadDatePicker RadDatePickerFrom
        {
            get
            {
                return RdpFrom;
            }
        }
        public Telerik.Web.UI.RadDatePicker RadDatePickerTo
        {
            get
            {
                return RdpTo;
            }
        }


        public Nengappi From
        {
            get
            {
                if (RdpFrom.SelectedDate == null)
                {
                    return null;
                }

                return new Nengappi((DateTime)RdpFrom.SelectedDate);
            }
            set
            {
                if (null != value)
                {
                    RdpFrom.SelectedDate = value.ToDateTime();
                }
            }
        }

        public Nengappi To
        {
            get
            {
                if (RdpTo.SelectedDate == null)
                {
                    return null;
                }

                return new Nengappi((DateTime)RdpTo.SelectedDate);
            }
            set
            {
                if (null != value)
                {
                    RdpTo.SelectedDate = value.ToDateTime();
                }
            }
        }

        /// <summary>
        /// カレンダーの共有
        /// ページサイズ削減の為
        /// </summary>
        public Telerik.Web.UI.RadCalendar SharedCalendar
        {
            set
            {
                this.RdpFrom.SharedCalendar = this.RdpTo.SharedCalendar = value;
            }
        }

        public Core.Type.NengappiKikan.EnumKikanType KikanType
        {
            get
            {
                return (Core.Type.NengappiKikan.EnumKikanType)this.DdlKikan.SelectedIndex;
            }
            set
            {
                this.DdlKikan.SelectedIndex = (int)value;
            }
        }


        public bool IsCreated
        {
            get
            {
                return Convert.ToBoolean(this.ViewState["IsCreated"]);
            }
            set
            {
                this.ViewState["IsCreated"] = value;
            }
        }

        public void RemoveFromTo()
        {
            ListItem item = this.DdlKikan.Items.FindByValue("4");
            if (null != item) this.DdlKikan.Items.Remove(item);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Create();
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: この呼び出しは、ASP.NET Web フォーム デザイナで必要です。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		デザイナ サポートに必要なメソッドです。このファイルの内容を
        ///		コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.CtlNengappiFromTo_PreRender);

        }
        #endregion

        public void Create()
        {
            if (IsCreated) return;

            IsCreated = true;

            // 当日を設定する。
            this.From = this.To = null;// Nengappi.Now;

            //DdlKikan.Items[0].Text = "指定なし";
            //DdlKikan.Items[1].Text = "指定日";
            //DdlKikan.Items[2].Text = "以前";
            //DdlKikan.Items[3].Text = "以降";
            //SDdlKikan.Items[0].Text = "から";
        }


        private void Create(Core.Type.NengappiKikan k)
        {
            this.Create();
            if (null != k)
            {
                if (null != k.From)
                    this.From = k.From;
                if (null != k.To)
                    this.To = k.To;
                this.KikanType = k.KikanType;
            }
        }


        public NengappiKikan GetNengappiKikan()
        {
            if (this.KikanType == Core.Type.NengappiKikan.EnumKikanType.NONE)
                return null;

            NengappiKikan k = new NengappiKikan();
            k.From = this.From;

            k.To = this.To;

            k.KikanType = this.KikanType;
            return k;
        }

        protected void CtlNengappiFromTo_PreRender(object sender, System.EventArgs e)
        {
            // ajaxで本コントロールがクライアントにロードされた時、
            // HTML内に書いたjavascriptが反映されない問題があり、また本コントロールの複数使用によるjvascript関数の重複をしないように
            // javascriptはタグの中に記述するようにした。
            string strDdlKikanOnChange =
                @"
	document.getElementById('{0}').style.display = (0 == this.selectedIndex)? 'none' : '';
	document.getElementById('{1}').style.display = (4 == this.selectedIndex)? '' : 'none';
";

            this.DdlKikan.Attributes["onchange"] =
                string.Format(strDdlKikanOnChange, this.TblFrom.ClientID, this.TblTo.ClientID);

            if (4 != this.DdlKikan.SelectedIndex)
                TblTo.Style.Add("display", "none");
            else
                TblTo.Style.Add("display", "");


            if (0 == this.DdlKikan.SelectedIndex)
                TblFrom.Style.Add("display", "none");
            else
                TblFrom.Style.Add("display", "");
        }
    }
}