using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

class Enemy : AnimationSprite
{
    private float damageAnimTimer      =  150;     // (milliseconds) time of being red after taking damage
    private float poisonedTimer        = 1000;     // (milliseconds) time between ticks of poison damage
    private float slowStatusCooldown   = 3000;     // (milliseconds) time until slow status is removed
    private float poisonStatusCooldown = 2000;     // (milliseconds) time until poison status is removed
    private float passiveFishIFrames   =  200;     // (milliseconds) time before enemy takes damage from passive fish again

    private byte melee1AnimTime = 3;         // (frames) all of these are frames before next animation frame starts
    private byte melee2AnimTime = 10;
    private byte melee3AnimTime = 10;
    private byte melee4AnimTime = 10;
    private byte ranged1AnimTime = 10;
    private byte ranged2AnimTime = 10;

    private float channelVolume8  = .5f; // Torpedo Hit on Enemy.wav
    private float channelVolume9  = .5f; // TorpedoHitsoundFreeze.wav
    private float channelVolume10 = .8f; // TorpedoHitsoundPoison.wav
    private float channelVolume11 = .8f; // DeathOfMonster[i].wav
    private float channelVolume12 = .8f; // Universal Freeze sound.wav
    private float channelVolume13 = .8f; // Universal poison sound.wav

    public Level level;
    private Player player;
    private TextCanvas canvas;
    public string status;           // needs to be public because status affects speed and speed is type specific
    public int enemyType;
    private float UltDamage;
    private float damage;
    private float waveStatIncreaseAtk = 1.1f;
    private float waveStatIncreaseHp = 1.2f;

    private float health;
    private float damageTaken;
    private float damageAnimTime;
    private float poisonTime;
    private float fishTime;
    private bool fishHitAble;
    private string statusBulletHit;
    private float statusCooldown;
    private bool hasStatus;

    private float lastXPos, lastYPos;
    private float lastRotation;

    FMODSoundSystem soundSystem;

    public Enemy(string fileName = "square.png", int cols = 1, int rows = 1, int eHealth = 10, float dmg = 1) : base(fileName, cols, rows)
    {
        health = eHealth;
        SetOrigin(width / 2, height / 2);
        level = game.FindObjectOfType<Level>();
        canvas = game.FindObjectOfType<TextCanvas>();
        player = game.FindObjectOfType<Player>();   
        fishTime = passiveFishIFrames;
        soundSystem = new FMODSoundSystem();
        damage = dmg;
        health *= level.waveNumber * waveStatIncreaseHp;
        damage *= level.waveNumber * waveStatIncreaseAtk;
    }



    public void Update()
    {
        if (((MyGame)game).isPaused) return;
        StatusCheck();
        CollisionCheck();
        Act();
        Animation();
        rotate();
        DamageFunctions();
        DamageColoring();
    }

    void Animation()
    {
        switch (enemyType)
        {
            case 1: //Animate(melee1AnimTime);
                SetCycle(0, 12, melee1AnimTime);
                break;
            case 2:
                SetCycle(0, 8, melee2AnimTime);
                break;
            case 3:  
                SetCycle(0, 7, melee3AnimTime); 
                break;
            case 4: 
                SetCycle(0, 5, melee4AnimTime); 
                break;
            case 5:
                SetCycle(0, 7, ranged1AnimTime);
                break;
            case 6:
                SetCycle(0, 8, ranged2AnimTime); 
                break;
        }

        Animate();

    }

    public void UltDamagaPercent()
    {
        UltDamage = health * 0.5f;
        health = (int)UltDamage;
        //Console.WriteLine("ULT Percent");
    }
    public void UltDamagaKill()
    {
        health = 0;
        //Console.WriteLine("ULT Kill");
    }


    protected virtual void CollisionCheck()
    {
        if (!fishHitAble) fishTime -= Time.deltaTime;
        if (fishTime <= 0) fishHitAble = true;

        // could this be optimized in a way so it only checks collision for one specific class?
        GameObject[] collisions = GetCollisions();
        
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];
            if (col is Enemy || col is Bullet) continue;
            if (col is Player) player.HealthUpdate(-damage);
            if (col is PassiveFish)
            {
               
                if (!fishHitAble) continue;
                
                damageTaken += player.currentAttack/5;
                fishHitAble = false;
                fishTime = passiveFishIFrames;

                //Console.WriteLine(col.name + " hit an enemy");
            }
            else if (col is PlayerBullet)
            {
                damageTaken += player.currentAttack/5;
                //Console.WriteLine(col.name + " hit an enemy");
                col.LateDestroy();

                soundSystem.PlaySound(soundSystem.LoadSound("Torpedo Hit on Enemy.wav", false), 8, false, channelVolume8, 0);
            }
            if (col is CoolPlayerBullet || col is PlayerSecondarySlowed)
            {
                //Console.WriteLine("HIT COOL");
                statusBulletHit = "slowed";

                soundSystem.PlaySound(soundSystem.LoadSound("TorpedoHitsoundFreeze.wav", false), 9, false, channelVolume9, 0);
            }
            
            if (col is PoisonPlayerBullet || col is PlayerSecondaryPoison) 
            {
                //Console.WriteLine("HIT POISON");
                statusBulletHit = "poisoned";


                soundSystem.PlaySound(soundSystem.LoadSound("TorpedoHitsoundPoison.wav", false), 10, false, channelVolume10, 0);
            }
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
            level.SomethingDied(x, y);
            //parent.RemoveChild(this);
            parent.RemoveChild(this);
            canvas.XPUpdate(5); //XP added
            player.UltValue(3f); // Ult value
            //level.RemoveChild(this);  // AAAAAAAAAAAAAAAAAAAAAAAAAAAA
            Random random = new Random();



            soundSystem.PlaySound(soundSystem.LoadSound("DeathOfMonster"+random.Next(1,5)+".wav", false), 11, false, channelVolume11, 0);
            //new Sound("DeathOfMonster"+random.Next(1,5)+".wav", false, true).Play();
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


            soundSystem.PlaySound(soundSystem.LoadSound("Universal Freeze sound.wav", false), 12, false, channelVolume12, 0);
            //new Sound("Universal Freeze sound.wav", false, true).Play();
        }
        // Initial reactions for poison effect
        if (statusBulletHit == "poisoned")
        {
            poisonTime = 0;
            statusCooldown = poisonStatusCooldown;

            status = statusBulletHit;
            statusBulletHit = null;
            hasStatus = true;


            soundSystem.PlaySound(soundSystem.LoadSound("Universal poison sound.wav", false), 13, false, channelVolume13, 0);
            //new Sound("Universal poison sound.wav", false, true).Play();
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