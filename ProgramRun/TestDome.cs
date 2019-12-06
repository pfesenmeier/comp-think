using System;
using System.Collections.Generic;
using System.Linq;

public class Command
{
    public Command() { }
    public Command(string type, string name, bool active)
    {
        this.Type = type;
        this.Name = name;
        this.Active = active;
    }

    public string Type;
    public string Name;
    public bool Active;
}

public class Device
{
    public Device() { }
    public Device(string id, string[] capabilities)
    {
        this.Id = id;
        this.Capabilities = capabilities;
    }
    public string Id;
    public string[] Capabilities;
}

public class Challenge
{
    private static Command[] Commands()
    {
        Command[] array = new Command[9];

        array[0] = new Command("audio", "turn down volume", true);
        array[1] = new Command("audio", "turn up volume", true);
        array[2] = new Command("music", "next song", true);
        array[3] = new Command("music", "previous song", true);
        array[4] = new Command("music", "purchase song", false); // payment integration still in beta
        array[5] = new Command("channel", "channel up", true);
        array[6] = new Command("channel", "channel down", true);
        array[7] = new Command("temperature", "raise temperature", true);
        array[8] = new Command("temperature", "lower temperature", true);

        return array;
    }

    private static Device[] Devices()
    {
        Device[] array = new Device[5];

        array[0] = new Device("Television", new string[] { "audio", "channel" });
        array[1] = new Device("Stereo system", new string[] { "audio", "music" });
        array[2] = new Device("Kitchen sink", new string[0]);
        array[3] = new Device("Paper shredder", new string[] { "shredding" });
        array[4] = new Device("Smart thermostat", new string[] { "temperature" });

        return array;
    }

    public static List<string> ListCommands(string deviceId)
    {
        var Challenge = new Challenge();
        var Commands = Challenge.Commands();
        var Devices = Challenge.Devices();
        var Device = new Device();
        foreach (var device in Devices)
        {
            if (device.Id == deviceId)
            {
                Device = device;
            }
            else
            {
                Device = new Device();
                return null;
            }
        }
        var Capabilities = Device.Capabilities;
        var DeviceCommands = new List<string>();
        foreach (var command in Commands)
        {
            if (command.Active == true)
            {
                foreach (string capability in Capabilities)
                {
                    if (command.Type == capability)
                    {
                        DeviceCommands.Add(command.Name);
                    }
                }
            }
        }
        return DeviceCommands;
    }

    public static bool CheckValid(string commandName, string deviceId)
    {
        // TODO: fill this out!
        return false;
    }
}