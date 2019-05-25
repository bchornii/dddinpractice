using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DddInPractice.Data.Configurations
{
    public class SnakMachineEntityTypeConfiguration : IEntityTypeConfiguration<SnackMachine>
    {
        public void Configure(EntityTypeBuilder<SnackMachine> builder)
        {
            builder.ToTable("SnakMachine");

            builder.HasKey(o => o.Id);

            builder.Ignore(o => o.DomainEvents);

            builder.Ignore(o => o.MoneyInTransaction);

            builder.OwnsOne(o => o.MoneyInside);

            builder.HasMany(b => b.Slots)
               .WithOne()
               .HasForeignKey("SnackMachineId")
               .OnDelete(DeleteBehavior.Cascade);

            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            var navigation = builder.Metadata
              .FindNavigation(nameof(SnackMachine.Slots));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property<DateTime?>("LastUpdated")
                .IsRequired(false);

            builder.Property<string>("LastUpdatedBy")
                .IsRequired(false);
        }
    }
}
