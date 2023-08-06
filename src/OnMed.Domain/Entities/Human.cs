using System.ComponentModel.DataAnnotations;

namespace OnMed.Domain.Entities;

public abstract class Human : Auditable
{
    [MaxLength(20)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(20)]
    public string LastName { get; set; } = String.Empty;

    [MaxLength(20)]
    public string MiddleName { get; set; } = String.Empty;

    public DateOnly BirthDay { get; set; }

    [MaxLength(13)]
    public string PhoneNumber { get; set; } = String.Empty;

    public bool IsMale { get; set; }

    public string ImagePath { get; set; } = String.Empty;

    public string Region { get; set; } = String.Empty;

    public string PasswordHash { get; set; } = String.Empty;

    public string Salt { get; set; } = String.Empty;
}
