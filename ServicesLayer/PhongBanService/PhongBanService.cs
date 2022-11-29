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

        public IEnumerable<PhongBan> getAllAsync(string page = "", string limit = "", string search = "")
        {
            int limitNew = 5;
            int pageNew = 1;
            if (page != "" && page != null)
            {
                pageNew = int.Parse(page);
            }
            if (limit != "" && limit != null)
            {
                limitNew = int.Parse(limit);
            }
            var r = _repository.GetAll();
            var queryResultPage = r;
            if (search != "" && search != null)
            {
                queryResultPage = r.Where(a =>
                {
                    return a.TenPhongBan.Contains(search) || a.TenPhongBan.Contains(search);
                });
            }
            
            queryResultPage = queryResultPage
            .Skip(limitNew * (pageNew - 1))
            .Take(limitNew);
            return queryResultPage;
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
