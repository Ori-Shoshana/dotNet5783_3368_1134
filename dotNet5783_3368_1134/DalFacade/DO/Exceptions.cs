
namespace DO;
/// <summary>
/// class exceptions - contains  different types of exceptions
/// </summary>
//if the id does not exist
public class IdNotExistException : Exception
{
    public  IdNotExistException(string msg) : base(msg) { }
}
//if the id already exists
public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string msg) : base(msg) { }
}
// if the product/order is not found
public class NoObjectFoundExeption : Exception
{
    public NoObjectFoundExeption(string msg) : base(msg) { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

