using OpenTK.Windowing.Desktop;
namespace Tyme_Engine.Core
{
    static class Program
    {
        public static bool inEditor;
        private static NativeWindowSettings settings = NativeWindowSettings.Default;
        static void Main(string[] args)
        {
            settings.NumberOfSamples = 16;
#if DEBUG
        inEditor = true;
#endif
            using (EngineWindow game = new EngineWindow(settings))
            {
                Debug.Log(inEditor);
                game.Run();
                game.VSync = OpenTK.Windowing.Common.VSyncMode.Adaptive;
                //OpenTK.Graphics.GraphicsMode Mode = new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(8, 8, 8, 8), 24);
            }
        }
    }
}