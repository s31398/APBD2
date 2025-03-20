using APBD.Exceptions;
using APBD.Interfaces;

namespace APBD.Models
{
    public class SmartWatch : Device, IPowerNotifier
    {
        private int _batteryPercentage;

        public int BatteryPercentage
        {
            get => _batteryPercentage;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(BatteryPercentage),
                        "Battery percentage must be between 0 and 100.");
                }

                _batteryPercentage = value;

                if (_batteryPercentage < 20)
                {
                    NotifyLowBattery(this, _batteryPercentage);
                }
            }
        }

        public SmartWatch(string id, string name, bool isDeviceTurnedOn, int batteryPercentage)
            : base(id, name, isDeviceTurnedOn)
        {
            BatteryPercentage = batteryPercentage;
        }

        public override void TurnOn()
        {
            if (BatteryPercentage < 11)
            {
                throw new EmptyBatteryException(
                    $"Cannot turn on {Name}. Battery is too low ({BatteryPercentage}%).");
            }

            base.TurnOn();
            BatteryPercentage = Math.Max(0, BatteryPercentage - 10);
        }

        public void NotifyLowBattery(Device device, int batteryLevel)
        {
            Console.WriteLine($"Warning: {device.Name} battery is low ({batteryLevel}%).");
        }

        public override string ToString()
        {
            return $"[SmartWatch: Id={Id}, Name={Name}, IsOn={IsDeviceTurnedOn}, Battery={BatteryPercentage}%]";
        }
    }
}
