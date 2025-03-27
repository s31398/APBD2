using APBD.Models;

namespace APBD.Interfaces;

/// <summary>
/// Interface for notify about low battery in device
/// </summary>
public interface IPowerNotifier
{
    /// <summary>
    /// Show warning when device battery is low
    /// </summary>
    /// <param name="device">Device with low battery</param>
    /// <param name="batteryLevel">Battery level value</param>
    void NotifyLowBattery(Device device, int batteryLevel);
}