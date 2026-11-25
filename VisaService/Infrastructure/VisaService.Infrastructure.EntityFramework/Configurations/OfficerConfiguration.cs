using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisaService.Domain.Entities;
using VisaService.ValueObjects;
using VisaService.ValueObjects.Validators;

namespace VisaService.Infrastructure.EntityFramework.Configurations;

public class OfficerConfiguration : IEntityTypeConfiguration<Officer>
{
    public void Configure(EntityTypeBuilder<Officer> builder)
    {
        builder.ToTable("officers");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("officer_id")
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

        builder.Property(x => x.Position)
            .HasColumnName("position")
            .IsRequired()
            .HasConversion(position => position.Value, str => new OfficerPosition(str))
            .HasMaxLength(OfficerPositionValidator.MAX_LENGTH);
    }
}
