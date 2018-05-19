using System;

namespace PrivateForum.Core.Utilities
{
    public static class ReflectionExtensions
    {
        public static object GetPropertyValue(this object source, string propertyName)
        {
            return source.GetType().GetProperty(propertyName).GetValue(source);
        }

        public static T GetPropertyValue<T>(this object source, string propertyName)
        {
            return (T)source.GetType().GetProperty(propertyName).GetValue(source);
        }
    }
}
