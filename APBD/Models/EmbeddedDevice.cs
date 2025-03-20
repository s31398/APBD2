using System.Text.RegularExpressions;
using APBD.Exceptions;

namespace APBD.Models;

public class EmbeddedDevice : Device
{
    private string _ipAddress;
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

    public string NetworkName { get; set; }

    public EmbeddedDevice(string id, string name, bool isDeviceTurnedOn,
        string ipAddress, string networkName)
        : base(id, name, isDeviceTurnedOn)
    {
        IpAddress = ipAddress;
        _ipAddress = ipAddress;
        NetworkName = networkName;
    }

    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }

    private void Connect()
    {
        if (!NetworkName.Contains("MD Ltd."))
        {
            throw new ConnectionException(
                $"Cannot connect {Name} to network \"{NetworkName}\". 'MD Ltd.' not found.");
        }
    }

    public override string ToString()
    {
        return $"[EmbeddedDevice: Id={Id}, Name={Name}, IsOn={IsDeviceTurnedOn}, IP={IpAddress}, Network={NetworkName}]";
    }
}