using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Gyomu;
using DLL;

namespace Yodokou_HanbaiKanri
{
    public class UserViewManager
    {
        public enum EnumItemType
        {
            Header, DataRow, Footer
        }

        private const string UniqueNamePrefix = "__UserView@";

        /// <summary>
        /// 画面での操作用のクラス
        /// </summary>
        public class EditDataField
        {
            public string FieldName
            {
                get;
                set;
            }
            public string Caption
            {
                get;
                set;
            }

            public bool Visible
            {
                get;
                set;
            }

            public bool ForceVisible
            {
                get;
                set;
            }


            public Core.Sql.EnumColumnType ColumnType
            {
                get;
                set;
            }
        }

        public static string GetGroupText(Core.Sql.EnumGroupType t)
        {
            switch (t)
            {
                case Core.Sql.EnumGroupType.AVG: return "平均";
                case Core.Sql.EnumGroupType.COUNT: return "カウント";
                case Core.Sql.EnumGroupType.COUNT_DISTINCT: return "個別のカウント";
                case Core.Sql.EnumGroupType.GROUP: return "グループ化";
                case Core.Sql.EnumGroupType.MIN: return "最小";
                case Core.Sql.EnumGroupType.MAX: return "最大";
                case Core.Sql.EnumGroupType.SUM: return "合計";

            }
            return null;
        }

        public class UserViewEventArgs
        {
            public class DisplayTableCells : System.Collections.Generic.Dictionary<string, TableCell>
            {
                new public TableCell this[string strFieldName]
                {
                    get
                    {
                        if (base.ContainsKey(strFieldName))
                            return base[strFieldName];
                        else
                            return new TableCell();         // インデクサのアクセスで対象フィールドが無いことによるエラーが発生しないようにダミーを返す
                    }
                }
            }

            private DisplayTableCells _tblTableCell = new DisplayTableCells();

            public DisplayTableCells TableCells
            {
                get
                {
                    return _tblTableCell;
                }
            }

            public UserView UserView
            {
                get;
                set;
            }
            public int DataRowIndex
            {
                get;
                internal set;
            }
            public EnumItemType ItemType
            {
                get;
                set;
            }

            public Telerik.Web.UI.GridItem GridItem
            {
                get;
                set;
            }

            public DataRow DataRow
            {
                get;
                set;
            }

            public DataView DataView
            {
                get;
                set;
            }
        }

        public class UserView
        {
            public int ListID
            {
                get;
                internal set;
            }
            public string UserID
            {
                get;
                internal set;
            }

            public string SelectCommandText
            {
                get;
                set;
            }

            public bool CachedSqlDataFactory
            {
                get;
                set;
            }

            public Core.Sql.SqlDataFactory SqlDataFactory
            {
                get;
                set;
            }




            public delegate void DataBoundEventHandler(UserViewEventArgs e);


            private event DataBoundEventHandler _DataBoundEventHandler = null;

            System.Text.StringBuilder _sb = new System.Text.StringBuilder();
            System.Web.UI.HtmlTextWriter _writer = null;


            public string SortText_ASC
            {
                get;
                set;
            }
            public string SortText_DESC
            {
                get;
                set;
            }


            public string HeaderCss
            {
                get;
                set;
            }

            /// <summary>
            /// データソース（データテーブル）に表示カラムが無ければエラーとするかどうか
            /// </summary>
            public bool ErrorIfNotFoundColumnInDataSource
            {
                get;
                set;
            }


            public List<DataField> GetVisibleDataFields()
            {
                System.Collections.Generic.List<DataField> lst = new System.Collections.Generic.List<DataField>();

                for (int i = 0; i < this.GridColumns.Count; i++)
                {
                    List<DataField> l = this.GridColumns[i].VisibleDataFields;
                    for (int t = 0; t < l.Count; t++)
                        lst.Add(l[t]);
                }

                return lst;
            }



            /// <summary>
            /// 表示対象の列を設定する。
            /// </summary>
            /// <returns></returns>
            private List<GridColumn> GetBindGridColumns()
            {

                System.Collections.Generic.List<GridColumn> lst = new System.Collections.Generic.List<GridColumn>();

                if (null != this.GroupBy)
                {
                    // グループの設定を採用する。
                    for (int i = 0; i < this.GroupBy.Count; i++)
                    {
                        GridColumn gc = new GridColumn();
                        gc.DataFields.Add(new DataField(gc, this.GroupBy[i]));
                        lst.Add(gc);
                    }
                }
                else
                {
                    for (int i = 0; i < this.GridColumns.Count; i++)
                    {
                        if (0 == this.GridColumns[i].VisibleDataFields.Count) continue;
                        lst.Add(this.GridColumns[i]);
                    }
                }



                return lst;
            }

            private System.Collections.Generic.List<GridColumn> BindGridColumn
            {
                get;
                set;
            }


            public System.Collections.Generic.List<GridColumn> GridColumns
            {
                get;
                private set;
            }



            public DataField GetDataField(string strFieldName)
            {
                // 本来DataFieldはコレクションに入れて管理したいが、動的にGridColumnsに追加されるなど、
                // DataFieldの内容は変化するので、都度検索することにした
                for (int i = 0; i < this.GridColumns.Count; i++)
                {
                    for (int k = 0; k < this.GridColumns[i].DataFields.Count; k++)
                    {
                        if (strFieldName.Equals(this.GridColumns[i].DataFields[k].ColumnInfo.FieldName))
                            return this.GridColumns[i].DataFields[k];
                    }
                }
                return null;
            }


            public void RemoveDataField(string strFieldName)
            {
                // 本来DataFieldはコレクションに入れて管理したいが、動的にGridColumnsに追加されるなど、
                // DataFieldの内容は変化するので、都度検索することにした
                for (int i = 0; i < this.GridColumns.Count; i++)
                {
                    for (int k = 0; k < this.GridColumns[i].DataFields.Count; k++)
                    {
                        Core.Sql.ColumnInfo ci = this.GridColumns[i].DataFields[k].ColumnInfo;
                        if (null == ci) continue;
                        if (strFieldName.Equals(ci.FieldName))
                        {
                            this.GridColumns[i].DataFields.RemoveAt(k);
                            if (0 == this.GridColumns[i].DataFields.Count)
                                this.GridColumns.RemoveAt(i);
                            return;
                        }
                    }
                }
            }



            private Core.Sql.GroupCollection _GroupCollection = null;
            public Core.Sql.GroupCollection GroupBy
            {
                get
                {
                    return _GroupCollection;
                }
                set
                {
                    _GroupCollection = value;
                    if (null == value || 0 == value.Count)
                    {
                        _GroupCollection = null;
                    }
                }
            }


            public string SortExpression
            {
                get
                {
                    if (null != this.GroupBy)
                        return this._GroupCollection.GetSortExpression();
                    else
                    {
                        List<DataField> lstDf = GetSortDataFields();
                        if (0 == lstDf.Count) return "";
                        string[] str = new string[lstDf.Count];
                        for (int i = 0; i < lstDf.Count; i++)
                        {
                            str[i] = lstDf[i].ColumnInfo.FieldName + ((lstDf[i].SortOrder == System.Data.SqlClient.SortOrder.Ascending) ? " ASC" : " DESC");
                        }

                        return string.Join(",", str);
                    }
                }
                set
                {
                    this.SetSort(value);
                }
            }

            public List<DataField> GetSortDataFields()
            {
                List<DataField> lstDf = new List<DataField>();

                System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                System.Collections.Generic.SortedList<int, DataField> lst = new SortedList<int, DataField>();
                for (int i = 0; i < array.Count; i++)
                {
                    if (0 < array[i].SortNo && array[i].SortOrder != System.Data.SqlClient.SortOrder.Unspecified)
                        lst.Add(array[i].SortNo, array[i]);
                }

                if (0 == lst.Count) return lstDf;
                int[] nSortNo = new int[lst.Count];
                string[] str = new string[lst.Count];
                lst.Keys.CopyTo(nSortNo, 0);
                for (int i = 0; i < nSortNo.Length; i++)
                {
                    lstDf.Add(lst[nSortNo[i]]);
                }

                return lstDf;
            }


            public Core.Error SaveSort(UserViewClass.EnumType type, string strTypeMei)
            {
                return UserViewClass.SaveSort(this.ListID, this.UserID, type, strTypeMei, this.SortExpression, Global.GetConnection());
            }

            protected UserView()
            {
                System.IO.TextWriter tr = new System.IO.StringWriter(_sb);
                _writer = new System.Web.UI.HtmlTextWriter(tr);
            }

