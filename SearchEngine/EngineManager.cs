using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace SearchEngine
{
    public class EngineManager
    {
        private EngineContext context;

        public EngineManager()
        {
            context = new EngineContext();
        }

        public Author AddAuthor(string name, string bio = "", string countryName = null)
        {
            Country country = null;
            if (countryName != null)
            {
                country = context.Countries
                    .FirstOrDefault(c => c.CountryName.ToLower() == countryName.ToLower());
                if (country == null)
                    throw new ObjectNotFoundException("Such countryName does not exist");
            }
            var author = new Author()
            {
                Name = name,
                Bio = bio,
                Country = country
            };
            context.Authors.Add(author);
            context.SaveChanges();
            return author;
        }

        public Article AddArticle(string title, string abst, string text,
            IEnumerable<int> authorsIds, IEnumerable<string> keywords)
        {
            var authors = authorsIds
                .Distinct()
                .Select(id =>
                {
                    var author = context.Authors.Find(id);
                    if (author == null)
                        throw new ObjectNotFoundException($"Author with id = {id} not found");
                    return author;
                });

            return AddArticle(title, abst, text, authors, keywords);
        }

        public Article AddArticle(string title, string abst, string text,
            IEnumerable<Author> authors, IEnumerable<string> keywords)
        {
            var article = new Article()
            {
                Title = title,
                Abstract = abst,
                Text = text
            };
            context.Articles.Add(article);

            foreach (var author in authors.Distinct())
            {
                context.Authorships.Add(new Authorship()
                {
                    Article = article,
                    Author = author
                });
            }

            context.SaveChanges();

            foreach (var keyword in keywords.Select(kw => kw.ToLower()).Distinct())
            {
                var tag = new Tag() { Name = keyword };

                try
                {
                    context.Tags.Add(tag);
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    context.Entry(tag).State = EntityState.Detached;
                    tag = context.Tags.FirstOrDefault(t => t.Name == keyword);
                }

                var articleTag = new ArticleTag() { Article = article, Tag = tag };
                context.ArticleTags.Add(articleTag);
            }

            context.SaveChanges();
            return article;
        }

        public List<Author> FindAuthors(string searchRequest)
        {
            if (string.IsNullOrEmpty(searchRequest)) return context.Authors.ToList();
            else return context.Authors
                .Where(a => a.Name.Contains(searchRequest)
                || a.Bio.Contains(searchRequest))
                .ToList();
        }

        public List<Article> FindArticles(
            IEnumerable<int> authorsIds = null, 
            IEnumerable<string> keywords = null, 
            string title = null,
            string abstr = null,
            string text = null)
        {
            var articles = context.Articles.AsQueryable();
            if (authorsIds != null && authorsIds.Any())
            {
                articles = articles
                    .SelectMany(ar => ar.Authorships)
                    .Distinct()
                    .Where(au => authorsIds.Contains(au.Author.Id))
                    .Select(au => au.Article);
            }
            if (keywords != null && keywords.Any())
            {
                var eKeywords = keywords.Select(kw => kw.ToLower()).Distinct();
                articles = articles
                    .SelectMany(ar => ar.ArticleTags)
                    .Distinct()
                    .Where(at => eKeywords.Contains(at.Tag.Name))
                    .Select(at => at.Article);
            }
            if (!string.IsNullOrEmpty(title))
            {
                articles = articles
                    .Where(ar => ar.Title.ToLower().Contains(title.ToLower()));
            }
            if (!string.IsNullOrEmpty(abstr))
            {
                articles = articles
                    .Where(ar => ar.Abstract.ToLower().Contains(abstr.ToLower()));
            }
            if (!string.IsNullOrEmpty(text))
            {
                articles = articles
                    .Where(ar => ar.Text.ToLower().Contains(text.ToLower()));
            }
            return articles.ToList();
        }
    }
}
