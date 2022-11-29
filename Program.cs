using System;

namespace Atmosphere
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new AtmosphereGame())
                game.Run();
        }
    }
}
