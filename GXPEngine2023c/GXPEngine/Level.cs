using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

class Level : GameObject
{
    private int xBoundarySize = 500;    // how many pixels can the player be from the sides before scrolling starts
    private int yBoundarySize = 250;    // same but top and bottom
    private int waveTime = 10;          // max time in seconds until next wave spawns

    private List<Enemy> enemies = new List<Enemy>();
    private Player player;
    //private ShootingEnemy sEnemy;
    //private MeleeEnemy mEnemy;
    private float timePassed;
    private int waveNumber;

    private Sprite background;


    public Level()
    {
        //Background:
        background = new Sprite("background_idea_1.png", false, false);
        background.scale = 3;   



        

        player = new Player();
        player.SetXY(game.width/2, game.height/2);

        /*sEnemy = new ShootingEnemy("checkers.png", 1, 1);
        sEnemy.SetXY(200, 200);

        mEnemy = new MeleeEnemy("colors.png", 1, 1);
        mEnemy.SetXY(400, 400);*/

        AddChild(background);



        AddChild(player);
        //AddChild(sEnemy);
        //AddChild(mEnemy);

        player = FindObjectOfType<Player>();


    }

    void EnemySpawning()
    {
        
        timePassed += Time.deltaTime;
        
        if (timePassed/1000 >= waveTime || enemies.Count == 0)
        {
            waveNumber++;
            Console.WriteLine("new wave");

            // spawn the enemies
            for (int i = 0; i < waveNumber; i++)
            {
                if (i%2 == 0) enemies.Add(new ShootingEnemy());
                else          enemies.Add(new MeleeEnemy());

                // RANDOM ENEMY SPAWNING DOESN'T KEEP THE PLAYER IN MIND YET
                // ALSO ONLY SPAWNS IN THE SPACE OF THE STARTING SCREEN
                enemies.Last().SetXY(Utils.Random(100, game.width - 100), Utils.Random(100, game.height - 100));

                AddChild(enemies.Last());
  
                Console.WriteLine("enemies.count: "+enemies.Count);

                
            }


            // reset timer
            // timePassed -= waveTime*1000;     // DOESN'T WORK IF WAVE SPAWNS BECAUSE NO ENEMIES ON SCREEN
            timePassed = 0;
            Console.WriteLine("new timePassed: " + timePassed);
            Console.WriteLine("waveNumber: " + waveNumber);
        }


    }


    public void HandleScroll()
    {
        if (player == null) return;

        if (player.x + x < xBoundarySize) x = xBoundarySize - player.x;
        if (player.y + y < yBoundarySize) y = yBoundarySize - player.y;
        
        if (player.x + x > game.width - xBoundarySize) x = game.width - xBoundarySize - player.x;
        if (player.y + y > game.height - yBoundarySize) y = game.height - yBoundarySize - player.y;

        if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
        if (y > 0) y = 0;
        // same but on top
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        EnemySpawning();
        HandleScroll();
    }
}