using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Raucse;

namespace RaucseTest
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void TestMethod()
		{
			Assert.IsTrue(Test.False(), "I wanted to make sure that this is false.");
		}
	}
}
