using System;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Base
{
    public class xUnitTestBase : UnitTestBase
    {
        private readonly ITestOutputHelper _output;

        public xUnitTestBase(ITestOutputHelper output)
        {
            _output = output;
        }

        protected override void Output(string s)
        {
            _output.WriteLine(s);
        }

        protected override void Output(object o)
        {
            _output.WriteLine(SerializeToString(o));
        }

        public static void NotThrows(Action a)
        {
            NotThrows<Exception>(a);
        }

        public static void NotThrows<TException>(Action a) where TException : Exception
        {
            string message = null;

            try
            {
                a.Invoke();
            }
            catch (TException e)
            {
                message = e.Message;
            }

            Assert.Null(message);
        }
    }
}
