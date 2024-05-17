using Data.Domain;

namespace Data.Repositories.LogEventRepository
{
    public interface ILogEventRepository
    {
        Task<long> Create(LogEvent logEvent);
    }
}
