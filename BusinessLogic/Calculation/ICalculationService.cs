using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public interface ICalculationService
    {
        Task<ImmutableList<GlobalVariable>> CalculateAsync(string sourceCode, CancellationToken cancellationToken);
    }
}