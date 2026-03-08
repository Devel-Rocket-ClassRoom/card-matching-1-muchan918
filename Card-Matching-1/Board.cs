using System;

public class Board
{
    private readonly int totalNum; // 카드 짝 개수

    public int Width { get; private set; }
    public int Height { get; private set; }
    public int[] CheckNum { get; set; } // 카드를 2개씩 초기화 시킬 때 필요한 배열
    public Card[,] CardBoard { get; set; }

    public Board(int  width, int height)
    {
        Width = width;
        Height = height;
        totalNum = Width * Height / 2 + 1;
        CheckNum = new int[totalNum];

        // 입력 받은 행과 열을 바로 적용 시키기 위해 밑변과 높이에 각각 +1
        CardBoard = new Card[Width + 1, Height + 1]; 
        SetBoard();
    }

    // 카드 짝 개수 반환
    public int GetTotalNum()
    {
        return totalNum - 1;
    }

    // 보드 한칸씩 카드 할당
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

    // 카드에 들어갈 Number 랜덤하게 부여
    // CheckNum을 통해 2장씩만 들어간다
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

    // 보드 출력
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

    // 답지 출력
    public void PrintAnswer()
    {
        for (int i = 1; i < Width + 1; i++)
        {
            for (int j = 1; j < Height + 1; j++)
            {
                CardBoard[i, j].ApplyState("Pair");
            }
        }

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
                CardBoard[i, j].PrintCard();
            }
            Console.WriteLine();
        }

        for (int i = 1; i < Width + 1; i++)
        {
            for (int j = 1; j < Height + 1; j++)
            {
                CardBoard[i, j].ApplyState("Unknown");
            }
        }
    }

    // 행과 열에 있는 카드를 Open 상태로 변경
    public void ChooseNum(int row, int col)
    {
        CardBoard[row, col].ApplyState("Open");
    }

    // 행과 열에 있는 카드를 Unknown 상태로 변경
    public void HideNum(int row, int col)
    {
        CardBoard[row, col].ApplyState("Unknown");
    }
}