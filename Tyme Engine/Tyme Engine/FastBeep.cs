using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tyme_Engine
{
    static class FastBeep
    {
        [DllImport("dlls\\FastBeepLib.dll")]
        public static extern void PrintMessage();
        [DllImport("dlls\\FastBeepLib.dll")]
        public static extern void FastBeepInit(int Buffersize);
        [DllImport("dlls\\FastBeepLib.dll")]
        public static extern void FastBeepFree();
        [DllImport("dlls\\FastBeepLib.dll")]
        public static extern void FastBeepSetFrequency(float freq);
        [DllImport("dlls\\FastBeepLib.dll")]
        public static extern void FastBeepPlay();
        [DllImport("dlls\\FastBeepLib.dll")]
        public static extern void FastBeepPause();
    }
}
