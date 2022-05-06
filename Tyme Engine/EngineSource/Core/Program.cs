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
                OpenTK.Graphics.GraphicsMode Mode = new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(8, 8, 8, 8), 24);
            }
        }
    }
}