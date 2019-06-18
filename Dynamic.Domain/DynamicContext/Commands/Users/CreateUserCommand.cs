using Dynamic.Shared.DynamicContext.Commands;

namespace Dynamic.Domain.DynamicContext.Commands.Users
{
    public class CreateUserCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}