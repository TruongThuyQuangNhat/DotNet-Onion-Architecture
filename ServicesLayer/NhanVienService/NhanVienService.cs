using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.NhanVienService
{
    public class NhanVienService : IService<NhanVien>
    {
        #region Property  
        private IRepository<NhanVien> _repository;
        #endregion

        #region Constructor  
        public NhanVienService(IRepository<NhanVien> repository)
        {
            _repository = repository;
        }
        #endregion


        public IEnumerable<NhanVien> getAll()
        {
            return _repository.GetAll();
        }

        public NhanVien getOne(string id)
        {
            return _repository.Get(id);
        }

        public void create(NhanVien t)
        {
            _repository.Insert(t);
        }

        public void update(NhanVien t)
        {
            _repository.Update(t);
        }

        public void delete(string id)
        {
            NhanVien n = getOne(id);
            _repository.Remove(n);
            _repository.SaveChanges();
        }
    }
}
