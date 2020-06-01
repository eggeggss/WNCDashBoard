using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

   

    public interface IEFAdapter
    {

    }
    public interface IRepository
    {

    }
    public interface IRepository<T>: IRepository
    {
        T Get(string name);
        IEnumerable<T> GetAll();
        bool Insert(T t);

        bool Update(T t);
        bool Delete(T t);

    }
}
