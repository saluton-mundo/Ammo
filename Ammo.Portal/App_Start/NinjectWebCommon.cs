[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Ammo.Portal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Ammo.Portal.App_Start.NinjectWebCommon), "Stop")]

namespace Ammo.Portal.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using Ammo.Domain.Services.Abstract;
    using Ammo.Domain.Services.Concrete;

    using Ammo.Domain.Repositories.Abstract;
    using Ammo.Domain.Repositories.Concrete;
    using Domain.Repositories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                // DAL bindings
                RegisterRepositories(kernel);

                // BLL bindings
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBaseService>().To<BaseService>();
            kernel.Bind<IBookmarkService>().To<BookmarkService>();
            kernel.Bind<IBulletService>().To<BulletService>();
            kernel.Bind<IBulletCollectionService>().To<BulletCollectionService>();
            kernel.Bind<IBulletCollectionBulletService>().To<BulletCollectionBulletService>();
            kernel.Bind<IJournalService>().To<JournalService>();
            kernel.Bind<IJournalIndexService>().To<JournalIndexService>();
            kernel.Bind<IJournalCoverService>().To<JournalCoverService>();
            kernel.Bind<IJournalTagService>().To<JournalTagService>();
        }        

        private static void RegisterRepositories(IKernel kernel)
        {
            kernel.Bind<IBookmarkRepository>().To<BookmarkRepository>();
            kernel.Bind<IBulletRepository>().To<BulletRepository>();
            kernel.Bind<IBulletCollectionRepository>().To<BulletCollectionRepository>();
            kernel.Bind<IBulletCollectionBulletRepository>().To<BulletCollectionBulletRepository>();
            kernel.Bind<IJournalRepository>().To<JournalRepository>();
            kernel.Bind<IJournalIndexRepository>().To<JournalIndexRepository>();
            kernel.Bind<IJournalTagRepository>().To<JournalTagRepository>();
            kernel.Bind<ISubscriptionRepository>().To<SubscriptionRepository>();
        }
    }
}
