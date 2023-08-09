﻿using OnMed.Domain.Entities;

namespace Onmed.Domain.Entities.Doctors;

public class DoctorCategories : Auditable
{
    public long DoctorId { get; set; }
    public long CategoryId { get; set; }
}
