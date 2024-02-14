﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy : Enemy
{

    private float speed = 1;            // how fast this enemy is moving
    private float bulletCooldown = 1;   // how many seconds need to pass until the next bullet is shot
    private float bulletSpeed = 3;      // how fast is the bullet going
    private float playerDistance = 150; // how much distance from the player until it stops moving
    
    private float time;
    private Player player;
    private float xPointToPlayer, yPointToPlayer;


    public ShootingEnemy(string fileName, int cols, int rows) : base("checkers.png", cols, rows)
    {
        scale = 1;
        SetOrigin(width / 2, height / 2);


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
        /**/

        // Total speed difference from speed
        //Console.WriteLine(Mathf.Abs(Mathf.Abs(xPointToPlayer) + Mathf.Abs(yPointToPlayer) - 1));


        


        // SHOOTING
        time += Time.deltaTime;
        

        if (time / 1000 >= bulletCooldown)
        {
            AddChild(new Bullet(bulletSpeed * xPointToPlayer, bulletSpeed * yPointToPlayer));
            time -= bulletCooldown * 1000;
        }


        // MOVEMENT
        // if enemy is close enough to player, stop moving
        if (DistanceTo(player) <= playerDistance) return;


        x += speed * xPointToPlayer;
        y += speed * yPointToPlayer;



    }





}