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
    }




    protected virtual void Act()
    {
        Console.WriteLine("Behavior.Act: not implemented");
        // Even better: make this an *abstract* method to force implementing it in subclasses!
    }
}