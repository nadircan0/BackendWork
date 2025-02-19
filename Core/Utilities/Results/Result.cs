using System;

namespace Core.Utilities.Results;

public class Result : IResult
{

    

    public Result(bool success, string message):this(success)
    {

        // properties can be set in constructure which is read only (only have get method)
        Message = message;

    }

      public Result(bool success)
    {
        Success = success;
    }

    public bool Success { get;  }

    public string Message { get;  }



}
