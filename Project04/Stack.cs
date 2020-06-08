using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project04
{
    class Stack<T>
    {
        public StackNode<T> First { get; set; } //at the bottom
        public StackNode<T> Last { get; set; } //at the top
        public int Count { get;  set; }

        private T[] StackArray = new T[10];

        public Stack()
        {
            Count = 0;
        }

        public void Push(T data)
        {

            if (Count == 0)
            {
                First = new StackNode<T>(data, null);
                Last = new StackNode<T>(data, First);
            }
            else
            {
                Last = new StackNode<T>(data, Last);
            }
            if (Count < 0)
            {
                throw new Exception("Wrong equation: too many operators");
            }
            if (Count >= 10)
            {
                throw new StackOverflowException();
            }


            StackArray[Count] = data;
            Count++;
        }

        public void Pop()
        {
            Last = Last.Previous;
            Count--;
        }

        public T Peek()
        {
            return Last.Data;
        }

        public bool isEmpty()
        {
            return Count == 0 ? true : false;
        }

        public void Clear()
        {
            First = null;
            Last = null;
        }
    }
}
