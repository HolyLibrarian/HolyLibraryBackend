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
        public IActionResult CreateCollection(CreateCollecitonDto createCollecitonDto)
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
            return Created(collection.Id.ToString(), collection);
        }

        [HttpGet]
        public IActionResult SearchCollections(string name = null, string author = null, string publisher = null)
        {
            var collections = dbContext.Collections
                .Where(x => x.Name.Contains(name) || name == null)
                .Where(x => x.Author.Contains(author) || author == null)
                .Where(x => x.Publisher.Contains(publisher) || publisher == null)
                .ToList();
            return Ok(collections);
        }

        [HttpGet("{collectionId}")]
        public IActionResult GetCollection(int collectionId)
        {
            var collection = dbContext.Collections.Where(x => x.Id == collectionId).FirstOrDefault();
            if (collection == null)
            {
                return NotFound();
            }
            return Ok(collection);
        }

        [HttpPut("{collectionId}")]
        public IActionResult EditCollection(int collectionId, EditCollectionDto editCollectionDto)
        {
            var collection = dbContext.Collections.Where(x => x.Id == collectionId).FirstOrDefault();
            if (collection == null)
            {
                return NotFound();
            }
            collection.Name = editCollectionDto.Name;
            collection.Isbn = editCollectionDto.Isbn;
            collection.Author = editCollectionDto.Author;
            collection.Publisher = editCollectionDto.Publisher;
            collection.Price = editCollectionDto.Price;
            collection.Location = editCollectionDto.Location;
            dbContext.Update(collection);
            dbContext.SaveChanges();
            return Ok(collection);
        }
    }
}
