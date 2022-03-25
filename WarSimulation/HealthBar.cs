using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WarSimulation
{
    internal sealed class HealthBar : UIElement, IMoveable
    {
        public Vector2 Position { get; set; }

        private int maxHealth { get; set; } = 100;

        public int CurrentHealth { get; set; } = 100;

        public Size barSize { get; init; } = new(100, 100);

        public HealthBar(int maxHealth, int currentHealth, string name) : base(name)
        {
            this.maxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        public sealed override Task Update()
        {
            //There is probably no implementation
            return Task.CompletedTask;
        }

        public override Task OnPaint(Graphics g)
        {
            int healthWidth = barSize.Width - (barSize.Width - ((barSize.Width / maxHealth) * (CurrentHealth)));
            g.FillRectangle(Brushes.White, new Rectangle() { Size = new Size(barSize.Width + 2, barSize.Height + 2), X = (int)Position.X - 1, Y = (int)Position.Y - 1 });
            g.FillRectangle(Brushes.Green, new Rectangle() { Size = new Size(healthWidth, barSize.Height), X = (int)Position.X, Y = (int)Position.Y });
            return Task.CompletedTask;
        }
    }
}
