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
    float maxSpeed = 11;            // 17
    float xSpeed = -11;             // -17
    float ySpeed = 0;
    Player player;
    float circlex;
    float circley;
    private int rotationFrameCount;

    public PassiveFish(string filename = "sprite_passiveFish.png", int cols = 8, int rows = 1) : base(0, 0, filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        //alpha = .3f;
        //circlex -= 15;
        //circley -= 15;
        circley -= 125;     // 300
        //scale = 2;
        rotation = 270;
        player = game.FindObjectOfType<Player>();
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        xSpeed += xPullBackForce;
        ySpeed += yPullBackForce;
        circlex += xSpeed;
        circley += ySpeed;

        x = circlex + player.x;
        y = circley + player.y;

        if (xSpeed <= -maxSpeed || xSpeed >= maxSpeed) xPullBackForce *= -1;
        if (ySpeed <= -maxSpeed || ySpeed >= maxSpeed) yPullBackForce *= -1;

        rotation -= (360f / 88f);      // 136f

        Animate(.1f);
        // calculating total amount of frames it takes to rotate, which is 136 frames   // 88
        rotationFrameCount++;
        if (xSpeed == 0)
        {
            Console.WriteLine("rotationFrameCount: "+rotationFrameCount);
            rotationFrameCount = 0;
        }
        
    }
}