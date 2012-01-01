using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Rapido.Infrastructure.Data;


namespace Rapido.Infrastructure.Migration
{

   


    public class Migrator
    {
        public string MigrationScript { get; set; }

        public string MigrationFilesAssemblyFilePath { get; set; }

        public SortedList<string, string> MigrationFileNamesAndTimestamps { get; set; }

        //"C:\\Dev\<projectname>\<projectname>.Db\Bin\Debug\<projectname>.Db.dll"
        public string MigrationFilesDirectoryPath { get; set; }

        MigrationFile MigrationFileService;

        IDatabase MigrationDatabaseService;

        //"<projectname>.Db.Migrations"
        public string MigrationClassFileNamespace { get; set; }

        public string MigratrionInfrastructureNamespace { get; set; }

        public Migrator(ConnectionStringInfo masterConnectionStringInfo
            , ConnectionStringInfo migrationConnectionStringInfo
            , string migrationClassFilesAssemblyFilePath
            , string migrationClassFilesDirectoryPath
            , string migrationClassFileNamespace
            , string migratrionInfrastructureNamespace)
        {


            MigrationFilesAssemblyFilePath = migrationClassFilesAssemblyFilePath;

            MigrationFilesDirectoryPath = migrationClassFilesDirectoryPath;

            MigrationClassFileNamespace = migrationClassFileNamespace;

            MigratrionInfrastructureNamespace = migratrionInfrastructureNamespace;

            MigrationFileService = new MigrationFile(migrationClassFilesDirectoryPath
                                                    , migratrionInfrastructureNamespace
                                                    , migrationClassFileNamespace);

            MigrationFileNamesAndTimestamps = MigrationFileService.GetMigrationFileNames();


            MigrationDatabaseService = GetMigrationDatabaseCreator(masterConnectionStringInfo, migrationConnectionStringInfo);
   
        }


        private IDatabase GetMigrationDatabaseCreator(ConnectionStringInfo masterConnectionStringInfo, ConnectionStringInfo migrationConnectionStringInfo)
        {
            if (migrationConnectionStringInfo.ProviderName == "SqlClient")
                return new MigrationDatabase(masterConnectionStringInfo, migrationConnectionStringInfo);
            else
                return new SqlServer2008Database(masterConnectionStringInfo, migrationConnectionStringInfo);
        }

        public void CreateSchemaVersion()
        {
            MigrationDatabaseService.CreateSchemaVersion();
        }

        public void CreateNewDatabaseAndSchemaVersion(string databaseName)
        {
            MigrationDatabaseService.CreateNewDatabaseAndSchemaVersion(databaseName.Trim());
        }


        public Migration GetMigration(string migrationFileVersion)
        {
            string migrationFile = MigrationFileNamesAndTimestamps[migrationFileVersion];

            string migrationClassFilename = MigrationFileService.GetFileNameFromFilePath(migrationFile);

            string migrationClass = migrationClassFilename.Replace(".cs", "");

            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(MigrationFilesAssemblyFilePath);
           
            Migration migration = (Migration)assembly.CreateInstance(MigrationClassFileNamespace + "." + migrationClass);

            return migration;
        }


        public string GetCurrentMigrationFileVersion(string currentSchemaVersion)
        {
            if (MigrationFileNamesAndTimestamps[currentSchemaVersion] == null) throw new Exception("no migration file found for current schema version");
          
            return currentSchemaVersion;       
        }

        public string GetPreviousMigrationFileVersion(string currentSchemaVersion)
        {
            if (MigrationFileNamesAndTimestamps[currentSchemaVersion] != null)
            {
                int index = MigrationFileNamesAndTimestamps.IndexOfKey(currentSchemaVersion);

                if (index > 0)
                {
                    return MigrationFileNamesAndTimestamps.Keys[index - 1];
                }
            }

            return "";
        }



