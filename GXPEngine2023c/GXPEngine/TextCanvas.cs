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
    private Sprite backgroundUI;

    private static int cornerOfset = 51;

    private float healthPosX = cornerOfset;
    private float healthPosY = cornerOfset;
    private float fuelPosX = Game.main.width - cornerOfset - 25/2 - 2;
    private float fuelPosY = Game.main.height - cornerOfset - 62;
    

    public TextCanvas() : base(Game.main.width, Game.main.height, false)
    {
        backgroundUI = new Sprite("sprite_fullUI.png", false);
    }

    public void Update()
    {
        
        player = game.FindObjectOfType<Player>();

        DrawSprite(backgroundUI);
        NoStroke();

        HealthBar();
        FuelBar();
       
    }

    void HealthBar()
    {
        if (player == null) return;
            ShapeAlign(CenterMode.Min, CenterMode.Min);
            Fill(255); Rect(healthPosX, healthPosY, 410, 25);
            Fill(255, 0, 0); Rect(healthPosX, healthPosY, Mathf.Clamp((player.HealthUpdate(0) * 4.1f), 0, 509), 25);

    }

    void FuelBar()
    {
        if (player == null) return;
            ShapeAlign(CenterMode.Center, CenterMode.Max);
            Fill(255); Rect(fuelPosX, fuelPosY, 30, 510);
            Fill(164, 148, 104); Rect(fuelPosX, fuelPosY, 30, player.fuelUpdate());
    }
}