            public static UserView New(int nListID, string strUserID, bool bLoadFromCache)
            {
                Core.Sql.Dataset.T_UserListRow dr =
                    Core.Sql.SqlDataFactory.getT_UserListRow(nListID, Global.GetConnection());
                if (null == dr) return null;

                UserView obj = new UserView();
                obj.CachedSqlDataFactory = bLoadFromCache;
                obj.ListID = nListID;
                obj.UserID = strUserID;
                obj.SelectCommandText = dr.SelectCommand;
                obj.ErrorIfNotFoundColumnInDataSource = false;
                obj.InnerTableRenderByText = true;
                if (bLoadFromCache)
                {
                    // キャッシュ使用
                    string strKey = "SqlDataFactory_ListID=" + nListID.ToString();
                    if (null != System.Web.HttpContext.Current.Application[strKey])
                    {
                        // アプリケーションキャッシュに格納して本オブジェクトでは持たない 
                        // UserViewオブジェクトは、各ユーザーのセッションに可能される。
                        // SqlDataFactoryのオブジェクトは全ユーザーで共通なので、各ユーザー単位で持つとリソースの無駄になるのでこのような方法にした
                        obj.SqlDataFactory = System.Web.HttpContext.Current.Application[strKey] as Core.Sql.SqlDataFactory;
                    }
                    else
                    {
                        obj.SqlDataFactory = new Core.Sql.SqlDataFactory(nListID, Global.GetConnection());
                        System.Web.HttpContext.Current.Application[strKey] = obj.SqlDataFactory;
                    }
                }
                else
                    obj.SqlDataFactory = new Core.Sql.SqlDataFactory(nListID, Global.GetConnection());


                // 標準設定を読み込む
                Dataset.T_UserViewRow drDefault =
                    UserViewClass.getT_UserViewRow(nListID, "", UserViewClass.EnumType.VIEW, "", Global.GetConnection());


                Dataset.T_UserViewRow drT_UserViewRow = null;

                if ("" != strUserID)
                {
                    drT_UserViewRow = UserViewClass.getT_UserViewRow(nListID, strUserID, UserViewClass.EnumType.VIEW, "", Global.GetConnection());
                    if (null == dr)
                        drT_UserViewRow = drDefault;	// デフォルトの設定を使用する。
                }
                else
                    drT_UserViewRow = drDefault;

                if (null == drT_UserViewRow)
                {
                    drT_UserViewRow = new Dataset.T_UserViewDataTable().NewT_UserViewRow();
                    drT_UserViewRow.ListID = nListID;
                    drT_UserViewRow.Sort = "";
                    drT_UserViewRow.Columns = "";
                    drT_UserViewRow.UserID = strUserID;
                }
                if (null != drDefault)
                {
                    if ("" == drT_UserViewRow.Columns) drT_UserViewRow.Columns = drDefault.Columns;
                    if ("" == drT_UserViewRow.Sort) drT_UserViewRow.Sort = drDefault.Sort;
                }

                obj.LoadUserViewColumnData(drT_UserViewRow.Columns);

                obj.SortExpression = drT_UserViewRow.Sort;

                return obj;
            }

            public void LoadUserViewColumnData(string strUserViewData)
            {
                if (null == this.GridColumns)
                    this.GridColumns = new List<GridColumn>();

                this.GridColumns.Clear();

                // 表示するフィールド取得(もともと非表示の項目は無視する)
                System.Collections.Generic.Dictionary<string, Core.Sql.ColumnInfo> tblActiveColumns =
                    new System.Collections.Generic.Dictionary<string, Core.Sql.ColumnInfo>();
                for (int i = 0; i < this.SqlDataFactory.Columns.Count; i++)
                {
                    if (!this.SqlDataFactory.Columns[i].Hide)
                        tblActiveColumns.Add(this.SqlDataFactory.Columns[i].FieldName, this.SqlDataFactory.Columns[i]);
                }

                System.Collections.Generic.List<string> lstSelectedColName = new System.Collections.Generic.List<string>(); // 実際に表示されるフィールド名

                if (!string.IsNullOrEmpty(strUserViewData))
                {
                    System.Collections.Generic.List<string> lstSelectedColumnName = new System.Collections.Generic.List<string>(strUserViewData.Split('\t'));
                    for (int i = 0; i < lstSelectedColumnName.Count; i++)
                    {
                        string strColName = lstSelectedColumnName[i];
                        if ("" == strColName) continue;
                        string[] str = strColName.Split(',');
                        GridColumn gc = new GridColumn();
                        for (int c = 0; c < str.Length; c++)
                        {
                            string strName = str[c];
                            if (!tblActiveColumns.ContainsKey(strName)) continue;
                            DataField df = new DataField(gc, tblActiveColumns[strName]);
                            df.UserViewSettingVisible = true;
                            gc.DataFields.Add(df);
                            lstSelectedColName.Add(str[c]);
                        }
                        if (0 == gc.DataFields.Count) continue;
                        this.GridColumns.Add(gc);
                    }

                    // 非選択カラム
                    string[] strActiveColumnName = new string[tblActiveColumns.Count];
                    tblActiveColumns.Keys.CopyTo(strActiveColumnName, 0);
                    for (int i = 0; i < strActiveColumnName.Length; i++)
                    {
                        string strName = strActiveColumnName[i];
                        if (!lstSelectedColName.Contains(strName))
                        {
                            GridColumn gc = new GridColumn();
                            DataField df = new DataField(gc, tblActiveColumns[strName]);
                            gc.DataFields.Add(df);
                            if (tblActiveColumns[strName].ColumnType == Core.Sql.EnumColumnType.RadGridColumn)
                                df.UserViewSettingVisible = true;// ★★★★★★★★★★規定のカラムは必ず表示にする。(現状敢えて非表示にしているかどうか識別できない)
                            else
                                df.UserViewSettingVisible = false;
                            this.GridColumns.Add(gc);
                        }
                    }
                }
                else
                {
                    string[] strActiveColumnName = new string[tblActiveColumns.Count];
                    tblActiveColumns.Keys.CopyTo(strActiveColumnName, 0);
                    for (int i = 0; i < strActiveColumnName.Length; i++)
                    {
                        string strName = strActiveColumnName[i];
                        GridColumn gc = new GridColumn();
                        DataField df = new DataField(gc, tblActiveColumns[strName]);

                        gc.DataFields.Add(df);
                        this.GridColumns.Add(gc);
                    }
                }


                // DataFieldsに全て格納
                /* 廃止
                this.DataFields = new System.Collections.Generic.Dictionary<string, DataField>();
                for (int i = 0; i < GridColumns.Count; i++)
                {
                    System.Collections.Generic.List<DataField> a = this.GridColumns[i].VisibleDataFields;
                    for (int t = 0; t < a.Count; t++)
                    {
                        this.DataFields.Add(a[t].ColumnInfo.FieldName, a[t]);
                    }
                }
                */

            }


            public string CreateTextData(DataTable dt, Core.Data.DataTable2Text.EnumDataFormat f, Core.Data.DataTable2Text.RowBoundCallback callback)
            {
                Core.Data.DataTable2Text d = CreateDataTable2Text();
                DataView dv = new DataView(dt);
                dv.Sort = this.SortExpression;
                d.DataSource = dt;
                d.SortExpression = this.SortExpression;
                d.Format = f;
                d.OnRowBoundCallback = callback;
                return d.GetTextData();
            }



            public Core.Data.DataTable2Text CreateDataTable2Text()
            {
                Core.Data.DataTable2Text d = new Core.Data.DataTable2Text();

                if (null != this.GroupBy)
                {
                    for (int i = 0; i < this.GroupBy.Count; i++)
                    {
                        Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(GroupBy[i].GroupFieldName,
                            GroupBy[i].ColumnInfo.Caption,
                            GroupBy[i].ColumnInfo.Format, GroupBy[i].ColumnInfo.TrueString, GroupBy[i].ColumnInfo.FalseString);

                    }
                }
                else
                {
                    System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (array[i].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.DBField ||
                            array[i].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.UserField)
                        {
                            Core.Data.DataTable2Text.ColumnInfo c = d.Columns.Add(array[i].ColumnInfo.FieldName, array[i].ColumnInfo.Caption,
                                array[i].ColumnInfo.Format, array[i].ColumnInfo.TrueString, array[i].ColumnInfo.FalseString);
                        }
                    }
                }

                d.SortExpression = this.SortExpression;

                return d;

            }


            private static string GetCss(Core.Sql.ColumnInfo.EnumTextAlign TextAlign)
            {
                switch (TextAlign)
                {
                    case Core.Sql.ColumnInfo.EnumTextAlign.Left:
                        return "";
                    case Core.Sql.ColumnInfo.EnumTextAlign.Center:
                        return "tc";
                    case Core.Sql.ColumnInfo.EnumTextAlign.Right:
                        return "tr";
                }
                return "";
            }

            private string GetText(UserViewManager.DataField m, DataRow dr)
            {
                Core.Sql.ColumnInfo c = (null != m.GroupInfo) ? m.GroupInfo.ColumnInfo : m.ColumnInfo;
                string strFieldName = (null != m.GroupInfo) ? m.GroupInfo.GroupFieldName : m.ColumnInfo.FieldName;


                if (!dr.Table.Columns.Contains(strFieldName))
                {
                    if (ErrorIfNotFoundColumnInDataSource)
                        throw new Exception(string.Format("データソースにカラム{0}が見つかりません。", strFieldName));
                    else
                        return "";
                }

                if (dr.IsNull(strFieldName))
                {
                    return "";
                }
                else
                {
                    if (dr[strFieldName] is bool)
                    {
                        return (Convert.ToBoolean(dr[strFieldName])) ? c.TrueString : c.FalseString;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(c.Format))
                            return string.Format(c.Format, dr[strFieldName]);
                        else
                            return Convert.ToString(dr[strFieldName]);
                    }
                }
            }

