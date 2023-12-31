﻿namespace OnMed.DataAccess.ViewModels.Hospitals;

public class HospitalBranchForCommonViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long HospitalId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public double AdressLatitude { get; set; }
    public double AdressLongitude { get; set; }
    public string ContactPhoneNumber { get; set; } = string.Empty;
}
