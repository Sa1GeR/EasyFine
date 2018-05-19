using System;
using System.Diagnostics;
using log4net;
using Ed.Core.Framework.Instrumentation;
using Ed.Core.Framework.Security;

namespace Ed.Core.Framework.Instrumentation
{

    /// <summary>
    /// Implementation of the TracLog
    /// </summary>
    /// <remarks>
    /// This is a class that wraps the log4net log, providing a simplified interface for developers
    /// 
    /// It also enforces logging of a process ID and the activity id
    /// </remarks>
    public class TracLog : ITracLog
    {
        private ILog log;

        // Note, these constants are only used in this file, putting them in constants would be cluttering it unneccesarily
        private const string ProcessCodeId = "ProcessCode";
        private const int ProcessCodeOffset = 2;// how many alpha characters are the start of the processcode
        private const string ActivityId = "ActivityId";
        private const string EventId = "EventID";
        private const string ExecutionChannelId = "ExecutionChannelId";
        private const string ModifiedById = "ModifiedById";
        private const string ChannelId = "ChannelId";
        private const string TaskId = "Task";
        private const string ContextURLId = "WebURLId";
        private const int TaskIdValue = 3;// Web

        public TracLog(ILog log)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            this.log = log;

        }

        public void Debug(object message, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Debug(message);
        }


        public void Debug(object message, Exception exception, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Debug(message, exception);
        }

        public void Error(object message, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Error(message);
        }

        public void Error(object message, Exception exception, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Error(message, exception);
        }

        public void Fatal(object message, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Fatal(message);
        }

        public void Fatal(object message, Exception exception, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Fatal(message, exception);
        }

        public void Info(object message, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Info(message);
        }

        public void Info(object message, Exception exception, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Info(message, exception);
        }

        public void Warn(object message, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Warn(message);
        }

        public void Warn(object message, Exception exception, IAuditContext auditContext)
        {
            SetThreadContextInformation(auditContext);
            log.Warn(message, exception);
        }
        

        public bool IsDebugEnabled
        {
            get {return log.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return log.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }

        private void SetThreadContextInformation(IAuditContext auditContext)
        {
            try
            {
                ThreadContext.Properties[ModifiedById] = auditContext.ModifiedBy;
                ThreadContext.Properties[ActivityId] = Trace.CorrelationManager.ActivityId;
                ThreadContext.Properties[TaskId] = TaskIdValue;
            }
            catch (Exception exception)
            {
                log.Error("Error in trying to log message", exception);
            }
        }
    }
}