            public string GetSortText()
            {
                if (string.IsNullOrEmpty(this.SortExpression)) return "";

                ArrayList lst = new ArrayList();
                System.Collections.Generic.List<string> lstText = new System.Collections.Generic.List<string>();

                string[] strSorts = this.SortExpression.Split(',');

                string strASC = "昇順";
                string strDESC = "降順";

                for (int i = 0; i < strSorts.Length; i++)
                {
                    strSorts[i] = strSorts[i].Trim();
                    string[] s = strSorts[i].Split(' ');

                    string strCol;
                    System.Data.SqlClient.SortOrder sort = System.Data.SqlClient.SortOrder.Ascending;

                    if (1 == s.Length)
                    {
                        strCol = s[0];
                        sort = System.Data.SqlClient.SortOrder.Ascending;
                    }
                    else
                    {
                        strCol = s[0];
                        if (s[1].ToUpper() == "ASC")
                            sort = System.Data.SqlClient.SortOrder.Ascending;
                        else if (s[1].ToUpper() == "DESC")
                            sort = System.Data.SqlClient.SortOrder.Descending;
                        else throw new Exception("pg error");
                    }

                    if (lst.Contains(strCol)) continue;

                    lst.Add(strCol);

                    DataField c = this.GetDataField(strCol);
                    string strCaption = (null == c) ? "???" : c.ColumnInfo.Caption;

                    lstText.Add(string.Format("{0} ({1})", strCaption,
                        (sort == System.Data.SqlClient.SortOrder.Ascending) ? strASC : strDESC));
                }

                return string.Join(" , ", lstText.ToArray());
            }


            private void SetSort(string strSort)
            {
                // 同じ項目が出現する可能性がある(order by hinban asc, binban, asc)ので、再設定する。
                System.Collections.Generic.List<UserViewManager.DataField> array = this.GetVisibleDataFields();
                for (int i = 0; i < array.Count; i++)
                {
                    array[i].SortOrder = System.Data.SqlClient.SortOrder.Unspecified;
                    array[i].SortNo = 0;
                }

                if (string.IsNullOrEmpty(strSort)) return;

                System.Collections.Generic.List<string> lstSortCol = new System.Collections.Generic.List<string>();
                string[] strSorts = strSort.Split(',');
                int nCount = 1;
                for (int i = 0; i < strSorts.Length; i++)
                {
                    strSorts[i] = strSorts[i].Trim();
                    string[] s = strSorts[i].Split(' ');

                    string strCol;
                    System.Data.SqlClient.SortOrder sort;

                    if (1 == s.Length)
                    {
                        strCol = s[0].Trim();
                        sort = System.Data.SqlClient.SortOrder.Ascending;
                    }
                    else
                    {
                        strCol = s[0].Trim();
                        if (s[1].ToUpper() == "ASC")
                            sort = System.Data.SqlClient.SortOrder.Ascending;
                        else if (s[1].ToUpper() == "DESC")
                            sort = System.Data.SqlClient.SortOrder.Descending;
                        else throw new Exception("pg error");
                    }

                    if (lstSortCol.Contains(strCol)) continue;

                    DataField df = this.GetDataField(strCol);
                    if (null == df) continue;

                    if (!df.Visible)
                    {
                        // このソート列が表示列でない場合はソートに加えない
                        continue;
                    }

                    df.SortOrder = sort;
                    df.SortNo = nCount++;

                    lstSortCol.Add(strCol);
                }

            }


            /// <summary>
            /// カラム内に複数行あるケースでHTMLで書き込むかどうか。
            /// </summary>
            public bool InnerTableRenderByText
            {
                get;
                set;
            }



            public void CreateRadGrid(Telerik.Web.UI.RadGrid dgd, int nColumnStartIndex, DataBoundEventHandler callback)
            {
                if (0 > nColumnStartIndex) nColumnStartIndex = 0;
                this._DataBoundEventHandler = callback;
                dgd.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(this.RadGrid_ItemDataBound);

                // カラムの最後から削除していく(デザイン時は既存列の最後に列が追加されている為)
                while (0 < dgd.MasterTableView.Columns.Count)
                {
                    string strUniqueName = dgd.MasterTableView.Columns[dgd.MasterTableView.Columns.Count - 1].UniqueName;
                    if (strUniqueName.StartsWith(UniqueNamePrefix))
                    {
                        // UniqueNameが数値で有れば動的に追加したカラムなので削除
                        dgd.MasterTableView.Columns.RemoveAt(dgd.MasterTableView.Columns.Count - 1);
                    }
                    else
                        break;
                }

                if (nColumnStartIndex > dgd.MasterTableView.Columns.Count)
                    nColumnStartIndex = dgd.MasterTableView.Columns.Count;

                System.Collections.ArrayList lstColumns = new ArrayList();  // 表示する列の一覧（表示順で挿入）

                this.BindGridColumn = this.GetBindGridColumns();
                List<string> lstFieldName = new List<string>();
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    for (int t = 0; t < this.BindGridColumn[i].DataFields.Count; t++)
                    {
                        DataField df = this.BindGridColumn[i].DataFields[t];
                        if (null != df.GroupInfo)
                            lstFieldName.Add(df.GroupInfo.GroupFieldName);
                        else
                            lstFieldName.Add(df.ColumnInfo.FieldName);
                    }
                }


                // 開始列までを挿入
                for (int i = 0; i < nColumnStartIndex; i++)
                {
                    if (!lstFieldName.Contains(dgd.MasterTableView.Columns[i].UniqueName))
                        lstColumns.Add(dgd.MasterTableView.Columns[i]);
                }


                // 列定義順に挿入
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];

                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn)
                    {
                        // 列タイプがRadGridColumnの場合、m.FieldNameにはUniqueNameがセットされている。
                        DataField df = gc.DataFields[0];
                        gc.WebUI_GridColumn = dgd.MasterTableView.Columns.FindByUniqueNameSafe(df.ColumnInfo.FieldName); // これだけは設定できる。 
                        if (null == gc.WebUI_GridColumn)
                        {
                            throw new Exception(string.Format("UniqueName={0}の列がありません。", df.ColumnInfo.FieldName));
                        }
                        gc.WebUI_GridColumn.HeaderText = df.ColumnInfo.Caption;   // 規定列はヘッダのテキストをココで設定する(これは後の処理では変更できない)

                        lstColumns.Add(gc.WebUI_GridColumn);
                    }
                    else
                    {
                        Telerik.Web.UI.GridBoundColumn bc = new Telerik.Web.UI.GridBoundColumn();
                        bc.ItemStyle.Wrap = false;
                        bc.HeaderStyle.Wrap = false;
                        // 2013/07/08 Headerの表示位置は「中央揃え」が通常である為変更
                        //bc.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                        bc.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        bc.HeaderStyle.CssClass = this.HeaderCss;
                        bc.FooterStyle.Wrap = false;

                        bc.UniqueName = gc.WebUI_GridColumnUniqueName;

#if DEBUG
                        string str = "";
                        if (1 == gc.DataFields.Count)
                        {
                            str = gc.DataFields[0].ColumnInfo.Caption;
                        }
                        else
                        {
                            for (int r = 0; r < gc.DataFields.Count; r++)
                            {
                                str += gc.DataFields[r].ColumnInfo.Caption + "/";
                            }
                        }

                        bc.HeaderText = str;
#endif

                        //dgd.MasterTableView.Columns.Insert(this._nColumnStartIndex + i, bc);	インサートするとデザイン時にあったコントロールがViewStateに登録されず、FindControl()出来ない
                        dgd.MasterTableView.Columns.Add(bc);    // 必ず最後に追加
                        int nInsertIndex = nColumnStartIndex + i;
                        gc.WebUI_GridColumn = bc;
                        lstColumns.Add(bc);
                    }
                }
#if DEBUG
                for (int y = 0; y < lstColumns.Count; y++)
                {
                    string str = "";
                    Telerik.Web.UI.GridColumn c = lstColumns[y] as Telerik.Web.UI.GridColumn;
                    if (c is Telerik.Web.UI.GridBoundColumn)
                    {
                        Telerik.Web.UI.GridBoundColumn d = c as Telerik.Web.UI.GridBoundColumn;
                        str = d.HeaderText;

                    }
                    else
                    {
                        str = c.UniqueName;
                    }
                }
