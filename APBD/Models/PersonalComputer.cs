using APBD.Exceptions;

namespace APBD.Models
{
    public class PersonalComputer(string id, string name, bool isDeviceTurnedOn, string operatingSystem)
        : Device(id, name, isDeviceTurnedOn)
    {
        public string OperatingSystem { get; set; } = operatingSystem;

        public override void TurnOn()
        {
            if (string.IsNullOrWhiteSpace(OperatingSystem))
            {
                throw new EmptySystemException(
                    $"Cannot turn on {Name}. No operating system installed.");
            }

            base.TurnOn();
        }

        public override string ToString()
        {
            return $"[PC: Id={Id}, Name={Name}, IsOn={IsDeviceTurnedOn}, OS={OperatingSystem ?? "null"}]";
        }
    }
}
