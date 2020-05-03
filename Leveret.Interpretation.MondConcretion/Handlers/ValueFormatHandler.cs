using MediatR;
using TIKSN.Leveret.Interpretation.MondConcretion.Messages;

namespace TIKSN.Leveret.Interpretation.MondConcretion.Handlers
{
    public class ValueFormatHandler : RequestHandler<ValueFormatRequest, string>
    {
        protected override string Handle(ValueFormatRequest request)
        {
            return request.MondValue.ToString();
        }
    }
}