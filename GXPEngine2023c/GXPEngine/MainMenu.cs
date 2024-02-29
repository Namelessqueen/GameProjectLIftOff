using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainMenu : GameObject
{
    //private Game awesomegame;

    public MainMenu(string bgImg = "square.png")
    {
        AddChild(new Sprite(bgImg));



    }


    void Update()
    {

        //awesomegame = game.FindObjectOfType<Game>();
        if (Input.GetKeyDown(Key.U))
        {
            MyGame supergame = game.FindObjectOfType<MyGame>();
            Console.WriteLine("smt happening");
            supergame.GameStart();

        }
        //awesomegame.GameStart();
        

    }

}