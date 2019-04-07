using Microsoft.Extensions.Logging;
using TIKSN.Analytics.Logging;

namespace TIKSN.Leveret
{
    public class LoggingSetup : LoggingSetupBase
    {
        public LoggingSetup(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        public override void Setup()
        {
            base.Setup();

            _loggerFactory.AddConsole(includeScopes: true);
        }
    }
}
