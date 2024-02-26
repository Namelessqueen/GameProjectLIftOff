using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Enemy : AnimationSprite
{
    private float damageAnimTimer      =  150;     // (milliseconds) time of being red after taking damage
    private float poisonedTimer        = 1000;     // (milliseconds) time between ticks of poison damage
    private float slowStatusCooldown   = 3000;     // (milliseconds) time until slow status is removed
    private float poisonStatusCooldown = 2000;     // (milliseconds) time until poison status is removed

    public Level level;
    public string status;           // needs to be public because status affects speed and speed is type specific

    private int health;
    private int damageTaken;
    private float damageAnimTime;
    private float poisonTime;
    private string statusBulletHit;
    private float statusCooldown;
    private bool hasStatus;

    private float lastXPos, lastYPos;
    private float lastRotation;
    public Enemy(string fileName = "square.png", int cols = 1, int rows = 1, int eHealth = 10) : base(fileName, cols, rows)
    {
        health = eHealth;
        SetOrigin(width / 2, height / 2);
        level = game.FindObjectOfType<Level>();
    }



    public void Update()
    {
        StatusCheck();
        CollisionCheck();
        Act();
        rotate();
        DamageFunctions();
        DamageColoring();
    }

    protected virtual void CollisionCheck()
    {
        
        // could this be optimized in a way so it only checks collision for one specific class?
        GameObject[] collisions = GetCollisions();
        
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];
            if (col is Enemy || col is Bullet) continue;

            if (col is PlayerBullet)
            {
                damageTaken++;

                Console.WriteLine(col.name + " hit an enemy");
                col.LateDestroy();
            }
            if (col is CoolPlayerBullet)
            {
                statusBulletHit = "slowed";
            }
            /*
            if (col is PoisonPlayerBullet)      // this bullet doesn't exist yet, but the functionality exists
            {
                statusBulletHit = "poisoned";
            }
            */

        }

    }


    protected virtual void StatusCheck()
    {
        // slowed is defined in own class
        
        if (status == "poisoned")
        {
            poisonTime += Time.deltaTime;
            if (poisonTime >= poisonedTimer)
            {
                poisonTime -= poisonedTimer;
                damageTaken++;
            }

        }

    }


    protected virtual void ReturnToNormal()
    {
        Console.WriteLine("Enemy.ReturnToNormal: not implemented");
    }


    protected virtual void DamageColoring()
    {
        SetColor(1, 1, 1);    // setting color to normal, every frame the code below will change color if needed

        if (damageTaken > 0)
        {
            health -= damageTaken;              // hehe this is where the actual health is decreased
            damageAnimTime = damageAnimTimer;
            damageTaken = 0;
        }

        if (damageAnimTime > 0)
        {
            damageAnimTime -= Time.deltaTime;
            SetColor(.9f, .3f, .3f);
            return;
        }

        //              color in decimals:  R    G    B
        if (status == "slowed")   SetColor(.1f, .5f, .9f);
        if (status == "poisoned") SetColor(.2f, .8f, .2f);

    }


    void DamageFunctions()
    {

        if (level == null) level = game.FindObjectOfType<Level>();

        // This is the place where enemies are killed
        if (health <= 0)
        {
            LateDestroy();
            parent.RemoveChild(this);
            //level.RemoveChild(this);  // AAAAAAAAAAAAAAAAAAAAAAAAAAAA
        }


        if (hasStatus)
        {
            // Measuring when status time is up
            statusCooldown -= Time.deltaTime;
            if (statusCooldown <= 0)
            {
                ReturnToNormal();

                hasStatus = false;
                status = null;
            } 
        }

        if (statusBulletHit == null) return;    // code below is only interesting if enemy got hit by status bullet

        // Initial reactions for slow effect
        if (statusBulletHit == "slowed")
        {
            statusCooldown = slowStatusCooldown;

            status = statusBulletHit;
            statusBulletHit = null;
            hasStatus = true;
        }
        // Initial reactions for poison effect
        if (statusBulletHit == "poisoned")
        {
            poisonTime = 0;
            statusCooldown = poisonStatusCooldown;

            status = statusBulletHit;
            statusBulletHit = null;
            hasStatus = true;
        }

    }

    void rotate()
    {
        rotation = (float)Mathf.Atan2((lastYPos - y), (lastXPos - x)) * 360 / (2 * Mathf.PI) + 90;
        if (lastXPos == x && lastYPos == y) rotation = lastRotation;

        lastXPos = x;
        lastYPos = y;
        lastRotation = rotation;

    }


    protected virtual void Act()
    {
        Console.WriteLine("Enemy.Act: not implemented");
        // Even better: make this an *abstract* method to force implementing it in subclasses!
    }
}