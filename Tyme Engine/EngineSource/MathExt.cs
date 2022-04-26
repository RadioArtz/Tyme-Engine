using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyme_Engine.Core
{
    public static class MathExt
    {
        public static double Lerp(double a,double b,float alpha)
        {
            return (a+(b-a)*alpha);
        }
        public static float Lerp(float a, float b, float alpha)
        {
            return (a+(b-a)*alpha);
        }
        public static double Clamp01(double input)
        {
            if (input > 1) { return 1; }
            else if (input < 0) { return 0; }
            return input;
        }
        public static float Clamp01(float input)
        {
            if (input > 1) { return 1; }
            else if (input < 0) { return 0; }
            return input;
        }
        public static int Clamp01(int input)
        {
            if (input > 1) { return 1; }
            else if (input < 0) { return 0; }
            return input;
        }
        public static T Clamp01
            
    }
}
