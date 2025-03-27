using APBD.Models;

namespace APBD.Interfaces;

/// <summary>
/// Interface for managing devices
/// </summary>
public interface IDeviceManager
{
    /// <summary>
    /// Show all devices in console
    /// </summary>
    void ShowAllDevices();
        
    /// <summary>
    /// Add new device to manager
    /// </summary>
    /// <param name="device">Device to add</param>
    void AddDevice(Device device);

    /// <summary>
    /// Turn on device by ID
    /// </summary>
    /// <param name="id">ID of device</param>
    void TurnOnDevice(string id);

    /// <summary>
    /// Edit data of device by ID
    /// </summary>
    /// <param name="id">Device ID</param>
    /// <param name="valueToInsert">New value</param>
    /// <param name="name">Property name to change</param>
    void EditDeviceData(string id, object valueToInsert, string name);

    /// <summary>
    /// Save all devices data to file
    /// </summary>
    void SaveDevices();
}