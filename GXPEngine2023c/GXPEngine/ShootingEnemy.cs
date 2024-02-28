using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy : Enemy
{
    private float speed             = 1;            // (pixels) how fast this enemy is moving
    private float bulletCooldown    = 3;            // (seconds) how many seconds need to pass until the next bullet is shot
    private float bulletSpeed       = 5;            // (pixels) how fast is the bullet going
    private float playerDistance    = 150;          // (pixels) how much distance from the player until it stops moving
    private float slowedMultiplier  = .75f;         // (math, base = 1) how much the enemy is slowed by cool bullets

    private List<Bullet> bullets = new List<Bullet>();
    private Player player;
    private float bulletTime;   // timer for bullets
    private float xPointToPlayer, yPointToPlayer;
    private float originalSpeed;

    public ShootingEnemy(string fileName = "sprite_enemy1.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
    {
        scale = 1;
        SetOrigin(width / 2, height / 2);

        originalSpeed = speed;
    }


    protected override void Act()
    {
        
        if (player == null) player = game.FindObjectOfType<Player>();

        /**/
        // this version looks smoother
        xPointToPlayer = (player.x - x) / DistanceTo(player);
        yPointToPlayer = (player.y - y) / DistanceTo(player);

        /*
        // this version uses the pythagoran theorem but looks worse
        xPointToPlayer = Mathf.Pow((target.x - x), 2) / Mathf.Pow(DistanceTo(target), 2);
        yPointToPlayer = Mathf.Pow((target.y - y), 2) / Mathf.Pow(DistanceTo(target), 2);
        
        if (target.x < x) xPointToPlayer *= -1;
        if (target.y < y) yPointToPlayer *= -1;

        // Total speed difference from speed
        Console.WriteLine(Mathf.Abs(Mathf.Abs(xPointToPlayer) + Mathf.Abs(yPointToPlayer) - 1));
        */


        // SHOOTING
        bulletTime += Time.deltaTime;
        

        if (bulletTime / 1000 >= bulletCooldown)
        {
            bullets.Add(new Bullet(bulletSpeed * xPointToPlayer, bulletSpeed * yPointToPlayer, level));
            bullets.Last().SetXY(x, y);
            level.AddChild(bullets.Last());
            bulletTime -= bulletCooldown * 1000;
        }


        // MOVEMENT
        // if enemy is close enough to player, stop moving
        if (DistanceTo(player) <= playerDistance) return;


        x += speed * xPointToPlayer;
        y += speed * yPointToPlayer;



    }


    protected override void StatusCheck()
    {
        base.StatusCheck();

        if (status == "slowed")
        {
            speed = originalSpeed * slowedMultiplier;
        }

    }
    

    protected override void ReturnToNormal()
    {
        //base.ReturnToNormal();

        speed = originalSpeed;
    }

    protected override void CollisionCheck()
    {
        
        base.CollisionCheck();

        //Console.WriteLine("status: "+status);

    }


}