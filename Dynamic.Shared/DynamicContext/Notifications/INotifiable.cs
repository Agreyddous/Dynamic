using System.Collections.Generic;
using FluentValidator;

namespace Dynamic.Shared.DynamicContext.Notifications
{
    public interface INotifiable
    {
        bool Valid { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}