using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class Player : AnimationSprite
{
    private float bulletSpeed = 1.5f;        // The speed bullets will travel at
    private float reloadTime = .9f;     // Time in seconds until player can shoot again
    private float reloadTimeSmall = .2f;
    
    private float speed;
    private bool isAttacking;
    private int attackState;
    private float reloadCooldown;
    private int currentHealth;
    private float lastXPos, lastYPos;
    private float lastRotation;
    private float bulletXRotHelp, bulletYRotHelp;
    private List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    private Level level;

    private bool isDashing;
    private int dashSpeed;
    private int dashTimer;
    private int dashCooldown;
    private int dashDuration;
    private bool isDashingEnemy;
    

    public Player() : base("sprite_sub.png", 1, 1)
    {
        SetOrigin(width/2, height/2);

        scale = .5f;
        speed = 2f;
        currentHealth = 100;
        isDashing = false;
        dashDuration = 30;
        dashSpeed = 3;

    }
    
    void Update()
    {
        if (((MyGame)game).isPaused) return;
        Movement();
        Dashing();
        Attacking();
        collisionPlayer();
        Gameover();
    }

    void Movement()
    {
        SetColor(1f, 1f, 1f);

        rotation = 0;
        
        if (!isDashingEnemy)
        {
            if (Input.GetKey(Key.A)) Move(-speed, 0);   // LEFT
            if (Input.GetKey(Key.D)) Move(speed, 0);   // RIGHT
            if (Input.GetKey(Key.W)) Move(0, -speed);   // UP
            if (Input.GetKey(Key.S)) Move(0, speed);   // DOWN
        }


        // Rotationi

        rotation = (float)Mathf.Atan2((lastYPos - y), (lastXPos - x)) * 360 / (2 * Mathf.PI) + 90;
        //if (isDashingEnemy) { rotation += 180; Move(0, -speed * dashSpeed); }
        if (lastXPos == x && lastYPos == y) rotation = lastRotation;
        lastXPos = x;
        lastYPos = y;

        lastRotation = rotation;
    }

    public void Dashing()
    {
        dashCooldown++;
        if (Input.GetKeyDown(Key.SPACE) && isDashing == false && dashCooldown > 200) 
        { 
            isDashing = true;   
            dashCooldown = 0;
            SetColor(1f, 1f, .0f);
            dashTimer = 0;

        }
        if (isDashing)
        {
            Move(0, speed * dashSpeed);
            dashTimer++;
            if (dashTimer > dashDuration)
            {
                dashTimer = 0;
                isDashing = false;
                SetColor(1f, 1f, 1f);
            }
        }
        // Enemy bounch dash
        /*
        if (isDashingEnemy)
        {
            dashTimer++;
            if (dashTimer > dashDuration)
            {
                dashTimer = 0;
                isDashingEnemy = false;
                SetColor(1f, 1f, 1f);
            }
        }*/

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
        var a = (rotation+180) * Mathf.PI / 180.0;
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
                playerBullets.Add(new CoolPlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
                playerBullets.Last().SetXY(x, y);
                level.AddChild(playerBullets.Last());
                reloadCooldown += reloadTime * 1000;
                break;

        }       
    }


   

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

            if (col is Bullet || col is Enemy)
            {   /*
                if (col is Enemy && isDashing)
                {
                    isDashingEnemy = true; isDashing = false;
                    dashTimer = 0;
                    return;
                }*/
                col.Destroy();
                Console.WriteLine(col.name +" hit player");
                HealthUpdate(-1);
            }
        }
    }


    public void GetCardAbility(int cardNumber)
    {
        switch (cardNumber)
        {
            case 0:
                CardAttack();
                CardPassive();
                break;
            case 1:
                CardHealth();
                CardPassive();
                break;
            case 2:
                CardSpeed();
                CardPassive();
                break;
            case 3:
                CardDefense();
                CardPassive();
                break;
            case 4:
                CardPassive();
                break;
        }
        // add more if more cards are added

    }


    void CardAttack()
    {
        Console.WriteLine("CardAttack chosen");

    }

    void CardHealth()
    {
        Console.WriteLine("CardHealth chosen");

    }

    void CardSpeed()
    {
        Console.WriteLine("CardSpeed chosen");

    }

    void CardDefense()
    {
        Console.WriteLine("CardDefense chosen");

    }

    void CardPassive()
    {
        Console.WriteLine("CardPassive chosen");
        AddChild(new PassiveFish());
    }



}

