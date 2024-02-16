using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

class Player : AnimationSprite
{
    private int bulletSpeed = 3;        // The speed bullets will travel at
    private float reloadTime = .5f;     // Time in seconds until player can shoot again
    private float reloadTimeSmall = .2f;
    
    private float speed;
    private bool isMoving;
    private bool isAttacking;
    private int attackState;
    private float reloadCooldown;
    private int currentHealth;
    private float lastXPos, lastYPos;
    private float lastRotation;
    private float bulletXRotHelp, bulletYRotHelp;
    private List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    private Level level;

    public Player() : base("sprite_sub.png", 1, 1)
    {
        SetOrigin(width/2, height/2);
        scale = .5f;
        speed = 2f;
        currentHealth = 3;

    }
    
    void Update()
    {
        Movement();
        Attacking();
        //Animation();
        collisionPlayer();
        Gameover();
    }

    void Movement()
    {
        // Moving
        rotation = 0;
        if (Input.GetKey(Key.A)) Move(-speed, 0);   // LEFT
        if (Input.GetKey(Key.D)) Move( speed, 0);   // RIGHT
        if (Input.GetKey(Key.W)) Move(0, -speed);   // UP
        if (Input.GetKey(Key.S)) Move(0,  speed);   // DOWN
        
        /*if (Input.AnyKey()) isMoving = true;
        else                isMoving = false;*/

        // Rotation
        rotation = (float)Mathf.Atan2((lastYPos - y), (lastXPos - x))*360/(2*Mathf.PI)-90;
        if (lastXPos == x && lastYPos == y) rotation = lastRotation;

        lastXPos = x;
        lastYPos = y;
        lastRotation = rotation;

    }

    void Attacking()
    {
        if (level == null) level = game.FindObjectOfType<Level>();
        // Changing weapons 
        if (Input.GetKeyDown(Key.T))
        {
            attackState++;
            attackState %= 2;
        }

        // Pressing J makes you attack
        if (Input.GetKey(Key.J)) isAttacking = true;
        else isAttacking = false;

        if (reloadCooldown/1000 > 0)
        {
            reloadCooldown -= Time.deltaTime;
            return;
        }

        if (!isAttacking) return;


        // helping the bullets getting the right rotation
        var a = rotation * Mathf.PI / 180.0;
        float cosa = (float)Math.Cos(a);
        float sina = (float)Math.Sin(a);

        bulletXRotHelp = (0 * cosa - -5 * sina);
        bulletYRotHelp = (0 * sina + -5 * cosa);

        switch (attackState)
        {
            case 0: // Small bullets
                playerBullets.Add(new PlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
                playerBullets.Last().SetXY(x, y);
                playerBullets.Last().scale = .3f;
                level.AddChild(playerBullets.Last());
                reloadCooldown += reloadTimeSmall * 1000;
                break;

            case 1: // Bigger bullets
                playerBullets.Add(new PlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
                playerBullets.Last().SetXY(x, y);
                level.AddChild(playerBullets.Last());
                reloadCooldown += reloadTime * 1000;
                break;

        }       
    }


    /*void Animation()
    {
        if (isMoving)
        { SetCycle(0, 3, 30); }
        else
        { SetCycle(4, 3, 60); }
        Animate();
    }*/

    public int HealthUpdate(int pHealthChange)
    {
        int healthChange = pHealthChange;

        currentHealth = currentHealth + healthChange;
        return currentHealth;
    }

    void Gameover()
    {
        if (currentHealth<1)
        {
            //Destroy();
        }
    }
    void collisionPlayer()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];

            if (col is Enemy || col is Bullet)
            {
                col.Destroy();
                Console.WriteLine(col.name +" hit player");
                HealthUpdate(-1);
            }
        }
    }
  }

