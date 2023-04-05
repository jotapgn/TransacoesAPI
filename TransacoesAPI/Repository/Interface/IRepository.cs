
namespace TransacoesAPI.Repository.Base
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
