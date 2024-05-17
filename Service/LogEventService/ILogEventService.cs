using Data.Domain;

namespace Service.LogEventService
{
    public interface ILogEventService
    {
        Task<long> Create(LogEvent logEvent);
    }
}
