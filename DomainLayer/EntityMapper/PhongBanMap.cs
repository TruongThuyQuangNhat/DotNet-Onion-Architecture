using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityMapper
{
    public class PhongBanMap : IEntityTypeConfiguration<PhongBan>
    {
        public void Configure(EntityTypeBuilder<PhongBan> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_phongbanid");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                   .HasColumnType("VARCHAR(100)");
            builder.Property(x => x.TenPhongBan)
                .HasColumnName("TenPhongBan")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired();
            builder.Property(x => x.CreatedDate)
              .HasColumnName("CreatedDate")
                 .HasColumnType("TIMESTAMP");
            builder.Property(x => x.ModifiedDate)
              .HasColumnName("ModifiedDate")
                 .HasColumnType("TIMESTAMP");
            builder.Property(x => x.IsActive)
              .HasColumnName("IsActive")
                 .HasColumnType("BOOLEAN");
        }
    }
}
