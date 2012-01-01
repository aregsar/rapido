using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Infrastructure.Migration
{
    public class ExistingTable : IExistingTable
    {
        private string _tableName;
        private StringBuilder Database { get; set; }

        public ExistingTable(StringBuilder sb, string name)
        {
            Database = sb;
            _tableName = name;
        }

        //public Table RemoveLastChar()
        //{
        //    Database.Replace(",", "", Database.Length - 1, 1);
        //    return this;
        //}

        private string AlterAdd
        {
            get
            {
                return "alter table " + _tableName + " add ";
            }
        }

        private string AlterAlter
        {
            get
            {
                return "alter table " + _tableName + " alter ";
            }
        }

        private string AlterDrop
        {
            get
            {
                return "alter table " + _tableName + " drop ";
            }
        }

        public IExistingTable RenameColumn(string name, string newName)
        {
            Database.AppendLine("exec sp_rename '" + _tableName + "." + name + "', '" + newName + "'");

            return this;
        }

        //=========================================================================================================================================================================
        //Drops
        //=========================================================================================================================================================================



        //ALTER TABLE table_name DROP COLUMN column_name
        public IExistingTable DropColumn(string name)
        {
            Database.AppendLine(AlterDrop + "column " + name);

            return this;
        }



        public IExistingTable DropPrimaryKey(string name)
        {
            string pkname = "PK_" + _tableName.Replace("dbo.", "") + "_" + name;

            DropConstraint(pkname);
            return this;
        }


        public IExistingTable DropIndexOn(string name)
        {
            //drop index dbo.TestAlterTable.IX_TestAlterTable_altid
            string indexname = "IX_" + _tableName.Replace("dbo.", "") + "_" + name;
            Database.AppendLine("drop index " + _tableName + "." + indexname);
            return this;
        }

        public IExistingTable DropCompositeIndex(string namea, string nameb)
        {
            string indexname = "IX_" + _tableName.Replace("dbo.", "") + "_" + namea + "_" + nameb;
            Database.AppendLine("drop index " + _tableName + "." + indexname);
            return this;
        }

        //ALTER TABLE Orders DROP CONSTRAINT fk_PerOrders
        //MySql: ALTER TABLE Orders DROP FOREIGN KEY fk_PerOrders 
        public IExistingTable DropForeignKey(string name, string foreignTableColumn)
        {
            string[] tableAndColumnParts = foreignTableColumn.Split('.');

            string fkname = "FK_" + _tableName.Replace("dbo.", "") + "_" + name + "_To_" + tableAndColumnParts[1] + "_" + tableAndColumnParts[2];

            DropConstraint(fkname);


            return this;
        }


        public IExistingTable DropConstraint(string constraintName)
        {
            //ALTER TABLE [dbo].[ActiveSubs4] DROP CONSTRAINT [PK_ActiveSub4]
            Database.AppendLine("alter table " + _tableName + " drop constraint " + constraintName);
            return this;
        }

        //=========================================================================================================================================================================
        //Add/Alter Columns
        //=========================================================================================================================================================================


        //ALTER TABLE table_name ADD column_name datatype
        public IExistingTable AddInt32ColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " int null");

            return this;
        }

       


        //ALTER TABLE table_name ALTER COLUMN column_name datatype
        public IExistingTable AlterToInt32Column(string name, int defaultValue = 0)
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");
            Database.AppendLine(AlterAlter + "column " + name + " int not null");

            return this;
        }

        //ALTER TABLE table_name ALTER COLUMN column_name datatype
        public IExistingTable AlterToInt32ColumnNullable(string name)
        {
            Database.AppendLine(AlterAlter + "column " + name + " int null");

            return this;
        }

        public IExistingTable AddInt64ColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " bigint null");

            return this;
        }

       

        public IExistingTable AlterToInt64Column(string name, int defaultValue = 0)
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " bigint not null");

            return this;
        }

        public IExistingTable AlterToInt64ColumnNullable(string name)
        {
            Database.AppendLine(AlterAlter + "column " + name + " bigint null");

            return this;
        }


        public IExistingTable AddInt16ColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " tinyint null");

            return this;
        }

     

        public IExistingTable AlterToInt16Column(string name, int defaultValue = 0)
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " tinyint not null");

            return this;
        }


        public IExistingTable AlterToInt16ColumnNullable(string name)
        {
            Database.AppendLine(AlterAlter + "column " + name + " tinyint null");

            return this;
        }

        public IExistingTable AddDateTimeColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " datetime null");

            return this;
        }

        public IExistingTable AlterToDateTimeColumn(string name, string defaultValue = "getdate()")
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " datetime not null");

            return this;
        }


        public IExistingTable AlterToDateTimeColumnNullable(string name)
        {
            Database.AppendLine(AlterAlter + "column " + name + " datetime null");

            return this;
        }

        public IExistingTable AddBooleanColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " bit null");

            return this;
        }

     

        public IExistingTable AlterToBooleanColumn(string name, int defaultValue = 0)
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " bit not null");

            return this;
        }

        public IExistingTable AlterToBooleanColumnNullable(string name)
        {
            Database.AppendLine(AlterAlter + "column " + name + " bit null");

            return this;
        }

        public IExistingTable AddStringColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " varchar(max) null");

            return this;
        }

 
        public IExistingTable AlterToStringColumn(string name, string defaultValue = "''")
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " varchar(max) not null");

            return this;
        }


        public IExistingTable AlterToStringColumnNullable(string name)
        {

            Database.AppendLine(AlterAlter + "column " + name + " varchar(max) null");

            return this;
        }


        public IExistingTable AddStringColumnNullable(string name, int maxLength)
        {
            Database.AppendLine(AlterAdd + name + " varchar(max) null".Replace("(max)", "(" + maxLength.ToString() + ")"));

            return this;
        }

     
        public IExistingTable AlterToStringColumn(string name, int maxLength, string defaultValue = "''")
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " varchar(max) not null".Replace("(max)", "(" + maxLength.ToString() + ")"));

            return this;
        }

        public IExistingTable AlterToStringColumnNullable(string name, int maxLength)
        {
            Database.AppendLine(AlterAlter + "column " + name + " varchar(max) null".Replace("(max)", "(" + maxLength.ToString() + ")"));

            return this;
        }


        public IExistingTable AddUnicodeStringColumnNullable(string name)
        {
            Database.AppendLine(AlterAdd + name + " nvarchar(max) null");

            return this;
        }

    

        public IExistingTable AlterToUnicodeStringColumn(string name, string defaultValue = "''")
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " nvarchar(max) not null");

            return this;
        }

        public IExistingTable AlterToUnicodeStringColumnNullable(string name)
        {
            Database.AppendLine(AlterAlter + "column " + name + " nvarchar(max) null");

            return this;
        }


        public IExistingTable AddUnicodeStringColumnNullable(string name, int maxLength)
        {
            Database.AppendLine(AlterAdd + name + " nvarchar(max) null".Replace("(max)", "(" + maxLength.ToString() + ")"));

            return this;
        }


     


        public IExistingTable AlterToUnicodeStringColumn(string name, int maxLength, string defaultValue = "''")
        {
            Database.AppendLine("update " + _tableName + " set " + name + " = " + defaultValue + " where " + name + " is null");

            Database.AppendLine(AlterAlter + "column " + name + " nvarchar(max) not null".Replace("(max)", "(" + maxLength.ToString() + ")"));

            return this;
        }

        public IExistingTable AlterToUnicodeStringColumnNullable(string name, int maxLength)
        {
            Database.AppendLine(AlterAlter + "column " + name + " nvarchar(max) null".Replace("(max)", "(" + maxLength.ToString() + ")"));

            return this;
        }








        //=========================================================================================================================================================================
        //Add Index
        //=========================================================================================================================================================================

        //CREATE INDEX IX_ActiveSubs6 ON  dbo.ActiveSubs6  (TotalNotifications)
        public IExistingTable AddIndexOn(string name, bool clustered = false)
        {
            string clusteredtext = clustered ? "clustered " : "";

            Database.AppendLine("create " + clusteredtext + "index IX_" + _tableName.Replace("dbo.", "") + "_" + name + " on " + _tableName + "(" + name + ")");
            //Database.AppendLine("create index IX_" + _tableName.Replace("dbo.", "") + "_" + name + " on " + _tableName + "(" + name + ")");
            return this;
        }

        public IExistingTable AddIndexOnDescending(string name, bool clustered = false)
        {
            string clusteredtext = clustered ? " clustered " : "";

            Database.AppendLine("create index IX_" + _tableName.Replace("dbo.", "") + "_" + name + " on " + _tableName + "(" + name + " desc)");
            return this;
        }

        //CREATE UNIQUE INDEX IX_ActiveSubs6 ON  dbo.ActiveSubs6  (TotalNotifications)
        public IExistingTable AddUniqueIndexOn(string name, bool clustered = false)
        {
            string clusteredtext = clustered ? " clustered " : "";

            Database.AppendLine("create unique index IX_" + _tableName.Replace("dbo.", "") + "_" + name + " on " + _tableName + "(" + name + ")");
            return this;
        }

        public IExistingTable AddUniqueIndexOnDescending(string name, bool clustered = false)
        {
            string clusteredtext = clustered ? " clustered " : "";

            Database.AppendLine("create unique index IX_" + _tableName.Replace("dbo.", "") + "_" + name + " on " + _tableName + "(" + name + " desc)");
            return this;
        }



        //Usages:
        //AddUniqueIndexComposite("a", "b")
        //AddUniqueIndexComposite("a desc", "b desc")
        //AddUniqueIndexComposite("a desc", "b")
        //AddUniqueIndexComposite("a", "b desc")
        public IExistingTable AddCompositeUniqueIndex(string namea_optionaldesc, string nameb_optionaldesc, bool clustered = false)
        {
            string clusteredtext = clustered ? "clustered " : "";

            string[] namea = namea_optionaldesc.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] nameb = nameb_optionaldesc.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //CREATE [ UNIQUE ] [ CLUSTERED | NONCLUSTERED ] INDEX index_name ON { table } ( column [ ASC | DESC ] [ ,...n ] ) 
            Database.AppendLine("create unique " + clusteredtext + "index IX_" + _tableName.Replace("dbo.", "") + "_" + namea[0] + "_" + nameb[0] + " on " + _tableName + "(" + namea_optionaldesc + "," + nameb_optionaldesc + ")");

            return this;
        }


        //=========================================================================================================================================================================
        //Add Keys
        //=========================================================================================================================================================================


        //ALTER TABLE dbo.ActiveSubs3 ADD CONSTRAINT PK_ActiveSub3_Columnname PRIMARY KEY(ActiveID)
        public IExistingTable AddPrimaryKey(string name, bool clustered = true)
        {
            Database.AppendLine("alter table " + _tableName + " add constraint PK_" + _tableName.Replace("dbo.", "") + "_" + name + (clustered ? " primary key clustered( " : " primary key non clustered( ") + name + ")");
            return this;
        }

        //ALTER TABLE dbo.ActiveSubs4 ADD CONSTRAINT PK_ActiveSub4_Columnname PRIMARY KEY(ActiveID DESC)
        public IExistingTable AddPrimaryKeyDescending(string name, bool clustered = true)
        {
            Database.AppendLine("alter table " + _tableName + " add constraint PK_" + _tableName.Replace("dbo.", "") + "_" + name + (clustered ? " primary key clustered( " : " primary key non clustered( ") + name + " desc)");
            return this;
        }




        //ALTER TABLE dbo.President_Lookup ADD CONSTRAINT fk_PresidentID
        //sFOREIGN KEY (PresidentID) REFERENCES dbo.Presidents (PresidentID)
        public IExistingTable AddForeignKey(string name, string foreignTableColumn, bool cascadeDelete = true)
        {
            string cascadeDeletetext = cascadeDelete ? " cascade on delete" : "";

            //parts 1=dbo 2=table 3=column
            string[] tableAndColumnParts = foreignTableColumn.Split('.');

            Database.AppendLine("alter table " + _tableName.Replace("dbo.", "") + " add constraint FK_" + _tableName.Replace("dbo.", "") + "_" + name
                + "_To_" + tableAndColumnParts[1] + "_" + tableAndColumnParts[2]
                + " foreign key (" + name + ") references " + tableAndColumnParts[0] + "." + tableAndColumnParts[1]
                + " (" + tableAndColumnParts[2] + ")");

            return this;
        }



    }
}
