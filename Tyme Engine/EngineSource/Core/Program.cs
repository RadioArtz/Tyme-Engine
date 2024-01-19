using OpenTK.Windowing.Desktop;
namespace Tyme_Engine.Core
{
    static class Program
    {
        public static bool inEditor;
        private static NativeWindowSettings settings = NativeWindowSettings.Default;
        public static EngineWindow GetEngineWindow { get; private set; }
        static void Main(string[] args)
        {
            FastBeep.FastBeepInit(5);
            settings.NumberOfSamples = 16;
#if DEBUG
        inEditor = true;
#endif
            GetEngineWindow = new EngineWindow(settings);
            Debug.Log(inEditor);
            GetEngineWindow.UpdateFrequency = 400;
            GetEngineWindow.VSync = OpenTK.Windowing.Common.VSyncMode.Adaptive;
            GetEngineWindow.Run();
            //OpenTK.Graphics.GraphicsMode Mode = new OpenTK.Graphics.GraphicsMode(new OpenTK.Graphics.ColorFormat(8, 8, 8, 8), 24);
            FastBeep.FastBeepFree();
        }
    }
}