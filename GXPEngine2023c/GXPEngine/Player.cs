using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

class Player : AnimationSprite
{
    private float speed;
    private bool isMoving;
    public Player() : base("barry.png", 7, 1)
    {
        speed = 2f;
    }

    void Update()
    {
        Movement();
        Animation();
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

}

