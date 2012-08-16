using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PlnWatchDataImporter
{
    static class Program
    {
        public static void ClearCurrentConsoleLine(int left, int width)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(left, currentLineCursor - 1);
            for (int i = 0; i < width; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor - 1);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
