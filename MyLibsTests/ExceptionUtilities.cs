using System;

namespace MyLibs.Tests
{
    public static class ExceptionUtilities
    {
        public static bool AreExceptionsEqual(Action expected, Action actual)
        {
            return ExceptionEquals(GetException(expected), GetException(actual));
        }

        private static Exception GetException(Action action)
        {
            try
            {
                action();
            }
            #pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return ex;
            }
            #pragma warning restore CA1031 // Do not catch general exception types

            return null;
        }

        private  static bool ExceptionEquals(Exception a, Exception b)
        {
            return a.GetType() == b.GetType() && a.HResult == b.HResult && a.Message == b.Message;
        }
    }
}
