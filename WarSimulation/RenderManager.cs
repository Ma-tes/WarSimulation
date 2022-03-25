using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarSimulation
{
    interface IRenderable 
    {

        public Task Update();

        public Task OnPaint(Graphics g);
    }

    internal sealed class GraphicsSwapper
    {
        private Bitmap GraphicsFront { get; set; }

        private Bitmap GraphicsBack { get; set; }

        public Bitmap[] SwapGraphics => new Bitmap[] { GraphicsFront, GraphicsBack };

        public GraphicsSwapper(Bitmap defaultGraphics) => (GraphicsFront, GraphicsBack) = (defaultGraphics, defaultGraphics);
    }

    internal sealed class RenderManager : UserControl //TODO: Create auto self adding to renderObjects 
    {

        protected override Size DefaultSize => new Size(800, 600);

        public IList<IRenderable> renderObjects = new List<IRenderable>();

        private GraphicsSwapper graphics;

        private Graphics graphicsHelper { get; set; }

        private Graphics defaultGraphics { get; set; }

        private int indexer = 0;

        public RenderManager(IntPtr hwnd)
        {
            if (hwnd != IntPtr.Zero)
                defaultGraphics = Graphics.FromHwnd(hwnd);
            else
                throw new Exception("Handle is not found");
            graphicsHelper = Graphics.FromImage(new Bitmap(800, 600));
            graphics = new GraphicsSwapper(new Bitmap(800, 600, graphicsHelper));
        }

        public async Task UpdateControls() 
        {
            int previousIndexer = 1 - indexer;
            graphicsHelper = Graphics.FromImage(graphics.SwapGraphics[indexer]);
            SwapBuffers(graphics.SwapGraphics[indexer], graphics.SwapGraphics[previousIndexer]);
            for (int i = 0; i < renderObjects.Count; i++)
            {
                await renderObjects[i].OnPaint(graphicsHelper);
                await renderObjects[i].Update();
            }
            for (int i = 0; i < UIElement.elements.Count; i++)
            {
                await UIElement.elements[i].OnPaint(graphicsHelper);
                await UIElement.elements[i].Update();
            }
            defaultGraphics.DrawImage(graphics.SwapGraphics[previousIndexer], new Point(0, 0));
            graphicsHelper.FillRectangle(Brushes.Black, new Rectangle() { Size = DefaultSize });
            indexer = indexer == 0 ? 1 : 0;
        }

        private void SwapBuffers(Bitmap bufferOne, Bitmap bufferTwo)
        {
            var oldGraphics = bufferOne; 
            bufferOne = bufferTwo;
            bufferTwo = oldGraphics;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //var paintEvent = new PaintEventArgs(windowHandler, new Rectangle() { Size = this.Size });
            //base.OnPaint(paintEvent);
        }
    }
}
