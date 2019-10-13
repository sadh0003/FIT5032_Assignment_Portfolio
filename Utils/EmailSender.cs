using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public async Task SendAsync(String toEmail, String subject, String contents, String fileLocation, String fileName)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@GoBook.com", "GoBook Accommodation Locater");

            String[] strlist = toEmail.Split(',');
            var to = new EmailAddress(strlist[0].Trim(), "");

            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (strlist.Length > 1)
            {
                for(var i = 1; i < strlist.Length; i++)
                {
                    msg.AddBcc(new EmailAddress(strlist[i].Trim(), ""));
                }
            }

            using (var fileStream = File.OpenRead(fileLocation + fileName))
            {
                await msg.AddAttachmentAsync(fileName, fileStream);
                var response = await client.SendEmailAsync(msg);
            }

        }

    }
}