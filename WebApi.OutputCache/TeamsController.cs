using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.Utils;
using WebApi.OutputCache.Utils.TimeAttributes;

namespace WebApi.Cache.OutPut
{
    public class TeamsController : ApiController
    {
        private static readonly List<UF> UFs = new List<UF>
            {
                new UF {Id = 1, Descricao = "São Paulo", Sigla = "SP"}
                
            };

        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public IEnumerable<UF> Get()
        {
            return UFs;
        }

        [CacheOutputUntil(2016, 7, 20)]
        public UF GetById(int id)
        {
            var team = UFs.FirstOrDefault(i => i.Id == id);
            if (team == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return team;
        }

        [InvalidateCacheOutput("Get")]
        public void Post(UF value)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            UFs.Add(value);
        }

        public void Put(int id, UF value)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            var uf = UFs.FirstOrDefault(i => i.Id == id);
            if (uf == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            uf.Id = value.Id;
            uf.Descricao = value.Descricao;
            uf.Sigla = value.Sigla;

            var cache = Configuration.CacheOutputConfiguration().GetCacheOutputProvider(Request);
            cache.RemoveStartsWith(Configuration.CacheOutputConfiguration().MakeBaseCachekey((TeamsController t) => t.GetById(0)));
        }

        public void Delete(int id)
        {
            var team = UFs.FirstOrDefault(i => i.Id == id);
            if (team == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            UFs.Remove(team);
        }
    }
}
