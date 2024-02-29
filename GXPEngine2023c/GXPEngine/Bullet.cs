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
    public int damage;

    public Bullet(float pVx, float pVy, Level lLevel, int dmg) : base("sprite_enemyProjectile.png")
    {
        SetOrigin(width/2, height/2);

        vx = pVx;
        vy = pVy;
        level = lLevel;
        damage = dmg;
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        x += vx;
        y += vy;
        rotation += 5;
        
        // Destroy if off screen
        if (x < -level.x || x > -level.x + game.width || 
            y < -level.y || y > -level.y + game.height)
        {
            //Console.WriteLine("Enemy bullet despawned");
            LateDestroy();
        }

    }
}