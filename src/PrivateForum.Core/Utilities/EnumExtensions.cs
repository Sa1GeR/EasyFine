using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{
    public static class EnumExtensions
    {
        public static string GetIdentityCode(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(IdentityCodeAttribute)) as IdentityCodeAttribute;

            return attribute == null ? string.Empty : attribute.IdentityCode;
        }

        public static string GetDescription(this Enum value)
        {

            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? string.Empty : attribute.Description;

        }

        public static int GetDisplayOrder(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = field != null ? Attribute.GetCustomAttribute(field, typeof(DisplayOrderAttribute)) as DisplayOrderAttribute : null;

            return attribute == null ? 0 : attribute.DisplayOrder;
        }

    }

    public static class EnumHelper
    {
        public static char NameOrder(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? 'Z' : attribute.Description.First();
        }

        private static int GetOrder(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DisplayOrderAttribute)) as DisplayOrderAttribute;

            return attribute == null ? int.MaxValue : attribute.DisplayOrder;
        }

        public static IEnumerable<KeyValuePair<int, string>> List<T>(bool nameOrder = true)
        {
            var list = new List<KeyValuePair<int, string>>();
            var fields = Enum.GetValues(typeof(T));

            foreach (Enum field in fields.Cast<Enum>().OrderBy(p => nameOrder ? NameOrder(p) : GetOrder(p)))
            {
                var descriptionAttribute = GetAttribute<DescriptionAttribute>(field);
                var identityAttribute = GetAttribute<IdentityCodeAttribute>(field);

                list.Add(new KeyValuePair<int, string>(Convert.ToInt16(field), descriptionAttribute.Description));
            }

            return list.ToList();
        }

        public static IEnumerable<KeyValuePair<string, string>> StringList<T>(bool nameOrder = true)
        {
            var list = new List<KeyValuePair<string, string>>();
            var fields = Enum.GetValues(typeof(T));

            foreach (Enum field in fields.Cast<Enum>().OrderBy(p => nameOrder ? NameOrder(p) : GetOrder(p)))
            {
                var descriptionAttribute = GetAttribute<DescriptionAttribute>(field);

                list.Add(new KeyValuePair<string, string>(field.ToString(), descriptionAttribute.Description));
            }

            return list;
        }

        public static IEnumerable<KeyValuePair<int, string>> BasicList<T>()
        {
            var list = new List<KeyValuePair<int, string>>();

            foreach (var field in Enum.GetValues(typeof(T)))
            {
                list.Add(new KeyValuePair<int, string>(Convert.ToInt16(field), field.ToString()));
            }

            return list;
        }

        public static T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            T attribute;

            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString())
                                            .FirstOrDefault();

            if (memberInfo != null)
            {
                attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                return attribute;
            }
            return null;
        }
    }


}
