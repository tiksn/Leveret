using System.Threading;
using System.Threading.Tasks;

namespace TIKSN.Leveret.Interpretation.Abstractions
{
    public interface IInterpretationService
    {
        Task<InterpretationResult> InterpretationAsync(string sourceCode, CancellationToken cancellationToken);
    }
}