using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IRepository<TEntity>
    {
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity> GetById(params object[] id);
        IQueryable<TEntity> GetAll();
    }
}
