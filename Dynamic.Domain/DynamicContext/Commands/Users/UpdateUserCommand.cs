using System;
using Dynamic.Shared.DynamicContext.Commands;

namespace Dynamic.Domain.DynamicContext.Commands.Users
{
    public class UpdateUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Guid Id { get; private set; }

        public void setId(Guid id) => Id = id;
    }
}