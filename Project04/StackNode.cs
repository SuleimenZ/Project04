using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project04
{
    class StackNode<T>
    {
        public T Data { get; set; }
        public StackNode<T> Previous { get; set; }
        public StackNode(T data, StackNode<T> previous)
        {
            Data = data;
            Previous = previous;
        }
    }
}
