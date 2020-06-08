using Newtonsoft.Json;
using SearchEngine.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class EngineInitializer : CreateDatabaseIfNotExists<EngineContext>
    {
        protected override void Seed(EngineContext context)
        {
            context.Countries.AddRange(GetCountries());
            context.SaveChanges();
        }

        private IEnumerable<Country> GetCountries()
        {
            string countriesJson = Encoding.UTF8.GetString(Resources.Countries);
            return JsonConvert.DeserializeObject<IEnumerable<Country>>(countriesJson);
        }
    }
}
