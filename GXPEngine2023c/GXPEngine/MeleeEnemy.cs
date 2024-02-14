using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MeleeEnemy : Enemy
{

    private float speed = 1.5f;     // how fast this enemy is moving

    private Player player;
    private float xPointToPlayer, yPointToPlayer;


    public MeleeEnemy(string fileName, int cols, int rows) : base("colors.png", cols, rows)
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


        // MOVEMENT
        x += speed * xPointToPlayer;
        y += speed * yPointToPlayer;



    }





}