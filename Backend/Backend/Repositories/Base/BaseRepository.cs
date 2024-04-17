using Dapper;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Repositories.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository<T>
    {
        private readonly IConfiguration? _configuration;
        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConnectionWrapper GetConnectionWrapper()
        {
            return new ConnectionWrapper(new SqlConnection(_configuration!.GetConnectionString("DefaultConnection")));
        }

        protected async Task<int> ExecuteAsync(string sql, object? param = null, int? commandTimeout = null)
        {
            using var w = GetConnectionWrapper();
            return await w.Connection.ExecuteAsync(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
        }

        protected async Task<IEnumerable<T>> QueryAsync(string sql, object? param = null, int? commandTimeout = null)
        {
            using var w = GetConnectionWrapper();
            return await w.Connection.QueryAsync<T>(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
        }

        protected async Task<T> QueryFirstOrDefaultAsync(string sql, object? param = null, int? commandTimeout = null)
        {
            using var w = GetConnectionWrapper();
            return await w.Connection.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
        }
    }
}
