using System;

class SurvivalMode : GameBase
{
    private int _tryNum = 0;
    private int _failStreak;
    private int _maxTryNum;

    public override void OnSuccess()
    {
        _failStreak = 0; // 짝 맞추면 연속 실패 초기화
        _tryNum++;
    }

    public override void OnFail()
    {
        _failStreak++; // 짝 못맞추면 연속 실패 + 1
        _tryNum++;
    }

    // 연속 _maxTryNum 만큼 틀리면 게임 종료
    public override bool IsGameOver(Board board, int findPair)
    {
        if (_failStreak == _maxTryNum)
        {
            board.PrintBoard();
            Console.WriteLine("====== 게임 오버 ======");
            Console.WriteLine($"{_maxTryNum}번 연속 틀렸습니다.");
            Console.WriteLine($"찾은 쌍: {findPair}/{board.GetTotalNum()}");
            return true;
        }

        if (findPair == board.GetTotalNum())
        {
            board.PrintBoard();
            Console.WriteLine("====== 게임 클리어 ======");
            Console.WriteLine($"총 시도 횟수: {_tryNum}");
            return true;
        }
        return false;
    }

    // 연속 실패 횟수로 게임 상태 출력
    public override void GetStatusText(Board board, int findPair)
    {
        board.PrintBoard();
        Console.WriteLine($"연속 실패 횟수: {_failStreak}/{_maxTryNum} | 찾은 쌍: {findPair}/{board.GetTotalNum()}");
        Console.WriteLine();
    }

    // 게임 난이도 세팅
    public override void SetGameLevel(GameLevel gamelevel)
    {
        _maxTryNum = (int)gamelevel + 2;
    }
}