using System;
using System.Linq.Expressions;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICustomerDal : IEntityRepository<Customer>
{
   
}
