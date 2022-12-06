using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ChucVu : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string TenChucVu { get; set; }

    }
}
