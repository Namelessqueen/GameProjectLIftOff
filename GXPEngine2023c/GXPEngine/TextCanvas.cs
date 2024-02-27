using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class TextCanvas : EasyDraw
{
    private Player player;


    private float healthPosX = 50;
    private float healthPosY = 50;

    public TextCanvas() : base(Game.main.width, Game.main.height, false)
    {

    }

    public void Update()
    {
        
        player = game.FindObjectOfType<Player>();

        if (player != null)
        {
            //Console.WriteLine(player.HealthUpdate(0).ToString());
            StrokeWeight(4);
            ShapeAlign(CenterMode.Min, CenterMode.Center);
            Fill(255);  Rect(healthPosX, healthPosY, 200, 25);
            Fill(255, 0, 0);  Rect(healthPosX, healthPosY, player.HealthUpdate(0) * 2, 25);
        }
    }
}

