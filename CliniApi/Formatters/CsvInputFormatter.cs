using CliniApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Text;

namespace CliniApi.Formatters;

public class CsvInputFormatter : TextInputFormatter
{
    public CsvInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(CreateDoctorDto)
            || type == typeof(UpdateDoctorDto)
            || type == typeof(CreatePatientDto)
            || type == typeof(UpdatePatientDto)
            || type == typeof(CreateMedicalServiceDto)
            || type == typeof(UpdateMedicalServiceDto)
            || type == typeof(CreateAppointmentDto);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        using var reader = new StreamReader(context.HttpContext.Request.Body, encoding);
        var content = await reader.ReadToEndAsync();

        if (string.IsNullOrWhiteSpace(content))
            return await InputFormatterResult.FailureAsync();

        var lines = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 2)
            return await InputFormatterResult.FailureAsync();

        var values = SplitCsvLine(lines[1]);

        try
        {
            if (context.ModelType == typeof(CreateDoctorDto))
            {
                return await InputFormatterResult.SuccessAsync(new CreateDoctorDto
                {
                    FullName = values[0],
                    Email = values[1],
                    Phone = values[2],
                    SpecialtyId = int.Parse(values[3]),
                    IsActive = bool.Parse(values[4])
                });
            }

            if (context.ModelType == typeof(UpdateDoctorDto))
            {
                return await InputFormatterResult.SuccessAsync(new UpdateDoctorDto
                {
                    FullName = values[0],
                    Email = values[1],
                    Phone = values[2],
                    SpecialtyId = int.Parse(values[3]),
                    IsActive = bool.Parse(values[4])
                });
            }

            if (context.ModelType == typeof(CreatePatientDto))
            {
                return await InputFormatterResult.SuccessAsync(new CreatePatientDto
                {
                    FullName = values[0],
                    DateOfBirth = DateTime.Parse(values[1]),
                    Gender = values[2],
                    Phone = values[3],
                    Address = values[4]
                });
            }

            if (context.ModelType == typeof(UpdatePatientDto))
            {
                return await InputFormatterResult.SuccessAsync(new UpdatePatientDto
                {
                    FullName = values[0],
                    DateOfBirth = DateTime.Parse(values[1]),
                    Gender = values[2],
                    Phone = values[3],
                    Address = values[4]
                });
            }

            if (context.ModelType == typeof(CreateMedicalServiceDto))
            {
                return await InputFormatterResult.SuccessAsync(new CreateMedicalServiceDto
                {
                    Name = values[0],
                    Price = decimal.Parse(values[1], CultureInfo.InvariantCulture),
                    IsActive = bool.Parse(values[2])
                });
            }

            if (context.ModelType == typeof(UpdateMedicalServiceDto))
            {
                return await InputFormatterResult.SuccessAsync(new UpdateMedicalServiceDto
                {
                    Name = values[0],
                    Price = decimal.Parse(values[1], CultureInfo.InvariantCulture),
                    IsActive = bool.Parse(values[2])
                });
            }

            if (context.ModelType == typeof(CreateAppointmentDto))
            {
                var services = values[5]
                    .Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s =>
                    {
                        var parts = s.Split(':');
                        return new CreateAppointmentServiceItemDto
                        {
                            ServiceId = int.Parse(parts[0]),
                            Quantity = int.Parse(parts[1])
                        };
                    })
                    .ToList();

                return await InputFormatterResult.SuccessAsync(new CreateAppointmentDto
                {
                    PatientId = int.Parse(values[0]),
                    DoctorId = int.Parse(values[1]),
                    AppointmentTime = DateTime.Parse(values[2]),
                    Reason = values[3],
                    Note = values[4],
                    Services = services
                });
            }

            return await InputFormatterResult.FailureAsync();
        }
        catch
        {
            return await InputFormatterResult.FailureAsync();
        }
    }

    private static List<string> SplitCsvLine(string line)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        var insideQuotes = false;

        foreach (var c in line)
        {
            if (c == '"')
            {
                insideQuotes = !insideQuotes;
                continue;
            }

            if (c == ',' && !insideQuotes)
            {
                result.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        result.Add(current.ToString().Trim());
        return result;
    }
}