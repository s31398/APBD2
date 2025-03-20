using APBD.Models;

namespace APBD.Interfaces;

public interface IPowerNotifier
{
    void NotifyLowBattery(Device device, int batteryLevel);
}
