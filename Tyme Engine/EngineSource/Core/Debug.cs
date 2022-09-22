using System;

namespace Tyme_Engine.Core
{
    public static class Debug
    {
        public static void Log(object obj)
        {
            Console.WriteLine(obj);
        }
        public static void Log(object obj,ConsoleColor textFrontColor)
        {
            Console.ForegroundColor = textFrontColor;
            Console.WriteLine(obj);
            Console.ResetColor();
        }
        public static void Log(object obj, ConsoleColor textFrontColor,ConsoleColor textBackColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = textFrontColor;
            Console.BackgroundColor = textBackColor;
            Console.WriteLine(obj);
            Console.ResetColor();
        }
    }
}