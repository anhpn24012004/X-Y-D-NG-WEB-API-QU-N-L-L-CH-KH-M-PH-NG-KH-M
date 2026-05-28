using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CliniApi.Api.Controllers
{

    [Route("api/[controller]")]
    public class AppointmentsController : BaseApiController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var result = await _appointmentService.CreateAsync(dto);
            return HandleResult(result);
        }

        [HttpPut("{id}/cancel")]
        [Consumes("application/json")]
        public async Task<IActionResult> Cancel(int id, CancelAppointmentDto dto)
        {
            var result = await _appointmentService.CancelAsync(id, dto);
            return HandleResult(result);
        }

        [HttpPut("{id}/complete")]
        [Consumes("application/json")]
        public async Task<IActionResult> Complete(int id, CompleteAppointmentDto dto)
        {
            var result = await _appointmentService.CompleteAsync(id, dto);
            return HandleResult(result);
        }
    }
}