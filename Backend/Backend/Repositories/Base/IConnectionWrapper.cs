using System.Data;

namespace Backend.Repositories.Base
{
    public interface IConnectionWrapper : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
