using System;

public class Board
{
    private readonly int totalNum;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int[] CheckNum { get; set; }
    public int[,] NumBoard { get; set; }
    public string[,] StringBoard { get; set; }

    public Board(int  width, int height)
    {
        Width = width;
        Height = height;
        totalNum = Width * Height / 2 + 1;
        CheckNum = new int[totalNum];
        NumBoard = new int[Width + 1, Height + 1];
        StringBoard = new string[Width + 1, Height + 1];
        SetNumBoard();
        SetStringBoard();
    }

    public int GetTotalNum()
    {
        return totalNum - 1;
    }

    public string GetBoardState(int row, int col)
    {
        return StringBoard[row, col];
    }

    public void SetNumBoard()
    {      
        for(int i = 1; i < Width+1; i++)
        {
            for(int j = 1; j < Height+1; j++)
            {
                NumBoard[i, j] = InitNumber();
            }
        }
    }

    public void SetStringBoard()
    {
        for (int i = 1; i < Width + 1; i++)
        {
            for (int j = 1; j < Height + 1; j++)
            {
                StringBoard[i, j] = "**";
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
                Console.Write($"\t{StringBoard[i, j]}");
            }
            Console.WriteLine();
        }
    }

    public void PrintAnswer()
    {
        Console.Write("\t");
        for (int i = 1; i < Height + 1; i++)
        {
            Console.Write($"{i}열\t");
        }
        Console.WriteLine();

        for (int i = 1; i < Width + 1; i++)
        {
            Console.Write($"{i}행");
            for (int j = 1; j < Height + 1; j++)
            {
                Console.Write($"\t{NumBoard[i, j]}");
            }
            Console.WriteLine();
        }
    }

    public void ChooseNum(int row, int col)
    {
        StringBoard[row, col] = $"[{NumBoard[row, col].ToString()}]";
    }

    public void HideNum(int row, int col)
    {
        StringBoard[row, col] = "**";
    }
}