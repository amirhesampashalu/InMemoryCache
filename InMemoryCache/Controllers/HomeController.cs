using System.Diagnostics;
using InMemoryCache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
            private readonly IMemoryCache _cache;
        private readonly string Key = "_MyData";

        public HomeController(ILogger<HomeController> logger,IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            DateTime Myvalue;

           if(!_cache.TryGetValue(Key, out Myvalue))
            {
                var Cacheoption=new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5));
                Myvalue = DateTime.Now;
                _cache.Set(Key, Myvalue,Cacheoption);
            }
                


            return View(Myvalue);
        }

        public IActionResult Removecache()
        {
            _cache.Remove(Key);
            return View(nameof(Index));
        }
        public IActionResult GeyCache()
        {
          var Myvalue= _cache.Get(Key);
            return View(nameof(Index), Myvalue);
        }
        public IActionResult GetorCreate()
        {
            var myvalue = _cache.GetOrCreate(Key, p =>
            {
                p.SetSlidingExpiration(TimeSpan.FromSeconds(20));
                return DateTime.Now;

            });
            return View(nameof(Index), myvalue);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
