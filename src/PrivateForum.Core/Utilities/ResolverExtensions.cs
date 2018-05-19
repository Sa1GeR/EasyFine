using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac;

namespace PrivateForum.Core.Utilities
{
    public static class ResolverExtensions
    {
        public static T TypedOrDerivedAs<T>(this IEnumerable<Parameter> parameters) where T : class
        {
            var lookupParameter = parameters.FirstOrDefault(parameter => (parameter as TypedParameter)?.Value is T);

            if (lookupParameter != null)
                return (lookupParameter as TypedParameter)?.Value as T;

            return null;
        }
    }
}
