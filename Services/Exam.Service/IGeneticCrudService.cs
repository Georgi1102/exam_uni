using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service
{
    public interface IGenericCrudService<K, T>
    {
        Task<IEnumerable<T>> GetAll();

        //TODO dont return the entities fix it later
        Task<T> GetById(K id);

        Task<T> Create(T model);

        Task<T> Update(K id, T model);

        Task<T> Delete(K id);
    }
}
