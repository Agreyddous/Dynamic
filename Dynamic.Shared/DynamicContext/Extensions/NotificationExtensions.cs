using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using Dynamic.Shared.DynamicContext.Enums;

namespace Dynamic.Shared.DynamicContext.Extensions
{
    public static class NotificationExtensions
    {
        public static string Beautify(this string propertyName) => Regex.Replace(propertyName, @"(\p{Lu}[^\p{Lu}])", " $1").TrimStart();

        public static string Description(this ENotifications notification)
        {
            FieldInfo info = notification.GetType().GetField(notification.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes != null && attributes.Length > 0 ? attributes[0].Description : notification.ToString();
        }
    }
}