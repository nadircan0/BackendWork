using System;

namespace Core.Utilities.Results;

public class DataResult<T> : Result, IDataResult<T>
{

    public T Data {get;}

    public DataResult(T data, bool success, String message):base(success, message)
    {
        Data = data;
    }

    public DataResult(T data, bool success):base(success)
    {
        Data = data;
    }
    
}
