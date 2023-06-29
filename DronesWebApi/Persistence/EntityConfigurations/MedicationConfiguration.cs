﻿using DronesWebApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesWebApi.Persistence.EntityConfigurations
{
    public class MedicationConfiguration: IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasKey(m => m.Code);
            builder.Property(m => m.Code).HasMaxLength(100).IsRequired();

            builder.Property(m => m.Name).HasMaxLength(100).IsRequired();

            builder.HasOne(m => m.Drone)
                .WithMany(m => m.LoadedMedications)
                .IsRequired(false);
        }
    }
}
