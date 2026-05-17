using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Repositores;

   public interface IGenricRepo<T> where T : class
{
    Task AddAsync(T entity) ;
    void Update(T entity) ;
    void DeleteAsync(T entity) ;
    Task<T> GetByIdAsync(Guid id) ;
    Task<IEnumerable<T>> GetAllAsync();
   
}

