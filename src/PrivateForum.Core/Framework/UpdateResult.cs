using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Framework
{
    public static class UpdateResultChainer
    {
        //public static async Task<UpdateResult> Continue(this Task<UpdateResult> updateResult, ICommand nextCommand)
        //{
        //    var currentResult = await updateResult;

        //    if (currentResult.Success)
        //    {
        //        var nextResult = await currentResult.commandBus.ExecuteAsync(nextCommand);

        //        List<TracMessage> messages = new List<TracMessage>();

        //        messages.AddRange(currentResult.Messages);
        //        messages.AddRange(nextResult.Messages);

        //        nextResult.Messages = messages;

        //        return nextResult;
        //    }

        //    return currentResult;
        //}
    }

    /// <summary>
    /// Represents the result of an application layer update operation. This update could be of the form create, modify or delete.
    /// </summary>
    public class UpdateResult
    {
        //internal ICommandBus commandBus;

        public static Task<UpdateResult> Create(UpdateResultStatus status, IEnumerable<string> messages = null)
        {
            return Task.FromResult(new UpdateResult(status, messages));
        }

        public static Task<UpdateResult<T>> Create<T>(UpdateResultStatus status, T data, IEnumerable<string> messages = null)
        {
            return Task.FromResult(new UpdateResult<T>(status, data, messages));
        }

        public UpdateResult() : this(UpdateResultStatus.Success, null) { }
        public UpdateResult(UpdateResultStatus status, IEnumerable<string> messages = null)
        {
            Status = status;
            Messages = new List<string>();

            if (messages != null) Messages.AddRange(messages);
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public UpdateResultStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the list of messages.
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// Indicates whether the operation has been successfully performed.
        /// </summary>
        public bool Success { get { return (Status == UpdateResultStatus.Success); } }

        public override string ToString()
        {
            return $"Messages:\n{MessageSummary}".Trim();
        }

        /// <summary>
        /// Summarises all error messages into a single string
        /// </summary>
        public string MessageSummary
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var message in Messages)
                {
                    sb.AppendFormat("{0}\n", message);
                }
                return sb.ToString().Trim();
            }
        }
    }

    public class UpdateResult<TData> : UpdateResult
    {
        public TData Data { get; set; }

        public UpdateResult() : base() { }

        public UpdateResult(UpdateResultStatus status, TData data)
            : base(status)
        {
            Data = data;
        }

        public UpdateResult(UpdateResultStatus status, IEnumerable<string> messages = null)
            : base(status, messages)
        {
        }

        public UpdateResult(UpdateResultStatus status, TData data, IEnumerable<string> messages = null)
            : base(status, messages)
        {
            Data = data;
        }
    }
}
