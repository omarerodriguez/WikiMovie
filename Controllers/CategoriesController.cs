using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WikiMovies.Data;
using WikiMovies.Interfaces;
using WikiMovies.Models;

namespace WikiMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository ;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        //READ ALL CATEGORIES
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await categoryRepository.GetCategories());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        //READ MOVIE BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await categoryRepository.GetCategoryById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        //CREATE CATEGORY
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            try
            {
                if(category == null)
                {
                    return BadRequest();
                }
                var result = await categoryRepository.AddCategory(category);
                return CreatedAtAction(nameof(AddCategory), new { Id = result.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new category record");
            }
        }
        //UPDATE CATEGORY
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            try
            {
                if(id != category.Id)
                {
                    return BadRequest();
                }
                var categoryUpdate = await categoryRepository.UpdateCategory(category);
                if(categoryUpdate == null)
                {
                    return NotFound($"CAtegory with Id = {id} not found");
                }
                return await categoryRepository.UpdateCategory(category);
            }
            catch (Exception)
            {
               return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update Category record");
            }
        }
        //DELETE CATEGORY
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var categoryDelete = await categoryRepository.GetCategoryById(id);
                if (categoryDelete == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }
                await categoryRepository.DeleteCategory(id);
                return Ok($"Category with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Delete category record");
            }
        }


    }

}
