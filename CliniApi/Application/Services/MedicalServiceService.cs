using AutoMapper;
using CliniApi.Application.Common;
using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;

namespace CliniApi.Application.Services
{
    public class MedicalServiceService : IMedicalServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MedicalServiceDto>>> GetAllActiveAsync()
        {
            var services = await _unitOfWork.MedicalServices.GetAllAsync();
            var activeServices = services.Where(s => s.IsActive);

            return Result<IEnumerable<MedicalServiceDto>>.Ok(
                _mapper.Map<IEnumerable<MedicalServiceDto>>(activeServices)
            );
        }

        public async Task<Result<MedicalServiceDto>> GetByIdAsync(int id)
        {
            var service = await _unitOfWork.MedicalServices.GetByIdAsync(id);

            if (service == null)
                return Result<MedicalServiceDto>.Fail("Medical service not found", StatusCodes.Status404NotFound);

            return Result<MedicalServiceDto>.Ok(_mapper.Map<MedicalServiceDto>(service));
        }

        public async Task<Result<MedicalServiceDto>> CreateAsync(CreateMedicalServiceDto dto)
        {
            if (dto.Price < 0)
                return Result<MedicalServiceDto>.Fail("Price must be greater than or equal to 0");

            var service = _mapper.Map<MedicalService>(dto);

            await _unitOfWork.MedicalServices.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return Result<MedicalServiceDto>.Created(
                _mapper.Map<MedicalServiceDto>(service),
                "Medical service created successfully"
            );
        }

        public async Task<Result<MedicalServiceDto>> UpdateAsync(int id, UpdateMedicalServiceDto dto)
        {
            var service = await _unitOfWork.MedicalServices.GetByIdAsync(id);

            if (service == null)
                return Result<MedicalServiceDto>.Fail("Medical service not found", StatusCodes.Status404NotFound);

            if (dto.Price < 0)
                return Result<MedicalServiceDto>.Fail("Price must be greater than or equal to 0");

            _mapper.Map(dto, service);

            _unitOfWork.MedicalServices.Update(service);
            await _unitOfWork.SaveChangesAsync();

            return Result<MedicalServiceDto>.Ok(
                _mapper.Map<MedicalServiceDto>(service),
                "Medical service updated successfully"
            );
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var service = await _unitOfWork.MedicalServices.GetByIdAsync(id);

            if (service == null)
                return Result.Fail("Medical service not found", StatusCodes.Status404NotFound);

            service.IsActive = false;

            _unitOfWork.MedicalServices.Update(service);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Medical service deleted successfully");
        }
    }
}
