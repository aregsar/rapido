using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Infrastructure.Migration
{

    public class Table : ITable
    {
        //ITable _table;

        private string _tableName;
        private StringBuilder Database { get; set; }

        public Table(StringBuilder sb, string name)
        {
            Database = sb;
            _tableName = name;
            Database.AppendLine("create table " + _tableName + "(");
        }

        private IExistingTable End()
        {
            Database.Replace(',', ')', Database.Length - (Environment.NewLine.Length + 1), 1);
            return new ExistingTable(Database, _tableName);
        }

        public IExistingTable WithPrimaryKey(string name, bool clustered = false)
        {
            Database.AppendLine("constraint PK_" + _tableName.Replace("dbo.", "") + "_" + name + (clustered ? " primary key clustered( " : " primary key nonclustered(") + name + "),");
            return End();

        }


        public IExistingTable WithPrimaryKeyDescending(string name, bool clustered = false)
        {
            Database.AppendLine("constraint PK_" + _tableName.Replace("dbo.", "") + "_" + name + (clustered ? " primary key clustered( " : " primary key nonclustered(") + name + " desc),");
            return End();
        }


        public IExistingTable WithoutPrimaryKey()
        {
            return End();
        }



        public ITable WithInt32IdentityColumn(string name)
        {
            Database.AppendLine(name + " int identity(1,1) not null,");
            return this;
        }

        public ITable WithInt32IdentityColumn(string name, int seed, int increment)
        {
            Database.AppendLine(name + " int identity(seed,increment) not null,".Replace("seed", seed.ToString()).Replace("increment", increment.ToString()));
            return this;

        }


        public ITable WithInt64IdentityColumn(string name)
        {
            Database.AppendLine(name + " bigint identity(1,1) not null,");
            return this;
        }

        public ITable WithInt64IdentityColumn(string name, int seed, int increment)
        {
            Database.AppendLine(name + " bigint identity(seed,increment) not null,".Replace("seed", seed.ToString()).Replace("increment", increment.ToString()));
            return this;

        }

        public ITable WithInt16IdentityColumn(string name)
        {
            Database.AppendLine(name + " smallint identity(1,1) not null,");
            return this;
        }

        public ITable WithInt16IdentityColumn(string name, int seed, int increment)
        {
            Database.AppendLine(name + " smallint identity(seed,increment) not null,".Replace("seed", seed.ToString()).Replace("increment", increment.ToString()));
            return this;

        }

        public ITable WithGuidIdentityColumn(string name)
        {
            Database.AppendLine(name + " uniqueidentifier not null default(newid()),");
            return this;
        }

        public ITable WithInt32Column(string name)
        {
            Database.AppendLine(name + " int not null,");
            return this;

        }


        public ITable WithInt32ColumnNullable(string name)
        {
            Database.AppendLine(name + " int null,");
            return this;

        }


        public ITable WithInt64Column(string name)
        {
            Database.AppendLine(name + " bigint not null,");
            return this;

        }


        public ITable WithInt64ColumnNullable(string name)
        {
            Database.AppendLine(name + " bigint null,");
            return this;

        }


        public ITable WithInt16Column(string name)
        {
            Database.AppendLine(name + " tinyint not null,");
            return this;

        }


        public ITable WithInt16ColumnNullable(string name)
        {
            Database.AppendLine(name + " tinyint null,");
            return this;

        }


        public ITable WithDateTimeColumn(string name)
        {
            Database.AppendLine(name + " datetime not null,");
            return this;

        }


        public ITable WithDateTimeColumnNullable(string name)
        {
            Database.AppendLine(name + " datetime null,");
            return this;

        }

        public ITable WithBooleanColumn(string name)
        {
            Database.AppendLine(name + " bit not null,");
            return this;

        }

        public ITable WithBooleanColumnNullable(string name)
        {
            Database.AppendLine(name + " bit null,");
            return this;

        }


        public ITable WithStringColumn(string name)
        {
            Database.AppendLine(name + " varchar(max) not null,");
            return this;

        }

        public ITable WithStringColumnNullable(string name)
        {
            Database.AppendLine(name + " varchar(max) null,");
            return this;

        }



        public ITable WithStringColumn(string name, int maxLength)
        {
            Database.AppendLine(name + " varchar(max) not null,".Replace("(max)", "(" + maxLength.ToString() + ")"));
            return this;

        }

        public ITable WithStringColumnNullable(string name, int maxLength)
        {

            Database.AppendLine(name + " varchar(max) null,".Replace("(max)", "(" + maxLength.ToString() + ")"));
            return this;

        }

        public ITable WithUnicodeStringColumn(string name)
        {
            Database.AppendLine(name + " nvarchar(max) not null,");
            return this;

        }

        public ITable WithUnicodeStringColumnNullable(string name)
        {
            Database.AppendLine(name + " nvarchar(max) null,");
            return this;

        }

        public ITable WithUnicodeStringColumn(string name, int maxLength)
        {
            Database.AppendLine(name + " nvarchar(max) not null,".Replace("(max)", "(" + maxLength.ToString() + ")"));
            return this;

        }

        public ITable WithUnicodeStringColumnNullable(string name, int maxLength)
        {
            Database.AppendLine(name + " nvarchar(max) null,".Replace("(max)", "(" + maxLength.ToString() + ")"));
            return this;

        }

        /*
        public ITable WithDoubleColumn(string name)
        {
            Database.AppendLine(name + " float not null,");
            return this;

        }

        public ITable WithByteArrayColumn(string name,int maxLength = 256)
        {
            Database.AppendLine(name + " image(" + maxLength + ") not null,");
            return this;

        }

        public ITable WithMoneyColumn(string name)
        {
            Database.AppendLine(name + " smallmoney not null,");
            return this;
        }

   
        
        public ITable WithDecimalColumn(string name,int mantisa,int exponent)
        {
            Database.AppendLine(name + " decimal(mantisa,exponent) not null,");
            return this;

        }
        */

        /*
       public ITable WithDoubleColumnNullable(string name)
       {
           Database.AppendLine(name + " float null,");
           return this;

       }

       public ITable WithByteArrayColumnNullable(string name,int maxLength = 256)
       {
           Database.AppendLine(name + " image(" + maxLength + ") null,");
           return this;

       }

       public ITable WithMoneyColumnNullable(string name)
       {
           Database.AppendLine(name + " smallmoney null,");
           return this;
       }

     
        
       public ITable WithDecimalColumnNullable(string name,int mantisa,int exponent)
       {
           Database.AppendLine(name + " decimal(mantisa,exponent) null,");
           return this;

       }
       */
    }
}
