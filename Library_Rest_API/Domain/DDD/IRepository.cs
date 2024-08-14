namespace Library_Rest_API.Domain.DDD
{
    public interface IRepository<ID, T>
    {
        Task<int> SaveAsync(T obj);
        Task<T?> GetByIdAsync(ID id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
