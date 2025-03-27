using APBD.Models;

namespace APBD.Interfaces;

/// <summary>
/// This interface help create device from line in file
/// </summary>
public interface IDeviceCreater
{
    /// <summary>
    /// Parse one text line and create device
    /// </summary>
    /// <param name="lineFromFile">Line with device info</param>
    /// <returns>Device object if line is ok</returns>
    Device CreateDevice(string lineFromFile);
}