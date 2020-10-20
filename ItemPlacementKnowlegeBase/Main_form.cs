using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemPlacementKnowlegeBase
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();
        }

        private void bt_runApp_Click(object sender, EventArgs e)
        {
            Application_form appForm = new Application_form();
            this.Visible = false;
            appForm.ShowDialog();
            this.Visible = true;
        }
    }
}
