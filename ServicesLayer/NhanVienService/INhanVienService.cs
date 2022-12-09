using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.NhanVienService
{
    public interface INhanVienService
    {
        IEnumerable<NhanVienGetAll> getAllAsync(
            string page, 
            string limit, 
            string search, 
            string key, 
            string options, 
            string chucDanh_id, 
            string chucVu_id, 
            string phongBan_id);

        int getTotalCount(
            string search,
            string chucDanh_id,
            string chucVu_id,
            string phongBan_id
            );
        NhanVien getOne(string id);
        void create(NhanVien t);
        void update(NhanVien t);
        void delete(string id);
    }
}
