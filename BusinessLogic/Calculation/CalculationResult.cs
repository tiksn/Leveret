using System;
using System.Collections.Immutable;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public sealed class CalculationResult
    {
        public static CalculationResult CreateSuccess(ImmutableList<GlobalVariable> globalVariables)
        {
            return new CalculationResult(globalVariables, succeed: true, message: null);
        }

        public static CalculationResult CreateFailure(string message)
        {
            return new CalculationResult(ImmutableList<GlobalVariable>.Empty, succeed: false, message: message);
        }

        private CalculationResult(ImmutableList<GlobalVariable> globalVariables, bool succeed, string message)
        {
            GlobalVariables = globalVariables ?? throw new ArgumentNullException(nameof(globalVariables));
            Succeed = succeed;
            Message = message;
        }

        public ImmutableList<GlobalVariable> GlobalVariables { get; }
        public bool Succeed { get; }
        public string Message { get; }
    }
}