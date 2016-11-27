﻿using System.IO;

namespace NoSqlRepositories.Logger
{
    public interface INoSqlLogger
    {
        /// <summary>
        /// Delete expired logs in the database. Others are not deleted
        /// </summary>
        /// <returns></returns>
        bool ClearLogs();

        /// <summary>
        /// Create a log in the application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <returns>ID of the created log</returns>
        string AddLog<T>(T contentLog) where T : class;

        /// <summary>
        /// Create a log in the application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <param name="level">Criticity of the log</param>
        /// <returns>ID of the created log</returns>
        string AddLog<T>(T contentLog, LogLevel level) where T : class;

        /// <summary>
        /// Create a log in the application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <param name="message">Custom message</param>
        /// <returns>ID of the created log</returns>
        string AddLog<T>(T contentLog, string message) where T : class;

        /// <summary>
        /// Create a log in the application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <param name="message">Custom message</param>
        /// <param name="level">Criticity of the log</param>
        /// <returns>ID of the created log</returns>
        string AddLog<T>(T contentLog, string message, LogLevel level) where T : class;

        /// <summary>
        /// Create a log in the application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <param name="message">Custom message</param>
        /// <param name="longMessage">Long description</param>
        /// <returns>ID of the created log</returns>
        string AddLog<T>(T contentLog, string message, string longMessage) where T : class;

        /// <summary>
        /// Create a log in the application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <param name="message">Custom message</param>
        /// <param name="longMessage">Long description</param>
        /// <param name="level">Criticity of the log</param>
        /// <returns>ID of the created log</returns>
        string AddLog<T>(T contentLog, string message, string longMessage, LogLevel level) where T : class;
        
        /// <summary>
        /// Create a log in the application with error level.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentLog">Object that contains informations about the log</param>
        /// <param name="message">Custom message</param>
        /// <param name="longMessage">Long description</param>
        /// <returns></returns>
        string AddError<T>(T contentLog, string message, string longMessage) where T : class;

        /// <summary>
        /// Add an attachment to a log item
        /// </summary>
        /// <param name="id">Id of the log item generated by the AddLog method</param>
        /// <param name="filePathAttachment">File path to the file to be linked to the log</param>
        /// <param name="contentType">Content type of the file</param>
        /// <param name="attachmentName">Name of the file to be attached</param>
        void AddAttachment(string id, Stream filePathAttachment, string contentType, string attachmentName);
    }
}
