using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NoSqlRepositories.JsonFiles.Net;

namespace NoSqlRepositories.Logger.Tests
{
    [TestClass]
    public class NoSqlLoggerTests
    {
        private JsonFileRepository<Log> repo;
        private NoSqlLogger logger;

        #region Initialize & Clean

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            NoSQLCoreUnitTests.ClassInitialize(testContext);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var dbName = "logsDb";

            repo = new JsonFileRepository<Log>(NoSQLCoreUnitTests.testContext.DeploymentDirectory, dbName);
            repo.TruncateCollection();
            logger = new NoSqlLogger(repo);
        }

        #endregion

        [TestMethod]
        public void ClearLogs()
        {
            string idGenerated = logger.AddError(new ErrorEntity()
            {
                Property1 = "Property 1",
                Property2 = "Sample 1"
            }, "Error message", "Long message");

            var repoItems = repo.GetAll();

            Assert.IsTrue(repoItems.Count() == 1);

            repo.ExpireAt(idGenerated, DateTime.Now.AddHours(-1));

            logger.ClearLogs();

            repoItems = repo.GetAll();

            Assert.IsTrue(repoItems.Count() == 0);
        }

        [TestMethod]
        public void LogsExpiration()
        {
            logger.AddError(new ErrorEntity()
            {
                Property1 = "Property 1",
                Property2 = "Sample 1"
            }, "Error message", "Long message");

            var repoItems = repo.GetAll();

            Assert.IsTrue(repoItems.Count() == 1);

            // We change expiration date to -1 in order to make all new logs automatically expired :
            logger = new NoSqlLogger(repo, -1);

            logger.AddError(new ErrorEntity()
            {
                Property1 = "Property 2",
                Property2 = "Sample 2"
            }, "Expired log", "Long message");

            repoItems = repo.GetAll();

            Assert.IsTrue(repoItems.Count() == 1);
        }

        [TestMethod]
        public void AddError()
        {
            string idGenerated = logger.AddError(new ErrorEntity()
            {
                Property1 = "Property 1",
                Property2 = "Sample 1"
            }, "Error message", "Long message");
            var repoItems = repo.GetAll();

            Assert.IsTrue(repoItems.Count() == 1);

            var log = repo.GetById(idGenerated);

            var errorEntity = JsonConvert.DeserializeObject<ErrorEntity>(log.ContentLog);

            Assert.IsTrue(errorEntity.Property1 == "Property 1");
            Assert.IsTrue(errorEntity.Property2 == "Sample 1");
            Assert.IsTrue(log.Message == "Error message");
            Assert.IsTrue(log.LongMessage == "Long message");
        }
    }
}
