namespace APBD.Exceptions;

/// <summary>
/// Exception thrown when attempting to turn on a personal computer without an installed operating system.
/// </summary>
/// <param name="message">The error message describing the reason for the exception.</param>
public class EmptySystemException(string message) : Exception(message);
