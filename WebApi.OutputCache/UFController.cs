using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.Utils;
using WebApi.OutputCache.Utils.TimeAttributes;

namespace WebApi.Cache.OutPut
{
    [AutoInvalidateCacheOutput]
    public class UFController : ApiController
    {
        private static readonly List<UF> UFS = new List<UF>
            {
                new UF {Id = 1, Sigla = "SP", Descricao = "São Paulo"},
                new UF {Id = 1, Sigla = "RJ", Descricao = "Rio de Janeiro"}
            };

        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public IEnumerable<UF> Get()
        {
            return UFS;
        }

        [CacheOutputUntil(2014, 7, 20)]
        public UF GetById(int id)
        {
            var team = UFS.FirstOrDefault(i => i.Id == id);
            if (team == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return team;
        }

        public void Post(UF value)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            UFS.Add(value);
        }

        public void Put(int id, UF value)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            var uf = UFS.FirstOrDefault(i => i.Id == id);
            if (uf == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            uf.Id = value.Id;
            uf.Descricao = value.Descricao;
            uf.Sigla = value.Sigla;
        }

        public void Delete(int id)
        {
            var uf = UFS.FirstOrDefault(i => i.Id == id);
            if (uf == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            UFS.Remove(uf);
        }
    }
}
