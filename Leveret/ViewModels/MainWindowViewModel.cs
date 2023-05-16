using ReactiveUI;
using System;
using System.Reactive.Linq;
using TIKSN.Leveret.Interpretation.Abstractions;

namespace TIKSN.Leveret.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<InterpretationResult> _executionResults;
    private string _inputSourceCode;

    public MainWindowViewModel(IInterpretationService interpretationService)
    {
        if (interpretationService == null)
        {
            throw new ArgumentNullException(nameof(interpretationService));
        }

        _executionResults = this
        .WhenAnyValue(x => x.InputSourceCode)
        .Throttle(TimeSpan.FromMilliseconds(800))
        .DistinctUntilChanged()
        .Where(code => !string.IsNullOrWhiteSpace(code))
        .SelectMany(interpretationService.InterpretationAsync)
        .ObserveOn(RxApp.MainThreadScheduler)
        .ToProperty(this, x => x.ExecutionResults);
    }

    public InterpretationResult ExecutionResults => _executionResults.Value;

    public string InputSourceCode
    {
        get => _inputSourceCode;
        set => this.RaiseAndSetIfChanged(ref _inputSourceCode, value);
    }
}