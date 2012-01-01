using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rapido.Infrastructure.Data;

namespace Rapido.Infrastructure.Migration
{
    public class SqlServer2008Database: IDatabase
    {   
        ConnectionStringInfo ConnectionInfo { get; set; }

        ConnectionStringInfo MasterConnectionInfo { get; set; }

        public SqlServer2008Database(ConnectionStringInfo masterDatabaseConnectionInfo, ConnectionStringInfo migrationDatabaseConnectionInfo)
        {
            ConnectionInfo = migrationDatabaseConnectionInfo;

            MasterConnectionInfo = masterDatabaseConnectionInfo;
        }

        public string GetSchemaVersion()
        {
            MigrationRepository repo = new MigrationRepository(ConnectionInfo);

            return repo.ExecuteScalar("select VersionTimestamp from dbo.SchemaVersion").ToString();
        }      

        public void CreateNewDatabaseAndSchemaVersion(string databaseName)
        {
            CreateNewDatabase(databaseName);

            CreateSchemaVersion();
        }

        private void CreateNewDatabase(string databaseName)
        {
            MigrationRepository masterRepo = new MigrationRepository(MasterConnectionInfo);

            masterRepo.ExecuteNonQuery("create database " + databaseName);
        }

        public void CreateSchemaVersion()
        {
            MigrationRepository repo = new MigrationRepository(ConnectionInfo);

            repo.ExecuteNonQuery("create table dbo.SchemaVersion(VersionTimestamp nvarchar(14) not null default(''))");

            repo.ExecuteNonQuery("insert into dbo.SchemaVersion(VersionTimestamp)Values('')");
        }

        public void ExecuteMigrtionScript(string migrationAndSchemaVersionUpdateScript)
        {
            MigrationRepository repo = new MigrationRepository(ConnectionInfo);

            repo.ExecuteNonQuery(migrationAndSchemaVersionUpdateScript);
        }
    }






}
