using System;

namespace GhostText.Repositories.DateTimes
{
    public class DateTimeRepository : IDateTimeRepository
    {
        public DateTimeOffset GetCurrentDateTime() =>
            DateTimeOffset.UtcNow;
    }
}
