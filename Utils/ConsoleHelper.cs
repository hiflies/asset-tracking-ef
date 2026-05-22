namespace AssetTracking.Utils
{
    public class ConsoleHelper
    {
        public static void WriteInfo(string message, bool hasSeparator = false)
        {
            Write(message, ConsoleColor.Blue, hasSeparator);
        }

        public static void WriteSuccess(string message, bool hasSeparator = false)
        {
            Write(message, ConsoleColor.Green, hasSeparator);
        }

        public static void WriteError(string message, bool hasSeparator = false)
        {
            Write(message, ConsoleColor.Red, hasSeparator);
        }

        public static void WriteWarning(string message, bool hasSeparator = false)
        {
            Write(message, ConsoleColor.Yellow, hasSeparator);
        }

        public static void Write(string message, ConsoleColor color, bool hasSeparator = false)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();

            if (hasSeparator)
            {
                WriteSeparator();
            }
        }

        public static void WriteSeparator()
        {
            Console.WriteLine("-----------------------------------".PadRight(Console.WindowWidth));
        }

        public static void AskEnterToContinue()
        {
            WriteSeparator();
            Console.Write("Press enter to continue");
            Console.ReadLine();
        }
    }
}