using System;

class TimeAttackMode : GameBase
{
    private int _limitTime;
    private DateTime _startTime;

    public override void OnSuccess()
    {
        
    }

    public override void OnFail()
    {
        
    }

    // 제한 시간 도달 시 게임 종료
    public override bool IsGameOver(Board board, int findPair)
    {
        int elapsed = (int)(DateTime.Now - _startTime).TotalSeconds;
        if (elapsed >= _limitTime)
        {
            board.PrintBoard();
            Console.WriteLine("====== 게임 오버 ======");
            Console.WriteLine("제한 시간을 초과했습니다.");
            Console.WriteLine($"찾은 쌍: {findPair}/{board.GetTotalNum()}");
            return true;
        }

        if (findPair == board.GetTotalNum())
        {
            board.PrintBoard();
            Console.WriteLine("====== 게임 클리어 ======");
            Console.WriteLine($"소요 시간: {elapsed}");
            return true;
        }
        return false;
    }

    // 게임 상태 출력
    public override void GetStatusText(Board board, int findPair)
    {
        int elapsed = (int)(DateTime.Now - _startTime).TotalSeconds;
        if (elapsed >= _limitTime) { elapsed = _limitTime; }
        board.PrintBoard();
        Console.WriteLine($"경과 시간: {elapsed}/{_limitTime} | 찾은 쌍: {findPair}/{board.GetTotalNum()}");
        Console.WriteLine();
    }

    // 게임 레벨 세팅
    public override void SetGameLevel(GameLevel gamelevel)
    {
        _limitTime = 30 * ((int)gamelevel + 1);
        _startTime = DateTime.Now;
    }
}