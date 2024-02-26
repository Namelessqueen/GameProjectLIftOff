using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CoolPlayerBullet : PlayerBullet
{
    float vx, vy;
    public CoolPlayerBullet(float pVx, float pVy) : base(pVx, pVy, "placeholderCoolBullet.png")
    {
        scale = 0.6f;
        vx = pVx;
        vy = pVy;
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        x += vx;
        y += vy;
        // TODO: Check whether offscreen / hit test, and then remove!
    }
}