/*
Joshua Culver
Last edit: 5/11/2022

Filename: PokerHand.cs
A class which both holds information on a player's hand, performs operations on the cards, and reports information
*/
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

class PokerHand
{
    public string name {get; private set;}
    public List<PokerCard> hand {get; private set;}
    public Dictionary<int, int> numbers {get; private set;}
    public Dictionary<char, int> suits {get; private set;}
    public string message;

    public PokerHand(List<string> entries)
    { 
        //Assuming the input starts with a name i.e. Black: or White: 
        name = entries[0];
        Debug.WriteLine("Hand name: " + name);
        //Cleans name for later output. 
        name = string.Concat(name.Where(char.IsLetterOrDigit));

        //Create list of size items to attempt to create cards from
        entries.RemoveAt(0);
        hand = new List<PokerCard>();

        //Creates cards and add them to the player's "hand"
        foreach(string card in entries)
        {
            PokerCard newCard = new PokerCard(card);
            hand.Add(newCard);
        }

        message = "";

        numbers = new Dictionary<int, int>();
        suits = new Dictionary<char, int>();

        //Sorts cards by value as well as building the dictionaries for reference
        SortCards();
    }

    //Sorts cards by value for straights and calls Match()
    public void SortCards()
    {
        if (hand.Count >= 2)
        {
            Debug.WriteLine("Sorting hand: ");

            foreach (PokerCard card in hand)
            {
                Debug.WriteLine($"{card.value}" + $"{card.suit}" + "\n");
            }

            //Uses LinQ to sort the cards in the hand by value
            hand = (from s in hand orderby s.value select s).ToList();

            Debug.WriteLine("Sorted hand: ");

            foreach (PokerCard card in hand)
            {
                Debug.WriteLine($"{card.value}" + $"{card.suit}" + "\n");
            }

            Match();
        }
        else
        {
            Console.WriteLine("Cannot sort less than two cards");
        }
    }

    //Matches values and suits of cards into the hands dictionaries
    private void Match()
    {
        foreach(PokerCard card in hand)
        {
            //If key exists increase value
            if(numbers.ContainsKey(card.value))
            {
                numbers[card.value] = numbers[card.value] + 1;
            }
            //If key doesn't exist add it
            else
            {
                numbers.Add(card.value, 1);
            }

            if(suits.ContainsKey(card.suit))
            {
                suits[card.suit] = suits[card.suit] + 1;
            }
            else
            {
                suits.Add(card.suit, 1);
            }
        }
    }

    //Checks if there are matching cards of any value in the given number. 2 would check for a pair.
    public bool HasMatch(int size)
    {
        foreach(KeyValuePair<int, int> entry in numbers)
        {
            if (entry.Value == size)
            {
                return true;
            }
        }

        return false;
    }

    //Finds and returns the value of cards which have matches of given number. 3 would find a three of a kind.
    public int GetMatch(int matches)
    {
        foreach(KeyValuePair<int, int> entry in numbers)
        {
            if (entry.Value == matches)
            {
                return entry.Key;
            }
        }

        return -1;
    }

    //Iterates over card values to check for a straight
    public bool Straight()
    {
        int index = 0;

        int val1, val2;

        while(index < hand.Count - 1)
        {
            val1 = hand[index].value;
            val2 = hand[index + 1].value;

            if (val2 != val1 + 1)
            {
                return false;
            }

            index++;
        }

        return true;
    }

    //Attempts to return an array of ints which have values of the three of a kind and pair for a full house
    public int[] GetFullHouse()
    {
        int[] result = {-1, -1};

        if(HasMatch(3) & HasMatch(2))
        {
            result = new int[] {GetMatch(3), GetMatch(2)};
            return (result);
        }

        return result;
    }

    //Checks for a flush. The hands dictionary will only have one entry if there's only one suit in it.
    public bool Flush()
    {
        if(suits.Count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
  
    }

    //Iterates over value dictionary and returns true of there are two pairs in the hand
    public bool TwoPair()
    {
        int counter = 0;

        foreach(KeyValuePair<int, int> entry in numbers)
        {
            if (entry.Value == 2)
            {
                counter++;
            }
        }

        if(counter == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Finds the highest card value and returns it
    public int HighCard()
    {
        int maxNum = numbers.Keys.Max();

        return maxNum;
    }

    //Finds key that matches passed value
    public string FindByValue(int num)
    {
        int value = numbers.FirstOrDefault(x => x.Value == num).Key;

        return FaceCheck(value);
    }

    //Converts a card value to a string while checking to see if it needs it's name looked up for faces
    public string FaceCheck(int num)
    {
        if(num > 10 & num < 15)
        {
            return Tables.RevLookUpFace(num);
        }
        else
        {
            return num.ToString();
        }
    }

    //Returns an int value representing the rank of the hand in descending rank
    //Also adds a message string to the hand with information on what hand it is
    public int HandValue()
    {
        if(hand.Count != 5)
        {
            Debug.WriteLine("Innapropriate number of cards in hand: " + name);
        }

        string value;

        if(Flush())
        {
            if(Straight())
            {
                message = $"- with straight flush: {Tables.LookUpSuit(hand[0].suit)} from {hand[0].value} to {hand[4].value}";
                return 9;
            }
        }

        if(HasMatch(4))
        {
            value = FindByValue(4);

            message = $"- with four of a kind: {value}";
            return 8;
        }

        if(HasMatch(3) & HasMatch(2))
        {
            string three = FindByValue(3);
            string two = FindByValue(2);

            message = $"- with full house: {three} over {two}";
            return 7;
        }

        if(Flush())
        {
            value = hand[0].suit.ToString();

            message = $"- with flush: {value}";
            return 6;
        }

        if(Straight())
        {
            string low = FaceCheck(hand[0].value);
            string high = FaceCheck(hand[4].value);

            message = $"- with flush: {low} to {high}";
            return 5;
        }

        if(HasMatch(3))
        {
            value = FindByValue(3);

            message = $"- with 3 of a kind: {value}";
            return 4;
        }

        if(TwoPair())
        {
            string first = FindByValue(2);
            int secondInt = numbers.LastOrDefault(x => x.Value == 2).Key;
            string second = FaceCheck(secondInt);
            
            message = $"- with two pairs: {first} and {second}";
            return 3;
        }

        if(HasMatch(2))
        {
            value = FindByValue(2);

            message = $"- with one pair: {value}";
            return 2;
        }

        int num = HighCard();

        value = FaceCheck(num);

        message = $"- with high card: {value}";
        return 1;
    }
}