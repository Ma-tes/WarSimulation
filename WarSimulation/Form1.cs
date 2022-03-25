using System.Diagnostics;
using System.Numerics;

namespace WarSimulation
{
    public partial class Form1 : Form
    {
        private RenderManager renderManager;

        private Random random = new Random();

        private const int FRAMES = 120;
        public Form1()
        {
            InitializeComponent();
            renderManager = new(this.Handle);
            for (int i = 0; i < 15; i++)
            {
                renderManager.renderObjects.Add(new SimulationCharacter("Test", random.Next(0, 50), new Vector2(0, 0), new Size(50, 50)) { Color = Color.Blue, futurePosition = new(200, 200)});
                renderManager.renderObjects.Add(new SimulationCharacter("Test2", random.Next(0, 50), new Vector2(400, 400), new Size(50, 50)) { Color = Color.Red, futurePosition = new(200, 200)});
            }
            Task.Run(() => 
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                long previosElapsedTime = 0;
                while (true) 
                {
                    if ((stopWatch.Elapsed.Milliseconds + 1) + (previosElapsedTime) >= 1000 / FRAMES)
                    {
                        renderManager.UpdateControls();
                        previosElapsedTime = stopWatch.Elapsed.Milliseconds;
                        stopWatch.Restart();
                    }
                } 
            });
        }
    }
}