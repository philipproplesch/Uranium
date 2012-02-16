using System.Linq;
using Uranium.Core.Models.Common;

namespace Uranium.Core.Data.Common
{
    public interface IRepository<T, in TPrimaryKey> 
        : IQueryable<T> where T : IEntityBase<TPrimaryKey>
    {
        T Get(TPrimaryKey id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(TPrimaryKey id);
        bool Delete(T entity);
        bool DeleteFromDatabase(TPrimaryKey id);
        bool DeleteFromDatabase(T entity);
    }
}