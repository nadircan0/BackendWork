using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICategoryDal : IEntityRepository<Category>
{
   
}
