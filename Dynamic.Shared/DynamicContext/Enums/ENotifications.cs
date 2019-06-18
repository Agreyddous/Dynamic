using System.ComponentModel;

namespace Dynamic.Shared.DynamicContext.Enums
{
    public enum ENotifications
    {
        [Description("Not Found")] NotFound,
        [Description("Internal Error")] InternalError,
        [Description("Id value isn't valid")] InvalidId,
        [Description("Value is null")] Null,
        [Description("Value is null or empty")] NullOrEmpty,
        [Description("Value is too small")] SmallNumber,
        [Description("Value is too big")] BigNumber,
        [Description("Format is invalid")] InvalidFormat,
        [Description("Length is too long")] LongLength,
        [Description("Length is too short")] ShortLength,
        [Description("Already in use")] AlreadyInUse
    }
}