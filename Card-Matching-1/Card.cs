using System;

public struct Card
{
    public int Num { get; set; }
    CardState cardState;
    public static ICardSkin _cardSkin;

    public Card(int num)
    {
        Num = num;
        cardState = CardState.Unknown;
    }

    public void ApplyState(string cardState)
    {
        if (Enum.TryParse(cardState, out CardState state))
        {
            this.cardState = state;
        }
    }

    public string GetCardState()
    {
        return $"{cardState}";
    }

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

enum CardState
{
    Unknown,
    Open,
    Pair
}
