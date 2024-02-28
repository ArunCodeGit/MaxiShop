using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var Categories = await _categoryRepository.GetAllAsync();
            return Ok(Categories);


        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult> Get(int id)
        {
            var Category = await _categoryRepository.GetByIdAsync(x=>x.Id == id);

            if(Category==null)
            {
                return NotFound($"{id} Category not found.");
            }

            return Ok(Category);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _categoryRepository.CreateAsync(category);
           
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryRepository.UpdateAsync(category);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }

            var category = _categoryRepository.GetByIdAsync(x=>x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteAsync(category);
            return NoContent();
        }
    }
}
