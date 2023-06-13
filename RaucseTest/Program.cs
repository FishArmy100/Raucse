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
            TestingUtils.Test("Is true true", () => true == true);
            TestingUtils.Test("Is false false", () => false == false);
            TestingUtils.Test("Failed test test", () => true == false);
        }
    }
}
