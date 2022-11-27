

namespace BO;
public class IdNotExistException : Exception
{
    public IdNotExistException(string msg) : base(msg) { }
}

public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string msg) : base(msg) { }
}

public class VariableIsSmallerThanZeroExeption : Exception
{
    public VariableIsSmallerThanZeroExeption(string msg) : base(msg) { }
}

public class VariableIsNullExeption : Exception 
{
    public VariableIsNullExeption(string msg) : base(msg) { }   
}


