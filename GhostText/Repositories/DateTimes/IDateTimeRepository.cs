using System;

namespace GhostText.Repositories.DateTimes
{
    public interface IDateTimeRepository
    {
        DateTimeOffset GetCurrentDateTime();
    }
}
