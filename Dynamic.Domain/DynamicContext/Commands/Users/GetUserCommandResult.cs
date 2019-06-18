using System.Collections.Generic;
using System.Net;
using FluentValidator;

namespace Dynamic.Domain.DynamicContext.Commands.Users
{
    public class GetUserCommandResult : CommandResult
    {
        public GetUserCommandResult() : base() { }
        public GetUserCommandResult(HttpStatusCode code) : base(code)  { }
        public GetUserCommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications) : base(code, notifications)  { }

        public string Username { get; set; }
        public string Email { get; set; }
    }
}