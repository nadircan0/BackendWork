using System;
using System.Data;
using Business.Abstract;
using Business.BussinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Bussiness;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;


namespace Business.Concrete;

public class ProductManager : IProductService
{

    IProductDal _productDal;
    ICategoryService _categoryService;

    public ProductManager(IProductDal productDal,ICategoryService categoryService, ILoger loger)
    {
        _productDal = productDal;
        _categoryService = categoryService;


    }

    //claim
    [SecuredOperation("product.add, admin")]
    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Add(Product product)
    {

        IResult result = BussinessRules.Run(
                        CheckIfProductNameExist(product.ProductName),
                        CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                        CheckIfCategoryLimitExceded());

        if(result != null)
        {
            return result;    
        }
        
        _productDal.Add(product);
        return new SuccessResult(Messages.ProductAdded);
    }

    [CacheAspect]
    public IDataResult<List<Product>> GetAll()
    {
        if (DateTime.Now.Hour == 19)
        {
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }

        return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
    }

    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
    }

    [CacheAspect]
    [PerformanceAspect(5)]
    public IDataResult<Product> GetById(int productId)

    {
        return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
    }

    public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
    }

    public IDataResult<List<ProductDetailDto>> GetProductDetails()
    {
        return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetais());
    }

    [ValidationAspect(typeof(ProductValidator))]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Update(Product product)
    {
        throw new NotImplementedException();
    }



    //Bussiness rules methods
    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
        var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
        if(result >= 15)
        {
            return new ErrorResult(Messages.ProductCountOfCategoryError);
        }
        return new SuccessResult();
    }


    private IResult CheckIfProductNameExist(string productName)
    {
        var result = _productDal.GetAll(p => p.ProductName == productName).Any();
        if(result == true)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExist);
        }   

        return new SuccessResult();
    }

    private IResult CheckIfCategoryLimitExceded()
    {
        var result = _categoryService.GetAll();
        if(result.Data.Count > 15)
        {
            return new ErrorResult(Messages.CategoryLimitExceded);
        }
    
        return new SuccessResult();
    }

    [TransactionScopeAspect]
    public IResult AddTransactionalTest(Product product)
    {
        throw new NotImplementedException();
    }
}
