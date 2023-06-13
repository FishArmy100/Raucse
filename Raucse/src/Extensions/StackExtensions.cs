using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Extensions
{
    public static class StackExtensions
    {
        /// <summary>
        /// Will set the top value to a new one, and return the old value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static T PeekUpdate<T>(this Stack<T> stack, T newValue)
        {
            T old = stack.Pop();
            stack.Push(newValue);
            return old;
        }

        /// <summary>
        /// Will visit the top value, and set it to the one return by the updator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <param name="updator"></param>
        /// <returns>Returns the old value</returns>
        public static T PeekUpdate<T>(this Stack<T> stack, Func<T, T> updator)
        {
            T updated = updator(stack.Peek());
            return PeekUpdate(stack, updated);
        }

        /// <summary>
        /// Pushes the default value to the top of the stack
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        public static void PushDefault<T>(this Stack<T> stack) where T : new()
        {
            stack.Push(new T());
        }

        /// <summary>
        /// Pushes an empty string to the top of the stack
        /// </summary>
        /// <param name="stack"></param>
        public static void PushDefault(this Stack<string> stack)
        {
            stack.Push(string.Empty);
        }
    }
}
