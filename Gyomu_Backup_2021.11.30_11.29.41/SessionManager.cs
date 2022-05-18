using DLL;
using System;
using System.Web;
using Yodokou_HanbaiKanri;
using System.Data;
namespace Gyomu
{
    public class SessionManager
    {
        private const string SESSION_USER_ID = "SESSION_USER_ID";
        private const string SESSION_USER_NAME = "SESSION_USER_NAME";
        private const string SESSION_USER_KUBUN = "SESSION_USER_KUBUN";

        public const string SESSION_USER = "SESSION_LOGIN_USER";
        public const string SESSION_USER_BUSYO = "SESSION_USER_BUSYO";

        public const string SESSION_HACCYU_NO = "SESSION_HACCYU_NO";


        public const string SESSION_Mitumori_Type = "SESSION_Mitumori_Type";

        public const string SESSION_KI = "SESSION_KI";

        public const string SESSION_ORDERED = "SESSION_ORDERED";
        public const string SESSION_DATA = "SESSION_DATA";
        public DataTable newdt = new DataTable();
        public static string TokuisakiCode;
        public static DataSet1.M_Tokuisaki2Row drTokuisaki;
        public static string NouhinsakiCode;



        internal static void Logout()
        {
            HttpContext.Current.Session.Abandon();
            System.Web.Security.FormsAuthentication.SignOut();
        }

        //internal static void Login(DataSet1.M_UserRow dr)
        internal static void Login(DataSet1.M_TantoRow dr)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(dr.UserID.ToString(), false);
            //System.Web.HttpContext.Current.Session[SESSION_USER_KUBUN] = dr.Gyomu;

            System.Web.HttpContext.Current.Session[SESSION_USER_ID] = dr.UserID;
            System.Web.HttpContext.Current.Session[SESSION_USER_NAME] = dr.UserName;
            System.Web.HttpContext.Current.Session[SESSION_USER_BUSYO] = dr.Busyo;

            LoginUser u = LoginUser.New(dr.UserID.ToString());
            System.Web.HttpContext.Current.Session[SESSION_USER] = u;
        }

        public void OshiraseData(DataTable dtB)
        {
            newdt = dtB;
        }


        public class LoginUser : Core.Web.WebUser
        {
            private string _strTwoLetterISOLanguageName = null;//★

            //private DataSet1.M_UserRow _drUser = null;
            private DataLogin.M_TantoRow _drUser = null;

            private System.Collections.Hashtable _tblSessionData = new System.Collections.Hashtable();

            private System.Collections.Hashtable _tblUserView = new System.Collections.Hashtable();	// 自身の表示設定(キーはリストID)

            internal static LoginUser New(string userID)
            {
                LoginUser u = new LoginUser
                {
                    //_drUser = Class1.getM_UserRow(userID, Global.GetConnection())
                    _drUser = ClassLogin.getM_TantoRow(userID, Global.GetConnection())
                };
                if (null == u._drUser) return null;

                //u.TwoLetterISOLanguageName = v;

                return u;
            }

            //public DataSet1.M_UserRow M_User
            public DataLogin.M_TantoRow M_user
            {
                get { return this._drUser; }
                set { this._drUser = value; }
            }


            public string UserID
            {
                get
                {
                    return this._drUser.UserID;
                }
            }

            public string UserName
            {
                get
                {
                    return this._drUser.UserName;
                }
            }

            public string TwoLetterISOLanguageName
            {
                get { return this._strTwoLetterISOLanguageName; }
                set { this._strTwoLetterISOLanguageName = value; }
            }

            internal UserViewManager.UserView GetUserView(int lIST_ID_DL)
            {
                if (_tblUserView.Contains(lIST_ID_DL))
                    return _tblUserView[lIST_ID_DL] as UserViewManager.UserView;
                else
                    return SetUserView(lIST_ID_DL);
            }

            private UserViewManager.UserView SetUserView(int lIST_ID_DL)
            {
                UserViewManager.UserView v = UserViewManager.UserView.New(lIST_ID_DL, this._drUser.UserID, true);
                _tblUserView.Remove(lIST_ID_DL);
                _tblUserView[lIST_ID_DL] = v;
                return v;
            }

            //public bool Gyomu
            //{
            //    get
            //    {
            //        return this._drUser.Gyomu;
            //    }
            //}
        }

        public static LoginUser User
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_USER];
                    if (null == obj) throw new Exception("SessionOut");
                    return obj as LoginUser;
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }

        internal static void MitumoriSyusei(string MitumoriNo)
        {
            System.Web.HttpContext.Current.Session[SESSION_HACCYU_NO] = MitumoriNo;
        }

        internal static void MitumoriSyuseiRow(string MitumoriRow)
        {

        }

        internal static void SessionData(DataTable dt)
        {
            System.Web.HttpContext.Current.Session[SESSION_DATA] = dt;
        }


        public static object SESSION_DT
        {
            get
            {
                try
                {
                    object obj = HttpContext.Current.Session[SESSION_DATA];
                    if (null == obj)
                    { throw new Exception("SessionOut"); }
                    return obj;
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }


        internal static void KI()
        {
            int KIno = 0;
            int CompanyBorn = 1995;
            string Today = DateTime.Now.ToShortDateString();
            string[] Year = Today.Split('/');
            if (9 <= int.Parse(Year[1]))
            {
                KIno = int.Parse(Year[0]) + 1 - CompanyBorn;
            }
            else
            {
                KIno = int.Parse(Year[0]) - CompanyBorn;
            }

            System.Web.HttpContext.Current.Session[SESSION_KI] = KIno;
        }

        internal static void SHIIRESAKI(string OrderedData)
        {
            System.Web.HttpContext.Current.Session[SESSION_ORDERED] = OrderedData;
        }


        public static string HACCYU_NO
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_HACCYU_NO];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToString(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }

        public static string ORDERED_DATA
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_ORDERED];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToString(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }


        public static string KII
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_KI];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToString(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }


        internal static void MitumoriType(string Type)
        {
            System.Web.HttpContext.Current.Session[SESSION_Mitumori_Type] = Type;
        }

        public static string Mitumori_Type
        {
            get
            {
                try
                {
                    object obj = System.Web.HttpContext.Current.Session[SESSION_Mitumori_Type];
                    if (null == obj) throw new Exception("SessionOut");
                    return Convert.ToString(obj);
                }
                catch
                {
                    System.Web.HttpContext.Current.Response.Redirect(Global.LoginPageURL, true);
                    return null;
                }
            }
        }

        public static void JucyuSyusei(string HaccyuNo)
        {
            System.Web.HttpContext.Current.Session[SESSION_HACCYU_NO] = HaccyuNo;
        }

        internal static void OrderedData(string strKeys)
        {
            System.Web.HttpContext.Current.Session[SESSION_ORDERED] = strKeys;
        }
    }
}