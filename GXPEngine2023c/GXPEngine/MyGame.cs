using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	public bool isPaused;
    private bool resetXP = false;
    
    // THIS IS IN ALMOST EVERY UPDATE FUNCTION if (((MyGame)game).isPaused) return;
    private TextCanvas canvas;

    public MyGame() : base(1377, 768, false, true, 1366, 768, true)     
	{
        //text:
        canvas = new TextCanvas();

        AddChild(new Level());
        AddChild(canvas);
    }

    public bool XPReset()
    {
        return resetXP;
    }
	
	void Update() {

		if (canvas.XPUpdate(0) >= canvas.XPNeeded())
		{
			AddChild(new LevelUpCard());
            resetXP = true;
        }
        else resetXP = false;


        if (Input.GetKeyDown(Key.O))
        {
            Console.WriteLine(GetDiagnostics());
        }
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}
}