using DomainLayer.EntityMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NhanVienMap());
            modelBuilder.ApplyConfiguration(new ChucDanhMap());
            modelBuilder.ApplyConfiguration(new ChucVuMap());
            modelBuilder.ApplyConfiguration(new PhongBanMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
