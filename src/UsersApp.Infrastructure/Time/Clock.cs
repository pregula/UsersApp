using UsersApp.Core.Abstractions;

namespace UsersApp.Infrastructure.Time;

public class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}