using Microsoft.Extensions.Logging;
using Mond;
using System;
using System.Linq;
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
            var state = new MondState()
            {
                Options = new MondCompilerOptions()
                {
                    MakeRootDeclarationsGlobal = true,
                    UseImplicitGlobals = true
                }
            };

            try
            {
                var result = state.Run(sourceCode);
                _logger.LogInformation($"Type: {result.Type}, IsEnumerable: {result.IsEnumerable}, IsLocked: {result.IsLocked}");

                var globalVariables = state.Run("return global;").Object.Where(x=>x.Value.Type != MondValueType.Function && x.Value.Type != MondValueType.Object);

                return string.Join(',', globalVariables.Select(x => $"{x.Key}={x.Value}"));

                if (result.Type == MondValueType.Undefined)
                    return string.Empty;

                return result.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return string.Empty;
        }
    }
}