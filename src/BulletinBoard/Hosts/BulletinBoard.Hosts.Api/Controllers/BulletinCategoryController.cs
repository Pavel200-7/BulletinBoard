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

        //[HttpPost]
        //[Route("filter")]
        //[ProducesResponseType(typeof(IReadOnlyCollection<BulletinCategoryDto>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetBulletinCategoryFilter(BulletinCategoryFilterDto category)
        //{
        //    var categoryDto = await _bulletinCategoryService.GetAsync(category);
        //    return Ok(categoryDto);
        //}




    }
}
