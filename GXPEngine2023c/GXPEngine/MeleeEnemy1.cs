using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MeleeEnemy1 : MeleeEnemy
{
    // Worm: Low HP, Low DMG, Low SPD

    public MeleeEnemy1(string fileName = "sprite_meleeEnemy1.png", int cols = 4, int rows = 3,
                       int hp = 1, 
                       int dmg = 2, 
                       float spd = 1.5f
                        ) : base(fileName, cols, rows, hp, dmg, spd)
    {
        enemyType = 1;

        SetOrigin(width / 2, height / 2);

    }

}