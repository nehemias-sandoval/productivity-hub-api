using productivity_hub_api.DTOs.Mail;

namespace productivity_hub_api.Service.MailService
{
    public interface IMailReminderService
    {
        bool SendHTMLMail(HTMLReminderMailDataDto htmlReminderMailDataDto);
    }
}
