using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(T entity)
        {
            bool created = false;//objeto no creado
            var save = await _unitOfWork.Context.Set<T>().AddAsync(entity); //creamos objeto
            if (save != null) created = true;
            return created;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {

            bool updated = false;
            _unitOfWork.Context.Set<T>().Attach(entity);//hace seguimiento de la entidad mediante el Id que es unico
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified; //trabaja en memoria y modifica,y con el commit mandamos a la base de datos
            updated = true;

            return updated;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereCondition)
        {
            bool deleted = false;
            //buscamos el objeto a eliminar
            var objetoEncontrado = _unitOfWork.Context.Set<T>().Where(whereCondition).FirstOrDefault();
            if (objetoEncontrado != null)
            {
                _unitOfWork.Context.Set<T>().Attach(objetoEncontrado);
                _unitOfWork.Context.Entry(objetoEncontrado).State = EntityState.Deleted;
                deleted = true;
            }
            return deleted;
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> whereCondition)
        {

            return await _unitOfWork.Context.Set<T>().Where(whereCondition).ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(int page, int nroOfElements)
        {
            var elemtsSaltar = (page - 1) * nroOfElements;
            return await _unitOfWork.Context.Set<T>().Skip(elemtsSaltar).Take(nroOfElements).ToListAsync();
        }
    }
}
