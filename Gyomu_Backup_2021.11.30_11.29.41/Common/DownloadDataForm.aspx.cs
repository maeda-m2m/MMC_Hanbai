using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gyomu.Common
{
    public partial class DownloadDataForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();


            FileDataInfo fi = new FileDataInfo();

            try
            {
                string[] str = SessionManager.User.Decode(this.Request.Url.Query.Substring(1));
                if (null == str)
                {
                    ShowErrMsg("不正なアクセスです。");
                    return;
                }


                fi.bDeleteFile = Convert.ToBoolean(str[0]);
                fi.nTextEncodingCodePage = Convert.ToInt32(str[1]);
                fi.strDataCacheKey = str[2];
                fi.strFileName = str[3];
                fi.strFilePath = str[4];
                fi.type = (EnumDataType)int.Parse(str[5]);

                if (null == fi) throw new Exception("");


                string strFileName = "";
                switch (fi.type)
                {
                    case EnumDataType.File:
                        if (!string.IsNullOrEmpty(fi.strFileName))
                            strFileName = fi.strFileName;
                        else
                            strFileName = System.IO.Path.GetFileName(fi.strFilePath);
                        break;
                    default:
                        strFileName = fi.strFileName;
                        break;
                }

                // 半角空白が+に変わるので.Replace("+", "%20")
                Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(strFileName).Replace("+", "%20"));
                Response.ContentType = "application/octet-stream";


                switch (fi.type)
                {
                    case EnumDataType.File:
                        {
                            this.Response.WriteFile(fi.strFilePath);
                            this.Response.Flush();	// ★ Flush()しないとファイルが削除された後にファイルを読みこんでしまう

                            if (fi.bDeleteFile)
                            {
                                System.IO.File.Delete(fi.strFilePath);
                            }

                        }
                        break;
                    case EnumDataType.Text:
                        this.Response.ContentEncoding = System.Text.Encoding.GetEncoding(fi.nTextEncodingCodePage);
                        this.Response.Write((string)SessionManager.User.GetCacheData(fi.strDataCacheKey));
                        break;
                    case EnumDataType.Binary:
                        this.Response.BinaryWrite((byte[])SessionManager.User.GetCacheData(fi.strDataCacheKey));
                        break;
                }


            }
            catch (Exception ex)
            {
                try
                {
                    string strFileName;
                    string strData;
                    if (1 == this.Request.QueryString.Count)
                    {
                        string[] str = SessionManager.User.GetSessionData(this.Request.Url.Query.Substring(1)) as string[];
                        strFileName = str[0];
                        strData = str[1];
                    }
                    else
                    {
                        strFileName = this.Request.QueryString[0];
                        object objData = SessionManager.User.GetSessionData(this.Request.QueryString[1]);
                        strData = (null != objData) ? Convert.ToString(objData) : "データがありません。";
                    }
                    if (strFileName != "")
                    {
                        this.Response.Clear();
                        Response.ContentEncoding = System.Text.Encoding.GetEncoding(0);
                        switch (SessionManager.User.TwoLetterISOLanguageName)
                        {
                            case "ja":
                                break;
                            case "en":
                                Response.ContentEncoding = System.Text.Encoding.ASCII;
                                break;
                            case "zh":
                                Response.ContentEncoding = System.Text.Encoding.UTF8;
                                break;
                        }
                        if (((Request.Browser.Browser == "IE") && (Convert.ToDouble(Request.Browser.Version) == 5.5)))
                        {
                            Response.AddHeader("Content-Disposition", "inline; filename=" + System.Web.HttpUtility.UrlEncode(strFileName));
                            Response.ContentType = "applicion/octet-stream-dummy";
                        }
                        else
                        {
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(strFileName));
                            Response.ContentType = "application/octet-stream";
                        }
                        Response.Write(strData);
                        Response.End();
                    }
                    else
                    {
                        this.Response.Clear();
                        this.Response.Write(ex.Message);
                    }

                }
                catch (Exception exce)
                {
                    if (exce.GetType() != typeof(System.Threading.ThreadAbortException))
                        ShowErrMsg(exce.Message);
                }
                finally
                {
                    try
                    {
                        SessionManager.User.RemoveSessionData(this.Request.QueryString[1]);
                    }
                    catch
                    {

                    }
                }
            }
            finally
            {
                this.Response.End();    // 必要
                if (null != fi)
                {
                    if (!string.IsNullOrEmpty(fi.strDataCacheKey))
                        SessionManager.User.RemoveCacheData(fi.strDataCacheKey);
                }
            }
        }

        private enum EnumDataType
        {
            File, Text, Binary
        }

        private class FileDataInfo
        {
            public EnumDataType type = EnumDataType.File;
            public string strDataCacheKey = "";
            public string strFileName = null;
            public string strFilePath;
            public int nTextEncodingCodePage = System.Text.Encoding.UTF8.CodePage;
            public bool bDeleteFile = false;
        }

        internal static string GetQueryString4Binary(string strFileName, byte[] bData)
        {

            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.Binary;
            fi.strFileName = strFileName;
            fi.strDataCacheKey = SessionManager.User.AddCacheData(bData, 3);
            return GetQueryString(fi);
        }

        private static string GetQueryString(FileDataInfo fi)
        {
            return SessionManager.User.Encode(new string[] {
            fi.bDeleteFile.ToString(),
            fi.nTextEncodingCodePage.ToString(),
            fi.strDataCacheKey,
            fi.strFileName,
            fi.strFilePath,
            ((int)fi.type).ToString()
            });
        }

        private void ShowErrMsg(string strMsg)
        {
            Response.Clear();
            Response.Write(string.Format("<script>window.alert('{0}');</script>", strMsg));
            Response.End();
        }

        internal static string GetQueryString4Text(string strFileName, string strTextData)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);

            return GetQueryString4Text(strFileName, strTextData, enc);
        }

        private static string GetQueryString4Text(string strFileName, string strTextData, Encoding enc)
        {
            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.Text;
            fi.strFileName = strFileName;
            fi.strDataCacheKey = SessionManager.User.AddCacheData(strTextData, 3);
            fi.nTextEncodingCodePage = enc.CodePage;
            return GetQueryString(fi);
        }

        internal static void UpQueryString4Text(string filename, byte[] v, long st, Stream c)
        {
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);

            FileDataInfo fi = new FileDataInfo();
            fi.type = EnumDataType.Text;
            fi.strFileName = filename;
            fi.nTextEncodingCodePage = enc.CodePage;

        }
    }
}