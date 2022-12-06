using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ChucDanh : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string TenChucDanh { get; set; }

    }
}
