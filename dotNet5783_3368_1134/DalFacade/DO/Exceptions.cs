
namespace DO;
public class IdNotExistException : Exception
{
    public  IdNotExistException(string msg) : base(msg) { }
}

public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string msg) : base(msg) { }
}