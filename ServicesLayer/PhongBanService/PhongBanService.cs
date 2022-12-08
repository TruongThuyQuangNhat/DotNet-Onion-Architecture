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

        public List<tempModel> getAllAsync()
        {
            List<tempModel> temp = this.getPhongBanWithParrentID("0");
            //var r = _repository.GetAll();
            return temp;
        }
        public List<PhongBan> GetChildren(string parrent_id)
        {
            var r = _repository.GetAll();
            var i = r.Where(i => i.parrent_id == parrent_id).ToList();
            return i;
        }
        public List<tempModel> getPhongBanWithParrentID(string parrent_id)
        {
            var allPB = _repository.GetAll().ToList();
            List<tempModel> test1 = new List<tempModel>();
            if (!parrent_id.Contains("PB"))
            {
                foreach (var item in allPB)
                {
                    if (item.parrent_id == parrent_id)
                    {
                        Console.WriteLine(item.parrent_id);
                        var model = new tempModel();
                        model.PhongBan = item;
                        model.listPhongBan = this.GetChildren(item.Id).ToArray();
                        foreach (var i in model.listPhongBan)
                        {
                            model.listTempModel = this.getPhongBanWithParrentID(i.Id);
                        }
                        test1.Add(model);
                    }
                }
            }
            return test1;
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
