using System;

class NumberSkin : ICardSkin
{
    public string GetDisplay(int cardNum)
    {
        return cardNum.ToString();
    }
    public ConsoleColor GetColor(int cardNum)
    {
        return ConsoleColor.White;
    }
}