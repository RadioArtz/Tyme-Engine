namespace Tyme_Engine.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EngineWindow game = new EngineWindow(1280, 720, "Tyme Engine"))
            {
                game.Run();
                game.VSync = OpenTK.VSyncMode.Off;
                
            }
        }
    }
}