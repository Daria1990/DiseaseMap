using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Cache
{
    /// <summary>
    /// Вспомогательный класс обертка для IMemoryCache, необходимый для написания unit тестов 
    /// </summary>
    public class MemoryCacheWrapper : IMemoryCacheWrapper
    {
        /// <summary>
        /// Кеширование на сервере
        /// </summary>
        public IMemoryCache _MemoryCache { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="memoryCache">Кеширование на сервере</param>
        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Метод кеширует значение заданного типа с заданными параметрами
        /// </summary>
        /// <typeparam name="TItem">Тип кешируемого значения</typeparam>
        /// <param name="key">ключ</param>
        /// <param name="value">кешируемое значение</param>
        /// <param name="options">Параметры кеширования</param>
        /// <returns>кешируемое значение</returns>
        public TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options)
        {
            return _MemoryCache.Set<TItem>(key,value, options);
        }

        /// <summary>
        ///  Метод достает значение из кеша
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="key">ключ</param>
        /// <param name="value">значение из кеша по ключу</param>
        /// <returns>Получилось ли достать значение из кеша</returns>
        public bool TryGetValue<TItem>(object key, out TItem value)
        {
            return _MemoryCache.TryGetValue<TItem>(key, out value);
        }
    }
}
