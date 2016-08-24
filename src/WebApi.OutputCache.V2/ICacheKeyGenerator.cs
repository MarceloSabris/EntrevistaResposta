using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace WebApi.OutputCache.Utils
{
    public interface ICacheKeyGenerator
    {
        string MakeCacheKey(HttpActionContext context, MediaTypeHeaderValue mediaType, bool excludeQueryString = false);
    }
}
