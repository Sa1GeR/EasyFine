using PrivateForum.Core.Framework.Logging;
using PrivateForum.Core.Framework.Security;
using System.Threading.Tasks;
using System.Transactions;

namespace PrivateForum.Core.Framework.Commanding
{
    public interface ICommandHandler<in TCommand> : ICommandHandlerBase 
        where TCommand : ICommand
    {
        Task<UpdateResult> ExecuteAsync(TCommand command);        
    }

    public interface ICommandHandler<in TCommand, TUpdateResultData> : ICommandHandlerBase 
        where TCommand : ICommand
    {       
        Task<UpdateResult<TUpdateResultData>> ExecuteAsync(TCommand command);
    }

    public interface ICommandHandlerBase
    {
        void Initialize(ICommandBus commandBus, 
            ILogger log,
            IAuditContext auditContext,
            IDomainRepositoryFactory commandRepositoryFactory,
            IConnectionFactory commandConnection);
    }
}
