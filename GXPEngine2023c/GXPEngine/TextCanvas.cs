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
    private MyGame myGame;

    private static int cornerOfset = 51;

    private float currentXP;
    private float neededXP;
    private float multiplierXP = 1f;

    private float healthPosX = cornerOfset;
    private float healthPosY = cornerOfset;
    private float fuelPosX = Game.main.width - cornerOfset - 25/2 - 2;
    private float fuelPosY = Game.main.height - cornerOfset - 62;
    private float XPPosX = cornerOfset - 2;
    private float XPPosY = cornerOfset + 48;

    
    public TextCanvas() : base(Game.main.width, Game.main.height, false)
    {
        backgroundUI = new Sprite("sprite_fullUI.png", false);
    }

    public void Update()
    {
        
        player = game.FindObjectOfType<Player>();
        myGame = game.FindObjectOfType<MyGame>();

        DrawSprite(backgroundUI);
        NoStroke();
        if (player == null) return;
        HealthBar();
        FuelBar();
        XPBar();
        if (myGame == null) return;
        //Console.WriteLine(currentXP);

    }

    void HealthBar()
    {
            ShapeAlign(CenterMode.Min, CenterMode.Min);
            Fill(255); Rect(healthPosX, healthPosY, 410, 25);
            Fill(255, 0, 0); Rect(healthPosX, healthPosY, Mathf.Clamp((player.HealthUpdate(0) * 4.1f), 0, 509), 25);

    }

    void FuelBar()
    {
            ShapeAlign(CenterMode.Center, CenterMode.Max);
            Fill(255); Rect(fuelPosX, fuelPosY, 30, 510);
            Fill(164, 148, 104); Rect(fuelPosX, fuelPosY, 30, Mathf.Clamp(player.fuelUpdate(),0, 510));
    }
    void XPBar()
    {
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Fill(255); Rect(XPPosX, XPPosY, 415, 9);
        Fill(102, 255, 153); Rect(XPPosX, XPPosY, Mathf.Clamp(XPUpdate(0) *(4.15f / multiplierXP), 0, (4.15f/ multiplierXP) * neededXP), 9);

        if (myGame == null) return;
        if (currentXP >= neededXP)
        {
            multiplierXP += 0.2f;
            currentXP = 0;
        }
    }

    public float XPUpdate(float pChange)
    {
        float change = pChange;
        neededXP = 100 * multiplierXP;
       
        currentXP = currentXP + change;
        return currentXP;
    }
    public float XPNeeded()
    {
        return neededXP;
    }
}

