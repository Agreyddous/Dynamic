using System.Collections.Generic;
using System.Net;
using FluentValidator;

namespace Dynamic.Shared.DynamicContext.Commands
{
    public interface ICommandResult
    {
        HttpStatusCode Code { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}