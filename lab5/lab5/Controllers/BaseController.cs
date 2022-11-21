using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace lab5.Controllers
{
    public class BaseController : Controller
    {
        protected IMemoryCache _cache;

        protected int _cacheTime = 360;

        public BaseController(IMemoryCache cache) 
        {
            _cache = cache;
        }

        protected void CacheClear()
        {
            MethodInfo clearMethod = _cache.GetType().GetMethod("Clear", BindingFlags.Instance | BindingFlags.Public);
            if (clearMethod != null)
            {
                clearMethod.Invoke(_cache, null);
                return;
            }
            else
            {
                PropertyInfo prop = _cache.GetType().GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Public);
                if (prop != null)
                {
                    object innerCache = prop.GetValue(_cache);
                    if (innerCache != null)
                    {
                        clearMethod = innerCache.GetType().GetMethod("Clear", BindingFlags.Instance | BindingFlags.Public);
                        if (clearMethod != null)
                        {
                            clearMethod.Invoke(innerCache, null);
                            return;
                        }
                    }
                }
            }
        }

        protected string GetStringFromSession(string key, string value = null, string defaultValue = "")
        {
            if (value != null)
            {
                HttpContext.Session.SetString(key, value);
                return value;
            }
            else if (HttpContext.Session.GetString(key) != null)
            {
                value = HttpContext.Session?.GetString(key);
                HttpContext.Session.SetString(key, value);
                return value;
            }
            else
            {
                HttpContext.Session.SetString(key, defaultValue);
                return defaultValue;
            }
        }
    }
}
