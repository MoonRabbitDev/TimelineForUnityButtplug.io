// Starts up a Buttplug Server, creates a client, connects to it, and has that
// client run a device scan. All output goes to the Unity Debug log.
//
// This is just a generic behavior, so you can attach it to any active object in
// your scene and it'll run on scene load.
//Created by the Buttplug.io Project, modified by K The Bunny

using System;
using System.Collections.Generic;
using Buttplug;
using ButtplugUnity;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartServerProcessAndScan : MonoBehaviour
{
    [SerializeField, Range(0, 1)] public float intensity = 0.5f;
    [SerializeField] public TextMeshProUGUI textbox;
    [SerializeField] public Slider slider;

    private ButtplugUnityClient client;

    public List<ButtplugClientDevice> Devices { get; } = new List<ButtplugClientDevice>();

    private void Update()
    {
        textbox.text = "Devices Paired: " + Devices.Count.ToString();
    }

    public void ChangeIntense()
    {

        intensity = slider.value;
        
    }

    public void ChangeIntenseTimeline(float value, int rotateType)
    {

        intensity = value;
        UpdateDevices(rotateType);
        
    }

    public void TurnOnNoRotate()
    {
        foreach (ButtplugClientDevice device in Devices)
        {
            device.SendVibrateCmd(intensity);
            Debug.Log("Command Turned Off");
        }
       

    }
    public void TurnOnCRotate()
    {
        UpdateDevices(1);

    }
        public void TurnOnCCRotate()
    {
        UpdateDevices(2);

    }
    public void TurnOff()
    {
         
        foreach (ButtplugClientDevice device in Devices)
        {
            device.SendStopDeviceCmd();
            Debug.Log("Command Turned Off");
        }

    }

    public void quitGame()
    {
        OnDestroy();
        
    }
    private void OnApplicationQuit() {
        
    }
    private async void Start()
    {
        client = new ButtplugUnityClient("Test Client");
        Debug.Log("Trying to create client");

        // Set up client event handlers before we connect.
        client.DeviceAdded += AddDevice;
        client.DeviceRemoved += RemoveDevice;
        client.ScanningFinished += ScanFinished;

        // Try to create the client.
        try {
            await ButtplugUnityHelper.StartProcessAndCreateClient(client, new ButtplugUnityOptions {
                // Since this is an example, we'll have the unity class output everything its doing to the logs.
                OutputDebugMessages = true,
            });
        }
        catch (ApplicationException e) {
            Debug.Log("Got an error while starting client");
            Debug.Log(e);
            return;
        }

        await client.StartScanningAsync();
    }

    private async void OnDestroy()
    {
        Devices.Clear();

        // On object shutdown, disconnect the client and just kill the server
        // process. Server process shutdown will be cleaner in future builds.
        if (client != null)
        {
            client.DeviceAdded -= AddDevice;
            client.DeviceRemoved -= RemoveDevice;
            client.ScanningFinished -= ScanFinished;
            await client.DisconnectAsync();
            client.Dispose();
            client = null;
        }

        ButtplugUnityHelper.StopServer();
        Debug.Log("I am destroyed now");
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    private void OnValidate()
    {
        
    }

    private void UpdateDevices(int rotation)
    {
        foreach (ButtplugClientDevice device in Devices)
        {
            switch (rotation)
            {
                case 0:
                    device.SendVibrateCmd(intensity);
                    break;
                case 1:
                    device.SendRotateCmd(intensity,true);
                    break;
                case 2:
                    device.SendRotateCmd(intensity,false);
                    break;
                default:
                    break;
            }
            
            //
        }
    }

    private void AddDevice(object sender, DeviceAddedEventArgs e)
    {
        Debug.Log($"Device {e.Device.Name} Connected!");
        Devices.Add(e.Device);
    }

    private void RemoveDevice(object sender, DeviceRemovedEventArgs e)
    {
        Debug.Log($"Device {e.Device.Name} Removed!");
        Devices.Remove(e.Device);
    }

    private void ScanFinished(object sender, EventArgs e)
    {
        Debug.Log("Device scanning is finished!");
    }

    private void Log(object text)
    {
        Debug.Log("<color=red>Buttplug:</color> " + text, this);
    }
}