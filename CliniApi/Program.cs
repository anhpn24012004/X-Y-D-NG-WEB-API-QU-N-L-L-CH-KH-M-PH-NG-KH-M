using CliniApi.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters;
using CliniApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CliniApi.Application.Interfaces;
using CliniApi.Application.Mappings;
using CliniApi.Infrastructure.UnitOfWork;
using CliniApi.Application.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using CliniApi.Domain.Entities;
using AppointmentServiceImpl = CliniApi.Application.Services.AppointmentService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

builder.Services.AddAutoMapper(typeof(ClinicProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicalServiceService, MedicalServiceService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentServiceImpl>();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new CsvOutputFormatter());
    options.InputFormatters.Add(new CsvInputFormatter());

    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
})
    .AddOData(options =>
    {
        options.Select()
            .Filter()
            .OrderBy()
            .Count()
            .Expand()
            .SetMaxTop(100)
            .EnableQueryFeatures();

        options.AddRouteComponents("odata", GetEdmModel());
    });

// Swagger for .NET 8
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    builder.EntitySet<Patient>("ODataPatients");
    builder.EntitySet<Appointment>("ODataAppointments");

    return builder.GetEdmModel();
}