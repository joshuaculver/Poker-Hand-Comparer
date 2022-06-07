/*
Joshua Culver
Last edit: 5/11/2022

Filename: PokerTable.cs
Class which represents a table with multiple players playing at it as well as operates and returns information on players
*/
using System.Diagnostics;

class PokerTable
{
    public List<PokerHand> hands {get; private set;}
    private List<string> messages;

    public PokerTable()
    {
        Debug.WriteLine("Creating table...");

        hands = new List<PokerHand>();
        messages = new List<string>();
    }

    //Takes a full line of standardized input
    public void AddHands(string input)
    {
        //Clears hands to ensure only two are being compared
        hands.Clear();

        Debug.WriteLine("Input: " + input);

        //Takes the single string input and splits it on empty spaces
        List<string> items = new List<string>();
        items = input.Split(' ').ToList();

        foreach(string item in items)
        {
            Debug.WriteLine(item);
        }

        //Magic number based solution for seperating info for the two hands of given format
        List<string> newHand = items.GetRange(0,6);
        PokerHand black = new PokerHand(newHand);
        hands.Add(black);
        Debug.WriteLine("Added hand...");

        newHand = items.GetRange(7,6);
        PokerHand white = new PokerHand(newHand);
        hands.Add(white);
        Debug.WriteLine("Added hand...");
    }

    //Clears hand list
    public void ClearHands()
    {
        hands.Clear();
    }

    //Outputs each player's name and cards to console
    public void PrintHands()
    {
        foreach (PokerHand player in hands)
        {
            Console.WriteLine("New hand: " + player.name);
            foreach(PokerCard card in player.hand)
            {
                Console.WriteLine("Card: ");
                Console.Write(card.value);
                Console.Write(card.suit + "\n");
            }
        }
    }

    //Returns index of winner in hands list. -1 indicates tie.
    public int GetWinner()
    {
        int blkScore = hands[0].HandValue();
        int whtScore = hands[1].HandValue();

        if(blkScore < whtScore)
        {
            return 1;
        }
        else if (blkScore == whtScore)
        {
            if(hands[0].HighCard() > hands[1].HighCard())
            {
                return 0;
            }
            else if(hands[0].HighCard() < hands[1].HighCard())
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            return 0;
        }
    }

    //Generates a string with information on the winning hand and player
    //If invalid cards exists instead reports that evaluation cannot proceed
    public string DisplayWinner()
    {
        int winner = GetWinner();

        if(winner < 0)
        {
            return "Tie.";
        }
        else
        {
            PokerHand champ = hands[winner];

            return (champ.name + " wins. " + champ.message);
        }
    }
}
