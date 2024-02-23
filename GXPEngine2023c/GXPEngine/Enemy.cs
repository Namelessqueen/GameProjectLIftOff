using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Enemy : AnimationSprite
{
    
    public string status;
    public int health;

    private Level level;
    private bool takingDamage;

    public Enemy(string fileName = "square.png", int cols = 1, int rows = 1, int eHealth = 3) : base(fileName, cols, rows)
    {
        health = eHealth;
        SetOrigin(width / 2, height / 2);
    }



    public void Update()
    {
        StatusCheck();
        CollisionCheck();
        Act();
        DamageFunctions();
    }

    protected virtual void CollisionCheck()
    {
        

        GameObject[] collisions = GetCollisions();
        
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];
            if (col is Enemy || col is Bullet) continue;

            if (col is PlayerBullet)
            {
                health--;
                takingDamage = true;
                SetColor(.9f, .3f, .3f);

                Console.WriteLine(col.name + " hit an enemy");
                col.LateDestroy();
            }
            if (col is CoolPlayerBullet)
            {
                // status = "slowed";       // the original code, the code below is placeholder
                status = "poisoned";
            }

        }
    }


    protected virtual void StatusCheck()
    {


        Console.WriteLine("Enemy.StatusCheck: not implemented");



    }


    void DamageFunctions()
    {

        if (level == null) level = game.FindObjectOfType<Level>();


        if (health <= 0)
        {
            LateDestroy();
            parent.RemoveChild(this);
            //level.RemoveChild(this);
        }
        



    }

    protected virtual void Act()
    {
        Console.WriteLine("Enemy.Act: not implemented");
        // Even better: make this an *abstract* method to force implementing it in subclasses!
    }
}