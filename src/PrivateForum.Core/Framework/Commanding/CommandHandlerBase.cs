using System;
using PrivateForum.Core.Framework.Security;
using System.Threading.Tasks;
using PrivateForum.Core.Framework.Logging;
using System.Collections.Generic;
using Autofac;

namespace PrivateForum.Core.Framework.Commanding
{
    public abstract class CommandHandlerBase<TCommand> : CommandHandlerBase, ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public abstract Task<UpdateResult> ExecuteAsync(TCommand command);
    }

    public abstract class CommandHandlerBase<TCommand, TUpdateResultData> : CommandHandlerBase, ICommandHandler<TCommand, TUpdateResultData>
        where TCommand : ICommand
    {
        public abstract Task<UpdateResult<TUpdateResultData>> ExecuteAsync(TCommand command);
    }

    public class CommandHandlerBase : ICommandHandlerBase
    {
        private IDomainRepositoryFactory _commandRepositoryFactory;
        private IConnectionFactory _commandConnection;


        protected ICommandBus CommandBus { get; private set; }
        protected IAuditContext ExecutionContext { get; private set; }
        protected ILogger Log { get; private set; }


        protected TRepository Provide<TRepository>()
            where TRepository : IDomainRepository
        {
            return _commandRepositoryFactory.Provide<TRepository>(_commandConnection);
        }

        void ICommandHandlerBase.Initialize(ICommandBus commandBus, 
            ILogger log,
            IAuditContext auditContext, 
            IDomainRepositoryFactory commandRepositoryFactory, 
            IConnectionFactory commandConnection)
        {
            if (commandBus == null) throw new ArgumentNullException("commandBus");
            if (auditContext == null) throw new ArgumentNullException("auditContext");
            if (commandRepositoryFactory == null) throw new ArgumentNullException("commandRepositoryFactory");
            if (commandConnection == null) throw new ArgumentNullException("commandConnection");
            if (log == null) throw new ArgumentNullException("log");

            Log = log;
            CommandBus = commandBus;
            ExecutionContext = auditContext;

            _commandRepositoryFactory = commandRepositoryFactory;
            _commandConnection = commandConnection;

            Initialization();
        }

        protected virtual void Initialization()
        {

        }
    }
}
