using System;
using System.Collections.Immutable;

namespace TIKSN.Leveret.BusinessLogic.Calculation
{
    public sealed class CalculationResult
    {
        public static CalculationResult CreateSuccess(ImmutableList<GlobalVariable> globalVariables)
        {
            return new CalculationResult(globalVariables, succeed: true);
        }

        public static CalculationResult CreateFailure()
        {
            return new CalculationResult(ImmutableList<GlobalVariable>.Empty, succeed: false);
        }


        private CalculationResult(ImmutableList<GlobalVariable> globalVariables, bool succeed)
        {
            GlobalVariables = globalVariables ?? throw new ArgumentNullException(nameof(globalVariables));
            Succeed = succeed;
        }

        public ImmutableList<GlobalVariable> GlobalVariables { get; }
        public bool Succeed { get; }
    }
}
