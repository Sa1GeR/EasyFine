using Autofac;
using PrivateForum.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrivateForum.App.Web.Configuration
{
    public class ConfiguratorFactory : IConfiguratorFactory
    {
        private IContainer _container;
        public ConfiguratorFactory(IContainer container)
        {
            this._container = container;
        }

        public TConfigurator Provide<TConfigurator>(params IConfiguratorParameter[] parameters) where TConfigurator : IConfiguratorBase
        {
            return this.Provide<TConfigurator>(parameters.AsEnumerable());
        }

        public TConfigurator Provide<TConfigurator>(IEnumerable<IConfiguratorParameter> parameters) where TConfigurator : IConfiguratorBase
        {
            var _parameters = this.ParseParameters(parameters);

            var configurator = _parameters.Count() == 0
                ? _container.Resolve<TConfigurator>() as IConfiguratorBase
                : _container.Resolve<TConfigurator>(_parameters) as IConfiguratorBase;

            if (configurator == null)
            {
                if ((typeof(TConfigurator) as IConfiguratorBase) is IConfiguratorBase)
                {
                    throw new Exception($"Unable to find {typeof(TConfigurator).Name}.");
                }
                else
                {
                    throw new Exception($"{typeof(TConfigurator).Name} is not a builder.");
                }
            }

            return (TConfigurator)configurator;
        }

        private IEnumerable<TypedParameter> ParseParameters(IEnumerable<IConfiguratorParameter> parameters)
        {
            var _parameters = new List<TypedParameter>();

            foreach (var parameter in parameters)
                _parameters.Add(new TypedParameter(parameter.GetType(), parameter));

            return _parameters;
        }
    }
}