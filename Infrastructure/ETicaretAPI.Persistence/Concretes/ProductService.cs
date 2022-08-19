using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        private readonly LogDbContext _logDbContext;
        private readonly IConfiguration _configuration;
        public ProductService(LogDbContext logDbContext, IConfiguration configuration)
        {
            this._logDbContext = logDbContext;
            this._configuration = configuration;
        }

        public async Task<bool> AddSingleRequest(RequestLog requestLog)
        {
            await _logDbContext.RequestLogs.AddAsync(requestLog);
            return await SaveAsync();
        }

        public List<Product> GetProducts()
            => new()
            {
                new(){Id=Guid.NewGuid(), Name="Product1", Price=100,Stock=10},
                new(){Id=Guid.NewGuid(), Name="Product2", Price=140,Stock=10},
                new(){Id=Guid.NewGuid(), Name="Product3", Price=130,Stock=10},
                new(){Id=Guid.NewGuid(), Name="Product4", Price=110,Stock=10},
                new(){Id=Guid.NewGuid(), Name="Product5", Price=190,Stock=10}
            };

        public async Task<bool> SaveAsync()
        {
            var saved = await _logDbContext.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task SendEmail()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse("abicoo1996@hotmail.com"));
            email.Subject = "Test email subject";
            email.Body = new TextPart(TextFormat.Html) { Text = "This is the body"};
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("EmailHost").Value,587,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendSms()
        {
            var accountSid = "ACfd1126bca4b01ba37db773587308abff";
            var authToken = "369e8f05d98f174feb671d6411579181";
            TwilioClient.Init(accountSid, authToken);
            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+905326254522"));
            messageOptions.Body = "This is SMS body!";
            messageOptions.From = "+19806554864";
            var message = await MessageResource.CreateAsync(messageOptions);
            var messageBody = message.Body;
        }
    }
}
