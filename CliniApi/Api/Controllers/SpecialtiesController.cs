using CliniApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CliniApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class SpecialtiesController : BaseApiController
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtiesController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var result = await _specialtyService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _specialtyService.GetByIdAsync(id);
            return HandleResult(result);
        }
    }
}
