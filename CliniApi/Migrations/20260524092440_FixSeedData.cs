using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CliniApi.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MedicalServices",
                columns: new[] { "ServiceId", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, "General Check-up", 200000m },
                    { 2, true, "Blood Test", 150000m },
                    { 3, true, "Ultrasound", 300000m },
                    { 4, true, "Electrocardiogram", 250000m },
                    { 5, true, "X-ray", 350000m },
                    { 6, true, "Pediatric Consultation", 180000m }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Address", "DateOfBirth", "FullName", "Gender", "Phone" },
                values: new object[,]
                {
                    { 1, "Ha Noi", new DateTime(1998, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Minh Duc", "Male", "0912000001" },
                    { 2, "Hai Phong", new DateTime(2000, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tran Thi Lan", "Female", "0912000002" },
                    { 3, "Da Nang", new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Le Hoang Nam", "Male", "0912000003" },
                    { 4, "Ho Chi Minh City", new DateTime(2002, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pham Ngoc Mai", "Female", "0912000004" },
                    { 5, "Can Tho", new DateTime(2015, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoang Gia Bao", "Male", "0912000005" },
                    { 6, "Bac Ninh", new DateTime(1988, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Do Thanh Tam", "Female", "0912000006" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "SpecialtyId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "General health examination", "General Medicine" },
                    { 2, "Heart and blood vessel care", "Cardiology" },
                    { 3, "Medical care for children", "Pediatrics" },
                    { 4, "Imaging and diagnostic services", "Radiology" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "Email", "FullName", "IsActive", "Phone", "SpecialtyId" },
                values: new object[,]
                {
                    { 1, "an.nguyen@clinic.com", "Dr. Nguyen Van An", true, "0901000001", 1 },
                    { 2, "binh.tran@clinic.com", "Dr. Tran Thi Binh", true, "0901000002", 2 },
                    { 3, "chau.le@clinic.com", "Dr. Le Minh Chau", true, "0901000003", 3 },
                    { 4, "dung.pham@clinic.com", "Dr. Pham Quoc Dung", true, "0901000004", 4 },
                    { 5, "ha.hoang@clinic.com", "Dr. Hoang Thu Ha", true, "0901000005", 1 },
                    { 6, "khoa.do@clinic.com", "Dr. Do Manh Khoa", false, "0901000006", 2 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentTime", "CreatedAt", "DoctorId", "Note", "PatientId", "Reason", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, "First visit", 1, "Regular health check", "Scheduled" },
                    { 2, new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 9, 10, 0, 0, DateTimeKind.Unspecified), 2, "Need ECG", 2, "Chest pain", "Scheduled" },
                    { 3, new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 9, 20, 0, 0, DateTimeKind.Unspecified), 1, "Completed successfully", 3, "Fever", "Completed" },
                    { 4, new DateTime(2026, 6, 2, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, "Patient cancelled", 4, "Child cough", "Cancelled" },
                    { 5, new DateTime(2026, 6, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 9, 40, 0, 0, DateTimeKind.Unspecified), 3, "Bring previous record", 5, "Pediatric consultation", "Scheduled" },
                    { 6, new DateTime(2026, 6, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 9, 50, 0, 0, DateTimeKind.Unspecified), 4, "Fasting required", 1, "Abdominal ultrasound", "Scheduled" },
                    { 7, new DateTime(2026, 6, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), 5, "Stable condition", 2, "Follow-up check", "Completed" },
                    { 8, new DateTime(2026, 6, 4, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 22, 10, 10, 0, 0, DateTimeKind.Unspecified), 2, "Monitor blood pressure", 6, "Heart check", "Scheduled" }
                });

            migrationBuilder.InsertData(
                table: "AppointmentServices",
                columns: new[] { "AppointmentId", "ServiceId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 200000m },
                    { 1, 2, 1, 150000m },
                    { 2, 4, 1, 250000m },
                    { 3, 1, 1, 200000m },
                    { 4, 6, 1, 180000m },
                    { 5, 6, 1, 180000m },
                    { 6, 3, 1, 300000m },
                    { 7, 1, 1, 200000m },
                    { 8, 4, 1, 250000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "AppointmentServices",
                keyColumns: new[] { "AppointmentId", "ServiceId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MedicalServices",
                keyColumn: "ServiceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MedicalServices",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MedicalServices",
                keyColumn: "ServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MedicalServices",
                keyColumn: "ServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MedicalServices",
                keyColumn: "ServiceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MedicalServices",
                keyColumn: "ServiceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "SpecialtyId",
                keyValue: 4);
        }
    }
}
