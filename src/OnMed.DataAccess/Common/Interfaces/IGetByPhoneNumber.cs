namespace OnMed.DataAccess.Common.Interfaces;

public interface IGetByPhoneNumber<TEntity>
{
    public Task<TEntity?> GetByPhoneNumberAsync(string phoneNumber);
}
