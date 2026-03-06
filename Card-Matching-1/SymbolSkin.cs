using System;

class SymbolSkin : ICardSkin
{
    string[] symbols = { "★", "♠", "♥", "♦", "♣", "●", "■", "▲", "☆", "♡", "○", "□" };

    ConsoleColor[] colors =
    {
        ConsoleColor.Yellow,
        ConsoleColor.Blue,
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Magenta,
        ConsoleColor.White,
        ConsoleColor.DarkYellow,
        ConsoleColor.DarkRed,
        ConsoleColor.DarkGreen,
        ConsoleColor.DarkCyan,
        ConsoleColor.DarkMagenta,
    };

    public string GetDisplay(int cardNum)
    {
        return symbols[cardNum - 1];
    }
    public ConsoleColor GetColor(int cardNum)
    {
        return colors[cardNum - 1];
    }
}