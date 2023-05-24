using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyme_Engine
{
    internal class NullableWrapper<T> where T : class
    {
        public T? Value { get; private set; }

        public NullableWrapper(T initial)
        {
            this.Value = initial;
        }

        public void Invalidate()
        {
            this.Value = null;
        }

        public bool IsValid()
        {
            return this.Value != null;
        }
    }
}
