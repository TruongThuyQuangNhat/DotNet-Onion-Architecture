using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.PhongBanService
{
    public class PhongBanService : IPhongBanService
    {
        private IRepository<PhongBan> _repository;

        public PhongBanService(IRepository<PhongBan> repository)
        {
            _repository = repository;
        }
        public void create(PhongBan phongban)
        {
            _repository.Insert(phongban);
        }

        public void delete(string id)
        {
            PhongBan temp = getOne(id);
            _repository.Remove(temp);
            _repository.SaveChanges();
        }

        public IEnumerable<PhongBan> getAllAsync(string parrent_id)
        {
            if(parrent_id == null)
            {
                parrent_id = "0";
            }
            var r = _repository.GetAll();
            r = r.Where(i => i.parrent_id == parrent_id).ToList();
            return r;
        }

        public PhongBan getOne(string id)
        {
            return _repository.Get(id);
        }

        public void update(PhongBan phongban)
        {
            _repository.Update(phongban);
        }
    }
}
