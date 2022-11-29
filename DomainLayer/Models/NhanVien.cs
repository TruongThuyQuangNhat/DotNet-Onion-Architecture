using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class NhanVien : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string ChucVu_ID { get; set; }
        public string PhongBan_ID { get; set; }
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

