using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.Runtime
{
    public class AppContext
    {
		protected AppContext()
		{
		}

        protected static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static T Get<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException(string.Format("Service '{0}' is not registered",
                    typeof(T).Name));
            }
        }

        public static void Set<T>(T service)
        {
            services[typeof(T)] = service;
        }

        protected static readonly Dictionary<string, object> namedServices = new Dictionary<string, object>();

        public static object Get(string name)
        {
            try
            {
                return namedServices[name];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException(string.Format("Service with name '{0}' is not registered",
                    name));
            }
        }

        public static void Set(string name, object service)
        {
            namedServices[name] = service;
        }
    }
}
