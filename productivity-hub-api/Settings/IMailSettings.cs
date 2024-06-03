namespace productivity_hub_api.Settings
{
    public interface IMailSettings
    {
        string SmtpServer { get; set; }

        int Port { get; set; }

        string SenderName { get; set; }

        string SenderEmail { get; set; }

        string Username { get; set; }

        string Password { get; set; }
    }
}
