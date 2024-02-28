using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

class PlayerBullet : AnimationSprite
{
    float vx, vy;
    public PlayerBullet(float pVx, float pVy, string filename = "circle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetOrigin(width/2, height/2);
        scale = 0.6f;
        vx = pVx;
        vy = pVy;
    }

    protected virtual void Update()
    {
        if (((MyGame)game).isPaused) return;
        x += vx;
        y += vy;
        // TODO: Check whether offscreen / hit test, and then remove!
    }
}

// SECONDARY BULLET CLASS

class PlayerSecondary : PlayerBullet
{
    int slider;
    private Player player;
    private List<AnimationCircles> Circles = new List<AnimationCircles>();

    public PlayerSecondary(float pVx, float pVy, int pSlider, string filename = "circle.png", int cols = 1, int rows = 1) : base(pVx, pVy, filename, cols, rows)
    {
        height = 5;
        alpha = 0.5f;
        player = game.FindObjectOfType<Player>();
        rotation = player.rotation;
        slider = pSlider;

        for (int i = 0; i < 10; i++)
        {
            Circles.Add(new AnimationCircles("circle.png", 1, 1));
            LateAddChild(Circles.Last());
            Circles.Last().SetOrigin(width / 2, height / 2);
            Circles.Last().width = 50;
            //Console.WriteLine("Circle generated");
        }
    }

    protected override void Update()
    {
        if (((MyGame)game).isPaused) return;
        base.Update();

        //width += 2;

        if (DistanceTo(player) > slider*3)
        {
            Destroy();
        }
    }
}



class AnimationCircles : AnimationSprite
{
    public AnimationCircles(string filename = "circle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        
    }

    void Update()
    {
        //width /= 2;
        height = 50;
    }
}