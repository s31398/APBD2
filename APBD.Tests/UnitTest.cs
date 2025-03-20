using System;
using System.IO;
using APBD.Models;
using APBD.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APBD.Tests;

[TestClass]
public class EmbeddedDeviceTests
{
    [TestMethod]
    public void TurnOnSucceeds()
    {
        var device = new EmbeddedDevice("1", "Device1", false, 
            "192.168.1.1", "MD Ltd. Wifi");
        device.TurnOn();
            
        Assert.IsTrue(device.IsDeviceTurnedOn);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InvalidIpThrowsException()
    {
        var device = new EmbeddedDevice("1", "Device1", false,
            "999.999.999.999", "MD Ltd. Wifi");
    }

    [TestMethod]
    [ExpectedException(typeof(ConnectionException))]
    public void InvalidNetworkThrowsException()
    {
        var device = new EmbeddedDevice("1", "Device1", false,
            "192.168.1.1", "OtherNetwork");
            
        device.TurnOn();
    }
}

[TestClass]
public class PersonalComputerTests
{
    [TestMethod]
    public void TurnOnSucceeds()
    {
        var pc = new PersonalComputer("1", "PC1", false, "Windows 10");
        pc.TurnOn();
            
        Assert.IsTrue(pc.IsDeviceTurnedOn);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptySystemException))]
    public void NullOperatingSystemThrowsException()
    {
        var pc = new PersonalComputer("1", "PC1", false, null);
        pc.TurnOn();
    }
}

[TestClass]
public class SmartWatchTests
{
    [TestMethod]
    public void TurnOnTest()
    {
        var watch = new SmartWatch("1", "Watch1", false, 52);
            
        watch.TurnOn();
            
        Assert.IsTrue(watch.IsDeviceTurnedOn);
        Assert.AreEqual(42, watch.BatteryPercentage);
    }

    [TestMethod]
    [ExpectedException(typeof(EmptyBatteryException))]
    public void LowBatteryThrowsException()
    {
        var watch = new SmartWatch("1", "Watch1", false, 8);
            
        watch.TurnOn();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void InvalidBatteryPercentageThrowsException()
    {
        var watch = new SmartWatch("1", "Watch1", false, 101);
    }
}

[TestClass]
public class DeviceManagerTests
{
    private string tempFilePath;

    [TestMethod]
    public void AddAndRemoveTest()
    {
        tempFilePath = Path.GetTempFileName();
        var lines = new string[]
        {
            "SW-1,Apple Watch SE2,true,27%",
            "P-1,LinuxPC,false,Linux Mint",
            "ED-1,Pi3,192.168.1.44,MD Ltd.Wifi-1"
        };
        File.WriteAllLines(tempFilePath, lines);
        
        var manager = new DeviceManager(tempFilePath);
            
        var pc = new PersonalComputer("52", "TestPC", false, "MacOs");
        manager.AddDevice(pc);
            
        manager.RemoveDevice("52");
            
        Assert.IsTrue(true);
    }
}