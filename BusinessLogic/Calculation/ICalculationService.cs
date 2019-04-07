using System.Threading;
using System.Threading.Tasks;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public interface ICalculationService
    {
        Task<string> CalculateAsync(string sourceCode, CancellationToken cancellationToken);
    }
}