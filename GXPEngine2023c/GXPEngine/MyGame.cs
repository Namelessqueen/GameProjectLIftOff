using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Runtime.CompilerServices;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	public bool isPaused;
    private bool resetXP = false;
    
    // THIS IS IN ALMOST EVERY UPDATE FUNCTION if (((MyGame)game).isPaused) return;
    private TextCanvas canvas;

    public MyGame() : base(1377, 768, false, false, 1366, 768, true)     
	{
        AddChild(new MainMenu());
    }

    public void GameStart()
    {
        
        //text
        MainMenu mainMenu = game.FindObjectOfType<MainMenu>();
        mainMenu.LateDestroy();
        canvas = new TextCanvas();
        //AddChild(new ArduinoInput());
        AddChild(new Level());
        AddChild(canvas);

    }

    public void GameOver() 
    {
        Level level = game.FindObjectOfType<Level>();
        level.LateDestroy();
        canvas.LateDestroy();
        AddChild(new MainMenu("checkers.png"));
    }

    public bool XPReset()
    {
        return resetXP;
    }
	
	void Update() {

		if (canvas == null) return;
        if (canvas.XPUpdate(0) >= canvas.XPNeeded() || Input.GetKeyDown(Key.P))
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