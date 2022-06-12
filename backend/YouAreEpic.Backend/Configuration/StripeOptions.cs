﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Configuration
{
    public class StripeOptions
    {
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
        public string WebhookSecret { get; set; }
        public string Domain { get; set; }
    }
}
