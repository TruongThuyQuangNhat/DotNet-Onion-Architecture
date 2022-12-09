using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class PhongBan : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string TenPhongBan { get; set; }

        [Required]
        public string parrent_id { get; set; }


    }
}
