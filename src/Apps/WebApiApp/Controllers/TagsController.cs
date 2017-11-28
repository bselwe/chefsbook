
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ChefsBook.Core.Contracts;
using ChefsBook.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChefsBook.WebApiApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly ITagsService tagsService;
        private readonly IMapper mapper;

        public TagsController(
            ITagsService tagsService,
            IMapper mapper)
        {
            this.tagsService = tagsService;
            this.mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<TagDTO>))]
        public async Task<IActionResult> GetTags()
        {
            var tags = await tagsService.AllAsync();
            var mappedTags = mapper.Map<List<TagDTO>>(tags);
            return Ok(mappedTags);
        }
    }
}
