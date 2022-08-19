namespace Wordle
{
    internal class Board
    {
        public static int cursorX;
        public static int cursorY;
        public static int index;
        public static char[] boxLetters = { ' ', ' ', ' ', ' ', ' ' };
        public static List<ConsoleColor> boxColors = new List<ConsoleColor>{
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGray,};
        public static bool resume;
        public static bool GameOver;
        public static DateTime NewDate = DateTime.Now;
        public const string pauseMenu = @"
      ╔═══════════════════╗
      ║    Time Travel    ║
      ║    Random Word    ║
      ║       Quit        ║
      ╚═══════════════════╝";
        public const string ChangeDate = @"
      ╔═══════════════════╗
      ║    ╔══════════╗   ║
      ║    ║          ║   ║
      ║    ╚══════════╝   ║
      ╚═══════════════════╝";
        public const string header = @"
      ╔═══════════╦╦╦═════╗
      ║   ╔╦╦╦═╦═╦╝║╠═╗   ║
      ║   ║║║║║║╠╣║║║╩╣   ║
      ║   ╚══╩═╩╝╚═╩╩═╝   ║
      ╚═══════════════════╝";
        public const string box = @"
      ╔═══╦═══╦═══╦═══╦═══╗
      ║   ║   ║   ║   ║   ║
      ╠═══╬═══╬═══╬═══╬═══╣
      ║   ║   ║   ║   ║   ║
      ╠═══╬═══╬═══╬═══╬═══╣
      ║   ║   ║   ║   ║   ║
      ╠═══╬═══╬═══╬═══╬═══╣
      ║   ║   ║   ║   ║   ║
      ╠═══╬═══╬═══╬═══╬═══╣
      ║   ║   ║   ║   ║   ║
      ╠═══╬═══╬═══╬═══╬═══╣
      ║   ║   ║   ║   ║   ║
      ╚═══╩═══╩═══╩═══╩═══╝

       Q W E R T Y U I O P 
        A S D F G H J K L
         Z X C V B N M";
        private static int secret;

        public static void WinMessage()
        {
            Console.SetCursorPosition(6, 6);

            switch (Board.cursorX)
            {
                case 8:  Console.WriteLine("       Genius!        "); break;
                case 10: Console.WriteLine("     Magnificent!     "); break;
                case 12: Console.WriteLine("     Impressive!      "); break;
                case 14: Console.WriteLine("      Splendid        "); break;
                case 16: Console.WriteLine("        Great         "); break;
                case 18: Console.WriteLine("        Phew...       "); break;
            }
        }

        public static void MoveRight()
        {
            if (cursorY != 24)
            {
                cursorY = cursorY + 4;
                Console.SetCursorPosition(cursorY, cursorX);
                index++;
            }
        }

        public static void MoveLeft()
        {
            if (cursorY != 8)
            {
                cursorY = cursorY - 4;
                Console.SetCursorPosition(cursorY, cursorX);
                index--;
            }
        }

        public static void NextRow()
        {
            if (cursorX != 18)
            {
                Console.SetCursorPosition(6, 20);
                Console.WriteLine("                    ");
                Array.Fill(boxLetters, ' ');

                cursorX = cursorX + 2;
                cursorY = 8;
                Console.SetCursorPosition(cursorY, cursorX);
                index = 0;
            }
        }

        public static void DrawColor(int yCoordinate, int index,ConsoleColor color)
        {
            Console.SetCursorPosition(yCoordinate - 1, cursorX);
            Console.BackgroundColor = color;
            Console.Write(" " + boxLetters[index] + " ");
            Console.ResetColor();
            Console.SetCursorPosition(yCoordinate, cursorX);
        }

        public static void SetRowColors()
        {
            for (int index = 0; index < boxColors.Count; index++)
            {
                DrawColor((8 + (index * 4)), index, boxColors[index]);
            }
        }

        public static void PauseScreen()
        {
            int option = 0;
            resume = false;
            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(pauseMenu);
            Console.SetCursorPosition(11, option + 2);

            while (!resume)
            {
                ConsoleKey pauseKey = Console.ReadKey(true).Key;
                switch (pauseKey)
                {
                    case ConsoleKey.UpArrow:
                        if (option != 0)
                        {
                            option--;
                            Console.SetCursorPosition(11, option + 2);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (option != 2)
                        {
                            option++;
                            Console.SetCursorPosition(11, option + 2);
                        }
                        break;

                    case ConsoleKey.Enter:
                        switch (option)
                        {
                            case 0: //change date
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine(ChangeDate);
                                Console.SetCursorPosition(12, 3);
                                NewDate = DateTime.Parse(Console.ReadLine());
                                Logic.GameWord = Logic.AnotherDay();
                                resume = true;
                                Console.SetCursorPosition(6, 6);
                                Console.WriteLine("      " + NewDate.ToString("d") + "     ");
                                NewGame();
                                break;

                            case 1: //random word
                                Logic.GameWord = Logic.RandomWord();
                                resume = true;
                                Console.SetCursorPosition(6, 6);
                                //Console.WriteLine("      " + NewDate.ToString("d") + "     ");
                                NewGame();
                                break;

                            case 2: //quit
                                Console.SetCursorPosition(6, 6);
                                Console.WriteLine("         bye          ");
                                Console.SetCursorPosition(6, 21);
                                Console.Write("                       ");
                                Console.SetCursorPosition(6, 22);
                                Console.Write("                       ");
                                Console.SetCursorPosition(6, 23);
                                Console.Write("                       ");
                                Console.SetCursorPosition(1, 21);
                                System.Environment.Exit(0);
                                
                                //resume = true;
                                //GameOver = true;
                                break;
                        }
                        break;

                    case ConsoleKey.PageDown:
                        if (secret >= 2)
                        {
                            Console.SetCursorPosition(0, 27);
                            Console.WriteLine(Logic.HintList.Count() + "     ");
                            foreach (var word in Logic.HintList)
                                Console.WriteLine(word);
                            Console.SetCursorPosition(cursorY, cursorX);
                            secret = 0;
                            resume = true;
                        }
                        else
                            secret++;
                        break;

                    case ConsoleKey.Escape:
                        resume = true;
                        break;
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(header);
            Console.SetCursorPosition(cursorY, cursorX);

            if (GameOver)
                Console.SetCursorPosition(2, 21);
        }

        public static void NewGame()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(34, 25);
            Console.WriteLine(header);
            Console.WriteLine(box);
            Console.SetCursorPosition(6, 6);
            Console.WriteLine("      " + NewDate.ToString("d") + "     ");

            cursorX = 8;
            cursorY = 8;
            index = 0;
            resume = false;
            GameOver = false;
            secret = 0;
            Logic.HintList = new List<string>(WordList.wordFive);
            
            Console.SetCursorPosition(cursorY, cursorX);

            while (!GameOver)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;

                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;

                    case >= ConsoleKey.A and <= ConsoleKey.Z:
                        boxLetters[index] = (char)key;
                        Console.Write(boxLetters[index]);
                        Console.SetCursorPosition(cursorY, cursorX);
                        MoveRight();
                        break;

                    case ConsoleKey.Backspace:
                        Console.Write(' ');
                        boxLetters[index] = ' ';
                        Console.SetCursorPosition(cursorY, cursorX);
                        if (cursorY != 8)
                            MoveLeft();
                        break;

                    case ConsoleKey.Enter:
                        var x = new string(boxLetters);

                        if (boxLetters.Contains(' '))
                        {
                            Console.SetCursorPosition(6, 20);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  Not enough letters  ");
                            Console.ResetColor();
                            Console.SetCursorPosition(cursorY, cursorX);
                        }

                        else if (!Logic.wordList.Contains(x.ToLower()))
                        {
                            Console.SetCursorPosition(6, 20);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  Not in word list   ");
                            Console.ResetColor();
                            Console.SetCursorPosition(cursorY, cursorX);
                        }

                        else
                        {
                            Logic.CheckWord();
                            SetRowColors();
                            NextRow();
                        }
                        break;

                    case ConsoleKey.Escape:
                        resume = false;
                        PauseScreen();
                        
                        break;
                }
            }
            PauseScreen();
        }

        public static void EliminateChar(char letter)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            if (KeyRow.rowOne.Contains(letter)) // (Q) 7, 21
            {
                Console.SetCursorPosition((Array.IndexOf(KeyRow.rowOne, letter) * 2 + 7), 21);
                Console.Write(letter);
            }
            else if (KeyRow.rowTwo.Contains(letter)) // (A) 8, 22
            {
                Console.SetCursorPosition((Array.IndexOf(KeyRow.rowTwo, letter)* 2 + 8), 22);
                Console.Write(letter);
            }
            else // (Z) 9, 23
            {
                Console.SetCursorPosition((Array.IndexOf(KeyRow.rowThree, letter) * 2 + 9), 23);
                Console.Write(letter);
            }

            Console.SetCursorPosition(cursorX, cursorY);
            Console.ResetColor();
        }
    }
}
