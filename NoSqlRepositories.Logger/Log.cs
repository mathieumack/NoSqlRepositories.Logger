using NoSqlRepositories.Core;
using System;

namespace NoSqlRepositories.Logger
{
    /// <summary>
    /// Log in the application
    /// </summary>
    public class Log : IBaseEntity
    {
        /// <summary>
        /// Indicate if the element is deleted or not
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Unique identifier of the item in repository
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Date of creation in repository
        /// </summary>
        public DateTime SystemCreationDate { get; set; }

        /// <summary>
        /// Last date of update in repository
        /// </summary>
        public DateTime SystemLastUpdateDate { get; set; }

        /// <summary>
        /// Custom message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Long description
        /// </summary>
        public string LongMessage { get; set; }

        /// <summary>
        /// Criticity of the log
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Custom object added to the log
        /// </summary>
        public object ContentLog { get; set; }
    }
}
