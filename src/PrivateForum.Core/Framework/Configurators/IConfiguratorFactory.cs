using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core
{
    public interface IConfiguratorFactory
    {
        TConfigurator Provide<TConfigurator>(params IConfiguratorParameter[] parameters) 
            where TConfigurator : IConfiguratorBase;

        TConfigurator Provide<TConfigurator>(IEnumerable<IConfiguratorParameter> parameters)
            where TConfigurator : IConfiguratorBase;
    }
}
