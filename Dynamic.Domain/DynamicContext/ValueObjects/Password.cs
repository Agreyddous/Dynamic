using System;
using System.Security.Cryptography;
using System.Text;
using Dynamic.Shared.DynamicContext.Enums;
using Dynamic.Shared.DynamicContext.Extensions;
using FluentValidator;
using FluentValidator.Validation;

namespace Dynamic.Domain.DynamicContext.ValueObjects
{
    public class Password : Notifiable
    {
        protected Password() { }

        public Password(string text)
        {
            text = text.Trim();


            if (text == null || text.Trim() == string.Empty)
                AddNotification($"{this.GetType().Name.Beautify()} {nameof(text).Beautify()}", ENotifications.InvalidFormat.Description());

            if (Valid)
            {
                ValidationContract contract = new ValidationContract();

                contract.HasMinLen(text, 5, $"{this.GetType().Name.Beautify()} {nameof(text).Beautify()}", ENotifications.InvalidFormat.Description())
                        .HasMaxLen(text, 50, $"{this.GetType().Name.Beautify()} {nameof(text).Beautify()}", ENotifications.InvalidFormat.Description());

                AddNotifications(contract.Notifications);

                if (Valid)
                {
                    Salt = GenerateSalt();
                    Text = Hash(text, Salt);
                }
            }
        }

        public string Text { get; private set; }
        public string Salt { get; private set; }

        public bool Validate(string password) => Text == Hash(password, Salt);

        private string Hash(string text, string salt)
        {
            Rfc2898DeriveBytes Rfc2898DeriveBytes = new Rfc2898DeriveBytes(text, Encoding.UTF8.GetBytes(salt), 1000);

            return BitConverter.ToString(Rfc2898DeriveBytes.GetBytes(20));
        }

        private string GenerateSalt()
        {
            string salt = string.Empty;

            byte[] bytes = new byte[128 / 8];
            using (RandomNumberGenerator keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                salt = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }

            return salt;
        }
    }
}