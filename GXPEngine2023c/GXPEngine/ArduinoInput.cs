using GXPEngine;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

public class ArduinoInput : GameObject
{
    SerialPort port;

    public ArduinoInput()
    {


    }


    void Update()
    {
        if (port == null)
        {
            port = new SerialPort();
            port.PortName = "COM4";
            port.BaudRate = 9600;
            port.RtsEnable = true;
            port.DtrEnable = true;
            port.Open();
        }
        
        //if (Input.GetKey(Key.K))        // while(true)
        if (true)
        {
            string a = port.ReadExisting();
            if (a != "")
                Console.WriteLine("Read from port: " + a);

            /*if (Console.KeyAvailable)     // DON'T NEED TO SEND INFO OVER, ONLY RECEIVE IT
            {
                ConsoleKeyInfo key = Console.ReadKey();
                port.Write(key.KeyChar.ToString());
            }*/
            //Thread.Sleep(30);     // CAUSES IMMENSE AMOUNTS OF LAG
        }
    }




}