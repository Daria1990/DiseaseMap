using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DiseaseMongoModel
{
    public interface ISearch<TEntity> where TEntity : BaseEntity
    {
        IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> filter);
        IList<TEntity> SearchAll();
    }
}
