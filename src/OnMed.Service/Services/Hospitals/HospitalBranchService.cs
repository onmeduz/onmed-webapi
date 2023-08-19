﻿using OnMed.Application.Exceptions.Files;
using OnMed.Application.Exceptions.Hospitals;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels.Hospitals;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Hospitals;
using OnMed.Service.Services.Common;

namespace OnMed.Service.Services.Hospitals;

public class HospitalBranchService : IHospitalBranchService
{
    private readonly IFileService _fileService;
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IHospitalBranchRepository _hospitalBranchRepository;
    private readonly IPaginator _paginator;

    public HospitalBranchService(IFileService fileService,
        IHospitalRepository hospitalRepository,
        IHospitalBranchRepository hospitalBranchRepository,
        IPaginator paginator)
    {
        this._fileService = fileService;
        this._hospitalRepository = hospitalRepository;
        this._hospitalBranchRepository = hospitalBranchRepository;
        this._paginator = paginator;
    }

    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(HospitalBranchCreateDto dto)
    {
        var hospital = await _hospitalRepository.GetByIdAsync(dto.HospitalId);
        if (hospital == null) throw new HospitalNotFoundException();

        string imagePath = await _fileService.UploadImageAsync(dto.Image, "hospitals");

        var hospitalBranch = new HospitalBranch();
        hospitalBranch.Name = dto.Name;
        hospitalBranch.HospitalId = dto.HospitalId;
        hospitalBranch.ImagePath = imagePath;
        hospitalBranch.Region = dto.Region;
        hospitalBranch.District = dto.District;
        hospitalBranch.Address = dto.Address;
        hospitalBranch.Destination = dto.Destination;
        hospitalBranch.AdressLatitude = dto.AdressLatitude;
        hospitalBranch.AdressLongitude = dto.AdressLongitude;
        hospitalBranch.ContactPhoneNumber = dto.ContactPhoneNumber;
        hospitalBranch.CreatedAt = hospitalBranch.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _hospitalBranchRepository.CreateAsync(hospitalBranch);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long hospitalBranchId)
    {
        var hospitalBranch = await _hospitalBranchRepository.GetByIdAsync(hospitalBranchId);
        if (hospitalBranch is null) throw new HospitalBranchNotFoundException();

        var result = await _fileService.DeleteImageAsync(hospitalBranch.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _hospitalBranchRepository.DeleteAsync(hospitalBranchId);
        return dbResult > 0;
    }

    public async Task<IList<HospitalBranchViewModel>> GetAllAsync(PaginationParams @params)
    {
        var hospitals = await _hospitalBranchRepository.GetAllAsync(@params);
        var count = await _hospitalBranchRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return hospitals;
    }

    public async Task<bool> UpdateAsync(long hospitalBranchId, HospitalBranchUpdateDto dto)
    {
        var hospitalBranch = await _hospitalBranchRepository.GetByIdAsync(hospitalBranchId);
        if (hospitalBranch == null) throw new HospitalBranchNotFoundException();
        hospitalBranch.Name = dto.Name;
        
        if (dto.Image is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(hospitalBranch.ImagePath);
            if (deleteResult == false) throw new ImageNotFoundException();
            string newImagePath = await _fileService.UploadImageAsync(dto.Image, "hospitals");
            hospitalBranch.ImagePath = newImagePath;
        }

        hospitalBranch.Region = dto.Region;
        hospitalBranch.District = dto.District;
        hospitalBranch.Address = dto.Address;
        hospitalBranch.Destination = dto.Destination;
        hospitalBranch.AdressLatitude = dto.AdressLatitude;
        hospitalBranch.AdressLongitude = dto.AdressLongitude;
        hospitalBranch.ContactPhoneNumber = dto.ContactPhoneNumber;
        hospitalBranch.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _hospitalBranchRepository.UpdateAsync(hospitalBranchId, hospitalBranch);

        return dbResult > 0;
    }
}
