using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Infrastructure.Migration
{

    public class SqlServerMigration
    {
        public ITable CreateTable(string name, StringBuilder Database)
        {
            return new Table(Database, "dbo." + name);
        }

        public IExistingTable AlterTable(string name, StringBuilder Database)
        {
            return new ExistingTable(Database, "dbo." + name);
        }

        public void DropTable(string name, StringBuilder Database)
        {
            Database.AppendLine("drop table dbo." + name);
        }

        public void TruncateTable(string name, StringBuilder Database)
        {
            Database.AppendLine("truncate table dbo." + name);
        }
    }

    public abstract class Migration 
    {
        //Later set the Base Migration type based on a configuration option
        SqlServerMigration Base = new SqlServerMigration();

        private StringBuilder Database = new StringBuilder();

        protected abstract Migration Up();

        protected abstract Migration Down();


        public Migration MigrateUp()
        {
            return Up();
        }

        public Migration MigrateDown()
        {
            return Down();
        }

        public void Clear()
        {
            Database.Clear();
        }

        public string Dump()
        {
            return Database.ToString();
        }

        public void WriteSqlLine(string script)
        {
            Database.AppendLine(script);
        }

        public ITable CreateTable(string name)
        {
            return Base.CreateTable(name, Database);
            //return new Table(Database, "dbo." + name);
        }

        public IExistingTable AlterTable(string name)
        {
            return Base.AlterTable(name, Database);
            //return new ExistingTable(Database, "dbo." + name);
        }

        public void DropTable(string name)
        {
            Base.DropTable(name, Database);
            //Database.AppendLine("drop table dbo." + name);
        }

        public void TruncateTable(string name)
        {
            Base.TruncateTable(name, Database);
            //Database.AppendLine("truncate table dbo." + name);
        }
    }

   


}
