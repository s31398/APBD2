using System.Text.RegularExpressions;
using APBD.Exceptions;

namespace APBD.Models;

/// <summary>
/// Represents an embedded device with an IP address and network name.
/// </summary>
public class EmbeddedDevice : Device
{
    private string _ipAddress;

    /// <summary>
    /// Gets or sets the IP address.
    /// </summary>
    public string IpAddress
    {
        get => _ipAddress;
        set
        {
            if (!Regex.IsMatch(value,
                    @"^(25[0-5]|2[0-4]\d|[01]?\d?\d)(\.(25[0-5]|2[0-4]\d|[01]?\d?\d)){3}$"))
            {
                throw new ArgumentException($"Invalid IP: {value}");
            }
            _ipAddress = value;
        }
    }

    /// <summary>
    /// Gets or sets the network name.
    /// </summary>
    public string NetworkName { get; set; }

    /// <summary>
    /// Initializes a new instance of the Device
    /// </summary>
    /// <param name="id">The device id.</param>
    /// <param name="name">The device name.</param>
    /// <param name="isDeviceTurnedOn">Initial state of the device.</param>
    /// <param name="ipAddress">The IP address.</param>
    /// <param name="networkName">The network name.</param>
    public EmbeddedDevice(string id, string name, bool isDeviceTurnedOn, string ipAddress, string networkName)
        : base(id, name, isDeviceTurnedOn)
    {
        IpAddress = ipAddress;
        NetworkName = networkName;
        _ipAddress = ipAddress;
    }

    /// <summary>
    /// Turns on the embedded device.
    /// </summary>
    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }

    /// <summary>
    /// Checks if the network name contains "MD Ltd."
    /// </summary>
    private void Connect()
    {
        if (!NetworkName.Contains("MD Ltd."))
        {
            throw new ConnectionException(
                $"Not possible to connect {Name} to network \"{NetworkName}\". 'MD Ltd.' not present.");
        }
    }

    /// <summary>
    /// Returns a string that represents the embeded device.
    /// </summary>
    /// <returns>A string containing the device data.</returns>
    public override string ToString()
    {
        return $"[EmbeddedDevice: Id={Id}, Name={Name}, IsTurnedOn={IsDeviceTurnedOn}, IP={IpAddress}, Network={NetworkName}]";
    }
}
