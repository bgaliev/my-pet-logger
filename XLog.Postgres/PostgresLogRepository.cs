using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using XLog.Core;
using XLog.Core.Models;

namespace XLog.Postgres
{
    public class PostgresLogRepository : ILogRepository
    {
        public async Task PersistAsync<TLogData>(Log<TLogData> log)
        {
            var npgsqlConnection =
                new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=IisLogs;User Id=postgres;Password=postgres;");
            var npgsqlCommand = npgsqlConnection.CreateCommand();
            npgsqlCommand.CommandText =
                "insert into logs(id,type,user_id,created_at,data) values(@id,@type,@user_id,@created_at,@data)";
            npgsqlCommand.Parameters.AddWithValue("@id", NpgsqlDbType.Varchar, log.Id);
            npgsqlCommand.Parameters.AddWithValue("@type", NpgsqlDbType.Varchar, log.Type);
            npgsqlCommand.Parameters.AddWithValue("@user_id", NpgsqlDbType.Varchar, log.UserId ?? "0xDEAD_BEEF");
            npgsqlCommand.Parameters.AddWithValue("@created_at", NpgsqlDbType.Timestamp, log.CreatedAt);
            npgsqlCommand.Parameters.AddWithValue("@data", NpgsqlDbType.Jsonb, log.Data);
            await npgsqlConnection.OpenAsync();
            await npgsqlCommand.ExecuteNonQueryAsync();

            await npgsqlCommand.DisposeAsync();
            await npgsqlConnection.DisposeAsync();
        }
    }
}