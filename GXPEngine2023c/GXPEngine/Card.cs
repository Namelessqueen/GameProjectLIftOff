using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Card : Sprite
{
    private int cardNumber;
    Player player;

    public Card(int cardNr = 0) : base("sprite_levelUpCard_"+cardNr+".png")
    {
        cardNumber = cardNr;
        alpha = .7f;
        SetOrigin(width / 2, height / 2);

    }


    public void GetAbility()
    {
        player = game.FindObjectOfType<Player>();
        player.GetCardAbility(cardNumber);
    }

}