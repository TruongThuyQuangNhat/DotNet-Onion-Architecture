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
    public class NhanVienMap : IEntityTypeConfiguration<NhanVien>
    {
        public void Configure(EntityTypeBuilder<NhanVien> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_nhanvienid");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired(); 
            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired();
            builder.Property(x => x.LastName)
              .HasColumnName("LastName")
                 .HasColumnType("VARCHAR(100)")
                 .IsRequired();
            builder.Property(x => x.Image)
                .HasColumnName("Image")
                   .HasColumnType("VARCHAR(100)");
                   
            builder.Property(x => x.ChucDanh_ID)
              .HasColumnName("ChucDanh_ID")
                 .HasColumnType("VARCHAR(100)")
                 .IsRequired();
            builder.Property(x => x.ChucVu_ID)
                .HasColumnName("ChucVu_ID")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired();
            builder.Property(x => x.PhongBan_ID)
              .HasColumnName("PhongBan_ID")
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
