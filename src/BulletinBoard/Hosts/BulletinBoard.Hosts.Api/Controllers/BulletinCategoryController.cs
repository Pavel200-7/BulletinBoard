using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Services.IServices;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;


//using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Hosts.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status500InternalServerError)]
    public class BulletinCategoryController : ControllerBase
    {
        private readonly IBulletinCategoryService _bulletinCategoryService;        

        public BulletinCategoryController(IBulletinCategoryService bulletinCategoryService)
        {
            _bulletinCategoryService = bulletinCategoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBulletinCategory(Guid id)
        {
            var categoryDto = await _bulletinCategoryService.GetByIdAsync(id);
            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBulletinCategory(BulletinCategoryCreateDto category)
        {
            var categoryDto = await _bulletinCategoryService.CreateAsync(category);
            if (category == null)
            {
                return BadRequest();
            }

            return Ok(categoryDto);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BulletinCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBulletinCategory(Guid id, BulletinCategoryUpdateDto category)
        {
            var categoryDto = await _bulletinCategoryService.UpdateAsync(id, category);
            return Ok(categoryDto);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBulletinCategory(Guid id)
        {
            bool isDeleted = await _bulletinCategoryService.DeleteAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(typeof(BulletinCategoryReadAllDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBulletinCategory()
        {
            var categoryDto = await _bulletinCategoryService.GetAllAsync();
            return Ok(categoryDto);
        }

        [HttpGet]
        [Route("SingleChain")]
        [ProducesResponseType(typeof(BulletinCategoryReadAllDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetsingleBulletinCategory(Guid id)
        {
            var categoryDto = await _bulletinCategoryService.GetSingleAsync(id);
            return Ok(categoryDto);
        }






    }
}
