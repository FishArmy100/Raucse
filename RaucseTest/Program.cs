using System;
using Raucse;
using Raucse.Extensions;
using Raucse.Extensions.Nullables;
using Raucse.FileManagement;
using System.Collections.Generic;
using System.Linq;
using Raucse.Strings;
using Raucse.Diagnostics;

namespace RaucseTest
{
    class Program
    {

        static void Main(string[] args)
        {
            string str = "hello there, I am Nate";
            StringReader reader = new StringReader(str);
            TestingUtils.TestCatchExceptions("First", () =>
            {
                while (!reader.IsAtEnd())
                    reader.Advance();
                //ConsoleHelper.WriteMessage(reader.Previous().ToString());
                return true;
            });
        }
    }
}
