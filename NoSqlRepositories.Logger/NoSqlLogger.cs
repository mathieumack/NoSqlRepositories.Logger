using NoSqlRepositories.Core;
using System;
using System.IO;

namespace NoSqlRepositories.Logger
{
    public class NoSqlLogger : INoSqlLogger
    {
        private readonly INoSQLRepository<Log> repository;
        private readonly int? daysBeforeExpiration;

        public NoSqlLogger(INoSQLRepository<Log> repository)
            : this(repository, null)
        {
        }

        public NoSqlLogger(INoSQLRepository<Log> repository, int? daysBeforeExpiration)
        {
            this.repository = repository;
            this.daysBeforeExpiration = daysBeforeExpiration;
        }

        #region Interface

        public string AddLog<T>(T contentLog) where T : class
        {
            return AddLog(contentLog, "N/A", "N/A", LogLevel.Info);
        }

        public string AddLog<T>(T contentLog, LogLevel level) where T : class
        {
            return AddLog(contentLog, "N/A", "N/A", level);
        }

        public string AddLog<T>(T contentLog, string message) where T : class
        {
            return AddLog(contentLog, message, "N/A", LogLevel.Info);
        }

        public string AddLog<T>(T contentLog, string message, LogLevel level) where T : class
        {
            return AddLog(contentLog, message, "N/A", level);
        }

        public string AddLog<T>(T contentLog, string message, string longMessage) where T : class
        {
            return AddLog(contentLog, message, longMessage, LogLevel.Info);
        }

        public string AddLog<T>(T contentLog, string message, string longMessage, LogLevel level) where T : class
        {
            var log = new Log()
            {
                Message = message,
                LongMessage = longMessage,
                ContentLog = contentLog,
                Level = level
            };
            var resultInsert = repository.InsertOne(log);
            if (resultInsert == InsertResult.inserted)
            {
                if (daysBeforeExpiration.HasValue)
                    repository.ExpireAt(log.Id, DateTime.Now.AddDays(daysBeforeExpiration.Value));
                return log.Id;
            }
            else
                return string.Empty; // Generate exception with contracts
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog"></param>
        /// <param name="message"></param>
        /// <param name="longMessage"></param>
        /// <returns></returns>
        public string AddError<T>(T contentLog, string message, string longMessage) where T : class
        {
            return AddLog(contentLog, message, longMessage, LogLevel.Error);
        }
        
        public void AddAttachment(string id, Stream filePathAttachment, string contentType, string attachmentName)
        {
            repository.AddAttachment(id, filePathAttachment, contentType, attachmentName);
        }

        public bool ClearLogs()
        {
            return repository.CompactDatabase();
        }

        #endregion
    }
}
