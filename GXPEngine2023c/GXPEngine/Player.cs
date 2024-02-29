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
    private float reloadTime = 0.3f;     // Time in seconds until player can shoot again
    private float reloadTimeSmall = .2f;
    
    private int baseMaxHealth = 100;    // max hp at start
    private int baseAttack = 5;         // attack at start
    private float speed = 2f;   // own speed

    private int playerMinDistanceFromBorder = 35;  // this is about the level itself, not the camera
    private float iFrameDuration = 500;
    private float iFrameCooldown;

    private float cardHpIncrease = 1.2f;
    private float cardAtkIncrease = 1.2f;
    private float cardAtkSpdIncrease = 1.2f;

    private string primaryType = "normal";
    private string secondaryType = "normal";

    private bool isAttacking;
    private int attackState;
    private float reloadCooldown;
    private float currentAttack;
    private float currentHealth;
    private int HealthCoolDown;
    private float currentFuel = 510;
    private int FuelCooldown;
    private float currentUlt;

    private int maxHealth;


    private float lastXPos, lastYPos;
    private float lastRotation;
    private float bulletXRotHelp, bulletYRotHelp;
    private List<PlayerBullet> playerBullets = new List<PlayerBullet>();
    private List<PlayerSecondary> PlayerSecondarys = new List<PlayerSecondary>();
    private List<Enemy> AllEnemys;
    private Enemy[] foundEnemies;
    private Level level;
    private int lengthFoundEnemies = 1;

    private bool isDashing = false;
    private int dashSpeed = 3;
    private int dashTimer;
    private int dashCooldown;
    private int dashDuration = 30;

    private int sliderInput;

    

    public Player() : base("sprite_player.png", 8, 3, 21)
    {
        SetOrigin(width/2, height/2);
        scale = .5f;
        //speed = 2f;
        maxHealth = baseMaxHealth;
        currentHealth = maxHealth;
        currentAttack = baseAttack;
        isDashing = false;
        //iFrameCooldown = iFrameDuration;
        //dashDuration = 30;
        //dashSpeed = 3;

        //currentFuel = 510;
        AllEnemys = new List<Enemy>();

        //Enemy[] foundEnemies = game.FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;

        Movement();
        Dashing();
        Attacking();
        IFrameFunctions();
        collisionPlayer();
        Gameover();
        DataVoid();
        Ultimate(); UltValue(0.005f);



    }

    void IFrameFunctions()
    {

        if (iFrameCooldown > 0)
        {
            iFrameCooldown -= Time.deltaTime;

            if (iFrameCooldown / iFrameDuration > .75f) alpha = .5f;
            else if (iFrameCooldown / iFrameDuration > .5f) alpha = 1;
            else if (iFrameCooldown / iFrameDuration > .25f) alpha = .5f;
            else alpha = 1;

        }


    }

    void Movement()
    {
        SetColor(1f, 1f, 1f);

        rotation = 0;

            if (Input.GetKey(Key.A) || Input.GetKeyDown(Key.A)) Move(-speed, 0);   // LEFT
            if (Input.GetKey(Key.D) || Input.GetKeyDown(Key.D)) Move(speed, 0);   // RIGHT
            if (Input.GetKey(Key.W) || Input.GetKeyDown(Key.W)) Move(0, -speed);   // UP
            if (Input.GetKey(Key.S) || Input.GetKeyDown(Key.S)) Move(0, speed);   // DOWN


        // Rotationi

        rotation = (float)Mathf.Atan2((lastYPos - y), (lastXPos - x)) * 360 / (2 * Mathf.PI) + 90;
        rotation = (float)Mathf.Atan2((lastYPos - y), (lastXPos - x)) * 360 / (2 * Mathf.PI) + 90;
        if (lastXPos == x && lastYPos == y) rotation = lastRotation;
        lastXPos = x;
        lastYPos = y;

        lastRotation = rotation;

        x = Mathf.Clamp(x, 0 + playerMinDistanceFromBorder, 5508 - playerMinDistanceFromBorder);
        y = Mathf.Clamp(y, 0 + playerMinDistanceFromBorder, 3072 - playerMinDistanceFromBorder);

        // 5508, 3072

        //slider input
        sliderInput = (int)Mathf.Clamp(sliderInput, 0, 100);
        if (Input.GetKey(Key.UP)) sliderInput++;
        if (Input.GetKey(Key.DOWN)) sliderInput--;
    }

    public void Dashing()
    {
        dashCooldown++;
        if (Input.GetKeyDown(Key.O) && isDashing == false && dashCooldown > 50) 
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
    }

    void Attacking()
    {
        if (level == null) level = game.FindObjectOfType<Level>();
        /*// Changing weapons 
        if (Input.GetKeyDown(Key.B)) primaryType = "normal";
        if (Input.GetKeyDown(Key.N)) primaryType = "slow";
        if (Input.GetKeyDown(Key.M)) primaryType = "poison";
        */

        // helping the bullets getting the right rotation
        var a = (rotation + 180) * Mathf.PI / 180.0;
        float cosa = (float)Math.Cos(a);
        float sina = (float)Math.Sin(a);

        bulletXRotHelp = (0 * cosa - -5 * sina);
        bulletYRotHelp = (0 * sina + -5 * cosa);



        // Pressing J makes you attack
        if (Input.GetKey(Key.J) || Input.GetKeyDown(Key.J)) isAttacking = true;
      
        else isAttacking = false;

      

        if (reloadCooldown/1000 > 0)
        {
            reloadCooldown -= Time.deltaTime;
            return;
        }
        

        if ((Input.GetKey(Key.H) || Input.GetKeyDown(Key.H)) && fuelUpdate() > 1)
        {
            switch (secondaryType)
            {
                case "normal":
                    PlayerSecondarys.Add(new PlayerSecondary((speed / 5) * bulletXRotHelp, (speed / 5) * bulletYRotHelp, sliderInput));
                    break;

                case "slow":
                    PlayerSecondarys.Add(new PlayerSecondary((speed / 5) * bulletXRotHelp, (speed / 5) * bulletYRotHelp, sliderInput, "SecondarySlowed.png"));
                    break;

                case "poison":
                    PlayerSecondarys.Add(new PlayerSecondary((speed / 5) * bulletXRotHelp, (speed / 5) * bulletYRotHelp, sliderInput, "SecondaryPoison.png"));
                    break;

            }

            PlayerSecondarys.Last().SetXY(x + (9 * bulletXRotHelp), y + (9 * bulletYRotHelp));
            level.AddChild(PlayerSecondarys.Last());
            reloadCooldown += reloadTime * 250;

      
            currentFuel = currentFuel - ((float)sliderInput / 100) * 10 ;
            FuelCooldown = 0;
        }


        if (!isAttacking) return;



        
        switch (primaryType)
        {
            case "normal": 
                playerBullets.Add(new PlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
                break;

            case "slow": 
                playerBullets.Add(new CoolPlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
                break;

            case "poison":
                playerBullets.Add(new PoisonPlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
                break;

        }
        playerBullets.Last().SetXY(x, y);
        playerBullets.Last().rotation = rotation + 180;
        level.AddChild(playerBullets.Last());
        reloadCooldown += reloadTime * 1000;
    }

    //Ultability
    public int AmmountEnemy()
    {
        return lengthFoundEnemies;
    }

    public int InputSlider()
    {
        return sliderInput;
    }
    void Ultimate()
    {
        
        Enemy[] foundEnemies = game.FindObjectsOfType<Enemy>();
        if (foundEnemies == null) return;
        //Console.WriteLine(foundEnemies.Length);
        lengthFoundEnemies = foundEnemies.Length;
        for (int i = 0; i < foundEnemies.Length; i++)
        {
            if ((Input.GetKeyDown(Key.U) && UltValue(0) >= 100) || Input.GetKeyDown(Key.Q))
            {   
                if (i >= foundEnemies.Length/2) foundEnemies[i].UltDamagaPercent();
                if (i < foundEnemies.Length/2) foundEnemies[i].UltDamagaKill();
                currentUlt = 0;

            }
        }
        
    }
    


   //STATS UPDATES
   
    public float UltValue(float pChange)
    {
          float Change = pChange;
          currentUlt += Change;

          return currentUlt;
    }
    public float HealthUpdate(float pChange)
    {
        if (pChange != 0 && iFrameCooldown <= 0)
        {
            float Change = pChange;
            currentHealth += Change;

            iFrameCooldown = iFrameDuration;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        return currentHealth;
        
    }

    void DataVoid()
    {
        if (((MyGame)game).isPaused) return;
        HealthCoolDown++;
        if (HealthCoolDown > 300)
        {
            currentHealth += 0.1f;
        }
        HealthCoolDown++;
        if (FuelCooldown > 300)
        {
            currentFuel++;
            currentFuel *= 1.01f;
        }
    }

    public float fuelUpdate()
    {
       
        currentFuel = Mathf.Clamp(currentFuel, 0, 509);
        FuelCooldown++;
       
        return currentFuel;
    }

    void Gameover()
    {
        if (currentHealth<1)
        {
            //Destroy();
            level.SomethingDied(x, y);
        }
    }
    void collisionPlayer()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];

            if (col is Bullet)
            {  
                col.Destroy();
                Console.WriteLine(col.name + " hit player");
                HealthUpdate(-5);
                HealthCoolDown = 0;
                
            }
            if (col is Enemy)
            {
                Console.WriteLine(col.name + " hit player");
                HealthUpdate(-10);
                HealthCoolDown = 0;
            }
        }
    }


    public void GetCardAbility(int cardNumber)
    {
        CardPassiveTurret();
        switch (cardNumber)
        {
            case 0:
                CardAttack();
                break;
            case 1:
                CardAtkSpeed();
                break;
            case 2:
                CardHealth();
                break;
            case 3:
                CardPassiveFish();
                break;
            case 4:
                CardPassiveTurret();
                break;
            case 5:
                primaryType = "normal";
                break;
            case 6:
                primaryType = "slow";
                break;
            case 7:
                primaryType = "poison";
                break;
            case 8:
                secondaryType = "normal";
                break;
            case 9:
                secondaryType = "slow";
                break;
            case 10:
                secondaryType = "poison";
                break;
        }
        // add more if more cards are added

    }


    void CardAttack()
    {
        Console.WriteLine("CardAttack chosen");
        float newAttack = currentAttack * cardAtkIncrease;
        currentAttack = (int)newAttack;

    }

    void CardHealth()
    {
        Console.WriteLine("CardHealth chosen");
        float newHealth = maxHealth * cardHpIncrease;
        maxHealth = (int)newHealth;
    }

    void CardAtkSpeed()
    {
        Console.WriteLine("CardAtkSpeed chosen");
        float newAtkSpd = reloadCooldown / cardAtkSpdIncrease;
        reloadCooldown = newAtkSpd;

    }

    void CardPassiveFish()
    {
        Console.WriteLine("CardPassiveFish chosen");
        AddChild(new PassiveFish());
    }

    void CardPassiveTurret()
    {
        Console.WriteLine("CardPassiveTurret chosen");
        AddChild(new PassiveTurret());
    }



}

