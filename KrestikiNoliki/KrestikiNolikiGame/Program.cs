using System;

namespace KrestikiNolikiGame
{
    class Program
    {
        static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int currentPlayer = 1;
        static int choice;
        static int flag = 0;

        // НОВОЕ: счётчик побед
        static int scorePlayer1 = 0;
        static int scorePlayer2 = 0;

        // НОВОЕ: кастомные символы
        static char symbolPlayer1 = 'X';
        static char symbolPlayer2 = 'O';

        static void Main(string[] args)
        {
            // КАСТОМИЗАЦИЯ
            Console.WriteLine("=== КАСТОМИЗАЦИЯ ===");
            Console.Write("Игрок 1, выберите свой символ (например @, #, $, %): ");
            symbolPlayer1 = Console.ReadLine()[0];

            Console.Write("Игрок 2, выберите свой символ: ");
            symbolPlayer2 = Console.ReadLine()[0];

            Console.Clear();

            bool playAgain = true;
            while (playAgain)
            {
                // Сброс поля
                for (int i = 0; i < 9; i++)
                    board[i] = (char)('1' + i);
                flag = 0;
                currentPlayer = 1;

                do
                {
                    Console.Clear();
                    ShowBoard();
                    ShowScore();

                    if (flag == 1 || flag == -1)
                        break;

                    Console.WriteLine($"\nИгрок {currentPlayer} ({(currentPlayer == 1 ? symbolPlayer1 : symbolPlayer2)}), выберите номер клетки (1-9): ");
                    choice = int.Parse(Console.ReadLine());

                    if (board[choice - 1] != 'X' && board[choice - 1] != 'O' &&
                        board[choice - 1] != symbolPlayer1 && board[choice - 1] != symbolPlayer2)
                    {
                        if (currentPlayer == 1)
                            board[choice - 1] = symbolPlayer1;
                        else
                            board[choice - 1] = symbolPlayer2;

                        currentPlayer = (currentPlayer == 1) ? 2 : 1;
                    }
                    else
                    {
                        Console.WriteLine("Клетка занята! Нажмите любую клавишу...");
                        Console.ReadKey();
                    }

                    CheckWin();

                } while (flag != 1 && flag != -1);

                Console.Clear();
                ShowBoard();
                ShowScore();

                if (flag == 1)
                {
                    int winner = (currentPlayer == 1) ? 2 : 1;
                    Console.WriteLine($"\nПобедил игрок {winner}!");
                    if (winner == 1) scorePlayer1++;
                    else scorePlayer2++;
                }
                else
                {
                    Console.WriteLine("\nНичья!");
                }

                ShowScore();

                Console.Write("\nСыграем ещё? (да - Enter, нет - 'n'): ");
                string answer = Console.ReadLine();
                playAgain = (answer != "n" && answer != "N");
            }
        }

        static void ShowBoard()
        {
            Console.WriteLine("     |     |     ");
            Console.WriteLine($"  {board[0]}  |  {board[1]}  |  {board[2]}  ");
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine($"  {board[3]}  |  {board[4]}  |  {board[5]}  ");
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine($"  {board[6]}  |  {board[7]}  |  {board[8]}  ");
            Console.WriteLine("     |     |     ");
        }

        static void ShowScore()
        {
            Console.WriteLine($"\n=== СЧЁТ ===\nИгрок 1 ({symbolPlayer1}): {scorePlayer1} | Игрок 2 ({symbolPlayer2}): {scorePlayer2}\n");
        }

        static void CheckWin()
        {
            // Горизонтали
            if (board[0] == board[1] && board[1] == board[2]) flag = 1;
            else if (board[3] == board[4] && board[4] == board[5]) flag = 1;
            else if (board[6] == board[7] && board[7] == board[8]) flag = 1;
            // Вертикали
            else if (board[0] == board[3] && board[3] == board[6]) flag = 1;
            else if (board[1] == board[4] && board[4] == board[7]) flag = 1;
            else if (board[2] == board[5] && board[5] == board[8]) flag = 1;
            // Диагонали
            else if (board[0] == board[4] && board[4] == board[8]) flag = 1;
            else if (board[2] == board[4] && board[4] == board[6]) flag = 1;
            // Ничья
            else
            {
                int filled = 0;
                foreach (char c in board)
                    if (c == symbolPlayer1 || c == symbolPlayer2) filled++;

                if (filled == 9) flag = -1;
                else flag = 0;
            }
        }
    }
}