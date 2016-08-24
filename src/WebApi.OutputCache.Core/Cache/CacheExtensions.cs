using System;

namespace WebApi.OutputCache.Core.Cache
{
    // classe para controle de chache
    public static class ExtencaoCache
    {
        /// <summary>
        /// Classe para pegar o cache 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="resultGetter"></param>
        /// <param name="bypassCache"></param>
        /// <returns></returns>
        public static T GetCachedResult<T>(this IApiOutputCache cache, string key, DateTimeOffset expiry, Func<T> resultGetter, bool bypassCache = true) where T : class
        {
            var result = cache.Get<T>(key);

            if (result == null || bypassCache)
            {
                result = resultGetter();
                if (result != null) cache.Add(key, result, expiry);
            }

            return result;
        }
    }
}