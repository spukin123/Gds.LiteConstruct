using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.Runtime
{
    public static class Guard
    {
        public delegate bool CheckFunction<T>(T arg);

        public static void ThrowIfArgumentNull(object value, string argName)
        {
            if (value == null)
                throw new ArgumentNullException(argName);
        }

        public static void CheckArgumentNotNull<T>(CheckFunction<T> func, T arg, string argName)
        {
            if (func(arg))
                throw new ArgumentNullException(argName);
        }

		public static void CheckArgumentValid<T>(CheckFunction<T> func, T arg, string argName)
		{
			if (!func(arg))
				throw new ArgumentException(string.Format("Argument '{0}' has incorrect value.", argName));
		}

        public static void ArgumentNotNull(object arg, string argName)
        {
            if (arg == null)
				throw new ArgumentNullException(string.Format("Argument '{0}' cannot be null.", argName), argName);
        }

        public static void ArgumentNotNullOrEmptyString(string value, string argName)
        {
            CheckArgumentNotNull<string>(string.IsNullOrEmpty, value, argName);
        }
    }
}
