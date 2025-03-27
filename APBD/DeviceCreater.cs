using APBD.Interfaces;
using APBD.Models;

namespace APBD;

/// <summary>
/// This class create a device from one line of text.
/// It parse the line and returns a device object.
/// </summary>
public class DeviceCreater : IDeviceCreater
{
    /// <summary>
    /// Create a device from one text line.
    /// </summary>
    /// <param name="lineFromFile">The text line with device info.</param>
    /// <returns>A device object.</returns>
    public Device CreateDevice(string lineFromFile)
    {
        if (string.IsNullOrWhiteSpace(lineFromFile))
        {
            throw new FormatException("Data line is empty or whitespace.");
        }

        var parts = lineFromFile.Split(',');
        if (parts.Length < 2)
        {
            throw new FormatException("Data line does not contain enough parts.");
        }

        var identifier = parts[0].Trim();

        if (identifier.StartsWith("SW-"))
        {
            if (parts.Length < 4)
            {
                throw new FormatException("SmartWatch data must contain 4 parts.");
            }

            var id = identifier;
            var name = parts[1].Trim();

            var isOn = bool.TryParse(parts[2].Trim(), out bool parsedIsOn)
                ? parsedIsOn
                : throw new FormatException("Invalid value for SmartWatch.");

            var batteryStr = parts[3].Trim().Replace("%", "");
            var battery = int.TryParse(batteryStr, out int parsedBattery)
                ? parsedBattery
                : throw new FormatException("Invalid battery percentage for SmartWatch.");

            return new SmartWatch(id, name, isOn, battery);
        }

        if (identifier.StartsWith("P-"))
        {
            if (parts.Length < 3)
            {
                throw new FormatException("PC data must contain at least 3 parts.");
            }
            
            var id = identifier;
            var name = parts[1].Trim();

            var isOn = bool.TryParse(parts[2].Trim(), out bool parsedIsOn)
                ? parsedIsOn
                : throw new FormatException("Invalid boolean value for PersonalComputer.");

            string os = null;
            if (parts.Length >= 4)
            {
                os = parts[3].Trim();
                if (os == "null")
                {
                    os = null;
                }
            }
            
            return new PersonalComputer(id, name, isOn, os);
        }

        if (identifier.StartsWith("ED-"))
        {
            if (parts.Length < 4)
            {
                throw new FormatException("EmbeddedDevice data must contain 4 parts.");
            }
            
            var id = identifier;
            var name = parts[1].Trim();
            var ip = parts[2].Trim();
            var network = parts[3].Trim();
            
            return new EmbeddedDevice(id, name, false, ip, network);
        }

        throw new FormatException("Unknown device type identifier.");
    }
}