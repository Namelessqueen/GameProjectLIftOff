using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy : Enemy
{

    private float speed = 1;                       // how fast this enemy is moving
    private float bulletCooldown = 3;              // how many seconds need to pass until the next bullet is shot
    private float bulletSpeed = 2;                 // how fast is the bullet going
    private float playerDistance = 150;            // how much distance from the player until it stops moving

    private float slowedMultiplier = .75f;         // how much the enemy is slowed by cool bullets
    
    private float time;
    private Player player;
    private float xPointToPlayer, yPointToPlayer;
    private bool hasStatus;
    private float statusCooldown;
    private float originalSpeed;
    


    public ShootingEnemy(string fileName = "checkers.png", int cols = 1, int rows = 1) : base(fileName, cols, rows)
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
        /**/

        // Total speed difference from speed
        //Console.WriteLine(Mathf.Abs(Mathf.Abs(xPointToPlayer) + Mathf.Abs(yPointToPlayer) - 1));


        // SHOOTING
        time += Time.deltaTime;
        

        if (time / 1000 >= bulletCooldown)
        {
            AddChild(new Bullet(bulletSpeed * xPointToPlayer, bulletSpeed * yPointToPlayer, player));
            time -= bulletCooldown * 1000;
        }


        // MOVEMENT
        // if enemy is close enough to player, stop moving
        if (DistanceTo(player) <= playerDistance) return;


        x += speed * xPointToPlayer;
        y += speed * yPointToPlayer;



    }


    protected override void StatusCheck()
    {

        if (hasStatus)
        {

            statusCooldown -= Time.deltaTime;

            if (statusCooldown <= 0)
            {

                SetColor(1, 1, 1);

                speed = originalSpeed;


                hasStatus = false;
                status = null;
            }
            
            return;
        }


        if (status == "slowed")
        {

            // NOT PRETTY, DOESN'T WORK WITH FINAL SPRITES
            /*
            Sprite spr1 = new Sprite("square.png");
            //spr1.SetOrigin(spr1.width / 2, spr1.height / 2);
            spr1.blendMode = BlendMode.LIGHTING;
            //spr1.SetXY(x, y);
            AddChild(spr1);
            */

            /*
            EasyDraw easyDraw = new EasyDraw(width, height, false);
            
            //easyDraw.Fill(Color.Red); //
            easyDraw.SetOrigin(width/2, height/2);
            easyDraw.Rect(100, 0, width, height);
            //easyDraw.Clear(ColorTranslator.FromHtml("#5552bae2"));
            //easyDraw.blendMode = BlendMode.LIGHTING;  //
            AddChild(easyDraw);
            //blendMode = BlendMode.MULTIPLY;   //
            */

            SetColor(.1f, .5f, .9f);

            speed *= slowedMultiplier;
            statusCooldown = 3000;
            hasStatus = true;

        }
        
        
        
    }

    protected override void CollisionCheck()
    {
        
        base.CollisionCheck();

        Console.WriteLine("status: "+status);





    }


}