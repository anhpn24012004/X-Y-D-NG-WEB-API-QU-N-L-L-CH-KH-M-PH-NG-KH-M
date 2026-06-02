using CliniApi.Domain.Entities;
using CliniApi.Infrastructure.Data;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CliniApi.Api.Controllers
{

    public class ODataAppointmentsController : ODataController
    {
        private readonly ClinicDbContext _context;

        public ODataAppointmentsController(ClinicDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Appointment> Get()
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Include(a => a.AppointmentServices)
                    .ThenInclude(s => s.MedicalService);
        }

        [EnableQuery]
        public SingleResult<Appointment> Get([FromODataUri] int key)
        {
            return SingleResult.Create(
                _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                        .ThenInclude(d => d.Specialty)
                    .Include(a => a.AppointmentServices)
                        .ThenInclude(s => s.MedicalService)
                    .Where(a => a.AppointmentId == key)
            );
        }
    }
}