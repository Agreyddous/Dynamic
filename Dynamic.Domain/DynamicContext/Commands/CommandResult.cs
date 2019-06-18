using System.Collections.Generic;
using System.Net;
using Dynamic.Shared.DynamicContext.Commands;
using FluentValidator;

namespace Dynamic.Domain.DynamicContext.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() : this(HttpStatusCode.InternalServerError) { }

        public CommandResult(HttpStatusCode code) : this(code, new List<Notification>()) { }

        public CommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications)
        {
            Code = code;
            Notifications = notifications;
        }

        public HttpStatusCode Code { get; private set; }

        public IReadOnlyCollection<Notification> Notifications { get; private set; }
    }
}