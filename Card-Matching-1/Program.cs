using System;
using System.Threading;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// 게임 전체 루프
while (true)
{
    Console.WriteLine("======= 카드 짝 맞추기 게임 ========");
    Console.WriteLine();
    
    // 게임 매니저 생성 (모드 선택 + 난이도 선택)
    GameManager GM = new GameManager();

    // 카드 스킨 선택 및 게임 시작
    GM.SelectSkin();
    GM.ReadyGame();
    GM.GameStart();

    // 재시작 질문
    while (true)
    {
        Console.Write("새 게임을 하시겠습니까? (Y/N): ");
        string input = Console.ReadLine().ToLower();

        if (input == "y")
        {
            Console.WriteLine("계속합니다.");
            Console.Clear();
            break;
        }
        else if (input == "n")
        {
            Console.WriteLine("종료합니다.");
            return;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}


