using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class NhanVien : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public string Image { get; set; }

        [Required]
        public string ChucVu_ID { get; set; }

        [Required]
        public string PhongBan_ID { get; set; }

        [Required]
        public string ChucDanh_ID { get; set; }
    }

    public class NhanVienGetAll
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string TenChucVu { get; set; }
        public string TenChucDanh { get; set; }
        public string TenPhongBan { get; set; }
    }
}

