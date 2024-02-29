using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MeleeEnemy4 : MeleeEnemy
{
    // Big purple: High HP, High DMG, Low SPD

    public MeleeEnemy4(string fileName = "sprite_meleeEnemy4.png", int cols = 4, int rows = 2,
                       int hp = 4,
                       int dmg = 5,
                       float spd = 1f
                        ) : base(fileName, cols, rows, hp, dmg, spd)
    {
        enemyType = 4;

        SetOrigin(width / 2, height / 2);

    }

}