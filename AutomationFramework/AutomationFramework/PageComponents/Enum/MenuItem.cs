using AutomationFramework.Utilities.Enum.Attributes;

namespace AutomationFramework.PageComponents.Enum
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
