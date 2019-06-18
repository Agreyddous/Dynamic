using System;
using System.Collections.Generic;
using System.Net;
using FluentValidator;

namespace Dynamic.Domain.DynamicContext.Commands.Users
{
    public class CreateUserCommandResult : CommandResult
    {
        public CreateUserCommandResult() : base() { }
        public CreateUserCommandResult(HttpStatusCode code) : base(code)  { }
        public CreateUserCommandResult(HttpStatusCode code, IReadOnlyCollection<Notification> notifications) : base(code, notifications)  { }

        public Guid Id { get; set; }
    }
}