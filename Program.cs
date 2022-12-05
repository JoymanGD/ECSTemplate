using System;

namespace Common
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ECSGame())
                game.Run();
        }
    }
}