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


        public IEnumerable<NhanVienGetAll> getAllAsync(
            string page = "", 
            string limit = "", 
            string search = "", 
            string key = "", 
            string options = "",
            string chucDanh_id = "",
            string chucVu_id = "",
            string phongBan_id = ""
            )
        {
            var queryResultPage = _repository.GetAll();
            var chucvu = _repositoryChuVu.GetAll();
            var chucdanh = _repositoryChucDanh.GetAll();
            var phongban = _repositoryPhongBan.GetAll();
            int limitNew = 5;
            int pageNew = 0;

            if(page != "" && page != null)
            {
                pageNew = int.Parse(page);
            }
            if (limit != "" && limit != null)
            {
                limitNew = int.Parse(limit);
            }
            // Search
            if (search != "" && search != null) {
                queryResultPage = from i in queryResultPage where i.FirstName.Contains(search.Trim()) || i.LastName.Contains(search.Trim()) select i;
            }
            if(chucDanh_id != "" && chucDanh_id != null)
            {
                queryResultPage = from i in queryResultPage where i.ChucDanh_ID.Equals(chucDanh_id) select i;
            }
            if (chucVu_id != "" && chucVu_id != null)
            {
                queryResultPage = from i in queryResultPage where i.ChucVu_ID.Equals(chucVu_id) select i;
            }
            if (phongBan_id != "" && phongBan_id != null)
            { 
                queryResultPage = from i in queryResultPage where i.PhongBan_ID.Equals(phongBan_id) select i;
            }
            // Sort
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

            // Paginator
            queryResultPage = queryResultPage
            .Skip(limitNew * (pageNew))
            .Take(limitNew).ToList();
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

        public int getTotalCount(
            string search,
            string chucDanh_id,
            string chucVu_id,
            string phongBan_id
        )
        {
            var list = _repository.GetAll();
            if(search != null && search != "")
            {
                list = from i in list where i.FirstName.Contains(search) || i.LastName.Contains(search) select i;
            }
            if (chucDanh_id != "" && chucDanh_id != null)
            {
                list = from i in list where i.ChucDanh_ID.Equals(chucDanh_id) select i;
            }
            if (chucVu_id != "" && chucVu_id != null)
            {
                list = from i in list where i.ChucVu_ID.Equals(chucVu_id) select i;
            }
            if (phongBan_id != "" && phongBan_id != null)
            {
                list = from i in list where i.PhongBan_ID.Equals(phongBan_id) select i;
            }
            return list.Count();
        }
    }
}
