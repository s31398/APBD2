using APBD.Interfaces;

namespace APBD;

/// <summary>
/// This static class creates a DeviceManager object.
/// It uses a FileDeviceRepository and a DeviceCreater.
/// </summary>
public static class DeviceManagerCreator
{
    /// <summary>
    /// Create a DeviceManager using a file path.
    /// </summary>
    /// <param name="filePath">The file path where device data is stored.</param>
    /// <returns>An IDeviceManager object.</returns>
    public static IDeviceManager CreateDeviceManager(string filePath)
    {
        IDeviceRepository repository = new FileDeviceRepository(filePath);
        IDeviceCreater creater = new DeviceCreater();
        return new DeviceManager(repository, creater);
    }
}