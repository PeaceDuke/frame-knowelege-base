using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Models;

namespace ItemPlacementKnowlegeBase
{    
    public partial class Form_edit_frame : Form
    {
        public KnowlegeBase Knowleges_edit = new KnowlegeBase();
      
        public Form_edit_frame()
        {
            InitializeComponent();
            Initialize();
        }
        public void Initialize()
        {
            bt_add.Enabled = false;
        }
        public string Name_frame()
        {
            return tb_name.Text;
        }
        private void Bt_add_Click(object sender, EventArgs e)
        {
            try
            {
                Knowleges_edit.AddFrame(tb_name.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Tb_name_TextChanged(object sender, EventArgs e)
        {
            if (tb_name.Text != "")
                bt_add.Enabled = true;
            else
                bt_add.Enabled = false;
        }
    }
}
