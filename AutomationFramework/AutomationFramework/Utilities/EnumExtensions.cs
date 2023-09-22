using AutomationFramework.Utilities.Enum.Attributes;

namespace AutomationFramework.Utilities
{
    public static class EnumExtensions
    {
        public static string GetEnumValue(this System.Enum value)
        {
            return value.GetType().GetField(value.ToString()).GetCustomAttributes(false).OfType<ValueAttribute>().FirstOrDefault().Value;
        }
    }
}
