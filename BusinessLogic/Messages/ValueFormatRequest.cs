using MediatR;
using Mond;

namespace TIKSN.Leveret.BusinessLogic.Messages
{
    public class ValueFormatRequest : IRequest<string>
    {
        public ValueFormatRequest(MondValue mondValue)
        {
            MondValue = mondValue;
        }

        public MondValue MondValue { get; }
    }
}