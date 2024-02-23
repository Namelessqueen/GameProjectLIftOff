using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Bullet : Sprite
{
    float vx, vy;
    Player player;
    public Bullet(float pVx, float pVy, Player pPlayer) : base("circle.png")
    {
        scale = 0.4f;
        vx = pVx;
        vy = pVy;
        player = pPlayer;
    }

    void Update()
    {
        x += vx;
        y += vy;
        // TODO: Check whether offscreen / hit test, and then remove!

        if (Mathf.Abs(player.x - x) > game.width || Mathf.Abs(player.y - y) > game.height)
        {
            LateDestroy();
            Console.WriteLine("bullet despawned");
        }
    }
}