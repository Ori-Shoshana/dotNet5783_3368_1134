
namespace DO;
public class IdNotExistException : Exception
{
    public  IdNotExistException(string msg) : base(msg) { }
}

public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string msg) : base(msg) { }
}

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

