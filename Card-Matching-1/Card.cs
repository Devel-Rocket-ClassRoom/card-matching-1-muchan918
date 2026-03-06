using System;

public struct Card
{
    public int Num { get; set; }
    CardState cardState;

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

    public string PrintCard()
    {
        string s = "";
        switch(cardState)
        {
            case CardState.Unknown:
                s = "**";
                break;
            case CardState.Open:
                s = $"[{Num}]";
                break;
            case CardState.Pair:
                s = $"{Num}]";
                break;
        }
        return s;
    }
}

enum CardState
{
    Unknown,
    Open,
    Pair
}
