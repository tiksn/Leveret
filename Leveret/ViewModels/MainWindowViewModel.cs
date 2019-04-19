using System;
using System.Reactive.Linq;
using ReactiveUI;
using TIKSN.Leveret.BusinessLogic.Calculation;

namespace TIKSN.Leveret.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<CalculationResult> _executionResults;
        private string _inputSourceCode;

        public MainWindowViewModel(ICalculationService calculationService)
        {
            _executionResults = this
            .WhenAnyValue(x => x.InputSourceCode)
            .Throttle(TimeSpan.FromMilliseconds(800))
            .DistinctUntilChanged()
            .Where(code => !string.IsNullOrWhiteSpace(code))
            .SelectMany(calculationService.CalculateAsync)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.ExecutionResults);
        }

        public CalculationResult ExecutionResults => _executionResults.Value;

        public string InputSourceCode
        {
            get => _inputSourceCode;
            set => this.RaiseAndSetIfChanged(ref _inputSourceCode, value);
        }
    }
}