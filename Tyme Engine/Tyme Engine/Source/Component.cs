using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tyme_Engine.Components
{
    class Component
    {
        public Tyme_Engine.Core.GameObject parentObject { get; }

        public Component()
        {
        }
        public void OnComponentAttached(Tyme_Engine.Core.GameObject parentObject)
        {
            Console.WriteLine("Component Created and attached to " + parentObject.objectName);
        }
    }
}

