using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class TextCanvas : EasyDraw
{
    private Player player;
    private List<Sprite> Hearts = new List<Sprite>();
    public TextCanvas() : base(Game.main.width, Game.main.height, false)
    {
    
    }

    public void Update()
    { 
        //Background updates every frame to remove the pervious text
        Clear(Color.MediumPurple);
        /*
        for (int i = 0; i < Hearts.Count; i++)
        {
            Hearts.Add(new Sprite("Heart.png", false, false));
        }*/

        player = game.FindObjectOfType<Player>();
        if (player != null)
        {

            Console.WriteLine(player.HealthUpdate(0).ToString());
            Text(player.HealthUpdate(0).ToString(), (game.width / 50), (game.height / 25));
        }
    }
}

