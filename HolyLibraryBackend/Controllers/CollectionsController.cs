using HolyLibraryBackend.Dto;
using HolyLibraryBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyLibraryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly HolyLibraryContext holyLibraryContext;

        public CollectionsController(HolyLibraryContext holyLibraryContext)
        {
            this.holyLibraryContext = holyLibraryContext;
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
            holyLibraryContext.Add(collection);
            holyLibraryContext.SaveChanges();
            return collection;
        }
    }
}
