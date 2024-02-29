using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy2 : ShootingEnemy
{
    // Pufferfish: (Very) Low HP, Mid DMG, Very Low SPD, Low Proj size, Cone attack (not implemented yet)

    public ShootingEnemy2(string fileName = "sprite_rangedEnemy2.png", int cols = 4, int rows = 2,
                       int hp = 3,
                       int dmg = 5,
                       float spd = 1f,
                       float projSize = .2f
                        ) : base(fileName, cols, rows, hp, dmg, spd, projSize)
    {
        enemyType = 6;

        SetOrigin(width / 2, height / 2);

    }

}
