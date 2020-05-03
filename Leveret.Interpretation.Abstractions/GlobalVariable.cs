using System;

namespace TIKSN.Leveret.Interpretation.Abstractions
{
    public sealed class GlobalVariable
    {
        public GlobalVariable(string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}