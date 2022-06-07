# Poker Hand Comparer
Based on [Poker Hands](http://codingdojo.org/kata/PokerHands/)

This program reads information from the "input" text file and from there builds two poker hands in order to determine a winner or if it is a draw. That information is then printed to the command line.

- Dictionaries (found in "Tables.cs") are used to translate face cards into appropriate data format and vice versa.
- Cards are stored as objects with seperate value and suit parameters, which are stored in hand objects.
- Hands check and store what kind of poker hand they have by strength as well as a message containing information on said poker hand
- Catches innapropriate or invalid card data and reports invalid hands instead of attempting to compare them

I chose to focus primarily on finding an effective and dynamic solution to parsing poker hands. The given problem suggests a very specific input format, so there is a point in which a magic number is used. If work were to continue on this project focus would be on making a more dynamic parser which could accept more generalized input. As well as allowing more than two hands to be given as input and compared. 
