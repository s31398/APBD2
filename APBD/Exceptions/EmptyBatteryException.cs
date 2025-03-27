namespace APBD.Exceptions;

/// <summary>
/// Exception is thrown when attempting to turn on a device with not enough battery level.
/// </summary>
/// <param name="message">The error message describs reason for an exception.</param>
public class EmptyBatteryException(string message) : Exception(message);
