using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.ChucDanhService
{
    public class ChucDanhService : IChucDanhService
    {
        private IRepository<ChucDanh> _repository;

        public ChucDanhService(IRepository<ChucDanh> repository)
        {
            _repository = repository;
        }
        public void create(ChucDanh ChucDanh)
        {
            _repository.Insert(ChucDanh);
        }

        public void delete(string id)
        {
            ChucDanh temp = getOne(id);
            _repository.Remove(temp);
            _repository.SaveChanges();
        }

        public IEnumerable<ChucDanh> getAllAsync(string page = "", string limit = "", string search = "")
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
                    return a.TenChucDanh.Contains(search) || a.TenChucDanh.Contains(search);
                });
            }

            queryResultPage = queryResultPage
            .Skip(limitNew * (pageNew - 1))
            .Take(limitNew);
            return queryResultPage;
        }

        public ChucDanh getOne(string id)
        {
            return _repository.Get(id);
        }

        public void update(ChucDanh ChucDanh)
        {
            _repository.Update(ChucDanh);
        }
    }
}
