using Dynamic.Domain.DynamicContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dynamic.Infra.DynamicContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Username).IsRequired();

            builder.OwnsOne(Partner => Partner.Email, email =>
            {
                email.Property(p => p.Address).HasColumnName("Email").HasMaxLength(100).IsRequired();

                email.Ignore(p => p.Valid);
                email.Ignore(p => p.Invalid);
                email.Ignore(p => p.Notifications);
            });

            builder.OwnsOne(Partner => Partner.Password, password =>
            {
                password.Property(p => p.Text).HasColumnName("Password").HasMaxLength(100).IsRequired();
                password.Property(p => p.Salt).HasColumnName("Salt").HasMaxLength(100).IsRequired();

                password.Ignore(p => p.Valid);
                password.Ignore(p => p.Invalid);
                password.Ignore(p => p.Notifications);
            });
            
            builder.Ignore(user => user.Valid);
            builder.Ignore(user => user.Invalid);
            builder.Ignore(user => user.Notifications);
        }
    }
}