using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

class Player : AnimationSprite
{
    private float speed;
    private bool isMoving;
    private int currentHealth;
    public Player() : base("barry.png", 7, 1)
    {
        speed = 2f;
        currentHealth = 3;
    }

    void Update()
    {
        //Console.WriteLine(HealthUpdate(0).ToString());
        Movement();
        Animation();
        collisionPlayer();
        Gameover();
    }

    void Movement()
    {
        if (Input.GetKey(Key.LEFT))
        {
            Move(-speed, 0);
            _mirrorX = true;
        }
        if (Input.GetKey(Key.RIGHT))
        {
            Move(speed, 0);
            _mirrorX = false;
        }
        if (Input.GetKey(Key.UP))
        {
            Move(0, -speed);
        }
        if (Input.GetKey(Key.DOWN))
        {
            Move(0, speed);
        }
        if (Input.AnyKey())
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    void Animation()
    {
        if (isMoving)
        { SetCycle(0, 3, 30); }
        else
        { SetCycle(4, 3, 60); }
        Animate();
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

            if (col is Enemy || col is Bullet)
            {
                col.Destroy();
                Console.WriteLine(col.name +" hit player");
                HealthUpdate(-1);
            }
            /*
            else if (col is PowerUp)
            {
            }*/
        }
    }
  }

