using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Bullet : Sprite
{
    float vx, vy;
    Level level;

    public Bullet(float pVx, float pVy, Level lLevel) : base("circle.png")
    {
        scale = 0.4f;
        vx = pVx;
        vy = pVy;

        level = lLevel;
        SetOrigin(width/2, height/2);
    }

    void Update()
    {
        x += vx;
        y += vy;
        
        // Destroy if off screen
        if (x < -level.x || x > -level.x + game.width || 
            y < -level.y || y > -level.y + game.height)
        {
            Console.WriteLine("Bullet despawned");
            LateDestroy();
        }

    }
}