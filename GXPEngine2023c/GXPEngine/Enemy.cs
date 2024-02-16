using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Enemy : AnimationSprite
{
    private int health;
    private Level level;
    private string status;

    public Enemy(string fileName = "square.png", int cols = 1, int rows = 1, int eHealth = 1) : base(fileName, cols, rows)
    {
        health = eHealth;
        SetOrigin(width / 2, height / 2);
    }



    public void Update()
    {
        Act();
        collisionCheck();
        DeathCheck();
    }

    void collisionCheck()
    {
        
        
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];

            if (col is PlayerBullet)
            {
                health--;
                
                Console.WriteLine(col.name + " hit an enemy");
                col.LateDestroy();
            }
            if (col is CoolPlayerBullet)
            {
                status = "slowed";
            }
            
        }
    }

    void DeathCheck()
    {

        if (level == null) level = game.FindObjectOfType<Level>();


        if (health <= 0)
        {
            LateDestroy();
            level.RemoveChild(this);
        }
        



    }

    protected virtual void Act()
    {
        Console.WriteLine("Behavior.Act: not implemented");
        // Even better: make this an *abstract* method to force implementing it in subclasses!
    }
}