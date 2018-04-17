using lab_3_State_Cache.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace lab_3_State_Cache.Middleware
{
    public class CacheLastRecord
    {
        readonly RequestDelegate _next;

        public CacheLastRecord(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,AirportContext airport, IMemoryCache cache)
        {
            Chek<Ticket>("Ticket", airport, cache);
            Chek<Flight>("Flight", airport, cache);
            Chek<Aircraft>("Aircraft", airport, cache);

            await _next.Invoke(context);
        }

        void Chek<T>(string key, AirportContext airport, IMemoryCache cache) where T : class
        {
            if (!cache.TryGetValue<T>(key, out T value))
            {
                value = airport.Set<T>().Last();

                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(250)
                };

                cache.Set<T>(key, value, options);
            }
        }
    }
}
