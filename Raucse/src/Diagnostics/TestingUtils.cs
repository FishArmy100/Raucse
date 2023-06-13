using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raucse.Diagnostics
{
    public static class TestingUtils
    {
        public enum TestResult
        {
            Failed,
            Succeeded,
            ThrewException
        }

        /// <summary>
        /// Will attempt to run a function, and will give that information to the logger about if the test failed or succeeded
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="test"></param>
        /// <param name="logger"></param>
        public static void Test(string testName, Func<bool> test, Action<string, TestResult> logger)
        {
            var (time, succeeded) = Timer.Time(test);
            if (succeeded)
            {
                logger($"Test: '{testName}' has succeeded in {time.Milliseconds}ms", TestResult.Succeeded);
            }
            else
            {
                logger($"Test: '{testName}' has failed", TestResult.Failed);
            }
        }

        /// <summary>
        /// Will attempt to run a function, and will display information to the console about if the test failed or succeeded
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="test"></param>
        public static void Test(string testName, Func<bool> test)
        {
            Test(testName, test, DefaultMessageLogging);
        }

        /// <summary>
        /// Will attempt to run a function, and will display information to the console about if the test failed, succeeded, or threw an exception
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="test"></param>
        public static void TestCatchExceptions(string testName, Func<bool> test)
        {
            TestCatchExceptions(testName, test, DefaultMessageLogging);
        }

        /// <summary>
        /// Will attempt to run a function, and will give information to the logger about if the test failed, succeeded, or threw an exception
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="test"></param>
        /// <param name="logger"></param>
        public static void TestCatchExceptions(string testName, Func<bool> test, Action<string, TestResult> logger)
        {
            try
            {
                Test(testName, test, logger);
            }
            catch (Exception e)
            {
                logger($"Test: '{testName}' has thrown an exception {e}", TestResult.ThrewException);
            }
        }

        private static void DefaultMessageLogging(string message, TestResult result)
        {
            switch (result)
            {
                case TestResult.Failed:
                    ConsoleHelper.WriteError(message);
                    break;
                case TestResult.Succeeded:
                    ConsoleHelper.WriteMessage(message);
                    break;
                case TestResult.ThrewException:
                    ConsoleHelper.WriteError(message);
                    break;
                default:
                    Debug.Panic("This should never be called");
                    break;
            }
        }
    }
}
