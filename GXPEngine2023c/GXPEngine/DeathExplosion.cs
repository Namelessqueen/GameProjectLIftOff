using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeathExplosion : AnimationSprite
{
    private byte frameTimer = 10;

    public DeathExplosion(string fileName = "sprite_death.png", int cols = 4, int rows = 3) : base(fileName, cols, rows, 9, false, false)
    {
        SetOrigin(width/2, height/2);
        SetCycle(0, 9, frameTimer);

    }


    void Update()
    {

        Animate();
        if (_currentFrame == 8)
        {
            LateDestroy();
        }


    }


}