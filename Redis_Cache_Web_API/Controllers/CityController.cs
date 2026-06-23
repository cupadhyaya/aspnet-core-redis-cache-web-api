using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Redis_Cache_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private IDistributedCache _cache;

        public CityController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet(Name = "GetCities")]
        public IActionResult Get()
        {
            string cacheKey = "cities";

            string[] cities;

            var citiesFromCache = _cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(citiesFromCache))
            {
                cities = JsonSerializer.Deserialize<string[]>(citiesFromCache);
            }
            else
            {
                cities = GetCity();

                var options = new DistributedCacheEntryOptions {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                _cache.SetString(cacheKey, JsonSerializer.Serialize(cities), options);
            }

            return Ok(cities);
        }

        public string[] GetCity()
        {
            return new string[] { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };
        }
    }
}
