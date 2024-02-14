using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class Level : GameObject
{
    private int xBoundarySize = 500;    // how many pixels can the player be from the sides before scrolling starts
    private int yBoundarySize = 250;    // same but top and bottom

    private Player player;
    private ShootingEnemy sEnemy;
    private MeleeEnemy mEnemy;


    public Level()
    {
        // Background:
        EasyDraw canvas = new EasyDraw(game.width, game.height);
        canvas.Clear(Color.MediumPurple);

        player = new Player();
        player.SetXY(50, 50);

        sEnemy = new ShootingEnemy("checkers.png", 1, 1);
        sEnemy.SetXY(200, 200);

        mEnemy = new MeleeEnemy("colors.png", 1, 1);
        mEnemy.SetXY(400, 400);

        AddChild(canvas);


        AddChild(player);
        AddChild(sEnemy);
        AddChild(mEnemy);

        player = FindObjectOfType<Player>();


    }

    void HandleScroll()
    {
        if (player == null) return;

        if (player.x + x < xBoundarySize) x = xBoundarySize - player.x;
        if (player.y + y < yBoundarySize) y = yBoundarySize - player.y;
        
        if (player.x + x > game.width - xBoundarySize) x = game.width - xBoundarySize - player.x;
        if (player.y + y > game.height - yBoundarySize) y = game.height - yBoundarySize - player.y;


        if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
        if (y > 0) y = 0;   // same but on top
    }

    void Update()
    {
        HandleScroll();
    }


}