using System;
using Entities.Abstract;

namespace Entities.Concrete;

public class Customer : IEntity
{

    public String CustomerId { get; set; }
    public String ContactName { get; set; }
    public String CompanyName { get; set; }
    public String City { get; set; }


}
