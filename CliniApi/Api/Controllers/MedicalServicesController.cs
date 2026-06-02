using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CliniApi.Api.Controllers
{
    [Route("api/medical-services")]
    public class MedicalServicesController : BaseApiController
    {
        private readonly IMedicalServiceService _medicalServiceService;

        public MedicalServicesController(IMedicalServiceService medicalServiceService)
        {
            _medicalServiceService = medicalServiceService;
        }

        [HttpGet]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicalServiceService.GetAllActiveAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicalServiceService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create(CreateMedicalServiceDto dto)
        {
            var result = await _medicalServiceService.CreateAsync(dto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(int id, UpdateMedicalServiceDto dto)
        {
            var result = await _medicalServiceService.UpdateAsync(id, dto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicalServiceService.DeleteAsync(id);
            return HandleResult(result);
        }
    }
}
