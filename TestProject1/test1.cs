
using System;
using System.Data.SqlClient;
using Xunit;
using POE_RaceDay_API.Database;

namespace POE_RaceDay_API.Tests
{
    public class AutoCreateInstanceAndTablesTests
    {
        private readonly string instanceName = "POERaceday_API";
        private readonly string databaseName = "RaceDay_Database";

        private string ConnectionString =>
            $@"Server=(localdb)\{instanceName};
               Database={databaseName};
               Integrated Security=true;";

        [Fact]
        public void InitializeSystem_CreatesDatabaseAndTables()
        {
            // Arrange
            var database = new AutoCreateInstanceAndTables();

            // Act
            database.InitializeSystem();

            // Assert
            using var connection =
                new SqlConnection(ConnectionString);

            connection.Open();

            string[] expectedTables =
            {
                "Users",
                "Events",
                "Categories",
                "Enrolments",
                "Results"
            };

            foreach (string table in expectedTables)
            {
                using var command = new SqlCommand(
                    "SELECT COUNT(*) FROM sys.tables " +
                    "WHERE name = @TableName",
                    connection);

                command.Parameters.AddWithValue(
                    "@TableName", table);

                int count = (int)command.ExecuteScalar();

                Assert.Equal(1, count);
            }
        }

        [Fact]
        public void InitializeSystem_CanRunMoreThanOnce()
        {
            // Arrange
            var database = new AutoCreateInstanceAndTables();

            // Act and Assert
            database.InitializeSystem();

            var exception = Record.Exception(
                () => database.InitializeSystem());

            Assert.Null(exception);
        }

        [Fact]
        public void UsersTable_ContainsRequiredColumns()
        {
            // Arrange
            var database = new AutoCreateInstanceAndTables();
            database.InitializeSystem();

            using var connection =
                new SqlConnection(ConnectionString);

            connection.Open();

            // Act
            string sql = @"
                SELECT COUNT(*)
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = 'Users'
                AND COLUMN_NAME IN
                ('UserID', 'FullName', 'Email',
                 'PasswordHash', 'Role', 'PhoneNumber',
                 'CreatedAt');";

            using var command = new SqlCommand(sql, connection);

            int count = (int)command.ExecuteScalar();

            // Assert
            Assert.Equal(7, count);
        }
    }
}

