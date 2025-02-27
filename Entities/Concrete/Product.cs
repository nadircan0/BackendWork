using System;
using Core.Entities;


namespace Entities.Concrete;

public class Product : IEntity
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public String ProductName { get; set; }
    public short UnitsInStock { get; set; }
    public decimal UnitPrice { get; set; }

}
 