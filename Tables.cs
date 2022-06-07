/*
Joshua Culver
Last edit: 5/11/2022

Filename: Tables.cs
A static class used to look up and translate card information
*/
public static class Tables
{
    //Face card to value in terms of numeric sequence
    public static Dictionary<char, int> faceTable = new Dictionary<char, int>
        {
            {'T', 10},
            {'J', 11},
            {'Q', 12},
            {'K', 13},
            {'A', 14}
        };

    //Numeric sequence of face card to suit name table
    public static Dictionary<int, string> revFaceTable = new Dictionary<int, string>
        {
            {11, "Jack"},
            {12, "Queen"},
            {13, "King"},
            {14, "Ace"}
        };

    //Char suit format to full suit name table
    public static Dictionary<char, string> suitTable = new Dictionary<char, string>
        {
            {'C', "clubs"},
            {'D', "diamonds"},
            {'H', "hearts"},
            {'S', "spades"}
        };

    //Method which is passed a char and returns the matching int value for a card if it exists
    //Returns -1 if it cannot be found
    public static int LookUpFace(char c)
    {
        try
        {
            int value = faceTable[c];

            return value;
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("Invalid card value");

            return -1;
        }
    }

    //Method which is the inverse of above. Takes int and returns string name of face cards
    //Returns -1 if it cannot be found
    public static string RevLookUpFace(int i)
    {
        try
        {
            string value = revFaceTable[i];

            return value;
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("Invalid card value");

            return "X";
        }
    }

    //Method which looks up the full name of suits using single char short hand
    //Returns null if it cannot be found
    public static string LookUpSuit(char c)
    {
        try
        {
            string value = suitTable[c];

            return value;
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("Invalid card suit");

            return "X";
        }
    }
}
