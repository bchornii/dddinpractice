using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DddInPractice.Data.Configurations
{
    public class SlotEntityTypeConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable("SnakMachineSlot");

            builder.HasKey(o => o.Id);

            builder.Ignore(o => o.DomainEvents);            

            builder.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("slotseq");

            builder.Property<long>("SnackMachineId")
                .IsRequired();

            builder.Property<int>("Position")
                .HasField("_position")
                .IsRequired();

            builder.Property<int>("ItemsQuantity")
                .HasField("_itemsQuantity")
                .IsRequired();

            builder.Property<decimal>("ItemPrice")
                .HasField("_itemPrice")
                .IsRequired();

            builder.HasOne(o => o.SnakType)
                .WithMany()
                .HasForeignKey("SnakTypeId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property<DateTime?>("LastUpdated")
                .IsRequired(false);

            builder.Property<string>("LastUpdatedBy")
                .IsRequired(false);
        }
    }
}
