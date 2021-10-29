using DiseaseMongoModel;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Cache
{
    /// <summary>
    /// Класс кеширования значений из заданного репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности, для репозитория которой будет осуществляться кеширование</typeparam>
    public class CacheRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Кеширование на сервере в обертке
        /// </summary>
        private IMemoryCacheWrapper _CacheWrapper;

        /// <summary>
        /// Репозиторий для которого будет осуществляться кеширование
        /// </summary>
        IRepository<TEntity> _Repository;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="memoryCacheWrapper">Кеширование на сервере в обертке</param>
        /// <param name="repository">Репозиторий для которого будет осуществляться кеширование</param>
        public CacheRepository(IMemoryCacheWrapper memoryCacheWrapper, IRepository<TEntity> repository)
        {
            _CacheWrapper = memoryCacheWrapper ?? throw new ArgumentNullException(nameof(memoryCacheWrapper));
            _Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Метод создает значение заданного типа и записывает его в кеш
        /// </summary>
        /// <param name="entity">создаваемая сущность</param>
        /// <returns>Успешно ли создана запись</returns>
        public bool Create(TEntity entity)
        {
            if (_Repository.Create(entity))
            {
                _CacheWrapper.Set(entity.Id, entity, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ConstantValues.MinutesInCache)
                });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод обновляет заданную сущность и обновляет ее в кеше
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        /// <returns>Успешно ли обновлена запись</returns>
        public bool Update(TEntity entity)
        {
            if (_Repository.Update(entity))
            {
                _CacheWrapper.Set(entity.Id, entity, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ConstantValues.MinutesInCache)
                });

                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод пытается получить запись по ее id. Сначала запись ищется в кеше, если в кеше записи нет, то ищем ее в словаре.
        /// </summary>
        /// <param name="id">id записи, которую необходимо извлечь</param>
        /// <returns>Запись, найденная по id</returns>
        public TEntity GetById(string id)
        {
            TEntity entity;

            if (!_CacheWrapper.TryGetValue(id, out entity))
            {
                entity = _Repository.GetById(id);
                if (entity != null)
                {
                    _CacheWrapper.Set(id, entity,
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(ConstantValues.MinutesInCache)));
                }
            }

            return entity;
        }

        /// <summary>
        /// Метод удаления записи, сначала запись удаляется в словаре, а потом и в кеше
        /// </summary>
        /// <param name="entity">Запись, которую необходимо удалить</param>
        /// <returns>Успешно ли удалена запись</returns>
        public bool Delete(TEntity entity)
        {
            if (_Repository.Delete(entity))
            {
                _CacheWrapper._MemoryCache.Remove(entity.Id);

                return true;
            }

            return false;
        }
    }
}
