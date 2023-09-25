using AutomationTestingFramework.Utilities.Enum.Attributes;

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
