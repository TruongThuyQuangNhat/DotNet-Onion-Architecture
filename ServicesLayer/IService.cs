using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public interface IService<T>
    {
        IEnumerable<T> getAll();
        T getOne(string id);
        void create(T t);
        void update(T t);
        void delete(string id);
    }
}
