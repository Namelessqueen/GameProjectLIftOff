using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerBullet : Sprite
{
    float vx, vy;
    public PlayerBullet(float pVx, float pVy) : base("circle.png")
    {
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