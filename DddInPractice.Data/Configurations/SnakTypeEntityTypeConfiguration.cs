using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DddInPractice.Data.Configurations
{
    public class SnakTypeEntityTypeConfiguration : IEntityTypeConfiguration<SnakType>
    {
        public void Configure(EntityTypeBuilder<SnakType> builder)
        {
            builder.ToTable("SnakType");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property<DateTime?>("LastUpdated")
                .IsRequired(false);

            builder.Property<string>("LastUpdatedBy")
                .IsRequired(false);
        }
    }
}
