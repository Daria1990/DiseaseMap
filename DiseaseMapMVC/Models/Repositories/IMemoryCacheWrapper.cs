using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiseaseMapMVC.Models.Repositories
{
    public interface IMemoryCacheWrapper
    {
        public IMemoryCache MemoryCache { get; set; }

        public TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options);

        public bool TryGetValue<TItem>(object key, out TItem value);
    }
}
