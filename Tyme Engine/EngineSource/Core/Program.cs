


namespace Tyme_Engine.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EngineWindow game = new EngineWindow())
            {
                game.Run();
                game.VSync = OpenTK.Windowing.Common.VSyncMode.Adaptive;
                //OpenTK.Graphics.GraphicsMode Mode = new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(8, 8, 8, 8), 24);
            }
        }
    }
}