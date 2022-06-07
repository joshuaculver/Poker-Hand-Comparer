/*
Joshua Culver
Last edit: 5/11/2022

Filename: PokerCard.cs
A class which holds information for a cards suit and value
*/
using System;
using System.Diagnostics;

class PokerCard
{
    public int value {get; private set;}
    public char suit {get; private set;}

    private char[] validSuit = {'C', 'D', 'H', 'S'};

    //Constructor which takes two character card format i.e. "9C" for 9 of clubs
    public PokerCard(string card)
    {
        Debug.WriteLine("input: " + card);

        //Check to see if the value can be cast directly to int or needs to be looked on on the face card table
        if(Char.IsDigit(card[0]))
        {
            value = (int)Char.GetNumericValue(card[0]);
            Debug.WriteLine("Value is int: " + value );
        }
        else if (Tables.LookUpFace(card[0]) != -1)
        {
            value = Tables.LookUpFace(card[0]);
            Debug.WriteLine("Value is face: " + value);
        }
        else
        {
            Debug.WriteLine("Failed to assign value...");
            value = -1;
        }

        //As above but for the suit of the card. Checks to make sure the suit is valid.
        if(Array.Exists(validSuit, x => x == card[1]))
        {
            Debug.Write("Suit is valid...");
            suit = card[1];
            Debug.Write("Suit is valid: " + suit);
        }
        else
        {
            Debug.WriteLine("Failed to assign suit...");
            value = 'X';
        }
        Debug.WriteLine(value + suit);
    }
}