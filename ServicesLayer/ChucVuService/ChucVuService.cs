using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.ChucVuService
{
    public class ChucVuService : IChucVuService
    {
        private IRepository<ChucVu> _repository;
        public ChucVuService(IRepository<ChucVu> repository)
        {
            _repository = repository;
        }
        public void create(ChucVu ChucVu)
        {
            _repository.Insert(ChucVu);
        }

        public void delete(string id)
        {
            ChucVu temp = getOne(id);
            _repository.Remove(temp);
            _repository.SaveChanges();
        }

        public IEnumerable<ChucVu> getAllAsync(string page = "", string limit = "", string search = "")
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
                    return a.TenChucVu.Contains(search) || a.TenChucVu.Contains(search);
                });
            }

            queryResultPage = queryResultPage
            .Skip(limitNew * (pageNew - 1))
            .Take(limitNew);
            return queryResultPage;
        }

        public ChucVu getOne(string id)
        {
            return _repository.Get(id);
        }

        public void update(ChucVu ChucVu)
        {
            _repository.Update(ChucVu);
        }
    }
}
