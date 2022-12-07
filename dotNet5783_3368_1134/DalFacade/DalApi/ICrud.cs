
using DO;
namespace DalApi;

public interface ICrud<T>
{
    int Add(T entity);  
    void Delete(int id);
    T GetById(int id);      
    void Update(T entity);
    IEnumerable<T> GetAll();
    int ListLength();

}
