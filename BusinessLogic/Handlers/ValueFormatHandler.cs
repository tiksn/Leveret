using MediatR;
using TIKSN.Leveret.BusinessLogic.Messages;

namespace TIKSN.Leveret.BusinessLogic.Handlers
{
    public class ValueFormatHandler : RequestHandler<ValueFormatRequest, string>
    {
        protected override string Handle(ValueFormatRequest request)
        {
            return request.MondValue.ToString();
        }
    }
}