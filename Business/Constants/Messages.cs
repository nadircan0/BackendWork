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
    internal static string AuthorizationDenied = "Authorization Denied";
    internal static string UserRegistered = "User Registered";
    internal static string UserNotFound = "User Not Found";
    internal static string PasswordError = "Password Error";
    internal static string SuccessfulLogin = "Successful Login";
    internal static string UserAlreadyExist = "User Already Exist";
    internal static string AccessTokenCreated = "Access Token Created";
    internal static string ProductCountOfCategoryError = "There are max 10 product in each category";
}