        public string GetNextMigrationFileVersion(string currentSchemaVersion)
        {
            int index = MigrationFileNamesAndTimestamps.IndexOfKey(currentSchemaVersion);

            if (MigrationFileNamesAndTimestamps.Keys.Count > index + 1)
            {
                return MigrationFileNamesAndTimestamps.Keys[index + 1];
            }

            return "";
        }


       
        public string GetMigrationAndSchemaVersionUpdateScript(string migrationScript, string migrationTimestamp)
        {
            if (migrationTimestamp == "") migrationTimestamp = "''";

            string beginTransaction = GetBeginTransactionScript();
            string rollbackTransactionOnError = GetRollbackTransactionOnErrorScript();
            string rollbackOnErrorOrCommitTransaction = GetRollbackOnErrorOrCommitTransactionScript();
            string updateSchemaVersion = GetUpdateSchemaVersionScript(migrationTimestamp);


            //string finalScript = beginTransaction + migrationScript + rollbackTransactionOnError + updateSchemaVersion + rollbackOnErrorOrCommitTransaction;
            string finalScript = beginTransaction + Environment.NewLine + migrationScript + Environment.NewLine + updateSchemaVersion + Environment.NewLine + " commit transaction";

            return finalScript;
        }

        private string GetBeginTransactionScript()
        {
            return "begin transaction ";
        }

        private string GetRollbackTransactionOnErrorScript()
        {
            return " if @@error<> 0 begin rollback transaction return end ";
        }

        private string GetRollbackOnErrorOrCommitTransactionScript()
        {
            return " if @@error<> 0 begin rollback transaction return end else begin commit transaction end";
        }

        private string GetUpdateSchemaVersionScript(string migrationTimestamp)
        {
            return "update dbo.SchemaVersion set versiontimestamp = '" + migrationTimestamp + "'";
        }

        public string GetCurrentSchemaVersion()
        {
            return MigrationDatabaseService.GetSchemaVersion();
        }

        public bool Rollback()
        {

            string currentSchemaVersion = MigrationDatabaseService.GetSchemaVersion();

            if (currentSchemaVersion != "")
            {
                string migrationFileVersion = GetCurrentMigrationFileVersion(currentSchemaVersion);

                string prevMigrationFileVersion = GetPreviousMigrationFileVersion(currentSchemaVersion);

                Migration migration = GetMigration(migrationFileVersion);

                string migrationScript = migration.MigrateDown().Dump();

                string migrationAndSchemaVersionUpdateScript = GetMigrationAndSchemaVersionUpdateScript(migrationScript, prevMigrationFileVersion);


                //NEWCODE
                //MigrationFileService.WriteMigrationFileDownScript(migrationFileVersion, migrationScript, MigrationFileNamesAndTimestamps);
                MigrationFileService.WriteMigrationFileDownScript(migrationFileVersion, migrationAndSchemaVersionUpdateScript, MigrationFileNamesAndTimestamps);

                MigrationDatabaseService.ExecuteMigrtionScript(migrationAndSchemaVersionUpdateScript);

                return true;
            }

            return false;
        }





     

        public bool Migrate()
        {
            string currentSchemaVersion = MigrationDatabaseService.GetSchemaVersion();

            string migrationFileVersion = GetNextMigrationFileVersion(currentSchemaVersion);

            if (migrationFileVersion != "")
            {

                Migration migration = GetMigration(migrationFileVersion);

                string migrationScript = migration.MigrateUp().Dump();

                string migrationAndSchemaVersionUpdateScript = GetMigrationAndSchemaVersionUpdateScript(migrationScript, migrationFileVersion);

           
                //NEWCODE
                //MigrationFileService.WriteMigrationFileUpScript(migrationFileVersion, migrationScript, MigrationFileNamesAndTimestamps);
                MigrationFileService.WriteMigrationFileUpScript(migrationFileVersion, migrationAndSchemaVersionUpdateScript, MigrationFileNamesAndTimestamps);

                MigrationDatabaseService.ExecuteMigrtionScript(migrationAndSchemaVersionUpdateScript);

                return true;
            }

            return false;
        }
   
