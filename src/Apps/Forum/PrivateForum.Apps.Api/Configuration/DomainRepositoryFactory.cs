using Autofac;
using PrivateForum.Core;
using PrivateForum.Core.Framework.Commanding;
using System;

namespace PrivateForum.App.Web.Configuration
{
    public class DomainRepositoryFactory : IDomainRepositoryFactory
    {
        private IContainer _container;

        public DomainRepositoryFactory(IContainer container)
        {
            _container = container;
        }

        public TRepository Provide<TRepository>(IConnectionFactory connection) where TRepository : IDomainRepository
        {
            var repository = (IDomainRepository)_container.Resolve<TRepository>();
            if (repository == null)
            {
                if ((typeof(TRepository) as IDomainRepository) is IDomainRepository)
                {
                    throw new Exception($"Unable to find {typeof(TRepository).Name}.");
                }
                else
                {
                    throw new Exception($"{typeof(TRepository).Name} is not command repository.");
                }
            }

            repository.Set(connection);
            return (TRepository)repository;
        }
    }

}