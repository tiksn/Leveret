using Avalonia.Controls;

namespace TIKSN.Leveret
{
    public static class AppBuilderExtensions
    {
        public static TAppBuilder UseTXFX<TAppBuilder>(this TAppBuilder builder)
            where TAppBuilder : AppBuilderBase<TAppBuilder>, new()
        {
            return builder.AfterSetup(_ =>
            {
            });
        }
    }
}
