using System;

namespace Core.Utilities.Results;

public class SuccessDataResult<T>:DataResult<T>
{

    public SuccessDataResult(T data, String message): base(data, true, message)
    {
        
    }

    public SuccessDataResult(T data): base(data, true)
    {
        
    }

    public SuccessDataResult(String message): base(default, true, message)
    {
        
    }
    public SuccessDataResult(): base(default, true)
    {
        
    }
}
