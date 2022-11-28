

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

public class SoldOutExeption : Exception
{
    public SoldOutExeption(string msg) : base(msg) { }
}

public class DataMissingException : Exception
{
    public DataMissingException(string msg) : base(msg) { }
}

public class ItemMissingException : Exception
{
    public ItemMissingException(string msg) : base(msg) { }
}

public class InvalidInputBO : Exception
{
    public InvalidInputBO(string msg) : base(msg) { }
}
public class noItemsInTheCart : Exception
{
    public noItemsInTheCart(string msg) : base(msg) { }
}