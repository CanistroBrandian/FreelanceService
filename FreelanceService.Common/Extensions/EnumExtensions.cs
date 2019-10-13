using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FreelanceService.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string DisplayName<T>(this T value) where T : System.Enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            FieldInfo field = value.GetType().GetField(value.ToString());
            DisplayAttribute[] attributes = (DisplayAttribute[])field
                .GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Name;
            }
            return value.ToString();
        }
    }
}
