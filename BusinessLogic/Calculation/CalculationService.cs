using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mond;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogger<CalculationService> _logger;

        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CalculationResult> CalculateAsync(string sourceCode, CancellationToken cancellationToken)
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

                var globalVariables = state.Run("return global;").Object.Where(x => x.Value.Type != MondValueType.Function && x.Value.Type != MondValueType.Object);

                return CalculationResult.CreateSuccess(globalVariables.Select(x => new GlobalVariable(x.Key.ToString(), x.Value.ToString())).ToImmutableList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return CalculationResult.CreateFailure(ex.Message);
            }
        }
    }
}