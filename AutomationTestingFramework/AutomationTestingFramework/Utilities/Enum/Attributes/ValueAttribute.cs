using System;

namespace AutomationTestingFramework.Utilities.Enum.Attributes
{
    public class ValueAttribute : Attribute
    {
        public string Value { get; }

        public ValueAttribute(string enumValue)
        {
            this.Value = enumValue;
        }
    }
}
