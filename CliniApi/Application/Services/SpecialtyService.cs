using AutoMapper;
using CliniApi.Application.Common;
using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;

namespace CliniApi.Application.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpecialtyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SpecialtyDto>>> GetAllAsync()
        {
            var specialties = await _unitOfWork.Specialties.GetAllAsync();
            var data = _mapper.Map<IEnumerable<SpecialtyDto>>(specialties);
            
            return Result<IEnumerable<SpecialtyDto>>.Ok(data);
        }

        public async Task<Result<SpecialtyDto>> GetByIdAsync(int id)
        {
            var specialty = await _unitOfWork.Specialties.GetByIdAsync(id);

            if (specialty == null)
            {
                return Result<SpecialtyDto>.Fail(
                    "Specialty not found",
                    StatusCodes.Status404NotFound
                 );
            }

            var data = _mapper.Map<SpecialtyDto>(specialty);
            return Result<SpecialtyDto>.Ok(data);
        }
    }
}
