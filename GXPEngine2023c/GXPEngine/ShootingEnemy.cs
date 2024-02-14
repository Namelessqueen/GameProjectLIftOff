using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy : Enemy
{

    private float speed = 1;
    private Sprite target;
    private float bulletCooldown = 1f;
    private float bulletSpeed = 3;
    private float time;
    private float xPointToPlayer, yPointToPlayer;


    public ShootingEnemy(string fileName, int cols, int rows) : base("checkers.png", cols, rows)
    {
        scale = 1;
        SetOrigin(width / 2, height / 2);


    }


    protected override void Act()
    {

        if (target == null) target = game.FindObjectOfType<Player>();

        /**/
        // this version looks smoother
        xPointToPlayer = (target.x - x) / DistanceTo(target);
        yPointToPlayer = (target.y - y) / DistanceTo(target);

        /*
        // this version uses the pythagoran theorem but looks worse
        xPointToPlayer = Mathf.Pow((target.x - x), 2) / Mathf.Pow(DistanceTo(target), 2);
        yPointToPlayer = Mathf.Pow((target.y - y), 2) / Mathf.Pow(DistanceTo(target), 2);
        
        if (target.x < x) xPointToPlayer *= -1;
        if (target.y < y) yPointToPlayer *= -1;
        /**/

        Console.WriteLine(Mathf.Abs(Mathf.Abs(xPointToPlayer) + Mathf.Abs(yPointToPlayer) - 1));





        // SHOOTING
        time += Time.deltaTime;
        //Console.WriteLine(time);

        if (time / 1000 >= bulletCooldown)
        {
            AddChild(new Bullet(bulletSpeed * xPointToPlayer, bulletSpeed * yPointToPlayer));
            time -= bulletCooldown * 1000;
        }


        // MOVEMENT
        // if enemy is close enough to player, stop moving
        if (DistanceTo(target) <= 150) return;


        x += speed * xPointToPlayer;
        y += speed * yPointToPlayer;



    }





}