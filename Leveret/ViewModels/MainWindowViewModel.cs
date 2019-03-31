using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TIKSN.Leveret.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<string> _executionResults;
        private string _inputSourceCode;

        public MainWindowViewModel()
        {
            _executionResults = this
            .WhenAnyValue(x => x.InputSourceCode)
            .Throttle(TimeSpan.FromMilliseconds(800))
            .DistinctUntilChanged()
            .Where(code => !string.IsNullOrWhiteSpace(code))
            .SelectMany(ExecuteCodeAsync)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.ExecutionResults);
        }

        public string ExecutionResults => _executionResults.Value;

        public string InputSourceCode
        {
            get => _inputSourceCode;
            set => this.RaiseAndSetIfChanged(ref _inputSourceCode, value);
        }

        private async Task<string> ExecuteCodeAsync(string code, CancellationToken token)
        {
            Console.WriteLine(code);
            return code;
        }
    }
}