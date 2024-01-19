using Tyme_Engine.Core;
using OpenTK;
using System.Diagnostics;
using OpenTK.Input;
using System;
using OpenTK.Mathematics;
using Tyme_Engine.IO;
using Tyme_Engine.Components;
using Assimp.Unmanaged;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Tyme_Engine
{
    class ArrayDemoMgr : UserScript   
    {
        //private Stopwatch _timer = new Stopwatch();
        bool ready = false;
        bool isSorted;
        GameObject[] ObjectArray;
        int[] ValueArray;
        Assimp.Scene MeshRef;
        ArrayDemoHelper[] ComponentArray;
        private KeyboardState? Keyboard;
        public int arraySize { get; private set; } = 128;
        public int currentIndex = 0;
        public ArrayDemoMgr()
        {
        }
        public override void Start()
        {
            //_timer.Start();
            string meshPath = Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/ArrayBarMesh.fbx");
            //string meshPath = Path.Combine(Environment.CurrentDirectory, "EngineContent/Meshes/cube.fbx");
            MeshRef = AssetImporter.LoadMeshSync(meshPath);
            ObjectArray = new GameObject[arraySize];            
            ComponentArray = new ArrayDemoHelper[arraySize];  
            ValueArray = new int[arraySize];     
            Random randomgen = new Random();
            //init array
            for (int i = 0; i < arraySize; i++)
            {
                ValueArray[i] = i;
            }
            //shuffle array
            foreach(int i in ValueArray)
            {
                int secondindex = randomgen.Next(1, arraySize);
                int firstvalue = ValueArray[i];
                ValueArray[i] = ValueArray[secondindex];
                ValueArray[secondindex] = firstvalue;
            }
            //create bars
            for (int i = 0; i < arraySize; i++)
            {
                ObjectArray[i] = new GameObject("ArrayBar" + i);
                ObjectArray[i].AddComponent(new TransformComponent());
                ObjectArray[i].AddComponent(new StaticMeshComponent(MeshRef, true));
                var helper = new ArrayDemoHelper(ValueArray[i], i);
                ObjectArray[i].AddComponent(helper);
                ComponentArray[i] = helper;
            }
            
            ready = true;
            FastBeep.FastBeepSetFrequency(0);
            FastBeep.FastBeepPlay();
        }

        public override void Update(float delta)
        {
            Keyboard = Program.GetEngineWindow.KeyboardState;
            if (Keyboard.IsKeyPressed(Keys.Space))
            {
                Random randomgen = new Random();
                ready = false;
                isSorted = false;
                foreach (int i in ValueArray)
                {
                    int secondindex = randomgen.Next(1, arraySize);
                    int firstvalue = ValueArray[i];
                    ValueArray[i] = ValueArray[secondindex];
                    ValueArray[secondindex] = firstvalue;
                }
                for (int i = 0; i < arraySize; i++)
                {
                    ComponentArray[i].UpdateValue(ValueArray[i]);
                }
                ready = true;
                FastBeep.FastBeepPlay();
            }


            if (isSorted || !ready)
                return;
            //slight mess, mainly takes care of skipping out in certain errorcases. also increments the index.
            #region
            for (int i = 0; i < arraySize; i++)
            {
                ComponentArray[i].SetMarkState(0);
            }
            if (currentIndex == arraySize - 1)
                currentIndex = 0;
            else
                currentIndex++;

            if (currentIndex >= arraySize - 1)
                return;
            #endregion  
            else
            {
                ComponentArray[currentIndex].SetMarkState(1);
                ComponentArray[currentIndex + 1].SetMarkState(1);
                //new Thread(() => Console.Beep((ValueArray[currentIndex] + 3) * 24, 200)).Start();   //threadded weil sonst nichts mehr funktioniert.
                FastBeep.FastBeepSetFrequency((ValueArray[currentIndex+1] + 5) * 3 + 60);

                //Main Sorting Step
                if (ValueArray[currentIndex] > ValueArray[currentIndex + 1])
                {
                    int firstValue = ValueArray[currentIndex];
                    ValueArray[currentIndex] = ValueArray[currentIndex + 1];
                    ValueArray[currentIndex + 1] = firstValue;
                }
            }
            //Update the bars values
            for (int i = 0; i < arraySize; i++)
            {
                ComponentArray[i].UpdateValue(ValueArray[i]);
            }
            VerifySort();
        }

        public void VerifySort()
        {
            isSorted = true;
            for (int i = 0; i < arraySize; i++)
            {
                if (ValueArray[i] > ValueArray[Math.Clamp(i + 1,0,arraySize-1)])
                {
                    isSorted = false;
                    return; ;
                }
            }
            for (int i = 0; i < arraySize; i++)
            {
                ComponentArray[i].SetMarkState(2);
            }
            FastBeep.FastBeepPause();
        }

        public override void PreRender(float delta)
        {
            if (this.IsPendingKill)
                return;
        }
    }
}