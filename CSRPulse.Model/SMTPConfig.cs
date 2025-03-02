﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class SMTPConfig
    {
        public string SenderAddress { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHTML { get; set; }
        public string Signature { get; set; }
    }
}
