using System;
using System.Threading;

public class GameManager
{
    private Board _board;
    private int _tryNum;
    private int _findPair;

    private const int MaxTryNum = 20;

    public GameManager()
    {
        Console.WriteLine("======= 카드 짝 맞추기 게임 ========");
        Console.WriteLine();
        _tryNum = 0;
        _findPair = 0;
        SelectLevel();
    }

    public void SelectLevel()
    {
        Console.WriteLine("난이도를 선택하세요:");
        Console.WriteLine("1. 쉬움 (2x4)");
        Console.WriteLine("2. 보통 (4x4)");
        Console.WriteLine("3. 어려움 (4x6)");
        while (true)
        {
            Console.Write("선택: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int level))
            {
                if(level == 1)
                {
                    _board = new Board(2, 4);
                    break;
                }
                else if(level == 2)
                {
                    _board = new Board(4, 4);
                    break;
                }
                else if(level == 3)
                {
                    _board = new Board(4, 6);
                    break;
                }
                else
                {
                    Console.WriteLine("1, 2, 3 중 하나를 입력하세요.");
                }
            }
            else
            {
                Console.WriteLine("1, 2, 3 중 하나를 입력하세요.");
            }
        }
    }

    public void GameStart()
    {
        while (true)
        {
            PrintInfo();
            Query();

            if (_tryNum == 20) break;
        }
    }

    public void PrintInfo()
    {
        _board.PrintBoard();
        Console.WriteLine($"시도 횟수: {_tryNum}/{MaxTryNum} | 찾은 쌍: {_findPair}/{_board.GetTotalNum()}");
        Console.WriteLine();
    }

    public void Query()
    {
        int row1, col1, row2, col2;

        while(true)
        {
            Console.Write("첫 번째 카드를 선택하세요 (행 열): ");
            string query1 = Console.ReadLine();
            string[] num = query1.Split(' ');

            if (num.Length == 2 && int.TryParse(num[0], out int row) && int.TryParse(num[1], out int col))
            {
                if (0 < row && row < _board.Width + 1 && 0 < col && col < _board.Height + 1)
                {
                    if (_board.GetBoardState(row, col) == "**")
                    {
                        _board.ChooseNum(row, col);
                        row1 = row;
                        col1 = col;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("뽑을 수 없는 행과 열입니다");
                    }
                }
                else
                {
                    Console.WriteLine("행은 1~4, 열은 1~4 범위로 입력하세요.");
                }
            }
            else
            {
                Console.WriteLine("행은 1~4, 열은 1~4 범위로 입력하세요.");
            }
        }

        Console.Clear();
        PrintInfo();

        while (true)
        {
            Console.Write("두 번째 카드를 선택하세요 (행 열): ");
            string query1 = Console.ReadLine();
            string[] num = query1.Split(' ');

            if (num.Length == 2 && int.TryParse(num[0], out int row) && int.TryParse(num[1], out int col))
            {
                if (0 < row && row < _board.Width + 1 && 0 < col && col < _board.Height + 1)
                {
                    if(_board.GetBoardState(row, col) == "**")
                    {
                        row2 = row;
                        col2 = col;
                        _board.ChooseNum(row, col);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("뽑을 수 없는 행과 열입니다");
                    }
                }
                else
                {
                    Console.WriteLine("행은 1~4, 열은 1~4 범위로 입력하세요.");
                }
            }
            else
            {
                Console.WriteLine("행은 1~4, 열은 1~4 범위로 입력하세요.");
            }
        }

        Console.Clear();
        PrintInfo();

        CheckPair(row1, col1, row2, col2);
    }

    public void CheckPair(int row1, int col1, int row2, int col2)
    {
        if (_board.NumBoard[row1,col1] == _board.NumBoard[row2, col2])
        {
            Console.WriteLine("짝을 찾았습니다!");
            _board.StringBoard[row1, col1] = _board.NumBoard[row1,col1].ToString();
            _board.StringBoard[row2, col2] = _board.NumBoard[row2, col2].ToString();
            _tryNum++;
            _findPair++;
        }
        else
        {
            _board.StringBoard[row1, col1] = "**";
            _board.StringBoard[row2, col2] = "**";
            _tryNum++;
            Console.WriteLine("짝이 맞지 않습니다!");
        }
        Thread.Sleep(1000);
        Console.Clear();
    }
}