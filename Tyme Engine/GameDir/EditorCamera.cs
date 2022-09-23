using Tyme_Engine.Core;
using OpenTK;
using OpenTK.Input;
using OpenTK.Mathematics;
using OpenTK.Input.Hid;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Tyme_Engine
{
    class EditorCamera : UserScript
    {
        private MouseState? Mouse;
        private KeyboardState? Keyboard;
        private EngineWindow _window;
        Vector2 lastPos;

        public override void Update(float delta)
        {
        }
        public EditorCamera(EngineWindow window)
        {
            _window = window;
        }
        public override void PreRender(float delta)
        {
            
            Mouse = _window.MouseState;
            Keyboard = _window.KeyboardState;
            ConsoleCommands();
            var transcomp = parentObject._transformComponent;
            var movespeed = delta*4;
            var sensitivity = .1f;

            float deltaX = Mouse.X - lastPos.X;
            float deltaY = Mouse.Y - lastPos.Y;
            lastPos = new Vector2(Mouse.X, Mouse.Y);
            Rendering.RenderInterface._hardcorelamp!._radius = _window.MouseState.Scroll.Y;
            if (!Mouse.IsButtonDown(MouseButton.Right))
            {
                _window.SetCursorGrabbed(CursorState.Normal);
                return;
            }
            
            _window.SetCursorGrabbed(CursorState.Grabbed);
            transcomp.transform.Rotation += new Vector3(-deltaY, deltaX, 0)*sensitivity;
            transcomp.transform.Rotation.X = MathHelper.Clamp(transcomp.transform.Rotation.X,-89.9f , 89.9f);

            if (Keyboard.IsKeyDown(Keys.LeftShift))
            {
                movespeed = delta * 25;
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                transcomp.transform.Location += MathExt.GetRightVector(transcomp.transform.Rotation) * -movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation)*-movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.W))
            {
                transcomp.transform.Location += MathExt.GetForwardVector(transcomp.transform.Rotation) * movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * movespeed;
            }
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                transcomp.transform.Location += MathExt.GetUpVector(transcomp.transform.Rotation) * -movespeed;
            }
        }

        private void ConsoleCommands()
        {
            if (Keyboard.IsKeyPressed(Keys.F1))
            {
                if (AssetManager.assets.Count == 0)
                {
                    Console.WriteLine("No assets loaded", ConsoleColor.Magenta);
                    return;
                }

                int loopCount = 0;
                foreach(AssetManager.Asset tAsset in AssetManager.assets)
                {
                    Console.WriteLine("Asset #" + loopCount + ", Name: " + tAsset.Name + ", Type: " + tAsset.Type.ToString() + ", Hash: " + tAsset.Hash,ConsoleColor.Magenta);
                    loopCount++;
                }
            }

            if (Keyboard.IsKeyPressed(Keys.F2))
            {
                foreach(GameObject obj in ObjectManager.GameObjects)
                {
                    if (obj.childComponents.Count == 0)
                    {
                        Console.WriteLine("No child components found");
                        return;
                    }
                        
                    Console.WriteLine("Object " + obj.objectName + " with child Components of Type: ");
                    foreach(Component comp in obj.childComponents)
                    {
                        if(comp.GetType() == typeof(Components.StaticMeshComponent))
                        {
                            Debug.Log("found StaticMeshComponent with texture index " + ((Components.StaticMeshComponent)comp).texture1.Handle + " ");
                        }
                        //Console.WriteLine(comp.GetType().ToString()+" ");
                    }
                }
            }

            if (Keyboard.IsKeyPressed(Keys.F3))
            {
                foreach (GameObject obj in ObjectManager.GameObjects)
                {
                    foreach(Component comp in obj.childComponents)
                    {
                        if (comp.GetType() == typeof(Components.StaticMeshComponent))
                            Debug.Log("Texture on static mesh with path " + obj._staticMeshComponent.texture1.m_path, ConsoleColor.DarkYellow);
                    }
                }
            }
        }
    }
}