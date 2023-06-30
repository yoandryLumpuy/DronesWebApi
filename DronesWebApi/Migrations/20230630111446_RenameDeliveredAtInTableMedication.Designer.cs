﻿// <auto-generated />
using System;
using DronesWebApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DronesWebApi.Migrations
{
    [DbContext(typeof(DronesContext))]
    [Migration("20230630111446_RenameDeliveredAtInTableMedication")]
    partial class RenameDeliveredAtInTableMedication
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("DronesWebApi.Core.Domain.Drone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BatteryCapacityInPercentage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeightLimitInGrams")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Drones");
                });

            modelBuilder.Entity("DronesWebApi.Core.Domain.DroneModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CruiserweightInGrams")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HeavyweightInGrams")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LightweightInGrams")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MiddleweightInGrams")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DroneModels");
                });

            modelBuilder.Entity("DronesWebApi.Core.Domain.Medication", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DatetimeDelivery")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DroneId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("WeightInGrams")
                        .HasColumnType("INTEGER");

                    b.HasKey("Code");

                    b.HasIndex("DroneId");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("DronesWebApi.Core.Domain.Drone", b =>
                {
                    b.HasOne("DronesWebApi.Core.Domain.DroneModel", "Model")
                        .WithMany("Drones")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("DronesWebApi.Core.Domain.Medication", b =>
                {
                    b.HasOne("DronesWebApi.Core.Domain.Drone", "Drone")
                        .WithMany("LoadedMedications")
                        .HasForeignKey("DroneId");

                    b.Navigation("Drone");
                });

            modelBuilder.Entity("DronesWebApi.Core.Domain.Drone", b =>
                {
                    b.Navigation("LoadedMedications");
                });

            modelBuilder.Entity("DronesWebApi.Core.Domain.DroneModel", b =>
                {
                    b.Navigation("Drones");
                });
#pragma warning restore 612, 618
        }
    }
}
