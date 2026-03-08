using System;

// 카드 스킨 인터페이스
public interface ICardSkin
{
    string GetDisplay(int cardNum);
    ConsoleColor GetColor(int cardNum);
}