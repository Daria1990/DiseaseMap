using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Cache
{
    /// <summary>
    /// Интерфейс для вспомогательного класса, который является оберткой для IMemoryCache, необходимый для написания unit тестов 
    /// </summary>
    public interface IMemoryCacheWrapper
    {
        /// <summary>
        /// Кеширование на сервере
        /// </summary>
        public IMemoryCache _MemoryCache { get; set; }

        /// <summary>
        /// Метод кеширует значение заданного типа с заданными параметрами
        /// </summary>
        /// <typeparam name="TItem">Тип кешируемого значения</typeparam>
        /// <param name="key">ключ</param>
        /// <param name="value">кешируемое значение</param>
        /// <param name="options">Параметры кеширования</param>
        /// <returns>кешируемое значение</returns>
        public TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options);

        /// <summary>
        ///  Метод достает значение из кеша
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key">ключ</param>
        /// <param name="value">значение из кеша по ключу</param>
        /// <returns>Получилось ли достать значение из кеша</returns>
        public bool TryGetValue<TItem>(object key, out TItem value);
    }
}
