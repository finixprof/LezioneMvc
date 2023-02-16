using Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Site.Helpers
{
    public static class EmailHelper
    {
        public static string HostSmtp { get; set; }

        public static int PortSmtp { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }

        //configurazione del client smtp
        //spostata nel program.cs
        public static SmtpClient SmtpClient { get; set; }

        public static void SendEmailConfermaRegistrazione(Utente utente, string link)
        {

            //preparazione della mail
            var mailMessage = new MailMessage
            {
                From = new MailAddress(Email),
                Subject = "Lezione mvc APP: Conferma indirizzo mail per registrazione portale",
                Body = $"<h1>Ciao {utente.Username}</h1>" +
                    $"<a href='{link}'>clicca qui per confermare</a>",
                IsBodyHtml = true,
            };
            //aggiunta destinatario/i
            mailMessage.To.Add(utente.Email);

            try
            {
                //invio della mail
                SmtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore invio mail", ex);
            }
        }
    }
}
