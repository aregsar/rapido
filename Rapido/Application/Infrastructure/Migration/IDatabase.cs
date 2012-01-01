using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Infrastructure.Migration
{
    public interface IDatabase
    {
        void CreateNewDatabaseAndSchemaVersion(string databaseName);

        void CreateSchemaVersion();

        string GetSchemaVersion();

        void ExecuteMigrtionScript(string migrationAndSchemaVersionUpdateScript);
    }
}
