using System;
using UnityEngine;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;

public class ArduinoDataHandler : MonoBehaviour
{
    public bool useArduino;

    private static SerialPort serialPort = new SerialPort("COM9", 250000);
    public int[] noteStates = new int[88];

    void Start()
    {

        if (useArduino)
        {
            OpenConnection();
        }
    }

    public void OpenConnection()
    {
        if (serialPort != null)
        {
            if(serialPort.IsOpen)
            {
                serialPort.Close();
            }
            else
            {
                serialPort.ReadTimeout = 50;
                serialPort.Open();
            }
        }
    }

    private void OnApplicationQuit()
    {
        TurnOffLeds();
        serialPort.Close();
    }

    public void TurnOffLeds()
    {
        //SendData(255);
    }

    public string ReadData()
    {
        return serialPort.ReadLine();
    }

    public static void SendData(int data)
    {
        byte[] buffer = new byte[] { Convert.ToByte(data) };
        serialPort.Write(buffer, 0, 1);
    }
}
