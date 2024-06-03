using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using productivity_hub_api.DTOs.Mail;
using productivity_hub_api.Settings;

namespace productivity_hub_api.Service.MailService
{
    public class MailReminderService : IMailReminderService
    {
        private IMailSettings _mailSettings;

        public MailReminderService(IMailSettings mailSettings) 
        {
            _mailSettings = mailSettings;
        }

        public bool SendHTMLMail(HTMLReminderMailDataDto htmlReminderMailDataDto)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    // Remitente
                    emailMessage.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));

                    // Destinatario
                    emailMessage.To.Add(new MailboxAddress(
                        htmlReminderMailDataDto.NombreDestinatario,
                        htmlReminderMailDataDto.EmailDestinatario));

                    // Contenido
                    string strTareaPendiente = htmlReminderMailDataDto.TareasPendientes > 1 ? "tareas pendientes" : "tarea pendiente";
                    emailMessage.Subject = "Recordatorio Diario";
                    emailMessage.Body = new TextPart(TextFormat.Html)
                    {
                        Text = $"<h2>Tiene { htmlReminderMailDataDto.TareasPendientes } { strTareaPendiente } para ahora</h2>"
                    };

                    // Configurar el cliente SMTP
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailSettings.SmtpServer, _mailSettings.Port, SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailSettings.Username, _mailSettings.Password);
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