        public void MigrateAll()
        {
            string currentSchemaVersion = MigrationDatabaseService.GetSchemaVersion();

            string migrationFileVersion = GetNextMigrationFileVersion(currentSchemaVersion);

            while (migrationFileVersion != "")
            {
                Migration migration = GetMigration(migrationFileVersion);

                string migrationScript = migration.MigrateUp().Dump();

                string migrationAndSchemaVersionUpdateScript = GetMigrationAndSchemaVersionUpdateScript(migrationScript, migrationFileVersion);

                MigrationFileService.WriteMigrationFileUpScript(migrationFileVersion, migrationAndSchemaVersionUpdateScript, MigrationFileNamesAndTimestamps);

                MigrationDatabaseService.ExecuteMigrtionScript(migrationAndSchemaVersionUpdateScript);

                currentSchemaVersion = MigrationDatabaseService.GetSchemaVersion();

                migrationFileVersion = GetNextMigrationFileVersion(currentSchemaVersion);
            }
        }

        public void RollbackAll()
        {
            string currentSchemaVersion = MigrationDatabaseService.GetSchemaVersion();

            while (currentSchemaVersion != "")
            {
                string migrationFileVersion = GetCurrentMigrationFileVersion(currentSchemaVersion);

                string prevMigrationFileVersion = GetPreviousMigrationFileVersion(currentSchemaVersion);

                Migration migration = GetMigration(migrationFileVersion);

                string migrationScript = migration.MigrateDown().Dump();

                string migrationAndSchemaVersionUpdateScript = GetMigrationAndSchemaVersionUpdateScript(migrationScript, prevMigrationFileVersion);

                MigrationFileService.WriteMigrationFileDownScript(migrationFileVersion, migrationAndSchemaVersionUpdateScript, MigrationFileNamesAndTimestamps);

                MigrationDatabaseService.ExecuteMigrtionScript(migrationAndSchemaVersionUpdateScript);

                currentSchemaVersion = MigrationDatabaseService.GetSchemaVersion();
            }
        }

    

   
        public void CreateNewMigrationFile(string migrationFileName)
        {
            MigrationFileService.CreateMigrationFile(migrationFileName.Trim());
        }

        public void CreateNewMigrationFileWithCreateTableTemplate(string migrationFileName)
        {
            MigrationFileService.CreateMigrationTableFile(migrationFileName.Trim());
        }


        public string ScriptMigration(string migrationFileNameAndTimeStamp, string prevMigrationFileNameAndTimeStamp)
        {
            string migrationFileVersion = MigrationFileService.GetTimeStamp(migrationFileNameAndTimeStamp);

            Migration migration = GetMigration(migrationFileVersion);

            StringBuilder sb = new StringBuilder();

            string prevMigrationFileVersion = MigrationFileService.GetTimeStamp(prevMigrationFileNameAndTimeStamp);

            ScriptMigration(migration, migrationFileVersion, prevMigrationFileVersion, sb);          

            return sb.ToString();
        }

        public void ScriptAllMigrations()
        {
            string prevMigrationFileVersion = "";

            foreach (string migrationFileVersion in MigrationFileNamesAndTimestamps.Keys)
            {
                Migration migration = GetMigration(migrationFileVersion);

                ScriptMigration(migration, migrationFileVersion, prevMigrationFileVersion);   
  
                prevMigrationFileVersion = migrationFileVersion;
            }
        }

        private void ScriptMigration(Migration migration,string migrationFileVersion, string prevMigrationFileVersion, StringBuilder sb = null)
        {
            string migrationScript = migration.MigrateUp().Dump();

            migrationScript = GetMigrationAndSchemaVersionUpdateScript(migrationScript, migrationFileVersion);

            MigrationFileService.WriteMigrationFileUpScript(migrationFileVersion, migrationScript, MigrationFileNamesAndTimestamps);

            migration.Clear();

            string rollbackScript = migration.MigrateDown().Dump();

            rollbackScript = GetMigrationAndSchemaVersionUpdateScript(rollbackScript, prevMigrationFileVersion);

            MigrationFileService.WriteMigrationFileDownScript(migrationFileVersion, rollbackScript, MigrationFileNamesAndTimestamps);

            if (sb != null)
            {
                sb.AppendLine("Migration Script:");
                sb.AppendLine(migrationScript);

                sb.AppendLine("Rollback Script:");
                sb.AppendLine(rollbackScript);
            }
        }
    }
}
