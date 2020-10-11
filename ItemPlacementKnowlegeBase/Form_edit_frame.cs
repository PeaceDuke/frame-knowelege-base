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
        public FormType form_type;
        public enum FormType
        {
            Insert,
            Delete
        }

        public bool Child()
        {
            if (cb_parrent.SelectedIndex == -1)
                return false;
            else
                return true;
        }

        public Form_edit_frame(FormType type)
        {
            InitializeComponent();
            Initialize();
            switch (type)
            {
                case FormType.Insert:
                    bt_add.Text = @"Добавить";                   
                    break;
                case FormType.Delete:
                    bt_add.Text = @"Удалить";
                    break;
            }
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
                Frame frame = new Frame(tb_name.Text);
                if (cb_parrent.SelectedIndex != -1)
                    frame.Parent = Knowleges_edit[cb_parrent.SelectedItem.ToString()];
                Knowleges_edit.Frames.Add(frame);

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

        private void Form_edit_frame_Load(object sender, EventArgs e)
        {

        }

        private void Bt_abort_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
