using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using System;
using System.Linq;

namespace Common.Lib.DAL.EFCore
{
    public class ServerRepository<T> : IRepository<T> where T : Entity
    {
        public virtual SaveResult<T> Add(T entity)
        {

            // creo una conexión contra la DB guardo la entity y le devuelvo 
            // a quien me haya hecho la llamada
            throw new NotImplementedException();
        }

        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> QueryAll()
        {
            throw new NotImplementedException();
        }

        public SaveResult<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}