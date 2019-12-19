using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ItemPlacementKnowlegeBase
{
    class GridDrawer : IDisposable
    {
        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; initGrid(); draw(); }
        }

        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; initGrid(); draw(); }
        }
        private int cellSize;
        public int CellSize
        {
            get { return cellSize; }
            set
            {
                cellSize = value;
                initGrid();
                defaultBitmap = createChessBoardBitmap(cellSize, 5);
                draw();
            }
        }

        PictureBox canvas;
        Label label;
        Bitmap defaultBitmap;

        Pen pen = new Pen(Color.Black, 1);
        List<Line> gridLines = new List<Line>();
        Point? selectexCell = null;
        Font font = new Font(FontFamily.Families[39], 13);
        public GridDrawer(int width, int height, int cellSize, PictureBox canvas, Label label)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.canvas = canvas;
            this.label = label;

            canvas.Width = width * cellSize + 1;
            canvas.Height = height * cellSize + 1;
            canvas.MouseMove += onMouseMove;
            canvas.MouseUp += onMouseUp;
            canvas.MouseLeave += onMouseLeave;
            canvas.Resize += onResize;

            initGrid();
            //create default bitmap
            defaultBitmap = createChessBoardBitmap(cellSize, 5);
                
            draw();
        }


        private void initGrid()
        {
            gridLines.Clear();
            //vertical lines
            for (var w = 0; w <= width; w++)
            {
                gridLines.Add(new Line(w * cellSize, 0, w * cellSize, height * cellSize));
            }

            //horizotal lines
            for (var h = 0; h <= height; h++)
            {
                gridLines.Add(new Line(0, h * cellSize, width * cellSize, h * cellSize));
            }
        }


        private void onMouseUp(object sender, MouseEventArgs e)
        {
            int cellX = e.X / cellSize;
            int cellY = e.Y / cellSize;
            var provider = KnowlegeBaseProvider.get();
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (Main_form.draggedData.Length != 0)
                    {
                        
                        provider.addFrame(Main_form.draggedData, cellX, cellY);
                        Main_form.draggedData = "";
                    }
                    break;
                case MouseButtons.Right:
                    provider.removeFrame(cellX, cellY);                    
                    break;                    
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / cellSize;
            int y = e.Y / cellSize;
            if (0 <= x && x < width && 0 <= y && y < height)
            {
                selectexCell = new Point(x, y);
            }
            else
            {
                selectexCell = null;
            }
            draw();
        }

        private void onMouseLeave(object sender, EventArgs e)
        {
            selectexCell = null;
            draw();
        }
        private void onResize(object sender, EventArgs e)
        {
            initGrid();
            draw();
        }

        public void draw()
        {
            var buffer = new Bitmap(canvas.Width, canvas.Height);
            var graphics = Graphics.FromImage(buffer);
            var filledCells = KnowlegeBaseProvider.get().getFramesToDraw();
            pen.Width = 1;

            //draw filled cells
            foreach (var cell in filledCells)
            {
                var bitmap = cell.bitmap;
                if(bitmap == null)
                {
                    bitmap = defaultBitmap;
                }
                graphics.DrawImage(bitmap, cell.x * cellSize, cell.y * cellSize, cellSize, cellSize);
            }


            //draw grid
            pen.Color = Color.Black;
            foreach (var line in gridLines)
            {
                graphics.DrawLine(pen, line.fromX, line.fromY, line.toX, line.toY);
            }

            ////draw frames in cells
            //pen.Color = Color.Black;
            //foreach (var cell in filledCells)
            //{
            //    graphics.DrawString(cell.name, font, pen.Brush, cell.x * cellSize, cell.y * cellSize);
            //}


            //draw selectex cell
            pen.Color = Color.Red;
            pen.Width = 3;
            if (selectexCell.HasValue)
            {
                var value = selectexCell.Value;
                graphics.DrawRectangle(pen, value.X * cellSize, value.Y * cellSize, cellSize, cellSize);
                var selectedCellValue = filledCells.Find(cell => cell.x == value.X && cell.y == value.Y);
                if(selectedCellValue != null)
                {
                    label.Text = selectedCellValue.name;
                }
                else
                {
                    label.Text = "";
                }
            }

            canvas.Image = buffer;
        }

        private class Line
        {
            public float fromX;
            public float fromY;
            public float toX; 
            public float toY;
            public Line(float fromX, float fromY, float toX, float toY)
            {
                this.fromX = fromX;
                this.fromY = fromY;
                this.toX = toX;
                this.toY = toY;
            }
        }

        public void Dispose()
        {
            canvas.Dispose();
            label.Dispose();
            defaultBitmap.Dispose();
        }

        private static Bitmap createChessBoardBitmap(int size, int cells)
        {
            var bitmap = new Bitmap(size, size);
            var cellSize = size / cells;
            Graphics gr = Graphics.FromImage(bitmap);
            for (int i = 0; i <= cells; i++)
                for (int j = 0; j <= cells; j++)
                {
                    var brush = (i % 2 == j % 2) ? Brushes.Red : Brushes.Yellow;
                    gr.FillRectangle(brush, i * cellSize, j * cellSize, cellSize, cellSize);
                }
            return bitmap;
        }
    }
}
