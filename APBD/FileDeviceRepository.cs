using APBD.Interfaces;

namespace APBD;

/// <summary>
/// This class read and write device data from a file.
/// </summary>
/// <param name="filePath">The file path to read and write data.</param>
public class FileDeviceRepository(string filePath) : IDeviceRepository
{
    /// <summary>
    /// Load all device data from the file.
    /// </summary>
    /// <returns>A list of text lines from the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if file does not exist.</exception>
    public List<string> LoadData()
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
        return new List<string>(File.ReadAllLines(filePath));
    }

    /// <summary>
    /// Save device data lines to the file.
    /// </summary>
    /// <param name="lines">The list of text lines to save.</param>
    public void SaveData(List<string> lines)
    {
        try
        {
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }
}