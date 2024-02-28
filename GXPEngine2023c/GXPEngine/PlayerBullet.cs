using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

class PlayerBullet : AnimationSprite
{
    float vx, vy;
    Level level;
    public PlayerBullet(float pVx, float pVy, string filename = "circle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetOrigin(width/2, height/2);
        scale = 0.6f;
        vx = pVx;
        vy = pVy;
        level = game.FindObjectOfType<Level>();
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        x += vx;
        y += vy;
        // TODO: Check whether offscreen / hit test, and then remove!
        // Destroy if off screen
        if (x < -level.x || x > -level.x + game.width ||
            y < -level.y || y > -level.y + game.height)
        {
            Console.WriteLine("Enemy bullet despawned");
            LateDestroy();
        }
    }
}