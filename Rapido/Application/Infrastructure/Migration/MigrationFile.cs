using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rapido.Infrastructure.Migration
{
    public class MigrationFile
    {
        public string MigrationFilesDirectoryPath { get; set; }
        public string MigratrionInfrastructureNamespace { get; set; }
        public string MigrationClassFileNamespace { get; set; }

        public MigrationFile(string migrationFilesDirectoryPath
            , string migratrionInfrastructureNamespace
            , string migrationClassFileNamespace)
        {
            MigrationFilesDirectoryPath = migrationFilesDirectoryPath;
            MigratrionInfrastructureNamespace = migratrionInfrastructureNamespace;
            MigrationClassFileNamespace = migrationClassFileNamespace;
        }
        public void CreateMigrationFile(string migrationName)
        {
            string timestamp = DateTime.UtcNow.Year + DateTime.UtcNow.Month.ToString().PadLeft(2, '0') + DateTime.UtcNow.Day.ToString().PadLeft(2, '0')
               + DateTime.UtcNow.Hour.ToString().PadLeft(2, '0') + DateTime.UtcNow.Minute.ToString().PadLeft(2, '0') + DateTime.UtcNow.Second.ToString().PadLeft(2, '0');
            string className = "_" + timestamp + "_" + migrationName;
            string filename = className + ".cs";
            string filecontent = BuildNewMigrationFileContent(className);
            System.IO.File.WriteAllText(Path.Combine(MigrationFilesDirectoryPath, filename), filecontent);

        }

        public void CreateMigrationTableFile(string migrationName)
        {
            string timestamp = DateTime.UtcNow.Year + DateTime.UtcNow.Month.ToString().PadLeft(2, '0') + DateTime.UtcNow.Day.ToString().PadLeft(2, '0')
               + DateTime.UtcNow.Hour.ToString().PadLeft(2, '0') + DateTime.UtcNow.Minute.ToString().PadLeft(2, '0') + DateTime.UtcNow.Second.ToString().PadLeft(2, '0');
            string className = "_" + timestamp + "_" + migrationName;
            string filename = className + ".cs";
            string filecontent = BuildNewMigrationTableFileContent(className);
            System.IO.File.WriteAllText(Path.Combine(MigrationFilesDirectoryPath, filename), filecontent);

        }

        private string BuildNewMigrationFileContent(string className)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using " + MigratrionInfrastructureNamespace + ";");
            sb.AppendLine("//Ctrl+k, Ctrl+D");

            sb.AppendLine("namespace " + MigrationClassFileNamespace);
            sb.AppendLine("{");
            sb.AppendLine("class " + className + " : Migration");
            sb.AppendLine("{");

            sb.AppendLine(" protected override Migration Up()");
            sb.AppendLine("{");
            sb.AppendLine(" return this;");
            sb.AppendLine("}");

            sb.AppendLine(" protected override Migration Down()");
            sb.AppendLine("{");
            sb.AppendLine(" return this;");
            sb.AppendLine("}");

            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb.ToString();
        }


        private string BuildNewMigrationTableFileContent(string className)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using " + MigratrionInfrastructureNamespace + ";");
            sb.AppendLine("//Ctrl+k, Ctrl+D");

            sb.AppendLine("namespace " + MigrationClassFileNamespace);
            sb.AppendLine("{");
            sb.AppendLine("class " + className + " : Migration");
            sb.AppendLine("{");

            sb.AppendLine(" protected override Migration Up()");
            sb.AppendLine("{");
            sb.AppendLine(@"this.CreateTable(""dbo.Table"")");
            sb.AppendLine(@".WithInt32IdentityColumn(""TableId"")");
            sb.AppendLine(@".WithStringColumn(""Name"",50)");
            sb.AppendLine(@".WithBooleanColumn(""Active"")");
            sb.AppendLine(@".WithPrimaryKey(""TableId"")");
            sb.AppendLine(@".AddIndexOn(""Name"");");
            sb.AppendLine(" return this;");
            sb.AppendLine("}");

            sb.AppendLine(" protected override Migration Down()");
            sb.AppendLine("{");
            sb.AppendLine(@"this.DropTable(""dbo.Table"");");
            sb.AppendLine(" return this;");
            sb.AppendLine("}");


            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public SortedList<string, string> GetMigrationFileNames()
        {

            //IEnumerable<string> files = System.IO.Directory.EnumerateFiles(TaskSettings.Global.MigrationFilesDirectoryPath);
            IEnumerable<string> files = System.IO.Directory.EnumerateFiles(MigrationFilesDirectoryPath);

            
            SortedList<string, string> fileList = new SortedList<string, string>();
            foreach (var file in files)
            {
                fileList.Add(GetTimeStamp(file), file);
            }

            return fileList;
        }

        public string GetFileNameFromFilePath(string file)
        {
            int index = file.LastIndexOf('\\');
            if (index > -1)
            {
                file = file.Substring(index + 1);
            }
            return file;
        }

        public string GetTimeStamp(string file)
        {

            string filename = GetFileNameFromFilePath(file);
            string[] parts = filename.Split('_');
            return parts[1];
        }

        public void WriteMigrationFileUpScript(string migrationFileVersion,string script,SortedList<string, string> migrationFileNamesAndTimestamps)
        {
            string migrationFile = migrationFileNamesAndTimestamps[migrationFileVersion];

            migrationFile = migrationFile.Replace(@"\Migrations\", @"\MigrationScripts\");
            migrationFile = migrationFile.Replace(@".cs", @".migrate.sql");

            //script = "begin transaction" + Environment.NewLine + script + Environment.NewLine + "commit transaction";
            File.WriteAllText(migrationFile, script);

        
        }

        public void WriteMigrationFileDownScript(string migrationFileVersion, string script, SortedList<string, string> migrationFileNamesAndTimestamps)
        {
            string migrationFile = migrationFileNamesAndTimestamps[migrationFileVersion];

            migrationFile = migrationFile.Replace(@"\Migrations\", @"\MigrationScripts\");
            migrationFile = migrationFile.Replace(@".cs", @".rollback.sql");

            //script = "begin transaction" + Environment.NewLine + script + Environment.NewLine + "commit transaction";
            File.WriteAllText(migrationFile, script);

        }


    }
}
