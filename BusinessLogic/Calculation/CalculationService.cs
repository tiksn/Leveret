using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogger<CalculationService> _logger;

        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> CalculateAsync(string sourceCode, CancellationToken cancellationToken)
        {
            _logger.LogInformation(sourceCode);
            return sourceCode;
        }
    }
}