using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1 {
    class Smtp {

        SmtpClient cl;
        public Smtp (string host, int port)

        {   this.cl = new SmtpClient (host,port);
            this.cl.UseDefaultCredentials = true;


        }

        public void Send( string _from, string _to, string Topic, string body, Stream attachment, string attachmentMineType)
        {
            
            MailMessage mail = new MailMessage(_from,_to,Topic,body);
            if(attachment != null)
            {
                 mail.Attachments.Add(
                    new Attachment(attachment, attachmentMineType)
                );



            }
            this.cl.Send(mail);

        }


    }
    class Program {
        static void Main(string[] args) {
        }
    }
}
