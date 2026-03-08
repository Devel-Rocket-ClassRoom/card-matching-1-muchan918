using System;
using System.Threading;

public class GameManager
{
    private Board _board;
    private int _findPair;
    private int _previewTime; // 미리보기 시간
    private int _boardWidth;
    private int _boardHeight;
    private GameBase _gameBase; // 게임 모드

    public GameManager()
    {
        _findPair = 0;
        SelectMode();
        SelectLevel();
    }

    // 게임 모드 선택
    public void SelectMode()
    {
        Console.WriteLine("게임 모드를 선택하세요");
        Console.WriteLine("1. 클래식");
        Console.WriteLine("2. 타임어택");
        Console.WriteLine("3. 서바이벌");
        while (true)
        {
            Console.Write("선택: ");
            string input = Console.ReadLine();

            // 게임 모드에 맞는 인스턴스 할당
            if(int.TryParse(input, out int mode))
            {
                if (mode == 1)
                {
                    _gameBase = new ClassicMode();
                    break;
                }
                else if (mode == 2)
                {
                    _gameBase = new TimeAttackMode();
                    break;
                }
                else if (mode == 3)
                {
                    _gameBase = new SurvivalMode();
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
        Console.Clear();
    }

    // 난이도 선택
    public void SelectLevel()
    {
        Console.WriteLine("======= 카드 짝 맞추기 게임 ========");
        Console.WriteLine();
        Console.WriteLine("난이도를 선택하세요:");
        Console.WriteLine("1. 쉬움 (2x4)");
        Console.WriteLine("2. 보통 (4x4)");
        Console.WriteLine("3. 어려움 (4x6)");
        while (true)
        {
            Console.Write("선택: ");
            string input = Console.ReadLine();

            // 1. 난이도에 따라 미리보기 시간, 보드 칸 개수가 달라진다
            // 2. 난이도에 따라 각 게임 모드의 종료 조건이 달라진다.
            if (int.TryParse(input, out int level))
            {
                if(level == 1)
                {
                    _gameBase.SetGameLevel(GameLevel.Easy);
                    _previewTime = 5;
                    _boardWidth = 2;
                    _boardHeight = 4;                   
                    break;
                }
                else if(level == 2)
                {
                    _gameBase.SetGameLevel(GameLevel.Normal);
                    _previewTime = 3;
                    _boardWidth = 4;
                    _boardHeight = 4;
                    break;
                }
                else if(level == 3)
                {
                    _gameBase.SetGameLevel(GameLevel.Hard);
                    _previewTime = 2;
                    _boardWidth = 4;
                    _boardHeight = 6;
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

    // 카드 스킨 선택하기
    public void SelectSkin()
    {
        Console.Clear();
        Console.WriteLine("======= 카드 짝 맞추기 게임 ========");
        Console.WriteLine();
        Console.WriteLine("카드 스킨을 선택하세요:");
        Console.WriteLine("1. 숫자 (기본)");
        Console.WriteLine("2. 알파벳 (컬러)");
        Console.WriteLine("3. 기호 (컬러)");
        while (true)
        {
            Console.Write("선택: ");
            string input = Console.ReadLine();

            // 선택에 따라 각 카드 스킨이 달라진다
            if (int.TryParse(input, out int skin))
            {
                if (skin == 1)
                {
                    Card._cardSkin = new NumberSkin();
                    break;
                }
                else if (skin == 2)
                {
                    Card._cardSkin = new AlphabetSkin();
                    break;
                }
                else if (skin == 3)
                {
                    Card._cardSkin = new SymbolSkin();
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

    // 게임 준비
    public void ReadyGame()
    {
        Console.Clear();
        Console.WriteLine("카드를 섞는중...");
        _board = new Board(_boardWidth, _boardHeight);
        Console.WriteLine();
        
        // 카드 정답 공개 ( _previewTime 초 만큼 )
        _board.PrintAnswer();
        Thread.Sleep(_previewTime*1000);
        Console.Clear();
    }

    // 게임 시작
    public void GameStart()
    {
        int row1 = 0, col1 = 0, row2 = 0, col2 = 0;
        bool isGameOver = false;

        // 종료 조건 전까지 루프
        while (!isGameOver)
        {
            // 게임 모드에 따라 다르게 출력
            _gameBase.GetStatusText(_board, _findPair);

            // 질문 두번으로 행과 열 정보를 참조 형식으로 받아옴
            Query("첫 번째 카드를 선택하세요 (행 열): ", ref row1, ref col1);
            Query("두 번째 카드를 선택하세요 (행 열): ", ref row2, ref col2);

            // 받아온 행과 열 정보를 통해 카드 종류가 같은 지 확인
            CheckPair(row1, col1, row2, col2);

            // 게임 종료 조건 판단
            isGameOver = _gameBase.IsGameOver(_board, _findPair);
        }
    }

    // 행과 열 질문하는 함수
    public void Query(string query, ref int row, ref int col)
    {
        while (true)
        {
            Console.Write($"{query} ");
            string q = Console.ReadLine();
            string[] num = q.Split(' ');

            if (num.Length == 2 && int.TryParse(num[0], out int r) && int.TryParse(num[1], out int c))
            {
                if (0 < r && r < _board.Width + 1 && 0 < c && c < _board.Height + 1)
                {
                    string s = _board.CardBoard[r, c].GetCardState();

                    // Unknown 카드를 뽑을 때만 row와 col에 값 할당
                    if (s == "Unknown")
                    {
                        _board.ChooseNum(r, c);
                        row = r;
                        col = c;
                        break;
                    }
                    else if (s == "Open")
                    {
                        Console.WriteLine("같은 카드를 선택할 수 없습니다. 다른 카드를 선택하세요.");
                    }
                    else if (s == "Pair")
                    {
                        Console.WriteLine("이미 짝을 찾은 카드입니다. 다른 카드를 선택하세요.");
                    }
                    else
                    {
                        Console.WriteLine($"행은 1~{_board.Width}, 열은 1~{_board.Height} 범위로 입력하세요.");
                    }
                }
                else
                {
                    Console.WriteLine($"행은 1~{_board.Width}, 열은 1~{_board.Height} 범위로 입력하세요.");
                }
            }
            else
            {
                Console.WriteLine($"행은 1~{_board.Width}, 열은 1~{_board.Height} 범위로 입력하세요.");
            }
        }
        Console.Clear();
        _gameBase.GetStatusText(_board, _findPair);
    }

    // 카드 짝이 맞는 지 체크
    public void CheckPair(int row1, int col1, int row2, int col2)
    {
        if (_board.CardBoard[row1,col1].Num == _board.CardBoard[row2, col2].Num)
        {
            // 짝을 찾으면 Pair 상태로 변경
            Console.WriteLine("짝을 찾았습니다!");
            _board.CardBoard[row1, col1].ApplyState("Pair");
            _board.CardBoard[row2, col2].ApplyState("Pair");
            _gameBase.OnSuccess(); // 게임 모드 별 짝 찾았을 때
            _findPair++;
        }
        else
        {
            // 짝을 못찾으면 다시 Unknown 상태로 변경
            Console.WriteLine("짝이 맞지 않습니다!");
            _board.CardBoard[row1, col1].ApplyState("Unknown");
            _board.CardBoard[row2, col2].ApplyState("Unknown");
            _gameBase.OnFail(); // 게임 모드 별 짝 못 찾았을 때
        }
        Thread.Sleep(1500);
        Console.Clear();
    }
}