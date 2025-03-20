using APBD.Models;

namespace APBD;

public class DeviceManager
{
    private const int MaxCapacity = 15;
    private List<Device> devices;

    public DeviceManager(string filePath)
    {
        devices = new List<Device>();

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            try
            {
                var device = ParseDevice(line);
                if (devices.Count >= MaxCapacity)
                {
                    Console.WriteLine("Max capacity reached (15). Cannot add more devices.");
                    break;
                }

                devices.Add(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse line \"{line}\": {ex.Message}");
            }
        }
    }

    private Device ParseDevice(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            return null;
        }

        var parts = line.Split(',');
        if (parts.Length < 2)
        {
            return null;
        }

        string firstPart = parts[0].Trim();

        if (firstPart.StartsWith("SW-"))
        {
            if (parts.Length < 4)
            {
                return null;
            }

            string id = firstPart.Substring(3);
            string name = parts[1].Trim();
            bool isOn = bool.Parse(parts[2].Trim());

            string batteryStr = parts[3].Trim().Replace("%", "");
            int battery = int.Parse(batteryStr);

            return new SmartWatch(id, name, isOn, battery);
        }
        else if (firstPart.StartsWith("P-"))
        {
            string id = firstPart.Substring(2);
            string name = parts[1].Trim();
            bool isOn = bool.Parse(parts[2].Trim());

            string os = (parts.Length >= 4) ? parts[3].Trim() : null;
            if (os == "null")
            {
                os = null;
            }

            return new PersonalComputer(id, name, isOn, os);
        }
        else if (firstPart.StartsWith("ED-"))
        {
            if (parts.Length < 4)
            {
                return null;
            }

            string id = firstPart.Substring(3);
            string name = parts[1].Trim();
            string ip = parts[2].Trim();
            string network = parts[3].Trim();

            return new EmbeddedDevice(id, name, false, ip, network);
        }
        else
        {
            return null;
        }
    }

    public void AddDevice(Device device)
    {
        if (devices.Count >= MaxCapacity)
        {
            throw new InvalidOperationException("Cannot add more devices.");
        }

        devices.Add(device);
    }

    public void RemoveDevice(string id)
    {
        var device = devices.Find(d => d.Id == id);
        if (device != null)
        {
            devices.Remove(device);
        }
        else
        {
            Console.WriteLine($"Device with ID={id} not found.");
        }
    }

    public void EditDeviceData(string id, object newValue, string propertyName)
    {
        var device = devices.Find(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"Device with ID={id} not found.");
            return;
        }

        switch (propertyName)
        {
            case "Name":
                device.Name = (string)newValue;
                break;

            case "BatteryPercentage":
                if (device is SmartWatch sw)
                {
                    sw.BatteryPercentage = (int)newValue;
                }

                break;

            case "OperatingSystem":
                if (device is PersonalComputer pc)
                {
                    pc.OperatingSystem = (string)newValue;
                }

                break;

            case "IPAddress":
                if (device is EmbeddedDevice ed)
                {
                    ed.IpAddress = (string)newValue;
                }

                break;

            case "NetworkName":
                if (device is EmbeddedDevice ed2)
                {
                    ed2.NetworkName = (string)newValue;
                }

                break;

            default:
                Console.WriteLine($"Property '{propertyName}' not found or not editable.");
                break;
        }
    }

    public void TurnOnDevice(string id)
    {
        Device device = null;
        
        foreach (var d in devices)
        {
            if (d.Id == id)
            {
                device = d;
                break;
            }
        }

        if (device == null)
        {
            Console.WriteLine($"Device with ID={id} not found.");
            return;
        }

        device.TurnOn();
    }

    public void TurnOffDevice(string id)
    {
        var device = devices.Find(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"Device with ID={id} not found.");
            return;
        }

        device.TurnOff();
    }

    public void ShowAllDevices()
    {
        foreach (var d in devices)
        {
            Console.WriteLine(d);
        }
    }

    public void SaveDataToFile(string filePath)
    {
        var lines = new List<string>();

        foreach (var d in devices)
        {
            if (d is SmartWatch sw)
            {
                lines.Add($"SW-{sw.Id},{sw.Name},{sw.IsDeviceTurnedOn},{sw.BatteryPercentage}%");
            }
            else if (d is PersonalComputer pc)
            {
                var osString = pc.OperatingSystem ?? "null";
                lines.Add($"P-{pc.Id},{pc.Name},{pc.IsDeviceTurnedOn},{osString}");
            }
            else if (d is EmbeddedDevice ed)
            {
                lines.Add($"ED-{ed.Id},{ed.Name},{ed.IpAddress},{ed.NetworkName}");
            }
        }

        File.WriteAllLines(filePath, lines);
    }
}