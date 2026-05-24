using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CliniApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class PatientsController : BaseApiController
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        private async Task<IActionResult> GetById(int id)
        {
            var result = await _patientService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
            var result = await _patientService.CreateAsync(dto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePatientDto dto)
        {
            var result = await _patientService.UpdateAsync(id, dto);
            return HandleResult(result);
        }
    }
}
