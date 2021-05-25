using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Repositories
{
    public class MemoryCacheWrapper : IMemoryCacheWrapper
    {
        public IMemoryCache MemoryCache { get; set; }

        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options)
        {
            return MemoryCache.Set<TItem>(key,value, options);
        }

        public bool TryGetValue<TItem>(object key, out TItem value)
        {
            return MemoryCache.TryGetValue<TItem>(key, out value);
        }
    }
}
