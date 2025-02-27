
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.AutoFac;

public class AutoFacBusinsessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
        builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

        builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();


 



        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

    }


}



// DEPENDENCY INJECTİON
// //💡 Bu Yapı Ne İşe Yarıyor?
// 	1.	Nesne bağımlılıklarını yönetiyor:
// 	•	Normalde, ProductManager nesnesinin IProductService olarak kullanılmasını istiyoruz.
// 	•	EfProductDal nesnesinin de IProductDal olarak enjekte edilmesini sağlıyoruz.
// 	2.	Bağımlılıkları manuel oluşturmak yerine otomatik bağlıyor:
// 	•	Eğer Autofac kullanmasaydık, ProductManager nesnesini her yerde new ile oluşturmamız gerekecekti: