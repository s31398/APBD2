namespace APBD.Models;

public abstract class Device(string id, string name, bool isDeviceTurnedOn)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public bool IsDeviceTurnedOn { get; set; } = isDeviceTurnedOn;

    public virtual void TurnOn()
    {
        IsDeviceTurnedOn = true;
    }

    public void TurnOff()
    {
        IsDeviceTurnedOn = false;
    }

    public override string ToString()
    {
        return $"[Device: Id={Id}, Name={Name}, IsOn={IsDeviceTurnedOn}]";
    }
}