using APBD.Exceptions;

namespace APBD.Models;

/// <summary>
/// Represents a personal computer device that may have an operating system.
/// </summary>
/// <param name="id">The computer id.</param>
/// <param name="name">The computer name.</param>
/// <param name="isDeviceTurnedOn">Initial state of the computer.</param>
/// <param name="operatingSystem">The operating system installed.</param>
public class PersonalComputer(string id, string name, bool isDeviceTurnedOn, string operatingSystem)
    : Device(id, name, isDeviceTurnedOn)
{
    /// <summary>
    /// Gets or sets the operating system.
    /// </summary>
    public string OperatingSystem { get; set; } = operatingSystem;

    /// <summary>
    /// Turns on the computer.
    /// </summary>
    public override void TurnOn()
    {
        if (string.IsNullOrWhiteSpace(OperatingSystem))
        {
            throw new EmptySystemException(
                $"Not possible to turn on {Name}. No operating system present.");
        }
        base.TurnOn();
    }

    /// <summary>
    /// Returns a string that represents the personl computer.
    /// </summary>
    /// <returns>A string containing the computer data</returns>
    public override string ToString()
    {
        return $"[PC: Id={Id}, Name={Name}, IsTurnedOn={IsDeviceTurnedOn}, OS={OperatingSystem ?? "null"}]";
    }
}
