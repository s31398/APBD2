namespace APBD.Exceptions;

/// <summary>
/// thorws Exception when a device cannot connect to the required network.
/// </summary>
/// <param name="message">The error message describing exception reason.</param>
public class ConnectionException(string message) : Exception(message);
