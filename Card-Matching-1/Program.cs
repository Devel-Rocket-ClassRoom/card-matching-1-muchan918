using System;
using System.Threading;

GameManager GM = new GameManager();

Console.Clear();
Console.WriteLine("카드를 섞는중...");
Thread.Sleep(1000);
Console.Clear();

GM.GameStart();

//Board board = new Board(4, 4);
//board.PrintBoard();
//Thread.Sleep(2000);
//board.ChooseNum(1, 1);
//board.PrintBoard();
//Thread.Sleep(2000);
//board.ChooseNum(2, 1);
//board.PrintBoard();
//Thread.Sleep(2000);
//board.HideNum(1, 1);
//board.PrintBoard();
