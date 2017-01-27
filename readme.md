[![NuGet status](https://img.shields.io/nuget/v/Simple.Data.Npgsql.svg)](https://www.nuget.org/packages/Simple.Data.Npgsql)
--
# Simple.Data.NpgSql
An adapter to PostgreSql databases for Simple.Data

# Use
The adapter makes use of the Npgsql open source database adapter. 

To use this adapter, you will need to add the provider name to your app.config or web.config file:

<pre>&lt;system.data&gt;
  &lt;DbProviderFactories&gt;
    &lt;add name="Npgsql Data Provider"
         invariant="Npgsql"
         support="FF"
         description=".Net Framework Data Provider for Postgresql Server"
         type="Npgsql.NpgsqlFactory, Npgsql" /&gt;
  &lt;/DbProviderFactories&gt;
&lt;/system.data&gt;
</pre>
  
# PostgreSql Types
The adapter is currently tested for all basic PostgreSql types documented in 9.0, including ARRAY types and refcursor types.  

Procedures that return refcursor are not yet supported.

User defined types will be returned as generic objects.

TODO: Investigate adding support for PostGIS types

# Identity column
PostgreSql does not support explicitly stating that a column is an identity column.  This adapter chooses the first column in the table that is not nullable, and that has an auto-incrementing default value (i.e., the column's default value is nextval(<some sequence>)).  This can be either handled implicitly using PostgreSql's serial or bigserial type or explicitly using PostgreSql's integer or bigint type and a sequence.

If there are multiple auto-incrementing columns, this adapter will use the first as the identity column.

# Timestamps
.NET's DateTime does not appear to handle PostgreSql's timestamp with time zone and time with time zone types.  Either that or I don't understand how to use it properly.  In either case, I would recommend you stick to the timestamp and time (without time zone) types or results may be unexpected.

I'm not sure yet whether this is a PostgreSql issue or an Npgsql issue.  More likely the latter.

TODO: Investigate using Npgsql's NpgsqlTimestamp type.


# Testing
The default tests assume a database cluster installed at localhost:5432.
Tests assume that the superuser account is named 'postgres' with password 'postgres' and that there is a default database named 'postgres'.  If your system is not set up like that, all these settings can be changed in the app.config file.

