namespace Wordle
{
    internal class Logic
    {
        static DateTime BeginningOfTime = new DateTime(2021, 6, 19);   //wordList[0]
        static DateTime EndOfTime = new DateTime(2027, 10, 14);        //wordList[2308]
        public static string[] wordList = WordList.wordFive;
        public static List<string> HintList = new List<string>(WordList.wordFive);
        public static string GameWord = TodaysWord();
        public static bool WinGame = false;

        public static string TodaysWord()
        {
            int day = (int)(DateTime.Today - BeginningOfTime).TotalDays;

            return wordList[day].ToUpper();
        }

        public static string RandomWord()
        {
            Random r = new Random();
            int day = r.Next(0, 2308);
            Board.NewDate = BeginningOfTime.AddDays(day);

            return wordList[day].ToUpper();
        }

        public static string AnotherDay()
        {
            DateTime selectedDay = Board.NewDate;
            int day = (int)(selectedDay - BeginningOfTime).TotalDays;

            return wordList[day].ToUpper();
        }

        public static void CheckWord()
        {
            var word = GameWord.ToCharArray();
            var letters = Board.boxLetters;
            var correctLetters = 0;

            for (int i = 0; i < word.Length; i++)
            {
                if (letters[i] == word[i])
                {
                    Board.boxColors[i] = ConsoleColor.DarkGreen;
                    HintList.RemoveAll(x => x[i] != (Char.ToLower(letters[i])));
                    correctLetters++;
                }

                else if (word.Contains(letters[i]))
                {
                    Board.boxColors[i] = ConsoleColor.DarkYellow;
                    HintList.RemoveAll(x => x[i] == (Char.ToLower(letters[i])));
                }

                else
                {
                    Board.boxColors[i] = ConsoleColor.Black;
                    HintList.RemoveAll(x => x.Contains(Char.ToLower(letters[i])));
                    Board.EliminateChar(letters[i]);
                }
            }

            if (correctLetters == 5)
            {
                WinGame = true;
                Board.WinMessage();
                Board.GameOver = true;
            }
            else if (Board.cursorX == 18)
            {
                Console.SetCursorPosition(6, 6);
                Console.WriteLine("        " + Logic.GameWord + "        ");
                Board.GameOver = true;
            }
        }
    }
}
