using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MeleeEnemy2 : MeleeEnemy
{
    // Crab: Mid HP, Mid DMG, Mid SPD
   
    public MeleeEnemy2(string fileName = "sprite_meleeEnemy2.png", int cols = 4, int rows = 2,
                       int hp = 4,
                       int dmg = 3,
                       float spd = 2.4f
                        ) : base(fileName, cols, rows, hp, dmg, spd)
    {
        enemyType = 2;
       
        SetOrigin(width / 2, height / 2);
 
    }

}