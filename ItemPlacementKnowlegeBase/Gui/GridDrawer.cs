using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ItemPlacementKnowlegeBase.Services;

namespace ItemPlacementKnowlegeBase.Gui
{
    class GridDrawer : IDisposable
    {
        public int Width { get; }
        public int Height { get; }
        public int CellSize { get; }

        PictureBox canvas;
        Label label;

        Pen pen = new Pen(Color.Black, 1);
        List<Line> gridLines = new List<Line>();
        Point? selectexCell = null;
        Font font = new Font(FontFamily.Families[39], 13);
        public GridDrawer(PictureBox canvas, Label label)
        {
            var field = KnowlegeBaseManager.get().loadField();
            Width = field.Width;
            Height = field.Heigth;
            CellSize = field.CellSize;
            this.canvas = canvas;
            this.label = label;

            canvas.Width = Width * CellSize + 1;
            canvas.Height = Height * CellSize + 1;
            canvas.MouseMove += onMouseMove;
            canvas.MouseUp += onMouseUp;
            canvas.MouseLeave += onMouseLeave;
            canvas.Resize += onResize;

            initGrid();
            draw();
        }


        private void initGrid()
        {
            gridLines.Clear();
            //vertical lines
            for (var w = 0; w <= Width; w++)
            {
                gridLines.Add(new Line(w * CellSize, 0, w * CellSize, Height * CellSize));
            }

            //horizotal lines
            for (var h = 0; h <= Height; h++)
            {
                gridLines.Add(new Line(0, h * CellSize, Width * CellSize, h * CellSize));
            }
        }


        private void onMouseUp(object sender, MouseEventArgs e)
        {
            int cellX = e.X / CellSize;
            int cellY = e.Y / CellSize;
            var provider = KnowlegeBaseManager.get();
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (Main_form.draggedData != null)
                        {
                            Item item = Main_form.draggedData;
                            Cell cell = new Cell(cellX, cellY, item);
                            if(!cellInField(cell, provider.loadField()))
                            {
                                break;
                            }
                            var sucsess = provider.placeItem(cell, item);
                            Main_form.draggedData = null;
                            if (!sucsess)
                            {
                                string reasoning = "не смог поставить предмет:\n";
                                foreach (var reason in provider.getLastReasoning())
                                {
                                    reasoning += reason + "\n";
                                }
                                MessageBox.Show(reasoning);
                            }
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        var cell = new Cell(cellX, cellY, null);
                        if(!cellInField(cell, provider.loadField()))
                        {
                            break;
                        }
                        var sucsess = provider.removeItem(cell);
                        if (!sucsess)
                        {
                            string reasoning = "не смог поставить убрать предмет:\n";
                            foreach (var reason in provider.getLastReasoning())
                            {
                                reasoning += reason + "\n";
                            }
                            MessageBox.Show(reasoning);
                        }
                        break;
                    }
            }
        }

        private bool cellInField(Cell cell, Field field)
        {
            return cell.X >= 0 && cell.X < field.Width && cell.Y >= 0 && cell.Y < field.Heigth;
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / CellSize;
            int y = e.Y / CellSize;
            if (0 <= x && x < Width && 0 <= y && y < Height)
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
            var filledCells = KnowlegeBaseManager.get().loadField().Cells.FindAll(_cell => _cell.Item != null);
            pen.Width = 1;

            //draw filled cells
            foreach (var cell in filledCells)
            {
                var bitmap = cell.Item.Bitmap;
                graphics.DrawImage(bitmap, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
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
                graphics.DrawRectangle(pen, value.X * CellSize, value.Y * CellSize, CellSize, CellSize);
                var selectedCellValue = filledCells.Find(_cell => _cell.X == value.X && _cell.Y == value.Y);
                if(selectedCellValue != null)
                {
                    label.Text = selectedCellValue.Item.Name;
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
        }

        
    }
}
