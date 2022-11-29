using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.ChucVuService
{
    public interface IChucVuService
    {
        IEnumerable<ChucVu> getAllAsync(string page, string limit, string search);
        ChucVu getOne(string id);
        void create(ChucVu ChucVu);
        void update(ChucVu ChucVu);
        void delete(string id);
    }
}
