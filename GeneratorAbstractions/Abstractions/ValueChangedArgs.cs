using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorSharedComponents.Abstractions
{
    public class ValueChangedArgs : EventArgs
    {
        public ValueChangedArgs(object? value)
        {
            Value = value;
        }

        public object? Value { get; }
    }
}
