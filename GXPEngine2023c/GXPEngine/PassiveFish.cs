using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PassiveFish : PlayerBullet 
{
    // DO NOT CHANGE ANY VALUES HERE, THESE ARE IMPORTANT
    float xPullBackForce = .5f;
    float yPullBackForce = .5f;
    float maxSpeed = 17;
    float xSpeed = -17;
    float ySpeed = 0;

    //private int rotationFrameCount;

    public PassiveFish(string filename = "sprite_passiveFish.png", int cols = 8, int rows = 1) : base(0, 0, filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        //alpha = .3f;
        x -= 15;
        y -= 15;
        y -= 300;
        scale = 1.5f;
        rotation = 270;
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        xSpeed += xPullBackForce;
        ySpeed += yPullBackForce;
        x += xSpeed;
        y += ySpeed;

        if (xSpeed <= -maxSpeed || xSpeed >= maxSpeed) xPullBackForce *= -1;
        if (ySpeed <= -maxSpeed || ySpeed >= maxSpeed) yPullBackForce *= -1;

        rotation -= (360f / 136f);

        Animate(.1f);
        /* // calculating total amount of frames it takes to rotate, which is 136 frames
        rotationFrameCount++;
        if (xSpeed == 0)
        {
            Console.WriteLine("rotationFrameCount: "+rotationFrameCount);
            rotationFrameCount = 0;
        }*/
        
    }
}