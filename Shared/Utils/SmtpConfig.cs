using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utils
{
    public class SmtpConfig
    {
        public SmtpConfig()
        {

        }

        public SmtpConfig(string smtpServer, string smtpPort, string smtpUser, string smtpPassword, string smtpFrom)
        {
            SmtpServer = smtpServer;
            SmtpPort = smtpPort;
            SmtpUser = smtpUser;
            SmtpPassword = smtpPassword;
            SmtpFrom = smtpFrom;
        }

        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpFrom { get; set; }
    }
}
