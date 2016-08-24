using System;
using System.Web.Http;
using WebApi.OutputCache.Utils;

namespace WebApi.Cache.OutPut
{
    /// <summary>
    /// classe para iguinorar controles para pegar o chache
    /// </summary>
    [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
    [RoutePrefix("ignore")]
    public class IguinorarController : ApiController
    {
        [Route("cached")]
        public string GetCached()
        {
            return DateTime.Now.ToString();
        }

        [IgnoreCacheOutput]
        [Route("uncached")]
        public string GetUnCached()
        {
            return DateTime.Now.ToString();
        }
    }
}