using Data.Domain;
using Data.Repositories.LogEventRepository;
using Data.Utils.UserContext;

namespace Service.LogEventService
{
    public class LogEventService(ILogEventRepository logEventRepository, IUserContext userContext) : ILogEventService
    {
        private readonly IUserContext _userContext = userContext;
        private readonly ILogEventRepository _logEventRepository = logEventRepository;

        public async Task<long> Create(LogEvent logEvent)
        {
            if (!string.IsNullOrEmpty(_userContext.Id))
            {
                logEvent.CreatedById = long.Parse(_userContext.Id);
            }
            logEvent.CreatedTime = DateTime.UtcNow;

            return await _logEventRepository.Create(logEvent);
        }
    }
}
