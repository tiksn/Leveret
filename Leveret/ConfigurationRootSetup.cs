using Microsoft.Extensions.Configuration;
using System;
using TIKSN.Configuration;

namespace TIKSN.Leveret
{
    public class ConfigurationRootSetup : ConfigurationRootSetupBase
    {
        private readonly string[] _args;

        public ConfigurationRootSetup(string[] args)
        {
            _args = args ?? throw new ArgumentNullException(nameof(args));
        }

        protected override void SetupConfiguration(IConfigurationBuilder builder)
        {
            base.SetupConfiguration(builder);

            builder.AddCommandLine(_args);
        }
    }
}