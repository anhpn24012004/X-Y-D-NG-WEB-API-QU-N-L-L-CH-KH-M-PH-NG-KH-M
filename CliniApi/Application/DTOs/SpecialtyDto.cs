namespace CliniApi.Application.DTOs
{
    public class SpecialtyDto
    {
        public int SpecialtyId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}