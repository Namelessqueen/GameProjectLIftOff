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
        //level = lLevel;
        damage = dmg;
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        x += vx;
        y += vy;
        if (level == null) level = game.FindObjectOfType<Level>();
        if (x < -level.x || x > -level.x + game.width ||
            y < -level.y || y > -level.y + game.height)
        {
            LateDestroy();
        }
        // TODO: Check whether offscreen / hit test, and then remove!
    }
}