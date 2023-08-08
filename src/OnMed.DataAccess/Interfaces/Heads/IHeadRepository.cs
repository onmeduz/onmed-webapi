﻿using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels;
using OnMed.Domain.Entities.Heads;

namespace OnMed.DataAccess.Interfaces.Heads;

public interface IHeadRepository : IRepository<Head,HeadViewModel>,
    IGetAll<HeadViewModel> , ISearchable<HeadViewModel>
{

}