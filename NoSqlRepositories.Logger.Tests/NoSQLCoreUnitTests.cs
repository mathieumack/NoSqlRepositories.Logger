using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoSqlRepositories.Core;
using NoSqlRepositories.Core.Helpers;
using System;

namespace NoSqlRepositories.Logger.Tests
{
    public class NoSQLCoreUnitTests
    {
        #region Members

        protected string baseFilePath;

        protected INoSQLRepository<Log> entityRepo;

        public static TestContext testContext;

        #endregion

        public NoSQLCoreUnitTests(INoSQLRepository<Log> entityRepo,
            string baseFilePath)
        {
            this.entityRepo = entityRepo;

            this.entityRepo.TruncateCollection();

            this.baseFilePath = baseFilePath;
        }

        public static void ClassInitialize(TestContext testContext)
        {
            // Overide datetime.now function
            var now = new DateTime(2016, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            NoSQLRepoHelper.DateTimeUtcNow = (() => now);

            NoSQLCoreUnitTests.testContext = testContext;
        }
    }
}
