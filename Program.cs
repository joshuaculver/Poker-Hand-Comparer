/*
Joshua Culver
Last edit: 5/11/2022

Filename: Program.cs
Main driver for testing testing the program
*/

class Program
{
    static void Main(string[] args)
    {
        //Creates table which has player hands, which in turn have cards
        PokerTable table = new PokerTable();

        try
        {
            //Read each line of input.txt for testing input
            string[] inputs = System.IO.File.ReadAllLines(@"input.txt");

            //Iterate through lines for testing
            foreach (string entry in inputs)
            {
                //Display line being evaluated
                Console.WriteLine("input: " + entry);

                //Give line to table to turn it into player hands
                table.AddHands(entry);
                
                //Evaluate and display which is the better hand
                Console.WriteLine(table.DisplayWinner() + "\n");
            }
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("Could not find 'input.txt'");
        }

        Console.Read();
    }
}