﻿using Microsoft.Extensions.Logging;
using TIKSN.Analytics.Logging;

namespace TIKSN.Leveret
{
    public class LoggingSetup : LoggingSetupBase
    {
        public LoggingSetup() : base()
        {
        }

        public override void Setup(ILoggingBuilder loggingBuilder)
        {
            base.Setup(loggingBuilder);

            loggingBuilder.AddConsole(options =>
            {
                options.IncludeScopes = true;
            });
        }
    }
}