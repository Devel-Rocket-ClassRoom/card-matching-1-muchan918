using System;

public interface ICardSkin
{
    string GetDisplay(int cardNum);
    ConsoleColor GetColor(int cardNum);
}