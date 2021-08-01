using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MY_CALS_BACKEND.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Services
{
    public interface IMailService
    {
        Task<Response> SendEmailAsync(string toEmail, string subject, string content);
        Task<Boolean> confirmEmailAsync(string userId, string token);
    }

    public class SendGridMailService : IMailService
    {
        private IConfiguration _configuration;
        private readonly UserManager<UserAccount> _userManager;
        public SendGridMailService(IConfiguration configuration,UserManager<UserAccount> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public Task<bool> confirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> SendEmailAsync(string toEmail, string subject, string content)
        {
            var apikey =  _configuration["SendGridKey"];
            var client = new SendGridClient(apikey);
            var from = new EmailAddress("idaissa93@gmail.com", "JAMAL ID AISSA");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            return await client.SendEmailAsync(msg);
        }
    }
}
