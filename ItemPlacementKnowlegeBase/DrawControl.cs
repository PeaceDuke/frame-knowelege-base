using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ItemPlacementKnowlegeBase
{
    public partial class DrawControl : UserControl
    {
        GridDrawer gridDrawer;


        [
        Category("Grid"),
        Description("Количество клеток по горизонтали"),
        EditorBrowsable(EditorBrowsableState.Always)
        ]
        public int WidthInCells
        {
            get { return gridDrawer.Width; }
            set { gridDrawer.Width = value; }
        }
        [
        Category("Grid"),
        Description("Количество клеток по вертикали"),
        EditorBrowsable(EditorBrowsableState.Always)
        ]
        public int HeightInCells
        {
            get { return gridDrawer.Height; }
            set { gridDrawer.Height = value; }
        }
        [
        Category("Grid"),
        Description("Размер одной ячейки в пикселях"),
        EditorBrowsable(EditorBrowsableState.Always)
        ]
        public int CellSize
        {
            get { return gridDrawer.CellSize; }
            set { gridDrawer.CellSize = value; }
        }
        public DrawControl()
        {
            InitializeComponent();
            gridDrawer = new GridDrawer(8, 4, 60, pictureBox, label);
        }

        private void DrawControl_Resize(object sender, EventArgs e)
        {
            ;
        }
    }
}
