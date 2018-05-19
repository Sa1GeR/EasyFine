using PrivateForum.Core.Framework.Logging;
using PrivateForum.Core.Framework.Security;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework.Commanding
{
    public class CommandBus : ICommandBus
    {
        private IConnectionFactory _commandConnection;
        private IDomainRepositoryFactory _commandRepositoryFactory;
        private ICommandHandlerFactory _commandHandlerFactory;
        
        protected ILogger Log { get; private set; }
        protected IAuditContext ExecutionContext { get; private set; }
        

        public CommandBus(ILogger log, 
            IAuditContext auditContext, 
            ICommandHandlerFactory commandHandlerFactory,
            IDomainRepositoryFactory commandRepositoryFactory,
            IConnectionFactory commandConnection)
        {
            if (log == null) throw new ArgumentNullException("log");
            if (auditContext == null) throw new ArgumentNullException("auditContext");

            if (commandHandlerFactory == null) throw new ArgumentNullException("commandHandlerFactory");
            if (commandRepositoryFactory == null) throw new ArgumentNullException("commandRepositoryFactory");
            if (commandConnection == null) throw new ArgumentNullException("commandConnection");

            _commandHandlerFactory = commandHandlerFactory;
            _commandRepositoryFactory = commandRepositoryFactory;
            _commandConnection = commandConnection;

            Log = log;
            ExecutionContext = auditContext;
        }
        
        public async Task<UpdateResult> ExecuteAsync<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            this.Log.Debug($"Execute command {command.GetType().Name}");
            var startTime = DateTime.Now;
            var result = await CreateCommandHandler<TCommand>(this).ExecuteAsync(command);
            this.Log.Info($"[{(DateTime.Now - startTime)}] {command}");
            this.Log.Debug($"Command executed {command.GetType().Name} Result: {result.Success}  Summary: {result.MessageSummary}");
            return result;
        }

        public async Task<UpdateResult<TUpdateResultData>> ExecuteAsync<TCommand, TUpdateResultData>(TCommand command)
            where TCommand : ICommand
        {
            this.Log.Debug($"Execute command {command.GetType().Name}");
            var startTime = DateTime.Now;
            var result = await CreateCommandHandler<TCommand, TUpdateResultData>(this).ExecuteAsync(command);
            this.Log.Info($"[{(DateTime.Now - startTime)}] {command}");
            this.Log.Debug($"Command executed {command.GetType().Name} Result: {result.Success}  Summary: {result.MessageSummary}");
            return result;
        }

        #region Protected
        protected virtual ICommandHandler<TCommand> CreateCommandHandler<TCommand>(ICommandBus commandBus)
            where TCommand : ICommand
        {
            var handler = _commandHandlerFactory.TryGetInstance<TCommand>();
            if (handler == null) throw new InvalidOperationException($"Command handler for {typeof(TCommand).Name} not found");
            
            handler.Initialize(commandBus, Log, ExecutionContext, _commandRepositoryFactory, _commandConnection);

            return handler;
        }

        protected virtual ICommandHandler<TCommand, TUpdateResultData> CreateCommandHandler<TCommand, TUpdateResultData>(ICommandBus commandBus)
            where TCommand : ICommand
        {
            var handler = _commandHandlerFactory.TryGetInstance<TCommand, TUpdateResultData>();
            if (handler == null) throw new InvalidOperationException($"Command handler for {typeof(TCommand).Name} not found");

            handler.Initialize(commandBus, Log, ExecutionContext, _commandRepositoryFactory, _commandConnection);

            return handler;
        }
        #endregion
    }
    
}
