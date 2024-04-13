using System;

namespace FileDb.Brokers.Logging;

public interface ILoggingBroker
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(Exception exception);
}