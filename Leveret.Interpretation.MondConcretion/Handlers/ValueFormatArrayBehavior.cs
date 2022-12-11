using MediatR;
using Mond;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TIKSN.Leveret.Interpretation.MondConcretion.Messages;

namespace TIKSN.Leveret.Interpretation.MondConcretion.Handlers
{
    public class ValueFormatArrayBehavior : IPipelineBehavior<ValueFormatRequest, string>
    {
        private readonly IMediator _mediator;

        public ValueFormatArrayBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<string> Handle(ValueFormatRequest request, RequestHandlerDelegate<string> next, CancellationToken cancellationToken)
        {
            if (request.MondValue.Type == MondValueType.Array)
            {
                var itemTasks = request.MondValue.AsList.Select(x => _mediator.Send(new ValueFormatRequest(x), cancellationToken));

                var itemValues = await Task.WhenAll(itemTasks);

                var arrayString = string.Join(", ", itemValues);

                return $"[{arrayString}]";
            }

            return await next();
        }
    }
}