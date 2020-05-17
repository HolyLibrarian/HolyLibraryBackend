using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly HolyLibraryContext dbContext;

        public CollectionsController(HolyLibraryContext holyLibraryContext)
        {
            dbContext = holyLibraryContext;
        }

        [HttpPost]
        public Collection CreateCollection(CreateCollecitonDto createCollecitonDto)
        {
            var collection = new Collection
            {
                Name = createCollecitonDto.Name,
                Isbn = createCollecitonDto.Isbn,
                Author = createCollecitonDto.Author,
                Publisher = createCollecitonDto.Publisher,
                Price = createCollecitonDto.Price,
                Location = createCollecitonDto.Location,
            };
            dbContext.Add(collection);
            dbContext.SaveChanges();
            return collection;
        }

        [HttpGet]
        public object SearchCollections(string name = null, string author = null, string publisher = null)
        {
            var collections = dbContext.Collections
                .Where(x => x.Name.Contains(name) || name == null)
                .Where(x => x.Author.Contains(author) || author == null)
                .Where(x => x.Publisher.Contains(publisher) || publisher == null)
                .ToList();
            return Ok(collections);
        }

        [HttpGet("{collectionId}")]
        public object GetCollection(int collectionId)
        {
            var collection = dbContext.Collections.Where(x => x.Id == collectionId).FirstOrDefault();
            if (collection == null)
            {
                return NotFound();
            }
            return Ok(collection);
        }
    }
}
