using DiseaseMongoModel;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Repositories
{
    public class CacheRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private IMemoryCacheWrapper _cacheWrapper;

        IRepository<TEntity> _repository;

        public CacheRepository(IMemoryCacheWrapper memoryCacheWrapper, IRepository<TEntity> repository)
        {
            _cacheWrapper = memoryCacheWrapper;
            _repository = repository;
        }

        public bool Create(TEntity entity)
        {
            if (_repository.Create(entity))
            {
                _cacheWrapper.Set(entity.Id, entity, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ConstantValues.MINUTES_IN_CACHE)
                });

                return true;
            }

            return false;
        }

        public bool Update(TEntity entity)
        {
            if (_repository.Update(entity))
            {
                _cacheWrapper.Set(entity.Id, entity, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ConstantValues.MINUTES_IN_CACHE)
                });

                return true;
            }

            return false;
        }

        public TEntity GetById(string id)
        {
            TEntity entity;

            if (!_cacheWrapper.TryGetValue(id, out entity))
            {
                entity = _repository.GetById(id);
                if (entity != null)
                {
                    _cacheWrapper.Set(id, entity,
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(ConstantValues.MINUTES_IN_CACHE)));
                }
            }

            return entity;
        }

        public bool Delete(TEntity entity)
        {
            if (_repository.Delete(entity))
            {
                _cacheWrapper.MemoryCache.Remove(entity.Id);

                return true;
            }

            return false;
        }
    }
}
