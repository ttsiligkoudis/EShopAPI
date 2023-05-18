using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using DataModels.Dtos;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="message">The message's instance</param>
        /// <returns></returns>
        // HttpPost api/Messages/SendMessage
        [HttpPost("SendMessage", Name = "SendMessage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ActionResult<object>> SendMessage(MessageDto message)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var mail = new MailMessage
            {
                From = new MailAddress("eshop.myportofolio@gmail.com"),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            mail.To.Add(message.Email);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("eshop.myportofolio@gmail.com", "zpuczaxvicakshwn"),
                EnableSsl = true
            };

            smtp.Send(mail);
            return NoContent();
        }
    }
}
