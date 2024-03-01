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
    private Level level;
    private Sprite backgroundUI;
    private MyGame myGame;
    public static Font gameFont = new Font("04b", 25);

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
    private float wavePosX = Game.main.width/2;
    private float wavePosY = 40;
    private float UltPosX = 50;
    private float UltPosY = 135;
    private float sliderPosX = Game.main.width - 106;
    private float sliderPosY = Game.main.height - 113;

    private int Cooldown;
    private bool UltFlash;

    public TextCanvas() : base(Game.main.width, Game.main.height, false)
    {
        backgroundUI = new Sprite("sprite_fullUI.png", false);
    }

    public void Update()
    {
        
        player = game.FindObjectOfType<Player>();
        level = game.FindObjectOfType<Level>();
        myGame = game.FindObjectOfType<MyGame>();

        DrawSprite(backgroundUI);
        NoStroke();
        if (player == null) return;
        HealthBar();
        FuelBar();
        XPBar();
        WaveText();
        UltBar();
        SliderBar();

    }

    void HealthBar()
    {
            ShapeAlign(CenterMode.Min, CenterMode.Min);
            Fill(255); Rect(healthPosX, healthPosY, 410, 25);
            Fill(102, 255, 102); Rect(healthPosX, healthPosY, 
                Mathf.Clamp(player.HealthUpdate(0) * (4.1f / player.MultiplierHealth()), 0, player.MaxHealth() * (4.1f / player.MultiplierHealth())), 25); ;

    }

    void FuelBar()
    {
            ShapeAlign(CenterMode.Center, CenterMode.Max);
            Fill(255); Rect(fuelPosX, fuelPosY, 30, 510);
            Fill(153, 204, 255); Rect(fuelPosX, fuelPosY, 30, Mathf.Clamp(player.fuelUpdate(),0, 510));
    }
    void SliderBar()
    {
        player.InputSlider();
        ShapeAlign(CenterMode.Center, CenterMode.Max);
        Fill(255); Rect(sliderPosX, sliderPosY, 13, 510);
        Fill(247, 219, 116); Rect(sliderPosX, sliderPosY, 13, Mathf.Clamp(player.InputSlider() * 5.1f, 0 , 510));
    }
    void XPBar()
    {
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Fill(255); Rect(XPPosX, XPPosY, 415, 9);
        Fill(166, 166, 166); Rect(XPPosX, XPPosY, Mathf.Clamp(XPUpdate(0) *(4.15f / multiplierXP), 0, (4.15f/ multiplierXP) * neededXP), 9);

        if (myGame == null) return;
        if (currentXP >= neededXP)
        {
            multiplierXP += 0.2f;
            currentXP = 0;
        }
    }

    void UltBar()
    {
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Fill(255); Rect(UltPosX, UltPosY, 412, 12);
        Fill(255, 0, 0); Rect(UltPosX, UltPosY, Mathf.Clamp((player.UltValue(0) * 4.12f),0, 412),12);
    }

    void WaveText()
    {
        TextAlign(CenterMode.Center, CenterMode.Center);
        ShapeAlign(CenterMode.Center, CenterMode.Center);
        TextFont(gameFont);
        Fill(22, 56, 102); Rect(wavePosX, (wavePosY - 3), 200, 40); 
        Ellipse(wavePosX - 100, (wavePosY - 3), 40, 40); Ellipse(wavePosX + 100, (wavePosY - 3), 40, 40);
        Fill(255); Text(("WAVE " + level.WaveNumber()), wavePosX+2, wavePosY);
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

