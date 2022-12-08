using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.PhongBanService
{
    public interface IPhongBanService
    {
        List<tempModel> getAllAsync();
        public List<PhongBan> GetChildren(string parrent_id);
        PhongBan getOne(string id);
        void create(PhongBan phongban);
        void update(PhongBan phongban);
        void delete(string id);
    }
}
