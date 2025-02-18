using System;
using System.Runtime.CompilerServices;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract;

public interface IProductDal: IEntityRepository<Product>
{

    List<ProductDetailDto> GetProductDetais();
    

}
