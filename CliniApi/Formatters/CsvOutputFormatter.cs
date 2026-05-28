using CliniApi.Application.Common;
using CliniApi.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace CliniApi.Formatters;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type? type)
    {
        return true;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var data = ExtractData(context.Object);
        var csv = new StringBuilder();

        switch (data)
        {
            case IEnumerable<DoctorDto> items:
                csv.AppendLine("DoctorId,FullName,Email,Phone,SpecialtyId,SpecialtyName,IsActive");
                foreach (var x in items)
                    csv.AppendLine($"{x.DoctorId},{E(x.FullName)},{E(x.Email)},{E(x.Phone)},{x.SpecialtyId},{E(x.SpecialtyName)},{x.IsActive}");
                break;

            case DoctorDto x:
                csv.AppendLine("DoctorId,FullName,Email,Phone,SpecialtyId,SpecialtyName,IsActive");
                csv.AppendLine($"{x.DoctorId},{E(x.FullName)},{E(x.Email)},{E(x.Phone)},{x.SpecialtyId},{E(x.SpecialtyName)},{x.IsActive}");
                break;

            case IEnumerable<PatientDto> items:
                csv.AppendLine("PatientId,FullName,DateOfBirth,Gender,Phone,Address");
                foreach (var x in items)
                    csv.AppendLine($"{x.PatientId},{E(x.FullName)},{x.DateOfBirth:yyyy-MM-dd},{E(x.Gender)},{E(x.Phone)},{E(x.Address)}");
                break;

            case PatientDto x:
                csv.AppendLine("PatientId,FullName,DateOfBirth,Gender,Phone,Address");
                csv.AppendLine($"{x.PatientId},{E(x.FullName)},{x.DateOfBirth:yyyy-MM-dd},{E(x.Gender)},{E(x.Phone)},{E(x.Address)}");
                break;

            case IEnumerable<MedicalServiceDto> items:
                csv.AppendLine("ServiceId,Name,Price,IsActive");
                foreach (var x in items)
                    csv.AppendLine($"{x.ServiceId},{E(x.Name)},{x.Price},{x.IsActive}");
                break;

            case MedicalServiceDto x:
                csv.AppendLine("ServiceId,Name,Price,IsActive");
                csv.AppendLine($"{x.ServiceId},{E(x.Name)},{x.Price},{x.IsActive}");
                break;

            case IEnumerable<SpecialtyDto> items:
                csv.AppendLine("SpecialtyId,Name,Description");
                foreach (var x in items)
                    csv.AppendLine($"{x.SpecialtyId},{E(x.Name)},{E(x.Description)}");
                break;

            case SpecialtyDto x:
                csv.AppendLine("SpecialtyId,Name,Description");
                csv.AppendLine($"{x.SpecialtyId},{E(x.Name)},{E(x.Description)}");
                break;

            case IEnumerable<AppointmentDto> items:
                csv.AppendLine("AppointmentId,PatientId,PatientName,DoctorId,DoctorName,SpecialtyName,AppointmentTime,Status,Reason,Note,TotalAmount");
                foreach (var x in items)
                    csv.AppendLine($"{x.AppointmentId},{x.PatientId},{E(x.FullName)},{x.DoctorId},{E(x.DoctorName)},{E(x.SpecialtyName)},{x.AppointmentTime:yyyy-MM-dd HH:mm:ss},{E(x.Status)},{E(x.Reason)},{E(x.Note)},{x.TotalAmount}");
                break;

            case AppointmentDto x:
                csv.AppendLine("AppointmentId,PatientId,PatientName,DoctorId,DoctorName,SpecialtyName,AppointmentTime,Status,Reason,Note,TotalAmount");
                csv.AppendLine($"{x.AppointmentId},{x.PatientId},{E(x.FullName)},{x.DoctorId},{E(x.DoctorName)},{E(x.SpecialtyName)},{x.AppointmentTime:yyyy-MM-dd HH:mm:ss},{E(x.Status)},{E(x.Reason)},{E(x.Note)},{x.TotalAmount}");
                break;
        }

        await context.HttpContext.Response.WriteAsync(csv.ToString(), selectedEncoding);
    }

    private static object? ExtractData(object? obj)
    {
        if (obj == null) return null;

        var dataProperty = obj.GetType().GetProperty("Data", BindingFlags.Public | BindingFlags.Instance);
        return dataProperty != null ? dataProperty.GetValue(obj) : obj;
    }

    private static string E(string? value)
    {
        if (string.IsNullOrEmpty(value)) return "";

        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";

        return value;
    }
}