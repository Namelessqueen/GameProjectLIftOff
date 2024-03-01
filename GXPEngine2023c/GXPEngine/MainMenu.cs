using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainMenu : GameObject
{
    float channelVolume17 = 1f;    // TitleScreen_BGM_Loopable.wav
    FMODSoundSystem soundSystem;

    public MainMenu(string bgImg = "STARTING SCREEN2.png")
    {
        AddChild(new Sprite(bgImg));

        if (bgImg == "STARTING SCREEN2.png")
        {
            soundSystem = new FMODSoundSystem();
            soundSystem.PlaySound(soundSystem.CreateStream("TitleScreen_BGM_Loopable.wav", true), 17, false, channelVolume17, 0);
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(Key.P))
        {
            MyGame supergame = game.FindObjectOfType<MyGame>();
            //Console.WriteLine("smt happening");
            supergame.GameStart();

        }
        

    }

}