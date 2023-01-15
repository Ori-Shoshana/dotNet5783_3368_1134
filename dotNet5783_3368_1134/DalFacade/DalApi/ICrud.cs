
namespace DalApi;
/// <summary>
/// interface Icrud decleration of all the functions
/// </summary>
public interface ICrud<T> where T : struct
{
    int Add(T entity);  
    void Delete(int id);
    T GetById(int id);      
    void Update(T entity);
    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);
    int ListLength();
    T GetByDelegate(Func<T?, bool>? func); 
}
