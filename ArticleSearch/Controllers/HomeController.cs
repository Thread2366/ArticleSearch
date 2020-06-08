using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArticleSearch.Models;
using SearchEngine;

namespace ArticleSearch.Controllers
{
    public class HomeController : Controller
    {
        private EngineManager manager = new EngineManager();

        public ActionResult Index()
        {
            return Redirect("/Home/ArticleSearch");
        }

        [HttpGet]
        public ActionResult ArticleSearch(string authors, string keywords, 
            string title, string abstr, string text)
        {
            var parsedAuthors = SplitInput(authors)
                .SelectMany(name => manager.FindAuthors(name))
                .Select(a => a.Id);
            var searchResult = manager.FindArticles(parsedAuthors, SplitInput(keywords),
                title, abstr, text);

            var model = searchResult
                .Select(ar => ArticleToModel(ar))
                .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult AuthorSearch(string searchRequest)
        {
            var authors = manager.FindAuthors(searchRequest);
            var model = authors
                .Select(a => AuthorToModel(a))
                .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult ArticleCreation()
        {
            var model = manager.FindAuthors()
                .Select(a => AuthorToModel(a));
            return View(model);
        }

        [HttpPost]
        public ActionResult ArticleCreation(string title, string abstr, string text,
            int[] authorsIds, string keywords)
        {
            manager.AddArticle(title, abstr, text, authorsIds, SplitInput(keywords));
            return Redirect("/Home/ArticleCreation");
        }

        [HttpGet]
        public ActionResult AuthorCreation()
        {
            var model = manager.GetCountries()
                .Select(c => CountryToModel(c));
            return View(model);
        }

        [HttpPost]
        public ActionResult AuthorCreation(string name, string bio, string country)
        {
            manager.AddAuthor(name, bio, country);
            return Redirect("/Home/AuthorCreation");
        }

        [HttpGet]
        public ActionResult ArticleDetails(int id)
        {
            var article = manager.GetArticleById(id);
            if (article == null) return new HttpNotFoundResult();
            var model = ArticleToModel(article);
            return View(model);
        }

        [HttpGet]
        public ActionResult AuthorDetails(int id)
        {
            var author = manager.GetAuthorById(id);
            if (author == null) return new HttpNotFoundResult();
            var model = AuthorToModel(author);
            return View(model);
        }

        private ArticleModel ArticleToModel(Article article)
        {
            return new ArticleModel()
            {
                Id = article.Id,
                Title = article.Title,
                Abstract = article.Abstract,
                Text = article.Text,
                AuthorsNames = string.Join(", ", article.Authorships.Select(au => au.Author.Name)),
                Keywords = string.Join(", ", article.ArticleTags.Select(at => at.Tag.Name))
            };
        }

        private AuthorModel AuthorToModel(Author author)
        {
            return new AuthorModel()
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio == "" ? "-" : author.Bio,
                Country = author.Country == null ? "-" : author.Country.CountryName
            };
        }

        private CountryModel CountryToModel(Country country)
        {
            return new CountryModel()
            {
                CountryCode = country.CountryCode,
                CountryName = country.CountryName
            };
        }

        private IEnumerable<string> SplitInput(string input)
        {
            return (input ?? "")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim());
        }
    }
}