using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	


	public MyGame() : base(1377, 768, false, true, 1366, 768, true)     
	{

		AddChild(new Level());
	}

	
	void Update() {
        if (Input.GetKeyDown(Key.P))
        {
            Console.WriteLine(GetDiagnostics());
        }
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}
}