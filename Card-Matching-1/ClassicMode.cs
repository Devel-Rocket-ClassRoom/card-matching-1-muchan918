using System;
using System.Reflection.Emit;

class ClassicMode : GameBase
{
    private int _tryNum = 0; // 게임 시도 횟수
    private int _maxTryNum;

    public override void OnSuccess()
    {
        _tryNum++;
    }

    public override void OnFail()
    {
        _tryNum++;
    }

    // 시도 횟수 모두 사용하면 게임 종료
    public override bool IsGameOver(Board board, int findPair)
    {
        if (_tryNum == _maxTryNum)
        {
            board.PrintBoard();
            Console.WriteLine("====== 게임 오버 ======");
            Console.WriteLine("시도 횟수를 모두 사용했습니다");
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

    // 시도 횟수로 게임 상태 출력
    public override void GetStatusText(Board board, int findPair)
    {
        board.PrintBoard();
        Console.WriteLine($"시도 횟수: {_tryNum}/{_maxTryNum} | 찾은 쌍: {findPair}/{board.GetTotalNum()}");
        Console.WriteLine();
    }

    // 시도 횟수 제한으로 게임 세팅
    public override void SetGameLevel(GameLevel gamelevel)
    {
        _maxTryNum = (int)gamelevel * 10;
    }
}