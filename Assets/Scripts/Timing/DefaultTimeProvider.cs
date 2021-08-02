
using System;

public class DefaultTimeProvider : ITimeProvider
{
    public DateTime UtcNow() {
        return DateTime.UtcNow;
    }
}