using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using parte_dos.Repositorys;
using parte_dos.Services;
using Microsoft.Practices.Unity.WebApi;
using System.Web.Http;
using parte_dos.Models;
using System.Collections.Generic;

namespace parte_dos.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private UnityContainer container;

        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IPersonaService, PersonaService>(
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<DBInterceptor>());
            container.RegisterType<IPersonaRepository, PersonaRepository>();


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }


    public class DBInterceptor : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input,
          GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result;
            if (ApplicationDbContext.applicationDbContext == null)
            {
                using (var context = new ApplicationDbContext())
                {
                    ApplicationDbContext.applicationDbContext = context;
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            result = getNext()(input, getNext);


                            if (result.Exception != null)
                            {
                                throw result.Exception;
                            }
                            context.SaveChanges();

                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("He hecho rollback de la transacción", e);
                        }
                    }
                }
                ApplicationDbContext.applicationDbContext = null;
            }
            else
            {

                result = getNext()(input, getNext);


                if (result.Exception != null)
                {
                    throw result.Exception;
                }
            }
            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private void WriteLog(string message)
        {

        }
    }
}