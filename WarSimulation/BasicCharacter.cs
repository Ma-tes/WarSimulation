using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WarSimulation
{
    internal class BasicCharacter : IRenderable, ICharacter, IMoveable
    {
        private readonly Size defaultSize = new(50, 50);

        public string Name { get; init; }

        public Vector2 Position { get; set; }

        public Size Size { get; set; }

        public Color Color { get; set; }

        protected bool Collide { get; set; } = false;

        public BasicCharacter(string name, Vector2 startPosition, Size size) 
        {
            Name = name;
            Position = startPosition;
            if ((size.Width * size.Height) > 25000)
                Size = defaultSize;
            else
                Size = size;
        }

        public virtual Task Update() 
        {
            return Task.CompletedTask;
        }

        public virtual Task OnPaint(Graphics e)
        {
            using (var customBrush = new Pen(Color).Brush)
                e.FillRectangle(customBrush, new Rectangle() { X = (int)Position.X, Y = (int)Position.Y, Size = Size });
            return Task.CompletedTask;
        }

        protected Vector2 GetPositionIndexer(Vector2 position, Vector2 destination) 
        {
            var indexer = position - destination;
            var absIndexer = new Vector2(Math.Abs(indexer.X), Math.Abs(indexer.Y));
            Vector2 nextMove = (indexer / (new Vector2((int)absIndexer.X | 1, (int)absIndexer.Y | 1))) * -1;
            return nextMove;
        }

        public virtual async Task<bool> OnCollision<T>(T[] @object) where T : BasicCharacter 
        {
            for (int i = 0; i < @object.Length; i++)
            {
                if (ICharacter.IsCollide<BasicCharacter>(this, @object[i])) 
                {
                    return true;
                }
            }
            return false;
        }
    }
}
