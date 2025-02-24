using System;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDal : EfEntityRepositoryBase<Product, NorthWindContext>, IProductDal
{
    public List<ProductDetailDto> GetProductDetails()
    {
        using (NorthWindContext context = new NorthWindContext())
        {
            var result = from p in context.Products
                    join c in context.Categories
                    on p.CategoryId equals c.CategoryId
                    select new ProductDetailDto
                        {
                            ProductId = p.ProductId, ProductName = p.ProductName,
                            CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock
                        };

            return result.ToList();
        }
        
    }
}
