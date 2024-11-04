using Dapper;
using MySqlConnector;

namespace FeiraTech.Infrastructure.Migrations
{
    public static class DatabaseMigrations
    {
        public static void MigrateDatabase(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
            //pega o nome do banco de dados
            var databaseName = connectionStringBuilder.Database;
            //remove o nome do banco de dados da string de conexão
            connectionStringBuilder.Remove("Database");
            //cria a conexão
            using var connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
            //verifica se o banco de dados existe
            var parameters = new DynamicParameters();
            //adiciona o nome do banco de dados como parâmetro
            parameters.Add("databaseName", databaseName);
            //executa a query para verificar se o banco de dados existe
            var records = connection.Query("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @databaseName", parameters);
            //se não existir, cria o banco de dados
            if (records.AsList().Count == 0)
            {
                connection.Execute($"CREATE DATABASE {databaseName}");
            }
        }
    }
}
