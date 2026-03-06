using System;

public class Board
{
    private readonly int totalNum;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int[] CheckNum { get; set; }
    public Card[,] CardBoard { get; set; }

    public Board(int  width, int height)
    {
        Width = width;
        Height = height;
        totalNum = Width * Height / 2 + 1;
        CheckNum = new int[totalNum];
        
        CardBoard = new Card[Width + 1, Height + 1];
        SetBoard();
    }

    public int GetTotalNum()
    {
        return totalNum - 1;
    }

    public void SetBoard()
    {      
        for(int i = 1; i < Width+1; i++)
        {
            for(int j = 1; j < Height+1; j++)
            {
                CardBoard[i, j] = new Card(InitNumber());
            }
        }
    }

    public int InitNumber()
    {
        while (true)
        {
            Random rand = new Random();
            int temp = rand.Next(1, Width * Height / 2 + 1);

            if (CheckNum[temp] < 2)
            {
                CheckNum[temp]++;
                return temp;
            }
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine("======= 카드 짝 맞추기 게임 ========");
        Console.WriteLine();
        Console.Write("\t");
        for(int i = 1; i < Height+1; i++)
        {
            Console.Write($"{i}열\t");
        }
        Console.WriteLine();
 
        for(int i = 1; i < Width+1; i++)
        {
            Console.Write($"{i}행");
            for(int j =1; j < Height + 1; j++)
            {
                CardBoard[i, j].PrintCard();
            }
            Console.WriteLine();
        }
    }

    public void PrintAnswer()
    {
        for (int i = 1; i < Width + 1; i++)
        {
            for (int j = 1; j < Height + 1; j++)
            {
                CardBoard[i, j].ApplyState("Pair");
            }
        }

        PrintBoard();

        for (int i = 1; i < Width + 1; i++)
        {
            for (int j = 1; j < Height + 1; j++)
            {
                CardBoard[i, j].ApplyState("Unknown");
            }
        }
    }

    public void ChooseNum(int row, int col)
    {
        CardBoard[row, col].ApplyState("Open");
    }

    public void HideNum(int row, int col)
    {
        CardBoard[row, col].ApplyState("Unknown");
    }
}