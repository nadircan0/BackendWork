using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Utilities.Results;

// general voids
public interface IResult
{
    bool Success { get; }
    String Message { get; }

}
