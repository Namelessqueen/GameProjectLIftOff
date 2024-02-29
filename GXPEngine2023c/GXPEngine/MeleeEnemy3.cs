using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MeleeEnemy3 : MeleeEnemy
{
    // Spine: Low HP, Mid DMG, High SPD

    public MeleeEnemy3(string fileName = "sprite_meleeEnemy3.png", int cols = 4, int rows = 2,
                       int hp = 3,
                       int dmg = 2,
                       float spd = 2.6f
                        ) : base(fileName, cols, rows, hp, dmg, spd)
    {
        enemyType = 3;

        SetOrigin(width / 2, height / 2);

    }

}