#endif

                // 追加されていない列を最後に追加
                for (int i = 0; i < dgd.MasterTableView.Columns.Count; i++)
                {
                    if (!lstColumns.Contains(dgd.MasterTableView.Columns[i]))
                        lstColumns.Add(dgd.MasterTableView.Columns[i]);
                }

                // OrderIndexの再設定
                for (int i = 0; i < lstColumns.Count; i++)
                {
                    Telerik.Web.UI.GridColumn c = lstColumns[i] as Telerik.Web.UI.GridColumn;
                    c.OrderIndex = i + 2; // 実際のセル列のindexと一致させる為2を加算
                }
            }



            private System.Web.UI.WebControls.Table CreateInnerTable(int nRows)
            {
                Table tbl = new Table();

                tbl.BorderWidth = Unit.Pixel(0);
                tbl.CellPadding = 2;
                tbl.Width = Unit.Percentage(100);
                for (int i = 0; i < nRows; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Wrap = false;
                    r.Cells.Add(c);

                    if (i != nRows - 1)
                        c.Style["border-bottom"] = "solid 1px black";

                    c.Text = "&nbsp;";
                    tbl.Rows.Add(r);
                }

                return tbl;
            }


            private void AddCss(TableCell cell, string strCss)
            {
                if (string.IsNullOrEmpty(cell.CssClass))
                    cell.CssClass = strCss;
                else
                    cell.CssClass += " " + strCss;
            }


            private void RadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
            {
                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:
                    case Telerik.Web.UI.GridItemType.Footer:
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        break;
                    default:
                        return;
                }

                Telerik.Web.UI.RadGrid dgd = sender as Telerik.Web.UI.RadGrid;
                UserViewEventArgs arg = new UserViewEventArgs();

                if (dgd.DataSource is DataView)
                    arg.DataView = dgd.DataSource as DataView;
                else if (dgd.DataSource is DataTable)
                    arg.DataView = (dgd.DataSource as DataTable).DefaultView;


                arg.UserView = this;
                arg.GridItem = e.Item;

                DataRow dr = null;
                System.Collections.Generic.Dictionary<int, Table> InnerTables =
                    new System.Collections.Generic.Dictionary<int, Table>(); // 結合列の内部テーブル

                // ----- 予め表示対象のセルを設定しておく-----
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];

                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ

                    Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;

                    int nColIndex = col.OrderIndex;

                    if (1 == gc.DataFields.Count)
                    {
                        this.AddCss(e.Item.Cells[nColIndex], UserView.GetCss(gc.DataFields[0].ColumnInfo.TextAlign));
                        if (null != gc.DataFields[0].GroupInfo)
                            arg.TableCells.Add(gc.DataFields[0].GroupInfo.GroupFieldName, e.Item.Cells[nColIndex]);
                        else
                            arg.TableCells.Add(gc.DataFields[0].ColumnInfo.FieldName, e.Item.Cells[nColIndex]);
                    }
                    else
                    {
                        // 結合列の場合
                        Table t = CreateInnerTable(gc.DataFields.Count);
                        t.CssClass = "def";
                        for (int c = 0; c < gc.DataFields.Count; c++)
                        {
                            UserViewManager.DataField df = gc.DataFields[c];

                            TableCell cell = t.Rows[c].Cells[0];
                            this.AddCss(cell, UserView.GetCss(df.ColumnInfo.TextAlign));

                            if (null != gc.DataFields[0].GroupInfo)
                                arg.TableCells.Add(df.GroupInfo.GroupFieldName, cell);
                            else
                                arg.TableCells.Add(df.ColumnInfo.FieldName, cell);
                        }
                        e.Item.Cells[nColIndex].CssClass = "fit";
                        e.Item.Cells[nColIndex].Text = "";
                        e.Item.Cells[nColIndex].Controls.Add(t);
                        InnerTables.Add(i, t);

                    }
                }

                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:

                        arg.ItemType = EnumItemType.Header;
                        e.Item.CssClass = this.HeaderCss;

                        for (int i = 0; i < this.BindGridColumn.Count; i++)
                        {
                            UserViewManager.GridColumn gc = this.BindGridColumn[i];

                            if (1 == gc.DataFields.Count)
                            {
                                DataField df = gc.DataFields[0];
                                string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;

                                TableCell cell = arg.TableCells[strFieldName];

                                AddCss(cell, gc.HeaderCSS);

                                string strCaption = df.ColumnInfo.Caption;
                                if (null != df.GroupInfo) strCaption += string.Format("({0})", GetGroupText(df.GroupInfo.GroupType));

                                switch (df.SortOrder)
                                {
                                    case System.Data.SqlClient.SortOrder.Ascending:
                                        strCaption += this.SortText_ASC;
                                        break;
                                    case System.Data.SqlClient.SortOrder.Descending:
                                        strCaption += this.SortText_DESC;
                                        break;
                                }

                                cell.Text = strCaption;
                            }
                            else
                            {
                                // 結合列の場合
                                for (int c = 0; c < gc.DataFields.Count; c++)
                                {
                                    UserViewManager.DataField df = gc.DataFields[c];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                    TableCell cell = arg.TableCells[strFieldName];
                                    this.AddCss(cell, this.HeaderCss);
                                    this.AddCss(cell, gc.HeaderCSS);

                                    string strCaption = df.ColumnInfo.Caption;
                                    if (null != df.GroupInfo)
                                        strCaption += string.Format("({0})", GetGroupText(df.GroupInfo.GroupType));

                                    switch (df.SortOrder)
                                    {
                                        case System.Data.SqlClient.SortOrder.Ascending:
                                            strCaption += this.SortText_ASC;
                                            break;
                                        case System.Data.SqlClient.SortOrder.Descending:
                                            strCaption += this.SortText_DESC;
                                            break;
                                    }

                                    cell.Text = strCaption;
                                }
                            }
                        }

                        break;
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        dr = (e.Item.DataItem as DataRowView).Row;

                        arg.DataRow = dr;
                        arg.DataRowIndex = e.Item.ItemIndex;
                        arg.ItemType = EnumItemType.DataRow;
                        for (int i = 0; i < this.BindGridColumn.Count; i++)
                        {
                            UserViewManager.GridColumn gc = this.BindGridColumn[i];

                            if (1 < gc.DataFields.Count)
                            {
                                // 結合列の場合
                                for (int c = 0; c < gc.DataFields.Count; c++)
                                {
                                    UserViewManager.DataField df = gc.DataFields[c];
                                    string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                    TableCell cell = arg.TableCells[strFieldName];
                                    cell.Text = GetText(df, dr);
                                    if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                                }
                            }
                            else
                            {
                                DataField df = gc.DataFields[0];
                                string strFieldName = (null != df.GroupInfo) ? df.GroupInfo.GroupFieldName : df.ColumnInfo.FieldName;
                                TableCell cell = arg.TableCells[strFieldName];
                                cell.Text = GetText(df, dr);
                                if ("" == cell.Text.Trim()) cell.Text = "&nbsp;";
                            }
                        }
                        break;
                    case Telerik.Web.UI.GridItemType.Footer:
                        arg.ItemType = EnumItemType.Footer;

                        break;

                }

                // コールバック
                if (null != this._DataBoundEventHandler)
                    this._DataBoundEventHandler(arg);


                // 結合列をViewStateに保存する為、動的コントロール(Table)をHTML化して書き込む
                for (int i = 0; i < this.BindGridColumn.Count; i++)
                {
                    UserViewManager.GridColumn gc = this.BindGridColumn[i];
                    if (1 == gc.DataFields.Count && gc.DataFields[0].ColumnInfo.ColumnType == Core.Sql.EnumColumnType.RadGridColumn) continue;    // ★★★★★既存列の場合はスキップ
                    Telerik.Web.UI.GridColumn col = gc.WebUI_GridColumn;
                    if (1 < gc.DataFields.Count)
                    {
                        if (InnerTableRenderByText)
                        {
                            _sb.Length = 0;
                            Table t = InnerTables[i];

                            t.RenderControl(_writer);
                            _writer.Flush();
                            e.Item.Cells[col.OrderIndex].Text = _sb.ToString();  // この処理で内部テーブルがViewstateに保存される。
                        }
                    }
                    if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Header)
                    {
                        // ★これが無いとAjaxで更新時にヘッダー文字が消える
                        col.HeaderText = e.Item.Cells[col.OrderIndex].Text;
                    }
                    else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Footer)
                    {
                        col.FooterText = e.Item.Cells[col.OrderIndex].Text;
                    }
                }




            }
        }

        public class GridColumn
        {
            public string HeaderCSS
            {
                get;
                set;
            }

            public Telerik.Web.UI.GridColumn WebUI_GridColumn
            {
                get;
                set;
            }

            /// <summary>
            /// RadGrid生成前にUniqueNameを取得したいケースが有る為
            /// </summary>
            public string WebUI_GridColumnUniqueName
            {
                get
                {
                    if (null != WebUI_GridColumn) return WebUI_GridColumn.UniqueName;
                    if (0 == DataFields.Count) return null;
                    string[] str = new string[DataFields.Count];
                    for (int i = 0; i < DataFields.Count; i++)
                    {
                        str[i] = DataFields[i].ColumnInfo.FieldName;
                    }
                    return UniqueNamePrefix + string.Join("/", str);
                }
            }

            public System.Collections.Generic.List<DataField> DataFields
            {
                get;
                set;
            }


            public System.Collections.Generic.List<DataField> VisibleDataFields
            {
                get
                {
                    System.Collections.Generic.List<DataField> lst = new System.Collections.Generic.List<DataField>();
                    for (int i = 0; i < DataFields.Count; i++)
                    {
                        if (DataFields[i].ForceVisible)
                            lst.Add(DataFields[i]);
                        else
                        {
                            if (DataFields[i].UserViewSettingVisible && DataFields[i].Visible)
                                lst.Add(DataFields[i]);
                        }
                    }
                    return lst;
                }
            }

            public GridColumn()
            {
                DataFields = new List<DataField>();
            }
        }

        public class DataField
        {
            public GridColumn GridColumn
            {
                get;
                private set;
            }

            public System.Data.SqlClient.SortOrder SortOrder
            {
                get;
                set;
            }

            public int SortNo
            {
                get;
                set;
            }

            public string SortExpression
            {
                get
                {
                    switch (this.SortOrder)
                    {
                        case System.Data.SqlClient.SortOrder.Ascending:
                            return this.ColumnInfo.FieldName + " ASC";
                        case System.Data.SqlClient.SortOrder.Descending:
                            return this.ColumnInfo.FieldName + " DESC";
                    }
                    return "";
                }
            }


            public EditDataField EditDataField
            {
                get
                {
                    EditDataField e = new EditDataField();
                    e.Caption = this.ColumnInfo.Caption;
                    e.ColumnType = this.ColumnInfo.ColumnType;
                    e.FieldName = this.ColumnInfo.FieldName;
                    e.Visible = this.Visible;
                    e.ForceVisible = this.ForceVisible;
                    return e;
                }
            }


            private Core.Sql.ColumnInfo _ColumnInfo = null;

            /// <summary>
            /// グループ列の場合は、グループのColumnInfoを返す.
            /// </summary>
            public Core.Sql.ColumnInfo ColumnInfo
            {
                get
                {
                    if (null != GroupInfo) return GroupInfo.ColumnInfo;
                    return _ColumnInfo;
                }
                set
                {
                    _ColumnInfo = value;
                }
            }

            public Core.Sql.GroupInfo GroupInfo
            {
                get;
                private set;
            }


            private bool _bVisible = true;
            public bool Visible
            {
                get
                {
                    if (ForceVisible) return true;
                    if (!UserViewSettingVisible) return false;
                    return _bVisible;
                }
                set
                {
                    _bVisible = value;
                }
            }

            /// <summary>
            /// UserViewの設定で表示対象かどうか
            /// </summary>
            public bool UserViewSettingVisible
            {
                get;
                set;
            }

            /// <summary>
            /// 強制的に表示かどうか
            /// </summary>
            public bool ForceVisible
            {
                get;
                set;
            }

            public DataField(GridColumn gc, Core.Sql.ColumnInfo c)
            {
                this.GridColumn = gc;
                this.ColumnInfo = c;
                this.Visible = true;
                this.UserViewSettingVisible = true;
                this.SortOrder = System.Data.SqlClient.SortOrder.Unspecified;
                this.ForceVisible = false;
            }

            public DataField(GridColumn gc, Core.Sql.GroupInfo g)
            {
                this.GridColumn = gc;
                this.GroupInfo = g;
                this.Visible = true;
                this.UserViewSettingVisible = true;
                this.SortOrder = System.Data.SqlClient.SortOrder.Unspecified;
                this.ForceVisible = false;
            }

        }

        /*
        public class UserViewEventArgs : System.EventArgs
        {
            public class DisplayTableCells : System.Collections.Generic.Dictionary<string, TableCell>
            {
                new public TableCell this[string strFieldName]
                {
                    get
                    {
                        if (base.ContainsKey(strFieldName))
                            return base[strFieldName];
                        else
                            return new TableCell();         // インデクサのアクセスで対象フィールドが無いことによるエラーが発生しないようにダミーを返す
                    }
                }
            }

            private DisplayTableCells _tblTableCell = new DisplayTableCells();

            public DisplayTableCells TableCells
            {
                get
                {
                    return _tblTableCell;
                }
            }

            public UserView UserView
            {
                get;
                set;
            }
            public int DataRowIndex
            {
                get;
                internal set;
            }
            public EnumItemType ItemType
            {
                get;
                set;
            }
            public DataRow DataRow
            {
                get;
                set;
            }

            public DataView DataView
            {
                get;
                set;
            }
        }

        public class UserView
        {
            public int ListID
            {
                get;
                internal set;
            }
            public string UserID
            {
                get;
                internal set;
            }
            public string SelectCommandText
            {
                get;
                set;
            }
            private MyColumnCollection _display = null;
            private MyColumnCollection _not_display = null;


            public delegate void DataBoundEventHandler(UserViewEventArgs e);


            private event DataBoundEventHandler _DataBoundEventHandler = null;

            public string SortText_ASC
            {
                get;
                set;
            }
            public string SortText_DESC
            {
                get;
                set;
            }

            private int _nColumnStartIndex = 0;

            private System.Collections.Generic.Dictionary<string, System.Data.SqlClient.SortOrder> _tblSort =
                new System.Collections.Generic.Dictionary<string, System.Data.SqlClient.SortOrder>();

            public string HeaderCss
            {
                get;
                set;
            }

            /// <summary>
            /// データソース（データテーブル）に表示カラムが無ければエラーとするかどうか
            /// </summary>
            public bool ErrorIfNotFoundColumnInDataSource
            {
                get;
                set;
            }

            public MyColumnCollection DisplayMyColumns
            {
                get
                {
                    return _display;
                }
            }


            public MyColumnCollection NotDisplayMyColumns
            {
                get
                {
                    return _not_display;
                }
            }

            private string _strSortExpression = "";
            public string SortExpression
            {
                get
                {
                    return _strSortExpression;
                }
                set
                {
                    _strSortExpression = value;
                    this.SetSort(value);
                }
            }

            public Core.Error SaveSort()
            {
                return UserViewClass.SaveSort(this.ListID, this.UserID, this.SortExpression, Global.GetConnection());
            }

            public static UserView New(int nListID, string strUserID)
            {
                Core.Sql.Dataset.T_UserListRow dr =
                    Core.Sql.SqlDataFactory.getT_UserListRow(nListID, Global.GetConnection());
                if (null == dr) return null;



                UserView obj = new UserView();
                obj.ListID = nListID;
                obj.UserID = strUserID;
                obj.SelectCommandText = dr.SelectCommand;
                obj.ErrorIfNotFoundColumnInDataSource = false;


                string strSort = "";

                UserViewManager.GetColumnsSetting(nListID, strUserID, out obj._display, out obj._not_display, ref strSort);

                obj.SortExpression = strSort;

                return obj;
            }

            public string CreateTextData(DataTable dt, Core.Data.DataTable2Text.EnumDataFormat f)
            {
                Core.Data.DataTable2Text d = CreateDataTable2Text();
                DataView dv = new DataView(dt);
                dv.Sort = SortExpression;

                d.DataSource = dt;
                d.SortExpression = this.SortExpression;
                d.Format = f;

                return d.GetTextData();
            }


            public Core.Data.DataTable2Text CreateDataTable2Text()
            {
                Core.Data.DataTable2Text d = new Core.Data.DataTable2Text();
                MyColumn[] array = this._display.All();

                for (int i = 0; i < array.Length; i++)
                {
                    d.AddColumnInfo(array[i].strFieldName, array[i].strCaption, array[i].dr.Format, array[i].dr.TrueString, array[i].dr.FalseString);
                }
                return d;
            }



            public void CreateDataGrid(DataGrid dgd,
                int nColumnStartIndex, bool bCreateColumn, DataTable dtSrc, DataBoundEventHandler callback)
            {
                this._nColumnStartIndex = nColumnStartIndex;
                this._DataBoundEventHandler = callback;

                dgd.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGird_ItemDataBound);

                if (bCreateColumn)
                {
                    for (int i = 0; i < this._display.Count; i++)
                    {
                        BoundColumn bc = new BoundColumn();
                        bc.HeaderStyle.Wrap = false;
                        bc.ItemStyle.Wrap = false;
                        bc.HeaderStyle.CssClass = this.HeaderCss;
                        dgd.Columns.AddAt(nColumnStartIndex, bc);
                    }
                }

                DataView dv = new DataView(dtSrc);
                dv.Sort = this.SortExpression;
                dgd.DataSource = dv;
                dgd.DataBind();

            }

            private static string GetCss(byte bTextAlign)
            {
                switch (bTextAlign)
                {
                    case 0:
                        return "";
                    case 1:	// 中央
                        return "tc";
                    case 2: //右寄せ
                        return "tr";
                }
                return "";
            }

            private string GetText(UserViewManager.MyColumn m, DataRow dr)
            {
                if (!dr.Table.Columns.Contains(m.strFieldName))
                {
                    if (ErrorIfNotFoundColumnInDataSource)
                        throw new Exception(string.Format("データソースにカラム{0}が見つかりません。", m.strFieldName));
                    else
                        return "";
                }

                if (dr.IsNull(m.strFieldName))
                {
                    return "";
                }
                else
                {
                    if (dr[m.strFieldName].GetType() == typeof(bool))
                    {
                        return (Convert.ToBoolean(dr[m.strFieldName])) ? m.dr.TrueString : m.dr.FalseString;
                    }
                    else
                    {
                        if ("" != m.dr.Format)
                            return string.Format(m.dr.Format, dr[m.strFieldName]);
                        else
                            return Convert.ToString(dr[m.strFieldName]);
                    }
                }
            }


            public string GetSortText()
            {
                if (string.IsNullOrEmpty(this.SortExpression)) return "";

                ArrayList lst = new ArrayList();
                System.Collections.Generic.List<string> lstText = new System.Collections.Generic.List<string>();

                string[] strSorts = this.SortExpression.Split(',');

                string strASC = "昇順";
                string strDESC = "降順";

                for (int i = 0; i < strSorts.Length; i++)
                {
                    strSorts[i] = strSorts[i].Trim();
                    string[] s = strSorts[i].Split(' ');

                    string strCol;
                    System.Data.SqlClient.SortOrder sort = System.Data.SqlClient.SortOrder.Ascending;

                    if (1 == s.Length)
                    {
                        strCol = s[0];
                        sort = System.Data.SqlClient.SortOrder.Ascending;
                    }
                    else
                    {
                        strCol = s[0];
                        if (s[1].ToUpper() == "ASC")
                            sort = System.Data.SqlClient.SortOrder.Ascending;
                        else if (s[1].ToUpper() == "DESC")
                            sort = System.Data.SqlClient.SortOrder.Descending;
                        else throw new Exception("pg error");
                    }

                    if (lst.Contains(strCol)) continue;

                    lst.Add(strCol);

                    MyColumn c = this.DisplayMyColumns.FindByColumnName(strCol);
                    string strCaption = (null == c) ? "???" : c.strCaption;

                    lstText.Add(string.Format("{0} ({1})", strCaption,
                        (sort == System.Data.SqlClient.SortOrder.Ascending) ? strASC : strDESC));
                }

                return string.Join(" , ", lstText.ToArray());
            }


            private void DataGird_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
            {
                switch (e.Item.ItemType)
                {
                    case ListItemType.Header:
                    case ListItemType.Footer:
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                    case ListItemType.SelectedItem:
                        break;
                    default:
                        return;
                }


                UserViewEventArgs arg = new UserViewEventArgs();
                arg.UserView = this;
                DataRow dr = null;

                for (int i = 0; i < this._display.Count; i++)
                {
                    UserViewManager.MyColumn m = this._display[i];

                    int nColIndex = this._nColumnStartIndex + i;

                    // 予め表示対象のセルを決定しておく
                    if (0 == m.InnerColumns.Count)
                    {
                        this.AddCss(e.Item.Cells[nColIndex], UserView.GetCss(m.dr.TextAlign));
                        arg.TableCells.Add(m.strFieldName, e.Item.Cells[nColIndex]);
                    }
                    else
                    {
                        // 結合列の場合
                        Table t = CreateInnerTable(m.InnerColumns.Count);
                        t.CssClass = "def";
                        for (int c = 0; c < m.InnerColumns.Count; c++)
                        {
                            UserViewManager.MyColumn mc = m.InnerColumns[c];

                            TableCell cell = t.Rows[c].Cells[0];
                            this.AddCss(cell, UserView.GetCss(mc.dr.TextAlign));
                            arg.TableCells.Add(mc.strFieldName, cell);
                        }
                        e.Item.Cells[nColIndex].CssClass = "fit";
                        e.Item.Cells[nColIndex].Text = "";
                        e.Item.Cells[nColIndex].Controls.Add(t);
                    }
                }



                switch (e.Item.ItemType)
                {
                    case ListItemType.Header:
                        arg.ItemType = EnumItemType.Header;
                        e.Item.CssClass = this.HeaderCss;
                        for (int i = 0; i < this._display.Count; i++)
                        {
                            UserViewManager.MyColumn m = this._display[i];

                            if (0 == m.InnerColumns.Count)
                            {
                                TableCell cell = arg.TableCells[m.strFieldName];

                                AddCss(cell, m.HeaderCSS);

                                if (_tblSort.ContainsKey(m.strFieldName))
                                {
                                    // ソートされている項目
                                    string str = "";
                                    if (_tblSort[m.strFieldName] == System.Data.SqlClient.SortOrder.Ascending)
                                        str = this.SortText_ASC;
                                    else
                                        str = this.SortText_DESC;
                                    cell.Text = m.strCaption + str;
                                }
                                else
                                    cell.Text = m.strCaption;
                            }
                            else
                            {
                                // 結合列の場合
                                for (int c = 0; c < m.InnerColumns.Count; c++)
                                {
                                    UserViewManager.MyColumn mc = m.InnerColumns[c];

                                    TableCell cell = arg.TableCells[mc.strFieldName];
                                    if (!string.IsNullOrEmpty(m.HeaderCSS))
                                    {
                                        if (!string.IsNullOrEmpty(cell.CssClass))
                                            cell.CssClass += " ";
                                        cell.CssClass += m.HeaderCSS;
                                    }

                                    if (_tblSort.ContainsKey(mc.strFieldName))
                                    {
                                        // ソートされている項目
                                        string str = "";
                                        if (this._tblSort[mc.strFieldName] == System.Data.SqlClient.SortOrder.Ascending)
                                            str = this.SortText_ASC;
                                        else
                                            str = this.SortText_DESC;
                                        cell.Text = mc.strCaption + str;
                                    }
                                    else
                                        cell.Text = mc.strCaption;
                                }
                            }
                        }
                        break;
                    case ListItemType.Item:
                    case ListItemType.AlternatingItem:
                    case ListItemType.SelectedItem:
                        dr = (e.Item.DataItem as DataRowView).Row;
                        arg.DataRow = dr;
                        arg.DataRowIndex = e.Item.ItemIndex;
                        arg.ItemType = EnumItemType.DataRow;
                        for (int i = 0; i < this._display.Count; i++)
                        {
                            UserViewManager.MyColumn m = this._display[i];
                            if (0 < m.InnerColumns.Count)
                            {
                                // 結合列の場合
                                for (int c = 0; c < m.InnerColumns.Count; c++)
                                {
                                    UserViewManager.MyColumn mc = m.InnerColumns[c];
                                    TableCell cell = arg.TableCells[mc.strFieldName];
                                    cell.Text = GetText(mc, dr);
                                    if ("" == cell.Text) cell.Text = "&nbsp;";
                                }
                            }
                            else
                            {
                                TableCell cell = arg.TableCells[m.strFieldName];
                                cell.Text = GetText(m, dr);
                                if ("" == cell.Text) cell.Text = "&nbsp;";
                            }
                        }
                        break;
                    case ListItemType.Footer:
                        arg.ItemType = EnumItemType.Footer;

                        break;

                }

                // コールバック
                if (null != this._DataBoundEventHandler)
                    this._DataBoundEventHandler(arg);
            }

            private void SetSort(string strSort)
            {
                // 同じ項目が出現する可能性がある(order by hinban asc, binban, asc)ので、再設定する。
                _tblSort.Clear();
                System.Collections.Generic.List<string> lstSortItem = new System.Collections.Generic.List<string>();
                string[] strSorts = strSort.Split(',');
                for (int i = 0; i < strSorts.Length; i++)
                {
                    strSorts[i] = strSorts[i].Trim();
                    string[] s = strSorts[i].Split(' ');

                    string strCol;
                    System.Data.SqlClient.SortOrder sort;

                    if (1 == s.Length)
                    {
                        strCol = s[0].Trim();
                        sort = System.Data.SqlClient.SortOrder.Ascending;
                    }
                    else
                    {
                        strCol = s[0].Trim();
                        if (s[1].ToUpper() == "ASC")
                            sort = System.Data.SqlClient.SortOrder.Ascending;
                        else if (s[1].ToUpper() == "DESC")
                            sort = System.Data.SqlClient.SortOrder.Descending;
                        else throw new Exception("pg error");
                    }

                    if (_tblSort.ContainsKey(strCol)) continue;

                    if (null == this.DisplayMyColumns.FindByColumnName(strCol))
                    {
                        // このソート列が表示列でない場合はソートに加えない
                        continue;
                    }

                    lstSortItem.Add(strSorts[i]);

                    _tblSort.Add(strCol, sort);
                }

                // 再度ソート文字列を設定し直す。
                this._strSortExpression = string.Join(",", lstSortItem.ToArray());
            }

            public void CreateRadGrid_Old(Telerik.Web.UI.RadGrid dgd, string strSort, int nColumnStartIndex, DataBoundEventHandler callback)
            {
                this._nColumnStartIndex = nColumnStartIndex;
                this._DataBoundEventHandler = callback;


                // 各列のUniqueNameに_displayのindexを保持させる。

                System.Collections.ArrayList lstRemove = new ArrayList();
                for (int i = 0; i < dgd.MasterTableView.Columns.Count; i++)
                {
                    Telerik.Web.UI.GridColumn gc = dgd.MasterTableView.Columns[i];
                    try
                    {
                        int n = int.Parse(gc.UniqueName);
                        if (!dgd.Page.IsPostBack)
                        {
                            throw new Exception("UniqueName" + i.ToString() + "が既に使用されています。");
                        }
                        lstRemove.Add(gc);
                    }
                    catch
                    {

                    }
                }
                for (int i = 0; i < lstRemove.Count; i++)
                {
                    dgd.MasterTableView.Columns.Remove(lstRemove[i] as Telerik.Web.UI.GridColumn);
                }


                for (int i = 0; i < this._display.Count; i++)
                {
                    UserViewManager.MyColumn m = this._display[i];

                    Telerik.Web.UI.GridBoundColumn bc = new Telerik.Web.UI.GridBoundColumn();
                    bc.ItemStyle.Wrap = false;
                    bc.HeaderStyle.CssClass = this.HeaderCss;
                    bc.UniqueName = i.ToString();
                    m.WebUI_GridColumn = bc;
                    dgd.MasterTableView.Columns.Insert(this._nColumnStartIndex + i, bc);	// 追加
                }


                dgd.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(this.RadGrid_ItemDataBound);

                // ソート
                SetSort(strSort);
            }


            public void CreateRadGrid(Telerik.Web.UI.RadGrid dgd, int nColumnStartIndex, DataBoundEventHandler callback)
            {
                this._nColumnStartIndex = nColumnStartIndex;
                this._DataBoundEventHandler = callback;


                // カラムの最後から削除していく
                while (0 < dgd.MasterTableView.Columns.Count)
                {
                    string strUniqueName = dgd.MasterTableView.Columns[dgd.MasterTableView.Columns.Count - 1].UniqueName;
                    try
                    {
                        int nNo = int.Parse(strUniqueName);
                        // UniqueNameが数値で有れば動的に追加したカラムなので削除
                        dgd.MasterTableView.Columns.RemoveAt(dgd.MasterTableView.Columns.Count - 1);
                    }
                    catch
                    {
                        break;
                    }
                }

                int nDefaultColumnCount = dgd.MasterTableView.Columns.Count;


                for (int i = 0; i < this._display.Count; i++)
                {
                    UserViewManager.MyColumn m = this._display[i];

                    Telerik.Web.UI.GridBoundColumn bc = new Telerik.Web.UI.GridBoundColumn();
                    bc.ItemStyle.Wrap = false;
                    bc.HeaderStyle.Wrap = false;
                    bc.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    bc.HeaderStyle.CssClass = this.HeaderCss;
                    bc.UniqueName = i.ToString();
                    m.WebUI_GridColumn = bc;
                    bc.OrderIndex = nColumnStartIndex + i;
                    //dgd.MasterTableView.Columns.Insert(this._nColumnStartIndex + i, bc);	インサートするとデザイン時にあったコントロールがViewStateに登録されず、FindControl()出来ない

                    dgd.MasterTableView.Columns.Add(bc);    // 必ず最後に追加
                }

                // デザイン時から元々あったOrderIndexの設定を行う。

                for (int i = 0; i < nColumnStartIndex; i++)
                {
                    dgd.MasterTableView.Columns[i].OrderIndex = i;
                }
                int nCount = 0;
                for (int i = nColumnStartIndex; i < nDefaultColumnCount; i++)
                {
                    dgd.MasterTableView.Columns[i].OrderIndex = nColumnStartIndex + this._display.Count + nCount++;
                }

                for (int i = 0; i < dgd.MasterTableView.Columns.Count; i++)
                {
                    dgd.MasterTableView.Columns[i].OrderIndex += 2; // 実際のセル列のindexと一致させる為
                }

                dgd.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(this.RadGrid_ItemDataBound);

            }

            private System.Web.UI.WebControls.Table CreateInnerTable(int nRows)
            {
                Table tbl = new Table();

                tbl.BorderWidth = Unit.Pixel(0);
                tbl.CellPadding = 2;
                tbl.Width = Unit.Percentage(100);
                for (int i = 0; i < nRows; i++)
                {
                    TableRow r = new TableRow();
                    TableCell c = new TableCell();
                    c.Wrap = false;
                    r.Cells.Add(c);

                    if (i != nRows - 1)
                        c.Style["border-bottom"] = "solid 1px black";

                    c.Text = "&nbsp;";
                    tbl.Rows.Add(r);
                }

                return tbl;
            }


            private void AddCss(TableCell cell, string strCss)
            {
                if (string.IsNullOrEmpty(cell.CssClass))
                    cell.CssClass = strCss;
                else
                    cell.CssClass += " " + strCss;
            }

            private void RadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
            {
                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:
                    case Telerik.Web.UI.GridItemType.Footer:
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        break;
                    default:
                        return;
                }

                Telerik.Web.UI.RadGrid dgd = sender as Telerik.Web.UI.RadGrid;
                UserViewEventArgs arg = new UserViewEventArgs();

                if (dgd.DataSource is DataView)
                    arg.DataView = dgd.DataSource as DataView;
                else if (dgd.DataSource is DataTable)
                    arg.DataView = (dgd.DataSource as DataTable).DefaultView;


                arg.UserView = this;
                DataRow dr = null;


                for (int i = 0; i < this._display.Count; i++)
                {
                    UserViewManager.MyColumn m = this._display[i];

                    Telerik.Web.UI.GridColumn col = dgd.MasterTableView.GetColumn(i.ToString());
                    int nColIndex = col.OrderIndex;

                    // 予め表示対象のセルを決定しておく
                    if (0 == m.InnerColumns.Count)
                    {
                        this.AddCss(e.Item.Cells[nColIndex], UserView.GetCss(m.dr.TextAlign));
                        arg.TableCells.Add(m.strFieldName, e.Item.Cells[nColIndex]);
                    }
                    else
                    {
                        // 結合列の場合
                        Table t = CreateInnerTable(m.InnerColumns.Count);
                        t.CssClass = "def";
                        for (int c = 0; c < m.InnerColumns.Count; c++)
                        {
                            UserViewManager.MyColumn mc = m.InnerColumns[c];

                            TableCell cell = t.Rows[c].Cells[0];
                            this.AddCss(cell, UserView.GetCss(mc.dr.TextAlign));

                            arg.TableCells.Add(mc.strFieldName, cell);
                        }
                        e.Item.Cells[nColIndex].CssClass = "fit";
                        e.Item.Cells[nColIndex].Text = "";
                        e.Item.Cells[nColIndex].Controls.Add(t);
                    }
                }


                switch (e.Item.ItemType)
                {
                    case Telerik.Web.UI.GridItemType.Header:
                        arg.ItemType = EnumItemType.Header;
                        e.Item.CssClass = this.HeaderCss;
                        for (int i = 0; i < this._display.Count; i++)
                        {
                            UserViewManager.MyColumn m = this._display[i];

                            if (0 == m.InnerColumns.Count)
                            {
                                TableCell cell = arg.TableCells[m.strFieldName];

                                AddCss(cell, m.HeaderCSS);
                                if (_tblSort.ContainsKey(m.strFieldName))
                                {
                                    // ソートされている項目
                                    string str = "";
                                    if (_tblSort[m.strFieldName] == System.Data.SqlClient.SortOrder.Ascending)
                                        str = this.SortText_ASC;
                                    else
                                        str = this.SortText_DESC;
                                    cell.Text = m.strCaption + str;
                                }
                                else
                                    cell.Text = m.strCaption;
                            }
                            else
                            {
                                // 結合列の場合
                                for (int c = 0; c < m.InnerColumns.Count; c++)
                                {
                                    UserViewManager.MyColumn mc = m.InnerColumns[c];

                                    TableCell cell = arg.TableCells[mc.strFieldName];
                                    this.AddCss(cell, this.HeaderCss);
                                    this.AddCss(cell, mc.HeaderCSS);
                                    if (_tblSort.ContainsKey(mc.strFieldName))
                                    {
                                        // ソートされている項目
                                        string str = "";
                                        if (this._tblSort[mc.strFieldName] == System.Data.SqlClient.SortOrder.Ascending)
                                            str = this.SortText_ASC;
                                        else
                                            str = this.SortText_DESC;
                                        cell.Text = mc.strCaption + str;
                                    }
                                    else
                                        cell.Text = mc.strCaption;
                                }
                            }
                        }
                        break;
                    case Telerik.Web.UI.GridItemType.Item:
                    case Telerik.Web.UI.GridItemType.AlternatingItem:
                    case Telerik.Web.UI.GridItemType.SelectedItem:
                        dr = (e.Item.DataItem as DataRowView).Row;
                        arg.DataRow = dr;
                        arg.DataRowIndex = e.Item.ItemIndex;
                        arg.ItemType = EnumItemType.DataRow;
                        for (int i = 0; i < this._display.Count; i++)
                        {
                            UserViewManager.MyColumn m = this._display[i];

                            if (0 < m.InnerColumns.Count)
                            {
                                // 結合列の場合
                                for (int c = 0; c < m.InnerColumns.Count; c++)
                                {
                                    UserViewManager.MyColumn mc = m.InnerColumns[c];
                                    TableCell cell = arg.TableCells[mc.strFieldName];
                                    cell.Text = GetText(mc, dr);
                                    if ("" == cell.Text) cell.Text = "&nbsp;";
                                }
                            }
                            else
                            {
                                TableCell cell = arg.TableCells[m.strFieldName];
                                cell.Text = GetText(m, dr);
                                if ("" == cell.Text) cell.Text = "&nbsp;";
                            }
                        }
                        break;
                    case Telerik.Web.UI.GridItemType.Footer:
                        arg.ItemType = EnumItemType.Footer;

                        break;

                }

                // コールバック
                if (null != this._DataBoundEventHandler)
                    this._DataBoundEventHandler(arg);

            }
        }


        public class MyColumn
        {
            public string strCaption = "";
            public string strFieldName = "";
            public Core.Sql.Dataset.T_UserListFieldRow dr = null;

            //private Telerik.WebControls.GridColumn _gc = null;
            private Telerik.Web.UI.GridColumn _gc2 = null;

            public MyColumn _parent = null;
            private MyColumnCollection _InnerColumns = null;

            public Telerik.Web.UI.GridColumn WebUI_GridColumn
            {
                get
                {
                    return _gc2;
                }
                set
                {
                    _gc2 = value;
                }
            }

            public MyColumnCollection InnerColumns
            {
                get
                {
                    return _InnerColumns;
                }
            }

            public string HeaderCSS
            {
                get;
                set;
            }

            public MyColumn()
            {
                _InnerColumns = new MyColumnCollection(this);
            }
        }


        public class MyColumnCollection : System.Collections.CollectionBase
        {
            public MyColumn _parent = null;

            public MyColumnCollection(MyColumn parent)
            {
                _parent = parent;
            }

            public void MoveTo(int nIndex, int nToIndex)
            {
                MyColumn m = this[nIndex];
                this.RemoveAt(nIndex);
                this.InnerList.Insert(nToIndex, m);
            }

            public void Insert(int nIndex, MyColumn m)
            {
                this.InnerList.Insert(nIndex, m);
            }

            public void Add(MyColumn m)
            {
                // 外部のコレクションに登録されている場合は削除しておきたいが・・・
                m._parent = this._parent;
                this.InnerList.Add(m);
            }


            public MyColumn this[int index]
            {
                get
                {
                    return this.InnerList[index] as MyColumn;
                }
            }


            /// <summary>
            /// 子アイテムを含む全アイテムから検索
            /// </summary>
            /// <param name="strColumnName"></param>
            /// <returns></returns>
            public MyColumn FindByColumnName(string strColumnName)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    MyColumn m = _FindMyColumn(this[i], strColumnName);
                    if (null != m) return m;
                }
                return null;
            }

            /// <summary>
            /// 指定のカラム名を含むインデックスを取得
            /// </summary>
            /// <param name="strColumnName"></param>
            /// <returns></returns>
            public int IndexOf(string strColumnName)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (0 == this[i].InnerColumns.Count)
                    {
                        if (this[i].strFieldName == strColumnName)
                            return i;
                    }
                    else
                    {
                        MyColumn m = _FindMyColumn(this[i], strColumnName);
                        if (null != m) return i;
                    }
                }
                return -1;
            }


            private MyColumn _FindMyColumn(MyColumn p, string strColumnName)
            {
                if (p.strFieldName == strColumnName) return p;
                if (0 == p.InnerColumns.Count) return null;
                for (int i = 0; i < p.InnerColumns.Count; i++)
                {
                    MyColumn m = _FindMyColumn(p.InnerColumns[i], strColumnName);
                    if (null != m) return m;
                }
                return null;
            }


            /// <summary>
            /// 階層をフラットな配列にばらす
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            public MyColumn[] All()
            {
                System.Collections.ArrayList lst = new ArrayList();
                for (int i = 0; i < this.Count; i++)
                {
                    GetChildren(this[i], ref lst);
                }
                MyColumn[] a = new MyColumn[lst.Count];
                lst.CopyTo(a, 0);
                return a;
            }

            /// <summary>
            /// 結合列は取得しないで
            /// </summary>
            /// <param name="lst"></param>
            private void GetChildren(MyColumn p, ref System.Collections.ArrayList lst)
            {
                if (0 == p.InnerColumns.Count)
                {
                    // 自身
                    lst.Add(p);
                }
                else
                {
                    for (int i = 0; i < p.InnerColumns.Count; i++)
                    {
                        GetChildren(p.InnerColumns[i], ref lst);
                    }
                }
            }
        }

        public static Core.Sql.Dataset.T_UserListFieldDataTable
            GetColumnsSetting(int nListID, string strUserID,
            out MyColumnCollection display, out MyColumnCollection not_display, ref string strSort)
        {
            display = new MyColumnCollection(null);
            not_display = new MyColumnCollection(null);

            Core.Sql.Dataset.T_UserListRow drUl = Core.Sql.SqlDataFactory.getT_UserListRow(nListID, Global.GetConnection());
            if (null == drUl) return null;

            Dataset.T_UserViewRow dr = null;

            Core.Sql.Dataset.T_UserListFieldDataTable dt =
                Core.Sql.SqlDataFactory.getT_UserListFieldDataTable(nListID, Global.GetConnection());


            // 無視するフィールドを予め削除しておく
            if ("" != drUl.IgnoreFields)
            {
                string[] str = drUl.IgnoreFields.Split('\t');
                for (int i = 0; i < str.Length; i++)
                {
                    Core.Sql.Dataset.T_UserListFieldRow d = dt.FindByListIDFieldName(nListID, str[i]);
                    if (null != d) d.Delete();
                }
                dt.AcceptChanges();
            }


            DataView dv = new DataView(dt);

            // 標準設定を読み込む
            Dataset.T_UserViewRow drDefault =
                UserViewClass.getT_UserViewRow(nListID, "", Global.GetConnection());

            if ("" != strUserID)
            {
                dr = UserViewClass.getT_UserViewRow(nListID, strUserID, Global.GetConnection());
                if (null == dr)
                    dr = drDefault;	// デフォルトの設定を使用する。
            }
            else
                dr = drDefault;

            if (null == dr)
            {
                dr = new Dataset.T_UserViewDataTable().NewT_UserViewRow();
                dr.ListID = nListID;
                dr.Sort = "";
                dr.Columns = "";
                dr.UserID = strUserID;
            }

            strSort = dr.Sort;


            System.Collections.ArrayList lstCol = new System.Collections.ArrayList();

            if ("" != dr.Columns)
            {
                // このユーザが選択している列を取得
                System.Collections.Hashtable tblCol = new System.Collections.Hashtable();
                for (int i = 0; i < dt.Count; i++)
                    tblCol.Add(dt[i].FieldName, dt[i]);

                System.Collections.ArrayList lstSelected = new System.Collections.ArrayList();

                string[] strColumns = dr.Columns.Split('\t');
                for (int i = 0; i < strColumns.Length; i++)
                {
                    string strColName = strColumns[i];
                    if ("" == strColName) continue;
                    string[] str = strColName.Split(',');
                    MyColumn m = new MyColumn();
                    if (1 < str.Length)
                    {
                        // グループ
                        System.Collections.ArrayList lstChild = new System.Collections.ArrayList();
                        for (int c = 0; c < str.Length; c++)
                        {
                            if (!tblCol.Contains(str[c])) continue;
                            lstChild.Add(str[c]);
                        }
                        if (0 == lstChild.Count) continue;
                        if (1 == lstChild.Count)
                        {
                            string s = lstChild[0] as string;
                            Core.Sql.Dataset.T_UserListFieldRow d = tblCol[s] as Core.Sql.Dataset.T_UserListFieldRow;
                            if (string.IsNullOrEmpty(d.Caption))
                                m.strCaption = s;
                            else
                                m.strCaption = d.Caption;

                            m.strFieldName = s;
                            m.dr = d;
                            lstSelected.Add(s);
                        }
                        else
                        {
                            for (int c = 0; c < lstChild.Count; c++)
                            {
                                string s = lstChild[c] as String;
                                MyColumn cc = new MyColumn();
                                Core.Sql.Dataset.T_UserListFieldRow d =
                                    tblCol[s] as Core.Sql.Dataset.T_UserListFieldRow;
                                cc.strFieldName = s;
                                if (string.IsNullOrEmpty(d.Caption))
                                    cc.strCaption = s;
                                else
                                    cc.strCaption = d.Caption;

                                cc.dr = d;
                                m.InnerColumns.Add(cc);
                                lstSelected.Add(s);
                            }
                        }
                    }
                    else
                    {
                        // 1列1項目の場合
                        if (!tblCol.Contains(strColName)) continue;

                        Core.Sql.Dataset.T_UserListFieldRow d =
                            tblCol[strColName] as Core.Sql.Dataset.T_UserListFieldRow;

                        if (string.IsNullOrEmpty(d.Caption))
                            m.strCaption = strColName;
                        else
                            m.strCaption = d.Caption;

                        m.strFieldName = d.FieldName;
                        m.dr = d;
                        lstSelected.Add(strColName);
                    }
                    display.Add(m);
                }

                System.Collections.ArrayList lstNotSelected = new System.Collections.ArrayList();
                for (int i = 0; i < dt.Count; i++)
                {
                    if (false == lstSelected.Contains(dt[i].FieldName))
                    {
                        MyColumn m = new MyColumn();
                        m.strFieldName = dt[i].FieldName;
                        m.strCaption = dt[i].Caption;
                        m.dr = dt[i];
                        not_display.Add(m);
                    }
                }
            }
            else
            {
                dv.RowFilter = null;
                dv.Sort = "ColumnIndex";
                for (int i = 0; i < dv.Count; i++)
                {
                    Core.Sql.Dataset.T_UserListFieldRow d = dv[i].Row as Core.Sql.Dataset.T_UserListFieldRow;
                    MyColumn m = new MyColumn();
                    m.strFieldName = d.FieldName;
                    m.strCaption = d.Caption;
                    m.dr = d;
                    display.Add(m);
                }
            }
            return dt;
        }
*/
    }
}
