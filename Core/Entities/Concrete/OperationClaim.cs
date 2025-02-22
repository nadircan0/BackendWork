using System;
using Microsoft.Identity.Client.Extensibility;

namespace Core.Entities.Concrete;

public class OperationClaim : IEntity
{


    public int Id { get; set; }
    public String Name { get; set; }

}
