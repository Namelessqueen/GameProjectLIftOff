using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

class PassiveTurret : AnimationSprite
{
    private float bulletSpeed = 1.5f;        // The speed bullets will travel at
    private float reloadTime = .9f;     // Time in seconds until turret shoots again
    //private byte animTime = 3;         // (frames) frames before next animation frame starts

    private float reloadCooldown;

    Player player;
    Level level;
    private float bulletXRotHelp, bulletYRotHelp;
    private List<PlayerBullet> playerBullets = new List<PlayerBullet>();

    public PassiveTurret(string filename = "sprite_passiveTurret.png", int cols = 4, int rows = 2) : base(filename, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        player = game.FindObjectOfType<Player>();
        level = game.FindObjectOfType<Level>();
        //alpha = .3f;
        //x -= 15;
        y += 45;
        //y -= 300;
        scale = 2;
        //rotation = 270;
    }

    void Update()
    {
        if (((MyGame)game).isPaused) return;

        Attacking();

        float numberyay = (1 - ((reloadTime * 1000) - reloadCooldown) / 1000) * 8;
        SetFrame(((int) numberyay + 6) % 8);

    }


    void Attacking()
    {
        // helping the bullets getting the right rotation
        var a = player.rotation * Mathf.PI / 180.0;
        float cosa = (float)Math.Cos(a);
        float sina = (float)Math.Sin(a);

        bulletXRotHelp = (0 * cosa - -5 * sina);
        bulletYRotHelp = (0 * sina + -5 * cosa);


        if (reloadCooldown / 1000 > 0)
        {
            reloadCooldown -= Time.deltaTime;
            return;
        }

        playerBullets.Add(new PlayerBullet(bulletSpeed * bulletXRotHelp, bulletSpeed * bulletYRotHelp));
        
        playerBullets.Last().SetXY(player.x, player.y);
        playerBullets.Last().rotation = player.rotation;
        level.AddChild(playerBullets.Last());
        reloadCooldown += reloadTime * 1000;
    }

}