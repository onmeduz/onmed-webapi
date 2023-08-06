namespace OnMed.DataAccess.Interfaces;

public interface IRepository<TEntity, TViewModel>
{
    public Task<int> CreateAsync(TEntity entity);

    public Task<int> UpdateAsync(long id, TEntity entity);

    public Task<int> DeleteAsync(long id);

    public Task<TEntity?> GetByIdAsync(long id);

    public Task<TViewModel?> GetByIdViewModelAsync(long id);

    public Task<long> CountAsync();
}
