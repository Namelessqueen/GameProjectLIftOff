﻿using System;
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

    private static int cornerOfset = 40;

    private float healthPosX = cornerOfset;
    private float healthPosY = cornerOfset;
    private float fuelPosX = Game.main.width - cornerOfset - 25;
    private float fuelPosY = Game.main.height - cornerOfset;


    public TextCanvas() : base(Game.main.width, Game.main.height, false)
    {

    }

    public void Update()
    {
        
        player = game.FindObjectOfType<Player>();

        HealthBar();
        FuelBar();
    }

    void HealthBar()
    {
        if (player != null)
        {
            //Console.WriteLine(player.HealthUpdate(0).ToString());
            StrokeWeight(4);
            ShapeAlign(CenterMode.Min, CenterMode.Min);
            Fill(255); Rect(healthPosX, healthPosY, 200, 25);
            Fill(255, 0, 0); Rect(healthPosX, healthPosY, player.HealthUpdate(0) * 2, 25);
        }
    }

    void FuelBar()
    {
        if (player != null)
        {
            //Console.WriteLine(player.fuelUpdate().ToString());
            StrokeWeight(4);
            ShapeAlign(CenterMode.Min, CenterMode.Max);
            Fill(255); Rect(fuelPosX, fuelPosY, 25, Game.main.height / 2);
            Fill(164, 148, 104); Rect(fuelPosX, fuelPosY, 25, (int)player.fuelUpdate());
        }
    }
}

