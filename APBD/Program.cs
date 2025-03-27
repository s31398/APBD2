using APBD.Interfaces;
using APBD.Models;

namespace APBD
{
    class Program
    {
        static void Main()
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"); 

            if (!File.Exists(filePath))
            {
                CreateSampleFile(filePath);
            }

            IDeviceManager manager = null;
            try
            {
                manager = DeviceManagerCreator.CreateDeviceManager(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing DeviceManager: " + ex.Message);
                return;
            }

            Console.WriteLine("Devices loaded from file:");
            manager.ShowAllDevices();

            if (manager is DeviceManager deviceManager)
            {
                foreach (var device in deviceManager.Devices)
                {
                    Console.WriteLine($"\nAttempting to turn on device {device.Id}:");
                    manager.TurnOnDevice(device.Id);
                }
            }
            else
            {
                Console.WriteLine("Unable to iterate devices.");
            }

            Console.WriteLine("\nAdding a new SmartWatch");
            try
            {
                SmartWatch newWatch = new SmartWatch("1", "New SmartWatch", false, 50);
                manager.AddDevice(newWatch);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding device: " + ex.Message);
            }

            Console.WriteLine("\nEditing device with ID '1' name to 'Updated PC':");
            manager.EditDeviceData("1", "Updated PC", "Name");

            Console.WriteLine("\nDevices after editing:");
            manager.ShowAllDevices();

            string outputFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.txt"); 
            try
            {
                manager.SaveDevices();
                File.Copy(filePath, outputFilePath, true);
                Console.WriteLine($"\nDevices saved to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving devices: " + ex.Message);
            }
        }

        static void CreateSampleFile(string filePath)
        {
            var sampleLines = new string[]
            {
                "SW-1,Apple Watch SE2,true,27%",
                "P-1,LinuxPC,false,Linux Mint",
                "P-2,ThinkPad T440,false",
                "ED-1,Pi3,192.168.1.44,MD Ltd.Wifi-1",
                "ED-2,Pi4,192.168.1.45,eduroam",
                "ED-3,Pi4,whatisIP,MyWifiName",
                "Capital33,BestPC_Ever,null,456217865891789",
                "16//16//16//16"
            };

            try
            {
                File.WriteAllLines(filePath, sampleLines);
                Console.WriteLine($"Sample file '{filePath}' created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sample file: {ex.Message}");
            }
        }
    }
}