using System;
using Raucse;
using Raucse.Extensions;
using Raucse.Extensions.Nullables;
using Raucse.FileManagement;
using System.Collections.Generic;
using System.Linq;
using Raucse.Strings;

namespace RaucseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StringMaker maker = new StringMaker();
            maker.AppendLine("Test List:");
            maker.TabIn();
            maker.TabIn(TabModes.Bulleted);
            maker.AppendLine("Test double tab");
            maker.TabOut();
            maker.TabOut();



            ConsoleHelper.WriteMessage(maker.ToString());
        }
    }
}
