namespace APBD.Models;

/// <summary>
/// Base class for all devices. Contains an id, a name and a status flag indicating if the device is on.
/// </summary>
/// <param name="id">The device id.</param>
/// <param name="name">The device name.</param>
/// <param name="isDeviceTurnedOn">Initial state of the device.</param>
public abstract class Device(string id, string name, bool isDeviceTurnedOn)
{
    /// <summary>
    /// Gets or sets the device id.
    /// </summary>
    public string Id { get; set; } = id;

    /// <summary>
    /// Gets or sets the device name.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets the current power state of the device.
    /// </summary>
    public bool IsDeviceTurnedOn { get; private set; } = isDeviceTurnedOn;

    /// <summary>
    /// Turns the device on.
    /// </summary>
    public virtual void TurnOn()
    {
        IsDeviceTurnedOn = true;
    }

    /// <summary>
    /// Turns the device off.
    /// </summary>
    public void TurnOff()
    {
        IsDeviceTurnedOn = false;
    }

    /// <summary>
    /// Returns a string representation of the device.
    /// </summary>
    /// <returns>A string containing the device id, name, and power state.</returns>
    public override string ToString()
    {
        return $"[Device: Id={Id}, Name={Name}, IsOn={IsDeviceTurnedOn}]";
    }
}
