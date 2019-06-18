using Dynamic.Shared.DynamicContext.Enums;
using Dynamic.Shared.DynamicContext.Extensions;
using FluentValidator;
using FluentValidator.Validation;

namespace Dynamic.Domain.DynamicContext.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email() { }

        public Email(string address)
        {
            Address = address;

            ValidationContract contract = new ValidationContract();

            contract.IsEmailOrEmpty(Address, $"{this.GetType().Name.Beautify()} {nameof(Address).Beautify()}", ENotifications.InvalidFormat.Description());

            AddNotifications(contract.Notifications);
        }

        public string Address { get; private set; }

        public override string ToString() => Address;
    }
}