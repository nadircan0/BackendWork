using System;

namespace Core.Utilities.Results;



public class ErrorDataResult<T>:DataResult<T>
{

    public ErrorDataResult(T data, String message): base(data, false, message)
    {
        
    }

    public ErrorDataResult(T data): base(data, false)
    {
        
    }

    public ErrorDataResult(String message): base(default, false, message)
    {
        
    }
    public ErrorDataResult(): base(default, false)
    {
        
    }
}

