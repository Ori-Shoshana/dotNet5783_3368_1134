

namespace BO;
public class VeriableNotExistException : Exception
{
    public VeriableNotExistException(string msg) : base(msg) { }
}

public class VeriableAlreadyExistException : Exception
{
    public VeriableAlreadyExistException(string msg) : base(msg) { }
}

public class VariableIsSmallerThanZeroExeption : Exception
{
    public VariableIsSmallerThanZeroExeption(string msg) : base(msg) { }
}

public class VariableIsNullExeption : Exception 
{
    public VariableIsNullExeption(string msg) : base(msg) { }   
}

public class InvalidInputExeption : Exception
{
    public InvalidInputExeption(string msg) : base(msg) { }
}
