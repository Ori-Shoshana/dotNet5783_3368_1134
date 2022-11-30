/// <summary>
/// class for exceptions
/// </summary>
namespace BO;

/// <summary>
/// if the variable not exists
/// </summary>
public class VeriableNotExistException : Exception
{
    public VeriableNotExistException(string msg) : base(msg) { }
}
/// <summary>
/// if the variable already exists
/// </summary>
public class VeriableAlreadyExistException : Exception
{
    public VeriableAlreadyExistException(string msg) : base(msg) { }
}
/// <summary>
/// if the variable is smaller than zero
/// </summary>
public class VariableIsSmallerThanZeroExeption : Exception
{
    public VariableIsSmallerThanZeroExeption(string msg) : base(msg) { }
}
/// <summary>
/// if the variable is null
/// </summary>
public class VariableIsNullExeption : Exception 
{
    public VariableIsNullExeption(string msg) : base(msg) { }   
}
/// <summary>
/// if the input is invalid
/// </summary>
public class InvalidInputExeption : Exception
{
    public InvalidInputExeption(string msg) : base(msg) { }
}
/// <summary>
/// if the id not exits
/// </summary>
public class IdNotExistException : Exception
{
    public IdNotExistException(string msg) : base(msg) { }
}
/// <summary>
/// if the id already exits
/// </summary>
public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string msg) : base(msg) { }
}
