using System;                             
using GXPEngine;                               
using System.Drawing;                           

public class MyGame : Game {
	public MyGame() : base(800, 600, false)     
	{

		EasyDraw canvas = new EasyDraw(800, 600);
		canvas.Clear(Color.MediumPurple);
		canvas.Fill(Color.Yellow);
		canvas.Ellipse(width / 2, height / 2, 200, 200);
		canvas.Fill(50);
		canvas.TextSize(32);
		canvas.TextAlign(CenterMode.Center, CenterMode.Center);
		canvas.Text("Welcome!", width / 2, height / 2);

		// Add the canvas to the engine to display it:
		AddChild(canvas);
		Console.WriteLine("MyGame initialized");
	}

	void Update() {
	
	}

	static void Main()                    
	{
		new MyGame().Start();            
	}
}