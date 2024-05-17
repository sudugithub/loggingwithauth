using Data.Domain;

namespace Data.Repositories.LogEventRepository
{
    public class LogEventRepository(Repository repository) : ILogEventRepository
    {
        private readonly Repository _repository = repository;

        public async Task<long> Create(LogEvent logEvent)
        {
            await _repository.LogEvents.AddAsync(logEvent);
            await _repository.SaveChangesAsync();

            return logEvent.Id;
        }
    }
}
