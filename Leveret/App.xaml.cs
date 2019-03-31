using Avalonia;
using Avalonia.Markup.Xaml;

namespace TIKSN.Leveret
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
