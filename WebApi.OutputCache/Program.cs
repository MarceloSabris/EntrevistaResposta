using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using WebApi.OutputCache.Core.Cache;
using WebApi.OutputCache.Utils;

namespace WebApi.Cache.OutPut
{
    class Program
    {
        static void Main(string[] args)
        {
            // criando o hostint para o serviço
            var config = new HttpSelfHostConfiguration("http://localhost:999");
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var server = new HttpSelfHostServer(config);

            config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new MemoryCacheDefault());

            server.OpenAsync().Wait();

            Console.ReadKey();

            server.CloseAsync().Wait();
        }
    }
}
