using System;
using Entities.Concrete;

namespace Business.Constants;

public static class Messages
{
    public static string ProductAdded = "Product added";   
    public static string ProductNameInvalid = "Product name invalid";
    internal static string MaintenanceTime = "Maintenance Time";
    internal static string ProductsListed = "Products Listed";
    internal static string ProductNameAlreadyExist = "product name allready exist";
    internal static string CategoryLimitExceded = "Category Limit Exceded";
    internal static string ProductCountOfCategoryError = "There are max 10 product in each category";
}
