using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    [Serializable]
    public class tempModel: PhongBan
    {
        public PhongBan PhongBan { get; set; }
        public List<PhongBan>? listPhongBan { get; set; }

        public tempModel()
        {
            listPhongBan = new List<PhongBan>();
        }


    }
}
