using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMongoModel
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        TEntity GetById(string id);
    }
}
