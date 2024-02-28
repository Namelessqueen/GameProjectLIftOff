using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ShootingEnemy1 : ShootingEnemy
{
    // Squid: (Very) Low HP, Mid DMG, Very Low SPD, Mid Proj size

    public ShootingEnemy1(string fileName = "sprite_rangedEnemy1.png", int cols = 4, int rows = 2,
                       int hp = 5,
                       int dmg = 5,
                       float spd = .5f,
                       float projSize = .5f
                        ) : base(fileName, cols, rows, hp, dmg, spd, projSize)
    {
        enemyType = 5;

        SetOrigin(width / 2, height / 2);

    }

}