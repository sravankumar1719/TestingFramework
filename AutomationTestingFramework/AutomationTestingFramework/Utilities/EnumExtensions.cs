using AutomationTestingFramework.Utilities.Enum.Attributes;
using System.Linq;

namespace AutomationTestingFramework.Utilities
{
    public static class EnumExtensions
    {
        public static string GetEnumValue(this System.Enum value)
        {
            return value.GetType().GetField(value.ToString()).GetCustomAttributes(false).OfType<ValueAttribute>().FirstOrDefault().Value;
        }
    }
}