using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netstats.Network
{
    public static class ReflectionMixins
    {
        public static T GetTypeAttribute<T>(this Type type, Func<T, bool> predicate) where T : class
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            predicate = predicate ?? new Func<T, bool>(x => true);
            return type.GetCustomAttributes(false)
                       .Where(x => x is T && predicate(x as T))
                       .Select(x => x as T)
                       .FirstOrDefault();
        }

        public static bool ImplementsInterface<T>(this Type type)
        {
            return type.GetInterface(typeof(T).Name) != null;
        }

        public static bool TypeHasAttribute<T>(this Type type, Func<T, bool> predicate = null) where T : class
        {
            return GetTypeAttribute<T>(type, predicate) != null;
        }
    }
}
