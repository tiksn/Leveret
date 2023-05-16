using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TIKSN.Leveret.Interpretation.MondConcretion.Messages;

namespace TIKSN.Leveret.Interpretation.MondConcretion.Handlers
{
    public class ValueFormatHandler : IRequestHandler<ValueFormatRequest, string>
    {
        public async Task<string> Handle(ValueFormatRequest request, CancellationToken cancellationToken)
        {
            return request.MondValue.ToString();
        }
    }
}