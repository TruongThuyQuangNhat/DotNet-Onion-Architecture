using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.ChucDanhService
{
    public interface IChucDanhService
    {
        IEnumerable<ChucDanh> getAllAsync(string page, string limit, string search);
        ChucDanh getOne(string id);
        void create(ChucDanh chucDanh);
        void update(ChucDanh chucDanh);
        void delete(string id);
    }
}
