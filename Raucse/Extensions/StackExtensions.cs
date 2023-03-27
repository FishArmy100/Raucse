using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class StackExtensions
    {
        public static T PeekUpdate<T>(this Stack<T> stack, T newValue)
        {
            T old = stack.Pop();
            stack.Push(newValue);
            return old;
        }

        public static T PeekUpdate<T>(this Stack<T> stack, Func<T, T> updator)
        {
            T updated = updator(stack.Peek());
            return PeekUpdate(stack, updated);
        }

        public static void PushDefault<T>(this Stack<T> stack) where T : new()
        {
            stack.Push(new T());
        }

        public static void PushDefault(this Stack<string> stack)
        {
            stack.Push(string.Empty);
        }
    }
}
