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
        public int ChucDanh_ID { get; set; }
    }
}
