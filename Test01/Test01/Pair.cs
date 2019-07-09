using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Test01
{
    class Pair<T>
    {
        private T first;
        private T second;

        public T getFirst()
        {
            return first;
        }

        public Pair<T> setFirst(T first)
        {
            this.first = first;
            return this;
        }

        public T getSecond()
        {
            return second;
        }

        public Pair<T> setSecond(T second)
        {
            this.second = second;
            return this;
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder("Pair{");
            sb.Append("frist=").Append(first);
            sb.Append(", second=").Append(second);
            sb.Append('}');
            return sb.ToString();
        }
    }
}

