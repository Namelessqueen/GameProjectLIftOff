using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	private Player player;
	private ShootingEnemy sEnemy;
	private MeleeEnemy mEnemy;


	public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		// Background:
		EasyDraw canvas = new EasyDraw(800, 600);
		canvas.Clear(Color.MediumPurple);
		
		player = new Player();
		player.SetXY(50, 50);

        sEnemy = new ShootingEnemy("checkers.png", 1, 1);
        sEnemy.SetXY(200, 200);

		mEnemy = new MeleeEnemy("colors.png", 1, 1);
		mEnemy.SetXY(400, 400);

        AddChild(canvas);


		AddChild(player);
		AddChild(sEnemy);
		AddChild(mEnemy);
		Console.WriteLine("MyGame initialized");
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}