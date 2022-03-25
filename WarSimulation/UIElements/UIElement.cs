using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSimulation
{
    internal abstract class UIElement : IRenderable
    {
        public string Name { get; set; }

        public static List<UIElement> elements = new();

        public UIElement(string name) 
        {
            Name = name;
            elements.Add(this);
        }

        public abstract Task Update();

        public abstract Task OnPaint(Graphics g);
    }
}
