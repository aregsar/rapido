using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Infrastructure.Migration
{



    public interface IExistingTable
    {
        IExistingTable RenameColumn(string name, string newName);
        IExistingTable DropColumn(string name);
        IExistingTable DropPrimaryKey(string name);
        IExistingTable DropIndexOn(string name);
        IExistingTable DropCompositeIndex(string namea, string nameb);
        IExistingTable DropForeignKey(string name, string foreignTableColumn);
        IExistingTable DropConstraint(string constraintName);
        IExistingTable AddInt32ColumnNullable(string name);
        IExistingTable AlterToInt32Column(string name, int defaultValue = 0);
        IExistingTable AlterToInt32ColumnNullable(string name);
        IExistingTable AddInt64ColumnNullable(string name);
        IExistingTable AlterToInt64Column(string name, int defaultValue = 0);
        IExistingTable AlterToInt64ColumnNullable(string name);
        IExistingTable AddInt16ColumnNullable(string name);
        IExistingTable AlterToInt16Column(string name, int defaultValue = 0);
        IExistingTable AlterToInt16ColumnNullable(string name);
        IExistingTable AddDateTimeColumnNullable(string name);
        IExistingTable AlterToDateTimeColumn(string name, string defaultValue = "getdate()");
        IExistingTable AlterToDateTimeColumnNullable(string name);
        IExistingTable AddBooleanColumnNullable(string name);
        IExistingTable AlterToBooleanColumn(string name, int defaultValue = 0);
        IExistingTable AlterToBooleanColumnNullable(string name);
        IExistingTable AddStringColumnNullable(string name);
        IExistingTable AlterToStringColumn(string name, string defaultValue = "''");
        IExistingTable AlterToStringColumnNullable(string name);
        IExistingTable AddStringColumnNullable(string name, int maxLength);
        IExistingTable AlterToStringColumn(string name, int maxLength, string defaultValue = "''");
        IExistingTable AlterToStringColumnNullable(string name, int maxLength);
        IExistingTable AddUnicodeStringColumnNullable(string name);
        IExistingTable AlterToUnicodeStringColumn(string name, string defaultValue = "''");
        IExistingTable AlterToUnicodeStringColumnNullable(string name);
        IExistingTable AddUnicodeStringColumnNullable(string name, int maxLength);
        IExistingTable AlterToUnicodeStringColumn(string name, int maxLength, string defaultValue = "''");
        IExistingTable AlterToUnicodeStringColumnNullable(string name, int maxLength);
        IExistingTable AddIndexOn(string name, bool clustered = false);
        IExistingTable AddIndexOnDescending(string name, bool clustered = false);
        IExistingTable AddUniqueIndexOn(string name, bool clustered = false);
        IExistingTable AddUniqueIndexOnDescending(string name, bool clustered = false);
        IExistingTable AddCompositeUniqueIndex(string namea_optionaldesc, string nameb_optionaldesc, bool clustered = false);
        IExistingTable AddPrimaryKey(string name, bool clustered = true);
        IExistingTable AddPrimaryKeyDescending(string name, bool clustered = true);
        IExistingTable AddForeignKey(string name, string foreignTableColumn, bool cascadeDelete = true);



    }
}
