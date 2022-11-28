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
    public class ChucDanhMap : IEntityTypeConfiguration<ChucDanh>
    {
        public void Configure(EntityTypeBuilder<ChucDanh> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_chucdanhid");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                   .HasColumnType("VARCHAR(100)");
            builder.Property(x => x.TenChucDanh)
                .HasColumnName("TenChucDanh")
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
