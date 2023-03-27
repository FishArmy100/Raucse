using System;
using Raucse;
using Raucse.FileManagement;

namespace RaucseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            StringMaker maker = new StringMaker();
            maker.AppendLine("Here is my List:");
            maker.TabIn(StringMaker.TabMode.Numbered);
            maker.AppendLines("Pears", "Apples", "Carrots");
            maker.Append("Peas");
            maker.AppendLine(" with Butter");

            ConsoleHelper.WriteMessage(maker.ToString());
            ConsoleHelper.WriteLine(maker.LineCount.ToString());
        }
    }
}
