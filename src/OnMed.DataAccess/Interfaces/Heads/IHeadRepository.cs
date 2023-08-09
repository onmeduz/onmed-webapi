using OnMed.DataAccess.Common.Interfaces;
using OnMed.Domain.Entities.Heads;

namespace OnMed.DataAccess.Interfaces.Heads;

public interface IHeadRepository : IRepository<Head>, IGetByPhoneNumber<Head?>
{}
