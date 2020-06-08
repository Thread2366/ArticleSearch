using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;
using SearchEngine.Properties;

namespace SearchEngine
{
    class EngineContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Authorship> Authorships { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        public EngineContext() : base()
        {
            Database.SetInitializer(new EngineInitializer());
        }
    }
}
