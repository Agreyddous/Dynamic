using Dynamic.Domain.DynamicContext.ValueObjects;
using Dynamic.Shared.DynamicContext.Entities;
using Dynamic.Shared.DynamicContext.Enums;
using Dynamic.Shared.DynamicContext.Extensions;
using FluentValidator.Validation;

namespace Dynamic.Domain.DynamicContext.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string username, Email email, Password password) => Update(username, email, password);

        public string Username { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }

        public void Update(string username, Email email, Password password)
        {
            Username = username;
            Email = email;
            Password = password;

            ValidationContract contract = new ValidationContract();

            contract.IsNullOrEmpty(Username, nameof(Username).Beautify(), ENotifications.NullOrEmpty.Description())
                .HasMinLen(Username, 3, nameof(Username).Beautify(), ENotifications.ShortLength.Description())
                .HasMaxLen(Username, 50, nameof(Username).Beautify(), ENotifications.LongLength.Description());

            AddNotifications(contract.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Password.Notifications);
        }
    }
}