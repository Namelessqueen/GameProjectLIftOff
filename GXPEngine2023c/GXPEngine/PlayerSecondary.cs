using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PlayerSecondary : AnimationSprite
{
    float vx, vy;
    public PlayerSecondary(float pVx, float pVy, string filename = "triangle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetOrigin(width / 2, 0);
        scale = 2f;
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