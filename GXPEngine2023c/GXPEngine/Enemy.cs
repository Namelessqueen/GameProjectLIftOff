using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Enemy : AnimationSprite
{


    public Enemy(string fileName, int cols, int rows) : base(fileName, cols, rows)
    {

        SetOrigin(width / 2, height / 2);
    }



    public void Update()
    {
        Act();
        collisionCheck();
    }

    void collisionCheck()
    {
        GameObject[] collisions = GetCollisions();
        for (int i = 0; i < collisions.Length; i++)
        {
            GameObject col = collisions[i];

            if (col is PlayerBullet)
            {
                Console.WriteLine(col.name + " killed an enemy");
                col.LateDestroy();
                LateDestroy();
                
            }
            /*
            else if (col is PowerUp)
            {
            }*/
        }
    }


    protected virtual void Act()
    {
        Console.WriteLine("Behavior.Act: not implemented");
        // Even better: make this an *abstract* method to force implementing it in subclasses!
    }
}