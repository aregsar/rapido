using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rapido.Infrastructure.Data;

namespace Rapido.Infrastructure.Migration
{
    public class MigrationRunner
    {
        Migrator _migrator;

        public MigrationRunner(ConnectionStringInfo masterConnectionStringInfo, ConnectionStringInfo migrationConnectionStringInfo
            , string migrationClassFilesAssemblyFilePath
            , string migrationClassFilesDirectoryPath
            , string migrationClassFileNamespace
            , string migratrionInfrastructureNamespace)
        {      
            _migrator = new Migrator( masterConnectionStringInfo
                                    , migrationConnectionStringInfo
                                    , migrationClassFilesAssemblyFilePath
                                    , migrationClassFilesDirectoryPath
                                    , migrationClassFileNamespace
                                    , migratrionInfrastructureNamespace);
        }


        public string Run(string arg)
        {
            if (arg.StartsWith("create:"))
            {
                _migrator.CreateNewDatabaseAndSchemaVersion(arg.Split(':')[1]);

                return "New Database Created";
            }
            if (arg.StartsWith("create_version"))
            {
                _migrator.CreateSchemaVersion();

                return "Schema Version Table added to database";
            }
            if (arg.StartsWith("version"))
            {
                string currentVersion = _migrator.GetCurrentSchemaVersion();

                if (string.IsNullOrEmpty(currentVersion)) return "Schema Version is empty";

                return "Current Schema Version Is: " + _migrator.GetCurrentSchemaVersion();
            }
            else if (arg.StartsWith("new_migration:"))
            {
                //create a migration file with empty up/down methods

                string filename = arg.Split(':')[1].Trim();

                if (filename != "")
                {
                    _migrator.CreateNewMigrationFile(arg.Split(':')[1]);

                    return "New Migration File Created";
                }

                return "Empty File Name";
            }
            else if (arg.StartsWith("table_migration:"))
            {
                //create a migtration file with boilerplate table create/table drop
                string filename = arg.Split(':')[1].Trim();

                if (filename != "")
                {
                    _migrator.CreateNewMigrationFileWithCreateTableTemplate(arg.Split(':')[1]);

                    return "New Migration File Template Created";
                }

                return "Empty File Name";
            }
            else
            {
                if (arg.StartsWith("script:"))
                {
                    //script:_20110712160822_some_migration
                    string[] parts = arg.Split(':');

                    string migrationFileVersionPart = parts[1];

                    //HACK: instead of pasting on command line
                    if (migrationFileVersionPart == "x")
                    {
                        //migrationFileVersionPart = "_20111011025813_create_users_table";
                        migrationFileVersionPart = "_20111023191558_create_users_table";
                    }
                    //END HACK

                    //TODO: FIX Third argument to prevfile version
                    string migrationScript = _migrator.ScriptMigration(migrationFileVersionPart, migrationFileVersionPart);

                    return migrationScript;
                }
                else if (arg.StartsWith("script_all"))
                {
                    _migrator.ScriptAllMigrations();

                    return "scripted all migration files";
                }
                else if (arg == "migrate")
                {
                    string currentVersion = _migrator.GetCurrentSchemaVersion();

                    //TODO: TEST THIS
                    if (!_migrator.Migrate())
                    {
                        if (string.IsNullOrEmpty(currentVersion)) currentVersion = "empty";

                        string msg = "No pending migrations remain - current version is " + currentVersion;

                        return msg;
                    }

                    if (string.IsNullOrEmpty(currentVersion)) currentVersion = "empty";

                    string currentSchemaVersion = "Migrated from version:  " + currentVersion + " to current version: " + _migrator.GetCurrentSchemaVersion();

                    return currentSchemaVersion;
                }
                else if (arg == "rollback")
                {
                    string currentVersion = _migrator.GetCurrentSchemaVersion();

                    //TODO: TEST THIS
                    if (!_migrator.Rollback())
                    {
                        if (string.IsNullOrEmpty(currentVersion)) currentVersion = "empty";

                        string msg = "No pending rollbacks remain - current version is " + currentVersion;

                        return msg;
                    }

                    string currentSchemaVersion = "Rolled back from version:  " + currentVersion + " to current version: " + _migrator.GetCurrentSchemaVersion();

                    return currentSchemaVersion;
                }
                else if (arg == "migrate_all")
                {
                    //TODO: TEST THIS
                    //_migrator.MigrateAll();

                    string currentSchemaVersion = "Current Schema Version Is: " + _migrator.GetCurrentSchemaVersion();

                    return currentSchemaVersion;
                }
                else if (arg == "rollback_all")
                {
                    //TODO: TEST THIS
                    //_migrator.RollbackAll();

                    string currentSchemaVersion = "Current Schema Version Is: " + _migrator.GetCurrentSchemaVersion();

                    return currentSchemaVersion;
                }
                else
                {
                    throw new Exception("invalid command: db:" + arg);
                }
            }
        }
    }





}
