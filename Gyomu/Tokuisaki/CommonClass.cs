using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web;

namespace Gyomu.Tokuisaki
{
    public class CommonClass
    {

        /// <summary>
        /// select文でデータを取得する
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable SelectedTable(string command, SqlConnection sql)
        {
            var da = new SqlDataAdapter(command, sql);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 追加、更新、削除を行う
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        public static void TranSql(string command, SqlConnection sql)
        {
            var da = new SqlCommand(command, sql);
            sql.Open();
            var tran = sql.BeginTransaction();
            try
            {
                da.Transaction = tran;
                da.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tran.Rollback();
            }
            finally
            {
                sql.Close();
            }
        }


        /// <summary>
        /// メールを送信するためのメソッド。
        /// </summary>
        /// <param name="body"></param>
        public static void Mail(string mail_to, string title, string body, string from)
        {
            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();

            string strMail_To = mail_to;
            string strTitle = title;
            string strBody = body;

            //JISコード
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(50220);

            //届くメアド登録
            System.Net.Mail.MailAddress Tomeado = new System.Net.Mail.MailAddress(address: strMail_To);

            //送られてきたメールアドレス登録
            System.Net.Mail.MailAddress Frommeado = new System.Net.Mail.MailAddress(address: from);

            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from: Frommeado, to: Tomeado)
            {

                Subject = strTitle,
                Body = strBody,
                SubjectEncoding = enc,
            };

            sc.Host = "192.168.2.156";

            sc.Port = 25;

            sc.EnableSsl = false;

            sc.Send(msg);
        }


        public static string GetErrorInfo(Exception ex)
        {
            System.IO.StringWriter w = new System.IO.StringWriter();


            // エラー発生ページ
            w.WriteLine("[エラー発生ページ]");
            w.WriteLine(HttpContext.Current.Request.Path);


            // エラーメッセージ
            w.WriteLine("[エラーメッセージ]");
            w.WriteLine(ex.Message);

            // スタックトレース
            w.WriteLine("[スタックトレース]");
            w.WriteLine(ex.StackTrace);

            // セッション値
            // スタックトレース
            w.WriteLine("[セッション値]");
            for (int i = 0; i < HttpContext.Current.Session.Count; i++) w.WriteLine("{0}：{1}", HttpContext.Current.Session.Keys[i], HttpContext.Current.Session[HttpContext.Current.Session.Keys[i]]);

            return w.ToString();

        }


    }
}