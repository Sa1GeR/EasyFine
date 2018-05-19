using PrivateForum.Core.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Commanding
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> TryGetInstance<T>() where T : ICommand;
        ICommandHandler<T, TOut> TryGetInstance<T, TOut>() where T : ICommand;
    }
}
