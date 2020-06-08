using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SearchEngine
{
    [TestFixture]
    static class Tests
    {
        static EngineManager manager = new EngineManager();

        [Test]
        public static void AddAuthorTest1()
        {
            manager.AddAuthor("author1");
            manager.AddAuthor("author2", countryName: "Russian Federation");
        }

        [Test]
        public static void AddAuthorTest2()
        {
            Assert.Throws(typeof(ObjectNotFoundException),
                () => manager.AddAuthor("author3", countryName: "Wakanda"));
        }
        
        [Test]
        public static void AddArticleTest1()
        {
            manager.AddArticle("title1", "abst1", "text1", new[] { 1, 2 }, new[] { "kw1", "kw2" });
        }

        [Test]
        public static void AddArticleTest2()
        {
            manager.AddArticle("title2", "abst2", "text2", new[] { 1 }, new[] { "kw2", "kw3" });
        }

        [Test]
        public static void FindArticlesTest1()
        {
            //var authors = manager.FindAuthors("2").Select(a => a.Id).Take(2);
            var result = manager.FindArticles(title: "1", abstr: "2");
        }
    }
}
