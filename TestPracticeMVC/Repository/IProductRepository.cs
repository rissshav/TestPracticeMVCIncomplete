using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPracticeMVC.Repository
{
    public interface IProductRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object ProductId);
        void Add(T Products);
        void Update(T Products);
        void Delete(object id);
        void Save();
    }
}
