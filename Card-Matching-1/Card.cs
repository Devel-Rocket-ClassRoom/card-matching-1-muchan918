using System;

public struct Card
{
    public int Num { get; set; } // 카드의 실제 값 ( 이 값을 짝 지어야함 )
    public CardState cardState; // 카드의 상태 열거형 (Unknown, Open, Pair)
    public static ICardSkin _cardSkin; // 카드 스킨 인터페이스

    public Card(int num)
    {
        Num = num;
        cardState = CardState.Unknown;
    }

    // 카드 상태 적용
    public void ApplyState(string cardState)
    {
        if (Enum.TryParse(cardState, out CardState state))
        {
            this.cardState = state;
        }
    }

    // 현재 카드 상태 얻기
    public string GetCardState()
    {
        return $"{cardState}";
    }

    // 상태에 따라 카드 출력
    // Unknown : **
    // Open : [Num]
    // Pair : Num
    public void PrintCard()
    {
        switch(cardState)
        {
            case CardState.Unknown:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\t**");
                Console.ResetColor();
                break;
            case CardState.Open:
                Console.ForegroundColor = _cardSkin.GetColor(Num);
                Console.Write($"\t[{_cardSkin.GetDisplay(Num)}]");
                Console.ResetColor();
                break;
            case CardState.Pair:
                Console.ForegroundColor = _cardSkin.GetColor(Num);
                Console.Write($"\t{_cardSkin.GetDisplay(Num)}");
                Console.ResetColor();
                break;
        }
    }
}

// 카드 상태 열거형 변수
public enum CardState
{
    Unknown,
    Open,
    Pair
}
