using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Bullet : Sprite
{
    float vx, vy;
    public Bullet(float pVx, float pVy) : base("circle.png")
    {
        scale = 0.4f;
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