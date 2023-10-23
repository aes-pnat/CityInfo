﻿namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo;
        private readonly string _mailFrom;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"] ?? string.Empty;
            _mailFrom = configuration["mailSettings:mailFromAddress"] ?? string.Empty;
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} tp {_mailTo}, with {nameof(CloudMailService)}.");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}

