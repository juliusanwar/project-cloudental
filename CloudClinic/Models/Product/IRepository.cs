using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.Product
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Read();
        T GetById(int? id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int? id);
    }
}
