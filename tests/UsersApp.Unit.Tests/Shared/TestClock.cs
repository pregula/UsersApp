using UsersApp.Core.Abstractions;

namespace UsersApp.Unit.Tests.Shared;

public class TestClock : IClock
{
    public DateTime Current() => new(2024, 04, 01, 18, 0, 0);
}