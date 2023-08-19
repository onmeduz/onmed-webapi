﻿using OnMed.Application.Utils;
using OnMed.Domain.Entities.Categories;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Persistance.Dtos.Doctors;

namespace OnMed.Service.Interfaces.Doctors;

public interface IDoctorService
{
    public Task<bool> CreateAsync(DoctorCreateDto dto);
    public Task<bool> DeleteAsync(long doctorId);
    public Task<long> CountByHospitalAsync(long hospitalId);
    public Task<IList<Category>> GetAllByHospitalAsync(long hospitalId, PaginationParams @params);
    public Task<Category> GetByIdAsync(long doctorId);
    public Task<bool> UpdateAsync(long doctorId, DoctorUpdateDto dto);
}
