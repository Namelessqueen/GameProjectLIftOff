﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LevelUpCard : GameObject
{
    private int allCardsCount = 11;  // total number of ability cards
    private float cardHeight = 400; // position based on pixels, bigger number moves it lower
    private int selectionCooldown = 100;

    private int cardAmount;     // number of cards being drawn, defaults to 3
    private int cardNumber;     // specifies the card used
    private Random random;
    private bool initialDone;
    private Sprite selectionArrow;
    private int cardSelected = 0;

    private List<Card> cards = new List<Card>();
    private List<int> selectedCards = new List<int>();
    private List<int> arrowxPos = new List<int>();


    public LevelUpCard(int cardCount = 3)
    {
        cardAmount = cardCount;
        random = new Random();

        selectionArrow = new Sprite("sprite_levelUpCard_selectionOutline.png");
        selectionArrow.SetOrigin(selectionArrow.width / 2, selectionArrow.height / 2);
        AddChild(selectionArrow);

    }


    void SpawnCards()
    {
        if (initialDone) return;
        for (int i = 0; i < cardAmount; i++)
        {
            cardNumber = random.Next(0, allCardsCount);
            while (selectedCards.Contains(cardNumber)) cardNumber = random.Next(0, allCardsCount);
            int cardX = game.width / (cardAmount + 1) * (i + 1);

            cards.Add(new Card(cardNumber));
            selectedCards.Add(cardNumber);
            arrowxPos.Add(cardX);

            cards.Last().SetXY(cardX, cardHeight);
            AddChild(cards.Last());
        }
        initialDone = true;
        ((MyGame)game).isPaused = true;
    }

    void CardSelection()
    {
        selectionCooldown -= Time.deltaTime;
        if (selectionCooldown > 0) return;
        if (Input.GetKeyDown(Key.A)) cardSelected--; selectionCooldown = 100;
        if (Input.GetKeyDown(Key.D)) cardSelected++; selectionCooldown = 100;
        if (cardSelected < 0) cardSelected = arrowxPos.Count - 1;
        if (cardSelected >= arrowxPos.Count) cardSelected = 0;

        selectionArrow.SetXY(arrowxPos[cardSelected], cardHeight);

        if (Input.GetKeyDown(Key.U)) SelectCard();

    }


    void SelectCard()
    {

        cards[cardSelected].GetAbility();
        LateDestroy();
        ((MyGame)game).isPaused = false;
    }

    void Update()
    {
        SpawnCards();
        CardSelection();
    }

}

