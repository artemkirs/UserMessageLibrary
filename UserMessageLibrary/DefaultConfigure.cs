using Autofac;
using System.Reflection;
using UserMessageLibrary.Repository.ContactRep;
using UserMessageLibrary.Repository.MessageRep;
using UserMessageLibrary.Repository.UserRep;

namespace UserMessageLibrary
{
    /// <summary>
    /// Настройка библиотеки при её запуске
    /// </summary>
    public class DefaultConfigureModule : Autofac.Module
    {

        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public DefaultConfigureModule(bool isDevelopment, Assembly? callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var coreAssembly =
              Assembly.GetAssembly(typeof(User));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
            if (coreAssembly != null)
            {
                _assemblies.Add(coreAssembly);
            }

            if (infrastructureAssembly != null)
            {
                _assemblies.Add(infrastructureAssembly);
            }

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            RegisterCommonDependencies(builder);
        }

        /// <summary>
        /// Внедрение зависимостей
        /// </summary>
        /// <param name="builder"></param>
        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope(); ;
            builder.RegisterType<ContactRepository>().As<IContactRepository>().InstancePerLifetimeScope(); ;
            builder.RegisterType<MessageRepository>().As<IMessageRepository>().InstancePerLifetimeScope(); ;


        }
    }
}
