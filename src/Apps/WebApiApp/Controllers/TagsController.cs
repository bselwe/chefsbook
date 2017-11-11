
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Core.Contracts;
using ChefsBook.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly ITagsRepository tagsRepository;
        private readonly IMapper mapper;

        public TagsController(
            ITagsRepository tagsRepository,
            IMapper mapper)
        {
            this.tagsRepository = tagsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await tagsRepository.AllAsync();
            var mappedTags = mapper.Map<List<TagDTO>>(tags);
            return Ok(mappedTags);
        }
    }
}
