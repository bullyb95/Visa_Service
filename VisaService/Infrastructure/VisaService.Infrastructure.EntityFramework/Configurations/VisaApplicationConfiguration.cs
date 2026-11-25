using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisaService.Domain.Entities;
using VisaService.ValueObjects;
using VisaService.ValueObjects.Validators;

namespace VisaService.Infrastructure.EntityFramework.Configurations;

public class VisaApplicationConfiguration : IEntityTypeConfiguration<VisaApplication>
{
    public void Configure(EntityTypeBuilder<VisaApplication> builder)
    {
        builder.ToTable("visa_application");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("application_id")
            .IsRequired();

        builder.Property(x => x.Number)
            .HasColumnName("number")
            .IsRequired()
            .HasConversion(number => number.Value, str => new ApplicationNumber(str))
            .HasMaxLength(ApplicationNumberValidator.MAX_LENGTH);
        builder.HasIndex(x => x.Number).IsUnique();

        builder.Property(x => x.FormFilled)
            .HasColumnName("form_filled")
            .IsRequired();

        builder.Property(x => x.DocumentsChecked)
            .HasColumnName("documents_checked")
            .IsRequired();

        builder.Property(x => x.ResultChecked)
            .HasColumnName("result_checked")
            .IsRequired();

        builder.Property(x => x.IsVisaIssued)
            .HasColumnName("is_visa_issued")
            .IsRequired(false);

        // Связь с Applicant настроена в ApplicantConfiguration (через поле _applications).

        builder.HasOne(x => x.AssignedOfficer)
            .WithMany()
            .HasForeignKey("officer_id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
