using Avalonia;

namespace TIKSN.Leveret
{
    public static class AppBuilderExtensions
    {
        public static AppBuilder UseTXFX(this AppBuilder builder)
        {
            return builder.AfterSetup(_ =>
            {
            });
        }
    }
}