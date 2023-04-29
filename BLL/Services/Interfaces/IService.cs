namespace BLL.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
