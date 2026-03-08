using System;

// 게임 모드 추상 클래스
abstract class GameBase
{
    public abstract void OnSuccess();
    public abstract void OnFail(); 
    public abstract bool IsGameOver(Board board, int findPair);
    public abstract void GetStatusText(Board board, int findPair);
    public abstract void SetGameLevel(GameLevel gamelevel);
}

// 게임 난이도 열거형 변수
public enum GameLevel
{
    Easy = 1,
    Normal,
    Hard
}