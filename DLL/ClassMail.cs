using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;

namespace DLL
{
    public class ClassMail
    {
        public static void ErrorMail(string mail_to, string title, string body)
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
            System.Net.Mail.MailAddress Frommeado = new System.Net.Mail.MailAddress(address: "example@example.com");
            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from: Frommeado, to: Tomeado);
            msg.Subject = strTitle;
            msg.Body = strBody;
            msg.SubjectEncoding = enc;
            //SMTPサーバーなどを設定する
            sc.Host = "192.168.2.156";
            sc.Port = 25;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            sc.Send(msg);
        }

        public static void GetErrorIP(IPHostEntry ipentry)
        {
            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
            string strMail_To = "maeda@m2m-asp.com";
            string strTitle = "MMC｜IPエントリー(エラー)";
            string strBody = "";
            foreach (IPAddress ip in ipentry.AddressList)
            {
                strBody += ip.ToString() + "\r\n";
            }
            //JISコード
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(50220);
            //届くメアド登録
            System.Net.Mail.MailAddress Tomeado = new System.Net.Mail.MailAddress(address: strMail_To);
            //送られてきたメールアドレス登録
            System.Net.Mail.MailAddress Frommeado = new System.Net.Mail.MailAddress(address: "example@example.com");
            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from: Frommeado, to: Tomeado);
            msg.Subject = strTitle;
            msg.Body = strBody;
            msg.SubjectEncoding = enc;
            //SMTPサーバーなどを設定する
            sc.Host = "192.168.2.156";
            sc.Port = 25;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            sc.Send(msg);
        }

        public static void GetIP(IPHostEntry ipentry)
        {
            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
            string strMail_To = "maeda@m2m-asp.com";
            string strTitle = "MMC｜IPエントリー";
            string strBody = "";
            foreach (IPAddress ip in ipentry.AddressList)
            {
                strBody += ip.ToString() + "\r\n";
            }
            //JISコード
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(50220);
            //届くメアド登録
            System.Net.Mail.MailAddress Tomeado = new System.Net.Mail.MailAddress(address: strMail_To);
            //送られてきたメールアドレス登録
            System.Net.Mail.MailAddress Frommeado = new System.Net.Mail.MailAddress(address: "example@example.com");
            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from: Frommeado, to: Tomeado);
            msg.Subject = strTitle;
            msg.Body = strBody;
            msg.SubjectEncoding = enc;
            //SMTPサーバーなどを設定する
            sc.Host = "192.168.2.156";
            sc.Port = 25;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            sc.Send(msg);
        }

        public static void ErrorMail2(string mail_to, string mail_title, string body, string datetimenow)
        {
            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
            string strMail_To = mail_to;
            string strTitle = mail_title;
            string strBody = body;
            //JISコード
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(50220);
            //届くメアド登録
            System.Net.Mail.MailAddress Tomeado = new System.Net.Mail.MailAddress(address: strMail_To);
            //送られてきたメールアドレス登録
            System.Net.Mail.MailAddress Frommeado = new System.Net.Mail.MailAddress(address: "example@example.com");
            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(from: Frommeado, to: Tomeado);
            //添付画像
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(@"C:\inetpub\wwwroot\MMC_Test用\ErrorIMG\Error" + datetimenow + ".jpg");
            msg.Attachments.Add(attachment);

            msg.Subject = strTitle;
            msg.Body = strBody;
            msg.SubjectEncoding = enc;
            //SMTPサーバーなどを設定する
            sc.Host = "192.168.2.156";
            sc.Port = 25;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            sc.Send(msg);
        }
    }
}
