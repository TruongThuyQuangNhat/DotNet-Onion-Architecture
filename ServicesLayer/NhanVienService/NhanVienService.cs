using DomainLayer.Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.NhanVienService
{
    public class NhanVienService : INhanVienService
    {
        #region Property  
        private IRepository<NhanVien> _repository;
        private IRepository<ChucVu> _repositoryChuVu;
        private IRepository<ChucDanh> _repositoryChucDanh;
        private IRepository<PhongBan> _repositoryPhongBan;
        #endregion

        #region Constructor  
        public NhanVienService(
            IRepository<NhanVien> repository, 
            IRepository<ChucVu> repoChucVu,
            IRepository<ChucDanh> repoChucDanh,
            IRepository<PhongBan> repoPhongBan
        )
        {
            _repository = repository;
            _repositoryChuVu = repoChucVu;
            _repositoryChucDanh = repoChucDanh;
            _repositoryPhongBan = repoPhongBan;
        }
        #endregion


        public IEnumerable<NhanVienGetAll> getAllAsync(string page = "", string limit = "", string search = "", string key = "", string options = "")
        {
            int limitNew = 5;
            int pageNew = 1;
            if(page != "" && page != null)
            {
                pageNew = int.Parse(page);
            }
            if (limit != "" && limit != null)
            {
                limitNew = int.Parse(limit);
            }
            var queryResultPage = _repository.GetAll();
            var chucvu = _repositoryChuVu.GetAll();
            var chucdanh = _repositoryChucDanh.GetAll();
            var phongban = _repositoryPhongBan.GetAll();
            if (search != "" && search != null) {
                queryResultPage = from i in queryResultPage where i.FirstName.Contains(search) || i.LastName.Contains(search) select i;
            }
            if (key != "" && key != null && options != "" && options != null)
            {
                if (key == "firstname" && options == "asc")
                {
                    queryResultPage = from i in queryResultPage orderby i.FirstName ascending select i;
                } else if (key == "firstname" && options == "desc")
                {
                    queryResultPage = from i in queryResultPage orderby i.FirstName descending select i;
                } else if (key == "lastname" && options == "asc")
                {
                    queryResultPage = from i in queryResultPage orderby i.LastName ascending select i;
                } else if (key == "lastname" && options == "desc")
                {
                    queryResultPage = from i in queryResultPage orderby i.LastName descending select i;
                }
            }

            queryResultPage = queryResultPage
            .Skip(limitNew * (pageNew - 1))
            .Take(limitNew);

            var ketqua = from temp in queryResultPage
                         join c in chucvu on temp.ChucVu_ID equals c.Id
                         join d in chucdanh on temp.ChucDanh_ID equals d.Id
                         join p in phongban on temp.PhongBan_ID equals p.Id
                         select new NhanVienGetAll
                         {
                             Id = temp.Id,
                             FirstName = temp.FirstName,
                             LastName = temp.LastName,
                             Image = temp.Image,
                             TenChucVu = c.TenChucVu,
                             TenChucDanh = d.TenChucDanh,
                             TenPhongBan = p.TenPhongBan,
                         };

            Console.WriteLine("6");
            return ketqua;
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
