using MediatR;
using Microsoft.Extensions.Logging;
using Mond;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TIKSN.Leveret.Interpretation.Abstractions;
using TIKSN.Leveret.Interpretation.MondConcretion.Factories;
using TIKSN.Leveret.Interpretation.MondConcretion.Messages;

namespace TIKSN.Leveret.Interpretation.MondConcretion
{
    public class InterpretationService : IInterpretationService
    {
        private readonly ILogger<InterpretationService> _logger;
        private readonly IMediator _mediator;
        private readonly IMondStateFactory _mondStateFactory;

        public InterpretationService(IMediator mediator, IMondStateFactory mondStateFactory, ILogger<InterpretationService> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mondStateFactory = mondStateFactory ?? throw new ArgumentNullException(nameof(mondStateFactory));
        }

        public async Task<InterpretationResult> InterpretationAsync(string sourceCode, CancellationToken cancellationToken)
        {
            var state = _mondStateFactory.Create();

            try
            {
                var result = state.Run(sourceCode);

                var globalVariable = state.Global;

                var globalVariables = globalVariable.AsDictionary
                    .Where(x => x.Value.Type != MondValueType.Function && x.Value.Type != MondValueType.Object)
                    .ToList();

                var globalVariablesTasks = globalVariables.Select(x => _mediator.Send(new ValueFormatRequest(x.Value), cancellationToken));

                var values = await Task.WhenAll(globalVariablesTasks);

                var variables = globalVariables.Zip(values, (g, v) => new GlobalVariable(g.Key.ToString(), v)).ToImmutableList();

                return InterpretationResult.CreateSuccess(variables);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return InterpretationResult.CreateFailure(ex.Message);
            }
        }
    }
}