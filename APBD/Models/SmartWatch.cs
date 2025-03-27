using APBD.Exceptions;
using APBD.Interfaces;

namespace APBD.Models;

/// <summary>
/// Represents a smartwatch device with a battery. Notifies when battery is low.
/// </summary>
/// <param name="id">The smartwatch id.</param>
/// <param name="name">The smartwatch name.</param>
/// <param name="isDeviceTurnedOn">Initial state of the smartwatch.</param>
/// <param name="batteryPercentage">The initial battery percentage.</param>
public class SmartWatch : Device, IPowerNotifier
{
    private int _batteryPercentage;

    /// <summary>
    /// Gets or sets the battery percentage.
    /// When battery is less than 20, a low battery notify is sent.
    /// </summary>
    public int BatteryPercentage
    {
        get => _batteryPercentage;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(BatteryPercentage),
                    "Battery percentage must be >= 0 && <= 100.");
            }

            _batteryPercentage = value;

            if (_batteryPercentage < 20)
            {
                NotifyLowBattery(this, _batteryPercentage);
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the Device.
    /// </summary>
    /// <param name="id">The smartwatch id.</param>
    /// <param name="name">The smartwatch name.</param>
    /// <param name="isDeviceTurnedOn">Initial state of the smartwatch.</param>
    /// <param name="batteryPercentage">The initial battery percentage.</param>
    public SmartWatch(string id, string name, bool isDeviceTurnedOn, int batteryPercentage)
        : base(id, name, isDeviceTurnedOn)
    {
        BatteryPercentage = batteryPercentage;
    }

    /// <summary>
    /// Returns a string representation of the smartwatch.
    /// </summary>
    /// <returns>A string containing the device state.</returns>
    public override string ToString()
    {
        return $"[SmartWatch: Id={Id}, Name={Name}, IsOn={IsDeviceTurnedOn}, Battery={BatteryPercentage}%]";
    }

    /// <summary>
    /// Notifies that the device battery is low by writing a warning to the console.
    /// </summary>
    /// <param name="device">The device with low battery.</param>
    /// <param name="batteryLevel">The current battery level.</param>
    public void NotifyLowBattery(Device device, int batteryLevel)
    {
        Console.WriteLine($"Warn: {device.Name} battery is low ({batteryLevel}%).");
    }

    /// <summary>
    /// Turns on the smartwatch.
    /// After turning on, the battery is decreased by 10.
    /// </summary>
    public override void TurnOn()
    {
        if (BatteryPercentage < 11)
        {
            throw new EmptyBatteryException(
                $"Not possible to turn on {Name}. Battery is too low ({BatteryPercentage}%).");
        }

        base.TurnOn();
        BatteryPercentage = Math.Max(0, BatteryPercentage - 10);
    }
}
