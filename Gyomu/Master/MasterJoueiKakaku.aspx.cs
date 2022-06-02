using System;
using DLL;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace Gyomu.Master
{
    public partial class MasterJoueiKakaku : System.Web.UI.Page
    {

        public string Text
        {
            get
            {
                object o = ViewState["Text"];
                return (o == null) ? string.Empty : (string)o;
            }

            set
            {
                ViewState["Text"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //K.Style["dispay"] = "";
                SetHani(head_rcb1);//範囲
                SetSeki(head_rcb2);//席数
                SetHani(head_rcmb_hani);
                head_lbl1.Visible = true;
                MesseageLabel.Visible = true;
                insert_panel.Visible = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------
        public class WhereGenerator
        {
            private System.Collections.ArrayList m_lstWhere = new System.Collections.ArrayList();
            public string WhereText
            {
                get
                {
                    string str = "";
                    for (int i = 0; i < m_lstWhere.Count; i++)
                    {
                        str += " " + m_lstWhere[i].ToString() + " ";
                        if (i < m_lstWhere.Count - 1) str += " AND ";
                    }
                    return str;
                }
            }

            public void Add(string strWhere)
            {
                strWhere = strWhere.Trim();
                if (null != strWhere && "" != strWhere)
                    m_lstWhere.Add(strWhere);
            }

            public WhereGenerator()
            {
            }
        }

        public class KensakuParam
        {
            public string z1 = null;
            public string z2 = null;
            public string z3 = null;
            public string z4 = null;
            public string z5 = null;
            public string z6 = null;
            public string z7 = null;

            internal void SetWhere1(SqlDataAdapter da, WhereGenerator w)
            {
                if (z1 != null)
                {
                    w.Add("Media like @c1");
                    da.SelectCommand.Parameters.AddWithValue("@c1", z1);
                }
                if (z2 != null)
                {
                    w.Add("ShiiresakiCode like @c2");
                    da.SelectCommand.Parameters.AddWithValue("@c2", z2);
                }
                if (z3 != null)
                {
                    w.Add("ShiiresakiName like @c3");
                    da.SelectCommand.Parameters.AddWithValue("@c3", z3);
                }
                if (z4 != null)
                {
                    w.Add("Range like @c4");
                    da.SelectCommand.Parameters.AddWithValue("@c4", z4);
                }
                if (z5 != null)
                {
                    w.Add("Capacity like @c5");
                    da.SelectCommand.Parameters.AddWithValue("@c5", z5);
                }
                if (z6 != null)
                {
                    w.Add("HyoujunKakaku like @c6");
                    da.SelectCommand.Parameters.AddWithValue("@c6", z6);
                }
                if (z7 != null)
                {
                    w.Add("ShiireKakaku like @c7");
                    da.SelectCommand.Parameters.AddWithValue("@c7", z7);
                }
            }
        }

        private KensakuParam SetKensakuParam()
        {
            var k = new KensakuParam();

            if (head_ddl1.SelectedValue != "")
            {
                k.z1 = head_ddl1.SelectedValue;
            }
            if (head_rcb.SelectedValue != "")
            {
                string[] Additems = head_rcb.SelectedValue.Split('/');
                k.z2 = Additems[0];
                k.z3 = Additems[1];
            }
            if (head_rcb1.SelectedValue != "")
            {
                k.z4 = head_rcb1.SelectedValue;
            }
            if (head_rcb2.SelectedValue != "")
            {
                k.z5 = head_rcb2.SelectedValue;
            }
            if (head_txt2.Text != "")
            {
                string txt1 = head_txt2.Text;
                txt1 = txt1.Replace(",", "");
                txt1 = txt1.Replace("\"", "");
                txt1 = txt1.Replace("￥", "");
                txt1 = txt1.Replace("円", "");
                k.z6 = txt1;
            }
            if (head_txt3.Text != "")
            {
                string txt1 = head_txt3.Text;
                txt1 = txt1.Replace(",", "");
                txt1 = txt1.Replace("\"", "");
                txt1 = txt1.Replace("￥", "");
                txt1 = txt1.Replace("円", "");
                k.z7 = txt1;
            }
            return k;
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //データバインド用
        private void Create()
        {
            try
            {
                var k = SetKensakuParam();
                var dt = GetMitumori(k, Global.GetConnection());
                if (dt.Rows.Count == 0)
                {
                    head_lbl1.Visible = true;
                    MesseageLabel.Visible = false;
                    head_lbl1.Text = "データが見つかりませんでした";
                    DGJoueiKakaku1.Visible = false;
                    return;
                }
                else
                {
                    head_lbl1.Visible = false;
                    MesseageLabel.Visible = false;
                    DGJoueiKakaku1.Visible = true;
                }
                DGJoueiKakaku1.VirtualItemCount = dt.Count;
                int nPageSize = DGJoueiKakaku1.PageSize;
                int nPageCount = dt.Count / nPageSize;
                if (0 < dt.Count % nPageSize) nPageCount++;
                if (nPageCount <= DGJoueiKakaku1.MasterTableView.CurrentPageIndex) DGJoueiKakaku1.MasterTableView.CurrentPageIndex = 0;
                DGJoueiKakaku1.DataSource = dt;
                DGJoueiKakaku1.DataBind();
            }
            catch (Exception ex)
            {
                head_lbl1.Text = ex.Message;
            };

        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------
        //radの内容を表示するための処理を他から取ってきている
        //----------------------------------------------------------------------------------------------------------------------------------
        protected void Unnamed_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            SetShiire(sender, e);

        }
        protected void head_shiire_name_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            SetShiire(sender, e);
        }

        internal static void SetShiire(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            var dt = Shiire(e.Text.Trim(), Global.GetConnection());
            string items = "";
            for (int d = 0; d < dt.Count; d++)
            {
                if (items == "")
                {
                    items = dt[d].ShiiresakiCode.ToString() + "/" + dt[d].ShiiresakiName;
                }
                else
                {
                    if (!items.Contains(dt[d].ShiiresakiCode.ToString()))
                    {
                        items += "," + dt[d].ShiiresakiCode.ToString() + "/" + dt[d].ShiiresakiName;
                    }
                    if (!items.Contains(dt[d].ShiiresakiName))
                    {
                        items += "," + dt[d].ShiiresakiCode.ToString() + "/" + dt[d].ShiiresakiName;
                    }
                }
            }
            string[] Additems = items.Split(',');
            for (int a = 0; a < Additems.Length; a++)
            {
                rad.Items.Add(new RadComboBoxItem(Additems[a], Additems[a]));
            }

            //for (int i = 0; i < dt.Count; i++)s
            //{
            //    rad.Items.Add(new RadComboBoxItem(dt[i].Range, dt[i].Range));
            //}
        }
        public static DataMaster.M_JoueiKakaku2DataTable Shiire(string v, SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_JoueiKakaku2 where ShiiresakiCode like @e or ShiiresakiName like @e order by ShiiresakiCode asc, ShiiresakiName asc;";
            da.SelectCommand.Parameters.AddWithValue("@e", v + "%");
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }


        ////----------------------------------------------------------------------------------------------------------------------------------
        ////radの「仕入先コード」の値を表示するための処理、listsetと同じ（このファイルに直書きしている）
        ////----------------------------------------------------------------------------------------------------------------------------------
        //internal static void SetShiireCode(RadComboBox rad)
        //{
        //    rad.Items.Clear();
        //    rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
        //    var dt = ShiireCodeDrop(Global.GetConnection());
        //    string items = "";
        //    for (int d = 0; d < dt.Count; d++)
        //    {
        //        if (items == "")
        //        {
        //            items = dt[d].ShiiresakiCode.ToString();
        //        }
        //        else
        //        {
        //            if (!items.Contains(dt[d].ShiiresakiCode.ToString()))
        //            {
        //                items += "," + dt[d].ShiiresakiCode.ToString();
        //            }
        //        }
        //    }
        //    string[] Additems = items.Split(',');
        //    for (int a = 0; a < Additems.Length; a++)
        //    {
        //        rad.Items.Add(new RadComboBoxItem(Additems[a], Additems[a]));
        //    }

        //    //for (int i = 0; i < dt.Count; i++)
        //    //{
        //    //    rad.Items.Add(new RadComboBoxItem(dt[i].Range, dt[i].Range));
        //    //}
        //}
        //public static DataMaster.M_JoueiKakaku2DataTable ShiireCodeDrop(SqlConnection sqlConnection)
        //{
        //    var da = new SqlDataAdapter("", sqlConnection);
        //    da.SelectCommand.CommandText =
        //        "SELECT * FROM M_JoueiKakaku2 order by ShiiresakiCode asc";
        //    var dt = new DataMaster.M_JoueiKakaku2DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}


        //----------------------------------------------------------------------------------------------------------------------------------
        //radの「席数」の値を表示するための処理、listsetと同じ（このファイルに直書きしている）
        //----------------------------------------------------------------------------------------------------------------------------------
        internal static void SetSeki(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            var dt = SekiDrop(Global.GetConnection());
            string items = "";
            for (int d = 0; d < dt.Count; d++)
            {
                if (items == "")
                {
                    items = dt[d].Capacity.Trim();
                }
                else
                {
                    if (!items.Contains(dt[d].Capacity))
                    {
                        items += "," + dt[d].Capacity.Trim();
                    }
                }
            }
            string[] Additems = items.Split(',');
            for (int a = 0; a < Additems.Length; a++)
            {
                rad.Items.Add(new RadComboBoxItem(Additems[a], Additems[a]));
            }

            //for (int i = 0; i < dt.Count; i++)
            //{
            //    rad.Items.Add(new RadComboBoxItem(dt[i].Range, dt[i].Range));
            //}
        }
        public static DataMaster.M_JoueiKakaku2DataTable SekiDrop(SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_JoueiKakaku2 order by Capacity asc ";
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //radの「範囲」の値を表示するための処理、listsetと同じ（このファイルに直書きしている）
        //----------------------------------------------------------------------------------------------------------------------------------
        internal static void SetHani(RadComboBox rad)
        {
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            var dt = HaniDrop(Global.GetConnection());
            string items = "";
            for (int d = 0; d < dt.Count; d++)
            {
                if (items == "")
                {
                    items = dt[d].Range.Trim();
                }
                else
                {
                    if (!items.Contains(dt[d].Range))
                    {
                        items += "," + dt[d].Range.Trim();
                    }
                }
            }
            string[] Additems = items.Split(',');
            for (int a = 0; a < Additems.Length; a++)
            {
                rad.Items.Add(new RadComboBoxItem(Additems[a], Additems[a]));
            }
            //for (int i = 0; i < dt.Count; i++)
            //{
            //    rad.Items.Add(new RadComboBoxItem(dt[i].Range, dt[i].Range));
            //}
        }
        public static DataMaster.M_JoueiKakaku2DataTable HaniDrop(SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_JoueiKakaku2 order by Range";
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //検索ボタンの処理
        protected void head_btn1_Click(object sender, EventArgs e)
        {
            Create();
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        //新規登録画面登録の処理の本体
        //----------------------------------------------------------------------------------------------------------------------------------
        internal bool Toroku()
        {
            try
            {
                var dt = new DataMaster.M_JoueiKakaku2DataTable();

                var dr = dt.NewM_JoueiKakaku2Row();

                //var dl = MaxNo(Global.GetConnection());

                //int no = dl.KanriNo;

                //dr.KanriNo = no + 1;

                string[] Additems = head_shiire_name.SelectedValue.Split('/');

                dr.ShiiresakiCode = int.Parse(Additems[0]);
                dr.ShiiresakiName = Additems[1];
                dr.Media = head_cmb_media.SelectedValue;
                dr.Range = head_rcmb_hani.SelectedValue;
                dr.Capacity = head_txt_seki.Text;
                dr.HyoujunKakaku = head_txt_hyoujun.Text;
                dr.ShiireKakaku = head_txt_shiirekakaku.Text;
                dt.AddM_JoueiKakaku2Row(dr);

                Insert(dt, Global.GetConnection());

                return (true);
            }
            catch
            {
                return (false);
            }
        }

        //新規登録画面登録の処理
        protected void BtnToroku_Click(object sender, EventArgs e)
        {
            bool bo = Toroku();
            if (bo)
            {

                System.Windows.Forms.MessageBox.Show("登録に成功しました", "メッセージボックス", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                insert_lbl.Text = "登録に失敗しました";
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //新規登録画面戻るボタンの処理
        //----------------------------------------------------------------------------------------------------------------------------------
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MasterJoueiKakaku.aspx");
        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //新規登録ボタンの処理
        protected void head_shinki_btn1_Click(object sender, EventArgs e)
        {
            insert_panel.Visible = true;
            main_panel.Visible = false;


        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //以下グリッド表の各種イベント
        //----------------------------------------------------------------------------------------------------------------------------------





        protected void DGJoueiKakaku1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            Create();
        }





        protected void DGJoueiKakaku1_EditCommand(object sender, GridCommandEventArgs e)
        {
            //メディア、メーカーコード、範囲、席数
            ViewState["Text"] = $"{(e.Item.Controls[2] as TableCell).Text}-{(e.Item.Controls[3] as TableCell).Text}-{(e.Item.Controls[5] as TableCell).Text}-{(e.Item.Controls[6] as TableCell).Text}";



            Create();

        }






        //桁区切りのため
        protected void DGJoueiKakaku1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;
                    var dr = (DataMaster.M_JoueiKakaku2Row)drv.Row;

                    if (!dr.IsShiireKakakuNull())
                    {
                        int i1 = int.Parse(dr.ShiireKakaku);
                        e.Item.Cells[DGJoueiKakaku1.Columns.FindByUniqueName("shiirekakaku").OrderIndex].Text = string.Format("{0:#,0}", i1);
                    }
                    int i2 = int.Parse(dr.HyoujunKakaku);
                    e.Item.Cells[DGJoueiKakaku1.Columns.FindByUniqueName("hyoujunkakaku").OrderIndex].Text = string.Format("{0:#,0}", i2);
                }
            }
            catch { }
        }





        protected void DGJoueiKakaku1_UpdateCommand(object sender, GridCommandEventArgs e)
        {



            //cellのitemのindex


            var k = SetKensakuParam();

            var dt = GetMitumori(k, Global.GetConnection());

            int i = e.Item.DataSetIndex;

            var dr = dt.Rows[i] as DataMaster.M_JoueiKakaku2Row;

            //rowのitemのindex
            dr[0] = (e.Item.Cells[3].Controls[0] as TextBox).Text.Trim();
            dr[1] = (e.Item.Cells[4].Controls[0] as TextBox).Text.Trim();
            dr[2] = (e.Item.Cells[2].Controls[0] as TextBox).Text.Trim();
            dr[3] = (e.Item.Cells[5].Controls[0] as TextBox).Text.Trim();
            dr[4] = (e.Item.Cells[6].Controls[0] as TextBox).Text.Trim();
            dr[5] = (e.Item.Cells[7].Controls[0] as TextBox).Text.Trim();
            dr[6] = (e.Item.Cells[8].Controls[0] as TextBox).Text.Trim();

            var primeKeys = ViewState["Text"].ToString().Split('-');


            Update(dr, primeKeys, Global.GetConnection());

            Create();

            System.Windows.Forms.MessageBox.Show("値が更新されました", "メッセージボックス", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
        }






        //編集コマンドで値を変更する
        public static DataMaster.M_JoueiKakaku2Row Update(DataMaster.M_JoueiKakaku2Row dr, string[] primeKeys, SqlConnection sql)
        {
            {

                var a = new SqlCommand("", sql)
                {
                    CommandText = @"
                    UPDATE M_JoueiKakaku2 
                    SET [ShiiresakiCode] = @shiiresakicode, [ShiiresakiName] = @shiiresakiname, [Media] = @media, [Range] = @range, [Capacity] = @capacity, [HyoujunKakaku] = @hyoujunkakaku, [ShiireKakaku] = @shiirekakaku
                    where ShiiresakiCode = @primeCode and Media = @primeMedia and Range = @primeRange and Capacity = @primeCapacity
                    "

                };
                a.Parameters.AddWithValue("@shiiresakicode", dr.ShiiresakiCode);
                a.Parameters.AddWithValue("@shiiresakiname", dr.ShiiresakiName);
                a.Parameters.AddWithValue("@media", dr.Media);
                a.Parameters.AddWithValue("@range", dr.Range);
                a.Parameters.AddWithValue("@capacity", dr.Capacity);
                a.Parameters.AddWithValue("@hyoujunkakaku", dr.HyoujunKakaku);
                a.Parameters.AddWithValue("@shiirekakaku", dr.ShiireKakaku);

                //メディア、メーカーコード、範囲、席数
                a.Parameters.AddWithValue("@primeMedia", primeKeys[0]);
                a.Parameters.AddWithValue("@primeCode", primeKeys[1]);
                a.Parameters.AddWithValue("@primeRange", primeKeys[2]);
                a.Parameters.AddWithValue("@primeCapacity", primeKeys[3]);

                try
                {
                    sql.Open();
                    SqlTransaction sqltra = sql.BeginTransaction();
                    a.Transaction = sqltra;
                    a.ExecuteNonQuery();
                    sqltra.Commit();
                }
                catch
                {

                }
                finally
                {
                    sql.Close();
                }
                return dr;
            }
        }










        protected void DGJoueiKakaku1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("本当に実行しますか", "メッセージボックス", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.None, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    int i = e.Item.DataSetIndex;
                    //var dt = Get_All(Global.GetConnection());
                    var k = SetKensakuParam();
                    var dt = GetMitumori(k, Global.GetConnection());
                    var dr = dt.Rows[i] as DataMaster.M_JoueiKakaku2Row;
                    DeleteRow(dr, Global.GetConnection());
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {

                }
            }
            Create();

        }
        //----------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------
        //クラス一覧
        //----------------------------------------------------------------------------------------------------------------------------------
        public static DataMaster.M_JoueiKakaku2DataTable Get_All(SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_JoueiKakaku2";
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }
        public static DataMaster.M_JoueiKakaku2DataTable GetMitumori(KensakuParam k, SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "SELECT * FROM M_JoueiKakaku2";
            if (k != null)
            {
                var w = new WhereGenerator();
                k.SetWhere1(da, w);
                if (!string.IsNullOrEmpty(w.WhereText))
                    da.SelectCommand.CommandText += " Where " + w.WhereText;
                da.SelectCommand.CommandText += " order by ShiiresakiCode asc ";
            }
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }
        internal static void SetShiireSaki(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            RadComboBox rad = (RadComboBox)sender;
            rad.Items.Clear();
            rad.Items.Add(new RadComboBoxItem(rad.EmptyMessage, ""));
            if (e.Text.Trim() != "")
            {
                var dt = GetShiireDT1(e.Text.Trim(), Global.GetConnection());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //rad.Items.Add(new RadComboBoxItem(dt[i].Abbreviation, dt[i].ShiireName));
                    rad.Items.Add(new RadComboBoxItem(dt[i].Capacity, dt[i].Capacity));
                }
            }
        }
        public static DataMaster.M_JoueiKakaku2DataTable GetShiireDT1(string v, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT * FROM M_JoueiKakaku2 WHERE (Capacity LIKE @e)";
            da.SelectCommand.Parameters.AddWithValue("@e", v + "%");
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }
        public static DataMaster.M_JoueiKakaku2DataTable GetShiireDT2(SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText = "SELECT Capacity FROM M_JoueiKakaku2 GROUP BY Capacity";
            var dt = new DataMaster.M_JoueiKakaku2DataTable();
            da.Fill(dt);
            return dt;
        }




        //public static DataMaster.M_JoueiKakaku2Row MaxNo(SqlConnection schedule)
        //{
        //    var da = new SqlDataAdapter("", schedule);
        //    da.SelectCommand.CommandText =
        //        "SELECT * FROM M_JoueiKakaku2 ORDER BY KanriNo desc";
        //    var dt = new DataMaster.M_JoueiKakaku2DataTable();
        //    da.Fill(dt);
        //    return dt[0];
        //}




        //登録に使うやつ
        public static void Insert(DataMaster.M_JoueiKakaku2DataTable dt, SqlConnection sqlConnection)
        {
            var da = new SqlDataAdapter("", sqlConnection);
            da.SelectCommand.CommandText =
                "Select * From M_JoueiKakaku2";
            da.InsertCommand = (new SqlCommandBuilder(da)).GetInsertCommand();
            SqlTransaction sql = null;
            try
            {
                sqlConnection.Open();
                sql = sqlConnection.BeginTransaction();
                da.SelectCommand.Transaction = da.InsertCommand.Transaction = sql;
                da.Update(dt);
                sql.Commit();
            }
            catch (Exception ex)
            {
                if (sql != null)
                    sql.Rollback();
            }
            finally
            {
                sqlConnection.Close();
            }
        }












        public static void DeleteRow(DataMaster.M_JoueiKakaku2Row dr, SqlConnection sql)
        {
            var da = new SqlCommand("", sql)
            {
                CommandText =
                @"
DELETE FROM M_JoueiKakaku2 
where ShiiresakiCode = @ShiiresakiCode and Media = @Media and Range = @Range and Capacity = @Capacity"
            };

            da.Parameters.AddWithValue("@ShiiresakiCode", dr.ShiiresakiCode);
            da.Parameters.AddWithValue("@Media", dr.Media);
            da.Parameters.AddWithValue("@Range", dr.Range);
            da.Parameters.AddWithValue("@Capacity", dr.Capacity);


            try
            {
                sql.Open();
                SqlTransaction sqltra = sql.BeginTransaction();
                da.Transaction = sqltra;
                da.ExecuteNonQuery();
                sqltra.Commit();
            }
            finally
            {
                sql.Close();
            }
        }











        protected void DownLoadButton_Click(object sender, EventArgs e)
        {
            string sqlCommand = "select * from M_JoueiKakaku2";

            var table = Tokuisaki.CommonClass.SelectedTable(sqlCommand, Global.GetConnection());

            string rows = "仕入先コード,仕入先名,メディア,範囲,席数,価格,仕入先価格" + "\r";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                rows += $"{table.Rows[i].ItemArray[0]},{table.Rows[i].ItemArray[1]},{table.Rows[i].ItemArray[2]},{table.Rows[i].ItemArray[3]},{table.Rows[i].ItemArray[4]},{table.Rows[i].ItemArray[5]},{table.Rows[i].ItemArray[6]}" + "\r";
            }

            string strFileName = ("上映会マスタcsv") + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + "csv";

            this.Ram.ResponseScripts.Add(string.Format("window.location.href='{0}';", this.ResolveUrl("~/Common/DownloadDataForm.aspx?" + Common.DownloadDataForm.GetQueryString4Text(strFileName, rows))));


        }









        protected void Ram_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }











        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                head_lbl1.Visible = false;
                MesseageLabel.Visible = true;
                MesseageLabel.Text = "アップロードするファイルを選択してください。";
                return;
            }


            Stream s = FileUpload.FileContent;

            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);

            StreamReader check = new StreamReader(s, enc);

            string strCheck = check.ReadLine();

            if (strCheck == null)
            {
                head_lbl1.Visible = false;
                MesseageLabel.Visible = true;
                MesseageLabel.Text = "ファイルが読み込めませんでした。";

                return;
            }

            bool bTab = strCheck.Split('\t').Length > strCheck.Split(',').Length;



            if (bTab)
            {
                while (check.EndOfStream == false)
                {
                    string strLineData = check.ReadLine();

                    string[] mData = strLineData.Split('\t');

                    var dt = new DataMaster.M_JoueiKakaku2DataTable();

                    var dr = dt.NewM_JoueiKakaku2Row();

                    dr.ItemArray = mData;

                    dt.AddM_JoueiKakaku2Row(dr);

                    UpdateCSVtanto(dt, Global.GetConnection());
                }
            }
            else
            {
                while (check.EndOfStream == false)
                {
                    string strLineData = check.ReadLine();

                    string[] mData = strLineData.Split(',');

                    var dt = new DataMaster.M_JoueiKakaku2DataTable();

                    var dr = dt.NewM_JoueiKakaku2Row();

                    dr.ItemArray = mData;

                    dt.AddM_JoueiKakaku2Row(dr);

                    UpdateCSVtanto(dt, Global.GetConnection());
                }
            }
            head_lbl1.Visible = false;
            MesseageLabel.Visible = true;
            MesseageLabel.Text = "ファイルのアップロードに成功しました。";


        }





        private void UpdateCSVtanto(DataMaster.M_JoueiKakaku2DataTable dt, SqlConnection sqlConnection)
        {

            SqlDataAdapter da = new SqlDataAdapter("", sqlConnection);

            da.SelectCommand.CommandText = @"
select * from M_JoueiKakaku2 
where ShiiresakiCode = @ShiiresakiCode and Media = @Media and Range = @Range and Capacity = @Capacity";

            da.SelectCommand.Parameters.AddWithValue("@ShiiresakiCode", dt[0].ShiiresakiCode);
            da.SelectCommand.Parameters.AddWithValue("@Media", dt[0].Media);
            da.SelectCommand.Parameters.AddWithValue("@Range", dt[0].Range);
            da.SelectCommand.Parameters.AddWithValue("@Capacity", dt[0].Capacity);

            var dtN = new DataMaster.M_JoueiKakaku2DataTable();

            da.Fill(dtN);

            SqlTransaction sqlTran = null;

            da.UpdateCommand = new SqlCommandBuilder(da).GetUpdateCommand();

            da.InsertCommand = new SqlCommandBuilder(da).GetInsertCommand();

            try
            {
                if (dtN.Count > 0)
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.UpdateCommand.Transaction = sqlTran;
                    dtN[0].ItemArray = dt[0].ItemArray;
                    da.Update(dtN);
                    sqlTran.Commit();
                }
                else
                {
                    sqlConnection.Open();
                    sqlTran = sqlConnection.BeginTransaction();
                    da.SelectCommand.Transaction = da.InsertCommand.Transaction = sqlTran;
                    da.Update(dt);
                    sqlTran.Commit();
                }
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    head_lbl1.Visible = false;
                    MesseageLabel.Visible = true;
                    MesseageLabel.Text = "ファイルのアップロードに失敗しました。";

                    sqlTran.Rollback();
                }
            }
            finally
            {
                sqlConnection.Close();
            }

        }



        //----------------------------------------------------------------------------------------------------------------------------------


    }
}
