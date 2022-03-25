using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WarSimulation
{
    internal sealed class SimulationCharacter : BasicCharacter
    {
        private Random random = new Random();

        public Vector2 futurePosition { get; set; }

        private Vector2 indexerPosition { get; set; }

        private Vector2 distrubtedPosition { get; set; } = new(200, 200);

        private protected Vector2 startPosition { get; private set; }

        protected HealthBar healthBar = new(100, 100, "SumulationHealthBar") {barSize = new Size(100, 20) };

        public SimulationCharacter(string name, int health, Vector2 startPosition, Size size) : base(name, startPosition, size)
        {
            distrubtedPosition = startPosition;
            this.startPosition = startPosition;
            healthBar.CurrentHealth -= health;
        }

        public override Task Update()
        {
            if ((int)Position.X == futurePosition.X && (int)Position.Y == futurePosition.Y)
            {
                futurePosition = startPosition;
            }
            else 
            {
                int precise = -50;
                Vector2 indexer;
                Vector2 nextMove;
                if ((int)(Position.X) == (int)distrubtedPosition.X && (int)(Position.Y) == (int)distrubtedPosition.Y )
                {
                    indexer = Position - futurePosition;
                    nextMove = GetPositionIndexer(Position, futurePosition);
                    distrubtedPosition = futurePosition - new Vector2(CalculateRatio(indexer.X, nextMove.X, precise), CalculateRatio(indexer.Y, nextMove.Y, precise));
                }
                else 
                {
                    nextMove = GetPositionIndexer(Position, (distrubtedPosition));
                    Position = new Vector2(((Position.X) + nextMove.X), ((Position.Y) + nextMove.Y));
                }
                    indexerPosition = nextMove;
            }
            healthBar.Position = new Vector2(this.Position.X - (this.Size.Width / 2), this.Position.Y - 25);
            return base.Update();
        }

        public override Task OnPaint(Graphics e)
        {
            e.DrawString(Position.ToString(), new Font("Arial", 8), Brushes.White, new PointF(this.Position.X + this.Size.Width, this.Position.Y));
            e.DrawString(indexerPosition.ToString(), new Font("Arial", 8), Brushes.White, new PointF(0, 0));
            e.DrawString(futurePosition.ToString(), new Font("Arial", 8), Brushes.White, new PointF(0, 12));
            return base.OnPaint(e);
        }

        private float CalculateRatio(float value, float indexer, int precise) 
        {
            float valueDifferent = ((random.Next(precise, Math.Abs((int)value))) * indexer);
            return valueDifferent;
        }
    }
}
