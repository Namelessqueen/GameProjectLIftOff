﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerBullet : AnimationSprite
{
    float vx, vy;
    public PlayerBullet(float pVx, float pVy, string filename = "circle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetOrigin(width/2, height/2);
        scale = 0.6f;
        vx = pVx;
        vy = pVy;
    }

    void Update()
    {
        x += vx;
        y += vy;
        // TODO: Check whether offscreen / hit test, and then remove!
    }
}