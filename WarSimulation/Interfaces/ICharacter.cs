using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSimulation
{
    internal interface ICharacter
    {
        public string Name { get; init; }    

        public Size Size { get; set; }

        public Color Color { get; set; }

        public static bool IsCollide<T>(T @objectOne, T @objectTwo) where T : ICharacter, IMoveable 
        {
            bool xAxis = ((objectOne.Position.X + objectOne.Size.Width) >= objectTwo.Position.X) || (objectOne.Position.X <= objectTwo.Position.X + objectTwo.Size.Width);
            bool yAxis = ((objectOne.Position.Y + objectOne.Size.Height) >= objectTwo.Position.Y) || (objectOne.Position.Y <= objectTwo.Position.Y + objectTwo.Size.Height);
            return xAxis || yAxis;
        }
    }
}
