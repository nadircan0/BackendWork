using System;
using System.Runtime.CompilerServices;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IProductDal: IEntityRepository<Product>
{



}
