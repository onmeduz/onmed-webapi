using AutoMapper;
using OnMed.Application.Exceptions.Files;
using OnMed.Application.Exceptions.Hospitals;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Hospitals;

namespace OnMed.Service.Services.Hospitals;

public class HospitalService : IHospitalService
{
    private readonly IFileService _fileService;
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IPaginator _paginator;
    private readonly IMapper _mapper;

    public HospitalService(IHospitalRepository hospitalRepository,
        IFileService fileService,
        IPaginator paginator,
        IMapper mapper)
    {
        this._fileService = fileService;
        this._hospitalRepository = hospitalRepository;
        this._paginator = paginator;
        this._mapper = mapper;
    }

    public async Task<long> CountAsync()
    {
        var hospitals = await _hospitalRepository.CountAsync();
        return hospitals;
    }

    public async Task<bool> CreateAsync(HospitalCreateDto dto)
    {
        // bu yerda ham ishlatdim!
        string imagePath = await _fileService.UploadImageAsync(dto.BrandImage, "hospitals");
        var hospital = _mapper.Map<Hospital>(dto);
        hospital.BrandImagePath = imagePath;
        hospital.CreatedAt = hospital.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _hospitalRepository.CreateAsync(hospital);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long hospitalId)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(hospitalId);
        if (hospital is null) throw new HospitalNotFoundException();

        var result = await _fileService.DeleteImageAsync(hospital.BrandImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _hospitalRepository.DeleteAsync(hospitalId);
        return dbResult > 0;
    }

    public async Task<IList<Hospital>> GetAllAsync(PaginationParams @params)
    {
        var hospitals = await _hospitalRepository.GetAllAsync(@params);
        var count = await _hospitalRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return hospitals;
    }

    public async Task<IList<Hospital>> SearchAsync(string search)
    {
        var searches = await _hospitalRepository.SearchAsync(search);
        return searches;
    }

    public async Task<bool> UpdateAsync(long hospitalId, HospitalUpdateDto dto)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(hospitalId);
        if (hospital == null) throw new HospitalNotFoundException();
        hospital.Name = dto.Name;
        hospital.LegalName = dto.LegalName;
        hospital.AdministratorPhoneNumber = dto.AdministratorPhoneNumber;
        hospital.FaxPhoneNumber = dto.FaxPhoneNumber;
        hospital.Description = dto.Description;
        hospital.Email = dto.Email;
        hospital.Website = dto.Website;

        if (dto.BrandImage is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(hospital.BrandImagePath);
            if (deleteResult == false) throw new ImageNotFoundException();
            string newImagePath = await _fileService.UploadImageAsync(dto.BrandImage, "hospitals");
            hospital.BrandImagePath = newImagePath;
        }

        hospital.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _hospitalRepository.UpdateAsync(hospitalId, hospital);

        return dbResult > 0;
    }
}
