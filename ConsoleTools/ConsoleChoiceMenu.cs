using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// GUI tools for the C# console applications.
/// By /u/programjm123
/// </summary>
namespace ConsoleTools
{
    /// <summary>
    /// Used to display options to user and store input. Options can be selected either by typing their label (e.g. 1,2,3...A,B,C) or selecting with arrow keys and pressing enter.
    /// </summary>
    public class ConsoleChoiceMenu
    {
        //fields
        private string[] options;
        private int currentIndex;
        private int result;
        private int startLeft;
        private int startTop;
        const string optionLabels = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ*%#@!~?+=";
        ConsoleColor mainColor;

        public ConsoleChoiceMenu(string[] options)
        {
            this.options = options;
        }
        public int Result
        { get
            {
                return result;
            }
        }
        public void ShowMenuAndStoreInput()
        {
            currentIndex = 0;
            bool originalCursorVisibility = Console.CursorVisible;
            var originalForegroundColor = Console.ForegroundColor;
            var originalBackgroundColor = Console.BackgroundColor;
            mainColor = Console.ForegroundColor.Equals(ConsoleColor.Gray)
                ? ConsoleColor.Gray : ConsoleColor.White;
            Console.CursorVisible = false;


            if (options == null)
                throw new InvalidOperationException("The options have not been initialized.");
            if (Console.CursorLeft!=0)
                Console.Out.WriteLine();

            startLeft = Console.CursorLeft;
            startTop = Console.CursorTop;

            Boolean choiceMade = false;

            for (int i=0; i<options.Length; i++)
            {
                ShowLine(i);
            }

            while (!choiceMade)
            {
                var input = Console.ReadKey(true);
                char keyChar = input.KeyChar;
                ConsoleKey key = input.Key;

                if (key.Equals(ConsoleKey.DownArrow))
                {
                    int oldIndex = currentIndex;
                    currentIndex = Math.Min(currentIndex + 1, options.Length - 1);
                    ShowLine(currentIndex);
                    ShowLine(oldIndex);
                }
                else if (key.Equals(ConsoleKey.UpArrow))
                {
                    int oldIndex = currentIndex;
                    currentIndex = Math.Max(currentIndex - 1, 0);
                    ShowLine(currentIndex);
                    ShowLine(oldIndex);
                }
                else if (key.Equals(ConsoleKey.Enter))
                {
                    choiceMade = true;
                    result = currentIndex;
                }
                else if (optionLabels.Substring(0, options.Length).Contains(Char.ToUpper(keyChar)))
                {
                    choiceMade = true;
                    result = optionLabels.IndexOf(Char.ToUpper(keyChar));
                }
            }

            Console.CursorVisible = originalCursorVisibility;
            Console.CursorTop = startTop + options.Length;
            Console.CursorLeft = 0;
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;
        }

        private void ShowLine(int index)
        {
            char label;
            if (index < optionLabels.Length)
                label = optionLabels[index];
            else
                label = ' ';

            Console.CursorLeft = startLeft;
            Console.CursorTop = startTop+index;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = mainColor;
            Console.Out.Write($"[{label}] ");
            if (index==currentIndex)
            {
                Console.BackgroundColor = mainColor;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            Console.Out.Write(options[index]);
        }

    }
}
