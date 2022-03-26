namespace Tyme_Engine.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EngineWindow game = new EngineWindow(800, 800, "Tyme Engine"))
            {
                game.Run(144);
                game.VSync = OpenTK.VSyncMode.On;
            }
        }
    }
}