using CliniApi.Domain.Entities;
using CliniApi.Infrastructure.Data;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CliniApi.Api.Controllers
{
    public class ODataPatientsController : ODataController
    {
        private readonly ClinicDbContext _context;

        public ODataPatientsController(ClinicDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Patient> Get()
        {
            return _context.Patients;
        }

        [EnableQuery]
        public SingleResult<Patient> Get([FromODataUri] int key)
        {
            return SingleResult.Create(
                _context.Patients.Where(p => p.PatientId == key)
            );
        }
    }
}