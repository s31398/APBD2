namespace APBD.Interfaces;

/// <summary>
/// Interface for repository that load and save device data
/// </summary>
public interface IDeviceRepository
{
    /// <summary>
    /// Load all lines from file
    /// </summary>
    /// <returns>List of strings with device data</returns>
    List<string> LoadData();

    /// <summary>
    /// Save lines to data source
    /// </summary>
    /// <param name="lines">Lines with device info</param>
    void SaveData(List<string> lines);
}