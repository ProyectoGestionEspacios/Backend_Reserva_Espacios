using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        //buscar por id, nombre u otro parametro
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> whereCondition);
        Task<IEnumerable<T>> Get(int pag, int nroOfElements);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereCondition);
    }

}
