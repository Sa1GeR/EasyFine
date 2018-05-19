using PrivateForum.Core.Framework.Security;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Commanding
{
    public interface ICommandBus
    {
        Task<UpdateResult> ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand;

        Task<UpdateResult<TUpdateResultData>> ExecuteAsync<TCommand, TUpdateResultData>(TCommand command)
            where TCommand : ICommand;
    }
}
