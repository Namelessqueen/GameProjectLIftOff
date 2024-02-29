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
    private int waveTime = 30;          // max time in seconds until next wave spawns
    private float enemyWaveRangeMultiplier = 1.05f;  // how much the range increases every wave
    private float meleeEnemyMinSpawnRange = 2;
    private float meleeEnemyMaxSpawnRange = 3;
    private float rangedEnemyMinSpawnRange = 1;
    private float rangedEnemyMaxSpawnRange = 2;

    public int waveNumber;

    private List<Enemy> enemies = new List<Enemy>();
    private Player player;
    private float timePassed;
    private int meleeEnemiesSpawning;
    private int rangedEnemiesSpawning;
    private float enemyxSpawn;
    private float enemyySpawn;
    private Random random;
    private Sprite background;


    public Level()
    {
        //Background:
        background = new Sprite("FINAL BACKGROUND.png", false, false);
        //background.scale = 4;   

        random = new Random();
        player = new Player();
        player.SetXY(1536, 1377);

        AddChild(background);

        AddChild(player);

        
        player = FindObjectOfType<Player>();


    }

    void EnemySpawning()
    {
        
        timePassed += Time.deltaTime;
        
        if (timePassed/1000 >= waveTime || enemies.Count == 0)
        {
            waveNumber++;
            Console.WriteLine("new wave");

            // spawn melee enemies
            meleeEnemiesSpawning = Utils.Random((int)meleeEnemyMinSpawnRange, (int)meleeEnemyMaxSpawnRange+1);
            meleeEnemyMinSpawnRange *= enemyWaveRangeMultiplier;
            meleeEnemyMaxSpawnRange *= enemyWaveRangeMultiplier;

            for (int i = 0; i < meleeEnemiesSpawning; i++)
            {
                int rand = Utils.Random(0, 4);
                switch (rand)
                {
                    case 0:
                        enemies.Add(new MeleeEnemy1());
                        break;
                    case 1:
                        enemies.Add(new MeleeEnemy2());
                        break;
                    case 2:
                        enemies.Add(new MeleeEnemy3());
                        break;
                    case 3:
                        enemies.Add(new MeleeEnemy4());
                        break;
                }

                // random spawn position
                if (random.Next(1, 3) == 1)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        // spawn above screen
                        enemyxSpawn = -x + random.Next(0, game.width); 
                        enemyySpawn = -y - 100;
                    }
                    else
                    {
                        // spawn below screen
                        enemyxSpawn = -x + random.Next(0, game.width); 
                        enemyySpawn = -y + game.height + 100;
                    }
                }
                else
                {
                    if (random.Next(1, 3) == 1)
                    {
                        // spawn left of screen
                        enemyxSpawn = -x - 100;
                        enemyySpawn = -y + random.Next(0, game.height);
                    }
                    else
                    {
                        // spawn right of screen
                        enemyxSpawn = -x + game.width + 100;
                        enemyySpawn = -y + random.Next(0, game.height);
                    }
                }

                enemies.Last().SetXY(enemyxSpawn, enemyySpawn);

                AddChild(enemies.Last());


            }
            
            // spawn ranged enemies
            rangedEnemiesSpawning = Utils.Random((int)rangedEnemyMinSpawnRange, (int)rangedEnemyMaxSpawnRange+1);
            rangedEnemyMinSpawnRange *= enemyWaveRangeMultiplier;
            rangedEnemyMaxSpawnRange *= enemyWaveRangeMultiplier;

            for (int i = 0; i < rangedEnemiesSpawning; i++)
            {
                int rand = Utils.Random(0, 2);
                switch (rand)
                {
                    case 0:
                        enemies.Add(new ShootingEnemy1());
                        break;
                    case 1:
                        enemies.Add(new ShootingEnemy2());
                        break;
                }

                // random spawn position
                if (random.Next(1, 3) == 1)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        // spawn above screen
                        enemyxSpawn = -x + random.Next(0, game.width);
                        enemyySpawn = -y - 100;
                    }
                    else
                    {
                        // spawn below screen
                        enemyxSpawn = -x + random.Next(0, game.width);
                        enemyySpawn = -y + game.height + 100;
                    }
                }
                else
                {
                    if (random.Next(1, 3) == 1)
                    {
                        // spawn left of screen
                        enemyxSpawn = -x - 100;
                        enemyySpawn = -y + random.Next(0, game.height);
                    }
                    else
                    {
                        // spawn right of screen
                        enemyxSpawn = -x + game.width + 100;
                        enemyySpawn = -y + random.Next(0, game.height);
                    }
                }

                enemies.Last().SetXY(enemyxSpawn, enemyySpawn);

                AddChild(enemies.Last());
            }

            // reset timer
            // timePassed -= waveTime*1000;     // DOESN'T WORK IF WAVE SPAWNS BECAUSE NO ENEMIES ON SCREEN
            timePassed = 0;
            Console.WriteLine("new timePassed: " + timePassed);
            Console.WriteLine("waveNumber: " + waveNumber);
        }


    }

    public void SomethingDied(float xpos, float ypos)
    {
        DeathExplosion deathExplosion = new DeathExplosion();
        deathExplosion.SetXY(xpos, ypos);
        AddChild(deathExplosion);


    }

    public void HandleScroll()
    {
        if (player == null) return;

        if (player.x + x < xBoundarySize) x = xBoundarySize - player.x;
        if (player.y + y < yBoundarySize) y = yBoundarySize - player.y;
        
        if (player.x + x > game.width - xBoundarySize) x = game.width - xBoundarySize - player.x;
        if (player.y + y > game.height - yBoundarySize) y = game.height - yBoundarySize - player.y;

        if (x > 0) x = 0;   // making sure the camera doesn't see the void on the left
        if (y > 0) y = 0;   // same but on top

        if (-x > background.width - game.width) x = -(background.width - game.width);   // right
        if (-y > background.height - game.height) y = -(background.height - game.height); // bottom

    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;
        EnemySpawning();
        HandleScroll();
    }
}