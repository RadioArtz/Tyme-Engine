namespace Tyme_Engine.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EngineWindow game = new EngineWindow(1280, 720, "Tyme Engine"))
            {
                game.Run(200);
                game.VSync = OpenTK.VSyncMode.Off;
                game.WindowState = OpenTK.WindowState.Maximized;
               //game.WindowBorder = OpenTK.WindowBorder.Hidden;
            }
        }
    }
}