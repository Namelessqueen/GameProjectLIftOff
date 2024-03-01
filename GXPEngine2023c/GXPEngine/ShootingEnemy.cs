using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy : Enemy
{
    private float bulletCooldown    = 3;            // (seconds) how many seconds need to pass until the next bullet is shot
    private float bulletSpeed       = 10;            // (pixels) how fast is the bullet going
    private float playerDistance    = 300;          // (pixels) how much distance from the player until it stops moving
    private float slowedMultiplier  = .75f;         // (math, base = 1) how much the enemy is slowed by cool bullets

    public int damage;

    private List<Bullet> bullets = new List<Bullet>();
    private Player player;
    private float bulletTime;   // timer for bullets
    private float xPointToPlayer, yPointToPlayer;
    private float originalSpeed;
    private float bulletSize;
    private float speed;//             = 1;            // (pixels) how fast this enemy is moving

    public ShootingEnemy(string fileName = "sprite_rangedEnemy1.png", int cols = 1, int rows = 1,
                       int hp = 1,
                       int dmg = 1,
                       float spd = 1,
                       float projSize = 1
                       ) : base(fileName, cols, rows, hp, dmg)
    {
        scale = 1;
        SetOrigin(width / 2, height / 2);
        level = game.FindObjectOfType<Level>();
        bulletSize = projSize;
        speed = spd;
        damage = dmg;
        originalSpeed = speed;
    }


    protected override void Act()
    {
        
        if (player == null) player = game.FindObjectOfType<Player>();

        /**/
        // this version looks smoother
        xPointToPlayer = (player.x - x) / DistanceTo(player);
        yPointToPlayer = (player.y - y) / DistanceTo(player);

        // SHOOTING
        bulletTime += Time.deltaTime;
        

        if (bulletTime / 1000 >= bulletCooldown)
        {
            bullets.Add(new Bullet(bulletSpeed * xPointToPlayer, bulletSpeed * yPointToPlayer, level, damage));
            bullets.Last().SetXY(x, y);
            bullets.Last().SetScaleXY(bulletSize);
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