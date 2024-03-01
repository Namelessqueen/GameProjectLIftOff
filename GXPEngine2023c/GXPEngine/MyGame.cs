using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Runtime.CompilerServices;
using GXPEngine.Core;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

    private float channelVolume15 = .3f; // LevelUp.wav
    private float channelVolume16 = 2f; // EngineStart (Only use if neccesary).wav


    public bool isPaused;
    private bool resetXP = false;
    
    // THIS IS IN ALMOST EVERY UPDATE FUNCTION if (((MyGame)game).isPaused) return;
    private TextCanvas canvas;
    private EasyDraw deathCanvas;
    FMODSoundSystem soundSystem;

    public MyGame() : base(1377, 768, false, false, 1366, 768, true)
    {
        AddChild(new MainMenu());
        soundSystem = new FMODSoundSystem();
        //AddChild(new ArduinoInput()); ///////////////////////////////////////////////////////////
    }

    public void GameStart()
    {
        
        //text
        MainMenu mainMenu = game.FindObjectOfType<MainMenu>();
        mainMenu.LateDestroy();
        if(deathCanvas != null) deathCanvas.LateDestroy();
        canvas = new TextCanvas();
        AddChild(new Level());
        AddChild(canvas);


        soundSystem.PlaySound(soundSystem.LoadSound("EngineStart (Only use if neccesary).wav", false), 16, false, channelVolume16, 0);

    }

    public void GameOver() 
    {
        deathCanvas = new EasyDraw(game.width,game.height);
        Level level = game.FindObjectOfType<Level>();
        level.LateDestroy();
        canvas.LateDestroy();
        /*ArduinoInput arduinoInput = game.FindObjectOfType<ArduinoInput>();
        if (arduinoInput != null) arduinoInput.LateDestroy();*/
        AddChild(new MainMenu("Game over screen2.png"));
        AddChild(deathCanvas);
        deathCanvas.TextFont(TextCanvas.gameFont); deathCanvas.TextAlign(CenterMode.Center,CenterMode.Center);
        deathCanvas.Text("Your final wave is : " + level.WaveNumber(), game.width / 2, game.height / 3);
    }

    public bool XPReset()
    {
        return resetXP;
    }
	
	void Update() {

		if (canvas == null) return;
        if (canvas.XPUpdate(0) >= canvas.XPNeeded() || Input.GetKeyDown(Key.X))
		{
			AddChild(new LevelUpCard());
            resetXP = true;

            soundSystem.PlaySound(soundSystem.LoadSound("LevelUp.wav", false), 15, false, channelVolume15, 0);
        }
        else resetXP = false;


        if (Input.GetKeyDown(Key.U))
        {
            Console.WriteLine(GetDiagnostics());
        }
    }

	static void Main()                          
	{
		new MyGame().Start();                   
	}
}