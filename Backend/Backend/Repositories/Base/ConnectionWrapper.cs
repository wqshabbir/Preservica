using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Repositories.Base
{
    [ExcludeFromCodeCoverage]
    public class ConnectionWrapper : IConnectionWrapper
    {
        private bool disposedValue;
        public IDbConnection Connection { get; }
        public ConnectionWrapper(IDbConnection connection)
        {
            Connection = connection;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    Connection.Dispose();
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
