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
using ItemPlacementKnowlegeBase.Gui;

namespace ItemPlacementKnowlegeBase
{
    public partial class DrawControl : UserControl
    {
        GridDrawer gridDrawer;
        public DrawControl()
        {
            InitializeComponent();
            gridDrawer = new GridDrawer(pictureBox, label);
        }

        private void DrawControl_Resize(object sender, EventArgs e)
        {
            ;
        }
    }
}
