using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rapido.Infrastructure.Migration
{
    public interface ITable
    {
        IExistingTable WithPrimaryKey(string name, bool clustered = false);

        IExistingTable WithPrimaryKeyDescending(string name, bool clustered = false);


        IExistingTable WithoutPrimaryKey();


        ITable WithInt32IdentityColumn(string name);


        ITable WithInt32IdentityColumn(string name, int seed, int increment);


        ITable WithInt64IdentityColumn(string name);


        ITable WithInt64IdentityColumn(string name, int seed, int increment);


        ITable WithInt16IdentityColumn(string name);


        ITable WithInt16IdentityColumn(string name, int seed, int increment);


        ITable WithGuidIdentityColumn(string name);


        ITable WithInt32Column(string name);


        ITable WithInt32ColumnNullable(string name);


        ITable WithInt64Column(string name);


        ITable WithInt64ColumnNullable(string name);


        ITable WithInt16Column(string name);


        ITable WithInt16ColumnNullable(string name);


        ITable WithDateTimeColumn(string name);


        ITable WithDateTimeColumnNullable(string name);


        ITable WithBooleanColumn(string name);


        ITable WithBooleanColumnNullable(string name);


        ITable WithStringColumn(string name);


        ITable WithStringColumnNullable(string name);


        ITable WithStringColumn(string name, int maxLength);


        ITable WithStringColumnNullable(string name, int maxLength);


        ITable WithUnicodeStringColumn(string name);


        ITable WithUnicodeStringColumnNullable(string name);


        ITable WithUnicodeStringColumn(string name, int maxLength);


        ITable WithUnicodeStringColumnNullable(string name, int maxLength);



        // ITable WithDoubleColumn(string name);


        // ITable WithByteArrayColumn(string name,int maxLength = 256);


        // ITable WithMoneyColumn(string name);


        // ITable WithDecimalColumn(string name,int mantisa,int exponent);


        // ITable WithDoubleColumnNullable(string name);


        // ITable WithByteArrayColumnNullable(string name,int maxLength = 256);


        // ITable WithMoneyColumnNullable(string name);



        // ITable WithDecimalColumnNullable(string name,int mantisa,int exponent);

    }
}
