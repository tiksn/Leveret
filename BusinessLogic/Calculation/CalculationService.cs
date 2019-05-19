using MediatR;
using Microsoft.Extensions.Logging;
using Mond;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TIKSN.Leveret.BusinessLogic.Factories;
using TIKSN.Leveret.BusinessLogic.Messages;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogger<CalculationService> _logger;
        private readonly IMediator _mediator;
        private readonly IMondStateFactory _mondStateFactory;

        public CalculationService(IMediator mediator, IMondStateFactory mondStateFactory, ILogger<CalculationService> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mondStateFactory = mondStateFactory ?? throw new ArgumentNullException(nameof(mondStateFactory));
        }

        public async Task<CalculationResult> CalculateAsync(string sourceCode, CancellationToken cancellationToken)
        {
            var state = _mondStateFactory.Create();

            try
            {
                var result = state.Run(sourceCode);

                var globalVariables = state.Run("return global;").Object.Where(x => x.Value.Type != MondValueType.Function && x.Value.Type != MondValueType.Object);

                var globalVariablesTasks = globalVariables.Select(x => _mediator.Send(new ValueFormatRequest(x.Value), cancellationToken));

                var values = await Task.WhenAll(globalVariablesTasks);

                var variables = globalVariables.Zip(values, (g, v) => new GlobalVariable(g.Key.ToString(), v)).ToImmutableList();

                return CalculationResult.CreateSuccess(variables);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return CalculationResult.CreateFailure(ex.Message);
            }
        }
    }
}