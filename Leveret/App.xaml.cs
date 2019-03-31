using Avalonia;
using Avalonia.Markup.Xaml;

namespace Leveret
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
