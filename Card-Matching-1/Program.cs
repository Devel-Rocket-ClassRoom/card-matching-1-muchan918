using System;
using System.Threading;

while (true)
{
    GameManager GM = new GameManager();

    GM.ReadyGame();
    GM.GameStart();

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


