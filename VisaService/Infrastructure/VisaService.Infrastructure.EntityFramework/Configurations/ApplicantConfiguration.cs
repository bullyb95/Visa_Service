using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisaService.Domain.Entities;
using VisaService.ValueObjects;
using VisaService.ValueObjects.Validators;

namespace VisaService.Infrastructure.EntityFramework.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("applicants");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("applicant_id")
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnName("last_name")
            .IsRequired()
            .HasConversion(lastName => lastName.Value, str => new LastName(str))
            .HasMaxLength(PersonNameValidator.MAX_LENGTH);

        builder.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasConversion(firstName => firstName.Value, str => new FirstName(str))
            .HasMaxLength(PersonNameValidator.MAX_LENGTH);

        builder.Property(x => x.MiddleName)
            .HasColumnName("middle_name")
            .IsRequired()
            .HasConversion(middleName => middleName.Value, str => new MiddleName(str))
            .HasMaxLength(PersonNameValidator.MAX_LENGTH);

        // ContactInfo — составной Value Object: хранится в той же таблице двумя колонками.
        builder.OwnsOne(x => x.ContactInfo, contactInfo =>
        {
            contactInfo.Property(c => c.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasConversion(email => email.Value, str => new Email(str))
                .HasMaxLength(EmailValidator.MAX_LENGTH);

            contactInfo.Property(c => c.Phone)
                .HasColumnName("phone")
                .IsRequired()
                .HasConversion(phone => phone.Value, str => new Phone(str))
                .HasMaxLength(PhoneValidator.MAX_LENGTH);
        });
        builder.Navigation(x => x.ContactInfo).IsRequired();

        builder.HasMany<VisaApplication>("_applications")
            .WithOne(x => x.Applicant)
            .HasForeignKey("applicant_id")
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Ignore(x => x.Applications);
    }
}
