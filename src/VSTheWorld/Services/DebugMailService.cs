using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSTheWorld.Services
{
    public class DebugMailService : IMailService

    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail: To: {to}, From: {from}, Subject: {subject}, Body: {body}");
            return true;
        }
    }
}
