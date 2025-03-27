using APBD.Interfaces;
using APBD.Models;

namespace APBD;

/// <summary>
/// This class manage devices.
/// It loads devices from file and let you add, remove, edit, turn on/off and save devices.
/// </summary>
public class DeviceManager : IDeviceManager
{
    private const int MaxCapacity = 15;
    private readonly List<Device> _devices;
    private readonly IDeviceRepository _repository;
    private readonly IDeviceCreater _creater;

    /// <summary>
    /// Get a read only list of devices.
    /// </summary>
    public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

    /// <summary>
    /// Constructor. It loads devices from file by repository.
    /// </summary>
    /// <param name="repository">The repository to load and save data.</param>
    /// <param name="creater">The creater to make device objects.</param>
    public DeviceManager(IDeviceRepository repository, IDeviceCreater creater)
    {
        _devices = new List<Device>();
        _repository = repository;
        _creater = creater;

        LoadDevices();
    }

    /// <summary>
    /// Load devices from repository file.
    /// </summary>
    private void LoadDevices()
    {
        var rawData = _repository.LoadData();

        foreach (var line in rawData)
        {
            try
            {
                var device = _creater.CreateDevice(line);
                if (device == null)
                {
                    throw new FormatException("Parsed device is null.");
                }

                if (_devices.Any(d => d.Id == device.Id))
                {
                    throw new InvalidOperationException($"Duplicate ID, which are not allowed: {device.Id}");
                }

                if (_devices.Count >= MaxCapacity)
                {
                    Console.WriteLine("Maximum capacity reached.");
                    break;
                }

                _devices.Add(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse \"{line}\": {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Add a new device.
    /// </summary>
    /// <param name="device">The device to add.</param>
    /// <exception cref="InvalidOperationException">Thrown if device already exists or capacity is full.</exception>
    public void AddDevice(Device device)
    {
        if (_devices.Count >= MaxCapacity)
        {
            throw new InvalidOperationException("Cannot add more devices.");
        }

        if (_devices.Any(d => d.Id == device.Id))
        {
            throw new InvalidOperationException($"Device with ID={device.Id} already exists.");
        }

        _devices.Add(device);
    }

    /// <summary>
    /// Remove a device by id.
    /// </summary>
    /// <param name="id">The id of device to remove.</param>
    public void RemoveDevice(string id)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device != null)
        {
            _devices.Remove(device);
        }
        else
        {
            Console.WriteLine($"Device ID={id} not found.");
        }
    }

    /// <summary>
    /// Edit a device data.
    /// </summary>
    /// <param name="id">The id of the device.</param>
    /// <param name="valueToInsert">The new value.</param>
    /// <param name="name">The property name to change.</param>
    public void EditDeviceData(string id, object valueToInsert, string name)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"Device with ID={id} not found.");
            return;
        }

        switch (name)
        {
            case "Name":
                device.Name = (string)valueToInsert;
                break;
            case "BatteryPercentage":
                if (device is SmartWatch smartWatch)
                {
                    smartWatch.BatteryPercentage = (int)valueToInsert;
                }
                break;
            case "OperatingSystem":
                if (device is PersonalComputer pesonalComputer)
                {
                    pesonalComputer.OperatingSystem = (string)valueToInsert;
                }
                break;
            case "IPAddress":
                if (device is EmbeddedDevice embeddedDevice)
                {
                    embeddedDevice.IpAddress = (string)valueToInsert;
                }
                break;
            case "NetworkName":
                if (device is EmbeddedDevice embeddedDevice1)
                {
                    embeddedDevice1.NetworkName = (string)valueToInsert;
                }
                break;
            default:
                Console.WriteLine($"Property '{name}' not found.");
                break;
        }
    }

    /// <summary>
    /// Turn on a device by id.
    /// </summary>
    /// <param name="id">The id of the device to turn on.</param>
    public void TurnOnDevice(string id)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"Device with ID={id} not found.");
            return;
        }

        try
        {
            device.TurnOn();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error happended, while turning on device {id}: {ex.Message}");
        }
    }

    /// <summary>
    /// Turn off a device by id.
    /// </summary>
    /// <param name="id">The id of the device to turn off.</param>
    public void TurnOffDevice(string id)
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"Device with ID={id} not found.");
            return;
        }

        try
        {
            device.TurnOff();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error turning off device {id}: {ex.Message}");
        }
    }

    /// <summary>
    /// Show all devices on console.
    /// </summary>
    public void ShowAllDevices()
    {
        foreach (var device in _devices)
        {
            Console.WriteLine(device);
        }
    }

    /// <summary>
    /// Save all devices to repository.
    /// </summary>
    public void SaveDevices()
    {
        var lines = new List<string>();
        foreach (var d in _devices)
        {
            if (d is SmartWatch sw)
            {
                lines.Add($"{sw.Id},{sw.Name},{sw.IsDeviceTurnedOn},{sw.BatteryPercentage}%");
            }
            else if (d is PersonalComputer pc)
            {
                var osString = pc.OperatingSystem ?? "null";
                lines.Add($"{pc.Id},{pc.Name},{pc.IsDeviceTurnedOn},{osString}");
            }
            else if (d is EmbeddedDevice ed)
            {
                lines.Add($"{ed.Id},{ed.Name},{ed.IpAddress},{ed.NetworkName}");
            }
        }

        _repository.SaveData(lines);
    }
}