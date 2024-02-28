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
    Level level;
    public PlayerBullet(float pVx, float pVy, string filename = "circle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        scale = 0.6f;
        vx = pVx;
        vy = pVy;
        level = game.FindObjectOfType<Level>();
    }

    protected virtual void Update()
    {
        if (((MyGame)game).isPaused) return;
        x += vx;
        y += vy;
        // TODO: Check whether offscreen / hit test, and then remove!
        // Destroy if off screen
        if (x < -level.x || x > -level.x + game.width ||
            y < -level.y || y > -level.y + game.height)
        {
            Console.WriteLine("Enemy bullet despawned");
            LateDestroy();
        }
    }
}

// SECONDARY BULLET CLASS

class PlayerSecondary : PlayerBullet
{
    float originalWidth = 0;
    float originalHeight = 0;
    int slider;
    private Player player;
    private List<AnimationCircles> Circles = new List<AnimationCircles>();

    public PlayerSecondary(float pVx, float pVy, int pSlider, string filename = "circle.png", int cols = 1, int rows = 1) : base(pVx, pVy, filename, cols, rows)
    {
        originalWidth = width;
        originalHeight = height;
        height = 5;
        alpha = 0.5f;
        player = game.FindObjectOfType<Player>();
        rotation = player.rotation;
        slider = pSlider;

        for (int i = 0; i < 10; i++)
        {
            AnimationCircles circle = new AnimationCircles("circle.png", 1, 1);
            Circles.Add(circle);
            LateAddChild(circle);
            circle.SetOrigin(width / 2, height / 2);
            circle.x = (i - 5) * width / 10;

            //Console.WriteLine("Circle generated, height = " + oldHeight + " , " + (oldHeight / height ) );
        }
    }

    protected override void Update()
    {
        if (((MyGame)game).isPaused) return;
        base.Update();

        width += 2;
        float widthFactor = originalWidth / width;
        for (int i = 0; i < 10; i++)
        {
            AnimationCircles circle = Circles[i];
            circle.width = (int)(circle.originalWidth * originalWidth / width / 2);
            circle.height = (int)((circle.originalHeight * originalHeight) / height / 2);
            //Console.WriteLine("Circleupdated " + circle.scale );
        }

        if (DistanceTo(player) > slider * 3)
        {
            Console.WriteLine("Removed SecondaryBullet");
            Destroy();
        }
    }
}



class AnimationCircles : AnimationSprite
{
    public int originalWidth;
    public int originalHeight;
    public AnimationCircles(string filename = "circle.png", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        originalWidth = width;
        originalHeight = height;
        //scale = .5f;
    }
}