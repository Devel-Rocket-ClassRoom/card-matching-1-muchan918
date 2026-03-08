using System;

// 숫자 카드 스킨
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