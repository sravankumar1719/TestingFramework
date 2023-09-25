using AutomationTestingFramework.Utilities.Enum.Attributes;

namespace AutomationTestingFramework.PageComponents.Enum
{
    public enum MenuItem
    {
        [Value("About")]
        About,

        [Value("All Items")]
        AllItems,

        [Value("Logout")]
        Logout,

        [Value("Reset App State")]
        ResetAppState
    }
}
