using GXPEngine;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public class ArduinoInput : GameObject
{
    SerialPort port;
    private int sliderValue;

    public ArduinoInput()
    {

        if (port == null)
        {
            port = new SerialPort();
            port.PortName = "COM9"; ////////////////////////////////////
            port.BaudRate = 9600;
            port.RtsEnable = true;
            port.DtrEnable = true;
            port.Open();
        }
        
    }

    public int SliderValue()
    {

        string a = port.ReadLine();

        sliderValue = int.Parse(a);

        return sliderValue;
    }


    /*
    void Update()
    {

        //if (Input.GetKey(Key.K))        // while(true)
        if (true)
        {
            string a = port.ReadExisting();
            int b = port.ReadByte();
            //int c = 1 * Convert.ToInt32(a);
            //Int32.TryParse(a, out c);
            //sliderValue = c;
            //Console.WriteLine(a);
            if (a.CompareTo("0") == true /* || a == "1" || a == "2" || a == "3" || a == "4"
                || a == "5" || a == "6" || a == "7" || a == "8" || a == "9"*//*)
            {
                sliderValue = 10;
                Console.WriteLine(sliderValue);
            }

            if (int.TryParse(a, out sliderValue))
            {

                //Console.WriteLine(sliderValue);


            }

            /*if (int.TryParse(a, out sliderValue))
            {

            }
            else 
            {
                sliderValue++;
                    
            }*/

            /*if (a != "" && a != null) {
                //Console.WriteLine("Read from port: " + a);
                
                //sliderValue = int.Parse(a);
                //sliderValue = Convert.ChangeType(a, typeof(float));
                //sliderValue = Convert.ToInt32(a);
            }*/

            /*if (Console.KeyAvailable)     // DON'T NEED TO SEND INFO OVER, ONLY RECEIVE IT
            {
                ConsoleKeyInfo key = Console.ReadKey();
                port.Write(key.KeyChar.ToString());
            }*//*
            //Thread.Sleep(30);     // CAUSES IMMENSE AMOUNTS OF LAG
        }
    }
            */
}