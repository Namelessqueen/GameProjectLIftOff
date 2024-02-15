using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using System.IO.Ports;

class Player : AnimationSprite
{
    private float speed;
    private bool isMoving;
    private int health;
    public Player() : base("barry.png", 7, 1)
    {
        speed = 2f;
        health = 3;
    }

    void Update()
    {
        HealthUpdate();
        Movement();
        Animation();
        //Console.WriteLine(health);
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

    void HealthUpdate()
    {
        if (Input.GetKeyDown(Key.SPACE) && health > 0)
        {
            health--;
        }
        if (health == 0)
        {
            this.Destroy();
        }
    }

}

