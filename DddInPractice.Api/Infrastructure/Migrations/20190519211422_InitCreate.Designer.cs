﻿// <auto-generated />
using System;
using DddInPractice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DddInPractice.Api.Infrastructure.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20190519211422_InitCreate")]
    partial class InitCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.slotseq", "'slotseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DddInPractice.Domain.Aggregates.SnakMachineAggr.Slot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "slotseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<decimal>("ItemPrice");

                    b.Property<int>("ItemsQuantity");

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<int>("Position");

                    b.Property<long>("SnackMachineId");

                    b.Property<int?>("SnakTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SnackMachineId");

                    b.HasIndex("SnakTypeId");

                    b.ToTable("SnakMachineSlot");
                });

            modelBuilder.Entity("DddInPractice.Domain.Aggregates.SnakMachineAggr.SnackMachine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("LastUpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("SnakMachine");
                });

            modelBuilder.Entity("DddInPractice.Domain.Aggregates.SnakMachineAggr.SnakType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("SnakType");
                });

            modelBuilder.Entity("DddInPractice.Domain.Aggregates.SnakMachineAggr.Slot", b =>
                {
                    b.HasOne("DddInPractice.Domain.Aggregates.SnakMachineAggr.SnackMachine")
                        .WithMany("Slots")
                        .HasForeignKey("SnackMachineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DddInPractice.Domain.Aggregates.SnakMachineAggr.SnakType", "SnakType")
                        .WithMany()
                        .HasForeignKey("SnakTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DddInPractice.Domain.Aggregates.SnakMachineAggr.SnackMachine", b =>
                {
                    b.OwnsOne("DddInPractice.Domain.Aggregates.SnakMachineAggr.Money", "MoneyInside", b1 =>
                        {
                            b1.Property<long>("SnackMachineId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("FiveDollarCount");

                            b1.Property<int>("OneCentCount");

                            b1.Property<int>("OneDollarCount");

                            b1.Property<int>("QuarterCount");

                            b1.Property<int>("TenCentCount");

                            b1.HasKey("SnackMachineId");

                            b1.ToTable("SnakMachine");

                            b1.HasOne("DddInPractice.Domain.Aggregates.SnakMachineAggr.SnackMachine")
                                .WithOne("MoneyInside")
                                .HasForeignKey("DddInPractice.Domain.Aggregates.SnakMachineAggr.Money", "SnackMachineId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}