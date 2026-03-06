using System;
using System.Threading;

public class GameManager
{
    private Board _board;
    private int _tryNum;
    private int _findPair;
    private int _maxTryNum;
    private int _previewTime;

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
                _maxTryNum = level * 10;
                if(level == 1)
                {
                    _previewTime = 5;
                    _board = new Board(2, 4);
                    break;
                }
                else if(level == 2)
                {
                    _previewTime = 3;
                    _board = new Board(4, 4);
                    break;
                }
                else if(level == 3)
                {
                    _previewTime = 2;
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

    public void ReadyGame()
    {
        Console.Clear();
        Console.WriteLine("카드를 섞는중...");
        Console.WriteLine();
        _board.PrintAnswer();
        Thread.Sleep(_previewTime*1000);
        Console.Clear();
    }

    public void GameStart()
    {
        int row1 = 0, col1 = 0, row2 = 0, col2 = 0;

        while (true)
        {
            PrintInfo();
            Query("첫 번째 카드를 선택하세요 (행 열): ", ref row1, ref col1);
            Query("두 번째 카드를 선택하세요 (행 열): ", ref row2, ref col2);
            CheckPair(row1, col1, row2, col2);

            if (_tryNum == _maxTryNum)
            {
                PrintInfo();
                Console.WriteLine("====== 게임 오버 ======");
                Console.WriteLine("시도 횟수를 모두 사용했습니다");
                Console.WriteLine($"찾은 쌍: {_findPair}/{_board.GetTotalNum()}");
                break;
            } 

            if (_findPair == _board.GetTotalNum())
            {
                PrintInfo();
                Console.WriteLine("====== 게임 클리어 ======");
                Console.WriteLine($"총 시도 횟수: {_tryNum}");
                break;
            }
        }
    }

    public void PrintInfo()
    {
        _board.PrintBoard();
        Console.WriteLine($"시도 횟수: {_tryNum}/{_maxTryNum} | 찾은 쌍: {_findPair}/{_board.GetTotalNum()}");
        Console.WriteLine();
    }

    public void Query(string query, ref int row, ref int col)
    {
        while(true)
        {
            Console.Write($"{query} ");
            string q = Console.ReadLine();
            string[] num = q.Split(' ');

            if (num.Length == 2 && int.TryParse(num[0], out int r) && int.TryParse(num[1], out int c))
            {
                if (0 < r && r < _board.Width + 1 && 0 < c && c < _board.Height + 1)
                {
                    if (_board.GetCardState(r, c) == "**")
                    {
                        _board.ChooseNum(r, c);
                        row = r;
                        col = c;
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
    }

    public void CheckPair(int row1, int col1, int row2, int col2)
    {
        if (_board.CardBoard[row1,col1].Num == _board.CardBoard[row2, col2].Num)
        {
            Console.WriteLine("짝을 찾았습니다!");
            _board.CardBoard[row1, col1].ApplyState("Pair");
            _board.CardBoard[row2, col2].ApplyState("Pair");
            _tryNum++;
            _findPair++;
        }
        else
        {
            Console.WriteLine("짝이 맞지 않습니다!");
            _board.CardBoard[row1, col1].ApplyState("Unknown");
            _board.CardBoard[row2, col2].ApplyState("Unknown");
            _tryNum++;
        }
        Thread.Sleep(1500);
        Console.Clear();
    }
}