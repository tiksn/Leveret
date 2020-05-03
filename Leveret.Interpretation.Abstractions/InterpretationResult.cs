using System;
using System.Collections.Immutable;

namespace TIKSN.Leveret.Interpretation.Abstractions
{
    public class InterpretationResult
    {
        public static InterpretationResult CreateSuccess(ImmutableList<GlobalVariable> globalVariables)
        {
            return new InterpretationResult(globalVariables, succeed: true, message: null);
        }

        public static InterpretationResult CreateFailure(string message)
        {
            return new InterpretationResult(ImmutableList<GlobalVariable>.Empty, succeed: false, message: message);
        }

        private InterpretationResult(ImmutableList<GlobalVariable> globalVariables, bool succeed, string message)
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